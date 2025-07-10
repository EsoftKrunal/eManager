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
/// Summary description for Owner
/// </summary>
public class Owner
{
    public static DataTable selectDataOwnerDetails()
    {
        string procedurename = "SelectOwnerDetails";
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

    public static DataTable selectDataOwnerPoolName()
    {
        string procedurename = "SelectOwnerPoolName";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }

    public static DataTable selectDataOwnerDetailsByOwnerId(int _OwnerId)
    {
        string procedurename = "SelectOwnerDetailsByOwnerId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void insertUpdateOwnerDetails(string _strProc, int _OwnerId, int _OwnerPoolId, string _OwnerName, string _OwnerShortName, string _OwnerCode, int _createdBy, int _modifiedBy, char _statusId, string _contactno, string _primaryMailId, string _secondaryMailId, string _thirdMailId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Ownerid", DbType.Int32, _OwnerId);
        oDatabase.AddInParameter(odbCommand, "@OwnerPoolid", DbType.Int32, _OwnerPoolId);
        oDatabase.AddInParameter(odbCommand, "@OwnerName", DbType.String, _OwnerName);
        oDatabase.AddInParameter(odbCommand, "@OwnerShortName", DbType.String, _OwnerShortName);
        oDatabase.AddInParameter(odbCommand, "@OwnerCode", DbType.String, _OwnerCode);
        oDatabase.AddInParameter(odbCommand, "@createdby", DbType.Int32, _createdBy);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, _modifiedBy);
        oDatabase.AddInParameter(odbCommand, "@statusid", DbType.String, _statusId);
        oDatabase.AddInParameter(odbCommand, "@contactNo", DbType.String, _contactno);
        oDatabase.AddInParameter(odbCommand, "@primaryMailId", DbType.String, _primaryMailId);
        oDatabase.AddInParameter(odbCommand, "@secondaryMailId", DbType.String, _secondaryMailId);
        oDatabase.AddInParameter(odbCommand, "@ThirdMailId", DbType.String, _thirdMailId);

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

    public static void deleteOwnerDetailsById(string _strProc, int _OwnerId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Ownerid", DbType.Int32, _OwnerId);
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
