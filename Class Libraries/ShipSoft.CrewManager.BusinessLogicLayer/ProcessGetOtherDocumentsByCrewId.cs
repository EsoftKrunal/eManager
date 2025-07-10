using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public  class ProcessGetOtherDocumentsByCrewId:IBusinessLogic 
    {
       private CrewOtherDocumentsDetails _crewotherdoc;
        private DataSet  _result;
       public ProcessGetOtherDocumentsByCrewId()
        {

        }
        public void Invoke()
        {
            CrewMemberDocumentsSelectData crewdocuments = new CrewMemberDocumentsSelectData();
            crewdocuments.OtherDetails = this.OtherDetails;
            ResultSet = crewdocuments.Get();
        }
       public CrewOtherDocumentsDetails OtherDetails
        {
            get { return _crewotherdoc; }
            set { _crewotherdoc = value; }
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
