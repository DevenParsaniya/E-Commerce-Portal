using E_Commerce_Website.Areas.Cart.Models;
using E_Commerce_Website.BAL;
using E_Commerce_Website.DAL.Cart;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.Cart.Controllers
{
    [CheckAccess]
    [Area("Cart")]
    [Route("Cart/[controller]/[action]")]
    public class CartController : Controller
    {
        Cart_DAL dalCart = new Cart_DAL();

        #region Cart List
        public IActionResult CartList()
        {
            DataTable dataTable = dalCart.CartSelectAll(Convert.ToInt32(CommonVariable.UserID()));
            return View(dataTable);
        }
        #endregion

        #region Cart Insert
        public IActionResult CartInsert(int ProductID, int UserID)
        {
            if (ModelState.IsValid)
            {
                if (dalCart.CartInsert(ProductID, UserID))
                {
                    Cart_DAL dalCart = new Cart_DAL();
                    DataTable dataTable = dalCart.CartCount(Convert.ToInt32(CommonVariable.UserID()));
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
                            break;
                        }
                    }
                    return RedirectToAction("Product_Shopping_ProductList", "Product", new { area = "Product" });
                }
                else
                {
                    return RedirectToAction("Product_Shopping_ProductByID", "Product");
                }
            }
            return View("Category_AddEdit");
        }
        #endregion

        #region Increment Quantity
        public IActionResult Increment_Quantity(int ProductID)
        {
            bool isSuccess = dalCart.Increment_Quantity(ProductID);
            if (isSuccess)
            {
                return RedirectToAction("Cart_List");
            }
            return RedirectToAction("Cart_List");
        }
        #endregion

        #region Decrement Quantity
        public IActionResult Decrement_Quantity(int ProductID)
        {
            bool isSuccess = dalCart.Decrement_Quantity(ProductID);
            if (isSuccess)
            {
                return RedirectToAction("Cart_List");
            }
            return RedirectToAction("Cart_List");
        }
        #endregion

        #region Remove Cart Product
        public IActionResult Remove_Cart_Product(int ProductID)
        {
            bool isSuccess = dalCart.Remove_Cart_Product(ProductID);
            if (isSuccess)
            {
                Cart_DAL dalCart = new Cart_DAL();
                DataTable dataTable = dalCart.CartCount(Convert.ToInt32(CommonVariable.UserID()));
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
                        break;
                    }
                }
                return RedirectToAction("Cart_List");
            }
            return RedirectToAction("Cart_List");
        }
        #endregion
    }
}
