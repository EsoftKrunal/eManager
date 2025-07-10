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
/// Summary description for Home_Page
/// </summary>
public class Home_Page
{
    public static DataTable GetCurrentYearInspections()
    {
        string procedurename = "PR_TotalInsCurrentYear";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable GetNext12MonthsInspections()
    {
        string procedurename = "PR_TotalInspNext12Mnths";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable GetNext12MonthsDueInspections()
    {
        string procedurename = "PR_Next12MnthsDueInsp";//PR_TotalDueInspNext12Mnths
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataSet GetSuperintendentAssignments()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_SupDetails";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDataset.Dispose();
            objDbCommand.Dispose();
        }
        return objDataset;
    }
}
