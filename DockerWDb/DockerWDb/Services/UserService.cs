using DockerWDb.Interfaces;
using DockerWDb.Models;

namespace DockerWDb.Services
{
    public class UserService : IUser
    {
        public Task<bool> EditUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> RegisterUser(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
