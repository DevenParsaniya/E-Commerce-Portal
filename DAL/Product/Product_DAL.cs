using E_Commerce_Website.Areas.Category.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.Product.Models;

namespace E_Commerce_Website.DAL.Product
{
    public class Product_DAL : Product_DALBase
    {
        #region Category DropDown
        public List<CategoryDropDownModel> CategoryDropDown()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_DropDown");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<CategoryDropDownModel> listOfCategories = new List<CategoryDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CategoryDropDownModel categoryDropDownModel = new CategoryDropDownModel();
                    categoryDropDownModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    categoryDropDownModel.CategoryName = dataRow["CategoryName"].ToString();
                    listOfCategories.Add(categoryDropDownModel);
                }
                return listOfCategories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : ProductFilter
        public DataTable ProductFilter(ProductFilterModel productFilterModel)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Filter");
                //sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productFilterModel.CategoryID);
                sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productFilterModel.ProductName);
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
