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

public partial class UserControls_SMSManualMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

       // btnSMS.Visible = new AuthenticationManager(6, UserId, ObjectType.Module).IsView;
        btnSMSAdmin.Visible = new AuthenticationManager(6, UserId, ObjectType.Module).IsView;

      //  btnSMS.CssClass = "btn1";
        btnSMSAdmin.CssClass = "btn1";
        btnAPPROVAL.CssClass = "btn1";
        btnComm.CssClass = "btn1";
        string Main = Convert.ToString(Session["MM1"]).Trim();
        if (Main == "M")
        {
           btnSMSAdmin.CssClass = "selbtn";
        }
        //else if (Main == "S")
        //{
        //    btnSMS.CssClass = "selbtn";
        //}
        else if (Main == "P")
        {
            btn_Publication.CssClass = "selbtn";
        }
        else if (Main == "R")
        {
            btn_SMSReview.CssClass = "selbtn";
        }
        else if (Main == "A")
        {
            btnAPPROVAL.CssClass = "selbtn";
        }
        else if (Main == "C")
        {
            btnComm.CssClass = "selbtn";
        }
    }
    protected void btnSMSAdmin_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "M";
        Session["MS1"] = "M";

        Session["MM"] = "";
        Session["MS"] = "";

        //if (Common.CastAsInt32(Session["loginid"]) == 19)
        //{
        //    Response.Redirect("Approval.aspx");
        //}
        //else
        //{
            Response.Redirect("ViewManualHeadings.aspx");
        //}
    }
    //protected void btnSMS_Click(object sender, EventArgs e)
    //{
    //    Session["MM1"] = "S";
    //    Session["MS1"] = "M";

    //    Session["MM"] = "";
    //    Session["MS"] = "";
    //    Response.Redirect("ReadManuals.aspx");
    //}
    protected void btn_Publication_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "P";
        Session["MS1"] = "";

        Session["MM"] = "C";
        Session["MS"] = "";
        Response.Redirect("PB_PublicationCommunication.aspx");
    }
    protected void btn_SMSReview_Click(object sender, EventArgs e)
    {
        Session["MM1"] = "R";
        Session["MS1"] = "";

        Session["MM"] = "";
        Session["MS"] = "";
        Response.Redirect("SMSReview.aspx");
    }

    protected void btnAPPROVAL_OnClick(object sender, EventArgs e)
    {
        Session["MM1"] = "A";
        Session["MS1"] = "M";

        Session["MM"] = "";
        Session["MS"] = "";
        Response.Redirect("Approval.aspx");
    }
    protected void btnComm_OnClick(object sender, EventArgs e)
    {
        Session["MM1"] = "C";
        Session["MS1"] = "M";

        Session["MM"] = "";
        Session["MS"] = "";
        Response.Redirect("Communication.aspx");
    }

}
