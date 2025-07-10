using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Procedures_ProceduresHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnSMS.Visible = new AuthenticationManager(6, UserId, ObjectType.Module).IsView;
        btnSMSAdmin.Visible = new AuthenticationManager(6, UserId, ObjectType.Module).IsView;

        btnSMS.CssClass = "btn1";
        btnSMSAdmin.CssClass = "btn1";
        btn_Publication.CssClass = "btn1";
        btn_SMSReview.CssClass = "btn1";
        if (!IsPostBack)
        {
            string Main = Convert.ToString(Session["MM1"]).Trim();
            if (Main == "M")
            {
                btnSMSAdmin.CssClass = "selbtn";
            }
            else if (Main == "S")
            {
                btnSMS.CssClass = "selbtn";
            }
            else if (Main == "P")
            {
                btn_Publication.CssClass = "selbtn";
            }
            else if (Main == "R")
            {
                btn_SMSReview.CssClass = "selbtn";
            }
        }
       
    }
    protected void btnSMSAdmin_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "M";
        Session["MS1"] = "M";

        Session["MM"] = "";
        Session["MS"] = "";
        btnSMS.CssClass = "btn1";
        btn_Publication.CssClass = "btn1";
        btn_SMSReview.CssClass = "btn1";
        btnSMSAdmin.CssClass = "selbtn";
        //if (Common.CastAsInt32(Session["loginid"]) == 19)
        //{
        //    //Response.Redirect("Approval.aspx");
        //    frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/Approval.aspx");
        //}
        //else
        //{
            frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMSAdminHome.aspx");
            //Response.Redirect("ViewManualHeadings.aspx");
       // }
    }
    protected void btnSMS_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "S";
        Session["MS1"] = "M";

        Session["MM"] = "";
        Session["MS"] = "";
        btnSMS.CssClass = "selbtn";
        btn_Publication.CssClass = "btn1";
        btn_SMSReview.CssClass = "btn1";
        btnSMSAdmin.CssClass = "btn1";
        //Response.Redirect("ProcedureSubMenu.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/ProcedureSubMenu.aspx");
    }
    protected void btn_Publication_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "P";
        Session["MS1"] = "";

        Session["MM"] = "C";
        Session["MS"] = "";
        btnSMS.CssClass = "btn1";
        btn_Publication.CssClass = "selbtn";
        btn_SMSReview.CssClass = "btn1";
        btnSMSAdmin.CssClass = "btn1";
        //Response.Redirect("PB_PublicationCommunication.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/PB_PublicationCommunicationHome.aspx");
    }
    protected void btn_SMSReview_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "R";
        Session["MS1"] = "";

        Session["MM"] = "";
        Session["MS"] = "";
        btnSMS.CssClass = "btn1";
        btn_Publication.CssClass = "btn1";
        btn_SMSReview.CssClass = "selbtn";
        btnSMSAdmin.CssClass = "btn1";
        //Response.Redirect("SMSReview.aspx");
        frm.Attributes.Add("src", "~/Modules/LPSQE/Procedures/SMSReview.aspx");
    }
}