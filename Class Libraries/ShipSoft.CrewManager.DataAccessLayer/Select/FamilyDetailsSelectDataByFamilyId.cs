using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class FamilyDetailsSelectDataByFamilyId : DataAccessBase
    {
       private FamilyDetails _familydetails;

       public FamilyDetailsSelectDataByFamilyId()
        {
            StoredProcedureName = StoredProcedure.Name.SelectFamilyDetailsByFamilyId.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            FamilyDetailsSelectByFamilyIdParameters _familyDetailsbyidParameters = new FamilyDetailsSelectByFamilyIdParameters(FamilyDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _familyDetailsbyidParameters.Parameters);
            return ds;
        }
       public FamilyDetails FamilyDetails
       {
           get { return _familydetails; }
           set { _familydetails = value; }
       }
    }

    public class FamilyDetailsSelectByFamilyIdParameters
    {
        private FamilyDetails _familydetails;
        private SqlParameter[] _parameters;

        public FamilyDetailsSelectByFamilyIdParameters(FamilyDetails familydetails)
        {
            FamilyDetails = familydetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewFamilyId" , FamilyDetails.Crewfamilyid )
            };

            Parameters = parameters;
        }

        public FamilyDetails FamilyDetails
        {
            get { return _familydetails; }
            set { _familydetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
