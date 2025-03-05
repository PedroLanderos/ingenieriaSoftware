using DockerWDb.Models;

namespace DockerWDb.Interfaces
{
    public interface IUser
    {
        Task<UserModel> RegisterUser(UserModel user);
        Task<UserModel> LoginUser(LoginModel loginModel);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(long id);
        Task<bool> EditUser(UserModel user);
    }
}
