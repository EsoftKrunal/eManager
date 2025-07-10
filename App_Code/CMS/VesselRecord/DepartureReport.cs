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
/// Summary description for DepartureReport
/// </summary>
public class DepartureReport
{
    public DepartureReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getData(int VesselId, int DepartureReportId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        SqlDataAdapter adp;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("get_DepartureReport");
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@DepartureReportId", DbType.Int16, DepartureReportId);
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
    public static void Insert_Data(int Id, int VesselId, int PortId, DateTime Dt, string Lat, string Long, string Draft_fwd, string Draft_Aft,
                                    string Recd_Fuel, string Recd_Ifo, string Recd_Mdo, string Recd_Fwd, string Rob_Fuel, string Rob_Ifo, string Rob_Mdo, string Rob_Fwd, string Eta_Next_Port,
                                    string Distance_Next_Port, string Cargo_Activity, string Cargo_Name, string Ships_Qty, string Shore_Qty, string Stowage, string Tank_Capacity_Each, string Time_Log, int _createdby, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertUpdateDepartureReport");
        oDatabase.AddInParameter(odbCommand, "@DepartureReportId", DbType.Int32, Id);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, VesselId);
        oDatabase.AddInParameter(odbCommand, "@PortId", DbType.Int32, PortId);
        oDatabase.AddInParameter(odbCommand, "@OnDate", DbType.DateTime, Dt);
        oDatabase.AddInParameter(odbCommand, "@Lat", DbType.String, Lat);
        oDatabase.AddInParameter(odbCommand, "@Long", DbType.String, Long);
        oDatabase.AddInParameter(odbCommand, "@DraftFWD", DbType.String, Draft_fwd);
        oDatabase.AddInParameter(odbCommand, "@DraftAFT", DbType.String, Draft_Aft);
        oDatabase.AddInParameter(odbCommand, "@RecdFuel", DbType.String, Recd_Fuel);
        oDatabase.AddInParameter(odbCommand, "@RecdIFO", DbType.String, Recd_Ifo);
        oDatabase.AddInParameter(odbCommand, "@RecdMDO", DbType.String, Recd_Mdo);
        oDatabase.AddInParameter(odbCommand, "@RecdFWD", DbType.String, Recd_Fwd);
        oDatabase.AddInParameter(odbCommand, "@RobFuel", DbType.String, Rob_Fuel);
        oDatabase.AddInParameter(odbCommand, "@RobIFO", DbType.String, Rob_Ifo);
        oDatabase.AddInParameter(odbCommand, "@RobMDO", DbType.String, Rob_Mdo);
        oDatabase.AddInParameter(odbCommand, "@RobFWD", DbType.String, Rob_Fwd);
        oDatabase.AddInParameter(odbCommand, "@ETANextPort", DbType.String, Eta_Next_Port);
        oDatabase.AddInParameter(odbCommand, "@DistanceNextPort", DbType.String, Distance_Next_Port);
        oDatabase.AddInParameter(odbCommand, "@CargoActivity", DbType.String, Cargo_Activity);
        oDatabase.AddInParameter(odbCommand, "@CargoName", DbType.String, Cargo_Name);
        oDatabase.AddInParameter(odbCommand, "@ShipsQty", DbType.String, Ships_Qty);
        oDatabase.AddInParameter(odbCommand, "@ShoreQty", DbType.String, Shore_Qty);
        oDatabase.AddInParameter(odbCommand, "@Stowage", DbType.String, Stowage);
        oDatabase.AddInParameter(odbCommand, "@TankCapacityEach", DbType.String, Tank_Capacity_Each);
        oDatabase.AddInParameter(odbCommand, "@TimeLog", DbType.String, Time_Log);
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
    public static void deletedepartureById(int _DepartureReportId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("delete_PositionDepartureById");
        oDatabase.AddInParameter(odbCommand, "@DepartureReportId", DbType.Int32, _DepartureReportId);
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
