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
/// Summary description for SearchReport
/// </summary>
public class SearchReport
{
	public SearchReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable SelectSearchDetails(string _Status, string _FromDate, string _ToDate, string _InspId, int _OwnerId, int _VesselId, string _DueDate, int _LoginId, int _PortName, string _InspName, string _Chapter, string _InspNum, int _CrewId)
    {
        string procedurename = "PR_RPT_Search";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _Status);
        if (_FromDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, DBNull.Value);
        if (_ToDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.String, _DueDate);

        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, _LoginId);
        objDatabase.AddInParameter(objDbCommand, "@Port", DbType.Int32, _PortName);
        objDatabase.AddInParameter(objDbCommand, "@InspectorName", DbType.String, _InspName);
        objDatabase.AddInParameter(objDbCommand, "@Chapter", DbType.String, _Chapter);
        objDatabase.AddInParameter(objDbCommand, "@InspectionNo", DbType.String, _InspNum);
        objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.Int32, _CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
}
