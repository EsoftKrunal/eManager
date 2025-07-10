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
/// Summary description for Training
/// </summary>
public class Training
{
    public static DataTable selectDataTrainingTypeId()
    {
        string procedurename = "SelectTrainingType";
        DataTable dt114 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt114.Load(dr);
        }
        return dt114;
    }
    public static DataTable selectDataStatus()
    {
        string procedurename = "Selectstatus";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
    public static DataTable selectDataTrainingDetails()
    {
        string procedurename = "SelectTrainingDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataTrainingDetailsByTrainingId(int _TrainingId)
    {
        string procedurename = "SelectTrainingDetailsByTrainingId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TrainingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdateTrainingDetails(string _strProc, int _Trainingid, string _TrainingName, int _TrainingType, int _createdby, int _modifiedby, char _status, int MTMTrainingId,string _CBT,string _SireChap)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TrainingId", DbType.Int32, _Trainingid);
        oDatabase.AddInParameter(odbCommand, "@TrainingName", DbType.String, _TrainingName);
        oDatabase.AddInParameter(odbCommand, "@TypeOfTraining", DbType.Int32, _TrainingType);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@MTMTrainingId", DbType.Int32, MTMTrainingId);

        oDatabase.AddInParameter(odbCommand, "@CBTNo", DbType.String, _CBT);
        oDatabase.AddInParameter(odbCommand, "@SireChap", DbType.String, _SireChap);
        
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
    public static void deleteTrainingDetails(string _strProc, int _Trainingid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TrainingId", DbType.Int32, _Trainingid);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);
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
    public static DataTable selectDataTrainingDetailsByTrainingTypeId(int _TrainingTypeId)
    {
        string procedurename = "SelectTrainingDetailsByTrainingTypeId";
        DataTable dt38 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TrainingTypeId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt38.Load(dr);
        }

        return dt38;
    }
    public static DataTable selectDataTrainingDetailsByAppraisalId(int _CrewId, int _CrewAppraisalId)
    {
        string procedurename = "SelectCrewTrainingRequirementdetailsByAppraisalId";
        DataTable dt311 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);
        objDatabase.AddInParameter(objDbCommand, "@CrewAppraisalId", DbType.Int32, _CrewAppraisalId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt311.Load(dr);
        }

        return dt311;
    }
}
