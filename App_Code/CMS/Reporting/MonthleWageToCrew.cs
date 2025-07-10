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
/// Summary description for MonthleWageToCrew
/// </summary>
public class MonthleWageToCrew
{
    public MonthleWageToCrew()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectwage1(int _VesselId, int _Month, int _Year, int _CrewId)
    {
        string procedurename = "Report_Wage_Payment";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, _Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, _Year);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataSet selectwage(int _crewid, int _paymonth, int _payyear,string _paymonthname)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand("SelectMonthlyWagePayToCrew");
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("SelectMonthlyWagePayToCrew_new");
        DataSet objRank = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int16, _crewid);
        objDatabase.AddInParameter(objDbCommand, "@Paymonth", DbType.Int16, _paymonth);
        objDatabase.AddInParameter(objDbCommand, "@Payyear", DbType.Int16, _payyear);
        objDatabase.AddInParameter(objDbCommand, "@payMonthname", DbType.String , _paymonthname);
        try
        {
            objRank = objDatabase.ExecuteDataSet(objDbCommand);

        }
        catch (SystemException es)
        {
            throw es;
        }
        finally
        {
            objRank.Dispose();
            objDbCommand.Dispose();
        }
        return objRank;
    }
    public static DataSet selectFinalWage(int _crewid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand("SelectMonthlyWagePayToCrew");
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("FinalBalanceOfWages");
        DataSet objRank = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int16, _crewid);
      
        try
        {
            objRank = objDatabase.ExecuteDataSet(objDbCommand);

        }
        catch (SystemException es)
        {
            throw es;
        }
        finally
        {
            objRank.Dispose();
            objDbCommand.Dispose();
        }
        return objRank;
    }
    public static DataTable selectCrewStatus(string _crewid)
    {
        string procedurename = "Check_Sign_Off";
        DataTable dt22 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.String, _crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }

        return dt22;
    }
    public static DataSet getempname(string _strproc, int _vesselid, int _paymonth, int _payyear)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand("SelectMonthlyWagePayToCrew");
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(_strproc);
        DataSet objRank = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@vessel", DbType.Int16, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Paymonth", DbType.Int16, _paymonth);
        objDatabase.AddInParameter(objDbCommand, "@Payyear", DbType.Int16, _payyear);
        
        try
        {
            objRank = objDatabase.ExecuteDataSet(objDbCommand);

        }
        catch (SystemException es)
        {
            throw es;
        }
        finally
        {
            objRank.Dispose();
            objDbCommand.Dispose();
        }
        return objRank;
    }
}
