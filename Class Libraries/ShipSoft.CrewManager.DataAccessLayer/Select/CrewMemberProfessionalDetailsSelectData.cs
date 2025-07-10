using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberProfessionalDetailsSelectData : DataAccessBase
    {
       private ProfessionalDetails _professionaldetails;

       public CrewMemberProfessionalDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewProfessionalDetailsinGrid.ToString();
        }
       public DataSet Get()
        {
            DataSet ds;
            CrewMemberProfessionalDetailsSelectDataParameters _ProfessionalDetailsByIdParameters = new CrewMemberProfessionalDetailsSelectDataParameters(ProfessionalDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _ProfessionalDetailsByIdParameters.Parameters);
            return ds;
        }
       public ProfessionalDetails ProfessionalDetails
       {
           get { return _professionaldetails; }
           set { _professionaldetails = value; }
       }
    }
    public class CrewMemberProfessionalDetailsSelectDataParameters
    {
        private ProfessionalDetails _professionaldetails;
        private SqlParameter[] _parameters;

        public CrewMemberProfessionalDetailsSelectDataParameters(ProfessionalDetails param)
        {
            ProfessionalDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , ProfessionalDetails.CrewId )
            };

            Parameters = parameters;
        }

        public ProfessionalDetails ProfessionalDetails
        {
            get { return _professionaldetails; }
            set { _professionaldetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
