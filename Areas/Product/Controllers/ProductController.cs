using E_Commerce_Website.Areas.Category.Models;
using E_Commerce_Website.Areas.Product.Models;
using E_Commerce_Website.DAL.Category;
using E_Commerce_Website.DAL.Product;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.Product.Controllers
{
    [Area("Product")]
    [Route("Product/[controller]/[action]")]
    public class ProductController : Controller
    {
        Product_DAL dalProduct = new Product_DAL();
        Category_DAL dalCategory = new Category_DAL();

        #region Product List (Active Select All)
        public IActionResult Product_List()
        {
            DataTable dataTable = dalProduct.ProductSelectAll();
            ViewBag.Save = TempData["Save"];
            ViewBag.Delete = TempData["Delete"];
            ViewBag.Update = TempData["Update"];
            return View(dataTable);
        }
        #endregion

        #region Product List (In Active Select All)
        public IActionResult DeletedProductList()
        {
            DataTable dataTable = dalProduct.ProductDeletedSelectAll();
            ViewBag.Retrive = TempData["Retrive"];
            return View(dataTable);
        }
        #endregion

        #region Product List Select All (User Side)
        public IActionResult ShoppingProductList()
        {
            DataTable dataTable = dalProduct.ProductSelectAll();
            ViewBag.Category_List = dalProduct.CategoryDropDown();
            return View(dataTable);
        }
        #endregion

        #region Product List Select By ID (User Side)
        public IActionResult ShoppingProductByID(int ProductID)
        {
            DataTable dataTable = dalProduct.ShoppingProductByID(ProductID);
            return View(dataTable);
        }
        #endregion

        #region Product Save (Insert & Update)
        public IActionResult ProductSave(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                if (dalProduct.ProductSave(productModel))
                {
                    TempData["Save"] = "Product Saved Successfully";
                    return RedirectToAction("Product_List");
                }
                else
                {
                    return RedirectToAction("Product_List");
                }
            }
            return View("Product_AddEdit");

        }
        #endregion

        #region Product Select By ID
        public IActionResult ProductAdd(int ProductID)
        {
            ProductModel productModel = dalProduct.ProductSelectByID(ProductID);
            ViewBag.CategoryList = dalProduct.CategoryDropDown();
            if (productModel != null)
            {
                return View("Product_AddEdit", productModel);
            }
            else
            {
                ViewBag.CategoryList = dalProduct.CategoryDropDown();
                return View("Product_AddEdit");
            }
        }
        #endregion

        #region Product Delete
        public IActionResult ProductDelete(int ProductID)
        {
            bool isSuccess = dalProduct.ProductDeleteByID(ProductID);
            if (isSuccess)
            {
                TempData["Delete"] = "Product Deleted Successfully";
                return RedirectToAction("Product_List");
            }
            return RedirectToAction("Product_List");
        }
        #endregion

        #region Product Retrive
        public IActionResult ProductRetrive(int ProductID)
        {
            bool isSuccess = dalProduct.ProductRetrive(ProductID);
            if (isSuccess)
            {
                TempData["Retrive"] = "Product Retrived Successfully";
                return RedirectToAction("Product_Deleted_List");
            }
            return RedirectToAction("Product_Deleted_List");
        }
        #endregion

        #region Multiple Product Delete 
        public IActionResult DeleteMultipleProducts(int[] ProductIDs)
        {
            Console.WriteLine(ProductIDs[0]);
            bool isSuccess = dalProduct.DeleteMultipleProducts(ProductIDs);
            if (isSuccess)
            {
                TempData["Delete"] = "Selected Products Deleted Successfully";
                return RedirectToAction("Product_List");
            }
            else
            {
                return RedirectToAction("Product_List");
            }
        }
        #endregion

        #region Product Filter
        public IActionResult ProductFilter(ProductFilterModel productFilterModel)
        {
            if (ModelState.IsValid)
            {
                DataTable dataTable = dalProduct.ProductFilter(productFilterModel);
                //ViewBag.CategoryList = dalProduct.CategoryDropDown();
                ModelState.Clear();
                return View("Product_List", dataTable);
            }
            else
            {
                // Handle invalid model state if needed
                return View("Product_List");
            }
        }
        #endregion
    }
}
