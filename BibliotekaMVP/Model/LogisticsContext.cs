using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaMVP.Model
{
    public class LogisticsContext
    {

        public LogisticsContext()
        {
            
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
