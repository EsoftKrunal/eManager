using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CrewSignOff
/// </summary>
public class cls_PortCallReport2
{
    public cls_PortCallReport2()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectVendorName_forReport()
    {
        string procedurename = "SelectVendors_forPortCallReport";
        DataTable dt48 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt48.Load(dr);
        }
        return dt48;
    }
    public static DataTable Report_PortCall2(int Month,int Year,int Vessel,string PoStatus, int Vendor)
    {
        string procedurename = "Report_PortCall2";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, Year);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32,Vessel);
        objDatabase.AddInParameter(objDbCommand, "@PoStatus", DbType.String,PoStatus);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, Vendor);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
