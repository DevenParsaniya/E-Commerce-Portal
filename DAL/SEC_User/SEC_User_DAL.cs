using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.SEC_User.Models;

namespace E_Commerce_Website.DAL.SEC_User
{
    public class SEC_User_DAL : SEC_User_DALBase
    {
        #region Method: SEC_User_SelectByUserNamePassword
        public DataTable SEC_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByUserNamePassword");
                sqlDatabase.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                sqlDatabase.AddInParameter(dbCommand, "Password", DbType.String, Password);

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

        #region Method: SEC_User_Register
        public bool SEC_User_Register(string UserName, string Password, string FirstName, string LastName, string MobileNumber , string UserEmail)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectUserName");
                sqlDatabase.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    DbCommand dbCommand1 = sqlDatabase.GetStoredProcCommand("PR_User_Insert");
                    sqlDatabase.AddInParameter(dbCommand1, "UserName", DbType.String, UserName);
                    sqlDatabase.AddInParameter(dbCommand1, "Password", DbType.String, Password);
                    sqlDatabase.AddInParameter(dbCommand1, "FirstName", DbType.String, FirstName);
                    sqlDatabase.AddInParameter(dbCommand1, "LastName", DbType.String, LastName);
                    sqlDatabase.AddInParameter(dbCommand1, "MobileNumber", DbType.String, MobileNumber);
                    sqlDatabase.AddInParameter(dbCommand1, "UserEmail", DbType.String, UserEmail);
                    sqlDatabase.AddInParameter(dbCommand1, "IsAdmin", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand1, "CreatedAt", SqlDbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand1, "ModifiedAt", SqlDbType.DateTime, DBNull.Value);
                    if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : UserFilter
        public DataTable SEC_UserFilter(SEC_UserFilterModel sEC_userFilterModel)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, sEC_userFilterModel.UserName);
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
