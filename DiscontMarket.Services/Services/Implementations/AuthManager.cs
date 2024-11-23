using DiscontMarket.ApiModels.Auth;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Abstractions;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DiscontMarket.Services.Services.Implementations
{
    public class AuthManager<T> : BaseTokenConstructor<T>, IAuthManager<T>, ITokenManager<T>
        where T : ApplicationUser
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<User> userManager,
            IConfiguration configuration)
        {
            ObjectValidator<UserManager<User>>.CheckIsNotNullObject(userManager);
            _userManager = userManager;

            ObjectValidator<IConfiguration>.CheckIsNotNullObject(configuration);
            _configuration = configuration;
        }

        public async Task<IBaseResponse<AuthResultStruct>> Login(LoginModel model)
        {
            try
            {
                ObjectValidator<LoginModel>.CheckIsNotNullObject(model);
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                    var token = GenerateToken(authClaims);
                    var refreshToken = GenerateRefreshToken();

                    if (int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays))
                    {
                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays); // Исправление

                        await _userManager.UpdateAsync(user);
                        var userId = user.Id;

                        return ResponseFactory<AuthResultStruct>.CreateSuccessResponse(new AuthResultStruct
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            RefreshToken = refreshToken,
                            Expiration = token.ValidTo,
                        });
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid configuration for refresh token validity");
                    }
                }
                throw new UnauthorizedAccessException("Access denied. User is not authorized.");
            }
            catch (InvalidOperationException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateUnauthorizedResponse(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateUnauthorizedResponse(ex);
            }
            catch (ArgumentException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<uint>> Register(RegisterModel model)
        {
            try
            {
                ObjectValidator<RegisterModel>.CheckIsNotNullObject(model);

                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                {
                    throw new UnauthorizedAccessException(" Thsit User already exists! Please check user Name");
                }


                var userUser = new User
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(userUser, model.Password);
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("User creation failed! Please check user details and try again." +
                        $"  Identity Errors: Enter correct password");
                }
                return ResponseFactory<uint>.CreateSuccessResponse(userUser.Id);
            }
            catch (InvalidOperationException ex)
            {
                return ResponseFactory<uint>.CreateUnauthorizedResponse(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                return ResponseFactory<uint>.CreateUnauthorizedResponse(ex);
            }
            catch (ArgumentException ex)
            {
                return ResponseFactory<uint>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<uint>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<AuthResultStruct>> UpdateToken(TokenModel tokenModel)
        {
            try
            {
                ObjectValidator<TokenModel>.CheckIsNotNullObject(tokenModel);
                if (tokenModel == null)
                {
                    throw new UnauthorizedAccessException("Invalid client request");
                }

                string accessToken = tokenModel.AccessToken;
                string refreshToken = tokenModel.RefreshToken;

                var principal = GetClaimsPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    throw new UnauthorizedAccessException("Invalid access token or refresh token");
                }

                string username = principal.Identity.Name;

                var user = await _userManager.FindByNameAsync(username);

                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    throw new UnauthorizedAccessException("Invalid access token or refresh token");
                }

                var newAccessToken = GenerateToken(principal.Claims.ToList());
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);


                return ResponseFactory<AuthResultStruct>.CreateSuccessResponse(new AuthResultStruct
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken,
                    Expiration = newAccessToken.ValidTo
                });
            }
            catch (InvalidOperationException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateUnauthorizedResponse(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateUnauthorizedResponse(ex);
            }
            catch (ArgumentException ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<AuthResultStruct>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> RevokeRefreshTokenByUsernameAsync(string username)
        {
            try
            {
                StringValidator.CheckIsNotNull(username);

                var user = await _userManager.FindByNameAsync(username);
                ObjectValidator<ApplicationUser>.CheckIsNotNullObject(user);

                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> RevokeAllRefreshTokensAsync()
        {
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    user.RefreshToken = null;
                    await _userManager.UpdateAsync(user);
                }
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        protected override string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        protected override JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            ObjectValidator<List<Claim>>.CheckIsNotNullObject(authClaims);

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            bool isTokenValidityInt = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            return new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(isTokenValidityInt ? tokenValidityInMinutes : 0),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
        }

        protected override ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string? token)
        {
            StringValidator.CheckIsNotNull(token);
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }
    }
}
