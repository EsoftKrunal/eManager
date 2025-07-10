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
public class VarianceReport
{
    public VarianceReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable getVesselAccordingtoOwner(int _ownerid)
    {
        string procedurename = "get_VesselAccordingto_Owner";
        DataTable dt9 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _ownerid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt9.Load(dr);
        }
        return dt9;
    }
    public static DataTable getVesselAccordingtoOwner_LoginUser(int _ownerid,int LoginId)
    {
        string procedurename = "get_VesselAccordingto_Owner_LoginUser";
        DataTable dt9 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _ownerid);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId );

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt9.Load(dr);
        }
        return dt9;
    }

    public static DataTable selectMonthlydata(int _ownerid, int _vesselid, int _budgetyear, string _monthid, int _budgetid)
    {
        string procedurename = "VarianceReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Ownerid", DbType.Int32, _ownerid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Current_Year", DbType.Int16, _budgetyear);
        objDatabase.AddInParameter(objDbCommand, "@Current_Month", DbType.String, _monthid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetType", DbType.Int32, _budgetid);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectYearlydata(int _vesselid,int _budgetyear, int _monthid)
    {
        string procedurename = "variancereport_year";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Vesselid1", DbType.Int32 , _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear1", DbType.Int16, _budgetyear);
        objDatabase.AddInParameter(objDbCommand, "@Monthid1", DbType.Int16, _monthid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectAnnualdata(int _vesselid,int _budgetyear)
    {
        string procedurename = "VarianceReport_Annual";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Vesselid", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int16, _budgetyear);
      

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
