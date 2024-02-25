using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.Product_Stock.Models;

namespace E_Commerce_Website.DAL.Product_Stock
{
    public class Product_Stock_DALBase : DALHelper
    {
        #region Method : Product Stock SelectAll
        public DataTable Product_StockSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Stock_SelectAll");
                DataTable dataTable = new DataTable();
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

        #region Method : Product Stock Insert and Update
        public bool Product_StockSave(Product_StockModel product_StockModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (product_StockModel.StockID == 0 || product_StockModel.StockID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Stock_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, product_StockModel.ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, product_StockModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@StockQuantity", DbType.Int32, product_StockModel.StockQuantity);
                    sqlDatabase.AddInParameter(dbCommand, "@CreatedAt", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@ModifiedAt", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Stock_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@StockID", DbType.Int32, product_StockModel.StockID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, product_StockModel.ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, product_StockModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@StockQuantity", DbType.Int32, product_StockModel.StockQuantity);
                    sqlDatabase.AddInParameter(dbCommand, "@ModifiedAt", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Product Stock Select By ID
        public Product_StockModel Product_StockSelectByID(int StockID)
        {
            Product_StockModel product_StockModel = new Product_StockModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Stock_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@StockID", DbType.Int32, StockID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    product_StockModel.StockID = Convert.ToInt32(dataRow["StockID"]);
                    product_StockModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    product_StockModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    product_StockModel.StockQuantity = Convert.ToInt32(dataRow["StockQuantity"]);
                    product_StockModel.CreatedAt = Convert.ToDateTime(dataRow["CreatedAt"]);
                    product_StockModel.ModifiedAt = Convert.ToDateTime(dataRow["ModifiedAt"]);
                }
                return product_StockModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Category Delete By ID
        public bool Product_StockDeleteByID(int StockID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Stock_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "@StockID", DbType.Int32, StockID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
