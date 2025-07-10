using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class TravelDocumentSubTypeSelectData:DataAccessBase
    {
       private TravelDocumentDetails _documentdetails;
       public TravelDocumentSubTypeSelectData()
       {
           StoredProcedureName = StoredProcedure.Name.selecttraveldocumentsubtype.ToString();
       }
       public DataSet Get()
       {
           DataSet ds;
           DocumentDetailsSelectDataParameters documentdetailsselectdataparameters = new DocumentDetailsSelectDataParameters(DocumentDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           ds = dbhelper.Run(base.ConnectionString,documentdetailsselectdataparameters.Parameters);
           return ds;
       }
       public TravelDocumentDetails DocumentDetails
       {
           get { return _documentdetails; }
           set { _documentdetails = value; }
       }
   }
       public class TravelDocumentSelectDataParameters
       {
           private TravelDocumentDetails _documentdetails;
           private SqlParameter[] _parameters;
           public TravelDocumentSelectDataParameters(TravelDocumentDetails documentdetails)
        {
            DocumentDetails = documentdetails;
            Build();
        }
           private void Build()
           {
               SqlParameter[] parameters =
            {
                new SqlParameter( "@DocumentId",DocumentDetails.DocumentTypeId)
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

