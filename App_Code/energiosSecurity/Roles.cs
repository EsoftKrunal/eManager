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
    public class Role
{
       
    public Role()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public System.Data.DataTable GetAll()
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetAllRoles", dt);
         
        return dt;
    }
 
   
    public bool IsAlreadyExists(string rName,int rid)
    {
        //@result
        
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@rName", rName   ) ,
                new SqlParameter( "@rid",rid) ,
                new SqlParameter( "@result",false)
                
            };
       parameters[2].Direction=ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsRoleAlreadyExists", parameters);
       if ((bool)parameters[2].Value==true){
           return true;
       }
        return false;
    }
   
    public System.Data.DataTable GetDetails_ById(int rid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@rid"		, rid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetRoleDetails_ByRoleId", dt, parameters);
        return dt;
    }
   
    
    public int Insert(SqlParameter[] parameters)
    {

            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_RoleInsert", parameters);
        return 1;
    }
    public int Update(SqlParameter[] parameters)
    {
        try
        {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_RoleUpdate", parameters);
        return 1;
        }
        catch(Exception ex)
        {
            throw ex;
        }
      
    }

    public bool UserAssigned(int rid)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@Roleid",rid) ,
                new SqlParameter( "@result",false)
            };
        parameters[1].Direction = ParameterDirection.Output;
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "Proc_RoleAssignedToUser", parameters);
        if ((bool)parameters[1].Value == true)
        {
            return true;
        }
        return false;
    }
}
}
