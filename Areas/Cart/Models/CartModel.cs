namespace E_Commerce_Website.Areas.Cart.Models
{
    public class CartModel
    {
        public int CartID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public int ProductQuantity { get; set; }

        public bool IsPurchased { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
