using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewLicenseSelectData:DataAccessBase 
    {
       private CrewLicenseDetails _crewlicensedetails;

       public CrewLicenseSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewLicenses.ToString();
        }
       

        public DataSet Get()
        {
            DataSet ds;
            CrewLicenseSelectDataParameters _crewMemberCouseSelctDataParameters = new CrewLicenseSelectDataParameters(LicenseDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewMemberCouseSelctDataParameters.Parameters);
            return ds;
        }

       public CrewLicenseDetails LicenseDetails
        {
            get { return _crewlicensedetails; }
            set { _crewlicensedetails = value; }
        }
    }
    public class CrewLicenseSelectDataParameters
    {
        private CrewLicenseDetails  _crewlicensedetails;
        private SqlParameter[] _parameters;

        public CrewLicenseSelectDataParameters(CrewLicenseDetails license)
        {
            this.LicenseDetails = license;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , this.LicenseDetails.CrewId )
            };

            Parameters = parameters;
        }

        public CrewLicenseDetails LicenseDetails
        {
            get { return _crewlicensedetails; }
            set { _crewlicensedetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
