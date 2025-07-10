using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberDocumentDetailsInsertData:DataAccessBase
    {
       private TravelDocumentDetails _documentdetails;
       private CrewMemberDocumentInsertDataParameters _crewmemberdocumentinsertdataparameters;
       public CrewMemberDocumentDetailsInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.insertcrewmemberdocumentdetailsingrid.ToString();
       }
       public void Add()
       {
           _crewmemberdocumentinsertdataparameters = new CrewMemberDocumentInsertDataParameters(DocumentDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           object id = dbhelper.RunScalar(base.ConnectionString,_crewmemberdocumentinsertdataparameters.Parameters);

       }
       public TravelDocumentDetails DocumentDetails
       {
           get { return _documentdetails;}
           set { _documentdetails = value; }
       }
       public class CrewMemberDocumentInsertDataParameters
       {
           private TravelDocumentDetails _documentdetails;
           private SqlParameter[] _parameters;
           public CrewMemberDocumentInsertDataParameters(TravelDocumentDetails documentdetails)
           {
               DocumentDetails = documentdetails;
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter("@CrewDocumentId",DocumentDetails.TravelDocumentId),
                new SqlParameter("@CrewId",DocumentDetails.CrewId),
                new SqlParameter("@DocumentTypeId",DocumentDetails.DocumentTypeId),
               
                new SqlParameter("@DocumentNumber",DocumentDetails.DocumentNumber),
                
                new SqlParameter("@IssueDate",DocumentDetails.IssueDate),
                new SqlParameter("@ExpiryDate",DocumentDetails.ExpiryDate),
                
                new SqlParameter("@PlaceOfIssue",DocumentDetails.PlaceOfIssue),
                new SqlParameter("@ImagePath",DocumentDetails.ImagePath),
                //new SqlParameter("@Remarks",DocumentDetails.Remarks),
                new SqlParameter("@CreatedBy",DocumentDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",DocumentDetails.ModifiedBy),
                //new SqlParameter("@ModifiedOn",DocumentDetails.ModifiedOn),
            };
               Parameters = parameters;
           }
           public TravelDocumentDetails DocumentDetails
           {
               get { return _documentdetails; }
               set {_documentdetails = value; }
           }

           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
