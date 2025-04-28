using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaMVP.Model
{
    public class AuthManager
    {
        private HttpClient client;
        public AuthManager()
        {
            client=Client.HttpClient;
        }
        public static User CurrentUser { get; set; }

        public async void SignIn(string username, string password)
        {
            try
            {
                string hashPwd = HashClass.HashMethod(password);
                var responce = await client.GetAsync($"SignIn/{username}/{hashPwd}");
            }
            catch
            {

            }
          
        }

        public async void SignUp(string username, string password)
        {
            try
            {

            }
            catch
            {

            }
            _context.Users.Add(new User()
            {
                Username = username,
                Password = HashClass.HashMethod(password)
            });
            await _context.SaveChangesAsync();
        }
    }
}
