using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
   public class FamilyDetailsDeleteData : DataAccessBase
    {
       private FamilyDetails _familyDetails;
        private FamilyDetailsDeleteDataParameters _familyDetailsDeleteDataParameters;

       public FamilyDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteFamilyDetailsById.ToString();
        }
        public void Delete()
        {
            _familyDetailsDeleteDataParameters = new FamilyDetailsDeleteDataParameters(FamilyDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _familyDetailsDeleteDataParameters.Parameters;
            dbhelper.Run();
        }
       public FamilyDetails FamilyDetails
        {
            get { return _familyDetails; }
            set { _familyDetails = value; }
        }
    }
    public class FamilyDetailsDeleteDataParameters
    {
        private FamilyDetails _FamilyDetails;
        private SqlParameter[] _parameters;

        public FamilyDetailsDeleteDataParameters(FamilyDetails exp)
        {
            FamilyDetails = exp;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewFamilyId"  , FamilyDetails.Crewfamilyid)
		    };

            Parameters = parameters;
        }

        public FamilyDetails FamilyDetails
        {
            get { return _FamilyDetails; }
            set { _FamilyDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
