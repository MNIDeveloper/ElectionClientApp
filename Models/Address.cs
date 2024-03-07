namespace ElectionApp.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public string Street { get; set; }

        public string Village { get; set; }

        public string Parish { get; set; }

        public string Postcode { get; set; }

        public int Constituancy { get; set; }

        public int IsCurrent { get; set; }

        public int PersonId { get; set; }
        public DateTime? DateMoved { get; set; }
    }
}
