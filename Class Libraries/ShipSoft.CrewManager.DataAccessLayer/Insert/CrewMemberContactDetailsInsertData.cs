using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberContactDetailsInsertData:DataAccessBase
    {
       private CrewContact _crewcontact;
       private CrewMemberContactDetailsInsertDataParameters _crewrecordcontactInsertDataparameters;

       public CrewMemberContactDetailsInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewRecordContactData.ToString();
       }
       public void Add()
       {
           _crewrecordcontactInsertDataparameters = new CrewMemberContactDetailsInsertDataParameters(CrewContact);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           dbhelper.Parameters = _crewrecordcontactInsertDataparameters.Parameters;
           dbhelper.Run();
           //object id = dbhelper.RunScalar(base.ConnectionString, _crewrecordcontactInsertDataparameters.Parameters);
           //this.CrewContact.Id = Convert.ToInt32(id.ToString());
       }
       public CrewContact CrewContact
       {
           get { return _crewcontact; }
           set { _crewcontact = value; }
       }
   }

   public class CrewMemberContactDetailsInsertDataParameters
   {
       private CrewContact _crewcontact;
       private SqlParameter[] _parameters;

       public CrewMemberContactDetailsInsertDataParameters(CrewContact Crewcontact)
       {
           CrewContact = Crewcontact;
           Build();
       }
       private void Build()
       {
           SqlParameter[] parameters =
            {
               // new SqlParameter("@C_CrewContactId",CrewContact.CrewContactId),
                new SqlParameter("@C_CrewId",CrewContact.CrewId),

                new SqlParameter("@P_AddressType",CrewContact.AddressType),
                new SqlParameter("@P_Address1", CrewContact.Address1) ,
                new SqlParameter("@P_Address2", CrewContact.Address2 ) ,
                new SqlParameter("@P_Address3", CrewContact.Address3 ) ,
                new SqlParameter("@P_CountryId",CrewContact.CountryId),
                new SqlParameter("@P_State",CrewContact.State),
                new SqlParameter("@P_City",CrewContact.City),
                new SqlParameter("@P_PINCode",CrewContact.PinCode),
                new SqlParameter("@P_NearestAirportCountryId",CrewContact.NearestAirportConuntryId),
                new SqlParameter("@P_LocalAirportId",CrewContact.LocalAirport),
                new SqlParameter("@P_TelephoneCountryId",CrewContact.TelephoneConuntryId),
                new SqlParameter("@P_TelephoneAreaCode",CrewContact.TelephonrAreaCode),
                new SqlParameter("@P_TelephoneNumber",CrewContact.TelephoneNumber),
                new SqlParameter("@P_MobileCountryId",CrewContact.MobileCountryId),
                new SqlParameter("@P_MobileNumber",CrewContact.MobileNumber),
                new SqlParameter("@P_FaxCountryId",CrewContact.FaxCountryId),
                new SqlParameter("@P_FaxAreaCode",CrewContact.FaxAreaCode),
                new SqlParameter("@P_FaxNumber",CrewContact.FaxNumber),
                new SqlParameter("@P_Email1",CrewContact.Email1),
                new SqlParameter("@P_Email2",CrewContact.Email2),
                new SqlParameter("@P_CreatedBy",CrewContact.CreatedBy),
                new SqlParameter("@P_ModifiedBy",CrewContact.ModifiedBy),

                new SqlParameter("@C_AddressType",CrewContact.CAddressType),
                new SqlParameter("@C_Address1", CrewContact.CAddress1) ,
                new SqlParameter("@C_Address2", CrewContact.CAddress2 ) ,
                new SqlParameter("@C_Address3", CrewContact.CAddress3 ) ,
                new SqlParameter("@C_CountryId",CrewContact.CCountryId),
                new SqlParameter("@C_State",CrewContact.CState),
                new SqlParameter("@C_City",CrewContact.CCity),
                new SqlParameter("@C_PINCode",CrewContact.CPinCode),
                new SqlParameter("@C_NearestAirportCountryId",CrewContact.CNearestAirportConuntryId),
                new SqlParameter("@C_LocalAirportId",CrewContact.CLocalAirport),
                new SqlParameter("@C_TelephoneCountryId",CrewContact.CTelephoneConuntryId),
                new SqlParameter("@C_TelephoneAreaCode",CrewContact.CTelephonrAreaCode),
                new SqlParameter("@C_TelephoneNumber",CrewContact.CTelephoneNumber),
                new SqlParameter("@C_MobileCountryId",CrewContact.CMobileCountryId),
                new SqlParameter("@C_MobileNumber",CrewContact.CMobileNumber),
                new SqlParameter("@C_FaxCountryId",CrewContact.CFaxCountryId),
                new SqlParameter("@C_FaxAreaCode",CrewContact.CFaxAreaCode),
                new SqlParameter("@C_FaxNumber",CrewContact.CFaxNumber),
                new SqlParameter("@C_Email1",CrewContact.CEmail1),
                new SqlParameter("@C_Email2",CrewContact.CEmail2),
                new SqlParameter("@C_CreatedBy",CrewContact.CCreatedBy),
                new SqlParameter ("@C_ModifiedBy",CrewContact.CModifiedBy),
            };

           Parameters = parameters;
       }
       public CrewContact CrewContact
       {
           get { return _crewcontact; }
           set { _crewcontact = value; }
       }

       public SqlParameter[] Parameters
       {
           get { return _parameters; }
           set { _parameters = value; }
       }

    }
}
