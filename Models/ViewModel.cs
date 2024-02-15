using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Models
{
    public class ViewModel : Controller
    {
        public DataTable CartTable { get; set; }
        public DataTable AddressTable { get; set; }
    }
}
