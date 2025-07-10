using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessSelectCrewMemberPersonalDetailsById : IBusinessLogic
    {
        private CrewMember _Member;
        private DataSet _resultset;

        public ProcessSelectCrewMemberPersonalDetailsById()
        {

        }
        public void Invoke()
        {
            CrewMemberPersonalDetailsSelectDataById selectcrewmember = new CrewMemberPersonalDetailsSelectDataById();
            selectcrewmember.CrewMember = _Member;
            ResultSet = selectcrewmember.Get();
            if (ResultSet.Tables.Count > 0)
            {
                if (ResultSet.Tables[0].Rows.Count > 0)
                {
                    _Member.EmpNo = ResultSet.Tables[0].Rows[0]["CrewNumber"].ToString();
                    _Member.FirstName = ResultSet.Tables[0].Rows[0]["FirstName"].ToString();
                    _Member.MiddleName = ResultSet.Tables[0].Rows[0]["MiddleName"].ToString();
                    _Member.LastName = ResultSet.Tables[0].Rows[0]["LastName"].ToString();
                    if(Convert.IsDBNull(ResultSet.Tables[0].Rows[0]["DateOfBirth"]))
                    {
                        _Member.DOB = Convert.ToDateTime("01/01/1900");
                    }
                    else
                    {
                        _Member.DOB = Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["DateOfBirth"].ToString());
                    }                   
                    _Member.PlaceOfBirth = ResultSet.Tables[0].Rows[0]["PlaceOfBirth"].ToString();
                    _Member.Age = ResultSet.Tables[0].Rows[0]["Age"].ToString();
                    _Member.Bmi = ResultSet.Tables[0].Rows[0]["Bmi"].ToString();
                    _Member.Nationalty = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["NationalityId"].ToString());
                    _Member.SexType = (SexType)Convert.ToInt32(ResultSet.Tables[0].Rows[0]["SexType"].ToString());

                    _Member.Countryofbirth = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["Countryofbirth"].ToString());
                    _Member.Maritalstatusid =(MaritalStatus)Convert.ToInt32(ResultSet.Tables[0].Rows[0]["Maritalstatusid"].ToString());
                    _Member.Datefirstjoin = Convert.ToDateTime(ResultSet.Tables[0].Rows[0]["Datefirstjoin"].ToString());
                    _Member.Rankappliedid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["Rankappliedid"].ToString());

                    _Member.Currentrank = ResultSet.Tables[0].Rows[0]["CurrentRank"].ToString();

                    _Member.Qualification = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["QualificationId"].ToString());
                    _Member.ShirtSize =(ShirtSize)Convert.ToInt32(ResultSet.Tables[0].Rows[0]["ShirtSizeId"].ToString());
                    _Member.LastVessel = ResultSet.Tables[0].Rows[0]["LastVessel"].ToString();

                    _Member.Recruitmentofficeid = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["Recruitmentofficeid"].ToString());
                    _Member.Height = ResultSet.Tables[0].Rows[0]["Height"].ToString();
                    _Member.Weight = ResultSet.Tables[0].Rows[0]["Weight"].ToString();
                    _Member.Waist = ResultSet.Tables[0].Rows[0]["Waist"].ToString();
                    _Member.ShoeSize = (ShoeSize) Convert.ToInt32(ResultSet.Tables[0].Rows[0]["ShoeSizeId"].ToString());
                    _Member.Photopath = ResultSet.Tables[0].Rows[0]["PhotoPath"].ToString();
                    _Member.CrewStatus = ResultSet.Tables[0].Rows[0]["CrewStatus"].ToString();
                    _Member.BloodGroup = Convert.ToInt16(ResultSet.Tables[0].Rows[0]["BloodGroupId"].ToString());
                    _Member.PassportNo = ResultSet.Tables[0].Rows[0]["passportno"].ToString();
                    _Member.Status = ResultSet.Tables[0].Rows[0]["status"].ToString();
                    _Member.RankExp = ResultSet.Tables[0].Rows[0]["RankExp"].ToString();
                    _Member.CurrentVessel = ResultSet.Tables[0].Rows[0]["CurrentVessel"].ToString();
                }
            }
        }
        public CrewMember CrueMember
        {
            get { return _Member; }
            set { _Member = value; }
        }
        private DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
