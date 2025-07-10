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
/// Summary description for EngineMaker
/// </summary>
public class EngineMaker
{
    public static DataTable selectDataEngineMakerDetails()
    {
        string procedurename = "SelectEngineMakerDetails";
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

    public static DataTable selectDataEngineMakerDetailsByEngineMakerId(int _EngineMakerId)
    {
        string procedurename = "SelectEngineMakerDetailsByEngineMakerId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@EngineMakerId", DbType.Int32, _EngineMakerId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void insertUpdateEngineMakerDetails(string _strProc, int _EngineMakerId, string _EngineMakerName, char _EngineMakerTypeId, int _createdBy, int _modifiedBy, char _statusId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@EngineMakerid", DbType.Int32, _EngineMakerId);
        oDatabase.AddInParameter(odbCommand, "@EngineMakerName", DbType.String, _EngineMakerName);
        oDatabase.AddInParameter(odbCommand, "@EngineMakerType", DbType.String, _EngineMakerTypeId);        
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

    public static void deleteEngineMakerDetailsById(string _strProc, int _EngineMakerId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@EngineMakerid", DbType.Int32, _EngineMakerId);
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
