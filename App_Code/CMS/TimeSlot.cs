using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for TimeSlot
/// </summary>
public class TimeSlot
{
    public DateTime Date;
    public double FromTime;
    public double ToTime;
    public bool FulldayWork;
    public double Duration;

    //-----------------
	public TimeSlot()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public TimeSlot(DateTime _Date, double _FromTime, double _ToTime)
    {
        Date = _Date;
        FromTime=_FromTime;  
        ToTime=_ToTime;
        Duration = ToTime - FromTime + 0.5;
        FulldayWork = false;   
    }
    public TimeSlot(DateTime _Date, double _FromTime, double _ToTime,bool _FullDayWork)
    {
        Date = _Date;
        FromTime = _FromTime;  
        ToTime = _ToTime;
        Duration = 0;
        FulldayWork = _FullDayWork;
    }
}
