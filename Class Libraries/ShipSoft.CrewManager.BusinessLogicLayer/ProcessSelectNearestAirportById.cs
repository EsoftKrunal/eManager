using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectNearestAirportById:IBusinessLogic
    {
       private int _CountryId;
       private DataSet _result;
       public ProcessSelectNearestAirportById()
       {
       }
       public void Invoke()
       {
           NearestAirportSelectDataById databyid = new NearestAirportSelectDataById();
           databyid.CountryId = CountryId;
           ResultSet = databyid.Get();
       }
       public int CountryId
       {
           get { return _CountryId; }
           set { _CountryId = value; }
       }
       public DataSet ResultSet
       {
           get { return _result; }
           set { _result = value; }
       }
    }
}
