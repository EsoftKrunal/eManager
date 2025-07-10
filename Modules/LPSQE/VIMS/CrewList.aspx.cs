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
using System.Text.RegularExpressions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using Ionic.Zip;   

public partial class VIMS_CrewList : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(274, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("UnAuthorized.aspx");
            }
            else
            {
                //btnPrintCompList.Visible = Auth.IsPrint;
            }
            
        }
        //*******************

        if (!Page.IsPostBack)
        {
            BindYear();
            ShowCrewList();
        }
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ShowCrewList();
    }


    public void ShowCrewList()
    {
        string fileter = " where 1=1";
        if (ddlyear.SelectedIndex != 0)
        {
            fileter = "";
        }

        string sql = " select * from PMS_CREW_HISTORY ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptCrewList.DataSource = dt;
        rptCrewList.DataBind();
    }
    public void BindYear()
    {
        ddlyear.Items.Clear();
        ddlyear.Items.Add(new ListItem("All", ""));
        for (int i = DateTime.Now.Year; i > 2012; i--)
        {
            ddlyear.Items.Add(new ListItem(i.ToString()));
        }

    }

}
