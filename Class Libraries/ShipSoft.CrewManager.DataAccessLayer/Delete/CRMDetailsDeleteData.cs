using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class CRMDetailsDeleteData : DataAccessBase
    {
       private CRMDetails _CRMDetails;
       private CRMDetailsDeleteDataParameters _CRMDetailsDeleteDataParameters;

       public CRMDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCRMDetailsBycrewcrmId.ToString();
        }
        public void Delete()
        {
            _CRMDetailsDeleteDataParameters = new CRMDetailsDeleteDataParameters(CRMDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _CRMDetailsDeleteDataParameters.Parameters;
            dbhelper.Run();
        }
       public CRMDetails CRMDetails
        {
            get { return _CRMDetails; }
            set { _CRMDetails = value; }
        }
    }
    public class CRMDetailsDeleteDataParameters
    {
        private CRMDetails _CRMDetails;
        private SqlParameter[] _parameters;

        public CRMDetailsDeleteDataParameters(CRMDetails exp)
        {
           CRMDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewCRMId"  , CRMDetails.CrewCRMId)
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
