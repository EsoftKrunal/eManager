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
public class PrintCrewList
{
    public PrintCrewList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public SqlDataAdapter adp = new SqlDataAdapter();

    public static DataTable selectCompanyDetails()
    {
        string procedurename = "PrintCompanyInfo";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable selectCrewListDetails(int _VesselId, string _SignOnDate, string _SignOffDate, string _fields)
    {
        string procedurename = "PrintCrewList";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        if (_SignOnDate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.String, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.String, Convert.ToDateTime(_SignOnDate).ToString("MM/dd/yyyy"));
        }
        if (_SignOffDate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.String, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.String, Convert.ToDateTime(_SignOffDate).ToString("MM/dd/yyyy"));
        }
        objDatabase.AddInParameter(objDbCommand, "@fields", DbType.String, _fields);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectCrewListDetailsWithExperience(int _VesselId)
    {
        string procedurename = "PrintCrewList2WithExperience";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable getreurningCrew(int _vesselid, string _signondate, string _signoffdate)
    {
        string procedurename = "countcrewlist";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        if (_signondate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.DateTime, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.DateTime, Convert.ToDateTime(_signondate).ToString("MM/dd/yyyy"));
        }
        if (_signoffdate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, Convert.ToDateTime(_signoffdate).ToString("MM/dd/yyyy"));
        }

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }

    public static DataTable selectCrewListDetailsforImoVessel(int _VesselId, int _flag)
    {
        string procedurename = "Get_CrewListforImoVessel";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@flag", DbType.Int32, _flag);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
