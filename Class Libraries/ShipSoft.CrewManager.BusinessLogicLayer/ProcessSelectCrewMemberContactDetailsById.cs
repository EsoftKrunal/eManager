using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewMemberContactDetailsById : IBusinessLogic
    {
        private CrewContact _crewContact;
        private DataSet _resultset;

        public ProcessSelectCrewMemberContactDetailsById()
        {

        }
        public void Invoke()
        {
            CrewMemberContactDetailsSelectDataById selectcrewmember = new CrewMemberContactDetailsSelectDataById();
            selectcrewmember.CrewMember = _crewContact;
            ResultSet = selectcrewmember.Get();
            int p,c;
            if (ResultSet.Tables.Count > 0)
            {
                if (ResultSet.Tables[0].Rows.Count >0)                
                {

                    if (ResultSet.Tables[0].Rows[0]["AddressType"].ToString() == "P")
                    {
                        p = 0; c = 1;
                    }
                    else
                    {
                        p = 1; c = 0;
                    }


                    _crewContact.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());

                    _crewContact.CrewContactId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewContactId"].ToString());
                    _crewContact.Address1 = ResultSet.Tables[0].Rows[p]["Address1"].ToString();
                    _crewContact.Address2 = ResultSet.Tables[0].Rows[p]["Address2"].ToString();
                    _crewContact.Address3 = ResultSet.Tables[0].Rows[p]["Address3"].ToString();
                    _crewContact.CountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["CountryId"].ToString());
                    _crewContact.State = ResultSet.Tables[0].Rows[p]["State"].ToString();
                    _crewContact.City = ResultSet.Tables[0].Rows[p]["City"].ToString();
                    _crewContact.PinCode = ResultSet.Tables[0].Rows[p]["PinCode"].ToString();
                    _crewContact.NearestAirportConuntryId = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["NearestAirportCountryId"].ToString());
                    //if (Convert.IsDBNull(ResultSet.Tables[0].Rows[p]["LocalAirportId"]))
                    //{
                    //    _crewContact.LocalAirportId = 0;
                    //}
                    //else
                    //{
                        _crewContact.LocalAirport = ResultSet.Tables[0].Rows[p]["LocalAirportId"].ToString();
                    //}
                    _crewContact.TelephoneConuntryId = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["TelephoneCountryId"].ToString());
                    _crewContact.TelephonrAreaCode = ResultSet.Tables[0].Rows[p]["TelephoneAreaCode"].ToString();
                    _crewContact.TelephoneNumber = ResultSet.Tables[0].Rows[p]["TelephoneNumber"].ToString();
                    _crewContact.MobileCountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["MobileCountryId"].ToString());
                    _crewContact.MobileNumber = ResultSet.Tables[0].Rows[p]["MobileNumber"].ToString();
                    _crewContact.FaxCountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["FaxCountryId"].ToString());
                    _crewContact.FaxAreaCode = ResultSet.Tables[0].Rows[p]["FaxAreaCode"].ToString();
                    _crewContact.FaxNumber =  ResultSet.Tables[0].Rows[p]["FaxNumber"].ToString();
                    _crewContact.Email1 = ResultSet.Tables[0].Rows[p]["Email1"].ToString();
                    _crewContact.Email2 = ResultSet.Tables[0].Rows[p]["Email2"].ToString();
                    _crewContact.CreatedBy = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["CreatedBy"].ToString());
                    try
                    {
                        _crewContact.ModifiedBy = Convert.ToInt32(ResultSet.Tables[0].Rows[p]["ModifiedBy"].ToString());
                    }
                    catch { _crewContact.ModifiedBy = 0; }
                                     
                    _crewContact.CAddress1 = ResultSet.Tables[0].Rows[c]["Address1"].ToString();
                    _crewContact.CAddress2 = ResultSet.Tables[0].Rows[c]["Address2"].ToString();
                    _crewContact.CAddress3 = ResultSet.Tables[0].Rows[c]["Address3"].ToString();
                    _crewContact.CCountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["CountryId"].ToString());
                    _crewContact.CState = ResultSet.Tables[0].Rows[c]["State"].ToString();
                    _crewContact.CCity = ResultSet.Tables[0].Rows[c]["City"].ToString();
                    _crewContact.CPinCode = ResultSet.Tables[0].Rows[c]["PinCode"].ToString();
                    _crewContact.CNearestAirportConuntryId = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["NearestAirportCountryId"].ToString());
                    //if (Convert.IsDBNull(ResultSet.Tables[0].Rows[c]["LocalAirportId"]))
                    //{
                    //    _crewContact.CLocalAirportId = 0;
                    //}
                    //else
                    //{
                        _crewContact.CLocalAirport = ResultSet.Tables[0].Rows[c]["LocalAirportId"].ToString();
                    //}
                    _crewContact.CTelephoneConuntryId = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["TelephoneCountryId"].ToString());
                    _crewContact.CTelephonrAreaCode = ResultSet.Tables[0].Rows[c]["TelephoneAreaCode"].ToString();
                    _crewContact.CTelephoneNumber = ResultSet.Tables[0].Rows[c]["TelephoneNumber"].ToString();
                    _crewContact.CMobileCountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["MobileCountryId"].ToString());
                    _crewContact.CMobileNumber = ResultSet.Tables[0].Rows[c]["MobileNumber"].ToString();
                    _crewContact.CFaxCountryId = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["FaxCountryId"].ToString());
                    _crewContact.CFaxAreaCode = ResultSet.Tables[0].Rows[c]["FaxAreaCode"].ToString();
                    _crewContact.CFaxNumber =  ResultSet.Tables[0].Rows[c]["FaxNumber"].ToString();
                    _crewContact.CEmail1 = ResultSet.Tables[0].Rows[c]["Email1"].ToString();
                    _crewContact.CEmail2 = ResultSet.Tables[0].Rows[c]["Email2"].ToString();
                    _crewContact.CCreatedBy = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["CreatedBy"].ToString());
                    try
                    {
                        _crewContact.CModifiedBy = Convert.ToInt32(ResultSet.Tables[0].Rows[c]["ModifiedBy"].ToString());
                    }
                    catch { _crewContact.CModifiedBy = 0; }
                }
            }
        }
        public CrewContact ContactDetails
        {
            get { return _crewContact; }
            set { _crewContact = value; }
        }
        private DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
