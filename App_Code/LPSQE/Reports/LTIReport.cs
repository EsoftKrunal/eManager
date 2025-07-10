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
/// Summary description for LTIReport
/// </summary>
public class LTIReport
{
    public LTIReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectLTIReportDetails(string _VesselId, int _Year)
    {
        string procedurename = "PR_RPT_LTIReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VESSELID", DbType.String, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@YEAR", DbType.Int32, _Year);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
}
