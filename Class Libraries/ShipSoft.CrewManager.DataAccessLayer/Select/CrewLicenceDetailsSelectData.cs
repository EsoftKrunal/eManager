using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
    public class CrewLicenceDetailsSelectData: DataAccessBase 
    {
          private CrewLicenseDetails  _crewlicensedetails;

        public CrewLicenceDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewLicenseDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewLicenceDetailsSelectDataParameters _crewLicenceDetailsSelectDataParameters = new CrewLicenceDetailsSelectDataParameters(crewlicensedetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewLicenceDetailsSelectDataParameters.Parameters);
            return ds;
        }
        public CrewLicenseDetails crewlicensedetails
        {
            get { return _crewlicensedetails; }
            set { _crewlicensedetails = value; }
        }
    }
    public class CrewLicenceDetailsSelectDataParameters
    {
        private CrewLicenseDetails _crewlicensedetails;
        private SqlParameter[] _parameters;

        public CrewLicenceDetailsSelectDataParameters(CrewLicenseDetails lice)
        {
            this.crewlicensedetails = lice;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" ,crewlicensedetails.CrewId )
            };

            Parameters = parameters;
        }

        public CrewLicenseDetails crewlicensedetails
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
