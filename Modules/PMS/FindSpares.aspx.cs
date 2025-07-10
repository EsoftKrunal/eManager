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

public partial class FindSpares : System.Web.UI.Page
{
    public DataTable dtSpareDetails;

    public string VesselCode
    {
        get { return ViewState["VSL"].ToString(); }
        set { ViewState["VSL"] = value; } 
    }
    public int CompId
    {
        get { return Common.CastAsInt32(ViewState["CompId"]); }
        set { ViewState["CompId"] = value; }
    }
    
    public string CompCode
    {
        set { ViewState["CompCode"] = value; }
        get { return ViewState["CompCode"].ToString(); }
    }
    
    public string ComponentCodeName
    {
        set { ViewState["_ComponentCodeName"] = value; }
        get { return ViewState["_ComponentCodeName"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            VesselCode = Session["CurrentShip"].ToString();
            CompId = Common.CastAsInt32(Request.QueryString["ComponentId"]);
            CompCode = Request.QueryString["ComponentCode"].ToString();
            //-----------

            string sql = " select * from ComponentMaster where ComponentCode='"+ CompCode + "'";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                ComponentCodeName = CompCode + " - " + dt.Rows[0]["ComponentName"].ToString();
            }

            ShowSpares();
         }
    }
    protected void btnsearch_click(object sender, EventArgs e)
    {
        ShowSpares();
    }
    public void ShowSpares()
    {
        string Parents = ProjectCommon.getParentComponents_Chain(CompId);
        lblComponent.Text = CompCode;
        string strSpares = "SELECT VesselCode,ComponentId,Office_Ship,SpareId,VESSELCODE + '#' + CONVERT(VARCHAR, COMPONENTID) + '#' + OFFICE_SHIP + '#' + CONVERT(VARCHAR, SPAREID) AS PKID, SpareName, Maker, MakerType, PartNo, DrawingNo from dbo.VSL_VesselSpareMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId IN(" + CompId + ", " + Parents + ") and (SpareName like '%" + txtfilter.Text + "%' or Maker like '%" + txtfilter.Text + "%' or PartNo like '%" + txtfilter.Text + "%' or DrawingNo like '%" + txtfilter.Text + "%') order by SPARENAME";
        DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(strSpares);
        rptspares.DataSource = dtspares;
        rptspares.DataBind();

        dtSpareDetails = (DataTable)Session["_dtSpareDetails"];
        foreach (RepeaterItem itm in rptspares.Items)
        {
            
            
            HiddenField hfdVesselCode = (HiddenField)itm.FindControl("hfdVesselCode");
            HiddenField hfdComponentID=(HiddenField)itm.FindControl("hfdComponentID");
            HiddenField hfdOfficeShip = (HiddenField)itm.FindControl("hfdOfficeShip");
            HiddenField hfdSpareID = (HiddenField)itm.FindControl("hfdSpareID");

            DataView dv = dtSpareDetails.DefaultView;
            dv.RowFilter = " VesselCode='"+hfdVesselCode.Value+"' and ComponentID="+ hfdComponentID.Value+ " and Office_Ship='"+ hfdOfficeShip .Value+ "' and SpareID="+ hfdSpareID .Value+ "";
            if (dv.ToTable().Rows.Count >0)
            {
                CheckBox chkSelectSpare = (CheckBox)itm.FindControl("chkSelectSpare");
                TextBox txtQtyCons = (TextBox)itm.FindControl("txtQtyCons");
                TextBox txtQtyRob = (TextBox)itm.FindControl("txtQtyRob");

                chkSelectSpare.Checked = true;
                txtQtyCons.Text = dv.ToTable().Rows[0]["QtyCons"].ToString();
                txtQtyRob.Text = dv.ToTable().Rows[0]["QtyRob"].ToString();
            }

        }

    }

    protected void btnUpdateSpareToSession_click(object sender, EventArgs e)
    {
        dtSpareDetails = (DataTable)Session["_dtSpareDetails"];
        dtSpareDetails.Clear();
        foreach (RepeaterItem itm in rptspares.Items)
        {
            CheckBox chkSelectSpare = (CheckBox)itm.FindControl("chkSelectSpare");

            if (chkSelectSpare.Checked)
            {
                HiddenField hfdVesselCode = (HiddenField)itm.FindControl("hfdVesselCode");
                HiddenField hfdComponentID = (HiddenField)itm.FindControl("hfdComponentID");
                HiddenField hfdOfficeShip = (HiddenField)itm.FindControl("hfdOfficeShip");
                HiddenField hfdSpareID = (HiddenField)itm.FindControl("hfdSpareID");

                Label lblsparename = (Label)itm.FindControl("lblsparename");
                Label lblMaker = (Label)itm.FindControl("lblMaker");
                Label lblpartNo = (Label)itm.FindControl("lblpartNo");
                Label lbldrawingno = (Label)itm.FindControl("lbldrawingno");

                TextBox txtQtyCons = (TextBox)itm.FindControl("txtQtyCons");
                TextBox txtQtyRob = (TextBox)itm.FindControl("txtQtyRob");



                DataRow dr = dtSpareDetails.NewRow();

                dr["RowId"] = "";
                dr["VesselCode"] = hfdVesselCode.Value;
                dr["ComponentId"] = hfdComponentID.Value;
                dr["Office_Ship"] = hfdOfficeShip.Value;
                dr["SpareId"] = hfdSpareID.Value;
                dr["SpareName"] = lblsparename.Text;
                dr["Maker"] = lblMaker.Text;
                dr["PartNo"] = lblpartNo.Text;

                dr["QtyCons"] = Common.CastAsInt32(txtQtyCons.Text);
                dr["QtyRob"] = Common.CastAsInt32(txtQtyRob.Text);
                dtSpareDetails.Rows.Add(dr);
            }
        }

        Session["_dtSpareDetails"]= dtSpareDetails;
    }
    protected void btnAddSpare_click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + ComponentCodeName.Trim().Split('-').GetValue(0).ToString().Trim() + "','" + Session["CurrentShip"].ToString().Trim() + "',' ');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowSpares();
    }
}
