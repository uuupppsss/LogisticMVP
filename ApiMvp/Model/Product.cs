using System;
using System.Collections.Generic;

namespace ApiMvp.Model;

public partial class Product
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string ProductName { get; set; } = null!;

    public virtual ICollection<ProductsInOrder> ProductsInOrders { get; set; } = new List<ProductsInOrder>();
}
