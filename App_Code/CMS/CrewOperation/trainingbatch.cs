using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for trainingbatch
/// </summary>
public class trainingbatch
{
    public static DataTable bindBatches(int x)
    {
        string procedurename = "Training_Batch";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@trainingid", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable Training()
    {
        string procedurename = "Training_Batch";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable crew_training(int trainid,int batch, int planid)
    {
        string procedurename = "Crew_TrainingDetails";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TrainingID", DbType.Int32, trainid);
        objDatabase.AddInParameter(objDbCommand, "@Batchnumber", DbType.Int32, batch);
        objDatabase.AddInParameter(objDbCommand, "@TrainingPlanningId", DbType.Int32, planid);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataSet updateCrewTraining(int planid,int tr_id, int crewid,int trainreqid, double perheadcost, string frmdt, string todt,  string attend)
       {
        string RValue;
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Update_CrewTraining";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@TrainingId", DbType.Int32, tr_id);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);
        objDatabase.AddInParameter(objDbCommand, "@TrainingPlanningId", DbType.Int32, planid);
        objDatabase.AddInParameter(objDbCommand, "@TrainingRequirementId", DbType.Int32, trainreqid);
        objDatabase.AddInParameter(objDbCommand, "@PerHeadCost", DbType.Double, perheadcost);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, frmdt);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, todt);
        objDatabase.AddInParameter(objDbCommand, "@Attended", DbType.String, attend);


        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
            //RValue = objDatabase.GetParameterValue(objDbCommand, "@ReturnValue").ToString();
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
