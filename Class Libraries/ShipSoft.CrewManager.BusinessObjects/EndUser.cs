using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class EndUser
    {
        private int _enduserid;
        private int _endusertypeid;
        private string _firstname;
        private string _lastname;
        private string _password;

        public EndUser()
        {

        }

        public int EndUserID
        {
            get { return _enduserid; }
            set { _enduserid = value; }
        }

        public int EndUserTypeID
        {
            get { return _endusertypeid; }
            set { _endusertypeid = value; }
        }

        public string FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
