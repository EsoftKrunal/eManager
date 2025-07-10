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
/// Summary description for VarianceReport
/// </summary>
public class VesselModuleReport
{
    public VesselModuleReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //----------
    public static DataTable selectVesselHeader(int _vesselid)
    {
        string procedurename = "Report_Vessel_OverviewParticulars";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;
    }
    public static DataTable selectVesselCrewDocuments(int _vesselid)
    {
        string procedurename = "Report_Vessel_CrewDocuments";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;
    }
    public static DataTable selectVesselDetails(int _vesselid)
    {
        string procedurename = "Report_Vessel_OverviewParticulars_WageScales";
        DataTable dt22 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }
        return dt22;
    }
    public static DataTable selectVesselBudgetDetails(int _vesselid, int _year)
    {
        string procedurename = "Report_Vessel_Budget";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, _year);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
        }
        return dt33;
    }
    //----------------------

   
}
