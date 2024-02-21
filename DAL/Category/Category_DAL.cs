using E_Commerce_Website.Areas.Product.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.Category.Models;

namespace E_Commerce_Website.DAL.Category
{
    public class Category_DAL : Category_DALBase
    {
        #region Method : CategoryFilter
        public DataTable CategoryFilter(CategoryFilterModel categoryFilterModel)
        {
            try
            {
                DataTable dataTable = new DataTable();
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Category_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@CategoryName", DbType.String, categoryFilterModel.CategoryName);
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
