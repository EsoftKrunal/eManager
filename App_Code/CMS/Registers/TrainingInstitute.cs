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
/// Summary description for TrainingInstitute
/// </summary>
public class TrainingInstitute
{
    public TrainingInstitute()
    {
       
    }
    public static DataTable selectDataTrainingInstituteDetails()
    {
        string procedurename = "SelectTrainingInstituteDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectDataStatusDetails()
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

    public static DataTable selectDataTrainingInstituteDetailsByTrainingInstituteId(int _TrainingInstituteId)
    {
        string procedurename = "SelectTrainingInstituteDetailsByTrainingInstituteId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InstituteId", DbType.Int32, _TrainingInstituteId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }
        return dt3;
    }

    public static void insertUpdateTrainingInstituteDetails(string _strProc, int _TrainingInstituteId, string _TrainingInstituteName, int _createdBy, int _modifiedBy, char _statusId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Instituteid", DbType.Int32, _TrainingInstituteId);
        oDatabase.AddInParameter(odbCommand, "@Institutename", DbType.String, _TrainingInstituteName);
        oDatabase.AddInParameter(odbCommand, "@createdby", DbType.Int32, _createdBy);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, _modifiedBy);
        oDatabase.AddInParameter(odbCommand, "@statusid", DbType.String, _statusId);

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

    public static void deleteTrainingInstituteDetailsById(string _strProc, int _TrainingInstituteId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Instituteid", DbType.Int32, _TrainingInstituteId);
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
}
