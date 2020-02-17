using IRunes.Data;
using IRunes.Models;
using SIS.MvcFramework;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRunes.Services
{
    public class UsersService : IUsersService
    {
        private readonly RunesDbContext db;

        public UsersService(RunesDbContext db)
        {
            this.db = db;
        }

        public bool EmailExists(string email)
        {
            return db.Users.Any(x => x.Email == email);
        }
        
        public string GetUserIdUsername(string username, string password)
        {
            var hashPassword = Hash(password);

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashPassword);
            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public string GetUserIdEmail(string email, string password)
        {
            var hashPassword = Hash(password);

            var user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == hashPassword);
            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public string GetUserName(string userId)
        {
            var username = db.Users.Where(x => x.Id == userId).Select(x => x.Username).FirstOrDefault();
            return username;
        }

        public void Register(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = Hash(password),
                Role = IdentityRole.User,
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return db.Users.Any(x => x.Username == username);
        }

        private string Hash(string input)
        {
            var crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
