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
/// Summary description for VesselLineUp
/// </summary>
public class VesselLineUp
{
    public VesselLineUp()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectVesselLineUpdata(int _ownerid, int _vesselid)
    {
        string procedurename = "Report_VesselLineUpReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Ownerid", DbType.Int32, _ownerid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
