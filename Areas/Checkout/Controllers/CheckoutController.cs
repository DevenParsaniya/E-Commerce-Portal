using E_Commerce_Website.Areas.Address.Models;
using E_Commerce_Website.BAL;
using E_Commerce_Website.DAL.Checkout;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Website.Models;
using System.Data;

namespace E_Commerce_Website.Areas.Checkout.Controllers
{
    [CheckAccess]
    [Area("Checkout")]
    [Route("Checkout/[controller]/[action]")]
    public class CheckoutController : Controller
    {
        Checkout_DAL dalCheckout = new Checkout_DAL();

        #region Checkout
        public IActionResult Checkout()
        {
            DataTable dataTable1 = dalCheckout.Checkout(Convert.ToInt32(CommonVariable.UserID()));
            DataTable dataTable2 = dalCheckout.AddressSelectAll();
            ViewModel viewModel = new ViewModel()
            {
                AddressTable = dataTable2,
                CartTable = dataTable1
            };
            return View(viewModel);
        }
        #endregion

        #region Billing
        public IActionResult Billing()
        {
            return View();
        }
        #endregion

        #region Address Insert
        public IActionResult AddressInsert(AddressModel addressModel, int UserID)
        {
            if (ModelState.IsValid)
            {
                if (dalCheckout.AddressInsert(addressModel, UserID))
                {
                    return RedirectToAction("Checkout");
                }
                else
                {
                    return RedirectToAction("Checkout");
                }
            }
            return RedirectToAction("Checkout");
        }
        #endregion
    }
}
