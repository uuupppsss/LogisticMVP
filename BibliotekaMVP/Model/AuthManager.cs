using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
                string hashUsername= HashClass.HashMethod(username);
                var responce = await client.GetAsync($"Users/SignIn/{hashUsername}/{hashPwd}");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                    CurrentUser = null;
                }
                else
                {
                    //success
                    CurrentUser = await responce.Content.ReadFromJsonAsync<User>();
                    Client.SetToken(CurrentUser.Token);
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
