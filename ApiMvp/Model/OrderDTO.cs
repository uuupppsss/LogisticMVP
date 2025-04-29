namespace ApiMvp.Model
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public int StatusId { get; set; }
        public string? Status {  get; set; }
        //public List<ProductDTO>? Products { get; set; } = null;

        public DateTime? OrderDate { get; set; }
    }
}
