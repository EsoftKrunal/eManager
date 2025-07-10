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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class UC_CrewOperation_CreatePortCall : System.Web.UI.UserControl
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        int mode, planid;
        lb_msg.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!(IsPostBack))
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            bindddl_VesselName();
        }
        //-----------------------------------------
        for (int i = 0; i <= gvsearch.Rows.Count - 1; i++)
        {
            HiddenField hfd;
            int j;
            hfd = (HiddenField)gvsearch.Rows[i].FindControl("hfd_OnOff");
            if (hfd != null)
            {
                j = Convert.ToInt32(hfd.Value);
                if (j == 2)
                {
                    gvsearch.Rows[i].BackColor = System.Drawing.Color.FromName("#C9F4DA");
                }
                else
                {
                    gvsearch.Rows[i].BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
    }
    #region PageLoaderControl
    public void bindddl_VesselName()
    {
        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_VesselName.DataValueField = "VesselId";
        this.ddl_VesselName.DataTextField = "VesselName1";
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    private void Load_Port()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.ddl_port.DataValueField = "PortId";
        this.ddl_port.DataTextField = "PortName";
        this.ddl_port.DataSource = dt4;
        this.ddl_port.DataBind();
    }
    #endregion
    protected void ddl_VesselName_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddata(gvsearch.Attributes["MySort"]);
        btn_save.Enabled = true && Auth.isAdd;
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }
    
    private void binddata(String Sort)
    {
        if (ddl_VesselName.SelectedIndex != 0)
        {
            DataSet ds;
            ds =PortPlanner.getCrewDetails(ddl_VesselName.SelectedValue);
             ds.Tables[0].DefaultView.Sort = Sort;
             this.gvsearch.DataSource = ds.Tables[0];
        }
        this.gvsearch.DataBind();
        gvsearch.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        binddata(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {
    }
    protected void gvsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        binddata(gvsearch.Attributes["MySort"]);
    }
    protected void gvsearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hfd;
        int i;
        hfd = (HiddenField)e.Row.FindControl("hfd_OnOff");
        if (hfd != null)
        {
            i = Convert.ToInt32(hfd.Value);
            if (i == 2)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#C9F4DA");
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightYellow;
            }
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        string NewId;
        NewId = "";
        string st = "";
        string rt = "";
        int crewidforbranchid;
        int createdby=Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfd;
        int Hvalue;
        foreach (GridViewRow dg in gvsearch.Rows)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)dg.FindControl("chk_select");
            Label lbchk = (Label)dg.FindControl("Lb_CrewID");
            hfd = (HiddenField)dg.FindControl("hfd_OnOff");
            Hvalue = Convert.ToInt32(hfd.Value);  
            if (chk.Checked == true)
            {
                ////*********** CODE TO CHECK FOR BRANCHID ***********
                //crewidforbranchid = Convert.ToInt32(lbchk.Text.Trim());
                //string xpc = Alerts.Check_BranchId(crewidforbranchid);
                //if (xpc.Trim() != "")
                //{
                //    gvsearch.SelectedIndex = -1;
                //    lb_msg.Text = xpc;
                //    return;
                //}
                ////************
                if (Hvalue == 1) // Sign Off Members
                {
                    if (st == "")
                    {
                        st = lbchk.Text.Trim();
                    }
                    else
                    {
                        st = st + ',' + lbchk.Text.Trim();
                    }
                }
                else // Sign On Members
                {
                    if (rt == "")  
                    {
                        rt = lbchk.Text.Trim();
                    }
                    else
                    {
                        rt = rt + ',' + lbchk.Text.Trim();
                    }
                }
            }
        }
        if (st == "" && rt == "")
        {
            lb_msg.Text = "Please select Crew Member.";
            return;
        }
        try
        {
            PortPlanner.insertdata(Convert.ToInt32(ddl_VesselName.SelectedValue), Convert.ToInt32(ddl_port.SelectedValue), txt_from.Text, txt_to.Text, st, rt,createdby,ref NewId);
            lb_msg.Text = ("Record Successfully Saved.[Port Reference No :" + NewId + ']');
            btn_save.Enabled = false;   
        }
        catch { lb_msg.Text = "Record Can't Saved."; }
    }
}