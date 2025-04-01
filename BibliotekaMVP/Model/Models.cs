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
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public double Distance { get; set; }
        public double TravelTime { get; set; }
    }

    public class Transport
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string LicensePlate { get; set; } //номерной знак
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
