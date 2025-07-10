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
/// Summary description for PortAgent1
/// </summary>
public class PortAgent1
{
    public PortAgent1()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectPortAgent(int _portoncallid)
    {
        string procedurename = "SelectPortAgentByPortOncallId";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand,"@PortCallId", DbType.Int32, _portoncallid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable selectPortCallRefNo(int _portcallid)
    {
        string procedurename = "SelectPortAgentBookingHeader";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCalliD", DbType.Int32, _portcallid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectPortAgentBookDetail(int _portoncallid, string _CrewIdList)
    {
        string procedurename = "getPortAgentBookingCrew";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewIdList", DbType.String, _CrewIdList);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _portoncallid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static void insertPortAgentBookingDetails(string _strProc, string _rfqno, int _portcallid, int _portagentid, string _ETA, string _ETD, string _Status, int _CreatedBy, string _Crewid, string _Contractid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@RFQNo", DbType.String, _rfqno);
        oDatabase.AddInParameter(odbCommand, "@PortCallId ", DbType.Int32 , _portcallid);
        oDatabase.AddInParameter(odbCommand, "@PortAgentId", DbType.Int32 , _portagentid);
        if (_ETA == "")
        {
        oDatabase.AddInParameter(odbCommand, "@ETA", DbType.DateTime , null);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ETA", DbType.DateTime , _ETA);
        }
        if(_ETD=="")
        {
        oDatabase.AddInParameter(odbCommand, "@ETD", DbType.DateTime, null);
        }
        else
        {
             oDatabase.AddInParameter(odbCommand, "@ETD", DbType.DateTime , _ETD);
        }

        
        oDatabase.AddInParameter(odbCommand, "@Status", DbType.String, _Status);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int16, _CreatedBy);

        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.String, _Crewid);
        oDatabase.AddInParameter(odbCommand, "@Contractid", DbType.String, _Contractid);
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
    public static DataTable selectPortAgentBookDetail_Show(string _portAgentbookid)
    {
        string procedurename = "getPortAgentBookingCrew_Show";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortAgentBookingId", DbType.String, _portAgentbookid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable selectPortAgentDetails(int _portagentid)
    {
        string procedurename = "getPortAgentpreviouscost";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortAgentId", DbType.Int32 , _portagentid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable selectPortAgentBookHeaderDetail(int PortAgentBookingId)
    {
        string procedurename = "SelectPortAgentBookingHeaderdetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortAgentBookingId", DbType.Int32, PortAgentBookingId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static void deletePortAgentBookDetails(string _strProc, int _PortAgentBookingId,out int _deletestatus)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        _deletestatus = -1;
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@PortAgentBookingId", DbType.Int32, _PortAgentBookingId);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.String, _deletestatus); 

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
       
                oDatabase.ExecuteNonQuery(odbCommand);
                _deletestatus = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
                scope.Complete();
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
               odbCommand.Dispose();
            }
        }
    }
    public static DataTable select_PortAgentMailSendingHedaerDetail(int PortAgentBookingId)
    {
        string procedurename = "Select_PortAgentBookingHeaderToSendMail";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortAgentBookingId", DbType.Int32, PortAgentBookingId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable select_PortAgentMailSendingBookingDetail(int PortAgentBookingId)
    {
        string procedurename = "Select_PortAgentMailSendBookingDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortAgentBookingId", DbType.Int32, PortAgentBookingId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
