

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
    /// Summary description for RoleRights
/// </summary>
    public class RoleRights
{
 
        public RoleRights()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public System.Data.DataTable GetRoleRights(int roleid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@roleid"		, roleid   ) 
              
            };
        ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetRoleRights_ByRoleID", dt,parameters);
        return dt;
    }
   
    
    public System.Data.DataTable GetDetails_ById(int rrid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@rrid"		, rrid   ) 
            };


        ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetRoleRightsDetails_ByRoleRightsId", dt, parameters);
        return dt;
    }
    public bool IsAlreadyExists(int roleid, int menuid)
    {
        //@result

        System.Data.DataTable dt = new System.Data.DataTable();
        SqlParameter[] parameters =
            {   new SqlParameter( "@roleid"		, roleid   ) ,
                new SqlParameter( "@pgid"		,menuid) ,
                new SqlParameter( "@result",false)
                
            };
        parameters[2].Direction = ParameterDirection.Output;
        ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsRoleRightsAlreadyExists", parameters);
        if ((bool)parameters[2].Value == true)
        {
            return true;
        }



        return false;
    }

    public int Insert(SqlParameter[] parameters)
    {

        try
        {
           
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_RoleRightsInsert", parameters);
            return 1;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int Update(SqlParameter[] parameters)
    {
        try
        {
            ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_RoleRightsUpdate", parameters);
        return 1;
        }
        catch(Exception ex)
        {
            throw ex;
        }
      
    }
}
}
