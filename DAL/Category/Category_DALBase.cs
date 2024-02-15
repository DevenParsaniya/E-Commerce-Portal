using E_Commerce_Website.Areas.Category.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace E_Commerce_Website.DAL.Category
{
    public class Category_DALBase : DALHelper
    {
        #region Method : Category SelectAll
        public DataTable CategorySelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_SelectAll");
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

        #region Method : Category Insert and Update
        public bool CategorySave(CategoryModel categoryModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (categoryModel.CategoryID == 0 || categoryModel.CategoryID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryName", DbType.String, categoryModel.CategoryName);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryDescription", DbType.String, categoryModel.CategoryDescription);
                    sqlDatabase.AddInParameter(dbCommand, "@CreatedAt", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@ModifiedAt", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, categoryModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryName", DbType.String, categoryModel.CategoryName);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryDescription", DbType.String, categoryModel.CategoryDescription);
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

        #region Method : Category Select By ID
        public CategoryModel CategorySelectByID(int CategoryID)
        {
            CategoryModel categoryModel = new CategoryModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    categoryModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    categoryModel.CategoryName = dataRow["CategoryName"].ToString();
                    categoryModel.CategoryDescription = dataRow["CategoryDescription"].ToString();
                    categoryModel.CreatedAt = Convert.ToDateTime(dataRow["CreatedAt"]);
                    categoryModel.ModifiedAt = Convert.ToDateTime(dataRow["ModifiedAt"]);
                }
                return categoryModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Category Delete By ID
        public bool CategoryDeleteByID(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_DeleteByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
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
