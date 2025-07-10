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
/// Summary description for UserRights_VIMS
/// </summary>
public class UserRights_VIMS
{
    # region "Assigned User"
    public static DataTable selectDataModulesDetails_ForVIMS()
    {
        string procedurename = "SelectApplicationModuleDetails_VIMS";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectDataAuthorityDetails_ForVIMS(int _ApplicationModuleId)
    {
        string procedurename = "SelectAuthorityDetails_VIMS";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ApplicationModuleId", DbType.Int32, _ApplicationModuleId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static void InsertUpdateAuthorityDetails_ForVIMS(string _strProc, int _ApplicationModuleId, int _userId, char _add, char _modify, char _delete, char _print, char _firstapp, char _secondapp)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ApplicationModuleId", DbType.Int32, _ApplicationModuleId);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.Int32, _userId);
        oDatabase.AddInParameter(odbCommand, "@CanAdd", DbType.String, _add);
        oDatabase.AddInParameter(odbCommand, "@CanModify", DbType.String, _modify);
        oDatabase.AddInParameter(odbCommand, "@CanDelete", DbType.String, _delete);
        oDatabase.AddInParameter(odbCommand, "@CanPrint", DbType.String, _print);
        oDatabase.AddInParameter(odbCommand, "@Approval1", DbType.String, _firstapp);
        oDatabase.AddInParameter(odbCommand, "@Approval2", DbType.String, _secondapp);

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
    # endregion

    # region "Onbehalf User"
    public static DataTable selectUserNames_ForVIMS()
    {
        string procedurename = "GetAllUsersNames";// SelectUserNames_VIMS
        DataTable dtu = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtu.Load(dr);
        }
        return dtu;
    }
    public static DataTable selectUserOnBehalf_ForVIMS(int _AssgUserId)
    {
        string procedurename = "SelectOnbehalfUserNames_VIMS";
        DataTable dto = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AssgnUserId", DbType.Int32, _AssgUserId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dto.Load(dr);
        }
        return dto;
    }
    public static void InsertUpdateUserOnbehalfDetails_ForVIMS(string _strProc, int _AssgnUserid, int _OnbehalfUserid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@AssignedUserId", DbType.Int32, _AssgnUserid);
        oDatabase.AddInParameter(odbCommand, "@OnbehalfUserId", DbType.Int32, _OnbehalfUserid);

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
    public static void deleteUserOnbehalf(string _strProc, int _assguserid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@AssignedUserId", DbType.Int32, _assguserid);
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
    # endregion
}
