using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class VisaSelectData: DataAccessBase
    {
       private CrewFamilyDocumentDetails _crewfamilydocumentdetails;
       public VisaSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectVisaDetails.ToString();
        }

        public DataSet Get()
        {
            
            DataSet ds;
            VisaDetailsParameters _visaDetailsParameters = new VisaDetailsParameters(CrewFamilyDocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _visaDetailsParameters.Parameters);
            return ds;
        }
       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
       {
           get { return _crewfamilydocumentdetails; }
           set { _crewfamilydocumentdetails = value; }
       }
    }
    public class VisaDetailsParameters
    {
        private CrewFamilyDocumentDetails _crewfamilydocumentdetails;
        private SqlParameter[] _parameters;

        public VisaDetailsParameters(CrewFamilyDocumentDetails crewfamilydocumentdetails)
        {
            CrewFamilyDocumentDetails = crewfamilydocumentdetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewFamilyId" , CrewFamilyDocumentDetails.CrewFamilyId )
            };

            Parameters = parameters;
        }

        public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewfamilydocumentdetails; }
            set { _crewfamilydocumentdetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
