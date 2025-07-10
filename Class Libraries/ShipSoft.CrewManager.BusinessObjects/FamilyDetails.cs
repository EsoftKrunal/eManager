using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class FamilyDetails
    {
       private int _crewfamilyid=-1;
       private int _crewid;
       private string _firstname;
       private string _middlename;
       private string _lastname;
       private int _relationshipid;
       private char _isnok;
       private string _familyemployeeno;
       private int _sextypeid;
       private string _placeofbirth;
       private int _nationalityid;
       private string _address1;
       private string _address2;
       private string _address3;
       private int _countryid;
       private string _state;
       private string _city;
       private string _pin;
       private int _nearestairportid;
       private int _telcountryid;
       private string _telareacode;
       private string _telno;
       private int _mobilecountryid;
       private string _mobileno;
       private int _faxcountryid;
       private string _faxareacode;
       private string _faxno;
       private string _email1;
       private string _email2;
       private string _passportno;
       private string _issuedate;
       private string _expirydate;
       private string _placeofissue;
       private string _bankname;
       private string _bankaccountno;
       private string _bankaddress;
       private string _branchname;
       private string _beneficiary;
       private string _personalcode;
       private string _swiftcode;
       private string _ibanno;
       private int _typeofremittanceid;
       private String _ECNR;
       private string _DOB;
       private string _PhotoPath;
       private int _createdby;
       private int _modifiedby;
       private DateTime _modifiedon;
       private String _status;
       
       public FamilyDetails()
        {

        }

       public int Crewfamilyid
       {
           get { return _crewfamilyid; }
           set { _crewfamilyid = value; }
       }

       public int CrewId 
        {
            get { return _crewid; }
            set { _crewid = value; }
        }

       public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

       public string Middlename
        {
            get { return _middlename; }
            set { _middlename = value; }
        }

       public string Lastname
       {
           get { return _lastname; }
           set { _lastname = value; }
       }

       public int Relationshipid
       {
           get { return _relationshipid; }
           set { _relationshipid = value; }
       }

       public char IsNok
       {
           get { return _isnok; }
           set { _isnok = value; }
       }

       public string Familyemployeeno
       {
           get { return _familyemployeeno; }
           set { _familyemployeeno = value; }
       }

       public int SextypeId
       {
           get { return _sextypeid; }
           set { _sextypeid = value; }
       }

       public string Placeofbirth
       {
           get { return _placeofbirth; }
           set { _placeofbirth = value; }
       }

       public int Nationalityid
       {
           get { return _nationalityid; }
           set { _nationalityid = value; }
       }

       public string Address1
       {
           get { return _address1; }
           set { _address1 = value; }
       }

       public string Address2
       {
           get { return _address2; }
           set { _address2 = value; }
       }

       public string Address3
       {
           get { return _address3; }
           set { _address3 = value; }
       }

       public int Countryid
       {
           get { return _countryid; }
           set { _countryid = value; }
       }

       public string State
       {
           get { return _state; }
           set { _state = value; }
       }

       public string City
       {
           get { return _city; }
           set { _city = value; }
       }

       public string Pin
       {
           get { return _pin; }
           set { _pin = value; }
       }

       public int NearestAirportid
       {
           get { return _nearestairportid; }
           set { _nearestairportid = value; }
       }

       public int TelCountryid
       {
           get { return _telcountryid; }
           set { _telcountryid = value; }
       }

       public string TelAreaCode
       {
           get { return _telareacode; }
           set { _telareacode = value; }
       }

       public string Telno
       {
           get { return _telno; }
           set { _telno = value; }
       }

       public int MobileCountryid
       {
           get { return _mobilecountryid; }
           set { _mobilecountryid = value; }
       }

       public string Mobileno
       {
           get { return _mobileno; }
           set { _mobileno = value; }
       }

       public int FaxCountryid
       {
           get { return _faxcountryid; }
           set { _faxcountryid = value; }
       }

       public string FaxAreaCode
       {
           get { return _faxareacode; }
           set { _faxareacode = value; }
       }

       public string Faxno
       {
           get { return _faxno; }
           set { _faxno = value; }
       }

       public string Email1
       {
           get { return _email1; }
           set { _email1 = value; }
       }

       public string Email2
       {
           get { return _email2; }
           set { _email2 = value; }
       }

       public string Passportno
       {
           get { return _passportno; }
           set { _passportno = value; }
       }

       public string Issuedate
       {
           get { return _issuedate; }
           set { _issuedate = value; }
       }

       public string Expirydate
       {
           get { return _expirydate; }
           set { _expirydate = value; }
       }

       public string Placeofissue
       {
           get { return _placeofissue; }
           set { _placeofissue = value; }
       }

       public string Bankname
       {
           get { return _bankname; }
           set { _bankname = value; }
       }

       public string Bankaccountno
       {
           get { return _bankaccountno; }
           set { _bankaccountno = value; }
       }

       public string Bankaddress
       {
           get { return _bankaddress; }
           set { _bankaddress = value; }
       }

       public string Branchname
       {
           get { return _branchname; }
           set { _branchname = value; }
       }

       public string Beneficiary
       {
           get { return _beneficiary; }
           set { _beneficiary = value; }
       }

       public string Personalcode
       {
           get { return _personalcode; }
           set { _personalcode = value; }
       }

       public string Swiftcode
       {
           get { return _swiftcode; }
           set { _swiftcode = value; }
       }

       public string Ibanno
       {
           get { return _ibanno; }
           set { _ibanno = value; }
       }

       public int Typeofremittanceid
       {
           get { return _typeofremittanceid; }
           set { _typeofremittanceid = value; }
       }

       public String DOB
       {
           get { return _DOB; }
           set { _DOB = value; }
       }

       public String ECNR
       {
           get { return _ECNR; }
           set { _ECNR = value; }
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

       public DateTime Modifiedon
       {
           get { return _modifiedon; }
           set { _modifiedon = value; }
       }
       public string PhotoPath
       {
           get { return _PhotoPath; }
           set { _PhotoPath = value; }
       }
       public string Status
       {
           get { return _status; }
           set { _status = value; }
       }
    }
}
