using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCrewMemberDangerousCargoDetails : IBusinessLogic
    {
       private CrewCargoDetails _crewCargoDetails;
        private DataSet _resultset;

       public ProcessSelectCrewMemberDangerousCargoDetails()
        {

        }
        public void Invoke()
        {
            CrewMemberDangerousCargoDetailsSelectData obj = new CrewMemberDangerousCargoDetailsSelectData();
            obj.CrewCargoDetails = _crewCargoDetails;
            ResultSet = obj.Get();
            if (ResultSet.Tables.Count > 0)
            {
                if (ResultSet.Tables[0].Rows.Count > 0)
                {
                    _crewCargoDetails.DangerousCargoId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["DangerousCargoId"].ToString());
                    _crewCargoDetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
                    _crewCargoDetails.CargoId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CargoId"].ToString());
                    _crewCargoDetails.Number = ResultSet.Tables[0].Rows[0]["Number"].ToString();
                    _crewCargoDetails.NationalityId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["NationalityId"].ToString());
                    _crewCargoDetails.GradeLevel = ResultSet.Tables[0].Rows[0]["GradeLevel"].ToString();
                    _crewCargoDetails.PlaceOfIssue = ResultSet.Tables[0].Rows[0]["PlaceOfIssue"].ToString();
                    _crewCargoDetails.DateOfIssue = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["DateOfIssue"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["DateOfIssue"].ToString()).ToString("MM/dd/yyyy");
                    _crewCargoDetails.ExpiryDate = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["ExpiryDate"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["ExpiryDate"].ToString()).ToString("MM/dd/yyyy");
                    _crewCargoDetails.ImagePath = ResultSet.Tables[0].Rows[0]["ImagePath"].ToString();
                }
            }
        }
       public CrewCargoDetails CrewCargoDetails
        {
            get { return _crewCargoDetails; }
            set { _crewCargoDetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
