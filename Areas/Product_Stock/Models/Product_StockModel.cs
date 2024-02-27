namespace E_Commerce_Website.Areas.Product_Stock.Models
{
    public class Product_StockModel
    {
        public int StockID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
    public class Product_StockFilterModel
    {
        public int StockID { get; set; }

        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }

    }
}
