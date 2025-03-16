using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Application.Responses;
using AuthenticationApi.Domain.Entities;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.Repositories
{
    public class UserRepository(AuthenticationDbContext context, IConfiguration config) : IUser
    {
        public Task<ApiResponse> DeleteUserAsync(int userId)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetByCriteriaAsyync(Expression<Func<UserEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Login(LoginDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> Register(UserDTO user)
        {
            try
            {
                var getUser = await GetUserByEmailAsync(user.Email!);
                if (getUser is not null) return new ApiResponse(false, "Email already in use");


            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ApiResponse> UpdateUserAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
