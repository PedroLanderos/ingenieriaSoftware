using DockerWDb.Models;
using DockerWDb.Responses;

namespace DockerWDb.Interfaces
{
    public interface IUser
    {
        Task<ApiResponse> Register(UserModel user);
        Task<ApiResponse> Login(LoginModel login);
        Task<UserModel> GetUser(int userId);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<ApiResponse> EditUserById(UserModel user);
    }
}
