using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberOtherDocumentDetails:IBusinessLogic 
    {
         private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
       public ProcessAddCrewMemberOtherDocumentDetails()
       {
       }
       public void Invoke()
       {
           CrewMemberOtherDocumentDetailsInsertData crewotherdocdetail = new CrewMemberOtherDocumentDetailsInsertData();
           crewotherdocdetail.OtherDocDetails = this.OtherDocDetails;
           crewotherdocdetail.Add();
            
       }
       public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }
    }
}
