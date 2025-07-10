using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberContactDetailsSelectDataById : DataAccessBase
    {
        private CrewContact _crewContact;

        public CrewMemberContactDetailsSelectDataById()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewMemberContactDetailsById.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;

            CrewMemberContactDetailsSelectByIDDataParameters _crewmemberselectbyiddataparameters = new CrewMemberContactDetailsSelectByIDDataParameters(_crewContact);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewmemberselectbyiddataparameters.Parameters);

            return ds;
        }

        public CrewContact CrewMember
        {
            get { return _crewContact; }
            set { _crewContact = value; }
        }
    }

    public class CrewMemberContactDetailsSelectByIDDataParameters
    {
        private  CrewContact _crewContact;
        private SqlParameter[] _parameters;

        public CrewMemberContactDetailsSelectByIDDataParameters(CrewContact crewmember)
        {
            ContactDetails = crewmember;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@Id" , _crewContact.CrewId)
            };

            Parameters = parameters;
        }

        public  CrewContact ContactDetails
        {
            get { return _crewContact; }
            set { _crewContact = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
