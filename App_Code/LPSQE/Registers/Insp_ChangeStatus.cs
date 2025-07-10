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
/// Summary description for Insp_ChangeStatus
/// </summary>
public class Insp_ChangeStatus
{
    public Insp_ChangeStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable CheckInspNo(string _InspNum)
    {
        string procedurename = "PR_CheckInspNum";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspNum", DbType.String, _InspNum);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
    public static DataTable ChangeInspectionStatus(int _InspDueId, string _InspStatus)
    {
        string procedurename = "PR_ChangeInspStatus";
        DataTable dt2 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
        objDatabase.AddInParameter(objDbCommand, "@SelectedStatus", DbType.String, _InspStatus);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt2.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt2;
    }
}
