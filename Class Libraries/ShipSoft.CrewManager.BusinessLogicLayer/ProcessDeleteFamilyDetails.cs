using System;
using System.Collections.Generic;
using System.Text;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessDeleteFamilyDetails : IBusinessLogic
    {
       private FamilyDetails _familyDetails;

       public ProcessDeleteFamilyDetails()
        {

        }

        public void Invoke()
        {
            FamilyDetailsDeleteData familydata = new FamilyDetailsDeleteData();
            familydata.FamilyDetails = this.FamilyDetails;
            familydata.Delete();
        }

       public FamilyDetails FamilyDetails
        {
            get { return _familyDetails; }
            set { _familyDetails = value; }
        }
    }
}
