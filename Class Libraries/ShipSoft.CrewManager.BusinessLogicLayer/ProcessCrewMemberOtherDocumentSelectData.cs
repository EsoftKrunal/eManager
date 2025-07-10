using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessCrewMemberOtherDocumentSelectData:IBusinessLogic 
    {
            private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
        private DataSet  _result;
        public ProcessCrewMemberOtherDocumentSelectData()
        {

        }
        public void Invoke()
        {
            CrewMemberOtherDocumentSelectData  crewmembercoursesdetails = new CrewMemberOtherDocumentSelectData();
            crewmembercoursesdetails.OtherDocDetails  = this.OtherDocDetails;
            ResultSet = crewmembercoursesdetails.Get();
        }
        
         public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
