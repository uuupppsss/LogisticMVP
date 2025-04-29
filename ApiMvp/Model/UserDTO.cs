namespace ApiMvp.Model
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string? Token { get; set; }
    }
}
