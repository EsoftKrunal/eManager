using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberCourseDetailsSelectData:DataAccessBase 
    {
          private  CrewCourseCertificateDetails _crewCourseCertificateDetails;

        public CrewMemberCourseDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewCourseCertificateDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberCourseDetailsSelectDataParameters _CrewMemberCourseDetailsSelectDataParameters = new CrewMemberCourseDetailsSelectDataParameters(_crewCourseCertificateDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _CrewMemberCourseDetailsSelectDataParameters.Parameters);
            return ds;
        }
        public CrewCourseCertificateDetails crewCourseCertificateDetails
        {
            get { return _crewCourseCertificateDetails; }
            set { _crewCourseCertificateDetails = value; }
        }
    }
    public class CrewMemberCourseDetailsSelectDataParameters
    {
        private CrewCourseCertificateDetails _crewCourseCertificateDetails;
        private SqlParameter[] _parameters;

        public CrewMemberCourseDetailsSelectDataParameters(CrewCourseCertificateDetails crewCourseCertificateDetails)
        {
            this.crewCourseCertificateDetails = crewCourseCertificateDetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , crewCourseCertificateDetails.CrewId )
            };

            Parameters = parameters;
        }

        public CrewCourseCertificateDetails crewCourseCertificateDetails
        {
            get { return _crewCourseCertificateDetails; }
            set { _crewCourseCertificateDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
