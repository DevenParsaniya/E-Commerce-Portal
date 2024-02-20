using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace E_Commerce_Website.DAL.Order
{
    public class Order_DALBase : DALHelper
    {
        #region Method : Order SelectAll (Pending)
        public DataTable OrderPendingSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Order_Pending_SelectAll");
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

        #region Method : Order SelectAll (Completed)
        public DataTable OrderCompletedSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Order_Completed_SelectAll");
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

        #region Method : Order SelectByPK
        public DataTable OrderSelectByPK(int OrderID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Order_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@OrderID", DbType.Int32, OrderID);
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

        #region Method : Order Complete
        public bool OrderComplete(int OrderID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Order_Complete");
                sqlDatabase.AddInParameter(dbCommand, "@OrderID", DbType.Int32, OrderID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Order Insert
        public bool OrderInsert(int UserID, int[] ProductIDs, int AddressID)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Order_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@AddressID", DbType.Int32, AddressID);
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                DataTable ProductIDsTable = new DataTable();
                ProductIDsTable.Columns.Add("ProductID", typeof(int));
                foreach (var productId in ProductIDs)
                {
                    ProductIDsTable.Rows.Add(productId);
                }
                // Convert DataTable to a comma-separated string of ProductIDs
                string ProductIDsString = string.Join(",", ProductIDsTable.AsEnumerable().Select(row => row.Field<int>("ProductID")));

                // Pass the string as a parameter
                sqlDatabase.AddInParameter(dbCommand, "@ProductIDs", DbType.String, ProductIDsString);
                sqlDatabase.AddInParameter(dbCommand, "@isCompleted", DbType.Boolean, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@OrderStatus", DbType.String, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Completed", DbType.DateTime, DBNull.Value);
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
