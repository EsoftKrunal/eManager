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

public partial class InspReportCat : System.Web.UI.Page
{
    Authority Auth;
    string Mode = "New";
    public int Edit_SrNO
    {
        get{ return Common.CastAsInt32(ViewState["Edit_SrNO"]);}
        set {ViewState["Edit_SrNO"]=value;} 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 152);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 70);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");
        //}
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        lblMessege.Text = "";
        lblMSGPopUp.Text = "";
        if (!Page.IsPostBack)
        {
            Edit_SrNO = 0;
            BindInspections();
        }
    }
    protected void BindInspections()
    {
        ddlInspection.DataSource =Common.Execute_Procedures_Select_ByQuery("select * from m_inspection where inspectiongroup in ( select m.id from m_inspectiongroup m where ID=3 ) order by NAME");
        ddlInspection.DataTextField = "NAME";
        ddlInspection.DataValueField= "ID";
        ddlInspection.DataBind();
        ddlInspection.Items.Insert(0, new ListItem("< SELECT >", " "));
    }

    protected void ddlInspection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();
    }

    // --------  events
    protected void btn_Save_InspGrp_Click(object sender, EventArgs e)
    {
        //-------------------
        int Srno = Common.CastAsInt32(txtSrno.Text.Trim());
        if (Srno <= 0)
        {
            lblMSGPopUp.Text = "Please enter Sr#.";
            return;
        }
        //-------------------
        if (txtMainHeading.Text.Trim() == "")
        {
            lblMSGPopUp.Text = "Please enter KPI Head.";
            return;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM m_InspectionReportCategory where Insid=" + ddlInspection.SelectedValue + " And Sno=" + Srno.ToString());
        if (dt.Rows.Count > 0)
        {
            if (Edit_SrNO != Common.CastAsInt32(dt.Rows[0]["Sno"].ToString()))
            {
                lblMSGPopUp.Text = "Sr# already exists.";
                return;
            }
        }

        if(Edit_SrNO <=0)
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO m_InspectionReportCategory(INSID,SNO,MAINHEADING,SUBHEADING) VALUES(" + ddlInspection.SelectedValue + "," + Srno + ",'" + txtMainHeading.Text  + "','" + txtSubHeading.Text + "')");
        else
            Common.Execute_Procedures_Select_ByQuery("UPDATE m_InspectionReportCategory SET MAINHEADING='" + txtMainHeading.Text + "',SUBHEADING='" + txtSubHeading.Text + "' WHERE INSID=" + ddlInspection.SelectedValue + " AND Sno=" + Srno);
        
        DivAddNewHead.Visible = false;
        BindList();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((ImageButton)(sender)).CommandArgument);
        Edit_SrNO=Id;
        txtSrno.Enabled = false; 
        DivAddNewHead.Visible = true;
        ShowRecord();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((ImageButton)(sender)).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM m_InspectionReportCategory where Insid=" + ddlInspection.SelectedValue + " And Sno=" + Id.ToString());
        BindList();
    }
    protected void btnAddNewHead_Click(object sender, EventArgs e)
    {
        if (ddlInspection.SelectedIndex > 0)
        {
            Edit_SrNO = 0;
            txtSrno.Enabled = true;

            txtSrno.Text = "";
            txtMainHeading.Text = "";
            txtSubHeading.Text = "";

            DivAddNewHead.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "test", "alert('Please select inspection.');", true);
        }
    }
    protected void btnCloseAddHeadPopup_Click(object sender, EventArgs e)
    {
        DivAddNewHead.Visible = false;
    }
    // --------  Finction
    public void BindList()
    {
        if (ddlInspection.SelectedIndex > 0)
        {
            string sql = "SELECT * FROM dbo.m_InspectionReportCategory WHERE INSID=" + ddlInspection.SelectedValue + " ORDER BY SNO";
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                this.rptCategories.DataSource = Dt;
                this.rptCategories.DataBind();
            }
            else
            {
                this.rptCategories.DataSource = null;
                this.rptCategories.DataBind();
            }
        }
        else
        {
            this.rptCategories.DataSource = null;
            this.rptCategories.DataBind();
        }
    }
    public void ShowRecord()
    {
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM m_InspectionReportCategory WHERE INSID=" + ddlInspection.SelectedValue + " AND SNO="+ Edit_SrNO.ToString());
        if (Dt.Rows.Count > 0)
        {
            txtSrno.Text = Dt.Rows[0]["Sno"].ToString();
            txtMainHeading.Text = Dt.Rows[0]["MainHeading"].ToString();
            txtSubHeading.Text = Dt.Rows[0]["SubHeading"].ToString();
        }
    }
   
}
