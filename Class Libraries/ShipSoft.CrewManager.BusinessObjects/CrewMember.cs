using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class CrewMember
    {
        private int _Id=-1;
        private string _EmpNo;
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private DateTime  _DOB;
        private string _PlaceOfBirth;
        private String _Age;
        private String _Bmi;
        private int _Nationality;
        private SexType _SexType;
        private String _CrewStatus;
        private int _CrewStatusId;
        private int _crewPersonalId;
        private int _countryofbirth;
        private MaritalStatus _maritalstatusid;
        private DateTime _datefirstjoin;
        private int _rankappliedid;
        private String _currentrank;
        private int _recruitmentofficeid;
        private string _height;
        private string _weight;
        private string _waist;
        private string _photopath;
        private string _LastVesssel;
        private int _Qualification;
        private ShirtSize _Shirtsize;
        private ShoeSize _Shoesize;
        private int _BloodGroup;
        //-----------------------
        private int _CreatedBy=0;
        private DateTime  _CreatedOn;
        private int _LastModifiedBy=0;
        private DateTime  _LastModifiedOn;
        private string _passportno;
        private string _status;
        private string _rankexp;
        public CrewMember()
        {
        }
        public int Id 
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string EmpNo
        {
            get { return _EmpNo; }
            set { _EmpNo = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName= value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public DateTime DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string PlaceOfBirth
        {
            get { return _PlaceOfBirth; }
            set { _PlaceOfBirth = value; }
        }
        public String  Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        public String Bmi
        {
            get { return _Bmi; }
            set { _Bmi = value; }
        }
        public int Nationalty
        {
            get { return _Nationality; }
            set { _Nationality  = value; }
        }
        public SexType SexType
        {
            get { return _SexType; }
            set { _SexType = value; }
        }
        public String CrewStatus
        {
            get { return _CrewStatus; }
            set { _CrewStatus = value; }
        }
        public int CrewStatusId
        {
            get { return _CrewStatusId; }
            set { _CrewStatusId = value; }
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
        public MaritalStatus Maritalstatusid
        {
            get { return _maritalstatusid; }
            set { _maritalstatusid = value; }
        }
        public DateTime Datefirstjoin
        {
            get { return _datefirstjoin; }
            set { _datefirstjoin = value; }
        }
        public int Rankappliedid
        {
            get { return _rankappliedid; }
            set { _rankappliedid = value; }
        }
        public String Currentrank
        {
            get { return _currentrank; }
            set { _currentrank = value; }
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
        public string Waist
        {
            get { return _waist; }
            set { _waist = value; }
        }
        public string Photopath
        {
            get { return _photopath; }
            set { _photopath = value; }
        }
        public String LastVessel
        {
            get { return _LastVesssel; }
            set { _LastVesssel = value; }
        }
        public int Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
        }
        public ShirtSize ShirtSize
        {
            get { return _Shirtsize; }
            set { _Shirtsize = value; }
        }
        public ShoeSize ShoeSize
        {
            get { return _Shoesize; }
            set { _Shoesize = value; }
        }
        //-------
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }
        public int Modifiedby
        {
            get { return _LastModifiedBy; }
            set { _LastModifiedBy = value; }
        }
        public DateTime ModifiedOn
        {
            get { return _LastModifiedOn; }
            set { _LastModifiedOn = value; }
        }
        public int BloodGroup
        {
            get { return _BloodGroup;}
            set { _BloodGroup=value;}
        }
        public string  PassportNo
        {
            get { return _passportno ; }
            set { _passportno = value; }
        }

        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string RankExp
        {
            get { return _rankexp; }
            set { _rankexp = value; }
        }

        private string _CurrentVesssel;

        public String CurrentVessel
        {
            get { return _CurrentVesssel; }
            set { _CurrentVesssel = value; }
        }

    }
}
