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
public class cls_DailyNoonReport
{
    public cls_DailyNoonReport()
    {
        
    }
    public static DataSet getData(int VesselId, int DailyNoonReportId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        SqlDataAdapter adp;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("get_DailyNoonReport");
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@DailyNoonReportId", DbType.Int16, DailyNoonReportId);
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
    public static void Insert_Data(int Id, int VesselId,DateTime Dt,string Lat, string Long,string Course,String Pspeed,int _createdby, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertUpdateDailyNoonReport");
        oDatabase.AddInParameter(odbCommand, "@DailyNoonReportId", DbType.Int32, Id);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);
        oDatabase.AddInParameter(odbCommand, "@OnDate", DbType.DateTime, Dt);
        oDatabase.AddInParameter(odbCommand, "@Lat", DbType.String, Lat);
        oDatabase.AddInParameter(odbCommand, "@Long", DbType.String, Long);
        oDatabase.AddInParameter(odbCommand, "@Course", DbType.String, Course);
        oDatabase.AddInParameter(odbCommand, "@Pspeed", DbType.String, Pspeed);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
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
    public static void deletedailyNoonById(int _DailyNoonReportId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("delete_PositionDailyNoonById");
        oDatabase.AddInParameter(odbCommand, "@DailyNoonReportId", DbType.Int32, _DailyNoonReportId);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                odbCommand.Dispose();
            }
        }
    }
    
}
