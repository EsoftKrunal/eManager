using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewMemberCouseSelctData:DataAccessBase 
    {
        private CrewCourseCertificateDetails _crewCourseCertificateDetails;

        public CrewMemberCouseSelctData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewMemberCourses.ToString();
        }
       

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberCouseSelctDataParameters _crewMemberCouseSelctDataParameters = new CrewMemberCouseSelctDataParameters(crewCourseCertificateDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewMemberCouseSelctDataParameters.Parameters);
            return ds;
        }

        public CrewCourseCertificateDetails crewCourseCertificateDetails
        {
            get { return _crewCourseCertificateDetails; }
            set { _crewCourseCertificateDetails = value; }
        }
    }
    public class CrewMemberCouseSelctDataParameters
    {
        private CrewCourseCertificateDetails _crewCourseCertificateDetails;
        private SqlParameter[] _parameters;

        public CrewMemberCouseSelctDataParameters(CrewCourseCertificateDetails crewCourseCertificateDetails)
        {
            this.crewCourseCertificateDetails = crewCourseCertificateDetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , this.crewCourseCertificateDetails.CrewId )
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
