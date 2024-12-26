using DiscontMarket.ApiModels.DTO.EntityDTOs.User;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Services.Helpers.Constants;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;
using Microsoft.AspNetCore.Identity;

namespace DiscontMarket.Services.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IBaseResponse<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var users = _userManager.Users.ToList();
                ObjectValidator<IEnumerable<User>>.CheckIsNotNullObject(users);

                return ResponseFactory<IEnumerable<User>>.CreateSuccessResponse(users);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<IEnumerable<User>>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<User>>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<User>> GetUserByIdAsync(uint userId)
        {
            try
            {
                NumberValidator<uint>.IsNotZero(userId);

                var user = await _userManager.FindByIdAsync(userId.ToString());
                ObjectValidator<User>.CheckIsNotNullObject(user);

                return ResponseFactory<User>.CreateSuccessResponse(user);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<User>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<User>.CreateErrorResponse(ex);
            }
        }


        public async Task<IBaseResponse<string>> GetAdminsEmail()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(AdminInfo.adminName);

                ObjectValidator<User>.CheckIsNotNullObject(user);

                return ResponseFactory<string>.CreateSuccessResponse(user.Email);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<string>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<string>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> UpdateUserAsync(uint userId, UpdateUserDTO userDto)
        {
            try
            {
                ObjectValidator<UpdateUserDTO>.CheckIsNotNullObject(userDto);
                NumberValidator<uint>.IsNotZero(userId);

                var employee = await _userManager.FindByIdAsync(userId.ToString());
                if (employee is null)
                    throw new ArgumentNullException("User Not found");

                employee.Email = userDto.Email;
                employee.FirstName = userDto.FirstName;
                employee.LastName = userDto.LastName;
                employee.MiddleName = userDto.MiddleName;

                await _userManager.UpdateAsync(employee);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUserByIdAsync(uint userId)
        {
            try
            {
                NumberValidator<uint>.IsNotZero(userId);

                var user = await _userManager.FindByIdAsync(userId.ToString());
                ObjectValidator<User>.CheckIsNotNullObject(user);

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return ResponseFactory<bool>.CreateSuccessResponse(true);
                }
                else
                {
                    return ResponseFactory<bool>.CreateErrorResponse(new Exception("Ошибка при удалении пользователя"));
                }
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }

        public async Task<IBaseResponse<string>> UpdateAdminsEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(AdminInfo.adminName);

                ObjectValidator<User>.CheckIsNotNullObject(user);

                user.Email = email; 
                _userManager.UpdateAsync(user);

                return ResponseFactory<string>.CreateSuccessResponse(user.Email);
            }
            catch (ArgumentNullException ex)
            {
                return ResponseFactory<string>.CreateNotFoundResponse(ex);
            }
            catch (Exception ex)
            {
                return ResponseFactory<string>.CreateErrorResponse(ex);
            }
        }
    }
}
