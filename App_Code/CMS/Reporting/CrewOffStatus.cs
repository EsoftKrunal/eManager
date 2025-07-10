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
/// Summary description for PrintCrewList
/// </summary>
public class cls_CrewOffStatus
{
    public cls_CrewOffStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public SqlDataAdapter adp = new SqlDataAdapter();
    public static DataTable fn_CrewOffStatus(int _NationalityId,int _ownerid)
    {
        string procedurename = "ReportCrewOffStatus";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NationalityId", DbType.Int32, _NationalityId);
        objDatabase.AddInParameter(objDbCommand, "@ownerId", DbType.Int32, _ownerid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
