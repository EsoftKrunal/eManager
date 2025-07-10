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

public class Sub_Chapter
{
    public static string ErrMsg = "";
    public static DataTable SubChaptersDetails(int _id, int _chapterid, string _subchapternum, string _subchaptername, int _createdby, int _modifiedby, string _transtype)
    {
        string procedurename = "PR_ADMS_SubChapters";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@ChapterId", DbType.Int32, _chapterid);
        objDatabase.AddInParameter(objDbCommand, "@SubChapterNo", DbType.String, _subchapternum);
        objDatabase.AddInParameter(objDbCommand, "@SubChapterName", DbType.String, _subchaptername);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
            return dt1;
        }
        catch (SqlException se)
        {
            dt1.Dispose();
            ErrMsg = se.Message;
            return null;
        }
    }
    public static DataTable ChaptersNameByInspGrpId(int _inspgrpid)
    {
        string procedurename = "PR_ChaptersNameByInspGroupId";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGroupId", DbType.Int32, _inspgrpid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
}
