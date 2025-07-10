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
/// Summary description for ExpiringDocuments
/// </summary>
public class ExpiringDocuments
{
    public static DataTable selectCrewExpiringDocumentsData(int _crewid,DateTime _fromdate,DateTime _todate)
    {
        string procedurename = "PrintCrewExpiringDocumentDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@todate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
