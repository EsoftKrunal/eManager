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
/// Summary description for PortHeaderReport
/// </summary>
public class PortHeaderReport
{
    public PortHeaderReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable getusername()
    {
        string procedurename = "GetAllUsersNames_new";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectReportData(DateTime _fromdate, DateTime _todate, int _userid)
    {
        string procedurename = "selectportcallheader_report";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int16, _userid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);
       

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectTravelBookingReportData(DateTime _fromdate, DateTime _todate, int _userid)
    {
        string procedurename = "TravelBookingHeader_Report";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int16, _userid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectportagentReportData(DateTime _fromdate, DateTime _todate, int _userid)
    {
        string procedurename = "portAgentBookingheader_Report";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int16, _userid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
