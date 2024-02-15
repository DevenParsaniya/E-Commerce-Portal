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

        #region Order List (Pending)
        public IActionResult PendingOrderList()
        {
            DataTable dataTable = dalOrder.OrderPendingSelectAll();
            ViewBag.Complete = TempData["Complete"];
            return View(dataTable);
        }
        #endregion

        #region Order List (Completed)
        public IActionResult CompletedOrderList()
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

    }
}
