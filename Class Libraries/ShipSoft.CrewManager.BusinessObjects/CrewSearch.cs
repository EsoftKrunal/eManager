using System;
using System.Collections.Generic;
using System.Text;
namespace ShipSoft.CrewManager.BusinessObjects
{
    public class BoCrewSearch
    {
        private int _LoginId;

        private string _CrewNumber;
        private string _FirstName;
       // private string _LastName;
        private int _Nationality;
        private int _CrewStatusId;
        private int _Rank;
        private string _PassportNo;
        private int _RecOffId;
        private int _AgeFrom;
        private int _AgeTo;
        private int _ExpFrom;
        private int _ExpTo;
        
        string _FromDate;
        string _ToDate;
        int _VesselId;
        int _Owner;
        int _VesselType;
        int _USvisa;
        int _SchengenVisa;
        int _FamilyMember;
        string _ReliefDue;

        public int LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }

        public string CrewNumber
        {
            get { return _CrewNumber; }
            set { _CrewNumber = value; }
        }
        public string FirstName
        {
            get { return _FirstName;}
            set { _FirstName = value;}
        }
     /*   public string LastName
        {
            get { return _LastName;}
            set { _LastName = value;}
        } */
        public int Nationality
        {
            get { return _Nationality;}
            set { _Nationality = value;}
        }
        public int CrewStatusId
        {
            get { return _CrewStatusId;}
            set { _CrewStatusId = value;}
        }
        public int Rank
        {
            get { return _Rank;}
            set { _Rank = value;}
        }
        public string PassportNo
        {
            get { return _PassportNo;}
            set { _PassportNo = value;}
        }
        public int RecOffId
        {
            get { return _RecOffId;}
            set { _RecOffId = value;}
        }
        public int AgeFrom
        {
            get { return _AgeFrom;}
            set { _AgeFrom = value;}
        }
        public int AgeTo
        {
            get { return _AgeTo;}
            set { _AgeTo = value;}
        }
        public int ExpFrom
        {
            get { return _ExpFrom;}
            set { _ExpFrom = value;}
        }
        public int ExpTo
        {
            get { return _ExpTo;}
            set { _ExpTo = value;}
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public int VesselId
        {
            get { return _VesselId; }
            set { _VesselId = value; }
        }
        public int Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        public int VesselType
        {
            get { return _VesselType; }
            set { _VesselType = value; }
        }
        public int USvisa
        {
            get { return _USvisa; }
            set { _USvisa = value; }
        }
        
        public int SchengenVisa
        {
            get { return _SchengenVisa; }
            set { _SchengenVisa = value; }
        }

        public int FamilyMember
        {
            get { return _FamilyMember; }
            set { _FamilyMember = value; }
        }
        public string ReliefDue
        {
            get { return _ReliefDue; }
            set { _ReliefDue = value; }
        }
    }
}
