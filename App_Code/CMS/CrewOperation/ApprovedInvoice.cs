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
/// Summary description for ApprovedInvoice
/// </summary>
public class ApprovedInvoice
{
    public ApprovedInvoice()
    {
        
    }
    public static DataTable SelectAuthorityOfDepartment(out int _retno, int _LoginId)
    {
        _retno = 0;
        string procedurename = "SelectAuthorityOfDepartmentId";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddOutParameter(objDbCommand, "@returnno", DbType.Int32, _retno);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _LoginId);
        objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.String, 3);
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

    public static DataTable getstatus()
    {
        string procedurename = "SelectInvoicePaymentStatus";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static void updatebookinvoice(string _invoiceid)
    {
        string procedurename = "BookInvoice";
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.String, _invoiceid);
        objDatabase.ExecuteNonQuery(objDbCommand);
        
    }
    public static DataTable selectapprovedinvoice(string _refno, string _invno, int _vendorid, int _vesselid,int  _loginId, int _status,string mtmpvno)
    {
        string procedurename = "SelectInvoice_Search";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RefNo", DbType.String, _refno);
        objDatabase.AddInParameter(objDbCommand, "@InvNo", DbType.String, _invno);
        objDatabase.AddInParameter(objDbCommand, "@Vendor", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginId);
        objDatabase.AddInParameter(objDbCommand, "@SearchMode", DbType.Int32, _status);
        objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.Int32, 5);
        objDatabase.AddInParameter(objDbCommand, "@MTMPVno", DbType.String, mtmpvno);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

   
   
}
