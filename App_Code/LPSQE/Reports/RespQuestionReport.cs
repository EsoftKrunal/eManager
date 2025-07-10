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
/// Summary description for RespQuestionReport
/// </summary>
public class RespQuestionReport
{
    public RespQuestionReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectRespByQuestionIdDetails(int _QuestionId)
    {
        string procedurename = "PR_PRT_RespQuestion";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@QuestionId", DbType.Int32, _QuestionId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
}
