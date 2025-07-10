using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AlertSignOffAppraisal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        gv_VesselManning.DataSource = ECommon.Execute_Procedures_Select_ByQuery_CMS("select CrewNumber,Firstname+ '' +  MiddleName + ' ' + LastName as FullName,  " +
                                    "(select vesselcode from vessel where vessel.vesselid=cpd.lastvesselid) as Vessel, " +
                                    "(select top 1 ContractReferenceNumber from crewcontractheader cch where cch.crewid=cpd.crewid And Status='A' order by contractid desc) as RefNumber, " +
                                    "convert(Varchar,SignOffDate,101) as SignOffDate " +
                                    "from crewpersonaldetails cpd where cpd.signoffdate is not null and isnull(lastvesselid,0) >0 and not exists " +
                                    "(select top 1 crewid from crewappraisaldetails cad where cad.AppraisalOccasionId=10 and cad.createdon between cpd.signoffdate and dateadd(day,8,cpd.signoffdate)) and cpd.signoffdate between getdate() and dateadd(day,30,getdate())");
        gv_VesselManning.DataBind();
    }
}
