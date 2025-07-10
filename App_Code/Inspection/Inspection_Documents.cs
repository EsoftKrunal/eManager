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
/// Summary description for Inspection_Documents
/// </summary>
public class Inspection_Documents
{
	public Inspection_Documents()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable DocumentDetails(int _id, int _superintendentid, int _doctypeid, string _docname, string _filepath, int _createdby, int _modifiedby, string _transtype)
    {
        string procedurename = "PR_ADMS_InspectionDocuments";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@SuperIntendentId", DbType.Int32, _superintendentid);
        objDatabase.AddInParameter(objDbCommand, "@DocumentTypeId", DbType.String, _doctypeid);
        objDatabase.AddInParameter(objDbCommand, "@DocumentName", DbType.String, _docname);
        objDatabase.AddInParameter(objDbCommand, "@FilePath", DbType.String, _filepath);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);

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
}
