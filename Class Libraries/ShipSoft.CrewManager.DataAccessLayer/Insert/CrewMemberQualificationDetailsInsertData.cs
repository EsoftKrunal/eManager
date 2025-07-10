using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberQualificationDetailsInsertData:DataAccessBase
    {
        private QualificationDetails _qualificationdetails;
        private CrewMemberQualificationDetailsInsertDataParameters _qualificationdetailsinsertdataparameters;
        public CrewMemberQualificationDetailsInsertData()
        {
            StoredProcedureName = StoredProcedure.Name.insertCrewMemberQualificationDetails.ToString();
        }
        public void Add()
        {
            _qualificationdetailsinsertdataparameters = new CrewMemberQualificationDetailsInsertDataParameters(QualificationDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            object id = dbhelper.RunScalar(base.ConnectionString, _qualificationdetailsinsertdataparameters.Parameters);
            //this.QualificationDetails.QualificationId = Convert.ToInt32(id.ToString());
        }
        public QualificationDetails QualificationDetails
        {
            get { return _qualificationdetails; }
            set {_qualificationdetails = value; }
        }
    }
    public class CrewMemberQualificationDetailsInsertDataParameters
    {
        private QualificationDetails _qualificationdetails;
        private SqlParameter[] _parameters;
        public CrewMemberQualificationDetailsInsertDataParameters(QualificationDetails qualificationdetails)
        {
            QualificationDetails = qualificationdetails;
            Build();
        }
        private void Build()
       {
           SqlParameter[] parameters =
            {
                new SqlParameter("@CrewQualificationId",QualificationDetails.QualificationId),
                new SqlParameter("@CrewId",QualificationDetails.CrewId),
                new SqlParameter("@CourseNameId",QualificationDetails.CourseNameId),
                new SqlParameter("@PassingYear",QualificationDetails.PassingYear),
                new SqlParameter("@PassingGrade",QualificationDetails.PassingGrade),
                new SqlParameter("@Subjects",QualificationDetails.Subjects),
                new SqlParameter("@ImagePath",QualificationDetails.ImagePath),
                new SqlParameter("@Remarks",QualificationDetails.Remarks),
                new SqlParameter("@CreatedBy",QualificationDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",QualificationDetails.ModifiedBy),
            };
           Parameters = parameters;
        }
        public QualificationDetails QualificationDetails
        {
            get { return _qualificationdetails; }
            set {_qualificationdetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
        

}
