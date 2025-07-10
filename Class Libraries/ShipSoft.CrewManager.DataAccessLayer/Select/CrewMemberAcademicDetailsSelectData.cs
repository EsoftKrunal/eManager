using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberAcademicDetailsSelectData : DataAccessBase
    {
       private AcademicDetails _academicdetails;

       public CrewMemberAcademicDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.selectCrewAcademicDetails.ToString();
        }
       public DataSet Get()
        {
            DataSet ds;
            CrewMemberAcademicDetailsSelectDataParameters _AcademicDetailsByIdParameters = new CrewMemberAcademicDetailsSelectDataParameters(AcademicDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _AcademicDetailsByIdParameters.Parameters);
            return ds;
        }
       public AcademicDetails AcademicDetails
       {
           get { return _academicdetails; }
           set { _academicdetails = value; }
       }
    }
    public class CrewMemberAcademicDetailsSelectDataParameters
    {
        private AcademicDetails _academicdetails;
        private SqlParameter[] _parameters;

        public CrewMemberAcademicDetailsSelectDataParameters(AcademicDetails param)
        {
            AcademicDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@AcademicDetailsId" , AcademicDetails.AcademicDetailsId),
                new SqlParameter( "@CrewId" , AcademicDetails.CrewId)
            };

            Parameters = parameters;
        }

        public AcademicDetails AcademicDetails
        {
            get { return _academicdetails; }
            set { _academicdetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
