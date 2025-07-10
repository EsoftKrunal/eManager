using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberExperienceDetailsInsertData : DataAccessBase
    {
       private ExperienceDetails _experiencedetails;
       private CrewMemberExperienceDetailsInsertDataParameters _experiencedetailsparameters;

       public CrewMemberExperienceDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.InsertUpdateExperienceDetails.ToString();
        }

        public void Add()
        {
            _experiencedetailsparameters = new CrewMemberExperienceDetailsInsertDataParameters(ExperienceDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, _experiencedetailsparameters.Parameters);
            this.ExperienceDetails.ExperienceId = Convert.ToInt32(id.ToString());
            //this.CrewMember.Id = Convert.ToInt32(id.ToString());
       
        }

       public ExperienceDetails ExperienceDetails
        {
            get { return _experiencedetails; }
            set { _experiencedetails = value; }
        }
    }

    public class CrewMemberExperienceDetailsInsertDataParameters
    {
        private ExperienceDetails _experiencedetails;
        private SqlParameter[] _parameters;

        public CrewMemberExperienceDetailsInsertDataParameters(ExperienceDetails experiencedetails)
        {
            ExperienceDetails = experiencedetails;
            Build();
        }

        private void Build()
        {

            SqlParameter[] parameters =
            {   new SqlParameter( "@CrewExperienceId"		, _experiencedetails.ExperienceId ) ,
                new SqlParameter( "@CrewId"		, _experiencedetails.CrewId ) ,
                new SqlParameter( "@CompanyName"		, _experiencedetails.Companyname ) ,
                new SqlParameter( "@RankId"		, _experiencedetails.RankId ) ,
                new SqlParameter( "@SignOn"		, _experiencedetails.SignOnDate ) ,
                new SqlParameter( "@SignOff"		, _experiencedetails.SignOffDate ), 
                new SqlParameter( "@SignOffReasonId"		, _experiencedetails.SignOffReasonId), 
                new SqlParameter( "@VesselName"		, _experiencedetails.Vesselname ) ,
                new SqlParameter( "@VesselTypeId"		, _experiencedetails.VesseltypeId) ,
                new SqlParameter( "@Registry"		, _experiencedetails.Registry) ,
                new SqlParameter( "@DWT"		, _experiencedetails.Dwt) ,
                new SqlParameter( "@GWT"		, _experiencedetails.Gwt) ,
                new SqlParameter( "@BHP"		, _experiencedetails.Bhp) ,
                new SqlParameter( "@BHP1"		, _experiencedetails.Bhp1) ,
                new SqlParameter( "@ExpFlag"		, _experiencedetails.Expflag) ,
                new SqlParameter( "@CreatedBy"		, _experiencedetails.Createdby  ) ,
                new SqlParameter( "@ModifiedBy"		, _experiencedetails.Modifiedby) 
            };

            Parameters = parameters;
        }

        public ExperienceDetails ExperienceDetails
        {
            get { return _experiencedetails; }
            set { _experiencedetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
