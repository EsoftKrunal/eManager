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
/// Summary description for InvoiceSearchScreen
/// </summary>
public class InvoiceSearchScreen
{
    public static DataTable SelectAuthorityOfDepartment(out int _retno, int _LoginId)
    {
        _retno = 0;
        string procedurename = "SelectAuthorityOfDepartmentId";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddOutParameter(objDbCommand, "@returnno", DbType.Int32, _retno);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _LoginId);
        objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.String, 2);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
            _retno = Convert.ToInt32(objDatabase.GetParameterValue(objDbCommand, "@returnno"));
        }

        return dt33;
    }
    public static DataTable selectDataVendor()
    {
        string procedurename = "SelectVendors";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }


    public static DataTable selectDataInvoiceStatus()
    {
        string procedurename = "SelectStatusSearchMode";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt10.Load(dr);
        }
        return dt10;
    }

    public static DataTable selectPoNo(int _VesselId,int _Vendorid)
    {
        string procedurename = "getPoNumber";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _Vendorid);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }

    public static DataTable selectPoAmount(int _PoId)
    {
        string procedurename = "get_PoAmount";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoId", DbType.Int32, _PoId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable get_PODetails(int _PoId)
    {
        string procedurename = "get_PODetails";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoId", DbType.Int32, _PoId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }

    public static DataTable selectApprovalDetails(string _refno, string _invno, int _vendorid, int _vesselid, int _loginid, int _status)
    {
        string procedurename = "SelectInvoice_Search";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RefNo", DbType.String, _refno);
        objDatabase.AddInParameter(objDbCommand, "@InvNo", DbType.String, _invno);
        objDatabase.AddInParameter(objDbCommand, "@Vendor", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginid);
        objDatabase.AddInParameter(objDbCommand, "@SearchMode", DbType.Int32, _status);
        objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.Int32, 3);
        objDatabase.AddInParameter(objDbCommand, "@MTMPVno", DbType.String, "");

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static void updateInvoiceApprovalDeatils(string _strProc, int _InvoiceId, int _PoNo, double _ApprovedAmt, string _Remarks, string _Attachment, int _modifiedBy, int _Verify2To)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Invoiceid", DbType.Int32, _InvoiceId);
        oDatabase.AddInParameter(odbCommand, "@PoId", DbType.Int32, _PoNo);
        //oDatabase.AddInParameter(odbCommand, "@PoAmount", DbType.Double, _PoAmt); double _PoAmt,
        oDatabase.AddInParameter(odbCommand, "@ApprovedAmount", DbType.Double, _ApprovedAmt);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, _Remarks);
        oDatabase.AddInParameter(odbCommand, "@Attachment", DbType.String, _Attachment);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, _modifiedBy);
        oDatabase.AddInParameter(odbCommand, "@Verify2To", DbType.Int32, _Verify2To);
        
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

    public static DataTable selectDataInvoiceApprovalDetailsById(int _InvoiceId)
    {
        string procedurename = "SelectInvoiceApprovalDetailsById";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.Int32, _InvoiceId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectDataInvoiceEnquiryDetailsById(int _InvoiceId)
    {
        string procedurename = "SelectInvoiceEnquiryDetailsById";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.Int32, _InvoiceId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
        }

        return dt33;
    }
    public static void insertinvoicedetails(string _strProc, int _InvoiceId, int _AccountHeadId, double _Amount)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _InvoiceId);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId", DbType.Int32, _AccountHeadId);
        oDatabase.AddInParameter(odbCommand, "@AmountUSD", DbType.Double, _Amount);
        

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
}
