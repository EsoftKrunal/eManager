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

public class Inspection_Checklist
{
    public static string ErrMsg = "";
    public static DataTable selectChapterDetails()
    {
        string procedurename = "PR_SelectChapterDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectSubChapterDetailsByChapId(int _chapterid)
    {
        string procedurename = "PR_SubChapterDetailsByChapterId";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ChapterId", DbType.Int32, _chapterid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
    public static DataTable InspectionCheckListDetails(int _id, int _subchapterid, string _questionnum, string _refnum, string _quest, int _questtype, string _desc, string _olddesc, int _vesseltype, string _defcode, int _createdby, int _modifiedby, string _transtype, int _sortorder, int _VersionId, string ShellScore)
    {
        string procedurename = "PR_ADMS_Questions";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@SubChapterId", DbType.Int32, _subchapterid);
        objDatabase.AddInParameter(objDbCommand, "@QuestionNo", DbType.String, _questionnum);
        objDatabase.AddInParameter(objDbCommand, "@RefNo", DbType.String, _refnum);
        objDatabase.AddInParameter(objDbCommand, "@Question", DbType.String, _quest);
        objDatabase.AddInParameter(objDbCommand, "@QuestionType", DbType.Int32, _questtype);
        objDatabase.AddInParameter(objDbCommand, "@Description", DbType.String, _desc);
        objDatabase.AddInParameter(objDbCommand, "@OldDescription", DbType.String, _olddesc);
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.Int32, _vesseltype);
        objDatabase.AddInParameter(objDbCommand, "@DeficiencyCode", DbType.String, _defcode);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        objDatabase.AddInParameter(objDbCommand, "@SortOrder", DbType.Int32, _sortorder);
        objDatabase.AddInParameter(objDbCommand, "@VersionId", DbType.Int32, _VersionId);
        objDatabase.AddInParameter(objDbCommand, "@ShellScore", DbType.String, ShellScore);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt3.Load(dr);
            }
            return dt3;
        }
        catch (SqlException se)
        {
            dt3.Dispose();
            ErrMsg = se.Message;
            return null;
        }
    }
}
