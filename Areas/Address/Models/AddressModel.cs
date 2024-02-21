namespace E_Commerce_Website.Areas.Address.Models
{
    public class AddressModel
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public int? Pincode {  get; set; }
        public int IsDefault { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
