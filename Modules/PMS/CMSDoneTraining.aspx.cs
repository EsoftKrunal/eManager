using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
//using Newtonsoft.Json;
using System.IO;
using Ionic.Zip;

public partial class CMSDoneTraining : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return Convert.ToString (Session["CurrentShip"]); }
    }
    public int CrewID
    {
        get { return Common.CastAsInt32(ViewState["_CrewID"]); }
        set { ViewState["_CrewID"]=value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        //-----------------------------
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "eee", "SetCalender();", true);
        //-----------------------------
        if (!IsPostBack)
        {
            CrewID = Common.CastAsInt32(Page.Request.QueryString["CrewID"]);
            BindCrewList();
        }
    }
    public void BindCrewList()
    {
        string sql = "select distinct crewid,crewnumber,crewname from(select h.CrewNumber, h.CrewName, c.* from PMS_CREW_TRAININGCOMPLETED c inner join PMS_CREW_HISTORY h on c.crewid= h.crewid) d where d.Crewid="+ CrewID + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptcrew.DataSource = dt;
        rptcrew.DataBind();
    }

    public DataTable BindTrainings(object crewid)
    {

        string sqlTrng = " select  ROW_NUMBER()over(order by N_DueDate)SNO,case when isnull(AttachmentFileName,'')='' then 0 else 1 end hasFile,* from PMS_CREW_TRAININGCOMPLETED Where VesselCode='" + VesselCode + "'and CrewID=" + crewid;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqlTrng);
        return dt;
    }
   
    //--------------------------------------------------------------------
    public void btndownload_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int TrainingRequirementId = Common.CastAsInt32(btn.CommandArgument);

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select Attachment,AttachmentFileName from dbo.PMS_CREW_TRAININGCOMPLETED where TrainingRequirementId=" + TrainingRequirementId + "");
        if (dt1.Rows.Count > 0)
        {
            byte[] fileBytes = (byte[])dt1.Rows[0][0];
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + dt1.Rows[0][1]);
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
            
        }
    }
    

}