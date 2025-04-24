using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaMVP.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public int StatusId { get; set; }

        public DateTime OrderDate { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; } = null!;

        //public override string ToString()
        //{
        //    return "ID: " + Id + " | Название: " + ProductName;
        //}
    }

    public class Route
    {
        public int Id { get; set; }

        public string StartPoint { get; set; } = null!;

        public string EndPoint { get; set; } = null!;

        public double Distance { get; set; }

        public double TravelTime { get; set; }

        public int IdOrder { get; set; }

        public int IdTransport { get; set; }
    }

    public class Transport
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string LicensePlate { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }
    }
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
