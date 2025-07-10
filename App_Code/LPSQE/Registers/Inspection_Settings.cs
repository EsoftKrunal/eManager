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
/// Summary description for Inspection_Settings
/// </summary>
public class Inspection_Settings
{
    public static string ErrMsg = "";
    public static DataTable InspectionSettingsDetails(int _id, string _inspduestatus, int _alertperiod, string _statuscolor, string _statusicon, string _mailto, int _createdby, int _modifiedby, string _transtype)
    {
        string procedurename = "PR_ADMS_InspectionSettings";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@InspDueStatus", DbType.String, _inspduestatus);
        objDatabase.AddInParameter(objDbCommand, "@AlertPeriod", DbType.Int32, _alertperiod);
        objDatabase.AddInParameter(objDbCommand, "@StatusColor", DbType.String, _statuscolor);
        objDatabase.AddInParameter(objDbCommand, "@StatusIcon", DbType.String, _statusicon);
        objDatabase.AddInParameter(objDbCommand, "@MailTo", DbType.String, _mailto);
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
