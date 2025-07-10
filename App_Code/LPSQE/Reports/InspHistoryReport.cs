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
/// Summary description for InspHistoryReport
/// </summary>
public class InspHistoryReport
{
    public InspHistoryReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public static DataTable SelectInspHistoryDetails(string _InspId, int _OwnerId, int _VesselId, string _FromDate, string _ToDate)
    public static DataTable SelectInspHistoryDetails(string _InspId, int _OwnerId, string _VesselId, string _FromDate, string _ToDate)
    {
        string procedurename = "PR_RPT_InspHistory";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _VesselId);
        if (_FromDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, DBNull.Value);
        if (_ToDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, DBNull.Value);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    //public static DataTable SelectInspPlannedDetails(string _InspId, int _OwnerId, int _VesselId, string _FromDate, string _ToDate)
    public static DataTable SelectInspPlannedDetails(string _InspId, int _OwnerId, string _VesselId, string _FromDate, string _ToDate)
    {
        string procedurename = "PR_RPT_InspPlanned";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _VesselId);
        if (_FromDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, DBNull.Value);
        if (_ToDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, DBNull.Value);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    //public static DataTable SelectInspDoneButOpenDetails(string _InspId, int _OwnerId, int _VesselId, string _FromDate, string _ToDate)
    public static DataTable SelectInspDoneButOpenDetails(string _InspId, int _OwnerId, string _VesselId, string _FromDate, string _ToDate)
    {
        string procedurename = "PR_RPT_InspDoneButOpen";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _VesselId);
        if (_FromDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, DBNull.Value);
        if (_ToDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, DBNull.Value);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    //public static DataTable SelectInspHistory_HistDetails(string _InspId, int _OwnerId, int _VesselId, string _FromDate, string _ToDate)
    public static DataTable SelectInspHistory_HistDetails(string _InspId, int _OwnerId, string _VesselId, string _FromDate, string _ToDate)
    {
        string procedurename = "PR_RPT_InspHistory_Hist";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _VesselId);
        if (_FromDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, DBNull.Value);
        if (_ToDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, DBNull.Value);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
}
