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
    public class Module
    {

     
        public Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool IsUserActive(int uid)
        {

            return true;
        }

        public System.Data.DataTable GetModuleDetails_ByModuleId(int mid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
            {    
                new SqlParameter( "@mid"		,mid)  
                
            };
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetModuleDetails_ByModuleID", dt, parameters);
            return dt;
        }
        public System.Data.DataTable GetAllModules()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetAllModules", dt);
            return dt;
        }
        public bool IsModuleAlreadyExists(string mname, int mid, string shortname)
        {
            //@result

            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
            {   new SqlParameter( "@mname"		, mname   ) ,
                new SqlParameter( "@mid"		,mid) ,
                new SqlParameter( "@shortname"		,shortname) ,
                new SqlParameter( "@result",false)
                
            };
            parameters[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_IsModuleAlreadyExists", parameters);
            if ((bool)parameters[3].Value == true)
            {
                return true;
            }



            return false;
        }
        public int Update(SqlParameter[] parameters)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_ModuleUpdate", parameters);
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
                SqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_ModuleInsert", parameters);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
