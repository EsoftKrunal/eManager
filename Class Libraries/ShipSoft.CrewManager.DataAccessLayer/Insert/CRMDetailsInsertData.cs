using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CRMDetailsInsertData : DataAccessBase
    {
       private CRMDetails _CRMDetails;
       private CRMDetailsInsertDataParameters _cRMDetailsInsertDataParameters;

       public CRMDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewMemberCRMDetails.ToString();
        }
        public void Add()
        {
            _cRMDetailsInsertDataParameters = new CRMDetailsInsertDataParameters(CRMDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, _cRMDetailsInsertDataParameters.Parameters);
            //this.CRMDetails.CrewCRMId = Convert.ToInt32(id.ToString());
        }
       public CRMDetails CRMDetails
        {
            get { return _CRMDetails; }
            set { _CRMDetails = value; }
        }
    }
    public class CRMDetailsInsertDataParameters
    {
        private CRMDetails _CRMDetails;
        private SqlParameter[] _parameters;

        public CRMDetailsInsertDataParameters(CRMDetails crmDetails)
        {
            CRMDetails = crmDetails;
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewCRMId"		,_CRMDetails.CrewCRMId) ,
                new SqlParameter( "@CrewId"		, _CRMDetails.CrewId ) ,
                new SqlParameter( "@Description"		, _CRMDetails.Description) ,
                new SqlParameter( "@ShowInAlert"		, _CRMDetails.ShowInAlert) ,
                new SqlParameter( "@AlertExpiryDate"		, _CRMDetails.AlertExpiryDate) ,
                new SqlParameter( "@CreatedBy"		, _CRMDetails.Createdby ) ,
                new SqlParameter( "@Modifiedby"		, _CRMDetails.Modifiedby) ,
                new SqlParameter("@CRMCategory", _CRMDetails.CRMCategory),
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
