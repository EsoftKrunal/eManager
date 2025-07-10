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

public partial class Reporting_CrewsAddressReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 107);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (!Page.IsPostBack)
        {
           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string Checklist="";
        if (CheckBoxList1.SelectedIndex < 0)
        {
            Label1.Text = "Select at least one option.";
        }
        else
        {
            Label1.Text = "";
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected==true)
                {
                    if (Checklist == "")
                    {
                        Checklist = CheckBoxList1.Items[i].Value;
                    }
                    else
                    {
                        Checklist = Checklist + "," + CheckBoxList1.Items[i].Value;
                    }
                }
            }
            string chname = "";
            for (int c = 0; c < this.CheckBoxList1.Items.Count; c++)
            {
                if (this.CheckBoxList1.Items[c].Selected == true)
                {
                    if (chname == "")
                    {
                        chname = this.CheckBoxList1.Items[c].Text;
                    }
                    else
                    {
                        chname = chname + "," + this.CheckBoxList1.Items[c].Text;
                    }
                }
            }
            
            IFRAME1.Attributes.Add("src", "CreaAddressReportHeader.aspx?Checklist1=" + Checklist + "&ChName=" + chname);
        }
    }
}
