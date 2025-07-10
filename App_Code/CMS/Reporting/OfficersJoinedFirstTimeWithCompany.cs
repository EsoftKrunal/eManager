using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
/// <summary>
/// Summary description for OfficersJoinedFirstTimeWithCompany
/// </summary>
public class OfficersJoinedFirstTimeWithCompany
{
    public OfficersJoinedFirstTimeWithCompany()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectOfficersJoinedFirstTimeWithCompany(int _status,string FD,string TD,string or)
    {
        string procedurename = "OfficersJoinedFirstTimeWithCompany";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.Int32, _status);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, FD);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, TD);
        objDatabase.AddInParameter(objDbCommand, "@or", DbType.String, or);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
