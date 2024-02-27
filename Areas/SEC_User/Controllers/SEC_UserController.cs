using E_Commerce_Website.Areas.SEC_User.Models;
using E_Commerce_Website.DAL.SEC_User;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("SEC_User/[controller]/[action]")]
    public class SEC_UserController : Controller
    {
        SEC_User_DAL dalSEC_User = new SEC_User_DAL();

        #region User Sign In
        public IActionResult SEC_User_SignIn()
        {
            return View();
        }
        #endregion

        #region User Sign Up
        public IActionResult SEC_User_SignUp()
        {
            return View();
        }
        #endregion

        #region User List (Select All)
        public IActionResult SEC_User_List()
        {
            DataTable dataTable = dalSEC_User.SEC_UserSelectALL();
            return View("SEC_User_List", dataTable);
        }
        #endregion

        #region User Select By ID
        public IActionResult SEC_UserByID(int UserID)
        {
            DataTable dataTable = dalSEC_User.SEC_UserSelectByID(UserID);
            return View("SEC_User_SingalUser", dataTable);
        }
        #endregion

        #region Login
        [HttpPost]
        public IActionResult Login(SEC_UserModel sEC_UserModel)
        {
            if (sEC_UserModel.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }
            if (sEC_UserModel.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }

            if (TempData["UserNameError"] != null || TempData["PasswordError"] != null)
            {
                return RedirectToAction("SEC_User_SignIn", "SEC_User");
            }
            else
            {
                DataTable dataTable = dalSEC_User.SEC_User_SelectByUserNamePassword(sEC_UserModel.UserName, sEC_UserModel.Password);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        HttpContext.Session.SetString("UserID", dataRow["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dataRow["UserName"].ToString());
                        HttpContext.Session.SetString("Password", dataRow["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dataRow["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dataRow["LastName"].ToString());
                        HttpContext.Session.SetString("UserEmail", dataRow["UserEmail"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dataRow["IsAdmin"].ToString());
                        HttpContext.Session.SetString("IsActive", dataRow["IsActive"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "UserName or Password is Invalid!";
                    return RedirectToAction("SEC_User_SignIn");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "True")
                {
                    Console.WriteLine(HttpContext.Session.GetString("UserName"));
                    return RedirectToAction("SEC_Admin_Dashboard", "SEC_Admin", new { area = "SEC_Admin" });
                }
                else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        public IActionResult Register(SEC_UserModel sEC_UserModel)
        {
            if (sEC_UserModel.UserName == null)
            {
                TempData["UserNameError"] = "User Name is Required!";
            }
            if (sEC_UserModel.Password == null)
            {
                TempData["PasswordError"] = "Password is Required!";
            }
            if (sEC_UserModel.FirstName == null)
            {
                TempData["FirstNameError"] = "First  Name is Required!";
            }
            if (sEC_UserModel.LastName == null)
            {
                TempData["LastNameError"] = "Last Name is Required!";
            }
            if (sEC_UserModel.UserEmail == null)
            {
                TempData["EmailAddressError"] = "Email Address is Required!";
            }
            if (sEC_UserModel.MobileNumber == null)
            {
                TempData["MobileNumberError"] = "Mobile Number is Required!";
            }

            if (TempData["UserNameError"] != null || TempData["PasswordError"] != null || TempData["FirstNameError"] != null || TempData["LastNameError"] != null || TempData["EmailAddressError"] != null || TempData["MobileNumberError"] != null)
            {
                return RedirectToAction("SEC_User_SignUp", "SEC_User");
            }
            else
            {
                bool IsSuccess = dalSEC_User.SEC_User_Register(sEC_UserModel.UserName, sEC_UserModel.Password, sEC_UserModel.FirstName, sEC_UserModel.LastName, sEC_UserModel.MobileNumber, sEC_UserModel.UserEmail);
                if (IsSuccess)
                {
                    return RedirectToAction("SEC_User_SignIn");
                }
                else
                {
                    return RedirectToAction("SEC_User_SignUp");
                }
            }
        }
        #endregion
    }
}
