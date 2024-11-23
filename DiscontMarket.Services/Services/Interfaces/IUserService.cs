﻿using DiscontMarket.ApiModels.DTO.EntityDTOs.User;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.Services.Services.Interfaces
{
    public interface IUserService
    {   
        IBaseResponse<IEnumerable<User>> GetAllUsers();

        Task<IBaseResponse<User>> GetUserByIdAsync(uint userId);
       
        Task<IBaseResponse<bool>> UpdateUserAsync(uint userId, UpdateUserDTO userDto);
    
        Task<IBaseResponse<bool>> DeleteUserByIdAsync(uint userId);
    }
}