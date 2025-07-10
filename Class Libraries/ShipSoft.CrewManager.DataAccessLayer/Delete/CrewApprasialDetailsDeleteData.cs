using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.DataAccessLayer.Delete
{
    public class CrewApprasialDetailsDeleteData:DataAccessBase 
    {
          private CrewApprasialDetails  _crewAprasialDetails;
        private CrewApprasialDetailsDeleteDataParameters _crewappdeletedata;

        public CrewApprasialDetailsDeleteData()
        {
            StoredProcedureName = StoredProcedure.Name.DeleteCrewApprasialDetailsById.ToString();
        }
        public void Delete()
        {
            _crewappdeletedata = new CrewApprasialDetailsDeleteDataParameters(ApprasialDetails);
            DataBaseHelper dbhelper = new DataBaseHelper(StoredProcedureName);
            dbhelper.Parameters = _crewappdeletedata.Parameters;
            dbhelper.Run();
        }
        public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }
    }
     public class CrewApprasialDetailsDeleteDataParameters
    {
        private CrewApprasialDetails  _crewAprasialDetails;
        private SqlParameter[] _parameters;

         public CrewApprasialDetailsDeleteDataParameters(CrewApprasialDetails app)
        {
            this.ApprasialDetails = app;
            Build();
        }

        private void Build()
        {
            SqlParameter[] parameters =
		    {
		        new SqlParameter( "@CrewAppraisalId"  , ApprasialDetails.CrewAppraisalId)
		    };

            Parameters = parameters;
        }

         public CrewApprasialDetails ApprasialDetails
       {
           get { return _crewAprasialDetails; }
           set { _crewAprasialDetails = value; }
       }

        public SqlParameter[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }
    }
}
