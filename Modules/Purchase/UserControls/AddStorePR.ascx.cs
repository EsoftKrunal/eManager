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
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;


public partial class UserControls_AddStorePR : System.Web.UI.UserControl
{
    #region Properties *******************************************
    public int PRId
    {
        get { return Convert.ToInt32(ViewState["_PRId"]); }
        set { ViewState["_PRId"] = value; }
    }
    public string vesselList
    {
        get { return ViewState["VslList"].ToString(); }
        set { ViewState["VslList"] = value; }
    }
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public string AccountList
    {
        get { return ViewState["AccList"].ToString(); }
        set { ViewState["AccList"] = value; }
    }
#endregion
    DataTable s_dtUOM;
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
            BindVessel();
            BindDepartment();
            BindAccount();
            txtDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            if (PRId>0)
            {   
                ShowPRData();
                GetDocCount("SS", ddlvessel.SelectedValue, PRId);
            }
            else
            {
                BindItemsRepeater();
                Common.Execute_Procedures_Select_ByQuery("DELETE FROM MP_VSL_TEMP_RequisitionDocuments WHERE LoginId=" + Convert.ToInt32(Session["loginid"].ToString()) + " AND PRType='SS'");
                GetDocCount("SS", ddlvessel.SelectedValue, 0);
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

        string sql = "select * from tblUOM order by uom asc";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        s_dtUOM = dsPrType.Tables[0];
    }

    #region Function  ********************************************

    //Loader ------------------------------------------
    public void BindVessel()
    {
        string sql = "SELECT vw.ShipID, vw.ShipID+' - '+vw.ShipName As ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        //ddlvessel.DataSource = dsPrType.Tables[0];
        //ddlvessel.DataTextField = "ShipName";
        //ddlvessel.DataValueField = "ShipID";
        //ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("<Select>", "0"));
        ddlvessel.SelectedIndex = 0;

        string vsllst = "";
        for (int i = 0; i < dsPrType.Tables[0].Rows.Count; i++)
        {
            ListItem li = new ListItem(dsPrType.Tables[0].Rows[i]["ShipName"].ToString(), dsPrType.Tables[0].Rows[i]["ShipId"].ToString());
            ddlvessel.Items.Add(li);
            vsllst = vsllst + ",'" + dsPrType.Tables[0].Rows[i]["ShipName"].ToString() + "'";
        }
        if (vsllst.Trim() != "") { vsllst = vsllst.Substring(1); }
        vesselList = vsllst;
    }
    public void BindAccount()
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + ddldepartment.SelectedValue + "' and prtype=1) dd where AccountNumber is not null";
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + ddldepartment.SelectedValue + "' and prtype=1) dd where AccountNumber is not null";
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
    public void BindDepartment()
    {
        //string sql = "select dept,deptname from  VW_sql_tblSMDPRDept";
        string sql = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID \r\nwhere  acc.Active = 'Y'  Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
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
    //Validatioin -------------------------------------
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
        //if (IsReveivedFromVessel())
        //{
        //    if (txtprno.Text.Trim() == "")
        //    {
        //        lblMsg.Text = "Please enter PR Number ";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtprno.ClientID + "').focus()", true);
        //        return false;
        //    }
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
        if (rptStoresData.Items.Count > 0)
        {
            RepeaterItem RptItm = (RepeaterItem)rptStoresData.Items[0];

            TextBox txtDesc = (TextBox)RptItm.FindControl("txtDesc");
            TextBox txtQuantity = (TextBox)RptItm.FindControl("txtQuantity");
            DropDownList ddlUOM = (DropDownList)RptItm.FindControl("ddlUOM");
            TextBox txtISSAIMPA = (TextBox)RptItm.FindControl("txtISSAIMPA");
            TextBox txtROB = (TextBox)RptItm.FindControl("txtROB");
            //Description
            if (txtDesc.Text.Trim() == "")
            {
                lblMsg.Text = "Description field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtDesc.ClientID + "').focus()", true);
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
            //txtISSAIMPA 
            //if (txtISSAIMPA.Text.Trim() == "")
            //{
            //    lblMsg.Text = "ISSA/IMPA field is empty ";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtISSAIMPA.ClientID + "').focus()", true);
            //    return false;
            //}
            //txtROB
            if (txtROB.Text.Trim() == "")
            {
                lblMsg.Text = "ROB field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtROB.ClientID + "').focus()", true);
                return false;
            }
            //if (Validations.Isinteger(txtROB.Text.Trim()) == false)
            //{
            //    lblMsg.Text = "ROB field must be integer ";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtROB.ClientID + "').focus()", true);
            //    return false;
            //}

        }
        return true;
    }

    //-------------------------------------------------
    public void BindItemsRepeater()
    {
        string sql = "select ROW_NUMBER() OVER (ORDER BY recid) AS 'RowNumber', RecID,convert(varchar,targetCompDate,106)as targetCompDate1,*, '' As POStatusId from  vw_tblSMDPODetail where 1=2";
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
            rptStoresData.DataSource = dsPrType;
            Session["SPrDataSet"] = dsPrType;
            rptStoresData.DataBind();
            HFTotalGrdRow.Value = rptStoresData.Items.Count.ToString();
        }

    }
    public void SetSerialNumberToRptData()
    {
        int RptLnt = rptStoresData.Items.Count;  
        int sno = 1;
        for (int i = RptLnt - 1; i >= 0; i--)
        {
            Label lblRowNumber = (Label)rptStoresData.Items[i].FindControl("lblRowNumber");
            lblRowNumber.Text = Convert.ToString(i+1);
        }
    }
    public void FillDataINDataSet()
    {
        int ItmIndex = 0;
        DataSet PrDataSet = (DataSet)Session["SPrDataSet"];
        object DtTargetDate;
        foreach (RepeaterItem tprItm in rptStoresData.Items) 
        {   
            TextBox txtDesc = (TextBox)tprItm.FindControl("txtDesc");
            TextBox txtQuantity = (TextBox)tprItm.FindControl("txtQuantity");
            DropDownList ddlUOM = (DropDownList)tprItm.FindControl("ddlUOM");
            TextBox txtISSAIMPA = (TextBox)tprItm.FindControl("txtISSAIMPA");
            TextBox txtROB = (TextBox)tprItm.FindControl("txtROB");
            //TextBox txttargetCompDate = (TextBox)tprItm.FindControl("txttargetCompDate");
           // if (txttargetCompDate.Text.Trim() != "") DtTargetDate = txttargetCompDate.Text.Trim(); else DtTargetDate = DBNull.Value;

            PrDataSet.Tables[0].Rows[ItmIndex]["Description"] = txtDesc.Text;
            PrDataSet.Tables[0].Rows[ItmIndex]["Qty"] = Common.CastAsDecimal(txtQuantity.Text);
            PrDataSet.Tables[0].Rows[ItmIndex]["UOM"] = ddlUOM.SelectedValue;
            PrDataSet.Tables[0].Rows[ItmIndex]["PARTNO"] = txtISSAIMPA.Text.Trim();
            PrDataSet.Tables[0].Rows[ItmIndex]["QtyOB"] = Common.CastAsDecimal(txtROB.Text);
            //PrDataSet.Tables[0].Rows[ItmIndex]["targetCompDate1"] = DtTargetDate;
            //if (txttargetCompDate.Text.Trim()!="")
            //{
            //    PrDataSet.Tables[0].Rows[ItmIndex]["targetCompDate"] = txttargetCompDate.Text.Trim();
            //}
            ItmIndex = ItmIndex + 1;
        }
        Session["SPrDataSet"] = PrDataSet;
    }
    public void ShowPRData()
    {
        lblMsg.Text="START:" +DateTime.Now.ToLongTimeString();
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
                txtDate.Text = dsPrType.Tables[0].Rows[0]["DateCreated1"].ToString();
                //ddldepartment.SelectedValue = dsPrType.Tables[0].Rows[0]["Dept"].ToString();
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
                //bind Item Details section
                Common.Set_Procedures("sp_NewPR_getPRDetail");
                Common.Set_ParameterLength(1);
                Common.Set_Parameters(new MyParameter("@PRId", PRId));
                dsPrType = new DataSet();
                dsPrType.Clear();
                dsPrType = Common.Execute_Procedures_Select();
                if (dsPrType != null)
                {
                    Session["SPrDataSet"] = dsPrType;
                    if (dsPrType.Tables[0].Rows.Count != 0)
                    {
                        rptStoresData.DataSource = dsPrType;
                        rptStoresData.DataBind();

                        HFTotalGrdRow.Value = rptStoresData.Items.Count.ToString();

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
        lblMsg.Text = lblMsg.Text+ "END:" + DateTime.Now.ToLongTimeString();
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
	if(ddldepartment.SelectedValue=="P" && txtprno.Text.Trim()!="")
	   lblMsg.Text = "Ship Provision PR can not be modified.";
	else
	   imgSave.Visible = true;

    }
    
    #endregion

    #region Events ***********************************************

    // Button ----------------------------------------
    public void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
    // Item Validation
            if (ItemValidation() == false) return; else lblMsg.Text = "";

    //Fill data set by the values entered in the textbox
        FillDataINDataSet();

     // Add a row to dataset
        DataSet PrDataSet = (DataSet)Session["SPrDataSet"];
        DataRow dr = PrDataSet.Tables[0].NewRow();
        dr["UOM"] = "";
        dr["RecID"] = "0";
        PrDataSet.Tables[0].Rows.InsertAt(dr, 0);
        rptStoresData.DataSource = PrDataSet;

    //BindUOM(ddlUOM);
        Session["SPrDataSet"] = PrDataSet;
        rptStoresData.DataBind();
        HFTotalGrdRow.Value = rptStoresData.Items.Count.ToString();
        SetSerialNumberToRptData();
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
    //Fill data set by the values entered in the textbox
        FillDataINDataSet();

        ImageButton imgDelete = (ImageButton)sender;
        int repeaterItemIndex = ((RepeaterItem)imgDelete.NamingContainer).ItemIndex;
        DataSet PrDataSet = (DataSet)Session["SPrDataSet"];
        PrDataSet.Tables[0].Rows.RemoveAt(repeaterItemIndex);
        Session["SPrDataSet"] = PrDataSet;
        rptStoresData.DataSource = PrDataSet;
        rptStoresData.DataBind();

        HFTotalGrdRow.Value = rptStoresData.Items.Count.ToString();

        SetSerialNumberToRptData();
    }
    //protected void imgSave_OnClick(Object sender, EventArgs e)
    //{
    //    //try
    //    {
    //    //Validation of Speare Master Data
    //        if (SparemasterValidation() == false) return; else lblMsg.Text = "";
    //    // Validation for   Speare Details Data
    //        if (ItemValidation() == false) return; else lblMsg.Text = "";
    //    // Insert Speare Master Data
    //        object dtETA;
    //        object dtPODate;
            
    //        if (txtETA.Text.Trim() != "")
    //            dtETA = Convert.ToDateTime(txtETA.Text.Trim());
    //        else
    //        {
    //            dtETA = DBNull.Value;
    //        }
    //    //For Podate
    //        if (txtDate.Text.Trim() != "")
    //            dtPODate = Convert.ToDateTime(txtDate.Text.Trim());
    //        else
    //        {
    //            dtPODate = DBNull.Value;
    //        }

    //        //Get all RecID By Comma Separated
    //        string AllRecID = "";
    //        foreach (RepeaterItem RptItm in rptStoresData.Items)
    //        {
    //            HiddenField RecID = (HiddenField)RptItm.FindControl("hfRecID");
    //            AllRecID = AllRecID + "," + RecID.Value;
    //        }
    //        AllRecID = AllRecID.Substring(1);
    //        if (AllRecID == "")
    //            AllRecID = "0";

    //        string POComments = txtprno.Text.Trim() + "- port : " + txtPort.Text.Trim() + "- ETA : " + txtETA.Text.Trim();
    //        Common.Set_Procedures("sp_NewPR_InsertStorePR_Office");
    //        Common.Set_ParameterLength(28);
    //        Common.Set_Parameters
    //            (
    //            new MyParameter("@PODate", dtPODate),
    //            new MyParameter("@POCreator", txtcreated.Text.Trim()),
    //            new MyParameter("@ShipID", ddlvessel.SelectedValue),
    //            new MyParameter("@InvoiceReceived", "0"),
    //            new MyParameter("@InvoicePosted", "0"),
    //            new MyParameter("@LocalMasterID", "0"),
    //            new MyParameter("@Locked", "0"),
    //            new MyParameter("@DateCreated", System.DateTime.Now.ToShortDateString()),
    //            new MyParameter("@PoComments", POComments),
    //            new MyParameter("@POCommentsVen", ""),
    //            new MyParameter("@HighPriority", "0"),
    //            new MyParameter("@StatusID ", "0"),  //1 means it is created in the office
    //            new MyParameter("@OrigPOID", "0"),
    //            new MyParameter("@CopyEquip", "0"),
    //            new MyParameter("@Dept", ddldepartment.SelectedValue),
    //            new MyParameter("@PRType", "1"),

    //            new MyParameter("@VerNo", "0"),
    //            new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
    //            new MyParameter("@RemarksSMD", txtsmdremarks.Text.Trim()),
    //            new MyParameter("@LandedRepairs", "0"),
    //            new MyParameter("@LandedStorage", "0"),
    //            new MyParameter("@isClosed", false),
    //            new MyParameter("@MultiVess", false),

    //            new MyParameter("@ReqNo", txtprno.Text.Trim()),
    //            new MyParameter("@PoID", PRId),
    //            new MyParameter("@Port", txtPort.Text.Trim()),
    //            new MyParameter("@Eta", dtETA),
    //            new MyParameter("@AllRecID", AllRecID)
                
    //            );
    //        Boolean Res;
    //        DataSet ResDS = new DataSet();
    //        Res = Common.Execute_Procedures_IUD(ResDS);

    //        int Poid = Convert.ToInt32(ResDS.Tables[0].Rows[0][0]);

    //        // Insert Speare Items Detail Data
    //        object TargetDate;
    //        foreach (RepeaterItem RptItm in rptStoresData.Items) 
    //        {
    //            TextBox txtAddedDesc = (TextBox)RptItm.FindControl("txtDesc");
    //            TextBox txtISSAIMPA = (TextBox)RptItm.FindControl("txtISSAIMPA");
    //            DropDownList ddlUOM = (DropDownList)RptItm.FindControl("ddlUOM");
    //            TextBox txtQuantity = (TextBox)RptItm.FindControl("txtQuantity");
    //            TextBox txtROB = (TextBox)RptItm.FindControl("txtROB");
    //            //TextBox txttargetCompDate = (TextBox)RptItm.FindControl("txttargetCompDate");
    //            HiddenField RecID = (HiddenField)RptItm.FindControl("hfRecID");

    //            //if (txttargetCompDate.Text.Trim() != "")
    //            //    TargetDate = Convert.ToDateTime(txttargetCompDate.Text.Trim());
    //            //else
    //            //{
    //            //    TargetDate = DBNull.Value;
    //            //}
    //            TargetDate = DBNull.Value;
    //            Common.Set_Procedures("sp_NewPR_InsertPRDetails_Office");
    //            Common.Set_ParameterLength(11);
    //            Common.Set_Parameters
    //                (
    //                    new MyParameter("@POID", Poid),  
    //                    new MyParameter("@Qty", txtQuantity.Text.Trim()),
    //                    new MyParameter("@Description", txtAddedDesc.Text.Trim()),
    //                    new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
    //                    new MyParameter("@UOM", ddlUOM.SelectedValue),
    //                    new MyParameter("@PartNo", txtISSAIMPA.Text.Trim()),
    //                    new MyParameter("@HasEquip", "0"),
    //                    new MyParameter("@PRID", "0"),
    //                    new MyParameter("@QtyOB", txtROB.Text.Trim()),
    //                    new MyParameter("@TargetCompDate", TargetDate),
    //                    new MyParameter("@RecID", RecID.Value)
    //                );

    //            ResDS = new DataSet();
    //            Res = Common.Execute_Procedures_IUD(ResDS);
    //        }

    //        if (PRId != 0)
    //        {
    //            lblMsg.Text = "Data updated successfully";
    //        }
    //        else
    //        {
    //            lblMsg.Text = "Data saved successfully";
    //        }
    //    }
    //    //catch(Exception ex)
    //    //{
    //    //lblMsg.Text = " Error while saving -: " + ex.Message;
    //    //}



    //}
    protected void imgSave_OnClick(Object sender, EventArgs e)
    {
        try
        {
            //Validation of Speare Master Data
            if (SparemasterValidation() == false) return; else lblMsg.Text = "";
            // Validation for   Speare Details Data
            if (ItemValidation() == false) return; else lblMsg.Text = "";

           // string FileName = "";
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
            //       // ImageAttachment = FU.FileBytes;
            //    }   
            //}
            // Insert Speare Master Data
            object dtETA;
            object dtPODate;

            if (txtETA.Text.Trim() != "")
                dtETA = Convert.ToDateTime(txtETA.Text.Trim());
            else
            {
                dtETA = DBNull.Value;
            }
            //For Podate
            if (txtDate.Text.Trim() != "")
                dtPODate = Convert.ToDateTime(txtDate.Text.Trim());
            else
            {
                dtPODate = DBNull.Value;
            }

            //Get all RecID By Comma Separated
            string AllRecID = "";
            foreach (RepeaterItem RptItm in rptStoresData.Items)
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
            Common.Set_Procedures("sp_NewPR_InsertStorePR_Office");
            Common.Set_ParameterLength(34);
            Common.Set_Parameters
                (
                new MyParameter("@PODate", dtPODate),
                new MyParameter("@POCreator", txtcreated.Text.Trim()),
                new MyParameter("@ShipID", ddlvessel.SelectedValue),
                new MyParameter("@InvoiceReceived", "0"),
                new MyParameter("@InvoicePosted", "0"),
                new MyParameter("@LocalMasterID", "0"),
                new MyParameter("@Locked", "0"),
                new MyParameter("@DateCreated", System.DateTime.Now.ToShortDateString()),
                new MyParameter("@PoComments", POComments),
                new MyParameter("@POCommentsVen", ""),
                new MyParameter("@HighPriority", "0"),
                new MyParameter("@StatusID ", "0"),  //1 means it is created in the office
                new MyParameter("@OrigPOID", "0"),
                new MyParameter("@CopyEquip", "0"),
                new MyParameter("@Dept", ""),
                new MyParameter("@PRType", "1"),

                new MyParameter("@VerNo", "0"),
                new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
                new MyParameter("@RemarksSMD", txtsmdremarks.Text.Trim()),
                new MyParameter("@LandedRepairs", "0"),
                new MyParameter("@LandedStorage", "0"),
                new MyParameter("@isClosed", false),
                new MyParameter("@MultiVess", false),

                new MyParameter("@ReqNo", txtprno.Text.Trim()),
                new MyParameter("@PoID", PRId),
                new MyParameter("@Port", txtPort.Text.Trim()),
                new MyParameter("@Eta", dtETA),
                new MyParameter("@AllRecID", AllRecID) ,
                new MyParameter("@MidCatId", Convert.ToInt32(ddldepartment.SelectedValue)),
                new MyParameter("@ReqTitle", txtReqTitle.Text.Trim()),
                new MyParameter("@LoginId", Convert.ToInt32(Session["loginid"].ToString())),
                new MyParameter("@IsUrgent", chkUrgent.Checked ? 1 : 0),
                new MyParameter("@IsFastTrack", chkFastTrack.Checked ? 1 : 0),
                new MyParameter("@IsSafetyUrgent", chkSafetyUrgent.Checked ? 1 : 0)
                //new MyParameter("@FileName", FileName.Trim()),
                //new MyParameter("@Attachment", FU.FileBytes)
                );
            Boolean Res;
            DataSet ResDS = new DataSet();
            Res = Common.Execute_Procedures_IUD(ResDS);
//		if(Res==false)
//{
//Response.Write(AllRecID);
//return;
//}
            int Poid = Convert.ToInt32(ResDS.Tables[0].Rows[0][0]);

            // Insert Speare Items Detail Data
            object TargetDate;
            foreach (RepeaterItem RptItm in rptStoresData.Items)
            {
                TextBox txtAddedDesc = (TextBox)RptItm.FindControl("txtDesc");
                TextBox txtISSAIMPA = (TextBox)RptItm.FindControl("txtISSAIMPA");
                DropDownList ddlUOM = (DropDownList)RptItm.FindControl("ddlUOM");
                TextBox txtQuantity = (TextBox)RptItm.FindControl("txtQuantity");
                TextBox txtROB = (TextBox)RptItm.FindControl("txtROB");
                //TextBox txttargetCompDate = (TextBox)RptItm.FindControl("txttargetCompDate");
                HiddenField RecID = (HiddenField)RptItm.FindControl("hfRecID");

                //if (txttargetCompDate.Text.Trim() != "")
                //    TargetDate = Convert.ToDateTime(txttargetCompDate.Text.Trim());
                //else
                //{
                //    TargetDate = DBNull.Value;
                //}
                TargetDate = DBNull.Value;
                
                ////////Common.Set_Procedures("sp_NewPR_InsertPRDetails_Office");
                ////////Common.Set_ParameterLength(11);
                ////////Common.Set_Parameters
                ////////    (
                ////////        new MyParameter("@POID", Poid),
                ////////        new MyParameter("@Qty", txtQuantity.Text.Trim()),
                ////////        new MyParameter("@Description", txtAddedDesc.Text.Trim()),
                ////////        new MyParameter("@AccountID", ddlAccountCode.SelectedValue),
                ////////        new MyParameter("@UOM", ddlUOM.SelectedValue),
                ////////        new MyParameter("@PartNo", txtISSAIMPA.Text.Trim()),
                ////////        new MyParameter("@HasEquip", "0"),
                ////////        new MyParameter("@PRID", "0"),
                ////////        new MyParameter("@QtyOB", txtROB.Text.Trim()),
                ////////        new MyParameter("@TargetCompDate", TargetDate),
                ////////        new MyParameter("@RecID", RecID.Value)
                ////////    );

                ////////ResDS = new DataSet();
                ////////Res = Common.Execute_Procedures_IUD(ResDS);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_NewPR_InsertPRDetails_Office", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@POID", Poid));
                    cmd.Parameters.Add(new SqlParameter("@Qty", txtQuantity.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@Description", txtAddedDesc.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@AccountID", ddlAccountCode.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@UOM", ddlUOM.SelectedValue));
                    cmd.Parameters.Add(new SqlParameter("@PartNo", txtISSAIMPA.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@HasEquip", "0"));
                    cmd.Parameters.Add(new SqlParameter("@PRID", "0"));
                    cmd.Parameters.Add(new SqlParameter("@QtyOB", txtROB.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@TargetCompDate", TargetDate));
                    cmd.Parameters.Add(new SqlParameter("@RecID", RecID.Value));
                    cmd.ExecuteNonQuery();
                }
                catch
                { }
                finally
                { con.Close(); }
            }

            if (PRId != 0)
            {
                //lblMsg.Text = "Data updated successfully";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Data updated successfully.');", true);
                Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
            }
            else
            {
                lblMsg.Text = "Data saved successfully";
                clearStorePR();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error while saving -: " + ex.Message;
        }



    }
    protected void imgCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
    }

    // Repeater ---------------------------------------
    protected void rptStoresData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DropDownList ddlUOM = (DropDownList)e.Item.FindControl("ddlUOM");
        ImageButton btnDelete = (ImageButton)e.Item.FindControl("btnDelete");
        HiddenField hdnStatusId = (HiddenField)e.Item.FindControl("hdnPOStatusId");

        // visibility management of item delete button
        if (IsReveivedFromVessel() == true)
            btnDelete.Visible = false;
        else
            btnDelete.Visible = true;

        if (hdnStatusId.Value != "")
        {
            if (Convert.ToInt32(hdnStatusId.Value) > 0)
                btnDelete.Visible = false;
            else
                btnDelete.Visible = true;
        }

        DataRowView drv = (DataRowView)e.Item.DataItem;
        string UOM = drv.Row["UOM"].ToString();
        ListItem li = ddlUOM.Items.FindByValue(UOM);

        ddlUOM.DataSource = s_dtUOM;
        ddlUOM.DataTextField = "UOM";
        ddlUOM.DataValueField = "UOM";
        ddlUOM.DataBind();
        if (li == null)
        {
            ddlUOM.Items.Add(new ListItem(UOM, UOM));
        }
        ddlUOM.SelectedValue = UOM;

    }

    // DropDown ---------------------------------------
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlvessel.SelectedIndex == 0)
        {
            return;
        }
        BindAccount();
    }

    protected void clearStorePR()
    {
        ddlvessel.SelectedIndex = 0;
        txtprno.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddlAccountCode.SelectedIndex = 0;
        txtPort.Text = "";
        txtETA.Text = "";
        txtsmdremarks.Text = "";
        txtReqTitle.Text = "";
      
        BindItemsRepeater();
        lblAttchmentCount.Text = "0";
        ImgAttachment.Enabled = true;
    }
    //protected void ImgAttachment_Click(object sender, ImageClickEventArgs e)
    //{
    //    divAttachment.Visible = true;
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
                    new MyParameter("@PRType", "SS"),
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
                    GetDocCount("SS", ddlvessel.SelectedValue,PRId);
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
            sql = "SELECT DocId, DocName As FileName,0 As RequisitionId, VesselCode, 0 As StatusId  FROM [MP_VSL_TEMP_RequisitionDocuments] with(nolock) WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " and PRType = 'SS'";
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
                sql = "Delete from MP_VSL_TEMP_RequisitionDocuments WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " AND DocId = " + DocId + " AND PRType='SS' ";
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
            sql = "SELECT Count(DocId) As DocumentCount FROM [MP_VSL_TEMP_RequisitionDocuments] with(nolock) WHERE [VesselCode] = '" + ddlvessel.SelectedValue + "' AND  LoginId =" + Convert.ToInt32(Session["loginid"].ToString()) + " and PRType = 'SS'";
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
