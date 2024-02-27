using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Areas.Product.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public string? ProductCode { get; set; }

        public IFormFile? PhotoPath { get; set; }

        public string? ProductImage { get; set; }

        public int? MinimumOrderQuantity { get; set; }

        public int? MaximumOrderQuantity { get; set; }
        [Required]
        public int CategoryID { get; set; }

        public bool? IsActive { get; set; }

        public int? DiscountPercentage { get; set; }

        public int ProductRating { get; set; } = 5;

        public DateTime? CreatedAt { get; set; } 

        public DateTime? ModifiedAt { get; set; }
    }
    public class ProductFilterModel
    {
        public int CategoryID { get; set; }

        public string? ProductName { get; set; }
    }
    public class ProductDropDownModel
    {
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
