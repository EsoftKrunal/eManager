using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberPersonalDetailsSelectDataById : DataAccessBase
    {
        private CrewMember _crewmember;

        public CrewMemberPersonalDetailsSelectDataById()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewMemberDetailsById.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;

            CrewMemberPersonaletailsSelectByIDDataParameters _crewmemberselectbyiddataparameters = new CrewMemberPersonaletailsSelectByIDDataParameters(CrewMember);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewmemberselectbyiddataparameters.Parameters);

            return ds;
        }

        public CrewMember CrewMember
        {
            get { return _crewmember; }
            set { _crewmember = value; }
        }
    }

    public class CrewMemberPersonaletailsSelectByIDDataParameters
    {
        private CrewMember _crewmember;
        private SqlParameter[] _parameters;

        public CrewMemberPersonaletailsSelectByIDDataParameters(CrewMember crewmember)
        {
            CrewMember = crewmember;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@Id" , CrewMember.Id )
            };

            Parameters = parameters;
        }

        public CrewMember CrewMember
        {
            get { return _crewmember; }
            set { _crewmember = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
