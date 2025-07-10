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

/// Summary description for CrewPlanner
/// </summary>
public class CrewPlanner
{
    public CrewPlanner()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int getReliverCellColor_Month(int CrewId, DateTime dt)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "getRelieverCellState_Month";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, CrewId);
        objDatabase.AddInParameter(objDbCommand, "@OnDate", DbType.DateTime, dt);
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
        return Convert .ToInt32(objDataset.Tables[0].Rows[0][0].ToString());
    }
    public static int getReliverCellColor(int CrewId, DateTime dt)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DataSet objDataset = new DataSet();
        string procedurename = "getRelieverCellState";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, CrewId);
        objDatabase.AddInParameter(objDbCommand, "@OnDate", DbType.DateTime, dt);
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
        return Convert.ToInt32(objDataset.Tables[0].Rows[0][0].ToString());
    }
    public static DataSet getCrewReliversDetails(int CrewId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "getRelieverDetails";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, CrewId);
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
    public static DataSet getCrewReliversList(int CrewId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "getCrewReliversList";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, CrewId);
        
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
    public static string getSignOn_OffDate(int CrewId)
    {

        Database objDatabase = DatabaseFactory.CreateDatabase();
        //DataSet objDataset = new DataSet();
        string procedurename = "getSignOn_OffDate";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, CrewId);

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
        return objDataset.Tables[0].Rows[0][0].ToString();
    }
    public static DataSet getData(string strprocname, string vessel, string rank, string fromdate, string todate)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
       string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@vessel", DbType.String, vessel);
        objDatabase.AddInParameter(objDbCommand, "@rank", DbType.String, rank);
        objDatabase.AddInParameter(objDbCommand, "@fromdate", DbType.String, fromdate);
        objDatabase.AddInParameter(objDbCommand, "@todate", DbType.String, todate);
        
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

        // Note: Connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataSet getDataSummary(string vessel, string rank, string fromdate, string todate)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectCrewSignOffSummary";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@vessel", DbType.String, vessel);
        objDatabase.AddInParameter(objDbCommand, "@rank", DbType.String, rank);
        objDatabase.AddInParameter(objDbCommand, "@fromdate", DbType.String, fromdate);
        objDatabase.AddInParameter(objDbCommand, "@todate", DbType.String, todate);

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
    public static int getCrewData(string strprocname, int crewid,DateTime dt,ref String relname)
    {
        int res;
       
        res = 0;
      
        Database objDatabase = DatabaseFactory.CreateDatabase();
         string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int16, crewid);
        objDatabase.AddInParameter(objDbCommand, "@Cdate", DbType.DateTime, dt);
        objDatabase.AddOutParameter(objDbCommand,"@Ret", DbType.Int16,res);
       try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
            res = Convert.ToInt32(objDatabase.GetParameterValue(objDbCommand, "@Ret"));
                 
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
            return res;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
            
        }
return res;
        // Note: connection was closed by ExecuteDataSet method call 

      
    }
}
