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
/// Summary description for cls_AllInvoiceList
/// </summary>
public class cls_AllInvoiceList
{
    public static DataTable SelectInvoice_Search_All(string _refno, string _invno, int _vendorid, int _vesselid,int _searchmode)
    {
        string procedurename = "SelectInvoice_Search_All";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RefNo", DbType.String, _refno);
        objDatabase.AddInParameter(objDbCommand, "@InvNo", DbType.String, _invno);
        objDatabase.AddInParameter(objDbCommand, "@Vendor", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@SearchMode", DbType.Int32, _searchmode);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
}
