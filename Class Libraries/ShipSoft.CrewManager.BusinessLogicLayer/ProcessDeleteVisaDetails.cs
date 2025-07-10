using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessDeleteVisaDetails : IBusinessLogic
    {
       private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;

       public ProcessDeleteVisaDetails()
        {

        }

        public void Invoke()
        {
            VisaDetailsDeleteData visadata = new VisaDetailsDeleteData();
            visadata.CrewFamilyDocumentDetails = this.CrewFamilyDocumentDetails;
            visadata.Delete();
        }

       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }
    }
}
