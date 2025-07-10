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
/// Summary description for NewPlanning
/// </summary>
public class NewPlanning
{
    public NewPlanning()
    {
    }
    public static DataTable selectDataVesselDetails(int _VesselId)
    {
        string procedurename = "SelectVessel";
        DataTable dt45 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt45.Load(dr);
        }

        return dt45;
    }
    public static DataTable selectDataVesselNameDetails(int _VesselId)
    {
        string procedurename = "SelectVesselMgmtType";
        DataTable dt8 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataSet getCrewDetails(string vessel)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectCrewSignOff_New";
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
    public static DataTable getmatrix()
    {
        string procedurename = "selectddlmatrix";
        DataTable dt8 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataSet getReliever(string vessel)
    {


        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectReliever_Port_Planner";
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
    public static void UpdReliver_Tempplanning(int crid, int _RowID, int relid, int relid1, int upd_flag, int _Loginid, int _rel_rankid, int _rel_rankid1)
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("upd_reliver");
        oDatabase.AddInParameter(odbCommand, "@RowID", DbType.Int32, _RowID);
        oDatabase.AddInParameter(odbCommand, "@crewid", DbType.Int32, crid);

        oDatabase.AddInParameter(odbCommand, "@ReliverID", DbType.Int32, relid);
        oDatabase.AddInParameter(odbCommand, "@ReliverID1", DbType.Int32, relid1);
        oDatabase.AddInParameter(odbCommand, "@upd_flag", DbType.Int32, upd_flag);
        oDatabase.AddInParameter(odbCommand, "@LoginSessionId", DbType.Int32, _Loginid);
        oDatabase.AddInParameter(odbCommand, "@rel_rankid", DbType.Int32, _rel_rankid);
        oDatabase.AddInParameter(odbCommand, "@rel_rankid1", DbType.Int32, _rel_rankid1);


        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                //RValue = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
    public static DataSet getstatus()
    
    
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectVesselPlanmingStatus";
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
        return objDataset;
    }
    public static DataSet getRelievers(string OwnerPool, string BNationality, string VesselType, string Vessel, string Tankers, string Rank, int RelType, int Exclude, string FName, string LName, string EmpNo,string RecOffice,string Nationalityid, int VesselId, string Compatyibility,int LoginID)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetReliversList1";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();

        objDatabase.AddInParameter(objDbCommand, "@ReliverType", DbType.Int32, RelType);
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.String, VesselType);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.String, Vessel);
        objDatabase.AddInParameter(objDbCommand, "@OwnerPool", DbType.String, OwnerPool);
        objDatabase.AddInParameter(objDbCommand, "@Rank", DbType.String, Rank);
        objDatabase.AddInParameter(objDbCommand, "@MatrixTankers", DbType.String, Tankers);
        objDatabase.AddInParameter(objDbCommand, "@BNationality", DbType.String, BNationality);
        objDatabase.AddInParameter(objDbCommand, "@ExcludePlanned", DbType.Int32, Exclude);
        objDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, FName);
        objDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, LName);
        objDatabase.AddInParameter(objDbCommand, "@EmpNo", DbType.String, EmpNo);
        objDatabase.AddInParameter(objDbCommand, "@RecrtOffice", DbType.String, RecOffice);
        objDatabase.AddInParameter(objDbCommand, "@Nationality", DbType.String, Nationalityid);
        objDatabase.AddInParameter(objDbCommand, "@SMOnth", DbType.String, 1);
        objDatabase.AddInParameter(objDbCommand, "@SYear", DbType.String, 0);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@Compatibility", DbType.String, Compatyibility);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginID);
        



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
    public static DataSet getFamilyMemberRelivers()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetFamilyMemberReliversList";
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
    public static int Add_Planning(int CrewId, int VesselId, string Remark, int LoginId,int PlannedRank,string Promotion) //string PortId,
    {
        int RValue;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Add_Planning");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId );
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);
        //oDatabase.AddInParameter(odbCommand, "@PortId", DbType.Int32, PortId);
        oDatabase.AddInParameter(odbCommand, "@Remark", DbType.String, Remark);
        oDatabase.AddInParameter(odbCommand, "@PlannedBy", DbType.Int32, LoginId);
        oDatabase.AddInParameter(odbCommand, "@PlannedRank", DbType.Int32, PlannedRank);
        oDatabase.AddInParameter(odbCommand, "@IsPromotion", DbType.String, Promotion);
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
            return 0;
        }
    }
    public static int Check_RelieverStatus(int CrewId, int VesselId)
    {
        DataSet objDataset = new DataSet();
        int RValue;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Check_RelieverStatus");
        oDatabase.AddInParameter(odbCommand, "@RelieverId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId );
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                objDataset = oDatabase.ExecuteDataSet(odbCommand);
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
                odbCommand.Dispose();
            }
            // Note: connection was closed by ExecuteDataSet method call 
            return Convert.ToInt32(objDataset.Tables[0].Rows[0][0]);
        }
    }
    public static int Check_RelieverStatus1(int CrewId, int CrewId1)
    {
        DataSet objDataset = new DataSet();
        int RValue;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Check_RelieverStatus1");
        oDatabase.AddInParameter(odbCommand, "@RelieverId", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@ReliveeId", DbType.Int32, CrewId1);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                objDataset = oDatabase.ExecuteDataSet(odbCommand);
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
                odbCommand.Dispose();
            }
            // Note: connection was closed by ExecuteDataSet method call 
            return Convert.ToInt32(objDataset.Tables[0].Rows[0][0]);
        }
    }
    public static int getRankGroupId(int rankid)
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("getRankGroupId");
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, rankid);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, 0);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                RValue = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
            return RValue;
        }
    }
    public static DataSet get_Vessels(int VType)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "get_VesselById";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.Int32, VType);
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
