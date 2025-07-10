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
/// Summary description for PrintCrewList
/// </summary>
public class cls_ReportCrewChange
{
    public cls_ReportCrewChange()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public SqlDataAdapter adp = new SqlDataAdapter();
    public static DataTable CrewChangeReport_Total(int _VesselId, DateTime _FromDate, DateTime _ToDate)
    {
        string procedurename = "ReportCrewChangeTotal";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable CrewChangeReport_SignOff(int _VesselId, DateTime _FromDate, DateTime _ToDate)
    {
        string procedurename = "ReportCrewChangeSignOff";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable CrewChangeReport_SignOn(int _VesselId, DateTime _FromDate, DateTime _ToDate)
    {
        string procedurename = "ReportCrewChangeSignOn";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _FromDate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _ToDate);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
