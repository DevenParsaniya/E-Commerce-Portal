using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.Cart.Models;
using System.Reflection;

namespace E_Commerce_Website.DAL.Cart
{
    public class Cart_DALBase : DALHelper
    {
        #region Method : Cart SelectAll
        public DataTable CartSelectAll(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_SelectAll");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, Convert.ToInt32(UserID));
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

        #region Method : Cart Insert

        public bool CartInsert(int ProductID, int UserID)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand1 = sqlDatabase.GetStoredProcCommand("PR_Cart_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand1, "@ProductID", DbType.Int32, ProductID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand1))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_Increment_Quantity");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductQuantity", DbType.Int32, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@IsPurchased", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@CreatedAt", DbType.DateTime, DBNull.Value);
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

        #region Method : Increment Quantity
        public bool Increment_Quantity(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_Increment_Quantity");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Method : Decrement Quantity
        public bool Decrement_Quantity(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_Decrement_Quantity");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region Method : Remove Cart Product
        public bool Remove_Cart_Product(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Cart Count
        public DataTable CartCount(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_CartCount");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
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

        #region Method : Update Order Status
        public bool Update_Order_Status(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cart_UpdateByPK");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
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
