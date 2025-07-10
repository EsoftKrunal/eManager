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
/// Summary description for StatusWiseCrewList
/// </summary>
public class StatusWiseCrewList
{
    public StatusWiseCrewList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectCrewStatus()
    {
        string procedurename = "SelectCrewStatus1";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }

    public static DataTable StatusWiseCrewMemberList(int _vesselid, string _statusid,string _fields)
    {
        string procedurename = "StatusWiseCrewMemberList";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@vesselid", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _statusid);
        objDatabase.AddInParameter(objDbCommand, "@Fields", DbType.String, _fields);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
}
