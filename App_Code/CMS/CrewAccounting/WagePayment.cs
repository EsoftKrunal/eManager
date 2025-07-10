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
/// Summary description for CashToMaster
/// </summary>
public class WagePayment
{
    public WagePayment()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int IsAlreadyPaid(int VesselID, int Month, int Year)
    {
        string procedurename = "IsAlreadyPaid";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public static DataTable get_Payroll(int VesselID,int Month,int Year)
    {
        string procedurename = "get_Payroll1";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable Get_Payroll_Income(int VesselID, int Month, int Year)
    {
        string procedurename = "Get_Payroll_Income1";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable Get_Payroll_Expence(int VesselID, int Month, int Year)
    {
        string procedurename = "Get_Payroll_Expence1";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable Get_Payroll_Expence_Fixed(int VesselID, int Month, int Year)
    {
        string procedurename = "Get_Payroll_Expence_Fixed1";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable Get_Payroll_Last(int VesselID, int Month, int Year)
    {
        string procedurename = "Get_Payroll_Last1";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static void PaidPayrollDetails(int _Vesselid, double _Month, int _Year,int CrewId,int ContractId,int Paid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("PaidPayrollDetails");
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _Vesselid);
        oDatabase.AddInParameter(odbCommand, "@Month", DbType.Int32, _Month);
        oDatabase.AddInParameter(odbCommand, "@Year", DbType.Int32, _Year);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@Paid", DbType.Int32,Paid);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                // _refno = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@refnumber"));
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }
}
