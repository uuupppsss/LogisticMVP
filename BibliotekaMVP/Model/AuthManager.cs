using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaMVP.Model
{
    public class AuthManager
    {
        LogisticsContext _context;
        public AuthManager()
        {
            _context = new LogisticsContext("TestDataBase123456789");
        }
        public static User CurrentUser { get; set; }

        public async void SignIn(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return;
            string hashPwd=HashClass.HashMethod(password);
            if (hashPwd != user.Password) return;
            CurrentUser = user;
        }

        public async void SignUp(string username, string password)
        {
            _context.Users.Add(new User()
            {
                Username = username,
                Password = HashClass.HashMethod(password)
            });
            await _context.SaveChangesAsync();
        }
    }
}
