using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberExperienceDetailsSelectData_MTMSM : DataAccessBase
    {
       private ExperienceDetails _experiencedetails;

       public CrewMemberExperienceDetailsSelectData_MTMSM()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewExperienceDetailsinGrid_MTMSM.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberExperienceDetailsSelectData_MTMSMParameters _ExperienceDetailsByIdParameters = new CrewMemberExperienceDetailsSelectData_MTMSMParameters(ExperienceDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _ExperienceDetailsByIdParameters.Parameters);
            return ds;
        }
       public ExperienceDetails ExperienceDetails
       {
           get { return _experiencedetails; }
           set { _experiencedetails = value; }
       }
    }
    public class CrewMemberExperienceDetailsSelectData_MTMSMParameters
    {
        private ExperienceDetails _experiencedetails;
        private SqlParameter[] _parameters;

        public CrewMemberExperienceDetailsSelectData_MTMSMParameters(ExperienceDetails experiencedetails)
        {
            ExperienceDetails = experiencedetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , ExperienceDetails.CrewId )
            };

            Parameters = parameters;
        }

        public ExperienceDetails ExperienceDetails
        {
            get { return _experiencedetails; }
            set { _experiencedetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
