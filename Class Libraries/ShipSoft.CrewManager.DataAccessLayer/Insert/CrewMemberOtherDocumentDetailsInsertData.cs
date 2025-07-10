using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
    public class CrewMemberOtherDocumentDetailsInsertData:DataAccessBase 
    {
        private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
        private CrewOtherDocumentsDetailsInsetDataParameters _crewotherdocdetails;
        public CrewMemberOtherDocumentDetailsInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.InsertUpdateCrewOtherDocumentDetails.ToString();
       }
       public void Add()
       {
           _crewotherdocdetails = new CrewOtherDocumentsDetailsInsetDataParameters(OtherDocDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString, _crewotherdocdetails.Parameters);

       }
        public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }
       public class CrewOtherDocumentsDetailsInsetDataParameters
       {
           private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
           private SqlParameter[] _parameters;
           public CrewOtherDocumentsDetailsInsetDataParameters(CrewOtherDocumentsDetails OtherDocDetails)
           {
               this.OtherDocDetails = OtherDocDetails;
               if (OtherDocDetails.ExpiryDate == "")
               {
                   OtherDocDetails.ExpiryDate = null;
               }
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@CrewOtherDocId",OtherDocDetails.CrewOtherDocId),
                new SqlParameter("@CrewId",OtherDocDetails.CrewId),
                new SqlParameter("@DocumentId",OtherDocDetails.CourseId ),
                new SqlParameter("@DocumentNumber",OtherDocDetails.DocumentNumber),
                new SqlParameter("@DateOfIssue",OtherDocDetails.DateOfIssue),
                new SqlParameter("@ExpiryDate",OtherDocDetails.ExpiryDate),
                new SqlParameter("@IssuedBy",OtherDocDetails.IssuedBy),
                new SqlParameter("@ImagePath",OtherDocDetails.ImagePath),
                new SqlParameter("@CreatedBy",OtherDocDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",OtherDocDetails.ModifiedBy),
                new SqlParameter("@IsActive",OtherDocDetails.IsActive), 
                
            };
               Parameters = parameters;
           }
          public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
