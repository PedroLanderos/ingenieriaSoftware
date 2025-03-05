using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.Mappers
{
    public class UserMapper
    {
        public static UserEntity ToEntity(UserDTO userDTO) => new()
        {
            Name = userDTO.Name,
            TelephoneNumber = userDTO.TelephoneNumber,
            Adress = userDTO.Adress,
            Email = userDTO.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
            Role = userDTO.Role
        };

        public static UserDTO FromEntity(UserEntity user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            TelephoneNumber = user.TelephoneNumber,
            Adress = user.Adress,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };

        public static IEnumerable<UserDTO> FromEntityList(IEnumerable<UserEntity> users)
        {
            return users.Select(user => FromEntity(user));
        }
    }
}
