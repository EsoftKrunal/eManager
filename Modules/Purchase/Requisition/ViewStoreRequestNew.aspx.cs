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

public partial class MenuPlanner_ViewStoreRequestNew : System.Web.UI.Page
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
    public int StoreItemId
    {
        set { ViewState["StoreItemId"] = value; }
        get { return Common.CastAsInt32(ViewState["StoreItemId"]); }
    }
    public string SelCatCode
    {
        get
        { return Convert.ToString(ViewState["_SelCatCode"]); }
        set
        { ViewState["_SelCatCode"] = value; }
    }
    #endregion -----------------------------------


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
            ddlUnit.DataSource = LoadUnits();
            ddlUnit.DataBind();
            if (RequisitionId>0)
            {
                ShowRequisitionMaster();

            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_TMP_Stores WHERE USERID=" + UserId);
                ResetAccountCodes();
            }
            //ShowTempStores();  
            ShowTempStores_Other();
            BindCategories();
        }
    }
    private void BindProducts()
    {
        string sql = "";
        string whereClause = "";
        if (txtSearch.Text.Trim() != "")
            whereClause = whereClause + " and ( PCode like '%" + txtSearch.Text.Trim() + "%' OR PName like '%" + txtSearch.Text.Trim() + "%' OR impano like '%" + txtSearch.Text.Trim() + "%' )";

        //if (sSelCode == "")
        //    sql = " select *,um.UnitName,StatusName=case when isnull(Active,0)=1 then 'Active' else 'Inactive' end from dbo.tblStoreItems si left join [dbo].[tbl_StoreUnitMaster] um on um.UnitId=si.Punit where Active=1 and Plevel=3 " + whereClause + " order by PCode";
        //else
        //    sql = " select *,um.UnitName,StatusName=case when isnull(Active,0)=1 then 'Active' else 'Inactive' end from dbo.tblStoreItems si left join [dbo].[tbl_StoreUnitMaster] um on um.UnitId=si.Punit where PCode like '" + sSelCode + ".%' and Active=1 and Plevel=3  " + whereClause + "  order by PCode";

        sql = " select *,um.UnitName,StatusName=case when isnull(Active,0)=1 then 'Active' else 'Inactive' end from dbo.tblStoreItems si left join [dbo].[tbl_StoreUnitMaster] um on um.UnitId=si.Punit where " + ((SelCatCode.Trim()=="")?"": " PCode like '" + SelCatCode + ".%' and ") + " Active =1 and Plevel=3 " + whereClause + " order by PCode";        

        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        rptProductLL.DataSource = dtP;
        rptProductLL.DataBind();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        BindProducts();
    }
    protected void radtype_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        pnlSelect.Visible = (radtype.SelectedIndex==0);
        pnlCreate.Visible = (radtype.SelectedIndex == 1);
    }
    public void ShowRequisitionMaster()
    {
        string strSQL = "SELECT *, (SELECT FirstName + ' ' + LastName FROM [dbo].[UserMaster] WHERE [LoginId] = TransferedBy) AS TransferBy FROM DBO.[MP_VSL_StoreReqMaster1] WHERE [VesselCode] = '" + VesselCode + "' AND  StoreReqId =" + RequisitionId;

        DataTable dtReqMaster = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtReqMaster != null && dtReqMaster.Rows.Count > 0)
        {
            lblReqNo.Text = " [ " + dtReqMaster.Rows[0]["ReqnNo"].ToString() + " ] ";
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
                btnSave.Visible = false;
                //btnAddNew.Visible = false;
            }
            else
            {
                btnTransfer.Visible = true;
                btnInactive.Visible = true;
                btnSave.Visible = true;
               // btnAddNew.Visible = true;
            }
        }
    }
    public void ShowRequisitionDetails()
    {
            
        //string sql = "DELETE FROM DBO.MP_VSL_TMP_Stores";
        //Common.Execute_Procedures_Select_ByQuery(sql);

        //sql = "INSERT INTO DBO.MP_VSL_TMP_Stores(UserId,VesselCode,ItemId,QTY,ROB) " +
        //      "SELECT " + UserId + ",VesselCode,ItemId,QTY,ROB FROM DBO.MP_VSL_StoreReqDetails WHERE VESSELCODE='" + VesselCode + "' AND StoreReqId=" + RequisitionId;

        //Common.Execute_Procedures_Select_ByQuery(sql);

        //ShowTempStores();

        string sql = "SELECT VSIM.*,QTY,ROB, OfficeQty ,vesselcode,un.UnitName " +
                        " FROM  " +
                        " DBO.MP_VSL_StoreReqDetails1 TMP " +
                        " LEFT JOIN DBO.tblStoreItems VSIM ON  TMP.PID=VSIM.PID " +
                        " LEFT JOIN DBO.tbl_StoreUnitMaster un ON un.UnitID=VSIM.Punit " +
                        "WHERE TMP.VesselCode='" + VesselCode + "' AND StoreReqId=" + RequisitionId;

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptStores.DataSource = dtSpares;
        rptStores.DataBind();

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

        //foreach (RepeaterItem rpt in rptStores.Items)
        //{
        //    ImageButton ch = (ImageButton)rpt.FindControl("btnDelStore");
        //    int ItemId = Common.CastAsInt32(ch.Attributes["ItemId"]);

        //    decimal ROB = Common.CastAsDecimal(((Label)rpt.FindControl("lblROBQty")).Text);
        //    decimal Qty = Common.CastAsDecimal(((TextBox)rpt.FindControl("txtReqQty")).Text);
        //    string Unit = ((Label)rpt.FindControl("lblUnit")).Text.Trim();

        //    if (Qty <= 0)
        //    {
        //        lblMsg.Text = "Please enter Qty.";
        //        ((TextBox)rpt.FindControl("txtReqQty")).Focus();
        //        return;
        //    }
        //    else
        //        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_TMP_AppUpdateStores " + UserId.ToString() + ",'" + VesselCode + "'," + ItemId + "," + Qty + "," + ROB);
        //}

        //try
        //{
        //    Common.Set_Procedures("[dbo].[MP_InsertUpdate_StoreReqMaster]");
        //    Common.Set_ParameterLength(8);
        //    Common.Set_Parameters(
        //       new MyParameter("@VesselCode", VesselCode),
        //       new MyParameter("@StoreReqId", RequisitionId),
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
            string SqlMstr = "UPDATE DBO.MP_VSL_StoreReqMaster1 SET [Remarks] = '" + txtRemarks.Text.Trim().Replace("'", "`") + "' WHERE VesselCode = '" + VesselCode + "' AND StoreReqId =" + RequisitionId;
            Common.Execute_Procedures_Select_ByQuery(SqlMstr);

            foreach (RepeaterItem rpt in rptStores.Items)
            {
                HiddenField hfdPid=(HiddenField)rpt.FindControl("hfdPid");

                TextBox txtOffQty = (TextBox)rpt.FindControl("txtOffQty");
                decimal Qty = Common.CastAsDecimal(txtOffQty.Text);
                string vesselcode = txtOffQty.Attributes["vesslcode"].ToString();

                string SQL = "UPDATE  DBO.MP_VSL_StoreReqDetails1 SET OfficeQty=" + Qty + " WHERE StoreReqId= " + RequisitionId + " AND VesselCode='" + vesselcode + "' AND PID=" + hfdPid.Value;
                Common.Execute_Procedures_Select_ByQuery(SQL);
            }

            ProjectCommon.ShowMessage("Data updated Successfully.");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save.Error :" + ex.Message.ToString();
        }

    }
    protected void btnDelStore_Click(object sender, EventArgs e)
    {
        ImageButton b=(ImageButton)sender;
        int ItemId = Common.CastAsInt32(b.Attributes["ItemId"]);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_TMP_Stores WHERE USERID=" + UserId.ToString() + " AND VESSELCODE='" + VesselCode + "' AND ITEMID=" + ItemId);
        ShowTempStores();
        ResetAccountCodes();
    }
    public void ShowTempStores()
    {
        string sql = "SELECT VSIM.*,QTY,ROB " +
                        "FROM  " +
                        "DBO.MP_VSL_TMP_Stores TMP " +
                        "LEFT JOIN DBO.MP_VSL_StoreItemMaster VSIM ON TMP.VESSELCODE=VSIM.VESSELCODE AND TMP.ItemId=VSIM.ItemId " +
                        "WHERE TMP.USERID=" + UserId.ToString();

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptStores.DataSource = dtSpares;
        rptStores.DataBind();
    }
    protected void btnSearchStores_Click(object sender, EventArgs e)
    {
        string sql = "SELECT VSIM.*," +
                     "(SELECT QTY FROM DBO.MP_VSL_TMP_Stores TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSIM.VESSELCODE AND TMP.ItemId=VSIM.ItemId ) as ReqQty," +
                     "(SELECT ROB FROM DBO.MP_VSL_TMP_Stores TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSIM.VESSELCODE AND TMP.ItemId=VSIM.ItemId ) as ROBQty, " +
                     "(SELECT UOM FROM DBO.MP_VSL_TMP_Stores TMP WHERE TMP.USERID=" + UserId + " AND TMP.VESSELCODE=VSIM.VESSELCODE AND TMP.ItemId=VSIM.ItemId ) as UOM " +
                     "FROM DBO.MP_VSL_StoreItemMaster VSIM " +
                     "WHERE VESSELCODE='" + VesselCode + "' AND ( Description LIKE '%" + txt_F_ItemDesc.Text + "%' ) OR (ISSA_IMPA LIKE '%" + txt_F_ItemDesc.Text + "%')";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        lblRecCount.Text = "( " + dt.Rows.Count + " ) Records Found.";
        rptPopStores.DataSource = dt;
        rptPopStores.DataBind();
    }
    protected string[] LoadUnits()
    {
        string Units = ",AMPULE,B0TTLE,BOX,BAG,BAR,BKS,BUNDLE,CANS,CAPSUAL,CARTONS,CASE,CHART,CM,COIL,CONTAINER,CYLINDER,Dozen,Drum,Gallons,HOSE,HRS,JAR,KGS,KITS,LBS,LOAF,LTR,MTR,PACK,PCS,PAIR,REAM,ROLL,SACHETS,SACKS,SAMPLE,SET,SHACKLE,SHEET,SQ.METER,STEPS,STRIP,SUITS,TINS,TRAY,TRIP,TUG,UNIT,VOL,WKS,LENGTH";
        return Units.Split(',');
    }
    protected void imgbtnAddStore_Click(object sender, ImageClickEventArgs e)
    {
        StoreItemId = 0;
        lblerrorMsg.Text = "";
        dvStore.Visible = true;
        txt_F_ItemDesc.Focus();
    }
    protected void btnEditStore_Click(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        int ItemId = Common.CastAsInt32(b.Attributes["ItemId"]);
        StoreItemId = ItemId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MP_VSL_StoreItemMaster WHERE VESSELCODE='" + VesselCode + "' AND ITEMID=" + StoreItemId);
        if(dt.Rows.Count>0)
        {
            txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtAddISSA.Text = dt.Rows[0]["ISSA_IMPA"].ToString();
            txtROB.Text=((Label)b.Parent.FindControl("lblROBQty")).Text;
            txtQty.Text = ((TextBox)b.Parent.FindControl("txtReqQty")).Text;
            ddlUnit.SelectedValue = ((Label)b.Parent.FindControl("lblUnit")).Text;
            txtAccountCode.Text = dt.Rows[0]["AccountCode"].ToString();
        }
        lblerrorMsg.Text = "";
        dvStore.Visible = true;
        txt_F_ItemDesc.Focus();
    }
    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        if (RequisitionId > 0)
        {
            ShowRequisitionDetails();
        }
        else
        {
            ShowTempStores();
        }
        if (dvStore.Visible)
            btnSearchStores_Click(sender, e);
    }
   
    protected void btnAddToList_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem rpt in rptPopStores.Items)
            {
                HtmlInputCheckBox ch = (HtmlInputCheckBox)rpt.FindControl("chkSelect");
                if (ch.Checked)
                {
                    int ItemId = Common.CastAsInt32(ch.Attributes["ItemId"]);
                    decimal ROB = Common.CastAsDecimal(((Label)rpt.FindControl("lblROBQty")).Text);
                    decimal Qty = 0;
                    Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_TMP_AppUpdateStores " + UserId.ToString() + ",'" + VesselCode + "'," + ItemId + "," + Qty + "," + ROB);
                }
            }
            lblerrorMsg.Text = "Added successfully.";
        }
        catch (Exception ex)
        {
            lblerrorMsg.Text = "Unable add. Error : " + ex.Message.ToString();
        }
    }
    protected void btnAddStoreItem_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MP_VSL_AddModify_StoreItem " + UserId + "," + VesselCode + "," + Common.CastAsInt32(StoreItemId) + ",'" + txtItemName.Text.Trim() + "','" + txtDescription.Text.Replace("'", "`") + "','" + txtAddISSA.Text + "','" + ddlUnit.SelectedValue + "'," + Common.CastAsDecimal(txtROB.Text) + "," + Common.CastAsDecimal(txtQty.Text) + "," + Common.CastAsInt32(txtAccountCode.Text));
            ShowTempStores();
            btnCancel_Click(sender, e);
            lblMessage2.Text = "Added successfully.";
        }
        catch (Exception ex)
        {
            lblMessage2.Text = "Unable add. Error : " + ex.Message.ToString();
        }
    }
    
    protected void ResetAccountCodes()
    {
        ////-------------------
        //string LastValue = ddlAccCode.SelectedValue;

        //DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT ACCOUNTCODE FROM DBO.MP_VSL_StoreItemMaster WHERE VesselCode='" + VesselCode + "' AND ItemId IN (SELECT DISTINCT ItemID FROM DBO.MP_VSL_TMP_Stores WHERE USERID=" + UserId + ")");
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
        ShowTempStores();
        txtItemName.Text = "";
        txt_F_ItemDesc.Text = "";
        txtDescription.Text = "";
        txtAddISSA.Text = "";
        ddlUnit.SelectedIndex = 0;
        txtROB.Text = "";
        txtQty.Text = "";
        txtAccountCode.Text = "";
        lblMessage2.Text = "";
        rptPopStores.DataSource = null;
        rptPopStores.DataBind();
        ResetAccountCodes();
        dvStore.Visible = false;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        string cYear = DateTime.Today.Year.ToString().Substring(2);
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.MP_VSL_StoreReqMaster SET ReqnNo=(SELECT '" + VesselCode + "-" + cYear + "-T-' + REPLACE(STR(ISNULL(MAX(RIGHT(D.ReqnNo,3)),0)+1,3),' ','0') FROM MP_VSL_StoreReqMaster D WHERE RIGHT(LEFT(D.ReqnNo,6),2)='" + cYear + "'),ApprovedBy='" + UserName + "',ApprovedOn=GETDATE(),Updated=1,UpdatedOn=GETDATE() WHERE VESSELCODE='" + VesselCode + "' AND StoreReqId=" + RequisitionId);
        //btnApprove.Visible = false; 
        ShowRequisitionMaster();
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            //if (rptOtherItems.Items.Count > 0)
            //{
            //    lblMsg.Text = "Please link other items in order to transfer the RFQ.";
            //    return;
            //}
            //else
            //{
                Common.Set_Procedures("[dbo].[TRANSFTER_TO_POS_StoreReq1]");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(
                   new MyParameter("@VesselCode", VesselCode.Trim()),
                   new MyParameter("@StoreReqId", RequisitionId),
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
                    //btnSave.Visible = false;
                    btnTransfer.Visible = false;
                    //btnModify.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Unable to send for RFQ.";
                }
            //}
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
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.MP_VSL_StoreReqMaster1 SET ACTIVEINACTIVE='I',OfficeRemarks='" + txtOFCComments.Text.Trim().Replace("'", "`") + "',OfficeRemarksBy='" + UserName + "',OfficeRemarksOn=getdate() WHERE VESSELCODE='" + VesselCode + "' AND StoreReqId=" + RequisitionId);
        ShowRequisitionMaster();
        dvInactive.Visible = false;  
    }
    protected void btnCloseComm_Click(object sender, EventArgs e)
    {
        dvInactive.Visible = false;  
    }

    //-----------------------------------------------------------------------------    
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        hfdSelProdId.Value = "0";
        divForwardProduct.Visible = true;
        BindddlUnitFor();
        BindCatefory();
        BindSubCatefory();
        txtSearch.Focus();
        this.form1.DefaultButton = "btnFind";
    }
    protected void btnDeleteItem_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int pid=Common.CastAsInt32(btn.Attributes["pid"]);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_StoreReqDetails1 WHERE VESSELCODE='" + VesselCode + "' AND STOREREQID=" + RequisitionId + " AND PID=" + pid);
        ShowRequisitionDetails();
    }
    protected void btnDeleteExtraItem_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfdStoreReqId = (HiddenField)btn.Parent.FindControl("hfdStoreReqId");
        HiddenField hfdSno = (HiddenField)btn.Parent.FindControl("hfdSno");
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.MP_VSL_StoreReqDetails1_Others WHERE VESSELCODE='" + VesselCode + "' AND STOREREQID=" + hfdStoreReqId.Value + " AND SNO=" + hfdSno.Value);
        ShowTempStores_Other();
    }
    protected void btnCloseForwardProductPopup_OnClick(object sender, EventArgs e)
    {
        divForwardProduct.Visible = false;
    }    
    protected void btnSaveForward_OnClick(object sender, EventArgs e)
    {
        if (radtype.SelectedIndex == 0) // select item
        {
            if (Common.CastAsInt32(hfdSelProdId.Value)<=0)
            {
                lblMsgForward.Text = " Please select product to continue.";
                ddlCategory.Focus();
                return;
            }
            else if (Common.CastAsDecimal(txtQtyFor.Text) <= 0)
            {
                lblMsgForward.Text = " Please enter Qty.";
                txtQtyFor.Focus();
                return;
            }
            else if (Common.CastAsDecimal(txtRobFor.Text) < 0)
            {
                lblMsgForward.Text = " Invalid ROB.";
                txtRobFor.Focus();
                return;
            }
            else
            {
                try
                {
                    Common.Set_Procedures("[dbo].[MP_VSL_Forward1]");
                    Common.Set_ParameterLength(6);
                    Common.Set_Parameters(
                       new MyParameter("@ReqID", RequisitionId),
                       new MyParameter("@VesselCode", VesselCode),
                       new MyParameter("@ModifiedBy", UserName),
                       new MyParameter("@ProductId", Common.CastAsInt32(hfdSelProdId.Value)),
                       new MyParameter("@QTY", Common.CastAsDecimal(txtQtyFor.Text)),
                       new MyParameter("@ROB", Common.CastAsDecimal(txtRobFor.Text)));
                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res = false;
                    res = Common.Execute_Procedures_IUD(ds);
                    if (res)
                    {
                        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        lblMsgForward.Text = "Unable to add item.";
                    }
                }
                catch (Exception ex)
                {
                    lblMsgForward.Text = "Unable to add item. RFQ.Error :" + ex.Message.ToString();
                }
            }
        }
        else
        {

            if (ddlCategory.SelectedIndex == 0)
            {
                lblMsgForward.Text = " Please select category";
                ddlCategory.Focus();
                return;
            }
            if (ddlSubcategory.SelectedIndex == 0)
            {
                lblMsgForward.Text = " Please select sub category";
                ddlSubcategory.Focus();
                return;
            }

            if (txtProductNameFor.Text == "")
            {
                lblMsgForward.Text = " Please enter product name ";
                txtProductNameFor.Focus();
                return;
            }
            if (ddlUnitFor.SelectedIndex == 0)
            {
                lblMsgForward.Text = " Please select unit";
                txtProductNameFor.Focus();
                return;
            }
            if (txtImpaFor.Text == "")
            {
                lblMsgForward.Text = " Please enter IMPA";
                txtImpaFor.Focus();
                return;
            }

            try
            {
                Common.Set_Procedures("[dbo].[MP_VSL_Forward]");
                Common.Set_ParameterLength(10);
                Common.Set_Parameters(
                   new MyParameter("@ReqID", RequisitionId),
                   new MyParameter("@VesselCode", VesselCode),
                   new MyParameter("@PCode", ddlSubcategory.SelectedValue),
                   new MyParameter("@ProductName", txtProductNameFor.Text.Trim()),
                   new MyParameter("@QTY", Common.CastAsDecimal(txtQtyFor.Text)),
                   new MyParameter("@ROB", Common.CastAsDecimal(txtRobFor.Text)),
                   new MyParameter("@Unit", ddlUnitFor.SelectedValue),
                   new MyParameter("@IMPA", txtImpaFor.Text),
                   new MyParameter("@Status", ddlStatusFor.SelectedValue),
                   new MyParameter("@ModifiedBy", UserName)
                   );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res = false;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    int pid = Common.CastAsInt32(ds.Tables[0].Rows[0]["pid"]);
                    if (pid <= 0)
                    {
                        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        lblMsgForward.Text = "Unable to forward. Product with sane IMPA Code already exists in the database.";
                    }
                }
                else
                {
                    lblMsgForward.Text = "Unable to forward.";
                }
            }
            catch (Exception ex)
            {
                lblMsgForward.Text = "Unable to forward. RFQ.Error :" + ex.Message.ToString();
            }
        }
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCatefory();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        tvCategories.SelectedNode.Selected = false;
        txtSearch.Text = "";
        SelCatCode = "";
        rptProductLL.DataSource = null;
        rptProductLL.DataBind();
        
    }
    

    public void ShowTempStores_Other()

    {
        string sql = " SELECT TMP.* " +
              " FROM " +
              " dbo.MP_VSL_StoreReqDetails1_Others TMP " +
              " WHERE VesselCode='" + VesselCode + "' And StoreReqId=" + RequisitionId;

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptOtherItems.DataSource = dtSpares;
        rptOtherItems.DataBind();
    }
    public void BindddlUnitFor()
    {
        string sql = " Select UnitID,UnitName from dbo.tbl_StoreUnitMaster  ";

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlUnitFor.DataSource = dtSpares;
        ddlUnitFor.DataTextField = "UnitName";
        ddlUnitFor.DataValueField= "UnitID";
        ddlUnitFor.DataBind();
        ddlUnitFor.Items.Insert(0, new ListItem("Select", ""));
    }
    
    public void BindCatefory()
    {
        string sql = " select PID,PCode,PCode+' : '+Pname as CodeName from dbo.tblStoreItems where Plevel=1 order by PID ";

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlCategory.DataSource = dtSpares;
        ddlCategory.DataTextField = "CodeName";
        ddlCategory.DataValueField = "PCode";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("Select", ""));
    }
    public void BindSubCatefory()
    {
        string sql = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,Active from dbo.tblStoreItems where PCode like '"+ddlCategory.SelectedValue+".%' and Plevel=2 order by PID   ";

        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlSubcategory.DataSource = dtSpares;
        ddlSubcategory.DataTextField = "CodeName";
        ddlSubcategory.DataValueField = "PCode";
        ddlSubcategory.DataBind();
        ddlSubcategory.Items.Insert(0, new ListItem("Select", ""));
    }
    private void BindCategories()
    {
        DataTable dtCategory = new DataTable();
        string sqlCategory = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,Active from dbo.tblStoreItems where Plevel=1 order by PID ";

        dtCategory = Common.Execute_Procedures_Select_ByQuery(sqlCategory);
        tvCategories.Nodes.Clear();

        if (dtCategory.Rows.Count > 0)
        {
            for (int i = 0; i < dtCategory.Rows.Count; i++)
            {
                TreeNode gn = new TreeNode();
                gn.Text = dtCategory.Rows[i]["CodeName"].ToString();
                gn.Value = dtCategory.Rows[i]["PID"].ToString();
                gn.ToolTip = dtCategory.Rows[i]["CodeName"].ToString();
                gn.Expanded = false;
                DataTable dtSubCategory;
                string sql = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,Active from dbo.tblStoreItems where PCode like '" + dtCategory.Rows[i]["PCode"].ToString() + ".%' and Plevel=2 order by PID ";
                dtSubCategory = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dtSubCategory != null)
                {
                    for (int j = 0; j < dtSubCategory.Rows.Count; j++)
                    {
                        TreeNode sn = new TreeNode();
                        sn.Text = dtSubCategory.Rows[j]["CodeName"].ToString();
                        sn.Value = dtSubCategory.Rows[j]["PID"].ToString();
                        sn.ToolTip = dtSubCategory.Rows[j]["CodeName"].ToString();
                        sn.Expanded = false;
                        if (dtSubCategory.Rows[j]["Active"].ToString() == "False")
                        {
                            sn.Text += "<i style='color:#d81818;font-size:10px;'>(&nbsp;Inactive)<i>";
                        }
                        gn.ChildNodes.Add(sn);
                    }
                }
                if (dtCategory.Rows[i]["Active"].ToString() == "False")
                {
                    gn.Text += "<i style='color:#d81818;font-size:10px;'>(&nbsp;Inactive)<i>";
                }
                tvCategories.Nodes.Add(gn);
            }
        }
    }
    protected void tvCategories_SelectedNodeChanged(object sender, EventArgs e)
    {

        string[] CatCode = tvCategories.SelectedNode.Text.Trim().Split(':');
        SelCatCode = CatCode[0].Trim();        
        BindProducts();
    }
    protected void tvCategories_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {

        DataTable dtProducts;
        string strProducts = "SELECT CategoryId,CategoryName,Inactive FROM [dbo].[MP_MainCategory] WHERE ParentCategoryId =" + e.Node.Value.Trim() + " AND Level = 'P' Order By CategoryName";
        string sqlCategory = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName from dbo.tblStoreItems where Active=1 and Plevel=1 order by PID ";

        dtProducts = Common.Execute_Procedures_Select_ByQuery(sqlCategory);
        if (dtProducts != null)
        {
            for (int k = 0; k < dtProducts.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtProducts.Rows[k]["CodeName"].ToString();
                ssn.Value = dtProducts.Rows[k]["PID"].ToString();
                ssn.ToolTip = dtProducts.Rows[k]["CodeName"].ToString();
                ssn.Expanded = false;
                e.Node.ChildNodes.Add(ssn);
            }
        }
    }
   
}