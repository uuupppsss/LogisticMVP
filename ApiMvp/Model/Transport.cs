using System;
using System.Collections.Generic;

namespace ApiMvp.Model;

public partial class Transport
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
}
