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
/// Summary description for CrewSignOff
/// </summary>
public class cls_CrewNationalitySummary
{
    public cls_CrewNationalitySummary()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable get_Tables()
    {
        string procedurename = "ReportCrewNationalitySummary1";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@Mode", DbType.Int32, x);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable get_Tables1()
    {
        string procedurename = "ReportCrewNationalitySummary2";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@Mode", DbType.Int32, x);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
