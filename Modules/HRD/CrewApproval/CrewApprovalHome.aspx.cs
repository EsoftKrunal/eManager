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
using System.Xml;

public partial class CrewApproval_CrewApprovalHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            btnDashBoard_Click(sender, e);
        }
	//btnChecklist.Visible=Session["loginid"].ToString()=="1";
    }

    protected void btnDashBoard_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#4371a5");
        //btnApp.ForeColor = System.Drawing.Color.White;

        //Button1.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button1.ForeColor = System.Drawing.Color.Black;

        //Button2.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button2.ForeColor = System.Drawing.Color.Black;

        //Button3.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button3.ForeColor = System.Drawing.Color.Black;

        btnDashBoard.CssClass = "selbtn";
        btnApp.CssClass = "btn1";
        Button1.CssClass = "btn1";
        Button2.CssClass = "btn1";
        Button3.CssClass = "btn1";
        btnChecklist.CssClass = "btn1";


        frm.Attributes.Add("src", "Dashboard.aspx");
    }
    protected void btnApp_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#4371a5");
        //btnApp.ForeColor = System.Drawing.Color.White;

        //Button1.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button1.ForeColor = System.Drawing.Color.Black;

        //Button2.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button2.ForeColor = System.Drawing.Color.Black;

        //Button3.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button3.ForeColor = System.Drawing.Color.Black;

        btnDashBoard.CssClass = "btn1";
        btnApp.CssClass = "selbtn";
        Button1.CssClass = "btn1";
        Button2.CssClass = "btn1";
        Button3.CssClass = "btn1";
        btnChecklist.CssClass = "btn1";


        frm.Attributes.Add("src", "ApplicantApproval.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //btnApp.ForeColor = System.Drawing.Color.Black;

        //Button1.BackColor=System.Drawing.Color.FromName("#4371a5");
        //Button1.ForeColor = System.Drawing.Color.White;

        //Button2.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button2.ForeColor = System.Drawing.Color.Black;

        //Button3.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button3.ForeColor = System.Drawing.Color.Black;
        btnDashBoard.CssClass = "btn1";
        btnApp.CssClass = "btn1";
        Button1.CssClass = "selbtn";
        Button2.CssClass = "btn1";
        Button3.CssClass = "btn1";
        btnChecklist.CssClass = "btn1";

        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CrewApprovalScreen.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //btnApp.ForeColor = System.Drawing.Color.Black;

        //Button1.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button1.ForeColor = System.Drawing.Color.Black;

        //Button2.BackColor = System.Drawing.Color.FromName("#4371a5");
        //Button2.ForeColor = System.Drawing.Color.White;

        //Button3.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button3.ForeColor = System.Drawing.Color.Black;
        btnDashBoard.CssClass = "btn1";
        btnApp.CssClass = "btn1";
        Button1.CssClass = "btn1";
        Button2.CssClass = "selbtn";
        Button3.CssClass = "btn1";
        btnChecklist.CssClass = "btn1";

        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CrewDocuments.aspx");
    }
    protected void btbCrewAssessment_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //btnApp.ForeColor = System.Drawing.Color.Black;

        //Button1.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button1.ForeColor = System.Drawing.Color.Black;

        //Button2.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button2.ForeColor = System.Drawing.Color.Black;

        //Button3.BackColor = System.Drawing.Color.FromName("#4371a5");
        //Button3.ForeColor = System.Drawing.Color.White;
        btnDashBoard.CssClass = "btn1";
        btnApp.CssClass = "btn1";
        Button1.CssClass = "btn1";
        Button2.CssClass = "btn1";
        Button3.CssClass = "selbtn";
        btnChecklist.CssClass = "btn1";

        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CrewAssessment.aspx");
    }
    protected void btnChecklist_Click(object sender, EventArgs e)
    {
        //btnApp.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //btnApp.ForeColor = System.Drawing.Color.Black;

        //Button1.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button1.ForeColor = System.Drawing.Color.Black;

        //Button2.BackColor = System.Drawing.Color.FromName("#c2c2c2");
        //Button2.ForeColor = System.Drawing.Color.Black;

        //Button3.BackColor = System.Drawing.Color.FromName("#4371a5");
        //Button3.ForeColor = System.Drawing.Color.White;
        btnDashBoard.CssClass = "btn1";
        btnApp.CssClass = "btn1";
        Button1.CssClass = "btn1";
        Button2.CssClass = "btn1";
        Button3.CssClass = "btn1";
        btnChecklist.CssClass = "selbtn";


        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CheckList.aspx");
    }
}
