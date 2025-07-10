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
/// Summary description for NCR
/// </summary>
public class NCR
{
    public NCR()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable InserNCRTrackerDetails(int _vesselid, int _ncrtype, string _ncrnum, string _defcode, string _targetclosedDate, string _responsibility, string _fileupload, int _createdby, int _modifiedby, string _issuedbyfirstname, string _issuedbylastname, int _issuedbyrank, string _empno, string _issuedate, string _imdate, string _padate, string _followupdate, string _description)
    {
        string procedurename = "PR_FR_InsertNCRTracker";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NC_VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@NC_Type", DbType.Int32, _ncrtype);
        objDatabase.AddInParameter(objDbCommand, "@NC_Number", DbType.String, _ncrnum);
        objDatabase.AddInParameter(objDbCommand, "@NC_DefCode", DbType.String, _defcode);
        if (_targetclosedDate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_TargetClosedDate", DbType.DateTime, _targetclosedDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@NC_Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@NC_Filepload", DbType.String, _fileupload);
        objDatabase.AddInParameter(objDbCommand, "@NC_CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@NC_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBFirstName", DbType.String, _issuedbyfirstname);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBLastName", DbType.String, _issuedbylastname);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBRank", DbType.Int32, _issuedbyrank);
        objDatabase.AddInParameter(objDbCommand, "@NC_EmpNo", DbType.String, _empno);
        if (_issuedate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_IssueDate", DbType.DateTime, _issuedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_IssueDate", DbType.DateTime, DBNull.Value);
        if (_imdate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_IMDate", DbType.DateTime, _imdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_IMDate", DbType.DateTime, DBNull.Value);
        if (_padate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_PADate", DbType.DateTime, _padate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_PADate", DbType.DateTime, DBNull.Value);
        if (_followupdate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_FollowUpDate", DbType.DateTime, _followupdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_FollowUpDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@NC_Description", DbType.String, _description);

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
    public static DataTable SelectNCRSearching(string _ncrcat, string _vesselid, string _fromdate, string _todate)
    {
        string procedurename = "PR_FR_SearchNCRTracker";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NCRCategory", DbType.String, _ncrcat);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);

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
    public static DataTable SelectVesselWiseNCRDetails(int _vesselid, string _ncrcategory, string _fromdate, string _todate, string _dueindays, string _status, string _responsibility, string _overdue)
    {
        string procedurename = "PR_FR_GetNCRDetails_Vsl";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@NCRCategory", DbType.String, _ncrcategory);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);
        objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.String, _dueindays);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _status);
        objDatabase.AddInParameter(objDbCommand, "@Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@OverDue", DbType.String, _overdue);

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
    public static DataTable GetFNCRDetailsByNCRId(int _ncrid, int _vesselid, string _defcode, string _targetclosedate, string _responsibility, string _closeddate, string _flaws, string _closedremarks, int _modifiedby, int _closedby, string _transtype, string _fileupload, int _issuedby, string _empno, string _issuedate, string _imdate, string _padate, string _followupdate, string _description, string _issuedbyfirstname, string _issuedbylastname, int _issuedbyrank)
    {
        string procedurename = "PR_FR_GetNCRById";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NCRId", DbType.Int32, _ncrid);
        objDatabase.AddInParameter(objDbCommand, "@NC_VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@NC_DefCode", DbType.String, _defcode);
        if (_targetclosedate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_TargetClosedDate", DbType.DateTime, _targetclosedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@NC_Responsibility", DbType.String, _responsibility);
        if (_closeddate!="")
            objDatabase.AddInParameter(objDbCommand, "@NC_ClosedDate", DbType.DateTime, _closeddate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_ClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@NC_Flaws", DbType.String, _flaws);
        objDatabase.AddInParameter(objDbCommand, "@NC_ClosedRemarks", DbType.String, _closedremarks);
        objDatabase.AddInParameter(objDbCommand, "@NC_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@NC_ClosedBy", DbType.Int32, _closedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        objDatabase.AddInParameter(objDbCommand, "@NC_Filepload", DbType.String, _fileupload);
        objDatabase.AddInParameter(objDbCommand, "@NC_IssuedBy", DbType.Int32, _issuedby);
        objDatabase.AddInParameter(objDbCommand, "@NC_EmpNo", DbType.String, _empno);
        if (_issuedate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_IssueDate", DbType.DateTime, _issuedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_IssueDate", DbType.DateTime, DBNull.Value);
        if (_imdate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_IMDate", DbType.DateTime, _imdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_IMDate", DbType.DateTime, DBNull.Value);
        if (_padate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_PADate", DbType.DateTime, _padate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_PADate", DbType.DateTime, DBNull.Value);
        if (_followupdate != "")
            objDatabase.AddInParameter(objDbCommand, "@NC_FollowUpDate", DbType.DateTime, _followupdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@NC_FollowUpDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@NC_Description", DbType.String, _description);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBFirstName", DbType.String, _issuedbyfirstname);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBLastName", DbType.String, _issuedbylastname);
        objDatabase.AddInParameter(objDbCommand, "@NC_IBRank", DbType.Int32, _issuedbyrank);
        
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
}
