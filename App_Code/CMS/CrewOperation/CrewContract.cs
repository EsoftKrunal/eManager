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
/// Summary description for CrewContract
/// </summary>
public class CrewContract
{
    public CrewContract()
    {
        //
        // TODO: Add constructor logic here
        //
    }
     public static int Close_Contract(int ContractId,int LoginId)
    { 
        DataTable dt11 = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Close_Contract");
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, LoginId);

        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt11.Load(dr);
        }
        return Convert.ToInt32(dt11.Rows[0][0]);
    }
    public static DataTable get_PendingCrewforContract(int LoginId)
      {
        string procedurename = "get_PendingCrewforContract";
        DataTable dt11 = new DataTable(); 

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
         objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable SelectRankOfSameOffgroup(int RankId)
    {
        string procedurename = "SelectRankOfSameOffgroup";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, RankId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable getCrewdetails(int _CrewID, int PortCallId)
      {
        string procedurename = "Contract_Bind";
        DataTable dt11 = new DataTable(); 

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewID", DbType.Int32, _CrewID);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, PortCallId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable getWages(int _Vesselid)
    {
        string procedurename = "SelectWageScalesByVesselId";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _Vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable bind_AssignedWages_by_StartDate(string _Startdate, int _seniority, int nationality, int wages, int rank, int chkstatus)
    {
        string procedurename = "Wages_Assigned_By_StartDate";
        DataTable dt21 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@StartDate", DbType.String, _Startdate);
        objDatabase.AddInParameter(objDbCommand, "@seniority", DbType.Int32, _seniority);
        objDatabase.AddInParameter(objDbCommand, "@nationality", DbType.Int32, nationality);
        objDatabase.AddInParameter(objDbCommand, "@wagescaleid", DbType.Int32, wages);
        objDatabase.AddInParameter(objDbCommand, "@rank", DbType.Int32, rank);
        objDatabase.AddInParameter(objDbCommand, "@STA", DbType.Int32, chkstatus);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt21.Load(dr);
        }

        return dt21;
    }
    public static DataTable bind_AssignedWages(int _contractid)
    {
        string procedurename = "get_WagesforHistoryContract";
        DataTable dt26 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, _contractid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt26.Load(dr);
        }

        return dt26;
    }
    public static DataTable bind_AssignedWages(int _seniority,int nationality,int wages,int rank)
    {
        string procedurename = "Wages_Assigned";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@seniority", DbType.Int32, _seniority);
        objDatabase.AddInParameter(objDbCommand, "@nationality", DbType.Int32, nationality);
        objDatabase.AddInParameter(objDbCommand, "@wagescaleid", DbType.Int32, wages);
        objDatabase.AddInParameter(objDbCommand, "@rank", DbType.Int32, rank);
              
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable Select_Contract(int LoginId,string CrewNumber)
    {
        string procedurename = "Select_Contract";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, CrewNumber);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable Select_Contract_Details1(int ContractId)
    {
        string procedurename = "SelectContractDetailsByContractId";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, ContractId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable Select_Contract_Details2(int ContractId)
    {
        string procedurename = "SelectContractDetails1ByContractId";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, ContractId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable bind_AssignedWages_Sum(int _seniority, int nationality, int wages, int rank)
    {
        string procedurename = "Wages_Assigned_Sum";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@seniority", DbType.Int32, _seniority);
        objDatabase.AddInParameter(objDbCommand, "@NationalityiD", DbType.Int32, nationality);
        objDatabase.AddInParameter(objDbCommand, "@wagescaleid", DbType.Int32, wages);
        objDatabase.AddInParameter(objDbCommand, "@rank", DbType.Int32, rank);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable bind_AssignedWages_Total(int _contractid)
    {
        string procedurename = "get_WagesforHistoryContract_Sum";
        DataTable dt26 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, _contractid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt26.Load(dr);
        }

        return dt26;
    }
   
    public static int InsertContractDetails(int CrewId, int Rankid, int Seniority, DateTime IssueDate, DateTime StartDate, DateTime EndDate, int Duration, int WageScaleId, int NationalityId, int Loginid, string Remark, int NewRankID, int PortCallId, double OAmount, int chkstatus, double ExtraOtRate, int TravelPayCriteria, int IsContractRevision = 0)
    {
        int RValue;
        RValue = 0;
         DataTable dt11 = new DataTable();
       
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertContractDetails");
        
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, -1);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@Rankid", DbType.Int32, Rankid);
        oDatabase.AddInParameter(odbCommand, "@Seniority", DbType.Int32, Seniority);
        oDatabase.AddInParameter(odbCommand, "@IssueDate", DbType.DateTime, IssueDate);
        oDatabase.AddInParameter(odbCommand, "@StartDate", DbType.DateTime, StartDate);
        oDatabase.AddInParameter(odbCommand, "@EndDate", DbType.DateTime, EndDate);
        oDatabase.AddInParameter(odbCommand, "@Duration", DbType.Int32, Duration);
        oDatabase.AddInParameter(odbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        oDatabase.AddInParameter(odbCommand, "@NationalityId", DbType.Int32, NationalityId);
        oDatabase.AddInParameter(odbCommand, "@Loginid", DbType.Int32, Loginid);
        oDatabase.AddInParameter(odbCommand, "@Remark", DbType.String, Remark);
        oDatabase.AddInParameter(odbCommand, "@NewRankID", DbType.Int32, NewRankID);
        oDatabase.AddInParameter(odbCommand, "@PortCallId", DbType.Int32, PortCallId);
        oDatabase.AddInParameter(odbCommand, "@OtherAmount", DbType.Double, OAmount);
        oDatabase.AddInParameter(odbCommand, "@STA", DbType.Int32, chkstatus);
        oDatabase.AddInParameter(odbCommand, "@ExtraOTRate", DbType.Double, ExtraOtRate);
        oDatabase.AddInParameter(odbCommand, "@TravelPayCriteria", DbType.Int32, TravelPayCriteria);
        oDatabase.AddInParameter(odbCommand, "@IsContractRevision", DbType.Int16, IsContractRevision);
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt11.Load(dr);
        }

        return Convert.ToInt32(dt11.Rows[0][0]);

    }
    public static DataTable Chk_ContractLicense(int _CrewId, int _RankID, int _NewRankID)
    {
        string procedurename = "Check_Crew_Contract";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, _RankID);
        objDatabase.AddInParameter(objDbCommand, "@NewRankId", DbType.Int32, _NewRankID);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static int UpdateContractDetails(int CrewId, int ContractId)
    { 
        DataTable dt11 = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateContractDetails");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);

        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt11.Load(dr);
        }
        return Convert.ToInt32(dt11.Rows[0][0]);
    }
    public static DataTable Select_Contract_Crew(int _CrewId)
    {
        string procedurename = "Select_Contract_Crew";
        DataTable dt11 = new DataTable();


        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable Select_ExperienceSummary_Crews(int _CrewId)
    {
        string procedurename = "SelectCrewExperienceSummary";
        DataTable dt22 = new DataTable();


        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }

        return dt22;
    }
    public static void UpdateContractDetailsAgain(int CrewId, int ContractId,int Duration)
    {
      
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateContractDetailsAgain");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@Duration", DbType.Int32, Duration);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
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
    public static DataTable Select_RecoverableExpenses(int _CrewId)
    {
        string procedurename = "selectrecoverablecost_bycrewid";
        DataTable dt22 = new DataTable();


        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }

        return dt22;
    }
 
}
