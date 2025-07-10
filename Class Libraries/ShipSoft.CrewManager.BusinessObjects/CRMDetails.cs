using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class CRMDetails
    {
       private int _CrewCRMId = -1;
       private int _CrewId;
       private string _Description;
       private string _ShowInAlert;
       private string _AlertExpiryDate;
       private int _CreatedBy;
       private int _ModifiedBy;
       private DateTime _modifiedon;
       private char _CRMCategory;

       public CRMDetails()
        {

        }

       public int CrewCRMId
       {
           get { return _CrewCRMId; }
           set { _CrewCRMId = value; }
       }

       public int CrewId
       {
           get { return _CrewId; }
           set { _CrewId = value; }
       }

       public string Description
       {
           get { return _Description; }
           set { _Description = value; }
       }

       public string ShowInAlert
       {
           get { return _ShowInAlert; }
           set { _ShowInAlert = value; }
       }

       public string AlertExpiryDate
       {
           get { return _AlertExpiryDate; }
           set { _AlertExpiryDate = value; }
       }

       public int Createdby
       {
           get { return _CreatedBy; }
           set { _CreatedBy = value; }
       }

       public int Modifiedby
       {
           get { return _ModifiedBy; }
           set { _ModifiedBy = value; }
       }

       public DateTime Modifiedon
       {
           get { return _modifiedon; }
           set { _modifiedon = value; }
       }
       public char CRMCategory
       {
           get { return _CRMCategory;}
           set { _CRMCategory = value; }
       }
    }
}
