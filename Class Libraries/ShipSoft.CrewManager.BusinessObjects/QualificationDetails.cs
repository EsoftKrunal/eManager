using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
    public class QualificationDetails
    {
        private int _qualificationid=-1;
        private int _crewid;
        private int _coursenameid;
        private string _passingyear;
        private string _passinggrade;
        private string _subjects;
        private string _imagepath=String.Empty;
        private string _remarks;
        private int _createdby;
        private int _modifiedby;
        
        public QualificationDetails()
        {
        }
        public int QualificationId
        {
            get { return _qualificationid; }
            set { _qualificationid = value; }
        }
        public int CrewId
        {
            get { return _crewid; }
            set { _crewid = value; }
        }
        public int CourseNameId
        {
            get { return _coursenameid;}
            set { _coursenameid = value; }
        }
        public string PassingYear
        {
            get { return _passingyear; }
            set { _passingyear = value; }
        }
        public string PassingGrade
        {
            get { return _passinggrade; }
            set { _passinggrade = value; }
        }
        public string Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }
        public string ImagePath
        {
            get { return _imagepath; }
            set { _imagepath = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        public int CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }
        public int ModifiedBy
        {
            get { return _modifiedby; }
            set { _modifiedby = value; }
        }
    }
}
