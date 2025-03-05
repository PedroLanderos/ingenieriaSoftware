using DockerWDb.Models;
using DockerWDb.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DockerWDb.Data;
using DockerWDb.Responses;

namespace DockerWDb.Services
{
    public class UserService(AppDbContext context, IConfiguration config) : IUser
    {
        public async Task<UserModel> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user is null ? null! : user!;
        }
        public async Task<ApiResponse> EditUserById(UserModel user)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(user.Id);

                if (existingUser == null)
                    return new ApiResponse(false, "User not found");

                // Actualizar las propiedades del usuario
                existingUser.Name = user.Name ?? existingUser.Name;
                existingUser.Email = user.Email ?? existingUser.Email;

                // Si se proporciona una nueva contraseña, encriptarla
                if (!string.IsNullOrEmpty(user.Password))
                {
                    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                // Actualizar el rol del usuario si es necesario
                existingUser.Role = user.Role ?? existingUser.Role;

                // Guardar los cambios en la base de datos
                context.Users.Update(existingUser);
                await context.SaveChangesAsync();

                return new ApiResponse(true, "User updated successfully");
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y devolver un mensaje de error
                return new ApiResponse(false, $"Error while updating user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return users;
            }
            catch (Exception)
            {

                throw new Exception("error while getting all users");
            }
        }

        public async Task<UserModel> GetUser(int userId)
        {
            try
            {
                var user = await context.Users.FindAsync(userId);

                if (user == null) return null!;
                else return user;
            }
            catch (Exception)
            {

                throw new Exception("error while get user");
            }
        }
        private string GenerateToken(UserModel user)
        {
            var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name!),
                new(ClaimTypes.Email, user.Email!),
                new("UserId", user.Id.ToString()) // Agrega el userId como claim
            };

            if (!string.IsNullOrEmpty(user.Role) && !Equals("string", user.Role))
                claims.Add(new(ClaimTypes.Role, user.Role!));

            var token = new JwtSecurityToken(
                issuer: config["Authentication:Issuer"],
                audience: config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApiResponse> Login(LoginModel login)
        {
            try
            {
                UserModel user = await GetUserByEmail(login.Email!);
                bool verifyPassword = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

                if (!verifyPassword)
                    return new ApiResponse(false, "invalid credentials");

                string token = GenerateToken(user);
                return new ApiResponse(true, token);


            }
            catch (Exception)
            {

                throw new Exception("error while login");
            }
        }

        public async Task<ApiResponse> Register(UserModel user)
        {
            try
            {
                var getUser = await GetUserByEmail(user.Email!);
                if (getUser is not null) return new ApiResponse(false, "user already exists");

                var result = context.Users.Add(new UserModel()
                {
                    Name = user!.Name,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Role = user.Role
                });

                await context.SaveChangesAsync();

                if (result.Entity.Id > 0) return new ApiResponse(true, "User registered");
                else return new ApiResponse(false, "error with data");
            }
            catch (Exception)
            {

                throw new Exception("error while register");
            }
        }
    }
}
