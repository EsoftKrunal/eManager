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

public partial class UserControls_LeftMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_vessel_pos_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["VPFileName"].ToString() == "")
        {
            Response.Redirect("VesselManage.aspx?MessMode=3");
        }
        else
        {
            Response.Redirect("VesselPos.aspx");
        }
    }
    protected void btn_CrewList_Click(object sender, ImageClickEventArgs e)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", "SELECT * FROM MW_UserVesselAuthority WHERE UserId='" + Session["UserId"].ToString() + "' AND Module='S'"));
        DataSet ds = Common.Execute_Procedures_Select();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            Response.Redirect("VesselManage.aspx?MessMode=1");
        }
        else
        {
            Response.Redirect("currcrewlist.aspx");
        }
    }
    protected void btn_vet_stat_Click(object sender, ImageClickEventArgs e)
    {
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", "SELECT * FROM MW_UserVesselAuthority WHERE UserId='" + Session["UserId"].ToString() + "' AND Module='V'"));
        DataSet ds = Common.Execute_Procedures_Select();
        if (ds.Tables[0].Rows.Count <= 0)
        {
            Response.Redirect("VesselManage.aspx?MessMode=2");
        }
        else
        {
            Response.Redirect("VettingStatusList.aspx");
        }
    }
}
