using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewMemberTravelDetailsDeleteData : DataAccessBase
    {
        private TravelDocumentDetails _DocumentDetails;
        private CrewMemberTravelDetailsDeleteDataParameters _crewmembertraveldeletedataparameters;

        public CrewMemberTravelDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewMemberTravelDetailsById.ToString();
        }
        public void Delete()
        {
            _crewmembertraveldeletedataparameters = new CrewMemberTravelDetailsDeleteDataParameters(DocumentDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewmembertraveldeletedataparameters.Parameters;
            dbhelper.Run();
        }
        public TravelDocumentDetails DocumentDetails
        {
            get { return _DocumentDetails; }
            set { _DocumentDetails = value; }
        }
    }

    public class CrewMemberTravelDetailsDeleteDataParameters
    {
        private TravelDocumentDetails _DocumentDetails;
        private SqlParameter[] _parameters;

        public CrewMemberTravelDetailsDeleteDataParameters(TravelDocumentDetails doc)
        {
            DocumentDetails = doc;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
                new SqlParameter( "@TravelDocumentId"  , DocumentDetails.TravelDocumentId)
		    };

            Parameters = parameters;
        }

        public TravelDocumentDetails DocumentDetails
        {
            get { return _DocumentDetails; }
            set { _DocumentDetails = value; }
        }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
