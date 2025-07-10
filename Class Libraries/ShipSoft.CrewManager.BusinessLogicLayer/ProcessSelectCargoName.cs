using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCargoName : IBusinessLogic
    {
       private DataSet _resultset;

       public ProcessSelectCargoName()
        {

        }

        public void Invoke()
        {
            CargoNameSelectData cargodata = new CargoNameSelectData();
            ResultSet = cargodata.Get();
        }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
