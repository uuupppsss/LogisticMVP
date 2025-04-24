using System;
using System.Collections.Generic;

namespace ApiMvp.Model;

public partial class Order
{
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    public int StatusId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<ProductsInOrder> ProductsInOrders { get; set; } = new List<ProductsInOrder>();

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

    public virtual OrderStatus Status { get; set; } = null!;
}
