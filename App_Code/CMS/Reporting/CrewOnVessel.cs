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
public class cls_CrewOnVessel
{
    public cls_CrewOnVessel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable get_HeaderDetails(int x)
    {
        string procedurename = "ReportCrewListWithWages_Header";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    //public static string get_WageScaleComponentName(int x)
    //{
    //    string procedurename = "get_WageScaleComponentName";
    //    DataTable dt3 = new DataTable();
    //    Database objDatabase = DatabaseFactory.CreateDatabase();
    //    DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
    //    objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, x);

    //    using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
    //    {
    //        dt3.Load(dr);
    //    }

    //    return (dt3.Rows[0][0].ToString());
    //}
    public static string get_WageScaleComponentName(int x)
    {
        string procedurename = "get_AllWageScaleComponentsName";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return (dt3.Rows[0][0].ToString());
    }
    public static DataTable get_Details(int x, string from, string to)
    {
        string procedurename = "ReportCrewListWithWages";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, x);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, from);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, to);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
