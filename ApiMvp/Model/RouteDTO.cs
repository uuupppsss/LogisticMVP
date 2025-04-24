namespace ApiMvp.Model
{
    public class RouteDTO
    {
        public int Id { get; set; }

        public string StartPoint { get; set; } = null!;

        public string EndPoint { get; set; } = null!;

        public double Distance { get; set; }

        public double TravelTime { get; set; }

        public int IdOrder { get; set; }

        public int IdTransport { get; set; }
    }
}
