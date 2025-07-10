using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessAddCRMDetails : IBusinessLogic
    {
       private CRMDetails _cRMDetails;
       public ProcessAddCRMDetails()
        {

        }

        public void Invoke()
        {
            CRMDetailsInsertData insertcrmdetails = new CRMDetailsInsertData();
            insertcrmdetails.CRMDetails = this.CRMDetails;
            insertcrmdetails.Add();
        }

       public CRMDetails CRMDetails
        {
            get { return _cRMDetails; }
            set { _cRMDetails = value; }
        }

    }
}
