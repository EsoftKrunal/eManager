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
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CrewNationality
/// </summary>
public class CrewNationality
{
    public static DataSet selectNationality()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DataSet objDataset = new DataSet();
        string procedurename = "SelectNationality";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        

        try
        {
            // execute command and get records
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
