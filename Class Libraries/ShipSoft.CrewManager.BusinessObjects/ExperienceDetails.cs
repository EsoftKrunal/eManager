using System;
using System.Collections.Generic;
using System.Text;

namespace ShipSoft.CrewManager.BusinessObjects
{
   public class ExperienceDetails
    {
       private int _experienceid=-1;
       private int _crewid;
       private string _companyname;
       private int _rankId;
       private DateTime _SignOnDate;
       private DateTime _SignOffDate;
       private int Duration;
       private int _SignOffReasonId;
       private string _vesselname;
       private int _vesseltypeid;
       private string _registry;
       private string _dwt;
       private string _gwt;
       private string _bhp;
       private string _bhp1;
       private char _expflag;
       private int _createdby;
       private int _modifiedby;
       
       public ExperienceDetails()
        {

        }
       public int ExperienceId
       {
           get { return _experienceid; }
           set { _experienceid = value; }
       }
       public int CrewId 
        {
            get { return _crewid; }
            set { _crewid = value; }
        }
       public string Companyname
        {
            get { return _companyname; }
            set { _companyname = value; }
        }
       public int RankId
        {
            get { return _rankId; }
            set { _rankId = value; }
        }
       public DateTime SignOnDate
       {
           get { return _SignOnDate; }
           set { _SignOnDate = value; }
       }
       public DateTime SignOffDate
       {
           get { return _SignOffDate; }
           set { _SignOffDate = value; }
       }
       public int SignOffReasonId
       {
           get { return _SignOffReasonId; }
           set { _SignOffReasonId = value; }
       }
       public string Vesselname
       {
           get { return _vesselname; }
           set { _vesselname = value; }
       }
       public int VesseltypeId
       {
           get { return _vesseltypeid; }
           set { _vesseltypeid = value; }
       }
       public string Registry
       {
           get { return _registry; }
           set { _registry = value; }
       }
       public string Dwt
       {
           get { return _dwt; }
           set { _dwt = value; }
       }
       public string Gwt
       {
           get { return _gwt; }
           set { _gwt = value; }
       }
       public string Bhp
       {
           get { return _bhp; }
           set { _bhp = value; }
       }
       public string Bhp1
       {
           get { return _bhp1; }
           set { _bhp1 = value; }
       }
       public char Expflag
       {
           get { return _expflag; }
           set { _expflag = value; }
       }
       public int Createdby
       {
           get { return _createdby; }
           set { _createdby = value; }
       }
       public int Modifiedby
       {
           get { return _modifiedby; }
           set { _modifiedby = value; }
       }
    }
}
