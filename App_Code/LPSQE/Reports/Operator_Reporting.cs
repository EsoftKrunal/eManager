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
/// Summary description for Operator_Reporting
/// </summary>
public class Operator_Reporting
{
    public Operator_Reporting()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectOperatorReportingDetails(string _InspId, int _OwnerId, string _VesselId, string _FromDate, string _ToDate)
    {
        string procedurename = "PR_RPT_OperatorReporting_temp";
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
    public static DataTable SelectObservationReportingDetails(string _InspId, string _VesselId, string _Inspector, string _CrewId,string _Chapter,string _FD,string _TD)
    {
        string procedurename = "PR_ObservationReport";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.String, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGrp", DbType.String, _InspId);
        objDatabase.AddInParameter(objDbCommand, "@InspectorName", DbType.String, _Inspector);
        objDatabase.AddInParameter(objDbCommand, "@Chapter", DbType.String, _Chapter);
        objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.Int32 , _CrewId);
        objDatabase.AddInParameter(objDbCommand, "@FD", DbType.String , _FD);
        objDatabase.AddInParameter(objDbCommand, "@TD", DbType.String, _TD);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

}
