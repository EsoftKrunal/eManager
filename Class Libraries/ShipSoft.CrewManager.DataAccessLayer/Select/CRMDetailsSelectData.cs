using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CRMDetailsSelectData : DataAccessBase
    {
       private CRMDetails _CRMDetails;

       public CRMDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCRMDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CRMDetailsSelectDataParameters _cRMDetailsSelectDataParameters = new CRMDetailsSelectDataParameters(CRMDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _cRMDetailsSelectDataParameters.Parameters);
            return ds;
        }
       public CRMDetails CRMDetails
       {
           get { return _CRMDetails; }
           set { _CRMDetails = value; }
       }
    }
    public class CRMDetailsSelectDataParameters
    {
        private CRMDetails _CRMDetails;
        private SqlParameter[] _parameters;

        public CRMDetailsSelectDataParameters(CRMDetails crmDetails)
        {
            CRMDetails = crmDetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewCRMId" , CRMDetails.CrewCRMId),
                new SqlParameter( "@CrewId" , CRMDetails.CrewId )
            };

            Parameters = parameters;
        }

        public CRMDetails CRMDetails
        {
            get { return _CRMDetails; }
            set { _CRMDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
