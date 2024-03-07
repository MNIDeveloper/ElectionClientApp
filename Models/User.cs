namespace ElectionApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Fname { get; set; } = null!;

        public string Lname { get; set; } = null!;

        public int? Position { get; set; }
    }
}
