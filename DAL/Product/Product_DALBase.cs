using E_Commerce_Website.Areas.Product.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using E_Commerce_Website.Areas.Category.Models;
using E_Commerce_Website.DAL.Category;

namespace E_Commerce_Website.DAL.Product
{
    public class Product_DALBase : DALHelper
    {
        #region Method : Product SelectAll (Active)
        public DataTable ProductSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_SelectAll");
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

        #region Method : Product SelectAll (In Active)
        public DataTable ProductDeletedSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Deleted");
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

        #region Method : Shopping Product By ID
        public DataTable ShoppingProductByID(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
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

        #region Method : Product Insert & Update
        public bool ProductSave(ProductModel productModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (productModel.ProductID == 0 || productModel.ProductID == null)
                {
                    if (productModel.PhotoPath != null)
                    {
                        string FilePath = "wwwroot\\Upload";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.PhotoPath.FileName);

                        productModel.ProductImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.PhotoPath.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.PhotoPath.CopyTo(fileStream);
                        }
                    }
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductDescription", DbType.String, productModel.ProductDescription);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductPrice", DbType.Int32, productModel.ProductPrice);
                    //sqlDatabase.AddInParameter(dbCommand, "@MinimumOrderQuantity", DbType.Int32, productModel.MinimumOrderQuantity);
                    //sqlDatabase.AddInParameter(dbCommand, "@MaximumOrderQuantity", DbType.Int32, productModel.MaximumOrderQuantity);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductCode", DbType.String, productModel.ProductCode);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductImage", DbType.String, productModel.ProductImage);
                    sqlDatabase.AddInParameter(dbCommand, "@DiscountPercentage", DbType.Int32, productModel.DiscountPercentage);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductRating", DbType.Int32, productModel.ProductRating);
                    sqlDatabase.AddInParameter(dbCommand, "@CreatedAt", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@ModifiedAt", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    if (productModel.PhotoPath != null)
                    {
                        string FilePath = "wwwroot\\Upload";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.PhotoPath.FileName);

                        productModel.ProductImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.PhotoPath.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.PhotoPath.CopyTo(fileStream);
                        }
                    }
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_UpdateByPK");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, productModel.ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductDescription", DbType.String, productModel.ProductDescription);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductPrice", DbType.Int32, productModel.ProductPrice);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductCode", DbType.String, productModel.ProductCode);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, productModel.IsActive);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductImage", DbType.String, productModel.ProductImage);
                    sqlDatabase.AddInParameter(dbCommand, "@DiscountPercentage", DbType.Int32, productModel.DiscountPercentage);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductRating", DbType.Int32, productModel.ProductRating);
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

        #region Method : Product Select By ID
        public ProductModel ProductSelectByID(int ProductID)
        {
            ProductModel productModel = new ProductModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    productModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    productModel.ProductName = dataRow["ProductName"].ToString();
                    productModel.ProductDescription = dataRow["ProductDescription"].ToString();
                    productModel.ProductPrice = Convert.ToDouble(dataRow["ProductPrice"]);
                    productModel.ProductCode = dataRow["ProductCode"].ToString();
                    productModel.ProductImage = dataRow["ProductImage"].ToString();
                    productModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    productModel.IsActive = Convert.ToBoolean(dataRow["IsActive"]);
                    productModel.DiscountPercentage = Convert.ToInt32(dataRow["DiscountPercentage"].ToString());
                    productModel.ProductRating = Convert.ToInt32(dataRow["ProductRating"]);
                    productModel.CreatedAt = Convert.ToDateTime(dataRow["CreatedAt"]);
                    productModel.ModifiedAt = Convert.ToDateTime(dataRow["ModifiedAt"]);
                }
                return productModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Product Delete
        public bool ProductDeleteByID(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_DeleteByPK");
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

        #region Method : Product Retrive
        public bool ProductRetrive(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_Retrive");
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

        #region Method : Delete Multiple Product
        public bool DeleteMultipleProducts(int[] ProductIDs)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Product_DeleteMultiple");
                DataTable productIdsTable = new DataTable();
                productIdsTable.Columns.Add("ProductID", typeof(int));
                foreach (var productId in ProductIDs)
                {
                    productIdsTable.Rows.Add(productId);
                }
                // Convert DataTable to a comma-separated string of ProductIDs
                string productIdsString = string.Join(",", productIdsTable.AsEnumerable().Select(row => row.Field<int>("ProductID")));

                // Pass the string as a parameter
                sqlDatabase.AddInParameter(dbCommand, "@ProductIDs", DbType.String, productIdsString);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
    #endregion
}