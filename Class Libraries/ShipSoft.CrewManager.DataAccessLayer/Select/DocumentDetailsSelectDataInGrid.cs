using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class DocumentDetailsSelectDataInGrid:DataAccessBase
    {
       private TravelDocumentDetails _documentdetails;
       public DocumentDetailsSelectDataInGrid()
       {
          StoredProcedureName = StoredProcedure.Name.selectdocumentdetailsingrid.ToString();
       }
       public DataSet Get()
       {
           DataSet ds;
           DocumentDetailsSelectDataParameters dataparameters = new DocumentDetailsSelectDataParameters(DocumentDetails);
           DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
           ds = dbhelper.Run(base.ConnectionString, dataparameters.Parameters);
           return ds;
       }
       public TravelDocumentDetails DocumentDetails
       {
           get { return _documentdetails;}
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
                new SqlParameter( "@CrewId",DocumentDetails.CrewId )
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
