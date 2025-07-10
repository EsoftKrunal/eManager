using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class VisaDetailsSelectDataByFamilyDocumentId : DataAccessBase
    {
       private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;

       public VisaDetailsSelectDataByFamilyDocumentId()
        {
            StoredProcedureName = StoredProcedure.Name.SelectVisaDetailsByFamilyDocumentId.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            VisaDetailsSelectByFamilyDocumentIdParameters _visaDetailsSelectByFamilyDocumentIdParameters = new VisaDetailsSelectByFamilyDocumentIdParameters(CrewFamilyDocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _visaDetailsSelectByFamilyDocumentIdParameters.Parameters);
            return ds;
        }
       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
       {
           get { return _crewFamilyDocumentDetails; }
           set { _crewFamilyDocumentDetails = value; }
       }
    }
    public class VisaDetailsSelectByFamilyDocumentIdParameters
    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private SqlParameter[] _parameters;

        public VisaDetailsSelectByFamilyDocumentIdParameters(CrewFamilyDocumentDetails crewfamilydocumentdetails)
        {
            CrewFamilyDocumentDetails = crewfamilydocumentdetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewFamilyDocumentId" , CrewFamilyDocumentDetails.Crewfamilydocumentid )
            };

            Parameters = parameters;
        }

        public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
