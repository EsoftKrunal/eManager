using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class CrewContact
    {
        private int _crewcontactid;
        private int _crewid;        
        private char _addresstype;
        private string _address1;
        private string _address2;
        private string _address3;
        private int  _countryid;
        private string _state;
        private string _city;
        private string _pincode;
        private int _nearestairportcountryid;
        private string _localairport;
        private int _telephonecountryid;
        private string _telephoneareacode;
        private string _telephonenumber;
        private int _mobilecountryid;
        private string _mobilenumber;
        private int _faxcountryid;
        private string _faxareacode;
        private string _faxnumber;
        private string _email1;
        private string _email2;
        private int _createdby;
        private int _modifiedby;

        private int _c_crewid;
        private char _c_addresstype;
        private string _c_address1;
        private string _c_address2;
        private string _c_address3;
        private int _c_countryid;
        private string _c_state;
        private string _c_city;
        private string _c_pincode;
        private int _c_nearestairportcountryid;
        private string _c_localairport;
        private int _c_telephonecountryid;
        private string _c_telephoneareacode;
        private string _c_telephonenumber;
        private int _c_mobilecountryid;
        private string _c_mobilenumber;
        private int _c_faxcountryid;
        private string _c_faxareacode;
        private string _c_faxnumber;
        private string _c_email1;
        private string _c_email2;
        private int _c_createdby;
        private int _c_modifiedby;

        public CrewContact()
        {

        }
        public int CrewContactId
        {
            get { return _crewcontactid; }
            set { _crewcontactid = value; }
        }

        public int CrewId
        {
            get { return _crewid; }
            set { _crewid = value; }
        }
        public char AddressType
        {
            get { return _addresstype; }
            set { _addresstype = value; }
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
        public int CountryId
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
        public string PinCode
        {
            get { return _pincode; }
            set { _pincode = value; }
        }
        public int NearestAirportConuntryId
        {
            get { return _nearestairportcountryid; }
            set { _nearestairportcountryid = value; }
        }
        public string LocalAirport
        {
            get { return _localairport; }
            set { _localairport = value; }
        }
        public int TelephoneConuntryId
        {
            get { return _telephonecountryid; }
            set { _telephonecountryid = value; }
        }
        public string TelephonrAreaCode
        {
            get { return _telephoneareacode; }
            set { _telephoneareacode = value; }
        }
        public string TelephoneNumber
        {
            get { return _telephonenumber; }
            set { _telephonenumber = value; }
        }
        public int MobileCountryId
        {
            get { return _mobilecountryid; }
            set { _mobilecountryid = value; }
        }
        public string MobileNumber
        {
            get { return _mobilenumber; }
            set { _mobilenumber = value; }
        }
        public int FaxCountryId
        {
            get { return _faxcountryid; }
            set { _faxcountryid = value; }
        }
        public string FaxAreaCode
        {
            get { return _faxareacode; }
            set { _faxareacode = value; }
        }
        public string FaxNumber
        {
            get { return _faxnumber; }
            set { _faxnumber = value; }
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
        public int CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }
        public int ModifiedBy
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }

        

        //public CrewContact()
        //{

        //}
        //public int CrewContactId
        //{
        //    get { return _crewcontactid; }
        //    set { _crewcontactid = value; }
        //}

        public int CCrewId
        {
            get { return _c_crewid; }
            set { _c_crewid = value; }
        }
        public char CAddressType
        {
            get { return _c_addresstype; }
            set { _c_addresstype = value; }
        }
        public string CAddress1
        {
            get { return _c_address1; }
            set { _c_address1 = value; }
        }
        public string CAddress2
        {
            get { return _c_address2; }
            set { _c_address2 = value; }
        }
        public string CAddress3
        {
            get { return _c_address3; }
            set { _c_address3 = value; }
        }
        public int CCountryId
        {
            get { return _c_countryid; }
            set { _c_countryid = value; }
        }
        public string CState
        {
            get { return _c_state; }
            set { _c_state = value; }
        }
        public string CCity
        {
            get { return _c_city; }
            set { _c_city = value; }
        }
        public string CPinCode
        {
            get { return _c_pincode; }
            set { _c_pincode = value; }
        }
        public int CNearestAirportConuntryId
        {
            get { return _c_nearestairportcountryid; }
            set { _c_nearestairportcountryid = value; }
        }
        public string CLocalAirport
        {
            get { return _c_localairport; }
            set { _c_localairport = value; }
        }
        public int CTelephoneConuntryId
        {
            get { return _c_telephonecountryid; }
            set { _c_telephonecountryid = value; }
        }
        public string CTelephonrAreaCode
        {
            get { return _c_telephoneareacode; }
            set { _c_telephoneareacode = value; }
        }
        public string CTelephoneNumber
        {
            get { return _c_telephonenumber; }
            set { _c_telephonenumber = value; }
        }
        public int CMobileCountryId
        {
            get { return _mobilecountryid; }
            set { _mobilecountryid = value; }
        }
        public string CMobileNumber
        {
            get { return _c_mobilenumber; }
            set { _c_mobilenumber = value; }
        }
        public int CFaxCountryId
        {
            get { return _faxcountryid; }
            set { _faxcountryid = value; }
        }
        public string CFaxAreaCode
        {
            get { return _faxareacode; }
            set { _faxareacode = value; }
        }
        public string CFaxNumber
        {
            get { return _c_faxnumber; }
            set { _c_faxnumber = value; }
        }
        public string CEmail1
        {
            get { return _c_email1; }
            set { _c_email1 = value; }
        }
        public string CEmail2
        {
            get { return _c_email2; }
            set { _c_email2 = value; }
        }
        public int CCreatedBy
        {
            get { return _c_createdby; }
            set { _c_createdby = value; }
        }
        public int CModifiedBy
        {
            get { return _c_modifiedby; }
            set { _c_modifiedby = value; }
        }
    }
}
