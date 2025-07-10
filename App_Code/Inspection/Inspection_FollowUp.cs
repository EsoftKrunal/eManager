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
/// Summary description for Inspection_FollowUp
/// </summary>
public class Inspection_FollowUp
{
	public Inspection_FollowUp()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable InspectionObservation(int ID, string CorrectiveActions, string Flaws, string Remarks, string TargetCloseDt, int Closed, string ClosedDate, string TransType, string _Responsibilty, int _FCreatedBy, int _FModifiedBy, int _FClosedBy)
	{

        string procedurename = "PR_ADMS_FollowUp";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
        objDatabase.AddInParameter(objDbCommand, "@CorrectiveActions", DbType.String, CorrectiveActions);
        objDatabase.AddInParameter(objDbCommand, "@Flaws", DbType.String, Flaws);
        objDatabase.AddInParameter(objDbCommand, "@TargetCloseDt", DbType.String, TargetCloseDt);
        objDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String,Remarks);
        objDatabase.AddInParameter(objDbCommand, "@Closed", DbType.String,Closed);
        if (ClosedDate!="")
            objDatabase.AddInParameter(objDbCommand, "@ClosedDate", DbType.DateTime, ClosedDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ClosedDate", DbType.DateTime, DBNull.Value);
        //objDatabase.AddInParameter(objDbCommand, "@ClosedDate", DbType.String, ClosedDate);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
        objDatabase.AddInParameter(objDbCommand, "@Responsibilty", DbType.String, _Responsibilty);
        objDatabase.AddInParameter(objDbCommand, "@F_CreatedBy", DbType.Int32, _FCreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@F_ModifiedBy", DbType.Int32, _FModifiedBy);
        objDatabase.AddInParameter(objDbCommand, "@F_ClosedBy", DbType.Int32, _FClosedBy);

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
    public static DataTable GetFollowUpItems(int _InspectionDueId)
    {
        string procedurename = "PR_GetFollowUpItems";
        DataTable dt2 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspectionDueId);
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
    public static DataTable GetFollowUpMailDetails(int _InspectionDueId, int _ObservationId)
    {
        string procedurename = "PR_GetFollowUpMailDetails";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspectionDueId);
            objDatabase.AddInParameter(objDbCommand, "@ObservationId", DbType.Int32, _ObservationId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt3.Load(dr);
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
        return dt3;
    }
}
