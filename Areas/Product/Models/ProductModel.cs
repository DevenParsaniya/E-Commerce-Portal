namespace E_Commerce_Website.Areas.Product.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public double ProductPrice { get; set; }
        public string? ProductCode { get; set; }

        public IFormFile? UploadImage { get; set; }

        public string? ProductImage { get; set; }
        public int? MinimumOrderQuantity { get; set; }
        public int? MaximumOrderQuantity { get; set; }

        public int CategoryID { get; set; }

        public bool? IsActive { get; set; }

        public int? DiscountPercentage { get; set; }

        public int ProductRating { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
    public class ProductFilterModel
    {
        public int? CategoryID { get; set; }
        public string? ProductName { get; set; }
    }
}
