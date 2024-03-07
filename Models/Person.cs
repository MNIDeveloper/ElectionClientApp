namespace ElectionApp.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; } = null!;

        public string MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Alias { get; set; }

        public int Address { get; set; }

        public DateTime RegDate { get; set; }

        public bool VFlag { get; set; }

        public string Pin { get; set; }
        public int? SocialSecurity { get; set; }
    }
}
