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
/// Summary description for SignOnOffActivityTracking
/// </summary>
public class SignOnOffActivityTracking
{
    public SignOnOffActivityTracking()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectSignOnOffActivityTracking(DateTime _fromdate, DateTime _todate, int _status)
    {
        string procedurename = "SignOnOffActivityTracking";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.Int32, _status);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
