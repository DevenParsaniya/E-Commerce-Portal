using E_Commerce_Website.Areas.Product.Models;
using E_Commerce_Website.Areas.Product_Stock.Models;
using E_Commerce_Website.DAL.Product;
using E_Commerce_Website.DAL.Product_Stock;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.Product_Stock.Controllers
{
    [Area("Product_Stock")]
    [Route("Product_Stock/[controller]/[action]")]
    public class Product_StockController : Controller
    {
        Product_Stock_DAL dalProduct_Stock = new Product_Stock_DAL();
        Product_DAL dalProduct = new Product_DAL();

        #region Product Stock List (Select All)
        public IActionResult Product_Stock_List()
        {
            DataTable dataTable = dalProduct_Stock.Product_StockSelectAll();
            ViewBag.Save = TempData["Save"];
            ViewBag.Delete = TempData["Delete"];
            return View(dataTable);
        }
        #endregion

        #region Product Stock Save (Insert & Update)
        public IActionResult Product_StockSave(Product_StockModel product_StockModel)
        {
            if (ModelState.IsValid)
            {
                if (dalProduct_Stock.Product_StockSave(product_StockModel))
                {
                    TempData["Save"] = "Product Stock Saved Successfully";
                    return RedirectToAction("Product_Stock_List");
                }
                else
                {
                    return RedirectToAction("Product_Stock_List");
                }
            }
            return View("Product_Stock_AddEdit");
        }
        #endregion

        #region Product Stock Select By ID
        public IActionResult Product_StockAdd(int StockID)
        {
            Product_StockModel product_StockModel = dalProduct_Stock.Product_StockSelectByID(StockID);
            ViewBag.CategoryList = dalProduct.CategoryDropDown();
            ViewBag.ProductList = dalProduct_Stock.ProductDropDown();
            if (product_StockModel != null)
            {
                return View("Product_Stock_AddEdit", product_StockModel);
            }
            else
            {
                ViewBag.CategoryList = dalProduct.CategoryDropDown();
                ViewBag.ProductList = dalProduct_Stock.ProductDropDown();
                return View("Product_Stock_AddEdit");
            }
        }
        #endregion

        #region Product Stock Delete 
        public IActionResult Product_StockDelete(int StockID)
        {
            bool isSuccess = dalProduct_Stock.Product_StockDeleteByID(StockID);
            if (isSuccess)
            {
                TempData["Delete"] = "Product Stock Deleted Successfully";
                return RedirectToAction("Product_Stock_List");
            }
            return RedirectToAction("Product_Stock_List");
        }
        #endregion

        #region Product Stock Filter
        public IActionResult Product_StockFilter(ProductFilterModel productFilterModel)
        {
            if (ModelState.IsValid)
            {
                DataTable dataTable = dalProduct_Stock.Product_StockFilter(productFilterModel);
                //ViewBag.CategoryList = dalProduct.CategoryDropDown();
                ModelState.Clear();
                return View("Product_Stock_List", dataTable);
            }
            else
            {
                // Handle invalid model state if needed
                return View("Product_Stock_List");
            }
        }
        #endregion
    }
}
