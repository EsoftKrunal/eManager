using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewMemberOtherDocumentDeleteData:DataAccessBase 
    {
           private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
        private CrewMemberOtherDocumentDeleteDataParameters _crewotherdocdeletedataparameter;

        public CrewMemberOtherDocumentDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewOtherDocumentsDetailsById.ToString();
        }
        public void Delete()
        {
            _crewotherdocdeletedataparameter = new CrewMemberOtherDocumentDeleteDataParameters(OtherDocDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewotherdocdeletedataparameter.Parameters;
            dbhelper.Run();
        }
        public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }
    }
     public class CrewMemberOtherDocumentDeleteDataParameters
    {
          private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
        private SqlParameter[] _parameters;

         public CrewMemberOtherDocumentDeleteDataParameters(CrewOtherDocumentsDetails otherdoc)
        {
            OtherDocDetails = otherdoc;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewOtherDocId"  , OtherDocDetails.CrewOtherDocId)
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
