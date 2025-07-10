using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberPersonalDetailsInsertData : DataAccessBase
    {
        private CrewMember _crewMember;
        private CrewMemberPersonalDetailsInsertDataParameters _crewmemberInsertDataparameters;
        public CrewMemberPersonalDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdatePersonalDetails.ToString();
        }
        public void Add()
        {
            char C;
            C = '|';
            string[] str;
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            _crewmemberInsertDataparameters = new CrewMemberPersonalDetailsInsertDataParameters(CrewMember);
	        object id = dbhelper.RunScalar(base.ConnectionString, _crewmemberInsertDataparameters.Parameters);
            str = id.ToString().Split(C);
            _crewMember.EmpNo = str[0];
            _crewMember.Id = Convert.ToInt32(str[1]) ;
        }
        public CrewMember CrewMember
        {
            get { return _crewMember; }
            set { _crewMember = value; }
        }
    }
    public class CrewMemberPersonalDetailsInsertDataParameters
    {
        private CrewMember _crewMember;
        private SqlParameter[] _parameters;

        public CrewMemberPersonalDetailsInsertDataParameters(CrewMember crewMember)
        {
            CrewMember = crewMember;
            Build();
        }
        private void Build()
        {
            SqlParameter[] parameters =
            {   new SqlParameter( "@CrewId"		, _crewMember.Id ) ,
                new SqlParameter( "@FirstName"		, CrewMember.FirstName ) ,
                new SqlParameter( "@MiddleName"		, CrewMember.MiddleName ) ,
                new SqlParameter( "@LastName"		, CrewMember.LastName ) ,
                new SqlParameter( "@DOB"		, CrewMember.DOB ), 
                new SqlParameter( "@POB"		, CrewMember.PlaceOfBirth ), 
                new SqlParameter( "@Bmi"		, CrewMember.Bmi ), 
                new SqlParameter( "@NationalityId"		, CrewMember.Nationalty ) ,
                new SqlParameter( "@SexType"		, CrewMember.SexType ) ,
                new SqlParameter( "@CountryOfBirth"		, CrewMember.Countryofbirth ),
                new SqlParameter( "@MaritalStatusId"		, CrewMember.Maritalstatusid ) ,
                new SqlParameter( "@DateFirstJoin"		, CrewMember.Datefirstjoin ) ,
                new SqlParameter( "@RankAppliedId"		, CrewMember.Rankappliedid) ,
                new SqlParameter( "@RecruitmentOfficeId"		, CrewMember.Recruitmentofficeid) ,
                new SqlParameter( "@Height"		, CrewMember.Height) ,
                new SqlParameter( "@Weight"		, CrewMember.Weight) ,
                new SqlParameter( "@Waist"		, CrewMember.Waist) ,
                new SqlParameter( "@PhotoPath"		, CrewMember.Photopath) ,
                new SqlParameter( "@QualificationId"		, CrewMember.Qualification) ,
                new SqlParameter( "@ShirtSizeId"		, CrewMember.ShirtSize) ,
                new SqlParameter( "@ShoeSize"		, CrewMember.ShoeSize) ,
                new SqlParameter( "@BloodGroupId"		, CrewMember.BloodGroup) ,

                new SqlParameter( "@CreatedBy"		, CrewMember.CreatedBy  ) ,
                new SqlParameter( "@LastModifiedBy"		, CrewMember.Modifiedby), 
           
            };

            Parameters = parameters;
        }
        public CrewMember CrewMember
        {
            get { return _crewMember; }
            set { _crewMember = value; }
        }
        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }

}
