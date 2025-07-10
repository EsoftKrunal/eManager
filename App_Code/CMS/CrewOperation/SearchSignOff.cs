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
/// Summary description for SearchSignOff
/// </summary>
public class SearchSignOff
{
    public SearchSignOff()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getMasterData(string TableName, string Field1, string Field2)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetMaster";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@MasterName", DbType.String, TableName);
        objDatabase.AddInParameter(objDbCommand, "@Field1", DbType.String, Field1);
        objDatabase.AddInParameter(objDbCommand, "@Field2", DbType.String, Field2);
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
    public static DataSet getVessel(int _userid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectVesselAccordingToUser";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _userid);
       
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
    public static DataSet getVesselCodeandName()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectVesselCodeandName";
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
    public static DataSet getCrewDetails(string vessel, string rank,string fromdate,string todate)
    {
       

        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectCrewSignOff";
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
    public static DataSet getPlanningDetails(int s_crewid,int s_session)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Planning";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@Relivee_CrewID", DbType.Int32, s_crewid);
        objDatabase.AddInParameter(objDbCommand, "@LoginSessionID", DbType.Int32, s_session);
             
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
    public static void DelReliver_Tempplanning(int _RowID)
        {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("DelReliver_temp");
        oDatabase.AddInParameter(odbCommand, "@RowID", DbType.Int32, _RowID);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
                
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
                
            }
                  
        }
    }
    public static void UpdReliver_Tempplanning(int crid,  int relid, int relid1, int upd_flag)
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("upd_reliver");
        oDatabase.AddInParameter(odbCommand, "@crewid", DbType.Int32, crid);
        oDatabase.AddInParameter(odbCommand, "@ReliverID", DbType.Int32, relid);
        oDatabase.AddInParameter(odbCommand, "@ReliverID1", DbType.Int32, relid1);
        oDatabase.AddInParameter(odbCommand, "@upd_flag", DbType.Int32, upd_flag);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }
    public static void DeleteReliver_planning(int crid,int VesselId)
        {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Delete_reliver");

        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, crid);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);

       
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();

            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();

            }

        }

    }
    public static int Check_Planning_Deleted(int Vesselid, int CrewId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Check_Planning_Deleted";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, Vesselid);
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

        return Convert.ToInt32(objDataset.Tables[0].Rows[0][0].ToString()) ;
    }
    public static DataTable getCrewRoleId(int _loginId)
    {
        string procedurename = "get_Crew_RoleId";
        DataTable dt88 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt88.Load(dr);
        }

        return dt88;
    }
    public static DataTable DeleteCrewfromPlanning(int _crewid)
    {
        string procedurename = "delete_RelieverfromPlanning";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;

    }

}
