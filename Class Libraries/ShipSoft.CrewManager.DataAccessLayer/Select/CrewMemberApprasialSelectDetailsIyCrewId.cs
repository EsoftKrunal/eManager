using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberApprasialSelectDetailsIyCrewId:DataAccessBase 
    {
         private CrewApprasialDetails  _crewAprasialDetails;

        public CrewMemberApprasialSelectDetailsIyCrewId()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewApprasials1.ToString();
        }
       

        public DataSet Get()
        {
            DataSet ds;
            CrewApprasialSelectDataParameters _crewMemberAppraisalDataParameters = new CrewApprasialSelectDataParameters(ApprasialDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewMemberAppraisalDataParameters.Parameters);
            return ds;
        }

       public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }
    }
    public class CrewApprasialSelectDataParameters
    {
         private CrewApprasialDetails  _crewAprasialDetails;
        private SqlParameter[] _parameters;

        public CrewApprasialSelectDataParameters(CrewApprasialDetails appr)
        {
            this.ApprasialDetails = appr;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , this.ApprasialDetails.CrewId ),
                new SqlParameter("@CrewApprasialId",this.ApprasialDetails.CrewAppraisalId)
            };

            Parameters = parameters;
        }

        public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
