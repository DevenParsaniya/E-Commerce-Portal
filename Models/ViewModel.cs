using E_Commerce_Website.Areas.Category.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Models
{
    public class ViewModel : Controller
    {
        public DataTable CartTable { get; set; }
        public DataTable AddressTable { get; set; }
        public CategoryDropDownModel CategoryList { get; set; }
        public DataTable ProductTable { get; set; }
    }
}
