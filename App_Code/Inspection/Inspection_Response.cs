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
/// Summary description for Inspection_Response
/// </summary>
public class Inspection_Response
{
    public static string ErrMsg = "";
    public static DataTable ResponseDetails(int _Id, int _inspdueid, string _QuestNum, string Response, int _FirstApp, int _FirstAppBy, int _SecApp, int _SecAppBy, int _InspCleared, string _InspValidity, string _Status, string _TransType, string _RespUploaded, string _ClosureRemarks, string _OperatorRemarks, string _FollowupItem)
    //public static DataTable ResponseDetails(int _Id, string _QuestNum, string Response, string _TransType)
    {
        string procedurename = "PR_ADMS_Response";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _Id);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _inspdueid);
        objDatabase.AddInParameter(objDbCommand, "@QuestionNo", DbType.String, _QuestNum);
        objDatabase.AddInParameter(objDbCommand, "@Response", DbType.String, Response);
        objDatabase.AddInParameter(objDbCommand, "@FirstApproval", DbType.Int32, _FirstApp);
        objDatabase.AddInParameter(objDbCommand, "@FirstApprovedBy", DbType.Int32, _FirstAppBy);
        objDatabase.AddInParameter(objDbCommand, "@SecondApproval", DbType.Int32, _SecApp);
        objDatabase.AddInParameter(objDbCommand, "@SecondApprovedBy", DbType.Int32, _SecAppBy);
        objDatabase.AddInParameter(objDbCommand, "@InspectionCleared", DbType.Int32, _InspCleared);
        if (_InspValidity != "")
            objDatabase.AddInParameter(objDbCommand, "@InspectionValidity", DbType.DateTime, _InspValidity);
        else
            objDatabase.AddInParameter(objDbCommand, "@InspectionValidity", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _Status);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _TransType);
        objDatabase.AddInParameter(objDbCommand, "@ResponseUploaded", DbType.String, _RespUploaded);
        objDatabase.AddInParameter(objDbCommand, "@ClosureRemarks", DbType.String, _ClosureRemarks);
        objDatabase.AddInParameter(objDbCommand, "@OperatorRemarks", DbType.String, _OperatorRemarks);
        objDatabase.AddInParameter(objDbCommand, "@FollowUpItem", DbType.String, _FollowupItem);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (SqlException se)
        {
           dt1.Dispose();
           throw se;
        }
        return dt1;
    }
    //***** Code To Check Whether Inspection is Random or Not
    public static DataTable CheckRandomInsp(int _InspectionDueId)
    {
        string procedurename = "PR_ChkRandomInsp";
        DataTable dt78 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspectionDueId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt78.Load(dr);
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
        return dt78;
    }
    public static DataSet CheckInspectionGroupResponse(int _inspdueid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_CheckInspGroupResponse";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _inspdueid);
        DataSet objDataset = new DataSet();
        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDataset.Dispose();
            objDbCommand.Dispose();
        }
        return objDataset;
    }
}
