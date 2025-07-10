using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectFamilyDetails : IBusinessLogic
    {
       private DataSet _resultset;
       private FamilyDetails _familyDetails;
       public ProcessSelectFamilyDetails()
        {

        }

        public void Invoke()
        {
            FamilyDetailsSelectData familyDetailsSelectData = new FamilyDetailsSelectData();
            familyDetailsSelectData.FamilyDetails = FamilyDetails;
            ResultSet = familyDetailsSelectData.Get(); 
        }

       public FamilyDetails FamilyDetails
       {
           get { return _familyDetails; }
           set { _familyDetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
