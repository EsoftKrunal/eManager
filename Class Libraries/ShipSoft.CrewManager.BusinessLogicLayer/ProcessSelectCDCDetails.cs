using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCDCDetails : IBusinessLogic
    {
       private DataSet _resultset;
       private CrewFamilyDocumentDetails _crewfamilydocumentdetails;
        public ProcessSelectCDCDetails()
        {

        }

        public void Invoke()
        {

            CDCDetailsSelectData cdcSelectData = new CDCDetailsSelectData();
            cdcSelectData.CrewFamilyDocumentDetails = CrewFamilyDocumentDetails;
            ResultSet = cdcSelectData.Get();
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
