using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class FamilyDetailsSelectData : DataAccessBase
    {
        private FamilyDetails _familydetails;

       public FamilyDetailsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectFamilyDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            FamilyDetailsParameters _familyDetailsParameters = new FamilyDetailsParameters(FamilyDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _familyDetailsParameters.Parameters);
            return ds;
        }
       public FamilyDetails FamilyDetails
       {
           get { return _familydetails; }
           set { _familydetails = value; }
       }
    }

    public class FamilyDetailsParameters
    {
        private FamilyDetails _familydetails;
        private SqlParameter[] _parameters;

        public FamilyDetailsParameters(FamilyDetails familydetails)
        {
            FamilyDetails = familydetails;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , FamilyDetails.CrewId )
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
