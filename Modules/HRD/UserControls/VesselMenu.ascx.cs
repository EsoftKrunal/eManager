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

public partial class VesselMenu : System.Web.UI.UserControl
{
    private void BindMgmtTypeDropDown()
    {
        DataTable dt2 = VesselDetailsGeneral.selectDataMgmtType();
        this.ddlMgmtType.DataValueField = "ManagementTypeId";
        this.ddlMgmtType.DataTextField = "ManagementTypeName";
        this.ddlMgmtType.DataSource = dt2;
        this.ddlMgmtType.DataBind();
        ddlMgmtType.SelectedIndex = int.Parse(Session["LastMgmt"].ToString());  
        ddlMgmtType.Items[0].Text = "< Mgmt. Type >";  
    }
    private void BindOwnerDropDown()
    {
        DataTable dt3 = VesselDetailsGeneral.selectDataOwnerName();
        this.ddlOwner.DataValueField = "OwnerId";
        this.ddlOwner.DataTextField = "OwnerShortName";
        this.ddlOwner.DataSource = dt3;
        this.ddlOwner.DataBind();
        ddlOwner.SelectedIndex = int.Parse(Session["LastOwner"].ToString());  
        ddlOwner.Items[0].Text = "< Owner >";
    }
    private void BindStatusDropDown()
    {
        DataTable dt9 = VesselDetailsGeneral.selectDataVesselStatus();
        this.ddlVesselStatus.DataValueField = "VesselStatusId";
        this.ddlVesselStatus.DataTextField = "VesselStatusName";
        this.ddlVesselStatus.DataSource = dt9;
        this.ddlVesselStatus.DataBind();
        ddlVesselStatus.SelectedIndex = int.Parse(Session["LastStatus"].ToString());
        ddlVesselStatus.Items[0].Text = "< Status >";
    }
    protected void DDlIndexChanged(object sender,EventArgs e) 
    {
        bindVesselGrid();
    }
    public void bindVesselGrid()
    {
        int _VesselType, _VesselStatus, _MgmtType, _OwnerId, _OwnerPoolId;
        _VesselType = 0;
        _VesselStatus = Convert.ToInt32(ddlVesselStatus.SelectedValue);
        _MgmtType = Convert.ToInt32(ddlMgmtType.SelectedValue);
        _OwnerId = Convert.ToInt32(ddlOwner.SelectedValue);
        _OwnerPoolId = 0;

        bool editallow=new AuthenticationManager(43, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsUpdate;
        btn_Add_Vessel.Visible = new AuthenticationManager(43, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsAdd;
        
        Session["LastMgmt"] = ddlMgmtType.SelectedIndex;
        Session["LastOwner"] = ddlOwner.SelectedIndex;
        Session["LastStatus"] = ddlVesselStatus.SelectedIndex; 
        int LoginId=Convert.ToInt32(Session["loginid"].ToString());
        if (LoginId==1)
            LoginId=0 ; 

        DataTable dt2 = VesselSearch.selectDataVesselDetails(LoginId, _VesselType, _VesselStatus, _MgmtType, _OwnerId, _OwnerPoolId);
        DataView dv = dt2.DefaultView;
        dv.Sort="VesselFullName ASC";
        DataTable dt = dv.ToTable();
        dt.Columns.Add("IsEdit");
        foreach (DataRow dr in dt.Rows)
        {
            dr["IsEdit"]=(editallow)?"":"none";
        }
        this.rptVessel.DataSource = dt;
        this.rptVessel.DataBind();
   
    }
    protected void Page_Load( object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());
        if (!IsPostBack)
        {
            if(Session["LastMgmt"] ==null) 
                Session["LastMgmt"]="0";
            if(Session["LastOwner"] ==null) 
                Session["LastOwner"] = "0";
            if (Session["LastStatus"] == null)
                Session["LastStatus"] = "1";
            BindMgmtTypeDropDown();
            BindOwnerDropDown();
            BindStatusDropDown();
            bindVesselGrid();
        }
    }
    protected void VesselView(object sender, EventArgs e)
    {
        Session["VMode"] = "View";
        Session["VesselId"] = ((LinkButton)sender).CommandArgument;
        Session["VesselName"] = ((LinkButton)sender).Text;
        Response.Redirect("VesselDetails.aspx");
    }
    protected void VesselEdit(object sender, EventArgs e)
    {
        Session["VMode"] = "Edit";
        Session["VesselId"] = ((ImageButton)sender).CommandArgument;
        Session["VesselName"] = ((ImageButton)sender).ToolTip;
        Response.Redirect("VesselDetails.aspx");
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Session["VesselId"] = "";
        Session["VMode"] = "New";
        Session["VesselName"] = "";
        Response.Redirect("VesselDetails.aspx?Mode=New");
    }
    protected void btn_Rename_Click(object sender, EventArgs e)
    {
        Response.Redirect("VesselRename.aspx");
    }
}
