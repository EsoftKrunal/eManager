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

public partial class Reporting_FamilyMembersReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 120);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!Page.IsPostBack)
        {
            BindCrewStatusDropDown();
            ddCountry.DataSource = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
            ddCountry.DataTextField = "CountryName";
            ddCountry.DataValueField = "CountryId";
            ddCountry.DataBind();
            ddCountry.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        //else
        //{
        //    Button1_Click(sender, e);
        //}
    }
    private void BindCrewStatusDropDown()
    {
        ddCrewStatus.DataSource = Budget.getTable("select crewstatusid,crewstatusname from crewstatus");
        ddCrewStatus.DataTextField = "CrewStatusName";
        ddCrewStatus.DataValueField = "CrewStatusId";
        ddCrewStatus.DataBind();
        ddCrewStatus.Items.Insert(0, new ListItem(" All ", "0"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txt_Exp_From.Text != "" && txt_Exp_To.Text != "")
        {
            if (Convert.ToInt32(txt_Exp_From.Text) > Convert.ToInt32(txt_Exp_To.Text))
            {
                this.lblMessage.Text = "From Age Should be less than To Age.";
                IFRAME1.Attributes.Add("src", "");
            }
            else
            {
                this.lblMessage.Text = "";
                int agefrom = 0, ageto = 99, relation = 0, gender = 0, country = 0;
                string areacode = "";
                char nok = 'N';
                if (txt_Exp_From.Text != "")
                {
                    agefrom = Convert.ToInt32(txt_Exp_From.Text);
                }
                if (txt_Exp_To.Text != "")
                {
                    ageto = Convert.ToInt32(txt_Exp_To.Text);
                }
                relation = Convert.ToInt32(ddl_Relation.SelectedValue);
                gender = Convert.ToInt32(ddGender.SelectedValue);
                country = Convert.ToInt32(ddCountry.SelectedValue);
                areacode = txt_P_Area_Code_Tel.Text;
                if (CheckBox1.Checked == true)
                {
                    nok = 'Y';
                }
                else
                {
                    nok = 'N';
                }
                IFRAME1.Attributes.Add("src", "FamilyMembersReportCrystal.aspx?AgeFrom=" + agefrom + "&AgeTo=" + ageto + "&Relation=" + relation + "&Gender=" + gender + "&Country=" + country + "&AreaCode=" + areacode + "&NOK=" + nok + "&Status=" + ddCrewStatus.SelectedValue);
            }
        }
        else
        {
            this.lblMessage.Text = "";
            int agefrom = 0, ageto = 99, relation = 0, gender = 0, country = 0;
            string areacode = "";
            char nok = 'N';
            if (txt_Exp_From.Text != "")
            {
                agefrom = Convert.ToInt32(txt_Exp_From.Text);
            }
            if (txt_Exp_To.Text != "")
            {
                ageto = Convert.ToInt32(txt_Exp_To.Text);
            }
            relation = Convert.ToInt32(ddl_Relation.SelectedValue);
            gender = Convert.ToInt32(ddGender.SelectedValue);
            country = Convert.ToInt32(ddCountry.SelectedValue);
            areacode = txt_P_Area_Code_Tel.Text;
            if (CheckBox1.Checked == true)
            {
                nok = 'Y';
            }
            else
            {
                nok = 'N';
            }
            IFRAME1.Attributes.Add("src", "FamilyMembersReportCrystal.aspx?AgeFrom=" + agefrom + "&AgeTo=" + ageto + "&Relation=" + relation + "&Gender=" + gender + "&Country=" + country + "&AreaCode=" + areacode + "&NOK=" + nok + "&Status=" + ddCrewStatus.SelectedValue);
            
        }
    }
}
