using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class PersonalDetails
    {
       private int _id;
       private int _crewPersonalId;
       private int _countryofbirth;
       private int _maritalstatusid;
       private DateTime _datefirstjoin;
       private int _rankappliedid;
       private int _currentrankid;
       private int _vesselpoolid;
       private int _ownerpoolid;
       private int _recruitmentofficeid;
       private string _height;
       private string _weight;
       private string _chest;
       private string _waist;
       private string _collar;
       private string _shoesize;
       private string _photopath;
       private int _createdby;
       private int _modifiedby;
       
       public PersonalDetails()
        {

        }

        public int Id 
        {
            get { return _id; }
            set { _id = value; }
        }
       public int CrewPersonalId
       {
           get { return _crewPersonalId; }
           set { _crewPersonalId = value; }
       }
       public int Countryofbirth
        {
            get { return _countryofbirth; }
            set { _countryofbirth = value; }
        }

       public int Maritalstatusid
        {
            get { return _maritalstatusid; }
            set { _maritalstatusid = value; }
        }

       public DateTime Datefirstjoin
       {
           get { return _datefirstjoin; }
           set {_datefirstjoin = value; }
       }

       public int Rankappliedid
       {
           get { return _rankappliedid; }
           set { _rankappliedid = value; }
       }

       public int Currentrankid
       {
           get { return _currentrankid; }
           set { _currentrankid = value; }
       }

       public int Vesselpoolid
       {
           get { return _vesselpoolid; }
           set { _vesselpoolid = value; }
       }

       public int Ownerpoolid
       {
           get { return _ownerpoolid; }
           set { _ownerpoolid = value; }
       }

       public int Recruitmentofficeid
       {
           get { return _recruitmentofficeid; }
           set { _recruitmentofficeid = value; }
       }

       public string Height
       {
           get { return _height; }
           set { _height = value; }
       }

       public string Weight
       {
           get { return _weight; }
           set { _weight = value; }
       }

       public string Chest
       {
           get { return _chest; }
           set { _chest = value; }
       }

       public string Waist
       {
           get { return _waist; }
           set { _waist = value; }
       }

       public string Collar
       {
           get { return _collar; }
           set { _collar = value; }
       }

       public string Shoesize
       {
           get { return _shoesize; }
           set { _shoesize = value; }
       }

       public string Photopath
       {
           get { return _photopath; }
           set { _photopath = value; }
       }

       public int Createdby
       {
           get { return _createdby; }
           set { _createdby = value; }
       }

       public int Modifiedby
       {
           get { return _modifiedby; }
           set { _modifiedby = value; }
       }
    }
}
