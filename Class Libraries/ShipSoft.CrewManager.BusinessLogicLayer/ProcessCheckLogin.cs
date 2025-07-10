using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCheckLogin : IBusinessLogic
    {   
        private int loginId;
        private string userid;
        private string password;
        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public int  LoginId
        {
            get { return loginId; }
        }
        public ProcessCheckLogin()
        {

        }
        public void Invoke()
        {
            CheckLogin Login = new CheckLogin();
            Login.UserId = userid;
            Login.Password = password;
            loginId=Login.LoginId();
        }
    }
}
