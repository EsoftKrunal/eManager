using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberExperienceDetailsSelectData : DataAccessBase
    {
       private ExperienceDetails _experiencedetails;

       public CrewMemberExperienceDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewExperienceDetailsinGrid.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberExperienceDetailsSelectDataParameters _ExperienceDetailsByIdParameters = new CrewMemberExperienceDetailsSelectDataParameters(ExperienceDetails);
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
    public class CrewMemberExperienceDetailsSelectDataParameters
    {
        private ExperienceDetails _experiencedetails;
        private SqlParameter[] _parameters;

        public CrewMemberExperienceDetailsSelectDataParameters(ExperienceDetails experiencedetails)
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
