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
/// Summary description for UserRights
/// </summary>
public class UserRights
{
    public static DataTable selectDataModulesDetails()
    {
        string procedurename = "SelectApplicationModuleDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectRoleDetails()
    {
        string procedurename = "SelectApplicationRoleDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataAuthorityDetails(int _ApplicationModuleId)
    {
        string procedurename = "SelectAuthorityDetails";
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
    public static void insertUpdateAuthorityDetails(string _strProc, int _ApplicationModuleId, int _UserId, char _add, char _modify, char _delete, char _print, char _verify)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ApplicationModuleId", DbType.Int32, _ApplicationModuleId);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.Int32, _UserId);
        oDatabase.AddInParameter(odbCommand, "@CanAdd", DbType.String, _add);
        oDatabase.AddInParameter(odbCommand, "@CanModify", DbType.String, _modify);
        oDatabase.AddInParameter(odbCommand, "@CanDelete", DbType.String, _delete);
        oDatabase.AddInParameter(odbCommand, "@CanPrint", DbType.String, _print);
        oDatabase.AddInParameter(odbCommand, "@CanVerify", DbType.String, _verify);
        
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
    public static DataTable selectadminroleId(int _LoginId)
    {
        string procedurename = "selectuserroleIdbyloginid";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@loginid", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectsuperuser(int _LoginId)
    {
        string procedurename = "selectsuperuserbyloginid";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@loginid", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }

        return dt12;
    }
}
