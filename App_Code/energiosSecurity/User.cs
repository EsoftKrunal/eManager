using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
namespace energiosSecurity
{

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    
	public User()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// Checks if user with given password exists in the database
    /// </summary>
    /// <param name="Uname">User name</param>
    /// <param name="Upassword">User password</param>
    /// <returns>UserID if user exist and password is correct</returns>
    public int IsUserExists(string Uname,string Upassword){
        int UserId;
         System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@Uname"		, Uname   ) ,
                new SqlParameter( "@upwd"		, Upassword   ) 
            };

        

        UserId = Convert.ToInt32(ESqlHelper.ExecuteScalar(ECommon.ConString, CommandType.StoredProcedure, "usp_IsUserExists", parameters));
        return UserId;
    }


    public bool IsUserAlreadyExists(string uname, int uid)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@Uname"		, uname   ) ,
                new SqlParameter( "@uid"		,uid) ,
                new SqlParameter( "@result",false)
                
            };
        parameters[2].Direction = ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsUserAlreadyExists", parameters);
        if ((bool)parameters[2].Value == true)
        {
            return true;
        }
        else
        { 
            return false;
        }


    }

    public bool IsEmailAlreadyExists(string Email, int uid)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@uEmail"		, Email   ) ,
                new SqlParameter( "@uid"		,uid) ,
                new SqlParameter( "@result",false)
                
            };
        parameters[2].Direction = ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsEmailAlreadyExists", parameters);
        if ((bool)parameters[2].Value == true)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public int UpdatePassword(int uid,string oldpwd,string newpwd)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@Userid"		, uid   ) ,
                new SqlParameter( "@OldPassword"		,oldpwd) ,
                new SqlParameter( "@Password"		,newpwd) ,
                new SqlParameter( "@ReturnValue",0)
                
            };
        parameters[3].Direction = ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_UpdatePassword", parameters);
        return Convert.ToInt32(parameters[3].Value);
        


    }
    public bool IsSuperUser( int uid)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   
                new SqlParameter( "@uid"		,uid) ,
                new SqlParameter( "@result",false)
                
            };
        parameters[1].Direction = ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsUserSuperAdmin", parameters);
        if ((bool)parameters[1].Value == true)
        {
            return true;
        }

        return false;
    }
    public System.Data.DataTable GetAllUser()
    {
        System.Data.DataTable dt = new System.Data.DataTable();
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetAllUsers", dt);
        return dt;
    }
    public System.Data.DataTable GetUserDetails_ByUserId(int uid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@uid",uid) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetUserDetails_ByUserID", dt, parameters);
       
        return dt;
    }
    public System.Data.DataTable GetUserDetails_ByUserName(string uName)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@Uname"		, uName   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetUserDetails_ByUserName", dt, parameters);
        return dt;
    }
    public System.Data.DataTable GetUserModules(string mName)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        return dt;
    }
    public System.Data.DataTable GetUserRoles(int uID)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        return dt;
    }
    public DataTable GetUserPageRights(int userid, int menuid)
    {

        System.Data.DataTable dt = new System.Data.DataTable();
         SqlParameter[] parameters =
            {   new SqlParameter( "@userid"		, userid   ) ,
                new SqlParameter( "@menuID"		,menuid)  
               
                
            };
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetActionRights",dt, parameters);

        return dt;
        
        
    }
    public DataTable GetPageRightsByUserID(int userid, string pageurl)
    {

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@userid", userid),
                new SqlParameter( "@url",pageurl)  
            };
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetPageRights", dt, parameters);

        return dt;


    }

    public int Insert(SqlParameter[] parameters)
    {

            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_UserInsert", parameters);
        return 1;
    }
    public int Update(SqlParameter[] parameters)
    {
        try
        {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_UserUpdate", parameters);
        return 1;
        }
        catch(Exception ex)
        {
            throw ex;
        }
      
    }
}
}
