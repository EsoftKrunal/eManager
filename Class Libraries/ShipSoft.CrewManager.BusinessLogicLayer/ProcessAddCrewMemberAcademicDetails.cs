using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using ShipSoft.CrewManager.DataAccessLayer.Insert;
using ShipSoft.CrewManager.BusinessObjects;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessAddCrewMemberAcademicDetails : IBusinessLogic
    {
        private AcademicDetails _AcademicDetails;

        public ProcessAddCrewMemberAcademicDetails()
        {

        }

        public void Invoke()
        {
            CrewMemberAcademicDetailsInsertData obj = new CrewMemberAcademicDetailsInsertData();
            obj.AcademicDetails = this.AcademicDetails;
            obj.Add();
        }

        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }

    }
}
