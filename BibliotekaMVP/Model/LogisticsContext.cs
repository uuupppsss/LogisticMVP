using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibliotekaMVP.Model
{
    public class LogisticsContext
    {
        private HttpClient client;
        public LogisticsContext()
        {
            client = Client.HttpClient;
        }

        //ORDERS    
        public async Task<List<Order>> GetOrders()
        {
            try
            {
                var responce = await client.GetAsync("Orders");
                if (!responce.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<List<Order>>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task AddOrder(Order order)
        {
            try
            {
                string json=JsonSerializer.Serialize(order);
                var responce = await client.PostAsync("Orders", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task DeleteOrder(int order_id)
        {
            try
            {
                var responce = await client.DeleteAsync($"Orders/{order_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task UpdateOrder(Order order)
        {
            try
            {
                string json = JsonSerializer.Serialize(order);
                var responce = await client.PutAsync($"Orders/{order.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task<List<string>> GetOrderStatuses()
        {
            try
            {
                var responce = await client.GetAsync($"Orders/Status");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                    return null;
                }
                else
                {
                    //success
                    return await responce.Content.ReadFromJsonAsync<List<string>>();
                }
            }
            catch
            {
                //error
                return null;
            }
        }

        //TRANSPORT
        public async Task<List<Transport>> GetTransports()
        {
            try
            {
                var responce = await client.GetAsync("Transports");
                if (!responce.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<List<Transport>>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task AddTransport(Transport transport)
        {
            try
            {
                
                string json = JsonSerializer.Serialize(transport);
                var responce = await client.PostAsync("Transports", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task DeleteTransport(int transport_id)
        {
            try
            {
                var responce = await client.DeleteAsync($"Transports/{transport_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task UpdateTRansport(Transport transport)
        {
            try
            {
                string json = JsonSerializer.Serialize(transport);
                var responce = await client.PutAsync($"Transports/{transport.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        //ROUTES
        public async Task<List<Route>> GetRoutes()
        {
            try
            {
                var responce = await client.GetAsync("Routes");
                if (!responce.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<List<Route>>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task AddRoutes(Route route)
        {
            try
            {
                Client.SetToken(AuthManager.CurrentUser.Token);
                string json = JsonSerializer.Serialize(route);
                var responce = await client.PostAsync("Routes", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task DeleteRoute(int route_id)
        { 
            try
            {
                var responce = await client.DeleteAsync($"Routes/{route_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task UpdateRoute(Route route)
        {
            try
            {
                string json = JsonSerializer.Serialize(route);
                var responce = await client.PutAsync($"Routes/{route.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        //PRODUCTS
        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var responce = await client.GetAsync("Products");
                if (!responce.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return await responce.Content.ReadFromJsonAsync<List<Product>>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                string json = JsonSerializer.Serialize(product);
                var responce = await client.PostAsync("Products", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task DeleteProduct(int product_id)
        {
            try
            {
                var responce = await client.DeleteAsync($"Products/{product_id}");
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                string json = JsonSerializer.Serialize(product);
                var responce = await client.PutAsync($"Products/{product.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!responce.IsSuccessStatusCode)
                {
                    //error
                }
                else
                {
                    //success
                }
            }
            catch
            {
                //error
            }
        }

        //private string _filename;
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Route> Routes { get; set; }
        //public DbSet<Transport> Transports { get; set; }
        //public DbSet<User> Users { get; set; }

        //public LogisticsContext(string filename)
        //{
        //    _filename = filename;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Database");
        //    Directory.CreateDirectory(sqlitePath);
        //    var filename = Path.Combine(sqlitePath, _filename);
        //    if (!File.Exists(filename))
        //        File.Create(filename);
        //    optionsBuilder.UseSqlite($"Data Source={filename}");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
