using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient; 
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewCourseCetificateDetailsInsetData:DataAccessBase 
    {
        private CrewCourseCertificateDetails _crewcoursecertificatedetails;
        private CrewCourseCetificateDetailsInsetDataParameters _crewcoursecetificatedetailsinsetdataparameters;
       public CrewCourseCetificateDetailsInsetData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewCourseCertificateDetails.ToString();
       }
       public void Add()
       {
           CrewCourseCetificateDetailsInsetDataParameters _crewcoursecetificatedetailsinsetdataparameters = new CrewCourseCetificateDetailsInsetDataParameters(CertificateDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString, _crewcoursecetificatedetailsinsetdataparameters.Parameters);

       }
        public CrewCourseCertificateDetails CertificateDetails
       {
           get { return _crewcoursecertificatedetails; }
           set { _crewcoursecertificatedetails = value; }
       }
       public class CrewCourseCetificateDetailsInsetDataParameters
       {
           private CrewCourseCertificateDetails _crewcoursecertificatedetails;
           private SqlParameter[] _parameters;
           public CrewCourseCetificateDetailsInsetDataParameters(CrewCourseCertificateDetails certificateDetails)
           {
               this.CertificateDetails  = certificateDetails;
               if (CertificateDetails.ExpiryDate == "")
               { 
                   CertificateDetails.ExpiryDate=null;
               }
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@@CourseCerficiateId",CertificateDetails.CourseCerficiateId),
                new SqlParameter("@CrewId",CertificateDetails.CrewId),
                new SqlParameter("@CourseCertificateId",CertificateDetails.CourseCertificateId),
                new SqlParameter("@DocumentNumber",CertificateDetails.DocumentNumber),
                new SqlParameter("@DateOfIssue",CertificateDetails.DateOfIssue),
                new SqlParameter("@ExpiryDate",CertificateDetails.ExpiryDate),
                new SqlParameter("@IssuedBy",CertificateDetails.IssuedBy),
                new SqlParameter("@ImagePath",CertificateDetails.ImagePath),
                new SqlParameter("@CreatedBy",CertificateDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",CertificateDetails.ModifiedBy),
                
            };
               Parameters = parameters;
           }
           public CrewCourseCertificateDetails CertificateDetails
           {
               get { return _crewcoursecertificatedetails; }
               set { _crewcoursecertificatedetails = value; }
           }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
