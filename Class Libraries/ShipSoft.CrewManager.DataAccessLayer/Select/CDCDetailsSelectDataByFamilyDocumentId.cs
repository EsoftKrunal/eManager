using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CDCDetailsSelectDataByFamilyDocumentId : DataAccessBase
    {
       private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;

       public CDCDetailsSelectDataByFamilyDocumentId()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCDCDetailsByFamilyDocumentId.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;

            CDCDetailsSelectByFamilyDocumentIdParameters _cdcDetailsSelectByFamilyDocumentIdParameters = new CDCDetailsSelectByFamilyDocumentIdParameters(CrewFamilyDocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _cdcDetailsSelectByFamilyDocumentIdParameters.Parameters);
            return ds;
        }
       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
       {
           get { return _crewFamilyDocumentDetails; }
           set { _crewFamilyDocumentDetails = value; }
       }
    }
    public class CDCDetailsSelectByFamilyDocumentIdParameters
    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        private SqlParameter[] _parameters;

        public CDCDetailsSelectByFamilyDocumentIdParameters(CrewFamilyDocumentDetails crewfamilydocumentdetails)
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
