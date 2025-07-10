using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ChangeHistory : System.Web.UI.Page
{
    AuthenticationManager authRecInv;
   public int BidId
    {
        set { ViewState["BidId"] = value; }
        get { return Common.CastAsInt32(ViewState["BidId"]); }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if(!IsPostBack)
        {
            //-----------------------
            BidId = Common.CastAsInt32(Request.QueryString["BidId"]);
            ShowData();
            //-----------------
        }
    }

    public void ShowData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPID,PRNUM,BIDGROUP FROM DBO.TBLSMDPOMASTERBID B INNER JOIN DBO.TBLSMDPOMASTER P ON B.POID=P.POID WHERE BIDID=" + BidId);
        if(dt.Rows.Count>0)
        {
            lblPONUM.Text ="Change History -  ( " +  dt.Rows[0]["SHIPID"].ToString() + " - " + dt.Rows[0]["PRNUM"].ToString()  + " - " + dt.Rows[0]["BIDGROUP"].ToString() + " )";
            //---------------------------------------
            DataTable dtd = Common.Execute_Procedures_Select_ByQuery("select ROW_NUMBER() over(order by actiontime) as sno,* from BidActionsHistory where BidId =" + BidId);
            rpt.DataSource = dtd;
            rpt.DataBind();
            //---------------------------------------
        }

    }
   
}
