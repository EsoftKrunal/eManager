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
/// Summary description for cls_SearchReliever
/// </summary>
public class cls_SearchReliever
{
    public cls_SearchReliever()
    {
    
    }
    public static DataTable SelectPassportNoOfAllCrews()
    {
        string procedurename = "SelectPassportOfAllCrewMembers";
        DataTable dt71 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt71.Load(dr);
        }

        return dt71;
    }
    public static DataTable SelectPassportNoforWebApplications(string _passportnumber)
    {
        string procedurename = "SelectPassportforWebApplication";
        DataTable dt77 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PassportNo", DbType.String, _passportnumber);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt77.Load(dr);
        }

        return dt77;
    }
    public static DataTable SelectParentOfFamilyMember(string Empno)
    {
        string procedurename = "SelectCrewMemberDetailsAccordingToFamilyMemberNumber";
        DataTable dt61 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FamilyNumber", DbType.String, Empno);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt61.Load(dr);
        }

        return dt61;
    }
    public static DataTable Check_CrewContractDate(DateTime _startdate, int _vesselid)
    {
        string procedurename = "check_contractdate";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@StartDate", DbType.DateTime, _startdate);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataSet getMasterData(string TableName, string Field1,string Field2, int loginid=0)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetMaster";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@MasterName", DbType.String , TableName);
        objDatabase.AddInParameter(objDbCommand, "@Field1", DbType.String, Field1 );
        objDatabase.AddInParameter(objDbCommand, "@Field2", DbType.String, Field2 );
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int32, loginid);
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
    public static DataSet getRelievers(string OwnerPool, string BNationality, string VesselType, string Vessel, string Tankers, string Rank, int RelType, int Exclude, string FName, string LName, string EmpNo, string RecOff, string Nationality,string Month,string Year)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetReliversList";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();

        objDatabase.AddInParameter(objDbCommand, "@ReliverType", DbType.Int32,RelType);
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.String, VesselType);
        objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.String, Vessel);
        objDatabase.AddInParameter(objDbCommand, "@OwnerPool", DbType.String, OwnerPool);
        objDatabase.AddInParameter(objDbCommand, "@Rank", DbType.String, Rank);
        objDatabase.AddInParameter(objDbCommand, "@MatrixTankers", DbType.String, Tankers);
        objDatabase.AddInParameter(objDbCommand, "@BNationality", DbType.String, BNationality);
        objDatabase.AddInParameter(objDbCommand, "@ExcludePlanned", DbType.Int32, Exclude);
        objDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String,FName);
        objDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, LName);
        objDatabase.AddInParameter(objDbCommand, "@EmpNo", DbType.String, EmpNo);
        objDatabase.AddInParameter(objDbCommand, "@RecrtOffice", DbType.String, RecOff);
        objDatabase.AddInParameter(objDbCommand, "@Nationality", DbType.String, Nationality);
        objDatabase.AddInParameter(objDbCommand, "@SMonth", DbType.String, Month );
        objDatabase.AddInParameter(objDbCommand, "@SYear", DbType.String, Year) ;
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
    public static int Add_Reliver(int ReliverId,int ReliverRankId,int ReliveeId,int ReliveeRankId,String Remark,int PlannedById, int PlannedRank, string Promotion) //string PortId,
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("AddReliver");
        oDatabase.AddInParameter(odbCommand, "@ReliveeRankID", DbType.Int32, ReliveeRankId);
        oDatabase.AddInParameter(odbCommand, "@ReliveeID", DbType.Int32, ReliveeId);
        oDatabase.AddInParameter(odbCommand, "@ReliverRankID", DbType.Int32, ReliverRankId);
        oDatabase.AddInParameter(odbCommand, "@ReliverId", DbType.Int32, ReliverId);
        //oDatabase.AddInParameter(odbCommand, "@PortId", DbType.Int32, PortId);
        oDatabase.AddInParameter(odbCommand, "@Remark", DbType.String, Remark);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, RValue);
        oDatabase.AddInParameter(odbCommand, "@PlannedBy", DbType.Int32, PlannedById);
        oDatabase.AddInParameter(odbCommand, "@PlannedRank", DbType.Int32, PlannedRank);
        oDatabase.AddInParameter(odbCommand, "@IsPromotion", DbType.String, Promotion);

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
                return  RValue;
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
                return  RValue;
        }
    }
    public static DataSet get_Vessels(int VType)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "get_VesselById";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.Int32,VType);
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
    public static DataSet get_Vessels_Login(int VType,int LoginId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "get_Vessel_LoginById";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselType", DbType.Int32, VType);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
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
    public static DataTable Chk_VesselType(int VType)
    {
        string procedurename = "ChkVesselType";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
       objDatabase.AddInParameter(objDbCommand, "@VesselTypeID", DbType.Int32, VType);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable Chk_VesselType_fromVessel(int VesselId)
    {
        string procedurename = "chk_VesselTypeId_fromVessel";
        DataTable dt32 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselID", DbType.Int32, VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt32.Load(dr);
        }

        return dt32;
    }
    public static int IS_DOCUMENTS_AVAILABLE(int VesselId,int relid, int rankid, int CrewId)
    {
        DataTable dt3 = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("IS_DOCUMENTS_AVAILABLE");
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);
        oDatabase.AddInParameter(odbCommand, "@ReliveeId", DbType.Int32, relid);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, rankid);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, CrewId);
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt3.Load(dr);
        }
        return Convert.ToInt32(dt3.Rows[0][0].ToString());
    }

    public static DataSet getAccountHead()
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "GetAccountHead";
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
