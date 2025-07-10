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
/// Summary description for Designation_Master
/// </summary>
public class Designation_Master
{
    public static string ErrMsg = "";
    public static DataTable DesignationDetails(int _id, string _desgname, string _desc, int _createdby, int _modifiedby, string _transtype)
    {
        string procedurename = "PR_ADMS_Designation";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@Designation", DbType.String, _desgname);
        objDatabase.AddInParameter(objDbCommand, "@Description", DbType.String, _desc);
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
        catch(SqlException se)
        {
            dt1.Dispose();
            ErrMsg = se.Message;
            return null;
        }
    }
}
