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
/// Summary description for TravelBooking
/// </summary>
public class TravelBooking
{
    public static DataTable selectSignOnOffTravelBookingDetails(string _crewidlist, int _portcallid)
    {
        string procedurename = "getTravelAgentBookingCrew";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewIdList", DbType.String, _crewidlist);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _portcallid);
        //objDatabase.AddInParameter(objDbCommand, "@", DbType.Int32, _vendorid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectPortCallDetails(int _portcallid)
    {
        string procedurename = "SelectTravelBookingHeaderDetails";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.String, _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static void InsertTravelBookingHeaderDetails(string _strProc, string _rfqno, int _portcallid, int _fromairport, int _toairport, int _travelagentid, DateTime _departuredate, int _class, char _status, int _createdby, string _crewid, string _contractid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@RFQNo", DbType.String, _rfqno);
        oDatabase.AddInParameter(odbCommand, "@PortCallId", DbType.Int32, _portcallid);
        oDatabase.AddInParameter(odbCommand, "@FromAirport", DbType.Int32, _fromairport);
        oDatabase.AddInParameter(odbCommand, "@ToAirport", DbType.Int32, _toairport);
        oDatabase.AddInParameter(odbCommand, "@TravelAgentId", DbType.Int32, _travelagentid);
        oDatabase.AddInParameter(odbCommand, "@DepartureDate", DbType.DateTime, _departuredate);
        oDatabase.AddInParameter(odbCommand, "@Class", DbType.Int32, _class);
        oDatabase.AddInParameter(odbCommand, "@Status", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.String, _crewid);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.String, _contractid);
        

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
    public static DataTable selectTravelBookingDetail_Show(string _travelbookingid)
    {
        string procedurename = "getTravelAgentBookingCrew_Show";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TravelBookingId", DbType.String, _travelbookingid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectTravelAgentDetails(int _travelagentid, int _fromid, int _toid)
    {
        string procedurename = "getTravelAgentpreviouscost";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TravelAgentId", DbType.Int32, _travelagentid);
        objDatabase.AddInParameter(objDbCommand, "@FromId", DbType.Int32, _fromid);
        objDatabase.AddInParameter(objDbCommand, "@ToId", DbType.Int32, _toid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectTravelHeaderDetail(int TravelBookingId)
    {
        string procedurename = "SelectTravelAgentBookingHeaderdetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TravelBookingId", DbType.Int32, TravelBookingId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static void deleteTravelBookingDetails(string _strProc, int _TravelBookingId, out int _deletestatus)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        _deletestatus = -1;
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TravelBookingId", DbType.Int32, _TravelBookingId);
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
    public static DataTable selectAirportsName()
    {
        string procedurename = "get_AirportsName";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
