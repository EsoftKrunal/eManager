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
/// Summary description for ReliefDueForNextSpecifiedDays
/// </summary>
public class ReliefDueForNextSpecifiedDays
{
    public ReliefDueForNextSpecifiedDays()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectReliefDueForNextSpecifiedDaysdetails(int _days,int _vid,string Rank,string PD)
    {
        string procedurename = "ReliefDueForNextSpecifiedDays";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Days", DbType.Int32, _days);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vid);
        objDatabase.AddInParameter(objDbCommand, "@Rank", DbType.String, Rank);
        objDatabase.AddInParameter(objDbCommand, "@PD", DbType.String, PD);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
