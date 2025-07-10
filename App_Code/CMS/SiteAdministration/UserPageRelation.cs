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
using ShipSoft.CrewManager.BusinessLogicLayer;

/// <summary>
/// Summary description for UserPageRelation
/// </summary>
public class UserPageRelation
{
    public UserPageRelation()
    {
       
    }
    public static DataTable selectDataLoginId(int _roleid )
    {
        string procedurename = "GetAllUsersNames_ByRoleId";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RoleId", DbType.Int32, _roleid);
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
    public static DataTable selectModuleName()
    {
        string procedurename = "SelectApplicationModuleDetails_Page";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectUserPage(int _moduleid,int _roleid,int _LoginId)
    {
        string procedurename = "SelectAppliactionPage_User";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ModuleId", DbType.Int32, _moduleid);
        objDatabase.AddInParameter(objDbCommand, "@RoleId", DbType.Int32, _roleid);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectDataUserPage(int _UserId,int _pageid)
    {
        string procedurename = "SelectUserPages";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int32, _UserId);
        objDatabase.AddInParameter(objDbCommand, "@pageid", DbType.Int32, _pageid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static void deleteUserPageRelationDetails(string _strProc, int _userid,int _moduleid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.Int32, _userid);
        oDatabase.AddInParameter(odbCommand, "@ModuleId", DbType.Int32, _moduleid);


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
    public static void insertUpdateUserVesselRelationDetails(string _strProc, int _userid, int _pageid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.Int32, _userid);
        oDatabase.AddInParameter(odbCommand, "@PageId", DbType.Int32, _pageid);
        

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

    public static int CheckUserPageAuthority(int _UserId, int _Pageid)
    {
        //string procedurename = "CheckUserPageAuthority";
        //DataTable dt2 = new DataTable();

        //Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@UserId", DbType.Int32, _UserId);
        //objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.Int32, _Pageid);

        //using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        //{
        //    dt2.Load(dr);
        //}
        
        //<= error

         AuthenticationManager am = new AuthenticationManager(_Pageid, _UserId, ObjectType.Page);
        if (am.IsView)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
