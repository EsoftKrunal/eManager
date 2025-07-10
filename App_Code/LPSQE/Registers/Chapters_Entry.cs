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

public class Chapters_Entry
{
    public static string ErrMsg="";
    public static DataTable ChaptersDetails(int _id, int _inspgroup, int _chapternum, string _chaptername, int _createdby, int _modifiedby, string _transtype)
    {
        string procedurename = "PR_ADMS_Chapters";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGroup", DbType.Int32, _inspgroup);
        objDatabase.AddInParameter(objDbCommand, "@ChapterNo", DbType.Int32, _chapternum);
        objDatabase.AddInParameter(objDbCommand, "@ChapterName", DbType.String, _chaptername);
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
}
