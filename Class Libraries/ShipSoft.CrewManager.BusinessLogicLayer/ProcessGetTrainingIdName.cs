using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using ShipSoft.CrewManager.DataAccessLayer.Select;
namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessGetTrainingIdName : IBusinessLogic 
    {
           private DataSet _result;

        public ProcessGetTrainingIdName()
       {

       }
       
        public void Invoke()
        {
            TrainingSelectData trainingdata = new TrainingSelectData();
            ResultSet = trainingdata.Get();
        }
        public DataSet ResultSet
        {
            get { return _result; }
            set { _result = value; }
        }
    }
}
