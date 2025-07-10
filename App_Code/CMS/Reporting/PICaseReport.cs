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
/// Summary description for PICaseReport
/// </summary>
public class PICaseReport
{
    public static DataTable selectPICaseDetailsData(string _fd, string _td, string _status)
    {
        string procedurename = "PrintPAndIMedicalReportByPeriod";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@FD", DbType.String, _fd);
        objDatabase.AddInParameter(objDbCommand, "@TD", DbType.String, _td);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _status);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
