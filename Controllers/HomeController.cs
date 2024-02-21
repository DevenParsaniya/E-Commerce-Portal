using E_Commerce_Website.BAL;
using E_Commerce_Website.DAL.Cart;
using E_Commerce_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace E_Commerce_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Index()
        {
            //Cart_DAL dalCart = new Cart_DAL();
            //DataTable dataTable = dalCart.CartCount(Convert.ToInt32(CommonVariable.UserID()));
            //if (dataTable.Rows.Count > 0)
            //{
            //    foreach (DataRow dataRow in dataTable.Rows)
            //    {
            //        HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
            //        break;
            //    }
            //}
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Thank_You()
        {
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Yash Khokhar", "yashkhokhar28@gmail.com"));
            //message.To.Add(new MailboxAddress(CommenVariable.FirstName(), CommenVariable.Email()));
            //message.Subject = "Order Confirmation";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "Hiii"
            //};
            //using (var client = new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("FromEmail300@gmail.com", "YourPassword");

            //    client.Send(message);
            //    client.Disconnect(true);
            //}
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
