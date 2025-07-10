using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetCrewMemberCourses:IBusinessLogic 
    {
       private CrewCourseCertificateDetails _crewCourseCertificateDetails;
       private DataSet _result;
        public ProcessGetCrewMemberCourses()
        {

        }
        public void Invoke()
        {
            CrewMemberCouseSelctData crewmembercourses = new CrewMemberCouseSelctData();
            crewmembercourses.crewCourseCertificateDetails = this.CrewCourseCertificateDetails;
            ResultSet = crewmembercourses.Get(); ;
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
