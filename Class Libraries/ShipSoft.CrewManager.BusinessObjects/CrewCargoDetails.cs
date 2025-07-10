using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class CrewCargoDetails
    {
       private int _DangerousCargoId = -1;
       private int _CrewId;
       private int _CargoId;
       private string _Number;
       private int _NationalityId;
       private string _GradeLevel;
       private string _PlaceOfIssue;
       private string _DateOfIssue;
       private string _ExpiryDate;
       private string _ImagePath;
       private int _CreatedBy=0;
       private int _ModifiedBy=0;

        //Constructor
        public CrewCargoDetails()
        {
        }

       public int DangerousCargoId
       {
           get { return _DangerousCargoId; }
           set { _DangerousCargoId = value; }
       }

       public int CrewId
       {
           get { return _CrewId; }
           set { _CrewId = value; }
       }

       public int CargoId
       {
           get { return _CargoId; }
           set { _CargoId = value; }
       }

       public string Number
       {
           get { return _Number; }
           set { _Number = value; }
       }

       public int NationalityId
       {
           get { return _NationalityId; }
           set { _NationalityId = value; }
       }

       public string GradeLevel
       {
           get { return _GradeLevel; }
           set { _GradeLevel = value; }
       }

       public string PlaceOfIssue
       {
           get { return _PlaceOfIssue; }
           set { _PlaceOfIssue = value; }
       }

       public string DateOfIssue
       {
           get { return _DateOfIssue; }
           set { _DateOfIssue = value; }
       }

       public string ExpiryDate
       {
           get { return _ExpiryDate; }
           set { _ExpiryDate = value; }
       }

       public string ImagePath
       {
           get { return _ImagePath; }
           set { _ImagePath = value; }
       }

       public int CreatedBy
       {
           get { return _CreatedBy; }
           set { _CreatedBy = value; }
       }

       public int Modifiedby
       {
           get { return _ModifiedBy; }
           set { _ModifiedBy = value; }
       }
    }
}
