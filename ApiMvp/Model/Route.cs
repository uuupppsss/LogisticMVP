using System;
using System.Collections.Generic;

namespace ApiMvp.Model;

public partial class Route
{
    public int Id { get; set; }

    public string StartPoint { get; set; } = null!;

    public string EndPoint { get; set; } = null!;

    public double Distance { get; set; }

    public double TravelTime { get; set; }

    public int IdOrder { get; set; }

    public int IdTransport { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Transport IdTransportNavigation { get; set; } = null!;
}
