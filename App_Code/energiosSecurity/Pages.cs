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
    /// Summary description for Pages
    /// </summary>
    public class Pages
    {
      
        public Pages()
        {
            //
            // TODO: Add constructor logic here
            // 
        }

        public System.Data.DataTable GetAll()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetAllPages", dt);

            return dt;
        }
        public System.Data.DataTable GetPagesByModuleID(int Modid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
            {   new SqlParameter( "@ModuleID"		, Modid   ) 
            };

            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetPagesByModuleID", dt, parameters);

            return dt;
        }
        public System.Data.DataTable GetDetails_ByMenuId(int Pageid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
            {   new SqlParameter( "@PageID"		, Pageid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetPageDetails_ByPageID", dt, parameters);
            return dt;
        }
        public System.Data.DataTable GetPageTitle(int uid,string purl)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlParameter[] parameters =
            {   new SqlParameter( "@url"		, purl   ) ,
                new SqlParameter( "@userid"		, uid   ) 
            };


            ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "usp_GetPageTitle", dt, parameters);
            return dt;
        }

        public int Update(SqlParameter[] parameters)
        {
            try
            {
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_PageUpdate", parameters);
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
                ESqlHelper.ExecuteNonQuery(ECommon.ConString, CommandType.StoredProcedure, "usp_PageInsert", parameters);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Boolean isExistPage(SqlParameter[] parameters)
        {
            try
            {
             // int cnt=  SqlHelper.ExecuteScalar(ECommon.CMSConString, CommandType.StoredProcedure, "sp_isExistPage", parameters);
                DataTable dt = new DataTable();
                ESqlHelper.FillDataTable(ECommon.ConString, CommandType.StoredProcedure, "sp_isExistPage", dt, parameters);
                if (dt.Rows.Count > 0)
                {
                    if (ECommon.CastAsInt32(dt.Rows[0][0].ToString()) > 0)
                    { return true; 
                    }
                    else 
                    { 
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}