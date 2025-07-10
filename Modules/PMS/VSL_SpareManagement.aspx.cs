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

public partial class VSL_SpareManagement : System.Web.UI.Page
{
    AuthenticationManager Auth;
    bool setScroll = true;

    public int SpareID
    {
        get { return Common.CastAsInt32(ViewState["SpareID"]); }
        set { ViewState["SpareID"] = value; }
    }
    public int ComponentId
    {
        get { return Common.CastAsInt32(ViewState["ComponentId"]); }
        set { ViewState["ComponentId"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString() ; }
        set { ViewState["VesselCode"] = value; }
    }
    public string Office_Ship
    {
        get { return ViewState["Office_Ship"].ToString(); }
        set { ViewState["Office_Ship"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        lblMsgCritical.Text = "";
        
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 16;
            VesselCode = Session["CurrentShip"].ToString();
            ddlSpareStatus.SelectedValue = "A";
            //btnSearch_Click(sender, e);
        }
    }
  
    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblRecordCount.Text = "( " + rptDefects.Items.Count + " ) records found.";   
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('rptDefects');", true);
    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    //public void BindFleet()
    //{
    //    try
    //    {
    //        DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
    //        this.ddlFleet.DataSource = dtFleet;
    //        this.ddlFleet.DataValueField = "FleetId";
    //        this.ddlFleet.DataTextField = "Name";
    //        this.ddlFleet.DataBind();
    //        ddlFleet.Items.Insert(0, new ListItem("< ALL Fleet >", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}    
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSearch = " SELECT VSM.*,CM.CriticalType,CM.ComponentCode,CM.ComponentName,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN(CM.ComponentCode)-3) AND   " +
                           " LEFT(ComponentCode,(LEN(CM.ComponentCode)-3)) = LEFT(CM.ComponentCode,(LEN(CM.ComponentCode)-3)))As LinkedTo, " +
                           " dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB, "+
                           " (SELECT TOP 1 StockLocation FROM VSL_StockInventory VSI  "+
                           " WHERE VSI.VesselCode = VSM.VesselCode AND VSI.ComponentId = VSM.ComponentId AND VSI.Office_Ship = VSM.Office_Ship AND VSI.SpareId = VSM.SpareId  "+
                           " ORDER BY VSI.UpdatedOn DESC ) AS StockLocation  "+
                           " FROM VSL_VesselSpareMaster VSM  " +                           
                           " INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId  ";

        string WhereCondition = "WHERE 1=1 ";
        
        WhereCondition = WhereCondition + "AND VSM.VesselCode = '" + VesselCode + "' ";
        
        if (txtCompCode.Text != "")
        {
            WhereCondition = WhereCondition + " AND CM.ComponentCode LIKE '%" + txtCompCode.Text.Trim().ToString() + "%' ";
            //WhereCondition = WhereCondition + " AND  ";
        }
        if (txtCompName.Text != "")
        {
            WhereCondition = WhereCondition + " AND  CM.ComponentName LIKE '%" + txtCompName.Text.Trim().ToString() + "%' ";
            //WhereCondition = WhereCondition + " AND  ";
        }
        if (chlsCriticalComponent.Checked)
        {
            WhereCondition = WhereCondition + " AND ISNULL(CM.CriticalType,'') <>'' ";
        }
        if (chlsCriticalSpare.Checked)
        {
            WhereCondition = WhereCondition + " AND Critical ='C' ";
        }
        if (ddlSpareStatus.SelectedIndex > 0)
        {
            WhereCondition = WhereCondition + " AND VSM.Status ='" + ddlSpareStatus.SelectedValue + "'";
        }
	if (chkOR.Checked)
        {
            WhereCondition = WhereCondition + " AND isOR ='1' ";
        }

        WhereCondition = WhereCondition + " order by VSM.VesselCode Desc ";

        Session["SQLDefectReport_Office"] = strSearch + WhereCondition;

        hfReportQuery.Value = strSearch + WhereCondition;
        Session["hfReportQuery"] = strSearch + WhereCondition;

        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(strSearch + WhereCondition);
        if (dtSearchData.Rows.Count > 0)
        {
            rptDefects.DataSource = dtSearchData;
            rptDefects.DataBind(); 
        }
        else
        {
            rptDefects.DataSource = null;
            rptDefects.DataBind();
        }

        

        
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            chk.Visible = false;
        }
        chlsCriticalComponent.Checked = false;
        chlsCriticalSpare.Checked = false;
        txtCompCode.Text = "";
        
        rptDefects.DataSource = null;
        rptDefects.DataBind();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        //Response.Redirect("~/Reports/SpareMgmtReport.aspx");        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenPrintWindow();", true);
        

    }
    protected void lnkEdit_OnClick(object sender, EventArgs e)
    {
        divEditCriticalComp.Visible = true;
        LinkButton lnk = (LinkButton)sender;
        HiddenField hfVesselCode = (HiddenField)lnk.Parent.FindControl("hfVesselCode");
        HiddenField hfComponentId = (HiddenField)lnk.Parent.FindControl("hfComponentId");
        HiddenField hfOffice_Ship = (HiddenField)lnk.Parent.FindControl("hfOffice_Ship");
        HiddenField hfSpareId = (HiddenField)lnk.Parent.FindControl("hfSpareId");
        HiddenField hfCritical = (HiddenField)lnk.Parent.FindControl("hfCritical");


        if (hfCritical.Value.ToUpper() == "C")
            chkCritical.Checked = true;
        else
            chkCritical.Checked = false;
        VesselCode = hfVesselCode.Value;
        ComponentId = Common.CastAsInt32( hfComponentId.Value);
        Office_Ship = hfOffice_Ship.Value;
        SpareID = Common.CastAsInt32(hfSpareId.Value);

    }
    protected void btnCloseCritical_OnClick(object sender, EventArgs e)
    {
        divEditCriticalComp.Visible = false;
        btnSearch_Click(sender, e);

    }
    


    
}

