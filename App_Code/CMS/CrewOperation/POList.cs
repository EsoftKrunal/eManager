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
/// Summary description for POList
/// </summary>
public class POList
{
    public POList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable getPODetailsforPortRefNumber(int _portcallid)
    {
        string procedurename = "get_PODetailsfor_PortCall";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable getVendorforTravelRFQ(int _travelrfqid)
    {
        string procedurename = "get_Vendorfor_TravelRFQ";
        DataTable dt54 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TravelRFQId", DbType.Int32, _travelrfqid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt54.Load(dr);
        }
        return dt54;
    }
    public static DataTable account_head(int _vesselid,int _poyear)
    {
        string procedurename = "PO_accounthead";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@vesselid", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@poyear", DbType.Int32, _poyear);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }
        return dt3;
    }

    public static DataTable selectDataPOVendor(int _portcallid)
    {
        string procedurename = "SelectVendorsAccordingToPortCallid";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallID", DbType.Int32, _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable rfq_number(int vendorid, int portcallid)
    {
        string procedurename = "SelectVendorRFQ";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, vendorid);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable selectPortCallRefNo(int vesselid, int portid)
    {
        string procedurename = "getPortCallRefNo";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, vesselid);
        objDatabase.AddInParameter(objDbCommand, "@PortId", DbType.Int32, portid);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectGVPOListDetails(int _LoginId)
    {
        string procedurename = "SelectPOHeaderDetails";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.String, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }

    public static DataTable selectRFQDetailsById(int _agentID, int _vendorid)
    {
        string procedurename = "selectRFQNoBYAgentID";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AgentBookingId", DbType.Int32, _agentID);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        //objDatabase.AddInParameter(objDbCommand, "@PortCallIdId", DbType.Int32, _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static DataTable selectGVPODetails()
    {
        string procedurename = "SelectPODetails";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static DataTable SelectPortCallHeaderETA(int _portcallid)
    {
        string procedurename = "selectportcallheaderdetailsbyportcallid";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32 , _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static void insertPOHeader(string _strproc,string _PONO,string _PODT,int _vesselid,int _portid,int _porcallid,string _EDA,string _ETD,int _vendorid,double _exchangerate,string _status,int _createdBy,out int _poid,int _Currency )

    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strproc);
        oDatabase.AddInParameter(odbCommand, "@PONO", DbType.String , _PONO);
        if (_PODT == "")
        {
            oDatabase.AddInParameter(odbCommand, "@PoDate", DbType.DateTime,null );
        }
        else
        {
             oDatabase.AddInParameter(odbCommand, "@PoDate", DbType.DateTime, _PODT );
        }
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32,_vesselid);
        oDatabase.AddInParameter(odbCommand, "@PortId", DbType.Int32, _portid);
        oDatabase.AddInParameter(odbCommand, "@PortCallId", DbType.Int32, _porcallid);
        if (_EDA == "")
        {
            oDatabase.AddInParameter(odbCommand, "@ETA", DbType.DateTime,null);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ETA", DbType.DateTime,_EDA);
        }
        if (_ETD == "")
        {
            oDatabase.AddInParameter(odbCommand, "@ETD", DbType.DateTime, null);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ETD", DbType.DateTime, _ETD);
        }

        oDatabase.AddInParameter(odbCommand, "@VendorId", DbType.Int32, _vendorid);
        oDatabase.AddInParameter(odbCommand, "@ExchangeRate", DbType.Double,_exchangerate);
        oDatabase.AddInParameter(odbCommand, "@Status", DbType.String,_status);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32,_createdBy);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, 0);
        oDatabase.AddInParameter(odbCommand, "@Currency", DbType.Int32 , _Currency);
        
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                _poid = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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

    public static void insertPODetails(string _strproc, int _PoId, int _AccountHeadId, string _Description, int _RFQNo, double _AmountLocal, double  _AmountUSD,string _tabname)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strproc);
        oDatabase.AddInParameter(odbCommand, "@POId", DbType.Int32, _PoId);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId", DbType.Int32, _AccountHeadId);
        oDatabase.AddInParameter(odbCommand, "@Description", DbType.String, _Description);
        oDatabase.AddInParameter(odbCommand, "@RFQNo", DbType.Int32, _RFQNo);
        oDatabase.AddInParameter(odbCommand, "@AmountLocal", DbType.Double, _AmountLocal);
        oDatabase.AddInParameter(odbCommand, "@AmountUSD", DbType.Double, _AmountUSD);
        oDatabase.AddInParameter(odbCommand, "@tabname", DbType.String, _tabname);
      


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

    public static DataTable SelectPOHeaderDetails(int _POID)
    {
        string procedurename = "selectpoheaderdetailsbypoid";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoID", DbType.Int32, _POID);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static DataTable selectPODetailsByPoId(int _POID)
    {
        string procedurename = "selectpodetailsbypoid";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoID", DbType.Int32, _POID);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable SelectCurrency()
    {
        string procedurename = "selectcurrency";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static DataTable selectVendorName_forSearch()
    {
        string procedurename = "SelectVendors_forPOSearch";
        DataTable dt48 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt48.Load(dr);
        }
        return dt48;
    }

    public static DataTable SelectAmountDetails(int _vesselid,int _budgetyear)
    {
        string procedurename = "SelectPOListDetails";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32,_vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int32, _budgetyear);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable SelectInvoiceAccordingToPO(int _poid)
    {
        string procedurename = "SelectInvoiceAccordingToPO";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@poid", DbType.Int32, _poid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static void CancelPO(int _POID)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("CancelPO");
        oDatabase.AddInParameter(odbCommand, "@PoId", DbType.Int32, _POID);
        

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
    public static DataTable SelectPO(int _vesselid, int _portid, string _pono,int _vendorname,int _loginid)
    {
        string procedurename = "searchpo_new";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@PortId", DbType.Int32, _portid);
        objDatabase.AddInParameter(objDbCommand, "@pono", DbType.String, _pono);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorname);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable getTotalCountofCrew(int _vendorid, string _totalcount)
    {
        string procedurename = "get_TotalCountfor_PO";
        DataTable dt88 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@CheckCount", DbType.String, _totalcount);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt88.Load(dr);
        }
        return dt88;
    }
}
