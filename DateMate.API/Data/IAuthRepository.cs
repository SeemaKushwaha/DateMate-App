using System.Threading.Tasks;
using DateMate.API.Models;

namespace DateMate.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username,string password);
        Task<User> Register(User user,string password);
        Task<bool> UserExists(string username);
        
    }
}