using E_Commerce_Website.Areas.Address.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace E_Commerce_Website.DAL.Checkout
{
    public class Checkout_DALBase : DALHelper
    {
        #region Method : Checkout
        public DataTable Checkout(int UserID)
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

        #region Method : Checkout
        public DataTable AddressSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Address_SelectAll");
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

        #region Method : Address Insert
        public bool AddressInsert(AddressModel addressModel, int UserID)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Address_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@Address", DbType.String, addressModel.Address);
                sqlDatabase.AddInParameter(dbCommand, "@Country", DbType.String, addressModel.Country);
                sqlDatabase.AddInParameter(dbCommand, "@State", DbType.String, addressModel.State);
                sqlDatabase.AddInParameter(dbCommand, "@City", DbType.String, addressModel.City);
                sqlDatabase.AddInParameter(dbCommand, "@Pincode", DbType.String, addressModel.Pincode);
                sqlDatabase.AddInParameter(dbCommand, "@isDefault", DbType.Boolean, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
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
