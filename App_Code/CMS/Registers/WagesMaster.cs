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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions; 

/// <summary>
/// Summary description for WagesMaster
/// </summary>
public class WagesMaster
{
    public WagesMaster()
    {
        
    }
    public static DataTable Get_Wage_Master_History(int WageScaleId, int NationalityId, int Seniority)
    {
        string procedurename = "Get_Wage_Master_History";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable WageComponents(int WageScaleId,int NationalityId,int Seniority)
    {
        string procedurename = "get_WageScalesComponents";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable WageComponents_Report(int WageScaleId, int NationalityId, int Seniority,string IR)
    {
        string procedurename = "get_WageScalesComponents_Report";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);
        objDatabase.AddInParameter(objDbCommand, "@OR", DbType.String, IR);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable WageComponents_FromHistory(int WageScaleRankId)
    {
        string procedurename = "get_WageComponents_FromHistory";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleRankId", DbType.Int32, WageScaleRankId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable WageComponents_FromHistory_Report(int WageScaleRankId, string IR)
    {
        string procedurename = "get_WageComponents_FromHistory_Report";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleRankId", DbType.Int32, WageScaleRankId);
        objDatabase.AddInParameter(objDbCommand, "@OR", DbType.String, IR);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable WageComponentsDetails(int WageScaleId, int NationalityId, int Seniority, string RankCode)
    {
        string procedurename = "get_WageScalesRankDetails";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);
        objDatabase.AddInParameter(objDbCommand, "@RankCode", DbType.String, RankCode);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void InsertWageScalesRankDetails(int WageScaleId, int NationalityId, int Seniority, string EffFrom, int nationality, int loginid)
    {
    
        string procedurename = "Insert_WageScalesRankDetails";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);
        objDatabase.AddInParameter(objDbCommand, "@EffFrom", DbType.String, EffFrom);
        objDatabase.AddInParameter(objDbCommand, "@Nationality", DbType.Int32, nationality );
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, loginid);

        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }
    public static void UpdateWageComponentsDetails(int WageScaleId, int NationalityId, int Seniority, string RankCode, string headerstr, string datastr, decimal OTRate)
    {
        string procedurename = "Update_WageScalesRankDetails";
        DataSet objDataset= new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);
        objDatabase.AddInParameter(objDbCommand, "@RankCode", DbType.String, RankCode);
        objDatabase.AddInParameter(objDbCommand, "@HeaderStr", DbType.String, headerstr);
        objDatabase.AddInParameter(objDbCommand, "@DataStr", DbType.String, datastr);
        objDatabase.AddInParameter(objDbCommand, "@OTRate", DbType.Decimal, OTRate);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }


    public static DataTable get_HeaderDetails(int WageScaleId, int NationalityId, int Seniority)
    {
        string procedurename = "get_WagesHeaderDetails";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable Insert_RankIn_WageScales(int WageScaleId, int NationalityId, int Seniority, string List)
    {
        string procedurename = "Insert_RankIn_WageScales";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@Seniority", DbType.Int32, Seniority);
        objDatabase.AddInParameter(objDbCommand, "@RankList", DbType.String, List );

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void InsertVessels(int WageScaleId,string VesselId)
    {
        string procedurename = "Insert_Vessels";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, VesselId);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }
    public static void Insert_WagerankHistory(int WageScaleRankId, DateTime _effectivedate)
    {
        //, string rankCode
        string procedurename = "Insert_WageScaleHistory";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@wageScaleRankid", DbType.Int32, WageScaleRankId);
        objDatabase.AddInParameter(objDbCommand, "@effectivedate", DbType.DateTime, _effectivedate.ToString("MM/dd/yyyy"));
        //objDatabase.AddInParameter(objDbCommand, "@RankCode", DbType.String, rankCode);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }

    public static DataTable CrewPortCallBillHeaderDetails(int payrollid, int contractid, int flag)
    {
        string procedurename = "Get_EarningDeductWages";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PayrollId", DbType.Int32, payrollid);
        objDatabase.AddInParameter(objDbCommand, "@contractId", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@flag", DbType.Int32, flag);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void UpdateCrewPortageBillHeaderDetails(int payrollid, int contractid, decimal extraOtHrs,  decimal extraOtRate, decimal extraOtAmount, decimal totalearnings, decimal totaldeductions,  string headerstr, string datastr, int loginid, int FD, int TD, decimal CURMONBAL, decimal PrevMonBal,decimal BALANCEOFWAGES, int travalPayDay, decimal travelPayAmount, decimal OtherAmount)
    {
        string procedurename = "Update_CrewPortageBillHeaderDetails";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@payrollid", DbType.Int32, payrollid);
        objDatabase.AddInParameter(objDbCommand, "@contractid", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@ExtraOTdays", DbType.Decimal, extraOtHrs);
        objDatabase.AddInParameter(objDbCommand, "@ExtraOTRate", DbType.Decimal, extraOtRate);
        objDatabase.AddInParameter(objDbCommand, "@ExtraOTAmount", DbType.Decimal, extraOtAmount);
        objDatabase.AddInParameter(objDbCommand, "@TotalEarnings", DbType.Decimal, totalearnings);
        objDatabase.AddInParameter(objDbCommand, "@TotalDeductions", DbType.Decimal, totaldeductions);
        objDatabase.AddInParameter(objDbCommand, "@HeaderStr", DbType.String, headerstr);
        objDatabase.AddInParameter(objDbCommand, "@DataStr", DbType.String, datastr);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, loginid);
        objDatabase.AddInParameter(objDbCommand, "@FD", DbType.Int32, FD);
        objDatabase.AddInParameter(objDbCommand, "@TD", DbType.Int32, TD);
        objDatabase.AddInParameter(objDbCommand, "@CURMONBAL", DbType.Decimal, CURMONBAL);
        objDatabase.AddInParameter(objDbCommand, "@PrevMonBal", DbType.Decimal, PrevMonBal);
        objDatabase.AddInParameter(objDbCommand, "@BALANCEOFWAGES", DbType.Decimal, BALANCEOFWAGES);
        objDatabase.AddInParameter(objDbCommand, "@travelPayDay", DbType.Int32, travalPayDay);
        objDatabase.AddInParameter(objDbCommand, "@travelPayAmount", DbType.Decimal, travelPayAmount);
        objDatabase.AddInParameter(objDbCommand, "@otherAmount", DbType.Decimal, OtherAmount);
        objDatabase.AddInParameter(objDbCommand, "@VERIFIED", DbType.Int32, 0);
        objDatabase.AddInParameter(objDbCommand, "@VERIFIEDBY", DbType.String, "");
        objDatabase.AddInParameter(objDbCommand, "@VERIFIEDON", DbType.String, DateTime.Now.ToString("dd-MMM-yyyy"));
        objDatabase.AddInParameter(objDbCommand, "@AUTOSAVED", DbType.Int32, 0);
       

       
       
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }

    public static DataTable WageComponentsDetailsHistory(int WageScaleId, int WageScaleRankId)
    {
        string procedurename = "GetWageScaleRankDetailsHistory";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleRankId", DbType.Int32, WageScaleRankId);
       
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

}
