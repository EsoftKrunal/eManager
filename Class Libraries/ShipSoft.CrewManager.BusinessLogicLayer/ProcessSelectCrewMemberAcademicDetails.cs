using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewMemberAcademicDetails: IBusinessLogic
    {
        private AcademicDetails _AcademicDetails;
        private DataSet _resultset;

        public ProcessSelectCrewMemberAcademicDetails()
        {

        }
        public void Invoke()
        {
            CrewMemberAcademicDetailsSelectData obj = new CrewMemberAcademicDetailsSelectData();
            obj.AcademicDetails = _AcademicDetails;
            ResultSet = obj.Get();
            if (ResultSet.Tables.Count > 0)
            {
                if (ResultSet.Tables[0].Rows.Count > 0)
                {
                    _AcademicDetails.AcademicDetailsId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["AcademicDetailsId"].ToString());
                    _AcademicDetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
                    _AcademicDetails.TypeOfCertificate = ResultSet.Tables[0].Rows[0]["TypeOfCertificate"].ToString();
                    _AcademicDetails.Institute = ResultSet.Tables[0].Rows[0]["Institute"].ToString();
                    _AcademicDetails.DurationForm =(Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["DurationFrom"]))? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["DurationFrom"].ToString()).ToString("MM/dd/yyyy");
                    _AcademicDetails.DurationTo = (Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["DurationTo"])) ? "" : Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["DurationTo"].ToString()).ToString("MM/dd/yyyy");
                    _AcademicDetails.Grade = ResultSet.Tables[0].Rows[0]["Grade"].ToString();
                    _AcademicDetails.ImagePath = ResultSet.Tables[0].Rows[0]["ImagePath"].ToString();
                }
            }
        }
        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
