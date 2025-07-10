using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberProfessionalDetailsInsertData : DataAccessBase
    {
        private  ProfessionalDetails _ProfessionalDetais;
        private CrewMemberProfessionalDetailsInsertDataParameters _ProfessionalDetailsInsertDataparameters;
        public CrewMemberProfessionalDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateProfessionalDetails.ToString();
        }
        public void Add()
        {
            string str;
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            _ProfessionalDetailsInsertDataparameters = new CrewMemberProfessionalDetailsInsertDataParameters(ProfessionalDetails);
            object id = dbhelper.RunScalar(base.ConnectionString, _ProfessionalDetailsInsertDataparameters.Parameters);
            str = id.ToString();
            _ProfessionalDetais.CertificateId= Convert .ToInt32(str);
        }
        public ProfessionalDetails ProfessionalDetails
        {
            get { return _ProfessionalDetais; }
            set { _ProfessionalDetais = value; }
        }
    }
    public class CrewMemberProfessionalDetailsInsertDataParameters
    {
        private ProfessionalDetails _ProfessionalDetais;
        private SqlParameter[] _parameters;

        public CrewMemberProfessionalDetailsInsertDataParameters(ProfessionalDetails param)
        {
            ProfessionalDetails = param;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {   new SqlParameter( "@CertificateId"		, _ProfessionalDetais.CertificateId ) ,
                new SqlParameter( "@CrewId"		, _ProfessionalDetais.CrewId ) ,
                new SqlParameter( "@DocumentTypeId"		, _ProfessionalDetais.DocumentTypeId ) ,
                new SqlParameter( "@DocumentSubTypeId"		, _ProfessionalDetais.DocumentSubTypeId ) ,
                new SqlParameter( "@CertificateTypeId"		, _ProfessionalDetais.CertificateId ), 
                new SqlParameter( "@CourseTypeId"		, _ProfessionalDetais.CourseTypeId ), 
                new SqlParameter( "@DocumentNumber"		, _ProfessionalDetais.DocumentNumber) ,
                new SqlParameter( "@IssueDate"		, _ProfessionalDetais.IssueDate  ) ,
                new SqlParameter( "@ExpiryDate"		, _ProfessionalDetais.ExpiryDate  ),
                new SqlParameter( "@NeverExpires"		, _ProfessionalDetais.NeverExpires  ) ,
                new SqlParameter( "@PlaceOfIssue"		, _ProfessionalDetais.PlaceOfIssue  ) ,
                new SqlParameter( "@ImagePath"		, _ProfessionalDetais.ImagePath) ,
                new SqlParameter( "@CertificateFlag"		, _ProfessionalDetais.CertificateFlag) ,
                new SqlParameter( "@CreatedBy"		, _ProfessionalDetais.CreatedBy ) ,
                new SqlParameter( "@ModifiedBy"		, _ProfessionalDetais.Modifiedby) ,
            };

            Parameters = parameters;
        }

        public ProfessionalDetails ProfessionalDetails
        {
            get { return _ProfessionalDetais; }
            set { _ProfessionalDetais = value; }
        }
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }

}
