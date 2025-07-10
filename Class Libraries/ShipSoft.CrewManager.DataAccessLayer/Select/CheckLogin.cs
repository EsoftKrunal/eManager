using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CheckLogin : DataAccessBase
    {
        private String userId;
        private String password;
        public CheckLogin()
        {
            StoredProcedureName = StoredProcedure.Name.CheckLogin.ToString();
        }
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public int LoginId()
        {
            DataSet ds;
            UserLoginParameteres loginParameters = new UserLoginParameteres(UserId,Password );
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, loginParameters.Parameters);
            return Convert.ToInt32(id.ToString());
        }
    }
    public class UserLoginParameteres
    {
        private String userId;
        private String password;
        private SqlParameter[] _parameters;
        public UserLoginParameteres(string UserId1, string Password1)
        {
            userId = UserId1;
            password = Password1; 
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {   new SqlParameter( "@UserName"		, userId   ) ,
                new SqlParameter( "@Password"		, password ) 
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
