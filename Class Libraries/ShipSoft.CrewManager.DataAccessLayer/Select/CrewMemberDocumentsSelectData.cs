using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using System.Data;
namespace ShipSoft.CrewManager.DataAccessLayer.Select
{
   public class CrewMemberDocumentsSelectData:DataAccessBase 
    {
       private CrewOtherDocumentsDetails _crewotherdocdetails;
        

        public CrewMemberDocumentsSelectData()
        {
            StoredProcedureName = StoredProcedure.Name.SelectOtherDocumentsByCrewId.ToString();
        }
       

        public DataSet Get()
        {
            DataSet ds;
            CrewMemberDocumentsSelectDataParameters _crewotherdocparameters = new CrewMemberDocumentsSelectDataParameters(OtherDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            ds = dbhelper.Run(base.ConnectionString, _crewotherdocparameters.Parameters);
            return ds;
        }

       public CrewOtherDocumentsDetails OtherDetails
        {
            get { return _crewotherdocdetails; }
            set { _crewotherdocdetails = value; }
        }
    }
    public class CrewMemberDocumentsSelectDataParameters
    {
        private CrewOtherDocumentsDetails _crewotherdocdetails;
        private SqlParameter[] _parameters;

        public CrewMemberDocumentsSelectDataParameters(CrewOtherDocumentsDetails codoc)
        {
            this.OtherDetails = codoc;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
            {
                new SqlParameter( "@CrewId" , this.OtherDetails.CrewId )
            };

            Parameters = parameters;
        }


        public CrewOtherDocumentsDetails OtherDetails
        {
            get { return _crewotherdocdetails; }
            set { _crewotherdocdetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
