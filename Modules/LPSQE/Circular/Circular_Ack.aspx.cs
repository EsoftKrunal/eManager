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

public partial class Circular_Ack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //------------------------------------
            ProjectCommon.SessionCheck_New();
            //------------------------------------
            int Key = Common.CastAsInt32(Request.QueryString["Key"]);
            //string Mode = Request.QueryString["Mode"];
            lblNumber.Text = Request.QueryString["No"];
            //if (Mode == "LFI")
            //{
                //pnlLFI.Visible = true;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("select *,(select vesselname from dbo.vessel v where v.vesselcode=n.vesselcode) as vesselname from [dbo].[Cir_Vessel_Notifications] n where CId=" + Key + " order by vesselname");
                rptCIR.DataSource = dt;
                rptCIR.DataBind();
            //}
            //else
            //{
            //    pnlFC.Visible = true;
            //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("select *,(select vesselname from dbo.vessel v where v.vesselcode=n.vesselcode) as vesselname from mtmpms.[dbo].[FocusCamp_Vessel_Notifications] n where FocusCampId=" + Key + " order by vesselname");
            //    rptFC.DataSource = dt;
            //    rptFC.DataBind();
            //}
                        
        }
    }
}
