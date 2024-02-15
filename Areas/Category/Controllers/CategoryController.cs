using E_Commerce_Website.Areas.Category.Models;
using E_Commerce_Website.DAL.Category;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Website.Areas.Category.Controllers
{
    [Area("Category")]
    [Route("Category/[controller]/[action]")]
    public class CategoryController : Controller
    {
        Category_DAL dalCategory = new Category_DAL();

        #region Category List (Select All)
        public IActionResult Category_List()
        {
            DataTable dataTable = dalCategory.CategorySelectAll();
            ViewBag.Save = TempData["Save"];
            ViewBag.Delete = TempData["Delete"];
            return View(dataTable);
        }
        #endregion

        #region Category Save (Insert & Update)
        public IActionResult CategorySave(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                if (dalCategory.CategorySave(categoryModel))
                {
                    TempData["Save"] = "Category Saved Successfully";
                    return RedirectToAction("Category_List");
                }
                else
                {
                    return RedirectToAction("Category_List");
                }
            }
            return View("Category_AddEdit");
        }
        #endregion

        #region Category Select By ID
        public IActionResult CategoryAdd(int CategoryID)
        {
            CategoryModel categoryModel = dalCategory.CategorySelectByID(CategoryID);
            if (categoryModel != null)
            {
                return View("Category_AddEdit", categoryModel);
            }
            else
            {
                return View("Category_AddEdit");
            }
        }
        #endregion

        #region Category Delete 
        public IActionResult CategoryDelete(int CategoryID)
        {
            bool isSuccess = dalCategory.CategoryDeleteByID(CategoryID);
            if (isSuccess)
            {
                TempData["Delete"] = "Category Deleted Successfully";
                return RedirectToAction("Category_List");
            }
            return RedirectToAction("Category_List");
        }
        #endregion
    }
}
