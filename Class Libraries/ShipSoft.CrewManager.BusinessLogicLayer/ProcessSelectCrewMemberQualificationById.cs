using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCrewMemberQualificationById:IBusinessLogic
    {
       private QualificationDetails _qualificationdetails;
       private DataSet _result;
       public ProcessSelectCrewMemberQualificationById()
       {
       }
       public void Invoke()
       {
           CrewMemberQualificationDetailsSelectDataById databyid = new CrewMemberQualificationDetailsSelectDataById();
           databyid.QualificationDetails = QualificationDetails;
           ResultSet = databyid.Get();
           QualificationDetails.CourseNameId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CourseNameId"].ToString());
           QualificationDetails.PassingYear = ResultSet.Tables[0].Rows[0]["PassingYear"].ToString();
           QualificationDetails.PassingGrade = ResultSet.Tables[0].Rows[0]["PassingGrade"].ToString();
           QualificationDetails.Subjects = ResultSet.Tables[0].Rows[0]["Subjects"].ToString();
           QualificationDetails.Remarks = ResultSet.Tables[0].Rows[0]["Remarks"].ToString();
       }
       public QualificationDetails QualificationDetails
       {
           get { return _qualificationdetails; }
           set { _qualificationdetails = value; }
       }
       public DataSet ResultSet
       {
           get { return _result; }
           set { _result = value; }
       }
    }
}
