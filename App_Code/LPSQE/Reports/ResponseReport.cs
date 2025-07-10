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
/// Summary description for ResponseReport
/// </summary>
public class ResponseReport
{
	public ResponseReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //public static DataTable SelectCompanyDetails()
    //{
    //    string procedurename = "PR_PrintCompanyInfo";
    //    DataTable dt1 = new DataTable();

    //    Database objDatabase = DatabaseFactory.CreateDatabase();
    //    DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

    //    using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
    //    {
    //        dt1.Load(dr);
    //    }
    //    return dt1;
    //}
    public static DataTable SelectResponseDetails(int _InspDueId)
    {
        string procedurename = "PR_RPT_Response";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
}
