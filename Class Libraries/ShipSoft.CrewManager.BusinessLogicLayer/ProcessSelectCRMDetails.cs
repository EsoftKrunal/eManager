using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCRMDetails : IBusinessLogic
    {
       private DataSet _resultset;
       private CRMDetails _cRMDetails;
       public ProcessSelectCRMDetails()
        {

        }

        public void Invoke()
        {
            CRMDetailsSelectData crmDetailsSelectData = new CRMDetailsSelectData();
            crmDetailsSelectData.CRMDetails = _cRMDetails;
            ResultSet = crmDetailsSelectData.Get();
            if (ResultSet.Tables.Count > 0)
            {
                if (ResultSet.Tables[0].Rows.Count > 0)
                {
                    _cRMDetails.CrewCRMId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewCRMId"].ToString());
                    _cRMDetails.Description = ResultSet.Tables[0].Rows[0]["Description"].ToString();
                    _cRMDetails.ShowInAlert =ResultSet.Tables[0].Rows[0]["ShowInAlert"].ToString();
                    _cRMDetails.AlertExpiryDate =(Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["AlertExpiryDate"]))? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["AlertExpiryDate"].ToString()).ToString("MM/dd/yyyy");
                    _cRMDetails.CRMCategory = Convert.ToChar(ResultSet.Tables[0].Rows[0]["CRMCategory"]);
                }
                else
                {
                    _cRMDetails.Description = "";
                }
            }
        }

       public CRMDetails CRMDetails
       {
           get { return _cRMDetails; }
           set { _cRMDetails = value; }
       }

        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
