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
/// Summary description for PromotioDetails
/// </summary>
public class PromotioDetails
{
    public PromotioDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectUserNames()
    {
        string procedurename = "GetAllUsersNames_new";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.String, _userId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectPromotioDetails(int _userid, DateTime _fromdate, DateTime _todate)
    {
        string procedurename = "PrintPromotionReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int32, _userid);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
