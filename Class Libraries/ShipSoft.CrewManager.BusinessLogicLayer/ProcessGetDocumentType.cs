using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetDocumentType:IBusinessLogic
    {
       private DataSet _result;
       public ProcessGetDocumentType()
       {
       }
       public void Invoke()
       {
           DocumentTypeSelectData documenttypeselectdata = new DocumentTypeSelectData();
           ResultSet = documenttypeselectdata.Get();
       }
       public DataSet ResultSet
       {
           get { return _result; }
           set { _result = value; }
       }
    }
}
