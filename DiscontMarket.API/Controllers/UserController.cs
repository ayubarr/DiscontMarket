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
            var response = await _userService.UpdateAdminProperty(
                user => user.Email,           
                (user, value) => user.Email = value, 
                emailEdit                  
            );    
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-support")]
        public async Task<IActionResult> UpdateSupportContacts([FromBody] string techcontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.SupportContacts,           
                (user, value) => user.SupportContacts = value, 
                techcontactsEdit                  
            );    
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-vk")]
        public async Task<IActionResult> UpdateVk([FromBody] string vkcontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.ClientsVk,           
                (user, value) => user.ClientsVk = value, 
                vkcontactsEdit                  
            );    
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-telegram")]
        public async Task<IActionResult> UpdateTelegram([FromBody] string tgcontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.ClientsTelegram,           
                (user, value) => user.ClientsTelegram = value, 
                tgcontactsEdit                  
            );    
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-whatsapp")]
        public async Task<IActionResult> UpdateWhatsapp([FromBody] string wtcontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.ClientsWhatsapp,           
                (user, value) => user.ClientsWhatsapp = value, 
                wtcontactsEdit                  
            );
            
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-worktime")]
        public async Task<IActionResult> UpdateWorkTime([FromBody] string timecontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.WorkTimeInfo,           
                (user, value) => user.WorkTimeInfo = value, 
                timecontactsEdit                  
            );
            
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-number")]
        public async Task<IActionResult> UpdateNumber([FromBody] string phonecontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.PhoneNumber,           
                (user, value) => user.PhoneNumber = value, 
                phonecontactsEdit                  
            );
            
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        

        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-returns-text")]
        public async Task<IActionResult> UpdateReturnsText([FromBody] string returncontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.ReturnsText,           
                (user, value) => user.ReturnsText = value, 
                returncontactsEdit                  
            );
            
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-adress")]
        public async Task<IActionResult> UpdateWhatsapp([FromBody] AdressData adressData)
        {
            var textResponse = await _userService.UpdateAdminProperty(
                user => user.TextAdress,           
                (user, value) => user.TextAdress = value, 
                adressData.textAdress                  
            );
            
            var linkResponse = await _userService.UpdateAdminProperty(
                user => user.HrefAdress,           
                (user, value) => user.HrefAdress = value, 
                adressData.hrefAdress                  
            );
            
            var frameResponse = await _userService.UpdateAdminProperty(
                user => user.HrefmapAdress,           
                (user, value) => user.HrefmapAdress = value, 
                adressData.hrefmapAdress                  
            );

            if (textResponse.IsSuccess && linkResponse.IsSuccess && frameResponse.IsSuccess)
            {
                return Ok(textResponse);
            }
            
            return Unauthorized(textResponse.Message);
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("update-contacts")]
        public async Task<IActionResult> UpdateContacts([FromBody] string textinfocontactsEdit)
        {
            var response = await _userService.UpdateAdminProperty(
                user => user.ContactInfoText,           
                (user, value) => user.ContactInfoText = value, 
                textinfocontactsEdit                  
            );
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }
        
        [HttpGet]
        [Route("get-user-data")]
        public async Task<IActionResult> GetUserData()
        {
            var response = await _userService.GetUserData(); 
            
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return Unauthorized(response.Message);
        }
    }
}
