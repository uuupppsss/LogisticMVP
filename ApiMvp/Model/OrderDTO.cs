namespace ApiMvp.Model
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public int StatusId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
