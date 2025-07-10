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
/// Summary description for PayInvoice
/// </summary>
public class PayInvoice
{
    public PayInvoice()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectPayInvoice(string _invoiceid)
    {
        string procedurename = "SelectPayInvoice";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.String, _invoiceid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    //public static int insertVoucherDetails(string _strProc, int _vendorid, string _bankname, string _CreditActNo,string _CreditActName, string _ChequeTTNo, double _ChequeTTAmt, double _bankcharges, string _ChequeTTDate, int _CreatedBy,string _invoiceid)
    public static int insertVoucherDetails(string _strProc, int _vendorid, string _bankname, string _CreditActNo, string _CreditActName, string _ChequeTTNo, string _ChequeTTAmt, string _bankcharges, string _ChequeTTDate, int _CreatedBy, string _invoiceid, int _currency,string _mtmVno)
    {
        DataTable dt = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@VendorId", DbType.Int32, _vendorid);
        oDatabase.AddInParameter(odbCommand, "@BankName", DbType.String, _bankname);
        oDatabase.AddInParameter(odbCommand, "@CreditActNo", DbType.String, _CreditActNo);
        oDatabase.AddInParameter(odbCommand, "@CreditActName", DbType.String, _CreditActName);
        oDatabase.AddInParameter(odbCommand, "@ChequeTTNo", DbType.String , _ChequeTTNo);
        if (_ChequeTTAmt == "")
        {
            oDatabase.AddInParameter(odbCommand, "@ChequeTTAmt", DbType.Decimal, 0);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ChequeTTAmt", DbType.Decimal, Convert.ToDecimal(_ChequeTTAmt));
        }
        if (_bankcharges == "")
        {
            oDatabase.AddInParameter(odbCommand, "@BankCharges", DbType.Decimal, 0);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@BankCharges", DbType.Decimal, Convert.ToDecimal(_bankcharges));
        }
        
        if (_ChequeTTDate == "")
        {
            oDatabase.AddInParameter(odbCommand, "@ChequeTTDate", DbType.DateTime, null);
        }
        else
        {
            oDatabase.AddInParameter(odbCommand, "@ChequeTTDate", DbType.DateTime, Convert.ToDateTime(_ChequeTTDate));
        }
        oDatabase.AddInParameter(odbCommand, "@CreatedBY", DbType.Int16, _CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBY", DbType.Int16, _CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@InvoiceId", DbType.String, _invoiceid);
        oDatabase.AddInParameter(odbCommand, "@CurrencyId", DbType.Int32, _currency);
        oDatabase.AddInParameter(odbCommand, "@MTMVno", DbType.String, _mtmVno);
        
       
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt.Load(dr);
        }
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public static string getvoucherno()
    {
        string procedurename = "getvoucherno";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt.Rows[0][0].ToString();

    }
}
