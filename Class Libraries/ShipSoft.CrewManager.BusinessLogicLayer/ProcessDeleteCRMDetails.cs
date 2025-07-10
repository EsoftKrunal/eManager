using System;
using System.Collections.Generic;
using System.Text;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessDeleteCRMDetails : IBusinessLogic
    {
       private CRMDetails _CRMDetails;

       public ProcessDeleteCRMDetails()
        {

        }

        public void Invoke()
        {
            CRMDetailsDeleteData crmdel = new CRMDetailsDeleteData();
            crmdel.CRMDetails = this.CRMDetails;
            crmdel.Delete();
        }

       public CRMDetails CRMDetails
        {
            get { return _CRMDetails; }
            set { _CRMDetails = value; }
        }
    }
}
