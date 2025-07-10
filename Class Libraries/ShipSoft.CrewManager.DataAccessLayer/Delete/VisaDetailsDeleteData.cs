using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;
namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class VisaDetailsDeleteData : DataAccessBase
    {
       private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
       private VisaDetailsDeleteDataParameters _visaDetailsDeleteDataParameters;

       public VisaDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteVisaDetailsByDocumentId.ToString();
        }
        public void Delete()
        {
            _visaDetailsDeleteDataParameters = new VisaDetailsDeleteDataParameters(CrewFamilyDocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _visaDetailsDeleteDataParameters.Parameters;
            dbhelper.Run();
        }
       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }
    }
    public class VisaDetailsDeleteDataParameters
    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private SqlParameter[] _parameters;

        public VisaDetailsDeleteDataParameters(CrewFamilyDocumentDetails exp)
        {
            CrewFamilyDocumentDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewFamilyDocumentId"  , CrewFamilyDocumentDetails.Crewfamilydocumentid)
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
