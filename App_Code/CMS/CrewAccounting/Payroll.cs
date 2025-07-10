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
public class Payroll
{
    public Payroll()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int IsLastPaid(int VesselID, int Month, int Year)
    {
        string procedurename = "IsLastPaid";
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
    public static int IsAlreadySaved(int VesselID, int Month, int Year)
    {
        string procedurename = "IsAlreadySaved";
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
        string procedurename = "Payroll_getCrewMembersList_Show";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@PayMonth", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@PayYear", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable checkStartDate(int VesselID, int Month, int Year)
    {
        string procedurename = "Check_PortageDate";
        DataTable dtcc = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselID);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, Year);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtcc.Load(dr);
        }

        return dtcc;
    }
    public static DataTable Get_Payroll_Expence(int VesselID,int Month,int Year)
    {
        string procedurename = "Get_Payroll_Expence";
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
        string procedurename = "Get_Payroll_Last";
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
        string procedurename = "Get_Payroll_Expence_Fixed";
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
        string procedurename = "Get_Payroll_Income";
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
    public static int InsertUpdatePayrollDetails(int _PayrollId,int _Vesselid, double _Month, int _Year,string CrewList,string ContractList,int _LoginId)
    {
        DataTable dt = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertPayrollDetails");
        oDatabase.AddInParameter(odbCommand, "@PayrollId", DbType.Int32, _PayrollId);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _Vesselid);
        oDatabase.AddInParameter(odbCommand, "@Month", DbType.Int32, _Month);
        oDatabase.AddInParameter(odbCommand, "@Year", DbType.Int32, _Year);
        oDatabase.AddInParameter(odbCommand, "@CrewList", DbType.String, CrewList);
        oDatabase.AddInParameter(odbCommand, "@ContractList", DbType.String, ContractList);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _LoginId);
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt.Load(dr);
        }
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public static DataTable IS_PAYROL_GENERATED(int _Vesselid, double _Month, int _Year,int CrewId,int ContractId)
    {
        DataTable dt = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("IS_PAYROL_GENERATED");
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _Vesselid);
        oDatabase.AddInParameter(odbCommand, "@Month", DbType.Int32, _Month);
        oDatabase.AddInParameter(odbCommand, "@Year", DbType.Int32, _Year);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.String, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.String, ContractId);
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static void InsertUpdatePayrollDetails_Calc_Cols(int PayrollId, int CrewId, int ContractId, double TotalEarning, double NetEarning, double Leave, double OpBal,double Bonus,double OpBalBonus, double TotPayable, double Netpaybale, double OtherAmount)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertPayrollDetails_CalculatedColumns");
        oDatabase.AddInParameter(odbCommand, "@PayrollId", DbType.Int32, PayrollId);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@TotalEarning", DbType.Currency, TotalEarning);
        oDatabase.AddInParameter(odbCommand, "@NetEarning", DbType.Currency, NetEarning);
        oDatabase.AddInParameter(odbCommand, "@Leave", DbType.Currency, Leave);
        oDatabase.AddInParameter(odbCommand, "@OpBal", DbType.Currency, OpBal);
        oDatabase.AddInParameter(odbCommand, "@Bonus", DbType.Currency, Bonus);
        oDatabase.AddInParameter(odbCommand, "@OpBalBonus", DbType.Currency, OpBalBonus);
        oDatabase.AddInParameter(odbCommand, "@TotPayable", DbType.Currency, TotPayable);
        oDatabase.AddInParameter(odbCommand, "@NetPayable", DbType.Currency, Netpaybale);
        oDatabase.AddInParameter(odbCommand, "@OtherAmount", DbType.Currency, OtherAmount);
        
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
