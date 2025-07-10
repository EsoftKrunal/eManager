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
public class PortPlanner
{
    public PortPlanner()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet insertdata(int vessel, int port, string fromdate, string todate,string crwlist, string reliverlist,
                                        int _createdby, 
                                        ref string NewId)
    {
        string RValue;
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Insert_Port_Planner";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
     
        objDatabase.AddInParameter(objDbCommand, "@portid", DbType.Int32, port);
        objDatabase.AddInParameter(objDbCommand, "@d1", DbType.String, fromdate);
        objDatabase.AddInParameter(objDbCommand, "@d2", DbType.String, todate);
        objDatabase.AddInParameter(objDbCommand, "@vesselcode", DbType.Int32, vessel);
        objDatabase.AddInParameter(objDbCommand, "@crewlist", DbType.String, crwlist);
        objDatabase.AddInParameter(objDbCommand, "@relieverlist", DbType.String, reliverlist);

        objDatabase.AddInParameter(objDbCommand, "@createdby", DbType.Int32, _createdby);

        objDatabase.AddOutParameter(objDbCommand, "@ReturnValue", DbType.String,100);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
            RValue = objDatabase.GetParameterValue(objDbCommand, "@ReturnValue").ToString();
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
        NewId = RValue;
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
        objDatabase.AddOutParameter(objDbCommand, "@RelName", DbType.String,100);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
            res = Convert.ToInt32(objDatabase.GetParameterValue(objDbCommand, "@Ret"));
            relname = objDatabase.GetParameterValue(objDbCommand, "@RelName").ToString();
          
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
    public static DataSet getCrewDetails(string vessel)
    {


        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "MergeSignOffSignON";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@vessel", DbType.String, vessel);
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
    public static DataTable selectCountryName()
    {
        string procedurename = "get_CountryName";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectPortName(int _countryid)
    {
        string procedurename = "get_PortNameByCountryId";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CountryId", DbType.Int32, _countryid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable selectCountry(int _portid)
    {
        string procedurename = "SelectPortDetailsByPortId";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@portid", DbType.Int32, _portid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
}
