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
/// Summary description for SignOn
/// </summary>
public class SignOn
{
    public SignOn()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable checkrfqforsignon(int _CrewId, int _PortCallId)
    {
        string procedurename = "check_rfq_for_signon";
        DataTable dt44 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _PortCallId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt44.Load(dr);
        }

        return dt44;
    }
    public static DataTable checkContractstartdate(DateTime _signondate, int _crewid)
    {
        string procedurename = "check_Contract_StartDate";
        DataTable dt55 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.DateTime, _signondate);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt55.Load(dr);
        }

        return dt55;
    }

    public static DataTable selectDataSignOnAsDetails(int _CrewId)
    {
        string procedurename = "get_NextRankList";
        DataTable dt2 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _CrewId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable selectpendingSignOnCrew(int _Loginid)
    {
        string procedurename = "get_PendingCrewforSignOn";
        DataTable dt2 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _Loginid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable chksignOn(int x)
    {
        string procedurename = "Check_sign_On";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable selectDataPortDetails()
    {
        string procedurename = "SelectPort";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectDataSignOnDetailsById(int _CrewId)
    {
        string procedurename = "SelectSignOnDetailsById";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static void updateSignOnDetails(string _strProc, int _crewId, int _signonportid, int _signonas, int _durationinmonths, DateTime _signondate, DateTime _reliefduedate, string _remarks,int _Loginid,int _PortCallID, int _manningagentId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@crewid", DbType.Int32, _crewId);
        oDatabase.AddInParameter(odbCommand, "@signonportid", DbType.Int32, _signonportid);
        oDatabase.AddInParameter(odbCommand, "@currentrankid", DbType.Int32, _signonas);
        oDatabase.AddInParameter(odbCommand, "@durationinmonths", DbType.Int32, _durationinmonths);
        oDatabase.AddInParameter(odbCommand, "@signondate", DbType.DateTime, _signondate.ToShortDateString());
        oDatabase.AddInParameter(odbCommand, "@reliefdue", DbType.DateTime, _reliefduedate.ToShortDateString());
        oDatabase.AddInParameter(odbCommand, "@remarks", DbType.String, _remarks);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _Loginid);
        oDatabase.AddInParameter(odbCommand, "@PortCallID", DbType.Int32, _PortCallID);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentId", DbType.Int32, _manningagentId);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                //_vesselId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
    public static void UpdateSignOnCrewDetails(int _crewId, DateTime _signondate,  string  _reliefdate,int duration)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateSignOnCrewDetails");
        oDatabase.AddInParameter(odbCommand, "@crewid", DbType.Int32, _crewId);
        oDatabase.AddInParameter(odbCommand, "@SignOnDate", DbType.DateTime, _signondate.ToShortDateString());
        if (_reliefdate == "")
        {
            oDatabase.AddInParameter(odbCommand, "@ReliefDueDate", DbType.DateTime, null);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ReliefDueDate", DbType.DateTime, Convert.ToDateTime(_reliefdate));
        }
        oDatabase.AddInParameter(odbCommand, "@Duration", DbType.Int32, duration);
       
        
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                //_vesselId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
    public static DataTable chkisplanned(int x, int PortCallId)
    {
        string procedurename = "Check_IsPlanned";
        DataTable dt6 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, x);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, PortCallId );

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
    public static DataTable chkfamilyorcrewmember(int x)
    {
        string procedurename = "Check_IsFamilyMember";
        DataTable dt7 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt7.Load(dr);
        }

        return dt7;
    }
    public static DataTable selectDataSignOnAsFamilyMember()
    {
        string procedurename = "SignonasFamilyMember";
        DataTable dt8 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _CurrentRankId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable selectDataContract(int _crewid)
    {
        string procedurename = "Check_CrewContract";
        DataTable dt8 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable selectSignOnData(int _crewid)
    {
        string procedurename = "SelectSignOnmemberDetails";
        DataTable dt8 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable getSignOnDetailsForExtension(int _crewid)
    {
        string procedurename = "get_CrewSignOnDetailsForExtension";
        DataTable dt8 = new DataTable();    
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static void UpdateSignOnDetailsForExtension(string _crewid,string ContractId,string LoginId, string ReiefDueDate, string remarks)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateCerwExtensionDetails");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _crewid);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, LoginId);
        oDatabase.AddInParameter(odbCommand, "@ReliefDueDate", DbType.Date, ReiefDueDate);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, remarks);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                //_vesselId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
