using DiscontMarket.ApiModels.Auth;
using DiscontMarket.ApiModels.DTO.EntityDTOs.User;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscontMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager<User> _authService;
        private readonly IUserService _userService;
        private readonly ITokenManager<User> _tokenManager;


        public UserController(IAuthManager<User> authManager, IUserService userService, ITokenManager<User> tokenManager)
        {
            _authService = authManager;
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet]
        public  IActionResult GetAll()
        {
            var response =  _userService.GetAllUsers();

            var userDtos = response.Data.Select(user => new CreateUserDTO
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ClientsVk = user.ClientsVk,
                ClientsWhatsapp = user.ClientsWhatsapp,
                ClientsTelegram = user.ClientsTelegram,
            });

            return Ok(userDtos);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(uint id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(uint id, UpdateUserDTO userDto)
        {
            var response = await _userService.UpdateUserAsync(id, userDto);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(uint id)
        {
            var response = await _userService.DeleteUserByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);

            return Ok(result);
        }

        [HttpPost]
        [Route("update-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel model)
        {
            var response = await _tokenManager.UpdateToken(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-email")]
        public async Task<IActionResult> UpdateEmail([FromBody] string emailEdit)
        {
            var response = await _userService.UpdateAdminsEmail(emailEdit);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-whatsapp")]
        public async Task<IActionResult> UpdateNumber([FromBody] string numberEdit)
        {
            var response = await _userService.UpdateAdminsNumber(numberEdit);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-number")]
        public async Task<IActionResult> UpdateWhatsapp([FromBody] string whatsappEdit)
        {
            var response = await _userService.UpdateAdminsWhatsApp(whatsappEdit);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-telegram")]
        public async Task<IActionResult> UpdateTelegram([FromBody] string telegramEdit)
        {
            var response = await _userService.UpdateAdminsTelegram(telegramEdit);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-vk")]
        public async Task<IActionResult> UpdateVk([FromBody] string vkEdit)
        {
            var response = await _userService.UpdateAdminsVk(vkEdit);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
    }
}
