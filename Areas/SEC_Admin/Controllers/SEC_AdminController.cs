using E_Commerce_Website.BAL;
using E_Commerce_Website.DAL.SEC_Admin;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.SEC_Admin.Controllers
{
    [CheckAccess]
    [Area("SEC_Admin")]
    [Route("SEC_Admin/[controller]/[action]")]
    public class SEC_AdminController : Controller
    {
        #region Admin Dashboard
        public IActionResult SEC_Admin_Dashboard()
        {
            SEC_Admin_DAL dalSEC_Admin = new SEC_Admin_DAL();
            DataTable dataTable = dalSEC_Admin.SEC_Admin_DashBoard();
            return View(dataTable);
        }
        #endregion

        #region Product Dashboard
        public IActionResult SEC_Product_Dashboard()
        {
            SEC_Admin_DAL dalSEC_Admin = new SEC_Admin_DAL();
            DataTable dataTable = dalSEC_Admin.SEC_Admin_DashBoard();
            return View(dataTable);
        }
        #endregion
    }
}
