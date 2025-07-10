using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewMemberCourseCrtificateDetailsSelectData:IBusinessLogic 
    {
        private CrewCourseCertificateDetails _crewCourseCertificateDetails;
        private DataSet  _result;
        public ProcessCrewMemberCourseCrtificateDetailsSelectData()
        {

        }
        public void Invoke()
        {
            CrewMemberCourseDetailsSelectData crewmembercoursesdetails = new CrewMemberCourseDetailsSelectData();
            crewmembercoursesdetails.crewCourseCertificateDetails = this.CrewCourseCertificateDetails;
            ResultSet = crewmembercoursesdetails.Get();
        }
        public CrewCourseCertificateDetails CrewCourseCertificateDetails
        {
            get { return _crewCourseCertificateDetails; }
            set { _crewCourseCertificateDetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
