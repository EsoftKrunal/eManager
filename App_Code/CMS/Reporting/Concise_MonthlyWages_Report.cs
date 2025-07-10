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
/// Summary description for Concise_MonthlyWages_Report
/// </summary>
public class Concise_MonthlyWages_Report
{
    public Concise_MonthlyWages_Report()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable getConciseMonthlyWagesDetails(string _empno, DateTime _from, DateTime _to)
    {
        string procedurename = "Report_Concise_Monthly_Wages";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _empno);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, _from);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, _to);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataSet getComponentName()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "get_Components_Name";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        
        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
}
