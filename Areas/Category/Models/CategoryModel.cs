using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Areas.Category.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
    }
    public class CategoryDropDownModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
