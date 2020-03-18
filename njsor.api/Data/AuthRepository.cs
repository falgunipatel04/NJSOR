using System.Threading.Tasks;
using njsor.api.Model;

namespace njsor.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public Task<User> Login(string username, string Password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Register(User User, string Password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExist(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}