using System.Configuration;

namespace ShipSoft.CrewManager.DataAccessLayer
{
    /// <summary>
    /// The AppConfiguaration class contains read-only properties that are essentially short cuts to settings in the web.config file.
    /// </summary>
    public class AppConfiguration
    {

        #region Public Properties

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
            }
        }
        //public static string Admin_ConnectionString
        //{
        //    get
        //    {
        //        return ConfigurationManager.ConnectionStrings["Admin_DBConnection"].ConnectionString;
        //    }
        //}

        #endregion

    }
}