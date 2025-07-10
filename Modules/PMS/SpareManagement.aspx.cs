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

public partial class SpareManagement : System.Web.UI.Page
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
        get { return ViewState["VesselCode"].ToString(); }
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
        lblMsg.Text = "";
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1049, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 16;
            //BindFleet();
            BindVessels();
            BindStockLocation();
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
    private void BindVessels()
    {
        //string strvessels = "";
        //DataTable dtVessels = new DataTable();
        //if (ddlFleet.SelectedIndex != 0)
        //    strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND and VESSELSTATUSID=1 AND  VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
        //else

        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel with(nolock) WHERE  VESSELSTATUSID=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VesselName";
        //strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE VESSELSTATUSID=1 AND ISNULL(IsExported,0) = 1 ORDER BY VesselName";

        DataTable dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);

        ddlVessels.DataSource = dtVessels;
        ddlVessels.DataTextField = "VesselName";
        ddlVessels.DataValueField = "VesselCode";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, "< Select >");
    }

    private void BindStockLocation()
    {
        string strLocations = "Select StockLocationId,StockLocation from SpareStockLocation with(nolock) where Status = 'A' ORDER BY StockLocation";
        DataTable dtStockLocation = Common.Execute_Procedures_Select_ByQuery(strLocations);
        ddlStockLocation.DataSource = dtStockLocation;
        ddlStockLocation.DataTextField = "StockLocation";
        ddlStockLocation.DataValueField = "StockLocationId";
        ddlStockLocation.DataBind();
        ddlStockLocation.Items.Insert(0, "< Select >");
    }
    #endregion
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessels();
        //if (ddlFleet.SelectedIndex != 0)
        //{
        //    DataTable dtVessels = new DataTable();
        //    string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
        //    dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        //    ddlVessels.Items.Clear();
        //    if (dtVessels.Rows.Count > 0)
        //    {
        //        ddlVessels.DataSource = dtVessels;
        //        ddlVessels.DataTextField = "VesselName";
        //        ddlVessels.DataValueField = "VesselCode";
        //        ddlVessels.DataBind();
        //    }
        //    else
        //    {
        //        ddlVessels.DataSource = null;
        //        ddlVessels.DataBind();
        //    }
        //    ddlVessels.Items.Insert(0, "< All >");
        //}
        //else
        //{
        //    ddlVessels.Items.Clear();
        //    BindVessels();
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Vessel.";
            ddlVessels.Focus();
            return;
        }

        string strSearch = " SELECT VSM.*,V.VesselName,CM.CriticalType,CM.ComponentCode,CM.ComponentName,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster with(nolock) WHERE LEN(ComponentCode)= (LEN(CM.ComponentCode)-3) AND   " +
                           " LEFT(ComponentCode,(LEN(CM.ComponentCode)-3)) = LEFT(CM.ComponentCode,(LEN(CM.ComponentCode)-3)))As LinkedTo, " +
                           " dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB, " +
                           " VSI.StockLocation AS StockLocation  " +
                           " FROM (Select VesselCode, ComponentId, Office_Ship, SpareId, SpareName, Maker, MakerType, PartNo, Critical, DrawingNo,isOR, PartName, Status, sum(MinQty) As MinQty from VSL_VesselSpareMaster with(nolock) group by VesselCode, ComponentId, Office_Ship, SpareId, SpareName, Maker, MakerType, PartNo, Critical, DrawingNo,isOR, PartName, Status ) VSM   " +
                           " inner join dbo.Vessel v with(nolock) on v.VesselCode=VSM.VesselCode " +
                           " INNER JOIN ComponentMaster CM with(nolock) ON VSM.ComponentId = CM.ComponentId  " +
                           " LEFT OUTER JOIN (SELECT SL.StockLocation, VSI.StockLocationId,VesselCode,ComponentId,Office_Ship,SpareId,sum(QtyRecd) As QtyRecd  FROM VSL_StockInventory VSI with(nolock) LEFT OUTER JOIN SpareStockLocation SL with(nolock) on VSI.StockLocationId = SL.StockLocationId Group by SL.StockLocation, VSI.StockLocationId,VesselCode,ComponentId,Office_Ship,SpareId) As VSI  on VSI.VesselCode = VSM.VesselCode AND VSI.ComponentId = VSM.ComponentId AND VSI.Office_Ship = VSM.Office_Ship AND VSI.SpareId = VSM.SpareId and VSI.QtyRecd > 0   ";

        string WhereCondition = "WHERE 1=1 ";
        if (ddlVessels.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + "AND VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }

        if (ddlStockLocation.SelectedIndex > 0)
        {
            WhereCondition = WhereCondition + "AND (VSI.StockLocationId = '" + ddlStockLocation.SelectedValue.ToString() + "') ";
        }
        if (txtCompCode.Text != "")
        {
            WhereCondition = WhereCondition + " AND  ( CM.ComponentCode LIKE '%" + txtCompCode.Text.Trim().ToString() + "%' or CM.ComponentName LIKE '%" + txtCompCode.Text.Trim().ToString() + "%') ";
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
        if (chkOR.Checked)
        {
            WhereCondition = WhereCondition + " AND isOR ='1' ";
        }

        if (ddlSpareStatus.SelectedIndex > 0)
        {
            WhereCondition = WhereCondition + " AND VSM.Status ='" + ddlSpareStatus.SelectedValue + "'";
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

        btnUpdateCriticalType.Visible = false;
        btnUpdateSpareOR.Visible = false;
        btnCancelUpdate.Visible = false;

        if (rptDefects.Items.Count > 0)
        {
            btnModify.Visible = true;
            btnMarkOR.Visible = true;
        }
        else
        {
            btnModify.Visible = false;
            btnMarkOR.Visible = false;
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            HiddenField hfdComponentCritical = (HiddenField)Itm.FindControl("hfdComponentCritical");
            HiddenField hfdSpareStatus = (HiddenField)Itm.FindControl("hfdSpareStatus");

            //chk.Visible = (hfdComponentCritical.Value.Trim() == "C" || hfdComponentCritical.Value.Trim() == "E") && hfdSpareStatus.Value.Trim() == "A";
            chk.Visible = hfdSpareStatus.Value.Trim() == "A";
        }
        btnUpdateCriticalType.Visible = true;
        btnCancelUpdate.Visible = true;
        btnModify.Visible = false;
        btnMarkOR.Visible = false;
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

        //ddlFleet.SelectedIndex = 0;
        ddlVessels.SelectedIndex = 0;
        txtCompCode.Text = "";
        btnUpdateCriticalType.Visible = false;
        btnCancelUpdate.Visible = false;
        btnModify.Visible = true;
        btnMarkOR.Visible = true;
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
        ComponentId = Common.CastAsInt32(hfComponentId.Value);
        Office_Ship = hfOffice_Ship.Value;
        SpareID = Common.CastAsInt32(hfSpareId.Value);

    }
    protected void btnCloseCritical_OnClick(object sender, EventArgs e)
    {
        divEditCriticalComp.Visible = false;
        btnSearch_Click(sender, e);

    }
    protected void btnCritical_OnClick(object sender, EventArgs e)
    {
        try
        {
            string sCritical = (chkCritical.Checked) ? "C" : "";
            DataTable ds = Common.Execute_Procedures_Select_ByQuery(" update VSL_VesselSpareMaster  set Critical='" + sCritical + "' where VesselCode='" + VesselCode + "' and ComponentId=" + ComponentId + " and Office_Ship='" + Office_Ship + "' and SpareId=" + SpareID);
            lblMsgCritical.Text = "Record saved successfully.";
        }
        catch (Exception ex)
        {
            lblMsgCritical.Text = "Error while saving record.";
        }
    }


    //--------------
    protected void btnUpdateCriticalType_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            string sCritical = (chk.Checked) ? "C" : "";

            HiddenField hfPreCritical = (HiddenField)Itm.FindControl("hfPreCritical");

            if (!(hfPreCritical.Value == "C") == chk.Checked)
            {
                HiddenField hfVesselCode = (HiddenField)Itm.FindControl("hfVesselCode");
                HiddenField hfComponentId = (HiddenField)Itm.FindControl("hfComponentId");
                HiddenField hfOffice_Ship = (HiddenField)Itm.FindControl("hfOffice_Ship");
                HiddenField hfSpareId = (HiddenField)Itm.FindControl("hfSpareId");
                HiddenField hfCritical = (HiddenField)Itm.FindControl("hfCritical");

                VesselCode = hfVesselCode.Value;
                ComponentId = Common.CastAsInt32(hfComponentId.Value);
                Office_Ship = hfOffice_Ship.Value;
                SpareID = Common.CastAsInt32(hfSpareId.Value);

                //string sql = " update VSL_VesselSpareMaster  set Critical='" + sCritical + "',CriticalUpdatedBy='" + Session["FullName"].ToString() + "',CriticalUpdatedByOn=Getdate() where VesselCode='" + VesselCode + "' and ComponentId=" + ComponentId + " and Office_Ship='" + Office_Ship + "' and SpareId=" + SpareID;
                //lblMsg.Text = sql;
                DataTable ds = Common.Execute_Procedures_Select_ByQuery(" update VSL_VesselSpareMaster  set Critical='" + sCritical + "',CriticalUpdatedBy='" + Session["FullName"].ToString() + "',CriticalUpdatedByOn=Getdate(),Updated=1,UpdatedOn=getdate() where VesselCode='" + VesselCode + "' and ComponentId=" + ComponentId + " and Office_Ship='" + Office_Ship + "' and SpareId=" + SpareID);
            }
            chk.Visible = false;
        }
        btnSearch_Click(sender, e);
    }

    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            chk.Visible = false;
        }
        btnUpdateCriticalType.Visible = false;
        btnUpdateSpareOR.Visible = false;
        btnCancelUpdate.Visible = false;
        btnModify.Visible = true;
        btnMarkOR.Visible = true;
    }

    protected void btnMarkOR_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            HiddenField HfPreOR = (HiddenField)Itm.FindControl("HfPreOR");
            chk.Visible = true;
            chk.Checked = (HfPreOR.Value.Trim() == "True" );
        }
        btnUpdateSpareOR.Visible = true;
        btnCancelUpdate.Visible = true;

        btnModify.Visible = false;
        btnMarkOR.Visible = false;
    }

    protected void btnUpdateSpareOR_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem Itm in rptDefects.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkEdit");
            string isOR = (chk.Checked) ? "1" : "0";

            HiddenField HfPreOR = (HiddenField)Itm.FindControl("HfPreOR");

            if (!(HfPreOR.Value == "True") == chk.Checked)
            {
                HiddenField hfVesselCode = (HiddenField)Itm.FindControl("hfVesselCode");
                HiddenField hfComponentId = (HiddenField)Itm.FindControl("hfComponentId");
                HiddenField hfOffice_Ship = (HiddenField)Itm.FindControl("hfOffice_Ship");
                HiddenField hfSpareId = (HiddenField)Itm.FindControl("hfSpareId");
                HiddenField hfCritical = (HiddenField)Itm.FindControl("hfCritical");

                VesselCode = hfVesselCode.Value;
                ComponentId = Common.CastAsInt32(hfComponentId.Value);
                Office_Ship = hfOffice_Ship.Value;
                SpareID = Common.CastAsInt32(hfSpareId.Value);

                //string sql = " update VSL_VesselSpareMaster  set Critical='" + sCritical + "',CriticalUpdatedBy='" + Session["FullName"].ToString() + "',CriticalUpdatedByOn=Getdate() where VesselCode='" + VesselCode + "' and ComponentId=" + ComponentId + " and Office_Ship='" + Office_Ship + "' and SpareId=" + SpareID;
                //lblMsg.Text = sql;
                DataTable ds = Common.Execute_Procedures_Select_ByQuery(" update VSL_VesselSpareMaster  set isOR='" + isOR + "',CriticalUpdatedBy='" + Session["FullName"].ToString() + "',CriticalUpdatedByOn=Getdate(),Updated=1,UpdatedOn=getdate() where VesselCode='" + VesselCode + "' and ComponentId=" + ComponentId + " and Office_Ship='" + Office_Ship + "' and SpareId=" + SpareID);
            }
            chk.Visible = false;
        }
        btnSearch_Click(sender, e);
    }
}