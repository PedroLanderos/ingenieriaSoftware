using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Responses;
using AuthenticationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Interfaces
{
    public interface IUser
    {
        Task<ApiResponse> Register(UserDTO user);
        Task<ApiResponse> DeleteUserAsync(int userId);
        Task<ApiResponse> UpdateUserAsync(UserDTO user);
        Task<UserEntity> GetUserByIdAsync(int userId);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<IEnumerable<UserDTO>> GetByCriteriaAsyync(Expression<Func<UserEntity, bool>> predicate);
        Task<ApiResponse> Login(LoginDTO loginDto);
    }
}
