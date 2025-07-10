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
/// Summary description for ReliefPlanning
/// </summary>
public class ReliefPlanning
{
    public ReliefPlanning()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable Report_Relief_Planning(int _vesselid,int _days,string Rank,string pd,int LoginId)
    {
        string procedurename = "Report_Relief_Planning";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Days", DbType.Int32, _days);
        objDatabase.AddInParameter(objDbCommand, "@Rank", DbType.String, Rank);
        objDatabase.AddInParameter(objDbCommand, "@PD", DbType.String, pd);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId );
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable Report_Monthly_Commited_Cost(int _month, int _year,string VesselId)
    {
        string procedurename = "Report_Monthly_Comitted_Cost";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, _month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, _year);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
