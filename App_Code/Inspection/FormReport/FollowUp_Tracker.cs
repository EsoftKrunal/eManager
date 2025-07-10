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
/// Summary description for FollowUp_Tracker
/// </summary>
public class FollowUp_Tracker
{
    public FollowUp_Tracker()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectFollowUpSearching(string _followupcat, string _vesselid, string _fromdate, string _todate, int _loginId)
    {
        string procedurename = "PR_FR_SearchFollowUpTracker";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FollowUpCategory", DbType.String, _followupcat);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginId);
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
    public static DataTable SelectVesselWiseFollowUpDetails(int _vesselid, string _followupcategory, string _fromdate, string _todate, string _dueindays, string _status, string _critical,  string _responsibility, string _overdue)
    {
        string procedurename = "PR_FR_GetFPDetails_Vsl";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FollowUpCategory", DbType.String, _followupcategory);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);
        objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.String, _dueindays);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _status);
        objDatabase.AddInParameter(objDbCommand, "@Critical", DbType.String, _critical);
        objDatabase.AddInParameter(objDbCommand, "@Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@OverDue", DbType.String, _overdue);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt2.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt2.Dispose();
            throw se;
        }
        return dt2;
    }
    public static DataTable GetFollowUpDetailsByInspObsvId(int _inspectiondueid, int _observationid, string _closeddate, string _remarks, string _transtype, int _closed, int _closedby, string _tblflag, string _correctiveaction, string _targetclosedate, string _responsibility, int _modifiedby, string _deficiency, string _flaws)
    {
        string procedurename = "PR_FR_GetFollowUpByID";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _inspectiondueid);
        objDatabase.AddInParameter(objDbCommand, "@ObservationId", DbType.Int32, _observationid);
        if (_closeddate != "")
            objDatabase.AddInParameter(objDbCommand, "@ClosedDate", DbType.DateTime, _closeddate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, _remarks);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        objDatabase.AddInParameter(objDbCommand, "@FP_Closed", DbType.String, _closed);
        objDatabase.AddInParameter(objDbCommand, "@F_ClosedBy", DbType.Int32, _closedby);
        objDatabase.AddInParameter(objDbCommand, "@TblFlagName", DbType.String, _tblflag);
        objDatabase.AddInParameter(objDbCommand, "@FP_CorrectiveActions", DbType.String, _correctiveaction);
        if (_targetclosedate!="")
            objDatabase.AddInParameter(objDbCommand, "@FP_TargetClosedDate", DbType.DateTime, _targetclosedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FP_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@FP_Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@FP_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@FP_Deficiency", DbType.String, _deficiency);
        objDatabase.AddInParameter(objDbCommand, "@FP_Flaws", DbType.String, _flaws);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt3.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt3.Dispose();
            throw se;
        }
        return dt3;
    }
    public static DataTable GetFollowUpDetailsById_Report(int _vesselid, string _fpcategoryid, string _fpfromdate, string _fptodate, string _fpdueindays, string _status, string _critical, string _responsibility, string _overdue)
    {
        string procedurename = "PR_FR_RPT_FollowUpByVslId";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FollowUpCategory", DbType.String, _fpcategoryid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fpfromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _fptodate);
        objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.String, _fpdueindays);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _status);
        objDatabase.AddInParameter(objDbCommand, "@Critical", DbType.String, _critical);
        objDatabase.AddInParameter(objDbCommand, "@Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@OverDue", DbType.String, _overdue);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt4.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt4.Dispose();
            throw se;
        }
        return dt4;
    }
    public static DataTable InserUpdateTrackerFollowUpList(int _vesselid, int _followupcat, string _critical, string _followupitem, string _targetcldate, int _closed, string _closedDate, string _responsibility,string _deficiency, string _correctiveactions, string _closedremarks, int _createdby, int _modifiedby, int _closedby, string _transtype)
    {
        string procedurename = "PR_FR_InsertUpdateTrackerFollowUp";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FP_VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FP_FollowUpCatId", DbType.Int32, _followupcat);
        objDatabase.AddInParameter(objDbCommand, "@FP_Critical", DbType.String, _critical);
        objDatabase.AddInParameter(objDbCommand, "@FP_FollowUp", DbType.String, _followupitem);
        if (_targetcldate != "")
            objDatabase.AddInParameter(objDbCommand, "@FP_TargetClosedDate", DbType.DateTime, _targetcldate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FP_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@FP_Closed", DbType.Int32, _closed);
        if (_closedDate != "")
            objDatabase.AddInParameter(objDbCommand, "@FP_ClosedDate", DbType.DateTime, _closedDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@FP_ClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@FP_Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@FP_Deficiency", DbType.String, _deficiency);
        objDatabase.AddInParameter(objDbCommand, "@FP_CorrectiveActions", DbType.String, _correctiveactions);
        objDatabase.AddInParameter(objDbCommand, "@FP_ClosedRemarks", DbType.String, _closedremarks);
        objDatabase.AddInParameter(objDbCommand, "@FP_CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@FP_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@FP_ClosedBy", DbType.Int32, _closedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt5.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt5.Dispose();
            throw se;
        }
        return dt5;
    }

    //public static DataSet getVessel(int _userid)
    //{
    //    Database objDatabase = DatabaseFactory.CreateDatabase();
    //    string procedurename = "SelectVesselAccordingToUser";
    //    DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
    //    DataSet objDataset = new DataSet();
    //    objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _userid);

    //    try
    //    {
    //        // execute command and get records
    //        objDataset = objDatabase.ExecuteDataSet(objDbCommand);
    //    }
    //    catch (Exception ex)
    //    {
    //        // if error is coming throw that error
    //        throw ex;
    //    }
    //    finally
    //    {
    //        // after used dispose dataset and commmand
    //        objDataset.Dispose();
    //        objDbCommand.Dispose();
    //    }

    //    // Note: connection was closed by ExecuteDataSet method call 

    //    return objDataset;
    //}
}
