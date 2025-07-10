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

/// <summary>
/// Summary description for CreateBatches
/// </summary>
public class CreateBatches
{
    public static DataTable selectPlanTrainingDetails()
    {
        string procedurename = "SelectPlanTrainingDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectBatchesGridDetails(int _id, int _id1)
    {
        string procedurename = "SelectBatchesGridDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@Id1", DbType.Int32, _id1);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static void UpdatebatchesDetails(string _strProc, int _crewid, int _TrainingId, int _TrainingPlanningId, int _batchno, char _confirm)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _crewid);
        oDatabase.AddInParameter(odbCommand, "@TrainingId", DbType.Int32, _TrainingId);
        oDatabase.AddInParameter(odbCommand, "@TrainingPlanningId", DbType.Int32, _TrainingPlanningId);
        oDatabase.AddInParameter(odbCommand, "@BatchNumber", DbType.Int32, _batchno);
        oDatabase.AddInParameter(odbCommand, "@Confirmed", DbType.String, _confirm);
      

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
