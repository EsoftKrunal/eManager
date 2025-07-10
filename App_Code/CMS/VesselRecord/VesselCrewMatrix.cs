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
using System.Transactions; 

/// <summary>
/// Summary description for VesselCrewMatrix
/// </summary>
public class VesselCrewMatrix
{
    public VesselCrewMatrix()
    {
       
    }
    public static DataSet getData(string strprocname, int vesselid,string signondate,string signoffdate)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, vesselid);
        if (signondate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.DateTime,null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOnDate", DbType.DateTime, Convert.ToDateTime(signondate));
        }
        if (signoffdate == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, Convert.ToDateTime(signoffdate));
        }
        
        
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
