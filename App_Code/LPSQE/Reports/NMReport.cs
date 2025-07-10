using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for NMReport
/// </summary>
public class NMReport
{
    public NMReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectNMReportDetails(string _Cat,string _FromDate, string _ToDate, string _VesselId)
    {
        string procedurename = "PR_RPT_NMReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CATEGORY", DbType.String, _Cat);
        objDatabase.AddInParameter(objDbCommand, "@FROMDATE", DbType.String, _FromDate);
        objDatabase.AddInParameter(objDbCommand, "@TODATE", DbType.String, _ToDate);
        objDatabase.AddInParameter(objDbCommand, "@VESSELID", DbType.String, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
}
