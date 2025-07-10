using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;

namespace energiosSecurity
{

/// <summary>
/// Summary description for User
/// </summary>
    public class Menu
{
       
        public Menu()
	{
		//
		// TODO: Add constructor logic here
		//
	}


        public System.Data.DataTable GetAll()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetAllMenu", dt);

            return dt;
        }
   
    public System.Data.DataTable GetDetails_ByModuleId(int mid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@moduleid"		, mid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetMenuBy_ModuleID", dt, parameters);
        return dt;
    }

    public System.Data.DataTable GetDetails_ByParentMenuId(int pid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@pid"		, pid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetMenuBy_ParentID", dt, parameters);
        return dt;
    }
    public System.Data.DataTable GetMenu_ByUserId(int userid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@userid"		, userid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetMenuBy_UserID", dt, parameters);
        return dt;
    }
    public System.Data.DataTable GetActionRights(int userid,int menuid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@userid"		, userid   ) ,
                new SqlParameter( "@menuid"		, menuid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetActionRights", dt, parameters);
        return dt;
    }

    public System.Data.DataTable GetDetails_ByMenuId(int mid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@mid"		, mid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetMenuBy_MenuID", dt, parameters);
        return dt;
    }
    public bool IsMenuHavingChilds(int mid,DataTable dtMain)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@pid"		, mid   ) ,
                new SqlParameter( "@result",false)
            };
        parameters[1].Direction = ParameterDirection.Output;

            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsMenuHavingChilds", parameters);
        if ((bool)parameters[1].Value == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        //DataTable tblChild=new DataTable();
        //tblChild = AppHelper.FilterDataTable(dt, "ParentMenuID=" + mid);
        //if (tblChild.Rows.Count>0 )
        //{
        //    return true;
        //}
        //else
        
        //    {
        //        return false;
        //    }
        
    }
    public int Update(SqlParameter[] parameters)
    {
        try
        {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_MenuUpdate", parameters);
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public int Insert(SqlParameter[] parameters)
    {
        try
        {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_MenuInsert", parameters);
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataTable GetURLs(string prefixText)
    {
        DataTable objDtURLs = new DataTable();
        SqlParameter[] objSqlParameterUrls = { new SqlParameter("@PageUrl", prefixText) };
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "ProcCheckPageURL", objDtURLs, objSqlParameterUrls);
        return objDtURLs;
    }

        public System.Data.DataTable GetMenuDetails_ByUserIdModuleId(int mid, int userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
                {   new SqlParameter( "@ModuleId"       , mid   ),
            new SqlParameter( "@LoginId"       , userid   )
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "Get_userMenuAccess", dt, parameters);
            return dt;
        }

        //public System.Data.DataTable InsertUpdateUserMenuAccess(int LoginId, int MenuId)
        //{
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    SqlParameter[] parameters =
        //        {   new SqlParameter( "@LoginId"       , LoginId   ),
        //    new SqlParameter( "@Menuid"       , MenuId   )
        //    };


        //    ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "InsertUpdateappmstr_UserMenuRelation", dt, parameters);
        //    return dt;
        //}

        public int InsertUpdateUserMenuAccess(SqlParameter[] parameters)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "InsertUpdateappmstr_UserMenuRelation", parameters);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public bool IsUserMenuAccessAlreadyExists(int uid, int menuid)
        //{
        //    //@result

        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    SqlParameter[] parameters =
        //        {   new SqlParameter( "@loginid"     , uid   ) ,
        //        new SqlParameter( "@menuid"       ,menuid) ,
        //        new SqlParameter( "@result",false)

        //    };
        //    parameters[2].Direction = ParameterDirection.Output;
        //    SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsUserMenuAccessAlreadyExists", parameters);
        //    if ((bool)parameters[2].Value == true)
        //    {
        //        return true;
        //    }



        //    return false;
        //}

        public static void deleteUserMenuRelationDetails(string _strProc, int _userid, int _moduleid)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
            oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _userid);
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
    }
}
