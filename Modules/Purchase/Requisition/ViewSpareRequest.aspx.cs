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
using System.Collections.Generic;

public partial class MenuPlanner_ViewSpareRequest : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int UserId
    {
        set { ViewState["UserId"] = value; }
        get { return Common.CastAsInt32(ViewState["UserId"]); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public int RequisitionId
    {
        set { ViewState["RequisitionId"] = value; }
        get { return Common.CastAsInt32(ViewState["RequisitionId"]); }
    }
    public string UserName
    {
        set { ViewState["UserName"] = value; }
        get { return ViewState["UserName"].ToString(); }
    }
    #endregion -----------------------------------
    //public DataTable BindUnits()
    //{
    //    string strSQL = "SELECT 0 AS [UnitId],'< Select >' AS [UnitName] UNION SELECT [UnitId], [UnitName] FROM [dbo].[MP_UnitMaster]";
    //    DataTable dtUnits = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    return dtUnits;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck_New();
        //---------------------------------------
        //------------------------------------------
        try
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["FullName"].ToString().Trim();
            VesselCode = Request.QueryString["VSL"].ToString().Trim();
            RequisitionId = Common.CastAsInt32(Request.QueryString["PrId"]);
            if(VesselCode=="")
            {
                ProjectCommon.ShowMessage("Your session is expired. Please login again.");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "s", "window.close();", true);
                return;
            }
        }
        catch 
        {
            ProjectCommon.ShowMessage("Your session is expired. Please login again.");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "s", "window.close();", true);
            return;
        }
        //------------------------------------------
        lblMsg.Text = "";
        lblerrorMsg.Text = "";
        //btnApprove.Visible = false;
        if (!IsPostBack)
        {
            if (RequisitionId>0) 
            {
                ShowRequisitionMaster();
                //btnApprove.Visible = (txtReqNo.Text.Trim() == "" && UserId <= 2);
            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_TMP_Spares WHERE USERID=" + UserId);
                ResetAccountCodes();
           }
            //ShowTempSpares();
        }
    }

    public void ShowRequisitionMaster()
    {
        string strSQL = "SELECT *, (SELECT FirstName + ' ' + LastName FROM [dbo].[UserMaster] WHERE [LoginId] = TransferedBy) AS TransferBy FROM DBO.[MP_VSL_SparesReqMaster] WHERE [VesselCode] = '" + VesselCode + "' AND  SpareReqId =" + RequisitionId;

        DataTable dtReqMaster = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtReqMaster != null && dtReqMaster.Rows.Count > 0)
        {
            lblReqNo.Text =  " [ " + dtReqMaster.Rows[0]["ReqnNo"].ToString() + " ] ";
            txtReqNo.Text = dtReqMaster.Rows[0]["ReqnNo"].ToString();
            txtPort.Text = dtReqMaster.Rows[0]["Port"].ToString();
            txtETA.Text =Common.ToDateString(dtReqMaster.Rows[0]["ETA"]);
            txtRemarks.Text = dtReqMaster.Rows[0]["Remarks"].ToString();
            txtUpdatedBy.Text = dtReqMaster.Rows[0]["UpdatedBy"].ToString();
            lblUpdatedOn.Text = Common.ToDateString(dtReqMaster.Rows[0]["UpdatedOn"]);
            ShowRequisitionDetails();
            //ddlAccCode.SelectedValue = dtReqMaster.Rows[0]["AccountCode"].ToString();
            lblAccountCode.Text = dtReqMaster.Rows[0]["AccountCode"].ToString();

            lblVerifiedBy.Text = dtReqMaster.Rows[0]["ApprovedBy"].ToString();
            lblVerifiedOn.Text =  Common.ToDateString(dtReqMaster.Rows[0]["ApprovedOn"]);

            lblTransferedBy.Text = dtReqMaster.Rows[0]["TransferBy"].ToString();
            lblTransferedOn.Text = Common.ToDateString(dtReqMaster.Rows[0]["TransferedOn"]);

            if (Convert.ToString(dtReqMaster.Rows[0]["STATUS"]) == "T" || Convert.ToString(dtReqMaster.Rows[0]["ACTIVEINACTIVE"]) == "I")
            {
                btnTransfer.Visible = false;
                btnInactive.Visible = false;
                //btnSave.Visible = false;
            }
            else
            {
                btnTransfer.Visible = true;
                btnInactive.Visible = true;
            }
        }
    }
    public void ShowRequisitionDetails()
    {
            
        ////string sql = "SELECT VSM.*,CM.ComponentCode,CMV.ACCOUNTCODES,QTY,ROB,(SELECT TOP 1 SRD.UOM FROM MP_VSL_SparesReqDetails SRD WHERE SRD.VESSELCODE=TMP.VESSELCODE AND SRD.COMPONENTID=TMP.COMPONENTID AND SRD.OFFICE_SHIP=TMP.OFFICE_SHIP AND SRD.SPAREID=TMP.SPAREID) AS UOM " +
        ////                "FROM  " +
        ////                "MP_VSL_SparesReqDetails TMP " +
        ////                "LEFT JOIN VSL_VesselSpareMaster VSM ON TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=VSM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID " +
        ////                "LEFT JOIN COMPONENTMASTER CM ON TMP.COMPONENTID=CM.COMPONENTID " +
        ////                "LEFT JOIN VSL_ComponentMasterForVessel CMV ON TMP.VESSELCODE=CMV.VESSELCODE AND CMV.COMPONENTID=CM.COMPONENTID " +
        ////                "WHERE TMP.VESSELCODE='" + VesselCode + "' AND TMP.SpareReqId=" + RequisitionId;
        //string sql = "DELETE FROM DBO.MP_VSL_TMP_Spares";
        //Common.Execute_Procedures_Select_ByQuery(sql);

        //sql = "INSERT INTO DBO.MP_VSL_TMP_Spares(UserId,VesselCode,ComponentId,Office_Ship,SpareId,QTY,ROB,UOM,OfficeQty,OfficeUOM) " +
        //      "SELECT " + UserId + ",VesselCode,ComponentId,Office_Ship,SpareId,QTY,ROB,UOM,OfficeQty,OfficeUOM FROM DBO.MP_VSL_SparesReqDetails WHERE VESSELCODE='" + VesselCode + "' AND SpareReqId=" + RequisitionId;

        //Common.Execute_Procedures_Select_ByQuery(sql);

        //ShowTempSpares();
        //ResetAccountCodes();

        //------------------------------ New COde ------------------

        string sql = "SELECT VSM.*,CM.ComponentCode,CMV.ACCOUNTCODES,QTY,ROB,(SELECT TOP 1 SRD.UOM FROM DBO.MP_VSL_SparesReqDetails SRD WHERE SRD.VESSELCODE=TMP.VESSELCODE AND SRD.COMPONENTID=TMP.COMPONENTID AND SRD.OFFICE_SHIP=TMP.OFFICE_SHIP AND SRD.SPAREID=TMP.SPAREID AND UOM IS NOT NULL) AS UOM,OfficeQty,(SELECT TOP 1 SRD.OfficeUOM FROM DBO.MP_VSL_SparesReqDetails SRD WHERE SRD.VESSELCODE=TMP.VESSELCODE AND SRD.COMPONENTID=TMP.COMPONENTID AND SRD.OFFICE_SHIP=TMP.OFFICE_SHIP AND SRD.SPAREID=TMP.SPAREID AND OfficeUOM IS NOT NULL) AS OfficeUOM " +
                        "FROM  " +
                        "DBO.MP_VSL_SparesReqDetails TMP " +
                        "LEFT JOIN DBO.VSL_VesselSpareMaster VSM ON TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=VSM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID " +
                        "LEFT JOIN DBO.COMPONENTMASTER CM ON TMP.COMPONENTID=CM.COMPONENTID " +
                        "LEFT JOIN DBO.VSL_ComponentMasterForVessel CMV ON TMP.VESSELCODE=CMV.VESSELCODE AND CMV.COMPONENTID=CM.COMPONENTID " +
                        "WHERE TMP.VESSELCODE='" + VesselCode + "' AND TMP.SpareReqId=" + RequisitionId;

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSpares.DataSource = dtSpares;
        rptSpares.DataBind();

        //ResetAccountCodes();

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "PrintReport('" + RequisitionId + "');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtPort.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter port of supply.";
        //    txtPort.Focus();
        //    return;
        //}
        //if (txtETA.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter ETA to port.";
        //    txtETA.Focus();
        //    return;
        //}
        //DateTime dt;
        //if (!DateTime.TryParse(txtETA.Text.Trim(), out dt))
        //{
        //    lblMsg.Text = "Please enter valid date.";
        //    txtETA.Focus();
        //    return;
        //}
        //if (txtUpdatedBy.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter updated by.";
        //    txtUpdatedBy.Focus();
        //    return;
        //}

        //foreach (RepeaterItem rpt in rptSpares.Items)
        //{
        //    ImageButton ch = (ImageButton)rpt.FindControl("btnDelSpare");
        //    int ComponentId = Common.CastAsInt32(ch.Attributes["componentid"]);
        //    string OfficeShip = ch.Attributes["officeship"];
        //    int SpareId = Common.CastAsInt32(ch.Attributes["spareId"]);

        //    decimal ROB = Common.CastAsDecimal(((Label)rpt.FindControl("lblROBQty")).Text);
        //    decimal Qty = Common.CastAsDecimal(((TextBox)rpt.FindControl("txtReqQty")).Text);
        //    string Unit = ((DropDownList)rpt.FindControl("ddlUnit")).SelectedValue;

        //    if (Qty <= 0)
        //    {
        //        lblMsg.Text = "Please enter Qty.";
        //        ((TextBox)rpt.FindControl("txtReqQty")).Focus();
        //        return;
        //    }
        //    else if (Unit.Trim() == "")
        //    {
        //        lblMsg.Text = "Please select Unit.";
        //        ((DropDownList)rpt.FindControl("ddlUnit")).Focus();
        //        return;
        //    }
        //    else
        //        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_TMP_AppUpdateSpares " + UserId.ToString() + ",'" + VesselCode + "'," + ComponentId + ",'" + OfficeShip + "'," + SpareId + "," + Qty + "," + ROB + ",'" + Unit + "'");
        //}

        //try
        //{

        //    Common.Set_Procedures("[dbo].[MP_InsertUpdate_SparesReqMaster]");
        //    Common.Set_ParameterLength(8);
        //    Common.Set_Parameters(
        //       new MyParameter("@VesselCode", VesselCode),
        //       new MyParameter("@SpareReqId", RequisitionId),
        //       new MyParameter("@AccountCode", ddlAccCode.SelectedValue),
        //       new MyParameter("@Port", txtPort.Text.Trim()),
        //       new MyParameter("@ETA", txtETA.Text.Trim()),
        //       new MyParameter("@Remarks", txtRemarks.Text.Trim()),
        //       new MyParameter("@UpdatedBy", txtUpdatedBy.Text.Trim()),
        //       new MyParameter("@UserId", UserId));
        //    DataSet ds = new DataSet();
        //    ds.Clear();
        //    Boolean res;
        //    res = Common.Execute_Procedures_IUD(ds);
        //    if (res)
        //    {
        //        RequisitionId = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
        //        //btnSave.Enabled = false;

        //        ProjectCommon.ShowMessage("Requistion Created Successfully.");
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "re", "RefreshParent();", true);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "cl", "window.close();", true);
        //    }
        //    else
        //    {
        //        lblMsg.Text = "Unable to add data.Error :" + Common.getLastError();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = "Unable to add data.Error :" + ex.Message.ToString();
        //}

        try
        {
            string SqlMstr = "UPDATE DBO.MP_VSL_SparesReqMaster SET [Remarks] = '" + txtRemarks.Text.Trim().Replace("'", "`") + "' WHERE VesselCode = '" + VesselCode + "' AND SpareReqId =" + RequisitionId;
            Common.Execute_Procedures_Select_ByQuery(SqlMstr);


            //foreach (RepeaterItem rpt in rptSpares.Items)
            //{
            //    TextBox txtReqQty = (TextBox)rpt.FindControl("txtReqQty");
            //    decimal Qty = Common.CastAsDecimal(txtReqQty.Text);
            //    string Unit = ((DropDownList)rpt.FindControl("ddlUnit")).SelectedValue;

            //    if (Qty <= 0)
            //    {
            //        lblMsg.Text = "Please enter Qty.";
            //        ((TextBox)rpt.FindControl("txtReqQty")).Focus();
            //        return;
            //    }
            //    if (Unit.Trim() == "")
            //    {
            //        lblMsg.Text = "Please select Unit.";
            //        ((DropDownList)rpt.FindControl("ddlUnit")).Focus();
            //        return;
            //    }
            //}

            foreach (RepeaterItem rpt in rptSpares.Items)
            {
                TextBox txtReqQty = (TextBox)rpt.FindControl("txtReqQty");
                int ComponentId = Common.CastAsInt32(txtReqQty.Attributes["componentid"]);
                string OfficeShip = txtReqQty.Attributes["officeship"];
                int SpareId = Common.CastAsInt32(txtReqQty.Attributes["spareId"]);
                string vesselcode = txtReqQty.Attributes["vesslcode"];

                decimal Qty = Common.CastAsDecimal(txtReqQty.Text);
                string Unit = ((DropDownList)rpt.FindControl("ddlUnit")).SelectedValue;

                //Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_TMP_AppUpdateSpares " + UserId.ToString() + ",'" + VesselCode + "'," + ComponentId + ",'" + OfficeShip + "'," + SpareId + "," + Qty + "," + ROB + ",'" + Unit + "'");

                string SQL = "UPDATE  DBO.MP_VSL_SparesReqDetails SET OfficeQty=" + Qty + ",OfficeUOM='" + Unit.Trim() + "' " +
                             "WHERE SpareReqId= " + RequisitionId + " AND VesselCode='" + vesselcode + "' AND ComponentId=" + ComponentId + " AND Office_Ship='" + OfficeShip + "' AND SpareId=" + SpareId;
                Common.Execute_Procedures_Select_ByQuery(SQL);
            }
            
            ProjectCommon.ShowMessage("Data updated Successfully.");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save.Error :" + ex.Message.ToString();
        }
    
    }
    protected void btnDelSpare_Click(object sender, EventArgs e)
    {
        ImageButton b=(ImageButton)sender;
        int ComponentId = Common.CastAsInt32(b.Attributes["componentid"]);
        string OfficeShip = b.Attributes["officeship"];
        int SpareId = Common.CastAsInt32(b.Attributes["spareId"]);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_TMP_Spares WHERE USERID=" + UserId.ToString() + " AND VESSELCODE='" + VesselCode + "' AND COMPONENTID=" + ComponentId + " AND OFFICE_SHIP='" + OfficeShip + "' AND SPAREID=" + SpareId);
        ShowTempSpares();
        ResetAccountCodes();
        
    }
    protected void imgbtnDelAllSpares_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_TMP_Spares");
        ShowTempSpares();
        ResetAccountCodes();
    }
    public void ShowTempSpares()
    {
        string sql = "SELECT VSM.*,CM.ComponentCode,CMV.ACCOUNTCODES,QTY,ROB,(SELECT TOP 1 SRD.UOM FROM DBO.MP_VSL_SparesReqDetails SRD WHERE SRD.VESSELCODE=TMP.VESSELCODE AND SRD.COMPONENTID=TMP.COMPONENTID AND SRD.OFFICE_SHIP=TMP.OFFICE_SHIP AND SRD.SPAREID=TMP.SPAREID AND UOM IS NOT NULL) AS UOM,OfficeQty,(SELECT TOP 1 SRD.OfficeUOM FROM DBO.MP_VSL_SparesReqDetails SRD WHERE SRD.VESSELCODE=TMP.VESSELCODE AND SRD.COMPONENTID=TMP.COMPONENTID AND SRD.OFFICE_SHIP=TMP.OFFICE_SHIP AND SRD.SPAREID=TMP.SPAREID AND OfficeUOM IS NOT NULL) AS OfficeUOM " +
                        "FROM  " +
                        "DBO.MP_VSL_TMP_Spares TMP " +
                        "LEFT JOIN DBO.VSL_VesselSpareMaster VSM ON TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=VSM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID " +
                        "LEFT JOIN DBO.COMPONENTMASTER CM ON TMP.COMPONENTID=CM.COMPONENTID " +
                        "LEFT JOIN DBO.VSL_ComponentMasterForVessel CMV ON TMP.VESSELCODE=CMV.VESSELCODE AND CMV.COMPONENTID=CM.COMPONENTID " +
                        "WHERE TMP.USERID=" + UserId.ToString();

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSpares.DataSource = dtSpares;
        rptSpares.DataBind();
    }
    protected void btnSearchSpares_Click(object sender, EventArgs e)
    {
        string sql = "SELECT VSM.*,CM.ComponentCode,CM.ComponentName,VCM.ACCOUNTCODES,CM.COMPONENTID AS ACTCOMPONENTID," +
                     "(SELECT QTY FROM DBO.MP_VSL_TMP_Spares TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=CM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID) as ReqQty," +
                     "ISNULL " +
                     "( " +
                     "(SELECT ROB FROM DBO.MP_VSL_TMP_Spares TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=CM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID)  " +
                     ", (SELECT [dbo].[fn_getInventoryROB](VCM.VESSELCODE,CM.COMPONENTID,VSM.OFFICE_SHIP,VSM.SPAREID,GETDATE())) " +
                     ") " +
                     "as ROBQty, " +
                     "(SELECT UOM FROM DBO.MP_VSL_TMP_Spares TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSM.VESSELCODE AND TMP.COMPONENTID=CM.COMPONENTID AND TMP.Office_Ship=VSM.OFFICE_SHIP AND TMP.SPAREID=VSM.SPAREID) as UOM " +
                     "FROM DBO.COMPONENTMASTER CM " +
                     "INNER JOIN DBO.VSL_ComponentMasterForVessel VCM ON CM.COMPONENTID=VCM.COMPONENTID AND VCM.VESSELCODE='" + VesselCode + "' " +
                     "INNER JOIN DBO.VSL_VesselSpareMaster VSM ON VSM.VESSELCODE=VCM.VESSELCODE AND VSM.COMPONENTID=CM.COMPONENTID " +
                     "WHERE LTRIM(RTRIM(CM.COMPONENTCODE)) LIKE '" + txt_F_CompCode.Text + "%' AND VCM.ACCOUNTCODES LIKE '%" + txt_F_AccCode.Text + "%'";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        lblRecCount.Text = "( " + dt.Rows.Count + " ) Records Found.";
        rptPopSpares.DataSource = dt;
        rptPopSpares.DataBind();
    }
    protected string[] LoadUnits()
    {
        string Units = ",AMPULE,B0TTLE,BOX,BAG,BAR,BKS,BUNDLE,CANS,CAPSUAL,CARTONS,CASE,CHART,CM,COIL,CONTAINER,CYLINDER,Dozen,Drum,Gallons,HOSE,HRS,JAR,KGS,KITS,LBS,LOAF,LTR,MTR,PACK,PCS,PAIR,REAM,ROLL,SACHETS,SACKS,SAMPLE,SET,SHACKLE,SHEET,SQ.METER,STEPS,STRIP,SUITS,TINS,TRAY,TRIP,TUG,UNIT,VOL,WKS,LENGTH";
        return Units.Split(',');
    }
    protected void imgbtnAddSpares_Click(object sender, ImageClickEventArgs e)
    {
        lblerrorMsg.Text = "";
        dvSpares.Visible = true;
        txt_F_CompCode.Focus();
    }
    protected void btnEditSpare_Click(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        string componentcode = b.Attributes["componentcode"].Trim();
        string officeship = b.Attributes["officeship"].Trim();
        string spareId = b.Attributes["spareId"].Trim();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('../Ship_AddEditSpares.aspx?CompCode=" + componentcode + "&VC=" + VesselCode + "&SPID=" + spareId + "&OffShip=" + officeship + "','');", true);
    }
    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        if (RequisitionId > 0)
        {
            ShowRequisitionDetails();
        }
        else
        {
            ShowTempSpares();
        }
        if (dvSpares.Visible)
            btnSearchSpares_Click(sender, e);
    }
   
    protected void btnAddToList_Click(object sender, EventArgs e)
    {
        //string AccountCode = "";
        try
        {
            foreach (RepeaterItem rpt in rptPopSpares.Items)
            {
                HtmlInputCheckBox ch = (HtmlInputCheckBox)rpt.FindControl("chkSelect");
                if (ch.Checked)
                {
                    int ComponentId = Common.CastAsInt32(ch.Attributes["componentid"]);
                    string OfficeShip = ch.Attributes["officeship"];
                    int SpareId = Common.CastAsInt32(ch.Attributes["spareId"]);
                    string Unit = ch.Attributes["uom"];
                    decimal ROB = Common.CastAsDecimal(((Label)rpt.FindControl("lblROBQty")).Text);
                    decimal Qty = 0;
                    Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_TMP_AppUpdateSpares " + UserId.ToString() + ",'" + VesselCode + "'," + ComponentId + ",'" + OfficeShip + "'," + SpareId + "," + Qty + "," + ROB + ",'" + Unit + "'");
                }
            }
            lblerrorMsg.Text = "Added successfully.";
        }
        catch (Exception ex)
        {
            lblerrorMsg.Text = "Unable add. Error : " + ex.Message.ToString();
        }
    }
    protected void ResetAccountCodes()
    {
        ////-------------------
        //string LastValue = ddlAccCode.SelectedValue;

        //DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT ACCOUNTCODES FROM [dbo].[VSL_ComponentMasterForVessel] WHERE COMPONENTID IN (SELECT DISTINCT COMPONENTID FROM DBO.MP_VSL_TMP_Spares WHERE USERID=" + UserId + ") AND VESSELCODE='" + VesselCode + "'");
        //List<string> acts = new List<string>();
        //foreach (DataRow dr in dtAccts.Rows)
        //{
        //    string[] parts = dr[0].ToString().Split(',');
        //    foreach (string item in parts)
        //    {
        //        if (!(acts.Contains(item)))
        //            acts.Add(item);
        //    }
        //}
        //ddlAccCode.Items.Clear();
        //foreach (string item in acts)
        //{
        //    ddlAccCode.Items.Add(new ListItem(item, item));
        //}
        //int lastIndex = ddlAccCode.Items.IndexOf((new ListItem(LastValue, LastValue)));
        //if (lastIndex >= 0)
        //    ddlAccCode.SelectedIndex = lastIndex;
        ////-------------------
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowTempSpares();
        txt_F_AccCode.Text = "";
        txt_F_CompCode.Text = "";
        rptPopSpares.DataSource = null;
        rptPopSpares.DataBind();
        ResetAccountCodes();
        dvSpares.Visible = false;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string cYear = DateTime.Today.Year.ToString().Substring(2);
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.MP_VSL_SparesReqMaster SET ReqnNo=(SELECT '" + VesselCode + "-" + cYear + "-S-' + REPLACE(STR(ISNULL(MAX(RIGHT(D.ReqnNo,3)),0)+1,3),' ','0') FROM DBO.MP_VSL_SparesReqMaster D WHERE RIGHT(LEFT(D.ReqnNo,6),2)='" + cYear + "'),ApprovedBy='" + UserName + "',ApprovedOn=GETDATE(),Updated=1,UpdatedOn=GETDATE() WHERE VESSELCODE='" + VesselCode + "' AND SpareReqId=" + RequisitionId);
        //btnApprove.Visible = false; 
        ShowRequisitionMaster();
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        //string SQL = "SELECT AccountID FROM DBO.sql_tblSMDPRAccounts WHERE ACCOUNTNUMBER in (SELECT AccountCode FROM dbo.MP_VSL_SparesReqMaster WHERE VesselCode='" + VesselCode.Trim() + "' AND SpareReqId=" + RequisitionId + " )";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        //if (dt.Rows.Count == 0)
        //{
        //    lblMsg.Text = "Sent for RFQ successfully.";
        //    return;
        //}

        try
        {
            Common.Set_Procedures("[dbo].[TRANSFTER_TO_POS_SpareReq]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@VesselCode", VesselCode.Trim()),
               new MyParameter("@SpareReqId", RequisitionId),
               new MyParameter("@TransferedBy", Session["loginid"].ToString())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Sent for RFQ successfully.";
                lblMsg.Style.Add("color", "Green");               
                btnTransfer.Visible = false;                 

            }
            else
            {
                lblMsg.Text = "Unable to send for RFQ. " + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to send for RFQ.Error :" + ex.Message.ToString();
        }
     
    }

    protected void btnInactive_Click(object sender, EventArgs e)
    {
        dvInactive.Visible = true;
    }
    protected void btnInactiveSave_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.MP_VSL_SparesReqMaster SET ACTIVEINACTIVE='I',OfficeRemarks='" + txtOFCComments.Text.Trim().Replace("'", "`") + "',OfficeRemarksBy='" + UserName + "',OfficeRemarksOn=getdate() WHERE VESSELCODE='" + VesselCode + "' AND SpareReqId=" + RequisitionId);
        ShowRequisitionMaster();
        dvInactive.Visible = false;
    }
    protected void btnCloseComm_Click(object sender, EventArgs e)
    {
        dvInactive.Visible = false;
    }
}