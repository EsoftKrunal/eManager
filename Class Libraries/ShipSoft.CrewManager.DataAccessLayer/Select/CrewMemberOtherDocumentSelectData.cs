using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data; 
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberOtherDocumentSelectData:DataAccessBase 
    {
       private CrewOtherDocumentsDetails _crewotherdocdet=new CrewOtherDocumentsDetails();
       public CrewMemberOtherDocumentSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectCrewOtherDocumentDetails.ToString();
        }

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberOtherDocumentSelectDataParameters _crewmemberotherdocselectparameters = new CrewMemberOtherDocumentSelectDataParameters(_crewotherdocdet);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewmemberotherdocselectparameters.Parameters);
            return ds;
        }
       public CrewOtherDocumentsDetails OtherDocDetails
       {
           get { return _crewotherdocdet; }
           set { _crewotherdocdet = value; }
       }
    }
    public class CrewMemberOtherDocumentSelectDataParameters
    {
        private CrewOtherDocumentsDetails _crewotherdocdet = new CrewOtherDocumentsDetails();
        private SqlParameter[] _parameters;

        public CrewMemberOtherDocumentSelectDataParameters(CrewOtherDocumentsDetails crewotherdet)
        {
            this.OtherDocDetails = crewotherdet;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , OtherDocDetails.CrewId )
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
