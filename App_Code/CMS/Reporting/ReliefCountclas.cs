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
/// Summary description for ReliefCountclas
/// </summary>
public class ReliefCountclas
{
    public static DataTable selectReliefCountData(int _VesselId, int _rankid, string _FD, string _TD)
    {
        string procedurename = "Report_ReliefCount";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, _rankid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _FD);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _TD);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
