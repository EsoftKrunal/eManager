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
/// Summary description for COC
/// </summary>
public class COC
{
    public COC()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable InserCOCTrackerDetails(int _vesselid, string _COCIssuedFrom, string _COCnum, string _defcode, string _targetclosedDate, string _responsibility, string _fileupload, int _createdby, int _modifiedby, string _issuedbyfirstname, string _issuedbylastname, int _issuedbyrank, string _empno, string _issuedate, string _surveySt, string _surveyEnd, string _followupdate, string _description, string _description1, string _surveyor, string _place)
    {
        string procedurename = "PR_FR_InsertCOCTracker";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CO_VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@CO_IssuedFrom", DbType.String, _COCIssuedFrom);
        objDatabase.AddInParameter(objDbCommand, "@CO_Number", DbType.String, _COCnum);
        objDatabase.AddInParameter(objDbCommand, "@CO_DefCode", DbType.String, _defcode);
        if (_targetclosedDate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_TargetClosedDate", DbType.DateTime, _targetclosedDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@CO_Responsibility", DbType.String, _responsibility);
        objDatabase.AddInParameter(objDbCommand, "@CO_Filepload", DbType.String, _fileupload);
        objDatabase.AddInParameter(objDbCommand, "@CO_CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@CO_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBFirstName", DbType.String, _issuedbyfirstname);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBLastName", DbType.String, _issuedbylastname);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBRank", DbType.Int32, _issuedbyrank);
        objDatabase.AddInParameter(objDbCommand, "@CO_EmpNo", DbType.String, _empno);
        if (_issuedate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_IssueDate", DbType.DateTime, _issuedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_IssueDate", DbType.DateTime, DBNull.Value);
        if (_surveySt != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_IMDate", DbType.DateTime, _surveySt );
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_IMDate", DbType.DateTime, DBNull.Value);
        if (_surveyEnd != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_PADate", DbType.DateTime, _surveyEnd);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_PADate", DbType.DateTime, DBNull.Value);
        if (_followupdate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_FollowUpDate", DbType.DateTime, _followupdate);   
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_FollowUpDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@CO_Description", DbType.String, _description);
        objDatabase.AddInParameter(objDbCommand, "@CO_Description1", DbType.String, _description1);
        objDatabase.AddInParameter(objDbCommand, "@CO_Suerveyor", DbType.String, _surveyor );
        objDatabase.AddInParameter(objDbCommand, "@CO_PlaceIssued", DbType.String, _place);

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
    public static DataTable SelectCOCSearching(string _vesselid, string _fromdate, string _todate)
    {
        string procedurename = "PR_FR_SearchCOCTracker";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
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
    public static DataTable SelectVesselWiseCOCDetails(int _vesselid, string _fromdate, string _todate, string _dueindays, string _status, string _responsibility, string _overdue)
    {
        string procedurename = "PR_FR_GetCOCDetails_Vsl";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
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
    public static DataTable GetFCOCDetailsByCOCId(int _COCid, int _vesselid, string _defcode, string _targetclosedate, string _responsibility, string _closeddate, string _flaws, string _closedremarks, int _modifiedby, int _closedby, string _transtype, string _fileupload, int _issuedby, string _empno, string _issuedate, string _imdate, string _padate, string _followupdate, string _description, string _description1, string _issuedbyfirstname, string _issuedbylastname, int _issuedbyrank, string _surveyor, string _place)
    {
        string procedurename = "PR_FR_GetCOCById";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@COCId", DbType.Int32, _COCid);
        objDatabase.AddInParameter(objDbCommand, "@CO_VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@CO_DefCode", DbType.String, _defcode);
        if (_targetclosedate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_TargetClosedDate", DbType.DateTime, _targetclosedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_TargetClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@CO_Responsibility", DbType.String, _responsibility);
        if (_closeddate!="")
            objDatabase.AddInParameter(objDbCommand, "@CO_ClosedDate", DbType.DateTime, _closeddate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_ClosedDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@CO_Flaws", DbType.String, _flaws);
        objDatabase.AddInParameter(objDbCommand, "@CO_ClosedRemarks", DbType.String, _closedremarks);
        objDatabase.AddInParameter(objDbCommand, "@CO_ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@CO_ClosedBy", DbType.Int32, _closedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        objDatabase.AddInParameter(objDbCommand, "@CO_Filepload", DbType.String, _fileupload);
        objDatabase.AddInParameter(objDbCommand, "@CO_IssuedBy", DbType.Int32, _issuedby);
        objDatabase.AddInParameter(objDbCommand, "@CO_EmpNo", DbType.String, _empno);
        if (_issuedate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_IssueDate", DbType.DateTime, _issuedate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_IssueDate", DbType.DateTime, DBNull.Value);
        if (_imdate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_IMDate", DbType.DateTime, _imdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_IMDate", DbType.DateTime, DBNull.Value);
        if (_padate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_PADate", DbType.DateTime, _padate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_PADate", DbType.DateTime, DBNull.Value);
        if (_followupdate != "")
            objDatabase.AddInParameter(objDbCommand, "@CO_FollowUpDate", DbType.DateTime, _followupdate);
        else
            objDatabase.AddInParameter(objDbCommand, "@CO_FollowUpDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@CO_Description", DbType.String, _description);
        objDatabase.AddInParameter(objDbCommand, "@CO_Description1", DbType.String, _description1);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBFirstName", DbType.String, _issuedbyfirstname);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBLastName", DbType.String, _issuedbylastname);
        objDatabase.AddInParameter(objDbCommand, "@CO_IBRank", DbType.Int32, _issuedbyrank);
        objDatabase.AddInParameter(objDbCommand, "@CO_Suerveyor", DbType.String, _surveyor);
        objDatabase.AddInParameter(objDbCommand, "@CO_PlaceIssued", DbType.String, _place);
        
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
