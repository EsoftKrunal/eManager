using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCrewMemberFamilyVisaDetails : IBusinessLogic
    {
        private CrewFamilyDocumentDetails _crewFamilyDocumentDetails;
        public ProcessAddCrewMemberFamilyVisaDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberFamilyVisaDetailsInsertData insertvisadetails = new CrewMemberFamilyVisaDetailsInsertData();
            insertvisadetails.CrewFamilyDocumentDetails = this.CrewFamilyDocumentDetails;
            insertvisadetails.Add();
        }

       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
        {
            get { return _crewFamilyDocumentDetails; }
            set { _crewFamilyDocumentDetails = value; }
        }

    }
}
