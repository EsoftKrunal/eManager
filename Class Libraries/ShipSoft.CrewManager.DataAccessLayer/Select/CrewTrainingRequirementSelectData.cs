using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewTrainingRequirementSelectData:DataAccessBase 
    {
             private CrewTrainingRequirement _crewTrainingRequirement;

        public CrewTrainingRequirementSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewTrainingRequirementdetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewTrainingRequirementSelectDataParameters _crewTrainingRequirementSelectDataParameters = new CrewTrainingRequirementSelectDataParameters(TrainingRequirement);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewTrainingRequirementSelectDataParameters.Parameters);
            return ds;
        }
       public CrewTrainingRequirement TrainingRequirement
       {
           get { return _crewTrainingRequirement; }
           set { _crewTrainingRequirement = value; }
       }
    }
    public class CrewTrainingRequirementSelectDataParameters
    {
      private CrewTrainingRequirement _crewTrainingRequirement;
        private SqlParameter[] _parameters;

        public CrewTrainingRequirementSelectDataParameters(CrewTrainingRequirement ctr)
        {
            this.TrainingRequirement = ctr;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" ,TrainingRequirement.CrewId )
            };

            Parameters = parameters;
        }

        public CrewTrainingRequirement TrainingRequirement
       {
           get { return _crewTrainingRequirement; }
           set { _crewTrainingRequirement = value; }
       }
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
    }

