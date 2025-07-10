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
/// Summary description for InvoiceReport
/// </summary>
public class InvoiceReport
{
    public InvoiceReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectREceivedInvoiceDate(DateTime _fromdate,DateTime _todate,int _mode,string _wherecondition)
    {
        string procedurename = "SelectInvoiceReportData";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);
        objDatabase.AddInParameter(objDbCommand, "@mode", DbType.Int16, _mode);
        objDatabase.AddInParameter(objDbCommand, "@condition", DbType.String, _wherecondition);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectPayInvoiceDate(string _condition1, string _condition2)
    {
        string procedurename = "SelectInvoicePayByDate";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@condition1", DbType.String, _condition1);
        objDatabase.AddInParameter(objDbCommand, "@condition2", DbType.String , _condition2);
        //objDatabase.AddInParameter(objDbCommand, "@Condition", DbType.DateTime, _wherecondition);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectUserVesselInvoice(int _vesselid,int _userid)
    {
        string procedurename = "UserVesselInvoice";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int16 , _userid);
        objDatabase.AddInParameter(objDbCommand, "@Vesselid", DbType.Int16 , _vesselid);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable getusername()
    {
        string procedurename = "GetAllUsersNames_new";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectVendorInvoice(int _vendorid)
    {
        string procedurename = "SelectVendorInvoice";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable Report_SelectVendorInvoice(string WhClause)
    {
        string procedurename = "Report_SelectVendorInvoice";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WhClause", DbType.String, WhClause);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    
    public static DataTable selectVendor()
    {
        string procedurename = "SelectVendors_All";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectVerifyInvoiceReport(int _mode, string _wherecondition)
    {
        string procedurename = "Invoice_VerifyByDate";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@mode", DbType.Int16, _mode);
        objDatabase.AddInParameter(objDbCommand, "@condition", DbType.String, _wherecondition);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
