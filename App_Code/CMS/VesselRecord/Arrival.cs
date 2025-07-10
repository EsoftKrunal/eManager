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
public class cls_Arrival
{
    public cls_Arrival()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getData(int VesselId, int ArrivalId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        SqlDataAdapter adp;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("get_Arrivals");
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@ArrivalId", DbType.Int16, ArrivalId);
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

    public static void insertUpdateArrivalDetails(string _strProc, int _arrivalid, int _vesselid,DateTime _ArrivaldateTime,string _Lat,string _Long,string _Draft_FWD,string _Draft_AFT,string _Rob_IFO,string _Rob_MDO,string _Rob_FW,string _Rob_ODLP,string _ROB_btb,string _ROB_FAOP_EOSP, string _ROB_AVG_SPEED,string _KTS_SAILING_TIME, string _KTS_EOSP_BERTH,string _DBTB,string _ME_FO_CONS,string _Avg,string _AUX_MDO_CONS,string _AVG1,string _AVG_REVS,string _SLIP, string _STOP_AGES, string _REASON, string _REASON_FOR_ANC, int _CreatedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ArrivalId", DbType.Int32, _arrivalid);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _vesselid);
        oDatabase.AddInParameter(odbCommand, "@ArrivaldateTime", DbType.DateTime, _ArrivaldateTime);
        oDatabase.AddInParameter(odbCommand, "@Lat", DbType.String, _Lat);
        oDatabase.AddInParameter(odbCommand, "@Long", DbType.String, _Long);
        oDatabase.AddInParameter(odbCommand, "@Draft_FWD", DbType.String, _Draft_FWD);
        oDatabase.AddInParameter(odbCommand, "@Draft_AFT", DbType.String, _Draft_AFT);
        oDatabase.AddInParameter(odbCommand, "@Rob_IFO", DbType.String, _Rob_IFO);
        oDatabase.AddInParameter(odbCommand, "@Rob_MDO", DbType.String, _Rob_MDO);
        oDatabase.AddInParameter(odbCommand, "@Rob_FW", DbType.String, _Rob_FW);
        oDatabase.AddInParameter(odbCommand, "@Rob_ODLP", DbType.String, _Rob_ODLP);
        oDatabase.AddInParameter(odbCommand, "@ROB_btb", DbType.String, _ROB_btb);
        oDatabase.AddInParameter(odbCommand, "@ROB_FAOP_EOSP", DbType.String, _ROB_FAOP_EOSP);
        oDatabase.AddInParameter(odbCommand, "@ROB_AVG_SPEED", DbType.String, _ROB_AVG_SPEED);
        oDatabase.AddInParameter(odbCommand, "@KTS_SAILING_TIME", DbType.String, _KTS_SAILING_TIME);
        oDatabase.AddInParameter(odbCommand, "@KTS_EOSP_BERTH", DbType.String, _KTS_EOSP_BERTH);
        oDatabase.AddInParameter(odbCommand, "@DBTB ", DbType.String, _DBTB);
        oDatabase.AddInParameter(odbCommand, "@ME_FO_CONS", DbType.String, _ME_FO_CONS);
        oDatabase.AddInParameter(odbCommand, "@Avg", DbType.String, _Avg);
        oDatabase.AddInParameter(odbCommand, "@AUX_MDO_CONS ", DbType.String, _AUX_MDO_CONS);
        oDatabase.AddInParameter(odbCommand, "@AVG1", DbType.String, _AVG1);
        oDatabase.AddInParameter(odbCommand, "@AVG_REVS", DbType.String, _AVG_REVS);
        oDatabase.AddInParameter(odbCommand, "@SLIP", DbType.String, _SLIP);
        oDatabase.AddInParameter(odbCommand, "@STOP_AGES", DbType.String, _STOP_AGES);
        oDatabase.AddInParameter(odbCommand, "@REASON  ", DbType.String, _REASON);
        oDatabase.AddInParameter(odbCommand, "@REASON_FOR_ANC ", DbType.String, _REASON_FOR_ANC);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _CreatedBy);
        



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
    public static void deleteArrivalDetailsById(string _strProc, int _arrivalid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@arrivalid", DbType.Int32, _arrivalid);
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
