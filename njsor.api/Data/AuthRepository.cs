using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<User> Login(string username, string Password)
        {
           var user= await _context.Users.FirstOrDefaultAsync(x=>x.Username== username);
           if (user == null)
           return null;
           if(!VerifyPasswordHash(Password,user.PasswordHash,user.PasswordSalt))
           return null;

           return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using ( var hvac= new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               
                var computedHash=hvac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i<computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string Password)
        {
           byte[] passwordHash,passwordSalt;
           CreatePasswordHash(Password,out passwordHash,out passwordSalt);
           user.PasswordHash=passwordHash;
           user.PasswordSalt=passwordSalt;
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using ( var hvac= new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hvac.Key;
                passwordHash=hvac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
           
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(x=>x.Username == username))
            return true;

            return false;
        }


    }
}