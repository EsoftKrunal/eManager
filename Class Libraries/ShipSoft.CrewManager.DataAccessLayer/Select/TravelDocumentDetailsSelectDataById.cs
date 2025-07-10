using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class TravelDocumentDetailsSelectDataById:DataAccessBase
    {
       private TravelDocumentDetails _documentdetails;
       public TravelDocumentDetailsSelectDataById()
       {
           StoredProcedureName = StoredProcedure.Name.selecttraveldocumentdetailsdata.ToString();
       }
       public DataSet Get()
       {
           DataSet ds;
           DocumentDetailsSelectDataParameters documentdetailsselectdataparameters = new DocumentDetailsSelectDataParameters(DocumentDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           ds = dbhelper.Run(base.ConnectionString, documentdetailsselectdataparameters.Parameters);
           return ds;
       }
       public TravelDocumentDetails DocumentDetails
       {
           get { return _documentdetails; }
           set { _documentdetails = value; }
       }
       public class DocumentDetailsSelectDataParameters
       {
           private TravelDocumentDetails _documentdetails;
           private SqlParameter[] _parameters;
           public DocumentDetailsSelectDataParameters(TravelDocumentDetails documentdetails)
           {
               DocumentDetails = documentdetails;
               Build();
           }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter( "@TravelDocumentId",DocumentDetails.TravelDocumentId ),
                new SqlParameter("@CrewId",DocumentDetails.CrewId),
                new SqlParameter("@DocumentTypeId",DocumentDetails.DocumentTypeId)
            };

               Parameters = parameters;
           }
           public TravelDocumentDetails DocumentDetails
           {
               get { return _documentdetails; }
               set { _documentdetails = value; }
           }
           public SqlParameter[] Parameters
           {
               get { return _parameters; }
               set { _parameters = value; }
           }
       }
    }
}
