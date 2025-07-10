using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
  public class ProcessGetMedicalDocumentType:IBusinessLogic
    {
      private DataSet _result;
      public ProcessGetMedicalDocumentType()
       {
       }
      public void Invoke()
      {
          MedicalDocumentTypeSelectData medicaldocumenttypeselectdata = new MedicalDocumentTypeSelectData();
          ResultSet = medicaldocumenttypeselectdata.Get();
      }
      public DataSet ResultSet
      {
          get { return _result; }
          set { _result = value; }
      }
    }
}
