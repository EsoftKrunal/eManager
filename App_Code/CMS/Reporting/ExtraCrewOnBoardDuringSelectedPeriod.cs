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
/// Summary description for ExtraCrewOnBoardDuringSelectedPeriod
/// </summary>
public class ExtraCrewOnBoardDuringSelectedPeriod
{
    public ExtraCrewOnBoardDuringSelectedPeriod()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectExtraCrewOnBoardDuringSelectedPeriod(DateTime _fromdate, DateTime _todate)
    {
        string procedurename = "ExtraCrewOnBoardDuringSelectedPeriod";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
