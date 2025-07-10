using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectVisaDetails : IBusinessLogic
    {
       private DataSet _resultset;
       private CrewFamilyDocumentDetails _crewfamilydocumentdetails;
        public ProcessSelectVisaDetails()
        {

        }

        public void Invoke()
        {
            VisaSelectData visaSelectData = new VisaSelectData();
            visaSelectData.CrewFamilyDocumentDetails = CrewFamilyDocumentDetails;
            ResultSet = visaSelectData.Get();
        }

       public CrewFamilyDocumentDetails CrewFamilyDocumentDetails
       {
           get { return _crewfamilydocumentdetails; }
           set { _crewfamilydocumentdetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
