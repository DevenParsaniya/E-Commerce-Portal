using E_Commerce_Website.BAL;
using E_Commerce_Website.DAL.Cart;
using E_Commerce_Website.DAL.Order;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.Order.Controllers
{
    [Area("Order")]
    [Route("Order/[controller]/[action]")]
    public class OrderController : Controller
    {
        Order_DAL dalOrder = new Order_DAL();
        Cart_DAL dalCart = new Cart_DAL();

        #region Order List (Pending)
        public IActionResult Order_PendingOrderList()
        {
            DataTable dataTable = dalOrder.OrderPendingSelectAll();
            ViewBag.Complete = TempData["Complete"];
            return View(dataTable);
        }
        #endregion

        #region Order List (Completed)
        public IActionResult Order_CompletedOrderList()
        {
            DataTable dataTable = dalOrder.OrderCompletedSelectAll();
            return View(dataTable);
        }
        #endregion

        #region Order Select By ID
        public IActionResult OrderSelectByID(int OrderID)
        {
            DataTable dataTable = dalOrder.OrderSelectByPK(OrderID);
            return View("Order_SingalOrder", dataTable);
        }
        #endregion

        #region Order Complete
        public IActionResult OrderComplete(int OrderID)
        {
            bool isSuccess = dalOrder.OrderComplete(OrderID);
            if (isSuccess)
            {
                TempData["Complete"] = "Order Completed Successfully";
                return RedirectToAction("Order_PendingOrderList");
            }
            return RedirectToAction("Order_PendingOrderList");
        }
        #endregion

        #region Order Insert
        public IActionResult Order_Insert(int UserID, int[] ProductIDs, int AddressID)
        {
            if (ModelState.IsValid)
            {
                if (dalOrder.OrderInsert(UserID, ProductIDs, AddressID))
                {
                    bool isSuccess = dalCart.Update_Order_Status(Convert.ToInt32(CommonVariable.UserID()));
                    if (isSuccess)
                    {
                        DataTable dataTable = dalCart.CartCount(Convert.ToInt32(CommonVariable.UserID()));
                        if (dataTable.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                HttpContext.Session.SetString("CartCount", dataRow["TotalCartItems"].ToString());
                                break;
                            }
                        }
                        return RedirectToAction("ThankYou", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ThankYou", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("ThankYou", "Home");
                }
            }
            return RedirectToAction("ThankYou", "Home");
        }
        #endregion
    }
}
