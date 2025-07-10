using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberAcademicDetailsInsertData : DataAccessBase
    {
        private AcademicDetails _AcademicDetails;
        private CrewMemberAcademicDetailsInsertDataParameters _AcademicDetailsInsertDataparameters;
        public CrewMemberAcademicDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateAcademicDetails.ToString();
        }
        public void Add()
        {
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            _AcademicDetailsInsertDataparameters = new CrewMemberAcademicDetailsInsertDataParameters(AcademicDetails);
            dbhelper.RunScalar(base.ConnectionString, _AcademicDetailsInsertDataparameters.Parameters);
        }
        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }
    }
    public class CrewMemberAcademicDetailsInsertDataParameters
    {
        private AcademicDetails _AcademicDetails;
        private SqlParameter[] _parameters;

        public CrewMemberAcademicDetailsInsertDataParameters(AcademicDetails param)
        {
            AcademicDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@AcademicDetailsId"		, AcademicDetails.AcademicDetailsId ) ,
                new SqlParameter( "@CrewId"		, AcademicDetails.CrewId) ,
                new SqlParameter( "@TypeOfCertificate"		, AcademicDetails.TypeOfCertificate ) ,
                new SqlParameter( "@Institute"		, AcademicDetails.Institute ) ,
                new SqlParameter( "@DurationFrom"		,AcademicDetails.DurationForm.Trim()),
                new SqlParameter( "@DurationTo"		,AcademicDetails.DurationTo.Trim()), 
                new SqlParameter( "@Grade"		, AcademicDetails.Grade), 
                new SqlParameter( "@ImagePath"		, AcademicDetails.ImagePath ) ,
                new SqlParameter( "@CreatedBy"		, AcademicDetails.CreatedBy) ,
                new SqlParameter( "@ModifiedBy"		, AcademicDetails.Modifiedby ),
            };

            Parameters = parameters;
        }

        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }

}
