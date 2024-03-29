using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_Website.Areas.SEC_User.Models;

namespace E_Commerce_Website.DAL.SEC_User
{
    public class SEC_User_DALBase : DALHelper
    {
        SEC_User_DAL dalSEC_User = new SEC_User_DAL();

        #region Method: SEC_User_SelectALL
        public DataTable SEC_UserSelectALL()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectAll");
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

        #region Method: SEC_User_SelectByID
        public DataTable SEC_UserSelectByID(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByPK");
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

        #region Method : User Insert and Update
        public bool SEC_UserSave(SEC_UserModel sEC_UserModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (sEC_UserModel.UserID == 0 || sEC_UserModel.UserID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, sEC_UserModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@FirstName", DbType.String, sEC_UserModel.FirstName);
                    sqlDatabase.AddInParameter(dbCommand, "@LastName", DbType.String, sEC_UserModel.LastName);
                    sqlDatabase.AddInParameter(dbCommand, "@UserEmail", DbType.String, sEC_UserModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@MobileNumber", DbType.Int32, sEC_UserModel.MobileNumber);
                    sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, sEC_UserModel.Password);
                    sqlDatabase.AddInParameter(dbCommand, "@CreatedAt", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@ModifiedAt", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, sEC_UserModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, sEC_UserModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@FirstName", DbType.String, sEC_UserModel.FirstName);
                    sqlDatabase.AddInParameter(dbCommand, "@LastName", DbType.String, sEC_UserModel.LastName);
                    sqlDatabase.AddInParameter(dbCommand, "@UserEmail", DbType.String, sEC_UserModel.UserEmail);
                    sqlDatabase.AddInParameter(dbCommand, "@MobileNumber", DbType.Int32, sEC_UserModel.MobileNumber);
                    sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, sEC_UserModel.Password);
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

        #region Method : User By ID
        public SEC_UserModel SEC_UserEdit(int UserID)
        {
            SEC_UserModel sEC_UserModel = new SEC_UserModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    sEC_UserModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    sEC_UserModel.UserName = dataRow["UserName"].ToString();
                    sEC_UserModel.FirstName = dataRow["FirstName"].ToString();
                    sEC_UserModel.LastName = dataRow["LastName"].ToString();
                    sEC_UserModel.UserEmail = dataRow["UserEmail"].ToString(); 
                    sEC_UserModel.MobileNumber = dataRow["MobileNumber"].ToString();
                    sEC_UserModel.Password = dataRow["Password"].ToString();
                    sEC_UserModel.CreatedAt = Convert.ToDateTime(dataRow["CreatedAt"]);
                    sEC_UserModel.ModifiedAt = Convert.ToDateTime(dataRow["ModifiedAt"]);
                }
                return sEC_UserModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : User Delete By ID
        public bool SEC_UserDeleteByID(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_DeleteByPK");
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
