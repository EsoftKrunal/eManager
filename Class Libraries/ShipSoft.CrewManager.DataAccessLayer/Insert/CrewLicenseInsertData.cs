using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public  class CrewLicenseInsertData:DataAccessBase 
    {
        private CrewLicenseDetails  _crewLicenseDetails;
       private CrewLicenseDetailsInsetDataParameters _crewlicensedetailsinsetdataparameters;
        public CrewLicenseInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewLicenseDetails.ToString();
       }
       public void Add()
       {
           CrewLicenseDetailsInsetDataParameters _crewlicensedetailsinsetdataparameters = new CrewLicenseDetailsInsetDataParameters(LicenseDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString, _crewlicensedetailsinsetdataparameters.Parameters);

       }
        public CrewLicenseDetails  LicenseDetails
       {
           get { return _crewLicenseDetails; }
           set { _crewLicenseDetails = value; }
       }
       public class CrewLicenseDetailsInsetDataParameters
       {
           private CrewLicenseDetails  _crewlicendsedetails;
           private SqlParameter[] _parameters;
           public CrewLicenseDetailsInsetDataParameters(CrewLicenseDetails lice)
           {
               this.LicenseDetails = lice;
               if (LicenseDetails.ExpiryDate == "")
               {
                   LicenseDetails.ExpiryDate = null;
               }
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@CrewLicenseId",LicenseDetails.CrewLicenseId),
                new SqlParameter("@CrewId",LicenseDetails.CrewId),
                new SqlParameter("@LicenseId",LicenseDetails.LicenseId),
                new SqlParameter("@Grade",LicenseDetails.Grade),
                new SqlParameter("@Number",LicenseDetails.Number),
                new SqlParameter("@IssueDate",LicenseDetails.IssueDate),
                new SqlParameter("@ExpiryDate",LicenseDetails.ExpiryDate),
                new SqlParameter("@PlaceofIssue",LicenseDetails.PlaceOfIssue),
                new SqlParameter("@CountryId",LicenseDetails.CountryId),
                new SqlParameter("@ImagePath",LicenseDetails.ImagePath),
                new SqlParameter("@IsVerified",LicenseDetails.IsVerified),
                new SqlParameter("@VerifiedBy",LicenseDetails.VerifiedBy),
                new SqlParameter("@CreatedBy",LicenseDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",LicenseDetails.ModifiedBy),
                
            };
               Parameters = parameters;
           }
           public CrewLicenseDetails LicenseDetails
           {
               get { return _crewlicendsedetails; }
               set { _crewlicendsedetails = value; }
           }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
