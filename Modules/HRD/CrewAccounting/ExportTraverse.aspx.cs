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

public partial class CrewAccounting_ExportTraverse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 35);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //*******************
        if (!Page.IsPostBack)
        {
            bindVesselNameddl();
            Bindgrid();
        }
    }
    private void Bindgrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RefNo");
        dt.Columns.Add("Vendor");
        dt.Columns.Add("InvAmt");
        dt.Columns.Add("InvNo");
        dt.Columns.Add("InvDt");
        dt.Columns.Add("DueDate");
        dt.Columns.Add("PONo");
        dt.Columns.Add("POAmt");
        dt.Columns.Add("VesselCode");
        dt.Columns.Add("ApprovedBy");
        dt.Columns.Add("Status");

             
       

        for (int i = 0; i <= 5; i++)
        {
            dt.Rows.Add(dt.NewRow());
        }
        gvinvoice.DataSource = dt;
        gvinvoice.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {


    }
    protected void bindVesselNameddl()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselCode", Convert.ToInt32(Session["loginid"].ToString()));
        dpvessel.DataSource = ds.Tables[0];
        dpvessel.DataValueField = "VesselId";
        dpvessel.DataTextField = "VesselCode";
        dpvessel.DataBind();
        dpvessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
}
