using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;
using System.Data.SqlClient ;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewCourseCertificateDetailsDeleteData:DataAccessBase 
    {
         private  CrewCourseCertificateDetails   _crewcertificatedetails;
        private CrewCourseCertificateDetailsDeleteDataParameters _crewcoursedeletedataparameters;

        public CrewCourseCertificateDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewCourseCeretificateDetailsById.ToString();
        }
        public void Delete()
        {
            _crewcoursedeletedataparameters = new CrewCourseCertificateDetailsDeleteDataParameters(CertificateDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewcoursedeletedataparameters.Parameters;
            dbhelper.Run();
        }
        public CrewCourseCertificateDetails CertificateDetails
        {
            get { return _crewcertificatedetails; }
            set { _crewcertificatedetails = value; }
        }
    }
     public class CrewCourseCertificateDetailsDeleteDataParameters
    {
         private CrewCourseCertificateDetails _crewCourseCertificateDetails;
        private SqlParameter[] _parameters;

         public CrewCourseCertificateDetailsDeleteDataParameters(CrewCourseCertificateDetails exp)
        {
            CertificateDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CourseCerficiateId"  , CertificateDetails.CourseCertificateId)
		    };

            Parameters = parameters;
        }

         public CrewCourseCertificateDetails CertificateDetails
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
