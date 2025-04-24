namespace ApiMvp.Model
{
    public class TransportDTO
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string LicensePlate { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }
    }
}
