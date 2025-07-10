using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;


namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessGetRankApplied : IBusinessLogic
    {
       private DataSet _result;

        public ProcessGetRankApplied()
    {
 
    }
        public void Invoke()
        {
            RankAppliedSelectData rankappliedselectData = new RankAppliedSelectData();
            ResultSet = rankappliedselectData.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
