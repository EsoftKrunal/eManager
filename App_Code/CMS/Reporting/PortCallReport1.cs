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
public class cls_PortCallReport1
{
    public cls_PortCallReport1()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable Report_PortCall1(DateTime From, DateTime To, int Vessel, string Status)
    {
        string procedurename = "Report_PortCall1";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, Vessel);
        objDatabase.AddInParameter(objDbCommand, "@CStatus", DbType.String, Status);
        objDatabase.AddInParameter(objDbCommand, "@CFrom", DbType.Date, From);
        objDatabase.AddInParameter(objDbCommand, "@CTo", DbType.Date, To);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static DataTable Report_PortCall1_CrewDetails(DateTime From, DateTime To, int Vessel, string Status)
    {
        string procedurename = "Report_PortCall1_CrewDetails";
        DataTable dt33 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, Vessel);
        objDatabase.AddInParameter(objDbCommand, "@CStatus", DbType.String, Status);
        objDatabase.AddInParameter(objDbCommand, "@CFrom", DbType.Date  , From);
        objDatabase.AddInParameter(objDbCommand, "@CTo", DbType.Date, To);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
        }

        return dt33;
    }
}
