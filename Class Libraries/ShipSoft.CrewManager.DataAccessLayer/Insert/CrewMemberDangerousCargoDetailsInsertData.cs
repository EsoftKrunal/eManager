using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberDangerousCargoDetailsInsertData : DataAccessBase
    {
       private CrewCargoDetails _crewCargoDetails;
       private CrewMemberDangerousCargoDetailsInsertDataParameters _crewMemberDangerousCargoDetailsInsertDataParameters;
       public CrewMemberDangerousCargoDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateDangerousCargoDetails.ToString();
        }
        public void Add()
        {
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            _crewMemberDangerousCargoDetailsInsertDataParameters = new CrewMemberDangerousCargoDetailsInsertDataParameters(CrewCargoDetails);
            dbhelper.RunScalar(base.ConnectionString, _crewMemberDangerousCargoDetailsInsertDataParameters.Parameters);
        }
       public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }
    }
    public class CrewMemberDangerousCargoDetailsInsertDataParameters
    {
        private CrewCargoDetails _crewCargoDetails;
        private SqlParameter[] _parameters;

        public CrewMemberDangerousCargoDetailsInsertDataParameters(CrewCargoDetails param)
        {
            CrewCargoDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@DangerousCargoId"		, CrewCargoDetails.DangerousCargoId) ,
                new SqlParameter( "@CrewId"		, CrewCargoDetails.CrewId) ,
                new SqlParameter( "@CargoId"		, CrewCargoDetails.CargoId ) ,
                new SqlParameter( "@Number"		, CrewCargoDetails.Number ) ,
                new SqlParameter( "@NationalityId"		, CrewCargoDetails.NationalityId ) ,
                new SqlParameter( "@GradeLevel"		, CrewCargoDetails.GradeLevel ) ,
                new SqlParameter( "@PlaceOfIssue"		, CrewCargoDetails.PlaceOfIssue ) ,
                new SqlParameter( "@DateOfIssue"		,CrewCargoDetails.DateOfIssue.Trim()),
                new SqlParameter( "@ExpiryDate"		,CrewCargoDetails.ExpiryDate.Trim()), 
                new SqlParameter( "@ImagePath"		, CrewCargoDetails.ImagePath ) ,
                new SqlParameter( "@CreatedBy"		, CrewCargoDetails.CreatedBy) ,
                new SqlParameter( "@ModifiedBy"		, CrewCargoDetails.Modifiedby ),
            };

            Parameters = parameters;
        }

        public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
