using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;

public partial class Regulation_ACK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            int Key = Common.CastAsInt32(Request.QueryString["Key"]);            
            lblNumber.Text = Request.QueryString["No"];

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select *,(select vesselname from dbo.vessel v where v.vesselcode=n.vesselcode) as vesselname from [dbo].[Reg_Vessel_Notifications] n where [RegId]=" + Key + " order by vesselname");
            rptRegulation.DataSource = dt;
            rptRegulation.DataBind();
                        
        }
    }
}
