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
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;


public partial class UserControls_AddLandedGoods : System.Web.UI.UserControl
{
    #region Properties  ************************************************************************
    public int PRId
    {
        get { return Convert.ToInt32(ViewState["_PRId"]); }
        set { ViewState["_PRId"] = value; }
    }
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public string vesselList
    {
        get { return ViewState["VslList"].ToString(); }
        set { ViewState["VslList"] = value; }
    }
    public string AccountList
    {
        get { return ViewState["AccList"].ToString(); }
        set { ViewState["AccList"] = value; }
    }
#endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region --------- USER RIGHTS MANAGEMENT -----------
        AuthenticationManager authPR = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        try
        {
            
            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        lblMsg.Text = "";
       
        txtcreated.Text = Session["UserName"].ToString();

        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["ViewPage"] == "true")
                imgSave.Visible = false;
            else imgSave.Visible = true;
            BindPRRepeter();
            BindVessel();
            BindDepartment();
            txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            BindAccount();
            if (PRId>0)
            {   
                ShowPRData();
                GetDocCount("PR", ddlvessel.SelectedValue, PRId);
            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("DELETE FROM MP_VSL_TEMP_RequisitionDocuments WHERE LoginId=" + Convert.ToInt32(Session["loginid"].ToString()) + " AND PRType='PR'");
                GetDocCount("PR", ddlvessel.SelectedValue, 0);
            }
            //if (!IsReveivedFromVessel())
            //{
            //    txtprno.Enabled = false;
            //}
            //else
            //{
            //    txtprno.Enabled = true;
            //}
        }
    }
    protected void clearLandedGoodsPR()
    {
        ddlvessel.SelectedIndex = 0;
        txtprno.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddlAccountCode.SelectedIndex = 0;
        txtPort.Text = "";
        txtETA.Text = "";
        txtsmdremarks.Text = "";
        txtcrewno.Text = "";
        txtdatecreated.Text = "";
        ddlpriority.SelectedIndex = 0;
        txtportdelv.Text = "";
        txtlndDate.Text = "";
        txtLndPort.Text = "";
        chkRepair.Checked = false;
        chkStorage.Checked = false;
        txtReqTitle.Text = "";
        BindPRRepeter();
        lblAttchmentCount.Text = "0";
        ImgAttachment.Enabled = true;
    }

    #region Function  ***********************************************************************

    // Loader -------------------------------------------------------
    public void BindPRRepeter()
    {
        string sql = "select ROW_NUMBER() OVER (ORDER BY recid) AS 'RowNumber',convert(varchar,targetCompDate,106)as targetCompDate1, *, '' As POStatusId from  vw_tblSMDPODetail where 1=2";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        if (dsPrType != null)
        {
            DataRow dr = dsPrType.Tables[0].NewRow();
            dr["UOM"] = "";
            dsPrType.Tables[0].Rows.InsertAt(dr, 0);
            rptData.DataSource = dsPrType;
            Session["SLNDItems"] = dsPrType;  
            rptData.DataBind();

            HFTotalGrdRow.Value = rptData.Items.Count.ToString();
        }

    }
    public void BindVessel()
    {
        string sql = "SELECT vw.ShipID, vw.ShipID+' - '+vw.ShipName As ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        ddlvessel.Items.Insert(0, new ListItem("<Select>", "0"));
        ddlvessel.SelectedIndex = 0;

        string vsllst = "";
        for (int i = 0; i < dsPrType.Tables[0].Rows.Count ; i++)
        {
            ListItem li = new ListItem(dsPrType.Tables[0].Rows[i]["ShipName"].ToString(), dsPrType.Tables[0].Rows[i]["ShipId"].ToString());
            ddlvessel.Items.Add(li); 
            vsllst = vsllst + ",'" + dsPrType.Tables[0].Rows[i]["ShipName"].ToString() + "'";
        }
        if (vsllst.Trim() != "") { vsllst = vsllst.Substring(1); }
        vesselList = vsllst; 
    }
    public void BindDepartment()
    {
        //string sql = "select dept,deptname from  VW_sql_tblSMDPRDept";
        string sql = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID where  acc.Active = 'Y'  Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddldepartment.DataSource = dsPrType;
        //ddldepartment.DataTextField = "deptname";
        //ddldepartment.DataValueField = "dept";
        ddldepartment.DataTextField = "MidCat";
        ddldepartment.DataValueField = "MidCatID";
        ddldepartment.DataBind();
        ddldepartment.Items.Insert(0, new ListItem("<Select>", "0"));
        ddldepartment.SelectedIndex = 0;
    }
    public void BindAccount()
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + ddldepartment.SelectedValue + "' and prtype=3) dd where AccountNumber is not null";
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountName  ,AccountID from VW_sql_tblSMDPRAccounts DA where DA.MidCatID='" + ddldepartment.SelectedValue + "') dd where AccountNumber is not null";
        string dbname = "";
        dbname = ConfigurationManager.AppSettings["DBName"].ToUpper().ToString();
        string sql = "EXEC GetBudgetAccountList '" + ddlvessel.SelectedValue + "'," + ddldepartment.SelectedValue + ", '"+ dbname + "'";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlAccountCode.DataSource = dsPrType;
        ddlAccountCode.DataTextField = "AccountNumber";
        ddlAccountCode.DataValueField = "AccountID";
        ddlAccountCode.DataBind();
        ddlAccountCode.Items.Insert(0, new ListItem("<Select>", "0"));
        ddlAccountCode.SelectedIndex = 0;

        string AccLst = "";
        AccountList = AccLst;
    }
    //public void BindAccount()
    //{
    //   // string sql = "select AccountID,AccountName from  VW_sql_tblSMDPRAccounts where AccountNumber not like '17%' and AccountNumber !=8590";
    //    string sql = "select (select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + ddldepartment.SelectedValue + "' and prtype=3";
    //    Common.Set_Procedures("ExecQuery");
    //    Common.Set_ParameterLength(1);
    //    Common.Set_Parameters(new MyParameter("@Query", sql));
    //    DataSet dsPrType = new DataSet();
    //    dsPrType.Clear();
    //    dsPrType = Common.Execute_Procedures_Select();

    //    ddlAccountCode.DataSource = dsPrType;
    //    ddlAccountCode.DataTextField = "AccountName";
    //    ddlAccountCode.DataValueField = "AccountID";
    //    ddlAccountCode.DataBind();
    //    ddlAccountCode.Items.Insert(0, new ListItem("<Select>", "0"));
    //    ddlAccountCode.SelectedIndex = 0;
    //}
    public DataTable BindUOM()
    {
        string sql = "select * from tblUOM order by uom asc";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        return dsPrType.Tables[0];
    }
    
    // Validatioin --------------------------------------------------
    public bool SparemasterValidation()
    {
        // Requisition Title
        if (string.IsNullOrWhiteSpace(txtReqTitle.Text.Trim()))
        {
            lblMsg.Text = "Please enter Requisition Title";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtReqTitle.ClientID + "').focus()", true);
            return false;
        }
        //vessel

        if (ddlvessel.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Vessel";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + ddlvessel.ClientID + "').focus()", true);
            return false;
        }
        //PR Number
        //if (txtprno.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter PR Number ";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtprno.ClientID + "').focus()", true);
        //    return false;
        //}
        //Date 
        if (txtDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Date ";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtDate.ClientID + "').focus()", true);
            return false;
        }
        //ddldepartment
        if (ddldepartment.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Department ";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + ddldepartment.ClientID + "').focus()", true);
            return false;
        }
        //ddlAccountCode
        if (ddlAccountCode.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select AccountCode ";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + ddlAccountCode.ClientID + "').focus()", true);
            return false;
        }
        //txtcreated
        if (txtcreated.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Submitted By  ";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtcreated.ClientID + "').focus()", true);
            return false;
        }
        return true;
    }
    public bool ItemValidation()
    {
        if (rptData.Items.Count > 0)
        {
            RepeaterItem RptItm = (RepeaterItem)rptData.Items[0];

            TextBox txtAddedDesc = (TextBox)RptItm.FindControl("txtAddedDesc");
            DropDownList ddlUOM = (DropDownList)RptItm.FindControl("ddlUOM");
            TextBox txtQuantity = (TextBox)RptItm.FindControl("txtQuantity");
            
            //Description
            if (txtAddedDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Description field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtAddedDesc.ClientID + "').focus()", true);
                return false;
            }
            //Quantity 
            if (txtQuantity.Text.Trim() == "")
            {
                lblMsg.Text = "Quantity field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtQuantity.ClientID + "').focus()", true);
                return false;
            } 
            //if (Validations.Isinteger(txtQuantity.Text.Trim()) == false)
            //{
            //    lblMsg.Text = "Quantity field must be integer ";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtQuantity.ClientID + "').focus()", true);
            //    return false;
            //}
        }
        return true;
    }

    // --------------------------------------------------------------
    public void FillDataINDataSet()
    {
        int ItmIndex = 0;
        DataSet PrDataSet = (DataSet)Session["SLNDItems"];
        object DtTargetDate;
        foreach (RepeaterItem tprItm in rptData.Items)
        {
            TextBox txtAddedDesc = (TextBox)tprItm.FindControl("txtAddedDesc");
            DropDownList ddlUOM = (DropDownList)tprItm.FindControl("ddlUOM");
            TextBox txtQuantity = (TextBox)tprItm.FindControl("txtQuantity");

            //TextBox txttargetCompDate = (TextBox)tprItm.FindControl("txttargetCompDate");
            //if (txttargetCompDate.Text.Trim() != "") DtTargetDate = txttargetCompDate.Text.Trim(); else DtTargetDate = DBNull.Value;

            PrDataSet.Tables[0].Rows[ItmIndex]["Description"] = txtAddedDesc.Text;
            PrDataSet.Tables[0].Rows[ItmIndex]["UOM"] = ddlUOM.SelectedValue;
            PrDataSet.Tables[0].Rows[ItmIndex]["Qty"] = Common.CastAsDecimal(txtQuantity.Text);
            PrDataSet.Tables[0].Rows[ItmIndex]["targetCompDate1"] = DBNull.Value;

            ItmIndex = ItmIndex + 1;
        }
        Session["SLNDItems"] = PrDataSet;
    }
    public void SetSerialNumberToRptData()
    {
        int RptLnt = rptData.Items.Count;
        int sno = 1;
        for (int i = RptLnt - 1; i >= 0; i--)
        {
            Label lblRowNumber = (Label)rptData.Items[i].FindControl("lblRowNumber");
            lblRowNumber.Text = Convert.ToString(i+1);
        }
    }
    public void ShowPRData()
    {
        Common.Set_Procedures("sp_NewPR_getPRMaster");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@PRId", PRId));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        if (dsPrType != null)
        {
            if (dsPrType.Tables[0].Rows.Count != 0)
            {
                ddlvessel.SelectedValue = dsPrType.Tables[0].Rows[0]["ShipID"].ToString();
                txtprno.Text = dsPrType.Tables[0].Rows[0]["ReqNo"].ToString();
                txtDate.Text = dsPrType.Tables[0].Rows[0]["Podate1"].ToString();
                // ddldepartment.SelectedValue = dsPrType.Tables[0].Rows[0]["Dept"].ToString();
                ddldepartment.SelectedValue = dsPrType.Tables[0].Rows[0]["MidCatId"].ToString();
                BindAccount();
                ddlAccountCode.SelectedValue = dsPrType.Tables[0].Rows[0]["AccountID"].ToString();
                txtPort.Text = dsPrType.Tables[0].Rows[0]["Port"].ToString();
                txtETA.Text = dsPrType.Tables[0].Rows[0]["ETADate"].ToString();
                txtcreated.Text = dsPrType.Tables[0].Rows[0]["POCreator"].ToString();
                txtsmdremarks.Text = dsPrType.Tables[0].Rows[0]["RemarksSMD"].ToString();
                txtReqTitle.Text = dsPrType.Tables[0].Rows[0]["RequisitionTitle"].ToString();
                chkUrgent.Checked = Convert.ToBoolean(dsPrType.Tables[0].Rows[0]["IsUrgent"].ToString()) ? true : false;
                chkFastTrack.Checked = Convert.ToBoolean(dsPrType.Tables[0].Rows[0]["IsFastTrack"].ToString()) ? true : false;
                chkSafetyUrgent.Checked = Convert.ToBoolean(dsPrType.Tables[0].Rows[0]["IsSafetyUrgent"].ToString()) ? true : false;
                //if (Convert.ToString(dsPrType.Tables[0].Rows[0]["FileName"]) != "")
                //{
                //    ImgAttachment.Visible = true;
                //}
                //else
                //{
                //    ImgAttachment.Visible = false;
                //}
                // Show added information
                txtlndDate.Text = dsPrType.Tables[0].Rows[0]["landedDate1"].ToString();
                txtLndPort.Text = dsPrType.Tables[0].Rows[0]["landedport"].ToString();
                chkStorage.Checked = (dsPrType.Tables[0].Rows[0]["LandedStorage"].ToString() == "True") ? true : false;
                chkRepair.Checked = (dsPrType.Tables[0].Rows[0]["landedRepairs"].ToString() == "True") ? true : false;

                //bind Item Details section
                Common.Set_Procedures("sp_NewPR_getPRDetail");
                Common.Set_ParameterLength(1);
                Common.Set_Parameters(new MyParameter("@PRId", PRId));
                dsPrType = new DataSet();
                dsPrType.Clear();
                dsPrType = Common.Execute_Procedures_Select();
                if (dsPrType != null)
                {
                    Session["SLNDItems"] = dsPrType;
                    if (dsPrType.Tables[0].Rows.Count != 0)
                    {
                        rptData.DataSource = dsPrType;
                        rptData.DataBind();
                        HFTotalGrdRow.Value = rptData.Items.Count.ToString();
                        SetSerialNumberToRptData();

                    }
                    else
                    {
                        lblMsg.Text = "No Data Found ";
                    }
                }
                else
                {
                    lblMsg.Text = "No Data Found ";
                }
            }
        }
    }
    public bool IsReveivedFromVessel()
    {
        if (ViewState["IsRcvFromVessel"] != null)
        {
            if (ViewState["IsRcvFromVessel"].ToString() == "0")
                return true;
            else
                return false;
        }
        else
        {
            if (PRId != 0)
            {
                string sql = "select ReceivedFrom from Vw_tblsmdpomaster where poid=" + PRId + "";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "0")
                        {
                            ViewState["IsRcvFromVessel"] = "0";
                            return true;
                        }
                        else if (dt.Rows[0][0].ToString() == "1")
                        {
                            ViewState["IsRcvFromVessel"] = "1";
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public void ShowSaveButton()
    {
        imgSave.Visible = true;
    }
    #endregion

    #region Events **************************************************************************

    // Button ----------------------------------------------------
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        // Item Validation
        if (ItemValidation() == false) return; else lblMsg.Text = "";

        //Fill data set by the values entered in the textbox
        FillDataINDataSet();

        // Add a row to dataset
        DataSet PrDataSet = (DataSet)Session["SLNDItems"];
        DataRow dr = PrDataSet.Tables[0].NewRow();
        dr["UOM"] = "";
        dr["RecID"] = "0";
        PrDataSet.Tables[0].Rows.InsertAt(dr, 0);
        rptData.DataSource = PrDataSet;

        //BindUOM(ddlUOM);
        Session["SLNDItems"] = PrDataSet;
        rptData.DataBind();

        HFTotalGrdRow.Value = rptData.Items.Count.ToString();
        SetSerialNumberToRptData();
    }
    public void imgDelete_Click(object sender, EventArgs e)
    {
        //Fill data set by the values entered in the textbox
        FillDataINDataSet();

        ImageButton imgDelete = (ImageButton)sender;
        int repeaterItemIndex = ((RepeaterItem)imgDelete.NamingContainer).ItemIndex;
        DataSet PrDataSet = (DataSet)Session["SLNDItems"];
        PrDataSet.Tables[0].Rows.RemoveAt(repeaterItemIndex);
        Session["SLNDItems"] = PrDataSet;

        rptData.DataSource = PrDataSet;
        rptData.DataBind();
        SetSerialNumberToRptData();
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        try
        {  
            //Validation of Speare Master Data
            if (SparemasterValidation() == false) return; else lblMsg.Text = "";
            // Validation for   Speare Details Data
            if (ItemValidation() == false) return; else lblMsg.Text = "";

            //string FileName = "";
            //    byte[] ImageAttachment ;
            //if (FU.HasFile)
            //{
            //    if (FU.PostedFile.ContentLength > (1024 * 1024 * 2))
            //    {
            //        lblMsg.Text = "File Size is Too big! Maximum Allowed is 2MB...";
            //        FU.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        FileName = FU.FileName;
            //        // ImageAttachment = FU.FileBytes;
            //    }
            //}
            // Insert Speare Master Data
            object DtPodate;
            object DtETA;
            if (txtDate.Text.Trim() != "")
                DtPodate = Convert.ToDateTime(txtDate.Text.Trim());
            else
                DtPodate = DBNull.Value;

            if (txtETA.Text.Trim() != "")
                DtETA = Convert.ToDateTime(txtETA.Text.Trim());
            else
                DtETA = DBNull.Value;

            //Get all RecID By Comma Separated
            string AllRecID = "";
            foreach (RepeaterItem RptItm in rptData.Items)
            {
                HiddenField RecID = (HiddenField)RptItm.FindControl("hfRecID");
                AllRecID = AllRecID + "," + RecID.Value;
            }
            if (!string.IsNullOrEmpty(AllRecID))
            {
                AllRecID = AllRecID.Substring(1);
            }       
            if (AllRecID == "")
                AllRecID = "0";


            string POComments = txtprno.Text.Trim() + "- port : " + txtPort.Text.Trim() + "- ETA : " + txtETA.Text.Trim();
            Common.Set_Procedures("sp_NewPR_InsertLandedGoodsMaster_Office");
            Common.Set_ParameterLength(36);
            Common.Set_Parameters   
                (
                new MyParameter("@PODate", DtPodate),
                new MyParameter("@POCreator", txtcreated.Text.Trim()),
                new MyParameter("@ShipID", ddlvessel.SelectedValue),
                new MyParameter("@InvoiceReceived", "0"),
                new MyParameter("@InvoicePosted", "0"),
                new MyParameter("@LocalMasterID", "0"),
                new MyParameter("@Locked", "0"),
                new MyParameter("@DateCreated", System.DateTime.Now.ToShortDateString()),
                new MyParameter("@PoComments", POComments),
                new MyParameter("@POCommentsVen", ""),
                new MyParameter("@StatusID", "0"), 
                new MyParameter("@HighPriority", "0"),
                new MyParameter("@OrigPOID", "0"),
                new MyParameter("@CopyEquip", "0"),
                new MyParameter("@Dept", ""),
                new MyParameter("@PRType", "3"),
                new MyParameter("@VerNo", "0"),
                new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
                new MyParameter("@RemarksSMD", txtsmdremarks.Text.Trim()),
                new MyParameter("@LandedRepairs", (chkRepair.Checked==true)?"1":"0"),
                new MyParameter("@LandedStorage", (chkStorage.Checked==true)?"1":"0"),
                new MyParameter("@isClosed", false),
                new MyParameter("@MultiVess", false),
                new MyParameter("@ReqNo", txtprno.Text.Trim()),
                new MyParameter("@PoID", PRId),
                new MyParameter("@Port", txtPort.Text.Trim()),
                new MyParameter("@Eta", DtETA),
                new MyParameter("@LandedPort", (txtLndPort.Text.Trim())),
                new MyParameter("@LandedDate", (txtlndDate.Text.Trim())),
                new MyParameter("@AllRecID", AllRecID),
                new MyParameter("@MidCatId", Convert.ToInt32(ddldepartment.SelectedValue)),
                new MyParameter("@ReqTitle", txtReqTitle.Text.Trim()),
                new MyParameter("@LoginId", Convert.ToInt32(Session["loginid"].ToString())) ,
                new MyParameter("@IsUrgent", chkUrgent.Checked ? 1 : 0),
                new MyParameter("@IsFastTrack", chkFastTrack.Checked ? 1 : 0),
                new MyParameter("@IsSafetyUrgent", chkSafetyUrgent.Checked ? 1 : 0)
                //new MyParameter("@FileName", FileName.Trim()),
                //new MyParameter("@Attachment", FU.FileBytes)
                );
            Boolean Res;
            DataSet ResDS = new DataSet();
            Res = Common.Execute_Procedures_IUD(ResDS);

            int Poid = Convert.ToInt32(ResDS.Tables[0].Rows[0][0]);

            // Insert Speare Items Detail Data
            
            object DTTargetCompDate;
            foreach (RepeaterItem RptItm in rptData.Items)
            {
                TextBox txtAddedDesc = (TextBox)RptItm.FindControl("txtAddedDesc");
                DropDownList ddlUOM = (DropDownList)RptItm.FindControl("ddlUOM");
                TextBox txtQuantity = (TextBox)RptItm.FindControl("txtQuantity");
                //TextBox txtTargetCompDate = (TextBox)RptItm.FindControl("txtTargetCompDate");
                HiddenField RecID = (HiddenField)RptItm.FindControl("hfRecID");

                //if (txtTargetCompDate.Text.Trim() != "")
                //    DTTargetCompDate = Convert.ToDateTime(txtTargetCompDate.Text.Trim());
                //else
                //    DTTargetCompDate = DBNull.Value;
                DTTargetCompDate = DBNull.Value;

                Common.Set_Procedures("sp_NewPR_InsertLandedGoodsDetails_Office");
                Common.Set_ParameterLength(8);
                Common.Set_Parameters
                    (
                        new MyParameter("@POID", Poid), 
                        new MyParameter("@Qty", txtQuantity.Text.Trim()),
                        new MyParameter("@Description", txtAddedDesc.Text.Trim()),
                        new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
                        new MyParameter("@UOM", ddlUOM.SelectedValue),
                        new MyParameter("@HasEquip", "0"),
                        new MyParameter("@targetCompDate", DTTargetCompDate),
                        new MyParameter("@RecID", RecID.Value)
                    );

                ResDS = new DataSet();
                Res = false;
                
                Res = Common.Execute_Procedures_IUD(ResDS);
            }

            if (PRId != 0)
            {
                // lblMsg.Text = "Data updated successfully";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Data updated successfully.');", true);
                Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
            }
            else
            {
                lblMsg.Text = "Data saved successfully";
                clearLandedGoodsPR();
            }
        }
       catch(Exception ex)
       {
           lblMsg.Text = " Error while saving -: "+ex.Message;
       }
    }
    public void btnBack_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }
    public void btncancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }
    protected void imgCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
    }
    protected void ImagePrint_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Print.aspx?PRID=" + PRId + "&PRType=3");
    }

    // Repeater --------------------------------------------------
    public void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {   
        DropDownList ddlUOM = (DropDownList)e.Item.FindControl("ddlUOM");
        ImageButton imgDelete = (ImageButton)e.Item.FindControl("imgDelete");
        HiddenField hdnStatusId = (HiddenField)e.Item.FindControl("hdnPOStatusId");

        // visibility management of item delete button
        if (IsReveivedFromVessel() == true)
            imgDelete.Visible = false;
        else
            imgDelete.Visible = true;

        if (hdnStatusId.Value != "")
        {
            if (Convert.ToInt32(hdnStatusId.Value) > 0)
                imgDelete.Visible = false;
            else
                imgDelete.Visible = true;
        }

        DataRowView drv = (DataRowView)e.Item.DataItem;
        string UOM = drv.Row["UOM"].ToString();
        ListItem li = ddlUOM.Items.FindByValue(UOM);

        ddlUOM.DataSource = BindUOM();
        ddlUOM.DataTextField = "UOM";
        ddlUOM.DataValueField = "UOM";
        ddlUOM.DataBind();
        if (li == null)
        {
            ddlUOM.Items.Add(new ListItem(UOM, UOM));
        }
        ddlUOM.SelectedValue = UOM;
    }

    // DropDown --------------------------------------------------
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (ddlvessel.SelectedIndex == 0)
        {
            return;
        }
        BindAccount();
    }
    //protected void ImgAttachment_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (PRId > 0)
    //    {
    //        string sql = "select FileName,Attachment from [tblSMDPOMaster] where poid=" + PRId.ToString() + "";
    //        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    //        string sDocName = "";
    //        if (dt.Rows.Count > 0)
    //        {
    //            try
    //            {
    //                sDocName = dt.Rows[0]["FileName"].ToString();
    //                byte[] DocFile = (byte[])dt.Rows[0]["Attachment"];

    //                Response.Clear();
    //                Response.ClearContent();
    //                Response.ClearHeaders();

    //                if (sDocName.EndsWith(".txt"))
    //                {
    //                    Response.ContentType = "text/plain";
    //                }
    //                if (sDocName.EndsWith(".xls"))
    //                {
    //                    Response.ContentType = "application/vnd.xls";
    //                }
    //                if (sDocName.EndsWith(".doc"))
    //                {
    //                    Response.ContentType = "application/ms-word";
    //                }
    //                if (sDocName.EndsWith(".pfd"))
    //                {
    //                    Response.ContentType = "application/pdf";
    //                }
    //                if (sDocName.EndsWith(".zip"))
    //                {
    //                    Response.ContentType = "application/x-zip-compressed";
    //                }
    //                if (sDocName.EndsWith(".gif"))
    //                {
    //                    Response.ContentType = "image/gif";
    //                }
    //                if (sDocName.EndsWith(".jpeg"))
    //                {
    //                    Response.ContentType = "image/jpeg";
    //                }
    //                if (sDocName.EndsWith(".png"))
    //                {
    //                    Response.ContentType = "image/png";
    //                }
    //                if (sDocName.EndsWith(".png"))
    //                {
    //                    Response.ContentType = "text/xml";
    //                }


    //                Response.AddHeader("Content-Disposition", "attachment; filename=" + sDocName);
    //                //Response.AddHeader("Content-Length", sDocName.Length.ToString());
    //                Response.OutputStream.Write(DocFile, 0, DocFile.Length - 1);
    //                //Response.End();

    //            }
    //            catch (Exception ex)
    //            {

    //                Response.Clear();
    //                Response.Write("<center> Invalid File !</center>");
    //                Response.End();
    //            }
    //        }
    //    }

    //}
    #endregion

    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlvessel.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select vessel.";
                ddlvessel.Focus();
                return;
            }
            if (rptDocuments.Items.Count >= 4)
            {
                lblMsg.Text = "Maximum 4 documents allowed, 200KB each.";
                return;
            }
            //string FileName = "";
            //    byte[] ImageAttachment ;
            if (FU.HasFile)
            {
                if (FU.PostedFile.ContentLength > (1024 * 1024 * 0.2))
                {
                    lblMsg.Text = "File Size is Too big! Maximum Allowed is 200KB...";
                    FU.Focus();
                    return;
                }
                //else
                //{
                //    FileName = FU.FileName;
                //    // ImageAttachment = FU.FileBytes;
                //}

                int storeReqId = PRId > 0 ? PRId : 0;
                string FileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileContent = FU.PostedFile.ContentType;
                Stream fs = FU.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                Common.Set_Procedures("[dbo].[InsertUpdate_RequisitionDocuments]");
                Common.Set_ParameterLength(7);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", ddlvessel.SelectedValue),
                    new MyParameter("@PoId", PRId),
                    new MyParameter("@PRType", "PR"),
                    new MyParameter("@UserId", Convert.ToInt32(Session["loginid"].ToString())),
                    new MyParameter("@DocName", FileName),
                    new MyParameter("@Doc", bytes),
                    new MyParameter("@ContentType", fileContent)
                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    ProjectCommon.ShowMessage("Document saved Successfully.");
                    ShowAttachment();
                    GetDocCount("PR", ddlvessel.SelectedValue, PRId);
                }
                else
                {
                    lblMsg.Text = "Unable to add Document.Error :" + Common.getLastError();
                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
    public void ShowAttachment()
    {
        string sql = "";
        if (PRId > 0)
        {
            sql = "SELECT DocId, DocName As FileName, PoId As RequisitionId, VesselCode, (Select top 1 StatusID from tblSMDPOMaster p where p.PoId= Pod.PoId) As StatusId FROM [tblSMDPODocuments] Pod with(nolock) WHERE Pod.[VesselCode] = '" + ddlvessel.SelectedValue + "' AND  Pod.PoId =" + PRId;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            rptDocuments.DataSource = DT;
            rptDocuments.DataBind();
        }
        else
        {
            sql = "SELECT DocId, DocName As FileName,0 As RequisitionId, VesselCode, 0 As StatusId  FROM [MP_VSL_TEMP_RequisitionDocuments] with(nolock) WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " and PRType = 'PR'";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            rptDocuments.DataSource = DT;
            rptDocuments.DataBind();
        }

    }
    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            string sql = "";
            if (PRId > 0)
            {
                sql = "Delete from MP_VSL_StoreReqDocument  WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  PoId =" + PRId + " AND DocId = " + DocId;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            }
            else
            {
                sql = "Delete from MP_VSL_TEMP_RequisitionDocuments WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " AND DocId = " + DocId + " AND PRType='PR' ";
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            }
            ShowAttachment();
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }

    }
    protected void GetDocCount(string PRType, string Vesselcode, int POid)
    {
        string sql = "";
        if (POid > 0)
        {
            sql = "SELECT Count(DocId) As DocumentCount FROM [tblSMDPODocuments] with(nolock) WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  PoId =" + POid;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblAttchmentCount.Text = DT.Rows[0]["DocumentCount"].ToString();
        }
        else
        {
            sql = "SELECT Count(DocId) As DocumentCount FROM [MP_VSL_TEMP_RequisitionDocuments] with(nolock) WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " and PRType = 'PR'";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblAttchmentCount.Text = DT.Rows[0]["DocumentCount"].ToString();
        }
        if (Convert.ToInt32(lblAttchmentCount.Text) > 0)
        {
            ImgAttachment.Enabled = true;
        }
        else
        {
            ImgAttachment.Enabled = false;
        }
    }
    protected void ImgAttachment_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(lblAttchmentCount.Text) <= 0)
        {
            return;
        }
        if (ddlvessel.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select vessel.";
            ddlvessel.Focus();
            return;
        }
        divAttachment.Visible = true;
        ShowAttachment();
    }
    protected void btnPopupAttachment_Click(object sender, EventArgs e)
    {
        divAttachment.Visible = false;
    }

    protected void imgClosePopup_Click(object sender, ImageClickEventArgs e)
    {
        divAttachment.Visible = false;
    }

    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex == 0)
        {
            return;
        }
        BindAccount();
    }
}
