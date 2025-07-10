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
/// Summary description for MiningScale
/// </summary>
public class MiningScale
{
    public MiningScale()
    {
        
    }
    public static DataSet getData(string strprocname,int vesselid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        if (vesselid != 0)
        {
            objDatabase.AddInParameter(objDbCommand,"@VesselId",DbType.Int16,vesselid);
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
    public static void insertVesselMiningScale(string _strProc, int _vesselManningScaleId, int _vesselid, int _rankid, int _nationalityid, int _nationalitygroupid, int _ownerscale, int _safescale, string _remarks, char _StatusId, int _createdby, int _modifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@VesselManningScaleId", DbType.Int16, _vesselManningScaleId);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int16, _vesselid);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int16, _rankid);
        oDatabase.AddInParameter(odbCommand, "@NationalityId", DbType.Int16, _nationalityid);
        oDatabase.AddInParameter(odbCommand, "@NationalityGroupId", DbType.Int16, _nationalitygroupid);
        oDatabase.AddInParameter(odbCommand, "@OwnerScale", DbType.Int16, _ownerscale);
        oDatabase.AddInParameter(odbCommand, "@SafeScale", DbType.Int16, _safescale);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, _remarks);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _StatusId);
        oDatabase.AddInParameter(odbCommand, "@CreatedBY", DbType.Int16, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int16, _modifiedBy);

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
    public static DataSet getManningData(string strprocname, int _vesselid )
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
       
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, _vesselid);
        
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
    public static void deletemanningdetails(String strprocname, int _nationalityid, int _nationgroupid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(strprocname);
        oDatabase.AddInParameter(odbCommand, "@NID", DbType.Int32, _nationalityid);
        oDatabase.AddInParameter(odbCommand, "@NGID", DbType.Int32, _nationgroupid);

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
    public static DataSet getNation(string strprocname, int _ngid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
       
            objDatabase.AddInParameter(objDbCommand, "@ngid", DbType.Int16, _ngid);
       
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
    public static void instVesselMinScale(string _strProc, int vid, int rankid, int nationid, int ngid, string oscale, string ssacle, int createdby, int modifiedby,string statusid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int16, vid);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int16, rankid);
        oDatabase.AddInParameter(odbCommand, "@NationalityId", DbType.Int16, nationid);
        oDatabase.AddInParameter(odbCommand, "@NationalityGroupId", DbType.Int16, ngid);
        oDatabase.AddInParameter(odbCommand, "@OwnerScale", DbType.String, oscale);
        oDatabase.AddInParameter(odbCommand, "@SafeScale", DbType.String, ssacle);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, statusid);
        oDatabase.AddInParameter(odbCommand, "@CreatedBY", DbType.Int16, createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int16, modifiedby);

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
    public static void Delete_VesselManningScale(int vid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Delete_VesselManningScale");
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.Int16, vid);
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
    





}
