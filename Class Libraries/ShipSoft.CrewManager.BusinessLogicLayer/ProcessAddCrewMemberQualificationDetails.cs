using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberQualificationDetails:IBusinessLogic
    {
       private QualificationDetails _qualificationdetails;
       public ProcessAddCrewMemberQualificationDetails()
       {
       }
       public void Invoke()
       {
           CrewMemberQualificationDetailsInsertData insertdata=new CrewMemberQualificationDetailsInsertData();
           insertdata.QualificationDetails=this.QualificationDetails;
           insertdata.Add();
       }
       public QualificationDetails QualificationDetails
       {
           get{return _qualificationdetails;}
           set{_qualificationdetails=value;}
       }
    }
}
