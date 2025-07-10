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
/// Summary description for StaffNotRelievedOnTime
/// </summary>
public class StaffNotRelievedOnTime
{
    public StaffNotRelievedOnTime()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectstaffnotrelievedontimedetails(DateTime frmdt, DateTime todt)
    {
        string procedurename = "StaffNotRelievedOnTime";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, frmdt);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, todt);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
