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
public class cls_PortCallReport3
{
    public cls_PortCallReport3()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable Report_PortCall3(int Month,int Year,int Vessel,string PoStatus)
    {
        string procedurename = "Report_PortCall3";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, Month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, Year);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32,Vessel);
        objDatabase.AddInParameter(objDbCommand, "@PoStatus", DbType.String,PoStatus);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
