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
/// Summary description for InspMisc_Report
/// </summary>
public class InspMisc_Report
{
    public InspMisc_Report()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetAvailableFields()
    {
        string procedurename = "PR_AvailableFields";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable SelectInspMiscDetails(int _intInspGrpId, int _intInspId, int _intOwnId, int _intVslId, string _strFrmDt, string _strToDt, string _strSelFlds)
    {
        string procedurename = "PR_RPT_InspMisc";
        DataTable dt44 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGroupId", DbType.Int32, _intInspGrpId);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _intInspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _intOwnId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _intVslId);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _strFrmDt);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _strToDt);
        objDatabase.AddInParameter(objDbCommand, "@SelFields", DbType.String, _strSelFlds);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt44.Load(dr);
        }

        return dt44;
    }
    public static DataTable SelectInspMiscDetails_1(int _intInspGrpId, int _intInspId, int _intOwnId, int _intFleetId, int _intVslId, string _strFrmDt, string _strToDt, string _strSelFlds)
    {
        string procedurename = "PR_RPT_InspMisc1";
        DataTable dt44 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGroupId", DbType.Int32, _intInspGrpId);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _intInspId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _intOwnId);
        objDatabase.AddInParameter(objDbCommand, "@FleetId", DbType.Int32, _intFleetId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _intVslId);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _strFrmDt);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _strToDt);
        objDatabase.AddInParameter(objDbCommand, "@SelFields", DbType.String, _strSelFlds);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt44.Load(dr);
        }

        return dt44;
    }
}
