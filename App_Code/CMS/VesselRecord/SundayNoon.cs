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
/// Summary description for SundayNoon
/// </summary>
public class SundayNoon
{
    public SundayNoon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getData(int VesselId, int SundayNoonReportId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        SqlDataAdapter adp;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("get_SundayNoonReport");
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@SundayNoonReportId", DbType.Int16, SundayNoonReportId);
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
    public static void Insert_Data(int Id, int VesselId, DateTime Dt, string Lat, string Long, string Course, String Pspeed, string Dist_sailed, string AvgSpeed, string KTS, string DistToGo, string NM, string MEFOCons, string AVGMEFOCons, string AUXMDOCONS, string AVGAUXCONS_MTDAY, string ROBIFO, string ROBDO, string ROBFW, int ETAPORT, string STOPPAGES, string STOPPAGEREASON, string Weatherdelaystovsl, string Wind, string Direction, string Force, string Seas, int _createdby, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertUpdateSundayNoonReport");
        oDatabase.AddInParameter(odbCommand, "@SundayNoonReportId", DbType.Int32, Id);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);
        oDatabase.AddInParameter(odbCommand, "@OnDate", DbType.DateTime, Dt);
        oDatabase.AddInParameter(odbCommand, "@Lat", DbType.String, Lat);
        oDatabase.AddInParameter(odbCommand, "@Long", DbType.String, Long);
        oDatabase.AddInParameter(odbCommand, "@Course", DbType.String, Course);
        oDatabase.AddInParameter(odbCommand, "@Pspeed", DbType.String, Pspeed);

        oDatabase.AddInParameter(odbCommand, "@DSFLP", DbType.String, Dist_sailed);
        oDatabase.AddInParameter(odbCommand, "@AVG_SPEED", DbType.String, AvgSpeed);
        oDatabase.AddInParameter(odbCommand, "@KTS", DbType.String, KTS);
        oDatabase.AddInParameter(odbCommand, "@DIST_TO_GO", DbType.String, DistToGo);
        oDatabase.AddInParameter(odbCommand, "@NM", DbType.String, NM);
        oDatabase.AddInParameter(odbCommand, "@ME_FO_CONS", DbType.String, MEFOCons);
        oDatabase.AddInParameter(odbCommand, "@AVG_ME_FO_CONS", DbType.String, AVGMEFOCons);
        oDatabase.AddInParameter(odbCommand, "@AUX_MDO_CONS", DbType.String, AUXMDOCONS);
        oDatabase.AddInParameter(odbCommand, "@AVG_AUX_CONS", DbType.String, AVGAUXCONS_MTDAY);
        oDatabase.AddInParameter(odbCommand, "@ROB_IFO", DbType.String, ROBIFO);
        oDatabase.AddInParameter(odbCommand, "@ROB_DO", DbType.String, ROBDO);
        oDatabase.AddInParameter(odbCommand, "@ROB_FW", DbType.String, ROBFW);
        oDatabase.AddInParameter(odbCommand, "@ETA_PORT", DbType.Int32, ETAPORT);
        oDatabase.AddInParameter(odbCommand, "@STOP_PAGES", DbType.String, STOPPAGES);
        oDatabase.AddInParameter(odbCommand, "@STOP_PAGE_REASON", DbType.String, STOPPAGEREASON);
        oDatabase.AddInParameter(odbCommand, "@WHETHER_DELAY_TO_VSL", DbType.String, Weatherdelaystovsl);
        oDatabase.AddInParameter(odbCommand, "@Wind", DbType.String, Wind);
        oDatabase.AddInParameter(odbCommand, "@DIRECTION", DbType.String, Direction);
        oDatabase.AddInParameter(odbCommand, "@FORCE", DbType.String, Force);
        oDatabase.AddInParameter(odbCommand, "@SEAS", DbType.String, Seas);


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
    public static void deleteSundayNoonById(int _SundayNoonReportId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("delete_PositionSundayNoonById");
        oDatabase.AddInParameter(odbCommand, "@SundayNoonReportId", DbType.Int32, _SundayNoonReportId);
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
