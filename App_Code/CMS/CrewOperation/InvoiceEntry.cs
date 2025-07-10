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
/// Summary description for InvoiceEntry
/// </summary>
public class InvoiceEntry
{
    public static DataTable CheckDuplicateInvoice(int _vendorid, string _invoiceno, int _invoiceid)
    {
        string procedurename = "check_duplicate_invoice";
        DataTable dt21 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceNo", DbType.String, _invoiceno);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.Int32, _invoiceid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt21.Load(dr);
        }

        return dt21;
    }
    public static DataTable selectUserNames(int mode)
    {
        string procedurename = "getuserfirstlastnames";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Mode", DbType.Int32, mode);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectCurrencyDD()
    {
        string procedurename = "selectcurrency";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static void insertUpdateInvoiceDetails(string _strProc,out int _refno, int _Invoiceid, int _vendorId, string _InvNo, double _Invamt,int _currencyId, double _gst, string _Invdate, string _due_date, int _vesselid, int _forwardto, int _createdby, int _modifiedby)
    {
        _refno=-1;
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@InvoiceId", DbType.Int32, _Invoiceid);
        oDatabase.AddInParameter(odbCommand, "@VendorId", DbType.Int32, _vendorId);
        oDatabase.AddInParameter(odbCommand, "@Invno", DbType.String, _InvNo);
        oDatabase.AddInParameter(odbCommand, "@InvoiceAmount", DbType.Double, _Invamt);
        oDatabase.AddInParameter(odbCommand, "@CurrencyId", DbType.Int32, _currencyId);
        oDatabase.AddInParameter(odbCommand, "@gst", DbType.Double, _gst);
        oDatabase.AddInParameter(odbCommand, "@InvDate", DbType.String, _Invdate);
        oDatabase.AddInParameter(odbCommand, "@DueDate", DbType.String, _due_date);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _vesselid);
        oDatabase.AddInParameter(odbCommand, "@ForwardTo", DbType.Int32, _forwardto);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddOutParameter(odbCommand, "@refnumber", DbType.Int32, _refno);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                _refno = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@refnumber"));
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
    public static DataTable selectDataInvoiceDetailsByInvoiceId(int _InvoiceId)
    {
        string procedurename = "SelectInvoiceDetailsByInvoiceId";
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

    public static DataTable SelectInvoiceMailDetails(int _VendorId)
    {
        string procedurename = "SelectInvoiceMailDetails";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _VendorId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
