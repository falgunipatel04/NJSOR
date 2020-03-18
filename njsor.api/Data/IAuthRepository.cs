using  njsor.api.Model;
using System.Threading.Tasks;

namespace njsor.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User User,string Password);
         Task<User> Login(string  username,string Password);
         Task<bool> UserExist(string username);
    }
}