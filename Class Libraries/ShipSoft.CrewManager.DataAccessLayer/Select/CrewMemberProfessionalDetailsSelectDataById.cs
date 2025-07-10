using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberProfessionalDetailsSelectDataById : DataAccessBase
    {
        private ProfessionalDetails _professionaldetails;
        public CrewMemberProfessionalDetailsSelectDataById()
        {
            StoredProcedureName = StoredProcedure.Name.SelectProfessionalDetailsdataById.ToString();
        }
        public DataSet Get()
        {
            DataSet ds;
            CrewMemberProfessionalDetailsSelectDataByIdParameters professionalDetailsSelectDataParameters = new CrewMemberProfessionalDetailsSelectDataByIdParameters(ProfessionalDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, professionalDetailsSelectDataParameters.Parameters);
            return ds;
        }
        public ProfessionalDetails ProfessionalDetails
        {
            get { return _professionaldetails; }
            set { _professionaldetails = value; }
        }
        public class CrewMemberProfessionalDetailsSelectDataByIdParameters
        {
            private ProfessionalDetails _professionaldetails;
            private SqlParameter[] _parameters;
            public CrewMemberProfessionalDetailsSelectDataByIdParameters(ProfessionalDetails param)
            {
                ProfessionalDetails = param;
                Build();
            }
            private void Build()
            {
                SqlParameter[] parameters =
            {
                new SqlParameter( "@CertificateId" , ProfessionalDetails.CertificateId)
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
}
