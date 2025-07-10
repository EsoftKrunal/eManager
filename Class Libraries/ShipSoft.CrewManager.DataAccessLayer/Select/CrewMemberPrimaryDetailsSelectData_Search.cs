using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberPrimaryDetailsSelectData_Search : DataAccessBase
    {
        private BoCrewSearch _crewmember;

        public CrewMemberPrimaryDetailsSelectData_Search()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewMemberDetails_Search.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;

            CrewMemberSelect_Search_DataParameters _crewmemberselect_search_dataparameters = new CrewMemberSelect_Search_DataParameters(CrewSearch);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewmemberselect_search_dataparameters.Parameters);

            return ds;
        }

        public BoCrewSearch CrewSearch
        {
            get { return _crewmember; }
            set { _crewmember = value; }
        }
    }

    public class CrewMemberSelect_Search_DataParameters
    {
        private BoCrewSearch _crewmember;
        private SqlParameter[] _parameters;

        public CrewMemberSelect_Search_DataParameters(BoCrewSearch crewmember)
        {
            CrewSearch = crewmember;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@LoginId" , CrewSearch.LoginId ),
                new SqlParameter( "@CrewNumber" , CrewSearch.CrewNumber ),
                new SqlParameter( "@FirstName" , CrewSearch.FirstName ),
              // new SqlParameter( "@LastName" , CrewSearch.LastName  ),
                new SqlParameter( "@Nationality" , CrewSearch.Nationality),
                new SqlParameter( "@CrewStatusId" , CrewSearch.CrewStatusId ),
                new SqlParameter( "@Rank" , CrewSearch.Rank ),
                new SqlParameter( "@PassportNo" , CrewSearch.PassportNo), 
                new SqlParameter( "@RecOfficeId" , CrewSearch.RecOffId ),
                new SqlParameter( "@AgeFrom" , CrewSearch.AgeFrom), 
                new SqlParameter( "@AgeTo" , CrewSearch.AgeTo ),
                new SqlParameter( "@ExpFrom" , CrewSearch.ExpFrom ),
                new SqlParameter( "@ExpTo" , CrewSearch.ExpTo),

                new SqlParameter( "@FromDate" , CrewSearch.FromDate),
                new SqlParameter( "@ToDate" , CrewSearch.ToDate),
                new SqlParameter( "@VesselId" , CrewSearch.VesselId),
                new SqlParameter( "@Owner" , CrewSearch.Owner),
                new SqlParameter( "@VesselType" , CrewSearch.VesselType),
                new SqlParameter( "@USvisa" , CrewSearch.USvisa),
                new SqlParameter( "@SchengenVisa" , CrewSearch.SchengenVisa),
                new SqlParameter( "@FamilyMember" , CrewSearch.FamilyMember),
                new SqlParameter( "@ReliefDueDate" , CrewSearch.ReliefDue)
            };

            Parameters = parameters;
        }

        public BoCrewSearch CrewSearch
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
