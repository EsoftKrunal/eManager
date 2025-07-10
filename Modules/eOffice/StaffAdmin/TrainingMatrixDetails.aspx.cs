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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;


public partial class Emtm_TrainingMatrixDetails : System.Web.UI.Page
{
    //Authority Auth;
    public AuthenticationManager Auth;
    public int tid
    {
        get{return Common.CastAsInt32(ViewState["tid"]);}
        set{ViewState["tid"] = value;}
    }
    public int gid
    {
        get { return Common.CastAsInt32(ViewState["gid"]); }
        set { ViewState["gid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        tid = Common.CastAsInt32(Page.Request.QueryString["tid"]);
        gid = Common.CastAsInt32(Page.Request.QueryString["gid"]);

        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 244);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training.aspx");
        }
        if (!IsPostBack)
        {

            lblTrainigName.Text = GetTrainingName();
            lblGroupName.Text = GetGroupName();
            BindOffice();
            BindGridData();


        }
    }

    

    public void BindGridData()
    {
        string sql = " select *,case when (isnull(Validity,0)<=0) then null else DATEADD(mm,Validity,LastDoneDt) end as NextDueDt,OfficeId " +
                     "   from " +
                     "   ( " +
                     "   select empcode,FirstName + ' ' + MiddleName + ' ' + FamilyName as EmpName,e.djc,o.OfficeName,p.PositionName,o.OfficeId,   " +
                     "   (SELECT ValidityPeriod from HR_TrainingMaster t where t.TrainingId=1) as Validity, " +
                     "   (select top 1 enddate from HR_TrainingPlanning et left join HR_TrainingPlanningDetails ed on et.TrainingPlanningId=ed.TrainingPlanningId where et.TrainingId=" + tid + " and ed.EmpId=E.EmpId order by et.enddate desc) as LastDoneDt " +
                     "   from Hr_PersonalDetails e  " +
                     "   inner join position p on e.Position=p.PositionId  " +
                     "   inner join office o on e.Office=o.OfficeId " +
                     "   where p.TrainingPositionGroup=" + gid +
                     "   ) a   ";
                    if (ddlOffice.SelectedIndex != 0)
                        sql= sql+ " where OfficeId = " + ddlOffice.SelectedValue + "";
                    


            //where OfficeId = " + ddlOffice.SelectedValue+"

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptDetails.DataSource = dt;
        rptDetails.DataBind();
    }

    public string GetTrainingName()
    {
        string ret = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select TrainingName from HR_TrainingMaster where trainingID=" + tid + "");
        if (dt.Rows.Count > 0)
        {
            ret = dt.Rows[0][0].ToString();
        }
        return ret;
    }
    public string GetGroupName()
    {
        string ret = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(" select GroupName from HR_TrainingPositionGroup where Groupid=" + gid);
        if (dt.Rows.Count > 0)
        {
            ret = dt.Rows[0][0].ToString();
        }
        return ret;
    }
    public void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(" select * from Office  ");
        ddlOffice.DataSource = dt;
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataValueField = "OfficeID";
        ddlOffice.DataBind();
        ddlOffice.Items.Insert(0,new System.Web.UI.WebControls.ListItem("All",""));
    }



    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridData();
    }


}
    
