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
/// Summary description for Inspection_TravelSchedule
/// </summary>
public class Inspection_TravelSchedule
{
    public static DataTable SelectInspectionDetailsByInspId(int _InspId)
    {
        string procedurename = "PR_SelectInsDueByID";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _InspId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable CheckInspectionOnHold(int _InspId)
    {
        string procedurename = "PR_CheckInspOnHold";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;
    }
    public static DataTable GetCheckListDetails(int _ChapterId)
    {
        string procedurename = "PR_CheckList";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ChapterId", DbType.Int32, _ChapterId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }
        return dt12;
    }
    public static DataTable GetInspGrpFromInspDueId(int _InspDueId)
    {
        string procedurename = "PR_InspGrpFromInspId";
        DataTable dt13 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt13.Load(dr);
        }
        return dt13;
    }
}
