using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class CrewLicenseDetailsDeleteData:DataAccessBase 
    {
         private CrewLicenseDetails  _crewlicensedetails;
       private CrewLicenseDetailsDeleteDataParameters _crewlicensedetailsdeletedataparameters;

       public CrewLicenseDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewLicenseDetailsById.ToString();
        }
        public void Delete()
        {
            _crewlicensedetailsdeletedataparameters = new CrewLicenseDetailsDeleteDataParameters(LicenseDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewlicensedetailsdeletedataparameters.Parameters;
            dbhelper.Run();
        }
       public CrewLicenseDetails LicenseDetails
        {
            get { return _crewlicensedetails; }
            set { _crewlicensedetails = value; }
        }
    }
     public class CrewLicenseDetailsDeleteDataParameters
    {
         private CrewLicenseDetails _crewLicenseDetails;
        private SqlParameter[] _parameters;

         public CrewLicenseDetailsDeleteDataParameters(CrewLicenseDetails exp)
        {
            this.LicenseDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewLicenseId"  , LicenseDetails.CrewLicenseId)
		    };

            Parameters = parameters;
        }

         public CrewLicenseDetails LicenseDetails
         {
             get { return _crewLicenseDetails; }
             set { _crewLicenseDetails = value; }
         }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
