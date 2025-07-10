using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessGetTravelDocumentDetailsinGrid : IBusinessLogic
    {
        private DataSet _resultset;
        private TravelDocumentDetails _documentdetails;
        public ProcessGetTravelDocumentDetailsinGrid()
        {
        }
        public void Invoke()
        {
            TravelDocumentDetailsSelectDataInGrid documentdetailsselectdataingrid = new TravelDocumentDetailsSelectDataInGrid();
            documentdetailsselectdataingrid.DocumentDetails = DocumentDetails;
            ResultSet = documentdetailsselectdataingrid.Get();
            //DocumentDetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
        }
        public TravelDocumentDetails DocumentDetails
        {
            get { return _documentdetails; }
            set { _documentdetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
