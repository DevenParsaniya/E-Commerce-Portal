namespace E_Commerce_Website.DAL
{
    public class DALHelper
    {
        #region Connection String
        public static string ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("myConnectionString");
        #endregion
    }
}
