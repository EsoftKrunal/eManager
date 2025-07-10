using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for MedicalTestDoneByPeriod
/// </summary>
public class MedicalTestDoneByPeriod
{
    public static DataTable selectMedicalDetailsData(int _vid,int _did,string _fd,string _td)
    {
        string procedurename = "PrintMedicalTestReportByPeriod";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vid);
        objDatabase.AddInParameter(objDbCommand, "@DocumentTypeId", DbType.Int32, _did);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.String, _fd);
        objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.String, _td);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
