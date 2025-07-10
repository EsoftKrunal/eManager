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
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

public class VesselDetailsOther
{
    public static DataTable selectDataEngineMakerNameDetails(int _EngineMakerType)
    {
        string procedurename = "SelectEngineMakerName";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@EngineMakerType", DbType.Int32, _EngineMakerType);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable selectDataFalgStateNameDetails()
    {
        string procedurename = "SelectFlagName";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
   
    public static DataTable selectDataEngineDesignNameDetails()
    {
        string procedurename = "SelectEngineDesignName";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable selectDataStrokeTypeNameDetails()
    {
        string procedurename = "SelectStrokeTypeName";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
    public static void updateVesselParticulars2(string _strProc, int _vesselId, string _InmarsatTerminalType, string _CallSign,
                                                string _Telephone1, string _Telephone2, string _Mobile, string _Fax, string _Email,
                                                string _Data, string _HSD, string _InmarsatC, int _MainEngineMakerId,
                                                int _aux1EngineMakerId, int _aux2EnginemakerId, int _aux3EnginemakerId,
                                                int _MainEngineStrokeTypeId, int _Aux1StrokeTypeId, int _Aux2EngineStrokeTypeId, int _Aux3EngineStrokeTypeId,
                                                string _mainEngineModel, string _aux1EngineModel, string _aux2EngineModel, string _aux3EngineModel,
                                                string _mainEngineKW, string _aux1EngineKW, string _aux2EngineKW, string _aux3EngineKW,
                                                string _mainengineBHP, string _aux1engineBHP, string _aux2engineBHP, string _aux3engineBHP,
                                                string _mainengineRPM, string _aux1engineRPM, string _aux2engineRPM, string _aux3engineRPM,string AccCode,float TrainingFee, 
                                                int _modifiedby,int ManningOffice)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@vesselid", DbType.Int32, _vesselId);

        oDatabase.AddInParameter(odbCommand, "@InmarsatTerminalType", DbType.String, _InmarsatTerminalType);
        oDatabase.AddInParameter(odbCommand, "@CallSign", DbType.String, _CallSign);
        oDatabase.AddInParameter(odbCommand, "@Telephone1", DbType.String, _Telephone1);
        oDatabase.AddInParameter(odbCommand, "@Telephone2", DbType.String, _Telephone2);
        oDatabase.AddInParameter(odbCommand, "@Mobile", DbType.String, _Mobile);
        oDatabase.AddInParameter(odbCommand, "@Fax", DbType.String, _Fax);
        oDatabase.AddInParameter(odbCommand, "@Email", DbType.String, _Email);
        oDatabase.AddInParameter(odbCommand, "@Data", DbType.String, _Data);
        oDatabase.AddInParameter(odbCommand, "@HSD", DbType.String, _HSD);
        oDatabase.AddInParameter(odbCommand, "@InmarsatC", DbType.String, _InmarsatC);

        oDatabase.AddInParameter(odbCommand, "@MainEngineMakerId", DbType.Int32, _MainEngineMakerId);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineMakerId", DbType.Int32, _aux1EngineMakerId);
        oDatabase.AddInParameter(odbCommand, "@aux2EnginemakerId", DbType.Int32, _aux2EnginemakerId);
        oDatabase.AddInParameter(odbCommand, "@aux3EnginemakerId", DbType.Int32, _aux3EnginemakerId);

        oDatabase.AddInParameter(odbCommand, "@MainEngineStrokeTypeId", DbType.Int32, _MainEngineStrokeTypeId);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineStrokeTypeId", DbType.Int32, _Aux1StrokeTypeId);
        oDatabase.AddInParameter(odbCommand, "@aux2EngineStrokeTypeId", DbType.Int32, _Aux2EngineStrokeTypeId);
        oDatabase.AddInParameter(odbCommand, "@aux3EngineStrokeTypeId", DbType.Int32, _Aux3EngineStrokeTypeId);

        oDatabase.AddInParameter(odbCommand, "@mainEngineModel", DbType.String, _mainEngineModel);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineModel", DbType.String, _aux1EngineModel);
        oDatabase.AddInParameter(odbCommand, "@aux2EngineModel", DbType.String, _aux2EngineModel);
        oDatabase.AddInParameter(odbCommand, "@aux3EngineModel", DbType.String, _aux3EngineModel);
        oDatabase.AddInParameter(odbCommand, "@mainEngineKW", DbType.String, _mainEngineKW);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineKW", DbType.String, _aux1EngineKW);
        oDatabase.AddInParameter(odbCommand, "@aux2EngineKW", DbType.String, _aux2EngineKW);
        oDatabase.AddInParameter(odbCommand, "@aux3EngineKW", DbType.String, _aux3EngineKW);

        oDatabase.AddInParameter(odbCommand, "@mainEngineBHP", DbType.String, _mainengineBHP);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineBHP", DbType.String, _aux1engineBHP);
        oDatabase.AddInParameter(odbCommand, "@aux2EngineBHP", DbType.String, _aux2engineBHP);
        oDatabase.AddInParameter(odbCommand, "@aux3EngineBHP", DbType.String, _aux3engineBHP);
        oDatabase.AddInParameter(odbCommand, "@mainEngineRPM", DbType.String, _mainengineRPM);
        oDatabase.AddInParameter(odbCommand, "@aux1EngineRPM", DbType.String, _aux1engineRPM);
        oDatabase.AddInParameter(odbCommand, "@aux2EngineRPM", DbType.String, _aux2engineRPM);
        oDatabase.AddInParameter(odbCommand, "@aux3EngineRPM", DbType.String, _aux3engineRPM);
        oDatabase.AddInParameter(odbCommand, "@AccCode", DbType.String, AccCode);
        oDatabase.AddInParameter(odbCommand, "@TrainingFee", DbType.Decimal, TrainingFee);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@ManningOfficeID", DbType.Int32, ManningOffice);
        

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
    public static DataTable selectDataVesselParticulars2DetailsByVesselId(int _VesselId)
    {
        string procedurename = "UpdateVesselParticulars2DetailsById";
        DataTable dt7 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt7.Load(dr);
        }

        return dt7;
    }
}
