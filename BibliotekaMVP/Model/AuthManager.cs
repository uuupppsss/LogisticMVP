using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                    CurrentUser = null;
                }
                else
                {
                    //success
                    CurrentUser = await responce.Content.ReadFromJsonAsync<User>();
                }

            }
            catch
            {
                //error
                CurrentUser = null;
            }
          
        }
    }
}
