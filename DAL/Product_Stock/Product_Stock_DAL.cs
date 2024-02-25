using E_Commerce_Website.Areas.Product.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
namespace E_Commerce_Website.DAL.Product_Stock
{
    public class Product_Stock_DAL : Product_Stock_DALBase
    {
        #region Product DropDown
        public List<ProductDropDownModel> ProductDropDown()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_DropDown");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<ProductDropDownModel> listOfProducts = new List<ProductDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ProductDropDownModel productDropDownModel = new ProductDropDownModel();
                    productDropDownModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    productDropDownModel.ProductName = dataRow["ProductName"].ToString();
                    listOfProducts.Add(productDropDownModel);
                }
                return listOfProducts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : ProductFilter
        public DataTable Product_StockFilter(ProductFilterModel productFilterModel)
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
