using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ShipSoft.CrewManager.DataAccessLayer.Select;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
   public class ProcessSelectCrewMemberQualificationDetails:IBusinessLogic
    {
       private DataSet _resultset;
       private QualificationDetails _qualificationdetails;

       public ProcessSelectCrewMemberQualificationDetails()
        {
        }
        public void Invoke()
        {
            CrewMemberQualificationDetailsSelectData qualificationselectdata = new CrewMemberQualificationDetailsSelectData();
            qualificationselectdata.QualificationDetails = QualificationDetails;
            ResultSet = qualificationselectdata.Get();
            QualificationDetails.CrewId = Convert.ToInt32(ResultSet.Tables[0].Rows[0]["CrewId"].ToString());
        }
       public QualificationDetails QualificationDetails
       {
           get { return _qualificationdetails; }
           set { _qualificationdetails = value; }
       }
        public DataSet ResultSet
        {
            get { return _resultset; }
            set { _resultset = value; }
        }
    }
}
