using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CheckAuthority : DataAccessBase
    {
        private int _userId;
        private int _ApplicationModuleId;
        public CheckAuthority()
        {
            StoredProcedureName = StoredProcedure.Name.CheckAuthority.ToString();
        }
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public int ApplicationModuleId
        {
            get { return _ApplicationModuleId; }
            set { _ApplicationModuleId = value; }
        }
        public DataSet Get()
        {
            DataSet ds;
            CheckAuthorityParameteres param = new CheckAuthorityParameteres(UserId,ApplicationModuleId);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, param.Parameters);
            return ds;
        }
    }
    public class CheckAuthorityParameteres
    {
        private int userId;
        private int ApplicationModuleId;
        private SqlParameter[] _parameters;
        public CheckAuthorityParameteres(int _UserId, int _ApplicationModuleId)
        {
            userId = _UserId;
            ApplicationModuleId = _ApplicationModuleId; 
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {   new SqlParameter( "@UserId"		, userId   ) ,
                new SqlParameter( "@ApplicationModuleId"		, ApplicationModuleId ) 
            };

            Parameters = parameters;
        }
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
