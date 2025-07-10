using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberExperienceDetailsSelectDataById : DataAccessBase
    {
       private ExperienceDetails _experiencedetails;
       public CrewMemberExperienceDetailsSelectDataById()
        {
            StoredProcedureName = StoredProcedure.Name.SelectExperienceDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberExperienceDetailsSelectByIdDataParameters experienceDetailsSelectDataParameters = new CrewMemberExperienceDetailsSelectByIdDataParameters(ExperienceDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, experienceDetailsSelectDataParameters.Parameters);
            return ds;
        }

       public ExperienceDetails ExperienceDetails
       {
           get { return _experiencedetails; }
           set { _experiencedetails = value; }
       }
    }

    public class CrewMemberExperienceDetailsSelectByIdDataParameters
    {
        private ExperienceDetails _experiencedetails;
        private SqlParameter[] _parameters;

        public CrewMemberExperienceDetailsSelectByIdDataParameters(ExperienceDetails experiencedetails)
        {
            ExperienceDetails = experiencedetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewExperienceId" , ExperienceDetails.ExperienceId )
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
