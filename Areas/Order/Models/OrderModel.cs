namespace E_Commerce_Website.Areas.Order.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int CartID { get; set; }
        public int AddressID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
