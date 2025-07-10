using System;
using System.Collections.Generic;
using System.Text;

using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.DataAccessLayer.Delete;

namespace ShipSoft.CrewManager.BusinessLogicLayer
{
    public class ProcessDeleteCrewMemberAcademicDetailsById : IBusinessLogic
    {
        private AcademicDetails _AcademicDetails;

        public ProcessDeleteCrewMemberAcademicDetailsById()
        {

        }

        public void Invoke()
        {
            CrewMemberAcademicDetailsDeleteData obj= new CrewMemberAcademicDetailsDeleteData();
            obj.AcademicDetails = this.AcademicDetails;
            obj.Delete();
        }

        public AcademicDetails AcademicDetails
        {
            get { return _AcademicDetails; }
            set { _AcademicDetails = value; }
        }
    }
}
