

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
    public class UserModule
{
     
        public UserModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public System.Data.DataTable GetUserModules(int uid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@userid"		, uid   ) 
              
            };
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetUserModules_ByUserID", dt, parameters);
        return dt;
    }
   
    public bool IsAlreadyExists(int roleid,int uid)
    {
        //@result
        
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@userid"		, uid   ) ,
                new SqlParameter( "@moduleid"		,roleid) ,
                new SqlParameter( "@result",false)
                
            };
       parameters[2].Direction=ParameterDirection.Output;
       SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsUserModuleAlreadyExists", parameters);
       if ((bool)parameters[2].Value==true){
           return true;
       }


        
        return false;
    }
   
    
    
    public int Insert(SqlParameter[] parameters)
    {

        SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_UserModuleInsert", parameters);
        return 1;
    }
    public int Update(SqlParameter[] parameters)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_UserModuleUpdate", parameters);
        return 1;
        }
        catch(Exception ex)
        {
            throw ex;
        }
      
    }
}
}
