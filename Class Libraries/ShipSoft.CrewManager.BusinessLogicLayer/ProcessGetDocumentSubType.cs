using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessGetDocumentSubType : IBusinessLogic
    {
        private DataSet _resultset;
        private DocumentDetails _documentdetails;
        public ProcessGetDocumentSubType()
        {
        }
        public void Invoke()
        {
            DocumentSubTypeSelectData documentsubtypeselectdata = new DocumentSubTypeSelectData();
            //documentsubtypeselectdata.DocumentDetails = DocumentDetails;
            ResultSet = documentsubtypeselectdata.Get();
            DocumentDetails.DocumentTypeId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DocumentTypeId"].ToString());
        }
        public DocumentDetails DocumentDetails
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
