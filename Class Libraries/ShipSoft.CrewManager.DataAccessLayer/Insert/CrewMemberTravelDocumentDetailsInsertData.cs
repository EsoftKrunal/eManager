using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Insert
{
   public class CrewMemberTravelDocumentDetailsInsertData:DataAccessBase
    {
       private TravelDocumentDetails _documentdetails;
       private CrewMemberDocumentInsertDataParameters _crewmemberdocumentinsertdataparameters;
       public CrewMemberTravelDocumentDetailsInsertData()
       {
           StoredProcedureName = StoredProcedure.Name.insertupdatecrewmembertraveldocumentdetails.ToString();
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
                new SqlParameter("@TravelDocumentId",DocumentDetails.TravelDocumentId),
                new SqlParameter("@CrewId",DocumentDetails.CrewId),
                new SqlParameter("@DocumentTypeId",DocumentDetails.DocumentTypeId),
                new SqlParameter("@DocumentNumber",DocumentDetails.DocumentNumber),
                new SqlParameter("@IssueDate",DocumentDetails.IssueDate),
                new SqlParameter("@ExpiryDate",DocumentDetails.ExpiryDate),
                new SqlParameter("@VisaName",DocumentDetails.VisaName),
                new SqlParameter("@ECNR",DocumentDetails.ECNR),
                new SqlParameter("@FlagStateId",DocumentDetails.FlagStateId),
                new SqlParameter("@PlaceOfIssue",DocumentDetails.PlaceOfIssue),
                new SqlParameter("@ImagePath",DocumentDetails.ImagePath),
                new SqlParameter("@CreatedBy",DocumentDetails.CreatedBy),
                new SqlParameter("@ModifiedBy",DocumentDetails.ModifiedBy),
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
