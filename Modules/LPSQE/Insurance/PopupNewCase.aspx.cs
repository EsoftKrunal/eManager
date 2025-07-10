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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Data.SqlClient;


public partial class PopupNewCase : System.Web.UI.Page
{
    public int CaseID
    {
        set { ViewState["CaseID"] = value; }
        get { return Common.CastAsInt32(ViewState["CaseID"]); }
    }
    public int MTMSrNO
    {
        set { ViewState["MTMSrNO"] = value; }
        get { return Common.CastAsInt32(ViewState["MTMSrNO"]); }
    }
    public int DocID
    {
        set { ViewState["DocID"] = value; }
        get { return Common.CastAsInt32(ViewState["DocID"]); }
    }
    public int SynID
    {
        set { ViewState["SynID"] = value; }
        get { return Common.CastAsInt32(ViewState["SynID"]); }
    }
    public int ExpID
    {
        set { ViewState["ExpID"] = value; }
        get { return Common.CastAsInt32(ViewState["ExpID"]); }
    }
    public int OffRemID
    {
        set { ViewState["OffRemID"] = value; }
        get { return Common.CastAsInt32(ViewState["OffRemID"]); }
    }

    public int ClaimedID
    {
        set { ViewState["ClaimedID"] = value; }
        get { return Common.CastAsInt32(ViewState["ClaimedID"]); }
    }

    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return ViewState["Mode"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
         {
             Response.Redirect("Default.aspx");
         }
        lblMessage.Text = "";
        lblPageMessage.Text = "";
        lblMsgExpence.Text = "";
        btnMsgSyn.Text = "";
        lblMSGOff.Text = "";
        lblMsgRecovery.Text = "";
        //txtMTMCaseNumber.Text = "";

       
        if (!Page.IsPostBack)
        {
            //DivApproveReject.Visible = true;
            ViewState["SortOrder"] = "ASC";             
 
            BindTime();
            BindGroups();
            BindVessels();
            BindUW();
            BindPolicyNo();
            BindCurrency();
            BindSynopsis();            
            
            if (Page.Request.QueryString["CID"] != null)
            {
                CaseID = Common.CastAsInt32(Page.Request.QueryString["CID"]);
                if (CaseID != 0)
                    ShowRecord();
            }            
            if (Page.Request.QueryString["Mode"] != null)
                Mode = Page.Request.QueryString["Mode"].ToString();

            ShowButtons();
            BindDocs();
            BindSynopsis();
            BindExpences();
            BindExpensesClaimed();
            BindOfficeComment();
            BindClosure();
        }
        SetMTMCaseNumber();
    }
    
    #region -------------- EVENTS ---------------------------------

    protected void btnCaseSave_OnClick(object sender, EventArgs e)
    {
        if (ddl_Vessels.SelectedIndex == 0)
        {
            lblPageMessage.Text = "Please select Vessel.";return;
        }
        if (ddl_Groups.SelectedIndex == 0)
        {
            lblPageMessage.Text = "Please select Group."; return;
        }
        if (ddlSubGroup.SelectedIndex == 0)
        {
            lblPageMessage.Text = "Please select Sub Group."; return;
        }

        if (ddl_UW.SelectedIndex == 0)
        {
            lblPageMessage.Text = "Please select Under Writer."; return;
        }
        if (ddlPolicyNumber.SelectedIndex == 0)
        {
            lblPageMessage.Text = "Please select Policy Number."; return;
        }
        if (txtIncidentDate.Text.Trim() == "")
        {
            lblPageMessage.Text = "Please enter incident date."; return;
        }

        string DateofIncident = "";
        if (txtIncidentDate.Text.Trim() != "")
            DateofIncident = txtIncidentDate.Text.Trim() + "`" + ddlHour.SelectedValue + "`" + ddlMin.SelectedValue;
        else
            DateofIncident = "";

        string SQL = "";
        SQL = "EXEC sp_IRM_InsetUpdateIRM_CaseMaster " + CaseID + ",'" + txtCaseNumber.Text.Trim().Replace("'", "`") + "','" + txtMTMCaseNumber.Text.Trim() + "'," + MTMSrNO + "," + ddl_Vessels.SelectedValue + ",'" + ddlPolicyNumber.SelectedValue + "','" + ddl_Groups.SelectedValue + "','" + ddlSubGroup.SelectedValue + "'," + ddl_UW.SelectedValue + ",'" + DateofIncident + "','" + txtCaseDesc.Text.Trim().Replace("'", "`") + "', " + ((txtClaimAmmount.Text.Trim() == "") ? "0" : txtClaimAmmount.Text.Trim().Replace(",", "")) + "," + Common.CastAsDecimal(txtCaseDeductibleAmt.Text.Trim().Replace(",", "")) + ",'" + txtCrewRank.Text.Trim().Replace("'", "`") + "','" + txtCrewName.Text.Trim().Replace("'", "`") + "','" + txtCrewNo.Text.Trim().Replace("'", "`") + "'," + ddlLocation.SelectedValue + ",'" + txtIncidentPlace.Text.Trim().Replace("'", "`") + "','" + ((ddlLocation.SelectedValue == "1") ? txtIncidentPlace.Text.Trim().Replace("'", "`") : txtToPort.Text.Trim().Replace("'", "`")) + "',1";
        
        DataTable dt = Budget.getTable(SQL).Tables[0];
        if (CaseID == 0)
        {
            lblPageMessage.Text = "Record saved successfully.";
            CaseID = Common.CastAsInt32(dt.Rows[0][0]);
        }
        else
            lblPageMessage.Text = "Record updated successfully.";

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ReloadParrent", "refreshparent()", true);
            
    }
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (CaseID == 0)
        {
            lblMessage.Text = "Please save the case first.";            
            return;
        }
        if (txt_Desc.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter description.";
            return;
        }
        if (DocID == 0)
        if (!flAttachDocs.HasFile)
        {
            lblMessage.Text = "Please select a file.";
            flAttachDocs.Focus();
            return;
        }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx" || Path.GetExtension(File.FileName).ToString() == ".gif" || Path.GetExtension(File.FileName).ToString() == ".jpg" || Path.GetExtension(File.FileName).ToString() == ".png")
            {
                FileName = "CaseDocs" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);
                
                
                string path = "/EMANAGERBLOB/LPSQE/Insurance/";
                flAttachDocs.SaveAs(Server.MapPath(path) + FileName);               
            }
            else
            {
                lblMessage.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
                return;
            }


        }
        string strSQL = "EXEC sp_IRM_InsertIntoIRM_CaseDocuments " + DocID + "," + CaseID + ",'" + ddlPolicyNumber.SelectedValue + "','" + txt_Desc.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            lblMessage.Text = "Record Successfully Saved.";
            txt_Desc.Text = "";
            BindDocs();
            DocID = 0;
        }
    }
    protected void btnSaveExpence_OnClick(object sneder, EventArgs e)
    {
        if (CaseID == 0)
        {
            lblMsgExpence.Text = "Please save the case first.";
            return;
        }
        if (txtExpDate.Text.Trim() == "")
        {
            lblMsgExpence.Text = "Please enter expense date.";
            txtExpDate.Focus();
            return;
        }
        try 
        {
            DateTime dt = Convert.ToDateTime(txtExpDate.Text.Trim());        
        }
        catch(Exception ex)
        {
            lblMsgExpence.Text = "Expense date is invalid.";
            txtExpDate.Focus();            return;
        }


        if (ddlLocalCurr.SelectedIndex == 0)
        {
            lblMsgExpence.Text = "Please select local currency.";
            ddlLocalCurr.Focus();
            return;
        }
        if (ddlExpensesStatus.SelectedIndex == 0)
        {
            lblMsgExpence.Text = "Please select expense status.";
            ddlExpensesStatus.Focus();return;
        }
        //   if (!fuExpence.HasFile)
        //{
        //    lblMsgExpence.Text = "Please select a file.";
        //    fuExpence.Focus();
        //    return;
        //}
        FileUpload img = (FileUpload)fuExpence;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = fuExpence.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx" || Path.GetExtension(File.FileName).ToString() == ".gif" || Path.GetExtension(File.FileName).ToString() == ".jpg" || Path.GetExtension(File.FileName).ToString() == ".png")
            {
                FileName = "Expence" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);
                
                string path = "/EMANAGERBLOB/LPSQE/Insurance/";
                fuExpence.SaveAs(Server.MapPath(path) + FileName);                
            }
            else
            {
                lblMsgExpence.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
                return;
            }
        }
        string strSQL = "EXEC sp_IRM_IRM_CaseExpenceDetails " + ExpID + "," + CaseID + ",'" + ddlPolicyNumber.SelectedValue + "','" + txtExpDate.Text.Trim() + "','" + txtExpDesc.Text.Trim().Replace("'", "`") + "'," + Common.CastAsDecimal(txtAmount.Text.Replace(",", "")) + ",'" + ddlLocalCurr.SelectedValue + "',"+txtRate.Text+"," + ((lblTotalUSDoler.Text == "") ? "0" : lblTotalUSDoler.Text.Replace(",", "")) + ",'" + FileName + "','"+txtBoucherNo.Text+"',"+ddlExpensesStatus.SelectedValue+"";//Convert.ToInt32(Session["loginid"].ToString()) 
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            lblMsgExpence.Text = "Record Successfully Saved.";
            BindExpences();

            txtExpDate.Text = "";
            txtExpDesc.Text = "";
            txtAmount.Text = "";
            txtRate.Text = "";
            txtBoucherNo.Text = "";
            ddlExpensesStatus.SelectedIndex = 0;
            ddlLocalCurr.SelectedIndex = 0;
            lblTotalUSDoler.Text = "";
            //divAddExpensesPopup.Visible = false;
            divAddExpensesPopup.Style.Add("display", "none");
            ExpID = 0;
        }
    }

    protected void ddl_Groups_SelectedIndexChanged(object sender, EventArgs e)
    {


        UP8.Update();

        BindSubGroups();
        BindUW();
        BindPolicyNo();
        if (ddl_Groups.SelectedItem.Text.ToUpper().Trim() == "PNI")
        {
            if (ddlSubGroup != null)
            {
                if (ddlSubGroup.SelectedItem.Text.ToUpper().Trim() == "PNI CREW")
                    tblCrew.Visible = true;
                else
                    tblCrew.Visible = false;
            }
        }
        else
            tblCrew.Visible = false;            
        
    }
    protected void ddlSubGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Groups.SelectedItem.Text.ToUpper().Trim() == "PNI")
        {
            if (ddlSubGroup.SelectedItem.Text.ToUpper().Trim() == "PNI CREW")
                tblCrew.Visible = true;
            else
                tblCrew.Visible = false;
        }
        else
            tblCrew.Visible = false;            
    }
    protected void ddl_Vessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        UP8.Update();
        ShowVesselDetails();
    }
    protected void ddlBroker_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    
    
    protected void ddlArrangedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlArrangedBy.SelectedValue == "2")
        //{
        //    txt_Premium.Text = "As arranged";
        //}
        //else
        //{
        //    txt_Premium.Text = "";
        //}
    }

    protected void btnDeleteDoc_Click(object sender, EventArgs e)
    {
        ImageButton btnDeleteDoc = (ImageButton)sender;
        int DocId = Common.CastAsInt32(btnDeleteDoc.CommandArgument);

        string strSQL = "SELECT [FileName] FROM IRM_PolicyDocuments WHERE DocId = " + DocId;
        DataTable dtFileName = Budget.getTable(strSQL).Tables[0];
        string DocFile = dtFileName.Rows[0]["FileName"].ToString();

        string strDelete = "DELETE FROM IRM_PolicyDocuments WHERE DocId = " + DocId + " ; SELECT -1";
        DataTable dtDelete =  Budget.getTable(strDelete).Tables[0];
        if(dtDelete.Rows.Count > 0)
        {
            string path = "/EMANAGERBLOB/LPSQE/Insurance/";
            System.IO.File.Delete(Server.MapPath(path) + DocFile);
        }
        BindDocs();
    }

    protected void ddl_UW_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindPolicyNo();
    }
    
    protected void ddlPolicyNumber_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SetPolicyDeductibleAmount();
    }
    protected void txtCrewNo_OnTextChanged(object sender, EventArgs e)
    {
        string sql = "select (FirstName+' '+Middlename+' '+lastName)name,(select rankCode from dbo.rank r where r.RankID=CP.CurrentRankID )Rank from dbo.CrewPersonalDetails CP where CrewNumber='"+txtCrewNo.Text+"'";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            txtCrewName.Text = dt.Rows[0][0].ToString();
            txtCrewRank.Text = dt.Rows[0][1].ToString();
        }
        else
        {
            txtCrewName.Text = "";
            txtCrewRank.Text = "";
        }
    }
    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex == 0)
        {
            lblLocationLableText.Text = "Incident Place :";
            trIncidentToPort.Visible=false;
        }
        else
        {
            lblLocationLableText.Text = "From Port :";
            trIncidentToPort.Visible = true;
        }
    }

    //Doc
    protected void imgEditDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgEdit = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgEdit.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);
        Label lblDesc = (Label)ImgEdit.Parent.FindControl("lblDesc");

        txt_Desc.Text = lblDesc.Text;

    }
    protected void imgDelDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgDel = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgDel.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);

        string sql = "delete from IRM_CaseDocuments  where DocID=" + hfDocID .Value+ "";
        DataSet dtGroups = Budget.getTable(sql);
        BindDocs();
        DocID = 0;
    }
    protected void btnCancelDoc_Click(object sender, EventArgs e)
    {
        DocID = 0;
        txt_Desc.Text = "";
    }
    //protected void btnCloseDocuments_OnClick(object sneder, EventArgs e)
    //{
    //    DivOtherDocs.Visible = false;
    //    DocID = 0;
    //    txt_Desc.Text = "";
    //}

    
    //Exp
    protected void imgEditExp_OnClick(object sender, EventArgs e)
    {
        //divAddExpensesPopup.Visible = true;
        divAddExpensesPopup.Style.Add("display", "");
        ImageButton ImgEdit = (ImageButton)sender;
        HiddenField hfExpID = (HiddenField)ImgEdit.Parent.FindControl("hfExpID");
        HiddenField hfExpStatusID= (HiddenField)ImgEdit.Parent.FindControl("hfExpStatusID");
        
        ExpID = Common.CastAsInt32(hfExpID.Value);

        Label lblExpDate = (Label)ImgEdit.Parent.FindControl("lblExpDate");
        Label lblExpDesc = (Label)ImgEdit.Parent.FindControl("lblExpDesc");
        Label lblExpAmount = (Label)ImgEdit.Parent.FindControl("lblExpAmount");
        Label lblExpCurr = (Label)ImgEdit.Parent.FindControl("lblExpCurr");
        Label lblRate = (Label)ImgEdit.Parent.FindControl("lblRate");
        Label lblExpTotUSDoler = (Label)ImgEdit.Parent.FindControl("lblExpTotUSDoler");
        Label lblBoucherNo = (Label)ImgEdit.Parent.FindControl("lblBoucherNo");

        txtExpDate.Text = lblExpDate.Text;
        txtExpDesc.Text = lblExpDesc.Text;
        txtAmount.Text = lblExpAmount.Text.Replace(",","");
        ddlLocalCurr.SelectedValue = lblExpCurr.Text.Trim().Replace(",", "");
        txtRate.Text = lblRate.Text;
        lblTotalUSDoler.Text = lblExpTotUSDoler.Text;
        txtBoucherNo.Text = lblBoucherNo.Text;
        try
        {
            ddlExpensesStatus.SelectedValue = hfExpStatusID.Value;
        }
        catch (Exception ex)
        {
            ddlExpensesStatus.SelectedIndex = 0;
        }

    }
    protected void imgDelExp_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgDel = (ImageButton)sender;
        HiddenField hfExpID = (HiddenField)ImgDel.Parent.FindControl("hfExpID");
        ExpID = Common.CastAsInt32(hfExpID.Value);

        string sql = "delete FROM dbo.IRM_CaseExpenceDetails where ExpID=" + ExpID + "";
        DataSet dtExp = Budget.getTable(sql);
        BindExpences();
        ExpID = 0;
    }
    protected void btnCancelExpences_OnClick(object sender, EventArgs e)
    {
        ExpID = 0;
        txtExpDate.Text = "";
        txtExpDesc.Text = "";
        txtRate.Text = "";
        txtAmount.Text = "";
        txtBoucherNo.Text = "";
        ddlExpensesStatus.SelectedIndex = 0;
        ddlLocalCurr.SelectedIndex = 0;
        lblTotalUSDoler.Text = "";
        //divAddExpensesPopup.Visible = false;
        divAddExpensesPopup.Style.Add("display", "none");
    }
    protected void imgUpdateClaimedDate_OnClick(object sender, EventArgs e)
    {
        ImageButton imgUpdateClaimedDate = (ImageButton)sender;
        HiddenField hfExpID = (HiddenField)imgUpdateClaimedDate.Parent.FindControl("hfExpID");
        TextBox txtClaimedDate = (TextBox)imgUpdateClaimedDate.Parent.FindControl("txtClaimedDate");
        if (txtClaimedDate.Text.Trim() != "")
        {
            string Sql = " update dbo.IRM_CaseExpenceDetails set Claimed=1,ClaimedDate= '" + txtClaimedDate.Text.Trim() + "' where ExpID=" + hfExpID.Value+ "";
            DataSet dtExp = Budget.getTable(Sql);
            BindExpences();
            //BindClaimed();
        }
    }
    protected void ddlLocalCurr_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        CurrencyConversion();
    }
    protected void OnTextChanged_txtAmount(object sender, EventArgs e)
    {
        //CurrencyConversion();
        if (txtAmount.Text != "")
        {
            if (Common.CastAsDecimal(txtRate.Text) != 0)
                lblTotalUSDoler.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtAmount.Text) / Convert.ToDecimal(txtRate.Text)), 2));
            else
                lblTotalUSDoler.Text = "0";
        }
        else
            lblTotalUSDoler.Text = "0";
    }
    protected void txtRate_txtAmount(object sender, EventArgs e)
    {   
        if (txtAmount.Text != "")
        {
            if (Common.CastAsDecimal(txtRate.Text)!= 0)
                lblTotalUSDoler.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtAmount.Text) / Convert.ToDecimal(txtRate.Text)), 2));
            else
                lblTotalUSDoler.Text = "0";
        }
        else
            lblTotalUSDoler.Text = "0";
    }

    protected void btnCancelClaim_OnClick(object sender, EventArgs e)
    {
        //BindClaimed();
    }
    //protected void btnCloseExpences_OnClick(object sender, EventArgs e)
    //{
    //    DivExpences.Visible = false;
    //    ExpID = 0;
    //    txtExpDate.Text = "";
    //    txtExpDesc.Text = "";
    //    txtAmount.Text = "";
    //    ddlLocalCurr.SelectedIndex=0;
    //    lblTotalUSDoler.Text = "";
    //}
    protected void btnOpenExpensesPopUp_OnClick(object sender, EventArgs e)
    {
        //divAddExpensesPopup.Visible = true;
        divAddExpensesPopup.Style.Add("display","");
        
    }
    protected void btnSubmitExpensed_OnClick(object sender, EventArgs e)
    {
        Boolean Flag=false;
        foreach (RepeaterItem Itm in rptExpences.Items)
        {
            CheckBox chk = (CheckBox)Itm.FindControl("chkClaimed");
            if (chk.Checked)
                Flag = true;
        }

        if (!Flag)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select expenses first!')", true);
            return;
        }
        divSubmitExpenses.Visible = true;
        BindViewExpences();
        txtExpSubmetedBy.Text = Session["UserName"].ToString();
    }
    protected void btnCancelSubmitExpenses_OnClick(object sender, EventArgs e)
    {
        divSubmitExpenses.Visible = false;
        txtExpSubmitedOn.Text = "";
        txtExpRemarks.Text = "";
    }
    protected void btnSaveExpenses_OnClick(object sender, EventArgs e)
    {
        if (txtExpSubmitedOn.Text.Trim() == "")
        {
            //lblMsgSubmitClaim.Text = "Invalid Date;";
            lblMsgSubmitClaim.Text = "Please enter date.";
            lblMsgSubmitClaim.Focus(); return;
        }
        try 
        {
            DateTime dt = Convert.ToDateTime(txtExpSubmitedOn.Text.Trim());
        }
        catch(Exception ed)
        {
            lblMsgSubmitClaim.Text = "Invalid Date.";            
            lblMsgSubmitClaim.Focus(); return;
        }
        if (txtExpRemarks.Text.Trim() == "")
        {
            lblMsgSubmitClaim.Text = "Please enter remarks.";
            lblMsgSubmitClaim.Focus(); return;
        }

        decimal TotAmount = 0;
        foreach (RepeaterItem Itm in rptExpences.Items)
        {
            CheckBox chkClaimed = (CheckBox)Itm.FindControl("chkClaimed");

            if (chkClaimed.Checked)
            {
                Label lblExpTotUSDoler = (Label)Itm.FindControl("lblExpTotUSDoler");
                TotAmount = TotAmount + Common.CastAsDecimal(lblExpTotUSDoler.Text);
            }
        }

        // Insert Master Record
        Common.Set_Procedures("dbo.sp_IRM_InsertIRM_CaseExpensesClaimed");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
                new MyParameter("@SubmitedBy", Common.CastAsInt32(Session["loginid"].ToString())),
                new MyParameter("@CaseID", CaseID),                
                new MyParameter("@SubmitedOn", txtExpSubmitedOn.Text),
                new MyParameter("@TotalAmountUS$", TotAmount),
                new MyParameter("@Remarks", txtExpRemarks.Text)
            );
        Boolean Res;
        int ClaimedID = 0;
        DataSet Ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(Ds);
        if (Res)
            ClaimedID = Common.CastAsInt32( Ds.Tables[0].Rows[0][0]);



        foreach (RepeaterItem Itm in rptExpences.Items)
        {
            HiddenField hfExpID = (HiddenField)Itm.FindControl("hfExpID");
            TextBox txtClaimedDate = (TextBox)Itm.FindControl("txtClaimedDate");
            CheckBox chkClaimed = (CheckBox)Itm.FindControl("chkClaimed");

            if (chkClaimed.Checked)
            {
                string Sql = " update dbo.IRM_CaseExpenceDetails set Claimed=1,ClaimedDate= '" + txtExpSubmitedOn.Text.Trim() + "' where ExpID=" + hfExpID.Value + "";
                DataSet dtExp = Budget.getTable(Sql);

                Common.Set_Procedures("dbo.sp_IRM_CaseExpensesClaimedDetails");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(
                        new MyParameter("@ClaimedID", ClaimedID),
                        new MyParameter("@ExpID", hfExpID.Value)
                    );
                Boolean Res1;
                DataSet Ds1 = new DataSet();
                Res1 = Common.Execute_Procedures_IUD(Ds1);

            }
        }

        BindExpensesClaimed();
        BindExpences();
        divSubmitExpenses.Visible = false;
        txtExpSubmitedOn.Text = "";
        txtExpRemarks.Text = "";
    }

    protected void lnkViewSubmitedClaimDetails_OnClick(object sender, EventArgs e)
    {
        LinkButton chk = (LinkButton)sender;
        HiddenField hfClaimedID = (HiddenField)chk.Parent.FindControl("hfClaimedID");
        ClaimedID = Common.CastAsInt32(hfClaimedID.Value);

        DivUpdateRecoverAmount.Visible = true;

        //-----------
        //show Master Date
        string sql = "SELECT Row_number() over (order by ClaimedID )Row " +
                  " , ClaimedID ,TotalAmountUS$,TotalRecoveredAmount,(TotalAmountUS$-isnull(TotalRecoveredAmount,0))LessRecovered " +
                  " ,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= EC.SubmitedBy)SubmitedBy " +
                  " ,replace(Convert(varchar,SubmitedOn,106),' ','-')SubmitedOn " +
                  " ,Remarks " +
              " FROM dbo.IRM_CaseExpensesClaimed EC where EC.ClaimedID=" + hfClaimedID.Value;

        DataTable DtM = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtM.Rows.Count > 0)
        {
            lblRcvSubmitedBy.Text = DtM.Rows[0]["SubmitedBy"].ToString();
            lblRcvSubmitedOn.Text = DtM.Rows[0]["SubmitedOn"].ToString();
            lblRcvRemarks.Text = DtM.Rows[0]["Remarks"].ToString();
            //txtRcvRecoveryAmount.Text = FormatCurrency(DtM.Rows[0]["TotalRecoveredAmount"]);
            lblRcvLessRecovered.Text = FormatCurrency(DtM.Rows[0]["LessRecovered"]);

        }
        //-----------
        sql = "select Row_number() over (order by EC.ClaimedID )Row ,CED.ExpID,EC.ClaimedID " +
                    " ,EC.SubmitedBy " +
                    " ,Replace(Convert(varchar, CED.Date,106),' ','-')Date " +
                    " ,EC.TotalAmountUS$ " +
                    " ,CED.Description  ,CED.Rate ,CED.LocalCurr ,CED.Amount ,isnull(CED.RecoveredAmount,0)RecoveredAmount,CED.TotalUSDoler,ClaimFile " +
                    " from Dbo.IRM_CaseExpensesClaimed EC  " +
                    " inner join Dbo.IRM_CaseExpensesClaimedDetails ECD on EC.ClaimedID=ECD.ClaimedID " +
                    " left join Dbo.IRM_CaseExpenceDetails CED on Ecd.ExpID=CED.ExpID   " +
                    " where EC.ClaimedID=" + hfClaimedID.Value;

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptViewRecordOfExpenses.DataSource = Dt;
        rptViewRecordOfExpenses.DataBind();

        if (Dt.Rows.Count > 0)
        {
            lblTotAmountUpdateRecovery.Text = FormatCurrency(Dt.Compute("sum(TotalUSDoler)", ""));
            lblTotRecoverAmountUR.Text = FormatCurrency(Dt.Compute("sum(RecoveredAmount)", ""));
            
        }

        //LinkButton btn= (LinkButton)sender;
        //HiddenField hfClaimedID = (HiddenField)btn.Parent.FindControl("hfClaimedID");
        //ClaimedID = Common.CastAsInt32(hfClaimedID.Value);
        ////hfClaimedID
        //divViewSubmitedClaimDetails.Visible = true;
        
        ////show Master Date
        //string sql = "SELECT Row_number() over (order by ClaimedID )Row " +
        //          " , ClaimedID ,TotalAmountUS$" +
        //          " ,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= EC.SubmitedBy)SubmitedBy " +
        //          " ,replace(Convert(varchar,SubmitedOn,106),' ','-')SubmitedOn " +
        //          " ,Remarks " +
        //      " FROM dbo.IRM_CaseExpensesClaimed EC where EC.ClaimedID=" + hfClaimedID .Value ;

        //DataTable DtM = Common.Execute_Procedures_Select_ByQuery(sql);
        //if (DtM.Rows.Count > 0)
        //{
        //    lblExpRcvSubmitedBy.Text = DtM.Rows[0]["SubmitedBy"].ToString();
        //    lblExpRcvSubmitedOn.Text = DtM.Rows[0]["SubmitedOn"].ToString();
        //    lblExpRcvRemarks.Text = DtM.Rows[0]["Remarks"].ToString();
        //}
        //// bind Grid Details
        //sql = "select Row_number() over (order by EC.ClaimedID )Row ,CED.ExpID,EC.ClaimedID " +
        //            " ,EC.SubmitedBy " +
        //            " ,Replace(Convert(varchar, CED.Date,106),' ','-')SubmitedOn " +
        //            " ,EC.TotalAmountUS$ " +
        //            " ,CED.Description as Remarks ,CED.Rate ,CED.LocalCurr ,CED.Amount as LocalAmount,isnull(CED.RecoveredAmount,0)RecoveredAmount,CED.TotalUSDoler as AmountUSDoler " +
        //            " from Dbo.IRM_CaseExpensesClaimed EC  " +
        //            " inner join Dbo.IRM_CaseExpensesClaimedDetails ECD on EC.ClaimedID=ECD.ClaimedID " +
        //            " left join Dbo.IRM_CaseExpenceDetails CED on Ecd.ExpID=CED.ExpID   " +
        //            " where EC.ClaimedID=" + hfClaimedID .Value;

        //DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        //rptRecoverClaim.DataSource = Dt;
        //rptRecoverClaim.DataBind();

    }
    protected void btnCancelRecoverClaim_OnClick(object sender, EventArgs e)
    {
        divViewSubmitedClaimDetails.Visible = false;
    }

    protected void btnSaveRecoverClaimed_OnClick(object sender, EventArgs e)
    {
        decimal TotRecoveredAmount = 0;
        //UPdate recover amount
        foreach (RepeaterItem Itm in rptRecoverClaim.Items)
        {
            TextBox txtExpRecoveryAmount = (TextBox)Itm.FindControl("txtExpRecoveryAmount");
            HiddenField hfExpID = (HiddenField)Itm.FindControl("hfExpID");
            HiddenField hfClaimedID = (HiddenField)Itm.FindControl("hfClaimedID");            

            TotRecoveredAmount = TotRecoveredAmount + Common.CastAsDecimal(txtExpRecoveryAmount.Text);

            string sql = "update  dbo.IRM_CaseExpenceDetails set " +
                       " Recovered=1 " +
                        " ,RecoveredAmount =" + Common.CastAsDecimal( txtExpRecoveryAmount.Text )+ "" +
                        " ,RecoveredDate =getdate() " +
                        " where ExpID="+hfExpID .Value;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        }

        // update total recoverd amount

        //string SqlRcv = "update dbo.IRM_CaseExpensesClaimed  set  " +
        //                " TotalRecoveredAmount=" + TotRecoveredAmount + 
        //                " where ClaimedID=" + ClaimedID + "";
        //DataTable DtRcv = Common.Execute_Procedures_Select_ByQuery(SqlRcv);

        ClaimedID = 0;
        BindExpensesClaimed();
        divViewSubmitedClaimDetails.Visible = false;
    }

    protected void chkRecoverClaim_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        HiddenField hfClaimedID = (HiddenField)chk.Parent.FindControl("hfClaimedID");
        ClaimedID = Common.CastAsInt32(hfClaimedID.Value);

        DivUpdateRecoverAmount.Visible = true;

        //-----------
        //show Master Date
        string sql = "SELECT Row_number() over (order by ClaimedID )Row " +
                  " , ClaimedID ,TotalAmountUS$,TotalRecoveredAmount,(TotalAmountUS$-isnull(TotalRecoveredAmount,0))LessRecovered " +
                  " ,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= EC.SubmitedBy)SubmitedBy " +
                  " ,replace(Convert(varchar,SubmitedOn,106),' ','-')SubmitedOn " +
                  " ,Remarks " +
              " FROM dbo.IRM_CaseExpensesClaimed EC where EC.ClaimedID=" + hfClaimedID.Value;

        DataTable DtM = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtM.Rows.Count > 0)
        {
            lblRcvSubmitedBy.Text = DtM.Rows[0]["SubmitedBy"].ToString();
            lblRcvSubmitedOn.Text = DtM.Rows[0]["SubmitedOn"].ToString();
            lblRcvRemarks.Text = DtM.Rows[0]["Remarks"].ToString();
            lblRcvTotRcvAmt.Text = FormatCurrency(DtM.Rows[0]["TotalRecoveredAmount"]);
            lblRcvLessRecovered.Text = FormatCurrency(DtM.Rows[0]["LessRecovered"]);
            
        }
        //-----------
        sql = "select Row_number() over (order by EC.ClaimedID )Row ,CED.ExpID,EC.ClaimedID " +
                    " ,EC.SubmitedBy " +
                    " ,Replace(Convert(varchar, CED.Date,106),' ','-')Date " +
                    " ,EC.TotalAmountUS$ " +
                    " ,CED.Description  ,CED.Rate ,CED.LocalCurr ,CED.Amount ,isnull(CED.RecoveredAmount,0)RecoveredAmount,CED.TotalUSDoler " +
                    " from Dbo.IRM_CaseExpensesClaimed EC  " +
                    " inner join Dbo.IRM_CaseExpensesClaimedDetails ECD on EC.ClaimedID=ECD.ClaimedID " +
                    " left join Dbo.IRM_CaseExpenceDetails CED on Ecd.ExpID=CED.ExpID   " +
                    " where EC.ClaimedID=" + hfClaimedID.Value;

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptViewRecordOfExpenses.DataSource = Dt;
        rptViewRecordOfExpenses.DataBind();
        chk.Checked = false;


        if (Dt.Rows.Count > 0)
            lblTotAmountUpdateRecovery.Text = FormatCurrency(Dt.Compute("sum(TotalUSDoler)", ""));
    }
    protected void btnRcvRecovery_OnClick(object sender, EventArgs e)
    {
        if (ddlRcvRecoveryStatus.SelectedIndex == 0)
        {
            lblMsgRecovery.Text = "Please select status.";
            ddlRcvRecoveryStatus.Focus(); return;
        }
        decimal TotRecoveredAmount = 0;
        //UPdate recover amount
        foreach (RepeaterItem Itm in rptViewRecordOfExpenses.Items)
        {
            TextBox txtExpRecdAmount = (TextBox)Itm.FindControl("txtExpRecdAmount");
            HiddenField hfExpID = (HiddenField)Itm.FindControl("hfExpID");
            HiddenField hfClaimedID = (HiddenField)Itm.FindControl("hfClaimedID");

            TotRecoveredAmount = TotRecoveredAmount + Common.CastAsDecimal(txtExpRecdAmount.Text);

            string sql = "update  dbo.IRM_CaseExpenceDetails set " +
                       " Recovered=1 " +
                        " ,RecoveredAmount =" + Common.CastAsDecimal(txtExpRecdAmount.Text) + "" +
                        " ,RecoveredDate =getdate() " +
                        " where ExpID=" + hfExpID.Value;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        }


        string sql1 = "update Dbo.IRM_CaseExpensesClaimed set TotalRecoveredAmount=" + Common.CastAsInt32(lblTotRecoverAmountUR.Text) + " ,RcvStatus=" + ddlRcvRecoveryStatus.SelectedValue + " where ClaimedID=" + ClaimedID + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql1);

        //BindExpences();
        BindExpensesClaimed();
        ShowRecord();
        ClaimedID = 0;
        DivUpdateRecoverAmount.Visible = false;
        //txtRcvRecoveryAmount.Text = "";
    }
    protected void btnRcvCancelRecovery_OnClick(object sender, EventArgs e)
    {
        DivUpdateRecoverAmount.Visible = false;
        //txtRcvRecoveryAmount.Text = "";
    }

    protected void txtExpRecdAmount_OnTextChanged(object sender, EventArgs e)
    {
        Decimal Tot = 0;
        foreach (RepeaterItem Itm in rptViewRecordOfExpenses.Items)
        {
            TextBox txtExpRecdAmount = (TextBox)Itm.FindControl("txtExpRecdAmount");
            Tot = Tot + Common.CastAsDecimal(txtExpRecdAmount.Text);
        }
        lblTotRecoverAmountUR.Text = FormatCurrency(Tot);
    }

    public void BindViewExpences()
    {
        string ExpID = "0";
        foreach (RepeaterItem Itm in rptExpences.Items)
        {
            HiddenField hfExpID = (HiddenField)Itm.FindControl("hfExpID");
            CheckBox chkClaimed = (CheckBox)Itm.FindControl("chkClaimed");
            if (chkClaimed.Checked)
                ExpID = ExpID + "," + hfExpID.Value;
        }
        
        string sql = "SELECT ExpID,Row_number() over (order by ExpID )Row " +
                     " ,CaseID " +
                      " ,PolicyNo " +
                      " ,replace(convert(varchar,Date,106),' ','-')Date " +
                      " ,Description " +
                      " ,Amount " +
                      " ,Rate " +
                      " ,(select sum(TotalUSDoler) from IRM_CaseExpenceDetails where caseID=" + CaseID + ")TotalCalaimAmount " +
                      " ,LocalCurr " +
                      " ,TotalUSDoler " +
                      " ,Claimed " +
                      " ,replace(convert(varchar,ClaimedDate,106),' ','-') ClaimedDate" +
                      " ,ClaimFile " +
                  " FROM dbo.IRM_CaseExpenceDetails where ExpID in (" + ExpID + ") and claimed = 0";
        DataTable dtExpence = Budget.getTable(sql).Tables[0];

        rptViewExpenses.DataSource = dtExpence;
        rptViewExpenses.DataBind();

        if (dtExpence.Rows.Count > 0)
            lblTotAmtSubmited.Text = FormatCurrency(dtExpence.Compute("sum(TotalUSDoler)", ""));
        else
            lblTotAmtSubmited.Text = "0.00";

        //lblTotalClaimedAmount.Text = FormatCurrency(dtExpence.Compute("sum(TotalUSDoler)", "").ToString());
        //if (dtExpence.Rows.Count > 0)
        //    txtClaimAmmount.Text = FormatCurrency(dtExpence.Rows[0]["TotalCalaimAmount"].ToString());


    }
    public void BindExpensesClaimed()
    {
        string sql = "SELECT Row_number() over (order by ClaimedID )Row " +
                  " , ClaimedID ,TotalAmountUS$" +
                  " ,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= EC.SubmitedBy)SubmitedBy "+
                  " ,replace(Convert(varchar,SubmitedOn,106),' ','-')SubmitedOn "+
                  " ,Remarks,isnull(TotalRecoveredAmount,0)TotalRecoveredAmount " +
                  " ,( case when len( convert(varchar(7000),Remarks))>70 then substring(Remarks,1,65)+'............'else  Remarks end ) as ShortComment" +
                  
                  " , (case when RcvStatus =1 then 'Approved' when RcvStatus =2 then 'Partly Approved' when RcvStatus =3 then 'Not Approved' else '' end)RcvStatus " +
              " FROM dbo.IRM_CaseExpensesClaimed EC where CaseID="+CaseID.ToString()+" ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        

        rptSubmitedExpenses.DataSource = Dt;
        rptSubmitedExpenses.DataBind();

        if (Dt.Rows.Count > 0)
        {
            lblTotalRecoveredAmount.Text = FormatCurrency(Dt.Compute("sum(TotalRecoveredAmount)", ""));
            lblTotalClaimedAmountOnSubmitedClaim.Text = FormatCurrency(Dt.Compute("sum(TotalAmountUS$)", ""));
        }

    }
    

    // Popup
    protected void btnCancelComments_OnClick(object sender, EventArgs e)
    {
        //DivApproveReject.Visible = false;
    }


    // Sysnopsis
    protected void btnAddSynopsis_OnClick(object sender, EventArgs e)
    {
        DivAddSynopsis.Visible = true;
        btnSaveSynopsis.Visible = true;
        fuSunopsis.Visible = true;
        aShoFile.Visible = false;
        //tblSynopsisEntry.Visible = true;
        //divSynopsis.Attributes.Add("style", "overflow-y: scroll;overflow-x: hidden; width: 100%; height: 98px; border:solid 1px gray;");
        
    }
    protected void btnSaveSynopsis_OnClick(object sender, EventArgs e)
    {
        if (CaseID == 0)
        {
            btnMsgSyn.Text = "Please save the case first.";
            return;
        }
        if (txtSynopsisText.Text.Trim()== "")
        {
            btnMsgSyn.Text = "Please enter synopsis.";
            return;
        }
        if (txtSynopsisDt.Text.Trim() == "")
        {
            btnMsgSyn.Text = "Please enter synopsis date.";
            txtSynopsisDt.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtSynopsisDt.Text.Trim(), out dt))
        {
            btnMsgSyn.Text = "Please enter valid date.";
            txtSynopsisDt.Focus();
            return;
        }

        if (Convert.ToDateTime(txtSynopsisDt.Text.Trim()) > DateTime.Today)
        {
            btnMsgSyn.Text = "Synopsis date can not be more than today.";
            txtSynopsisDt.Focus();
            return; 
        }

        //if (SynID == 0)
        //if (!fuSunopsis .HasFile)
        //{
        //    btnMsgSyn.Text = "Please select a file.";
        //    fuSunopsis.Focus();
        //    return;
        //}
        FileUpload img = (FileUpload)fuSunopsis;
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = fuSunopsis.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx" || Path.GetExtension(File.FileName).ToString() == ".gif" || Path.GetExtension(File.FileName).ToString() == ".jpg" || Path.GetExtension(File.FileName).ToString() == ".png")
            {
                FileName = "SysDocs" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);


                string path = "/EMANAGERBLOB/LPSQE/Insurance/";
                fuSunopsis.SaveAs(Server.MapPath(path) + FileName);
            }
            else
            {
                btnMsgSyn.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
                return;
            }


        }
        string strSQL = "EXEC sp_IRM_InsertUpdateIRM_Synopsis " + SynID + "," + CaseID + ",'"+ddlPolicyNumber.SelectedValue+"','" + txtSynopsisText.Text.Trim().Replace("'","`") + "','" + txtDocumentID.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "','" + txtSynopsisDt.Text.Trim() + "'";
        DataTable dtSyn = Budget.getTable(strSQL).Tables[0];
        if (dtSyn.Rows.Count > 0)
        {
            btnMsgSyn.Text = "Record Successfully Saved.";
            txtDocumentID.Text = "";
            txtSynopsisText.Text = "";
            BindSynopsis();
            SynID= 0;
            
            //tblSynopsisEntry.Visible = false;
            //divSynopsis.Attributes.Add("style", "overflow-y: scroll;overflow-x: hidden; width: 100%; height: 180px; border:solid 1px gray;");
            DivAddSynopsis.Visible = false;
        }
    }
    protected void btnCancelSynopsis_OnClick(object sender, EventArgs e)
    {
        DivAddSynopsis.Visible = false;
        txtSynopsisText.Text = "";
        txtDocumentID.Text = "";
        SynID = 0;
        //tblSynopsisEntry.Visible = false;
        //divSynopsis.Attributes.Add("style", "overflow-y: scroll;overflow-x: hidden; width: 100%; height: 180px; border:solid 1px gray;");
    }
    protected void imgSynopsisEdit_OnClick(object sender, EventArgs e)
    {
        DivAddSynopsis.Visible = true;
        ImageButton imgSynopsisEdit = (ImageButton)sender;
        HiddenField hfSysID = (HiddenField)imgSynopsisEdit.Parent.FindControl("hfSysID");
        HiddenField hfDocumentID = (HiddenField)imgSynopsisEdit.Parent.FindControl("hfDocumentID");
        Label lblSysComments = (Label)imgSynopsisEdit.Parent.FindControl("lblSysComments");
        SynID = Common.CastAsInt32(hfSysID.Value);

        txtSynopsisText.Text = lblSysComments.Text;
        txtDocumentID.Text = hfDocumentID.Value;

        btnSaveSynopsis.Visible = true;
        fuSunopsis.Visible = true;
        aShoFile.Visible = false;
        
    }
    protected void imgSynopsisView_OnClick(object sender, EventArgs e)
    {
        DivAddSynopsis.Visible = true;
        ImageButton imgSynopsisView = (ImageButton)sender;
        HiddenField hfSysID = (HiddenField)imgSynopsisView.Parent.FindControl("hfSysID");
        HiddenField hfDocumentID = (HiddenField)imgSynopsisView.Parent.FindControl("hfDocumentID");
        HiddenField hfFile = (HiddenField)imgSynopsisView.Parent.FindControl("hfFile");
        
        Label lblSysComments = (Label)imgSynopsisView.Parent.FindControl("lblSysComments");
        SynID = Common.CastAsInt32(hfSysID.Value);

        txtSynopsisText.Text = lblSysComments.Text;
        txtDocumentID.Text = hfDocumentID.Value;
        btnSaveSynopsis.Visible = false;
        fuSunopsis.Visible = false;
        
        string FilePath = "";
        if (hfFile.Value == "")
        {
            FilePath = "#";
            aShoFile.Visible = false;
        }
        else
        {
            aShoFile.Visible = true;
            FilePath = "/EMANAGERBLOB/LPSQE/Insurance/" + hfFile.Value;
        }
        aShoFile.HRef = FilePath;
        //----------------------------------------
    }

    protected void imgSynopsisDel_OnClick(object sender, EventArgs e)
    {

        ImageButton imgSynopsisDel = (ImageButton)sender;
        HiddenField hfSysID = (HiddenField)imgSynopsisDel.Parent.FindControl("hfSysID");

        string sql = "Delete from IRM_Synopsis where SysID=" + hfSysID.Value + "";
        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            btnMsgSyn.Text = "Record deleted successfully.";
            BindSynopsis();
        }
    }
    

    // Office Comments
    protected void btnEnterOffComment_OnClick(object sender, EventArgs e)
    {
        //tblOfficeComment.Visible = true;
        divOfficeRemarks.Visible = true;
    }
    protected void btnSaveOffComment_OnClick(object sender, EventArgs e)
    {
        if (CaseID == 0)
        {
            lblMSGOff.Text = "Please save the case first.";
            return;
        }
        if (txtOfficeComm.Text.Trim()== "")
        {
            lblMSGOff.Text = "Please enter office comments.";
            return;
        }
        string strSQL = "EXEC sp_IRM_InsertUpdateIRM_OfficeRemarks " + OffRemID + "," + CaseID + ",'" + ddlPolicyNumber.SelectedValue + "','" + txtOfficeComm.Text.Trim().Replace("'", "`") + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
        DataTable dtOff = Budget.getTable(strSQL).Tables[0];
        if (dtOff.Rows.Count > 0)
        {
            btnMsgSyn.Text = "Record Successfully Saved.";
            txtOfficeComm.Text = "";
            BindOfficeComment();
            BindSynopsis();
            //tblOfficeComment.Visible = false;
            OffRemID = 0;
            divOfficeRemarks.Visible = false;
         }
    }
    protected void btnCancelOffComment_OnClick(object sender, EventArgs e)
    {
        //tblOfficeComment.Visible = false;
        divOfficeRemarks.Visible = false;
        OffRemID = 0;
        txtOfficeComm.Text = "";
    }

    protected void imgEditOffRem_OnClick(object sender, EventArgs e)
    {
        divOfficeRemarks.Visible = true;
        ImageButton btm = (ImageButton)sender;
        Label lblSysComments = (Label)btm.Parent.FindControl("lblSysComments");
        HiddenField hfOffID = (HiddenField)btm.Parent.FindControl("hfOffID");
        OffRemID = Common.CastAsInt32(hfOffID.Value);
        txtOfficeComm.Text = lblSysComments.Text;
    }
    protected void imgDelOffRem_OnClick(object sender, EventArgs e)
    {
        ImageButton btm = (ImageButton)sender;
        HiddenField hfOffID = (HiddenField)btm.Parent.FindControl("hfOffID");

        string sql = "delete  from dbo.IRM_OfficeRemarks where OffID=" + hfOffID.Value + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        BindOfficeComment();
    }
    public void BindOfficeComment()
    {

        string strSQL = "select OfficeRemarks as FullComment,replace(convert(varchar,UpdatedOn ,106) ,' ','-')UpdatedOn,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= OC.UpdatedBy)UpdatedBy ,( case when len( convert(varchar(7000),OfficeRemarks))>80 then substring(OfficeRemarks,1,75)+'............'else  OfficeRemarks end ) as ShortComment , * " +
                       " from IRM_OfficeRemarks OC where CaseID=" + CaseID.ToString() + " And PolicyID='" + ddlPolicyNumber.SelectedValue + "'";
            
        //" , * from IRM_OfficeRemarks OC where CaseID=" + CaseID.ToString() + " And PolicyID='" + ddlPolicyNumber.SelectedValue + "'";

        DataTable dtOff = Budget.getTable(strSQL).Tables[0];
         if (dtOff.Rows.Count > 0)
        {
            rptOfficeComment.DataSource = dtOff;
            rptOfficeComment.DataBind();
        }
        else
        {
            rptOfficeComment.DataSource = null;
            rptOfficeComment.DataBind();
        }
    }
    //Open Popups

    //protected void btnShowOtherDock_OnClick(object sender, EventArgs e)
    //{
    //    DivOtherDocs.Visible = true;
    //}
    //protected void btnShowExpencesAndRecovery_OnClick(object sender, EventArgs e)
    //{
    //    BindExpences();
    //    //BindClaimed();
    //    BindExpensesClaimed();
    //    DivExpences.Visible = true;
    //}
    //protected void btnShowCaseClosure_OnClick(object sender, EventArgs e)
    //{
    //    DivCaseClosure.Visible = true;
    //    BindClosure();
    //}

    //Closure
    //protected void btnCloseCaseClosure_OnClick(object sender, EventArgs e)
    //{
    //    DivCaseClosure.Visible = false;
    //}
    protected void btnSaveCaseClosure_OnClick(object sender, EventArgs e)
    {
        if (txtClosureComments.Text.Trim() == "")
        {
            lblMsgCaseClosure.Text = "Please enter closure comments.";
            lblMsgCaseClosure.Focus();return;
        }
        if (txtClosureDate.Text.Trim() == "")
        {
            lblMsgCaseClosure.Text = "Please enter closure date.";
            txtClosureDate.Focus(); return;
        }

        string sql = "update  IRM_casemaster set  " +
                    " ClosureComment='"+txtClosureComments.Text.Trim().Replace("'","`")+"' " +
                    " ,ClosureDate ='"+txtClosureDate.Text.Trim()+"' " +
                    " ,ClosedBy=" + Convert.ToInt32(Session["loginid"].ToString()) + " " +
                    " ,CaseStatus =2" +
                    " where CaseID="+CaseID+"";

        DataSet ds = Budget.getTable(sql);
        if (ds != null)
        {
            lblMsgCaseClosure.Text = "Record saved successfully.";
        }
    }

    // Recovery
    protected void chkRecovered_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkRecovered = (CheckBox)sender;
        ImageButton imgBtnUpdateRecovery = (ImageButton)chkRecovered.Parent.FindControl("imgBtnUpdateRecovery");
        TextBox txtRecoveredAmount = (TextBox)chkRecovered.Parent.FindControl("txtRecoveredAmount");
        TextBox txtRecoveredDate = (TextBox)chkRecovered.Parent.FindControl("txtRecoveredDate");

        if (chkRecovered.Checked)
        {
            //imgBtnUpdateRecovery.Visible = true;
            txtRecoveredAmount.Visible = true;
            txtRecoveredDate.Visible = true;
        }
        else
        {
            //imgBtnUpdateRecovery.Visible = false;
            txtRecoveredAmount.Visible = false;
            txtRecoveredDate.Visible = false;
        }

    }
    protected void imgBtnUpdateRecoveryDate_OnClick(object sender, EventArgs e)
    {
        ImageButton imgBtnUpdateRecovery = (ImageButton)sender;
        HiddenField hfExpID = (HiddenField)imgBtnUpdateRecovery.Parent.FindControl("hfExpID");
        
        ImageButton imgBtnUpdateRecoveryDate = (ImageButton)imgBtnUpdateRecovery.Parent.FindControl("imgBtnUpdateRecoveryDate");
        TextBox txtRecoveredAmount = (TextBox)imgBtnUpdateRecovery.Parent.FindControl("txtRecoveredAmount");
        TextBox txtRecoveredDate = (TextBox)imgBtnUpdateRecovery.Parent.FindControl("txtRecoveredDate");

        string sql = "UPDATE  IRM_CaseExpenceDetails SET  "+
	                 "  Recovered=1 "+
                      " ,RecoveredAmount=" + txtRecoveredAmount .Text.Trim()+ " " +
                      " ,RecoveredDate='" + txtRecoveredDate.Text.Trim() + "' " +
                      " WHERE ExpID=" + hfExpID.Value+ "";

        DataSet dtExp = Budget.getTable(sql);
        //BindClaimed();
    }
    
    #endregion -------------------------------------------------

    #region ---------- TEXT CHANGE -----------------------------
   
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        TextBox txtAmount = (TextBox)sender;
        decimal amount = Common.CastAsDecimal(txtAmount.Text.Trim());
        txtAmount.Text = string.Format("{0:C}", amount).Trim().Replace("Rs.", "").Trim();
    }

    protected void txtIncidentDate_OnTextChanged(object sender, EventArgs e)
    {
        UP8.Update();
    }
    #endregion

    #region -------------- UDF ---------------------------------

    public void ShowRecord()
    {
        ddl_Vessels.Enabled = false;
        ddl_Groups.Enabled = false;

        string sql = " SELECT CM.CaseID " +
                        " ,PM.DeductibleAmount" +
                        " ,CM.VesselId" +
                        " ,CM.PolicyNo " +
                        " ,CM.GroupID " +
                        " ,CM.SubGroupID " +
                        " ,CM.CaseNumber " +
                        " ,CM.CompanyCaseNumber " +

                        " ,CM.LocationType " +
                        " ,CM.IncidentPlaceTo " +
                        

                        " ,GP.GroupName " +
                        
                        " ,CM.CaseDedectibleAmount" +                        
                        " ,UWM.UWID " +
                        " ,UWM.ShortName" +
                        " ,IncidentDate" +
                        " ,CM.IncidentDesc " +
                        " ,CM.ClaimAmount " +
                        //" ,(select sum(TotalUSDoler) from IRM_CaseExpenceDetails CD where CD.caseid=CM.caseid )TotClaimedAmount" + //and CD.claimed=0

                        " ,CM.CrewRank " +
                        " ,CM.CrewName " +
                        " ,CM.CrewNo" +
                        " ,CM.IncidentPlace" +
                        
                        
                        " ,CM.ExpDetail " +
                        " ,CM.ExpAmount " +
                        " ,CM.ExpLoacalCurr " +
                        " ,CM.ExpTotalUSDoler " +
                        " ,CM.ExpClaimed " +
                        " ,(select isnull(sum(TotalAmountUS$),0) from IRM_CaseExpensesClaimed where caseID=" + CaseID + ")TotClaimedAmount" +
                        " ,(select isnull(sum(TotalRecoveredAmount),0) from IRM_CaseExpensesClaimed where caseID="+CaseID+") RecoveredAmount" +

                        " ,ClaimDate " +
                        " ,(case when CM.CaseStatus=1 then 'Open' when CM.CaseStatus=2 then 'Close' end)CaseStatus " +
                        " FROM dbo.IRM_CaseMaster CM " +
                        " INNER JOIN IRM_Groups GP ON GP.GroupId = CM.GroupId  " +
                        " INNER JOIN IRM_UWMaster UWM ON UWM.UWId = CM.UWId  " +
                        " LEFT JOIN IRM_PolicyMaster PM on PM.PolicyNo=CM.PolicyNo where CM.CaseID=" + CaseID + "";
        DataTable dtSearchResult = Budget.getTable(sql).Tables[0];
        if (dtSearchResult.Rows.Count > 0)
        {
            //trMTMCaseNumber.Visible = true;
            DataRow Dr = dtSearchResult.Rows[0];
            ddl_Vessels.SelectedValue = Dr["VesselId"].ToString();
            ShowVesselDetails();
            ddl_Groups.SelectedValue = Dr["GroupID"].ToString();

            BindSubGroups();

            try
            { ddlSubGroup.SelectedValue = Dr["SubGroupID"].ToString(); }
            catch (Exception ez)
            { ddlSubGroup.SelectedIndex = 0; }

            BindUW();

            try
            {
                ddl_UW.SelectedValue = Dr["UWID"].ToString();
            }
            catch (Exception ez)
            { ddl_UW.SelectedIndex = 0; }

            BindPolicyNo();
                try
            {
                ddlPolicyNumber.SelectedValue = Dr["PolicyNo"].ToString();
            }
            catch (Exception ez)
                { ddlPolicyNumber.SelectedIndex = 0; }
            if (Dr["IncidentDate"].ToString() != "")
            {
                DateTime IncidentDate = Convert.ToDateTime(Dr["IncidentDate"].ToString().Substring(0,11));
                txtIncidentDate.Text = IncidentDate.ToString("dd-MMM-yyyy");
                //ddlHour.SelectedValue =  Dr["IncidentDate"].ToString().Substring(12,2);
                //ddlMin.SelectedValue =Dr["IncidentDate"].ToString().Substring(15,2);
                ddlHour.SelectedValue = IncidentDate.Hour.ToString();
                ddlMin.SelectedValue = IncidentDate.Minute.ToString();
            }
            else
                txtIncidentDate.Text = "";
            txtDeductibleAmt.Text = FormatCurrency(Dr["DeductibleAmount"]).ToString();
            txtCaseDeductibleAmt.Text = FormatCurrency(Dr["CaseDedectibleAmount"]).ToString();

            txtCaseDesc.Text = Dr["IncidentDesc"].ToString();
            txtCaseNumber.Text = Dr["CaseNumber"].ToString();
            if (Dr["CompanyCaseNumber"].ToString()!="")
                txtMTMCaseNumber.Text = Dr["CompanyCaseNumber"].ToString();            
            else
                txtMTMCaseNumber.Text = ddl_Vessels.SelectedValue + "/" + DateTime.Now.Year.ToString().Substring(2) +"/"+ ddl_Groups.SelectedItem.Text + "/"+Dr["CaseID"].ToString();

            txtCrewRank.Text = Dr["CrewRank"].ToString();
            txtCrewName.Text = Dr["CrewName"].ToString();
            txtCrewNo.Text = Dr["CrewNo"].ToString();


            if (Dr["LocationType"].ToString() == "2")
            {
                trIncidentToPort.Visible = true;
                txtToPort.Text = Dr["IncidentPlaceTo"].ToString();
            }   
            else
            {
                trIncidentToPort.Visible = false;   
            }

            txtIncidentPlace.Text = Dr["IncidentPlace"].ToString();
                

            lblTotalAmountRecd.Text = FormatCurrency(Dr["RecoveredAmount"]).ToString();


            txtClaimAmmount.Text = FormatCurrency(Dr["TotClaimedAmount"]).ToString();//ClaimAmount
          
            txtExpencesClaimedDetails.Text = Dr["ExpDetail"].ToString();
            txtExpClaimAmount.Text = Dr["ExpAmount"].ToString();
            txtLocalCurr.Text = Dr["ExpLoacalCurr"].ToString();
            txtTotUSDoler.Text = Dr["ExpTotalUSDoler"].ToString();
            txtClaimed.Text = Dr["ExpClaimed"].ToString();
            txtDateOfClaim.Text = (Dr["ClaimDate"].ToString() == "01-Jan-1900") ? "" :Common.ToDateString(Dr["ClaimDate"]);


            if (ddl_Groups.SelectedItem.Text.ToUpper().Trim() == "PNI")
            {
                if (ddlSubGroup != null)
                {
                    if (ddlSubGroup.SelectedItem.Text.ToUpper().Trim() == "PNI CREW")
                        tblCrew.Visible = true;
                    else
                        tblCrew.Visible = false;
                }
            }
            else
                tblCrew.Visible = false;            
        }
                        
    }
    public void SetPolicyDeductibleAmount()
    {
        string sql = "Select DeductibleAmount from  IRM_PolicyMaster where PolicyNo='"+ddlPolicyNumber.SelectedValue+"'";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            txtDeductibleAmt.Text = FormatCurrency( dt.Rows[0][0]);
        }
    }
    public void BindGroups()
    {
        try
        {
            string sql = "select GroupId,ShortName  from IRM_groups G where (select Count(SubGroupID) from IRM_Subgroups SG where G.groupid=SG.groupid)>0";
            //DataTable dtGroups = Budget.getTable("SELECT GroupId,ShortName FROM IRM_Groups ORDER BY GroupName").Tables[0];
            DataTable dtGroups = Budget.getTable(sql).Tables[0];
            ddl_Groups.DataSource = dtGroups;
            ddl_Groups.DataTextField = "ShortName";
            ddl_Groups.DataValueField = "GroupId";
            ddl_Groups.DataBind();
            ddl_Groups.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindSubGroups()
    {
        try
        {
            this.ddlSubGroup.Items.Clear();
            string sql = "SELECT SubGroupId,SubGroupName FROM IRM_SubGroups WHERE GroupId = " + ddl_Groups.SelectedValue.Trim() + " ORDER BY SubGroupName";
            DataTable dtSubGroups = Budget.getTable(sql).Tables[0];
            this.ddlSubGroup.DataSource = dtSubGroups;
            this.ddlSubGroup.DataValueField = "SubGroupId";
            this.ddlSubGroup.DataTextField = "SubGroupName";
            this.ddlSubGroup.DataBind();
            this.ddlSubGroup.Items.Insert(0, new ListItem("< Select >", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindVessels()
    {
        try
        {
            DataTable dtVessels = Budget.getTable("select VESSELNAME,VESSELID FROM dbo.vessel v order by vesselname").Tables[0];

            this.ddl_Vessels.DataSource = dtVessels;
            this.ddl_Vessels.DataValueField = "VESSELID";
            this.ddl_Vessels.DataTextField = "VESSELNAME";
            this.ddl_Vessels.DataBind();

            this.ddl_Vessels.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void BindUW()
    {

        string strUW = "select distinct  CM.UWID ,UM.ShortName FROM IRM_PolicyMaster CM  " +
                       " Left Join IRM_UWMaster UM on UM.UWID=CM.UWID " +
                       "  where GroupID=" + ddl_Groups.SelectedValue + " and VesselID='" + ddl_Vessels.SelectedValue + "' ";

        //"SELECT VesselID,GroupID,(SELECT ShortName FROM IRM_UWMaster UW where UW.UWID=CM.UWID)ShortName , (SELECT UWID FROM IRM_UWMaster UW where UW.UWID=CM.UWID)UWID FROM IRM_PolicyMaster CM where GroupID=" + ddl_Groups.SelectedValue + " and VesselID='" + ddl_Vessels.SelectedValue + "'";
        DataTable dtUW = Budget.getTable(strUW).Tables[0];
        ddl_UW.DataSource = dtUW;
        ddl_UW.DataTextField = "ShortName";
        ddl_UW.DataValueField = "UWId";
        ddl_UW.DataBind();
        ddl_UW.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void BindPolicyNo()
    {
        string strPolcy = "SELECT VesselID,GroupID,UWID ,PolicyNo FROM IRM_PolicyMaster CM where VesselID='" + ddl_Vessels.SelectedValue + "' and GroupID=" + ddl_Groups.SelectedValue + " and UWID=" + ddl_UW.SelectedValue + "";
        DataTable DtPolcy = Budget.getTable(strPolcy).Tables[0];
        ddlPolicyNumber.DataSource = DtPolcy;
        ddlPolicyNumber.DataTextField = "PolicyNo";
        ddlPolicyNumber.DataValueField = "PolicyNo";
        ddlPolicyNumber.DataBind();
        ddlPolicyNumber.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void GetNextPolicyNo()
    {
        string strPolicyNo = "SELECT SUBSTRING(MAX(PolicyNo),0,3) + REPLACE(STR(ISNULL(CONVERT(int, SUBSTRING(MAX(PolicyNo),3,LEN(MAX(PolicyNo)))),0) + 1, 6 ),' ','0' ) AS PolicyNo FROM IRM_PolicyMaster ";
        DataTable dtGroups = Budget.getTable(strPolicyNo).Tables[0];

    }
    public void BindDocs()
    {

        string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy,* from IRM_CaseDocuments CS where CaseID="+CaseID.ToString()+" And PolicyID='" + ddlPolicyNumber.SelectedValue + "'";
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtDocs;
            rptDocs.DataBind();
        }
        else
        {
            rptDocs.DataSource = null;
            rptDocs.DataBind();
        }
    }
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlHour.Items.Add("0" + i);

            }
            else
            {
                ddlHour.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));

            }
        }

        for (int j = 0; j < 60; j++)
        {
            if (j < 10)
            {
                ddlMin.Items.Add("0" + j);

            }
            else
            {
                ddlMin.Items.Add(new ListItem(Convert.ToString(j), Convert.ToString(j)));

            }
        }
    }
    public void ShowButtons()
    {
        if (Mode == "V")
        {
            btnCaseSave.Visible = false;
            BtnCaseCancel.Visible = false;
            tblDocument.Visible = false;
        }
        else
        {
            btnCaseSave.Visible = true;
            BtnCaseCancel.Visible = true;
            tblDocument.Visible = true;
        }
    }
    public void ShowVesselDetails()
    {
        //if (ddl_Vessels.SelectedIndex != 0)
        //{
        //    string SQL = "SELECT FS.FlagStateName, VM.YearBuilt, VM.LDT ,VT.VesselTypeName FROM dbo.Vessel VM " +
        //                 "INNER JOIN dbo.VesselType VT ON VT.VesselTypeId = VM.VesselTypeId " +
        //                 "INNER JOIN dbo.FlagState FS ON FS.FlagStateId = VM.FlagStateId " +
        //                 "WHERE  VM.VesselId =" + ddl_Vessels.SelectedValue.Trim();
        //    DataTable dt = Budget.getTable(SQL).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        lbl_VesselType.Text = dt.Rows[0]["VesselTypeName"].ToString();
        //        lbl_Flag.Text = dt.Rows[0]["FlagStateName"].ToString();
        //        lbl_YearBuild.Text = dt.Rows[0]["YearBuilt"].ToString();
        //        lbl_GrossTonnage.Text = dt.Rows[0]["LDT"].ToString();
        //    }
        //    else
        //    {
        //        lbl_VesselType.Text = "";
        //        lbl_Flag.Text = "";
        //        lbl_YearBuild.Text = "";
        //        lbl_GrossTonnage.Text = "";
        //    }
        //}
        //else
        //{
        //    lbl_VesselType.Text = "";
        //    lbl_Flag.Text = "";
        //    lbl_YearBuild.Text = "";
        //    lbl_GrossTonnage.Text = "";
        //}
        BindUW();
        BindPolicyNo();
    
    }
    public void BindCurrency()
    {
        DataSet ds = new DataSet();

        try
        {
            // To run on client side
            string strSQL = "SELECT Curr FROM DBO.VW_tblWebCurr ORDER BY Curr";
            ds = Budget.getTable(strSQL);
        }
        catch (Exception ex)
        {
            // To run My side
            //SqlConnection myConnection = new SqlConnection();
            //myConnection.ConnectionString = "Data Source=172.30.1.10;Initial Catalog=MTMPOS;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!;Connection Timeout=300;";
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = myConnection;
            //SqlDataAdapter Adp = new SqlDataAdapter();
            //Adp.SelectCommand = cmd;
            //cmd.CommandText = "SELECT Curr FROM VW_tblWebCurr ORDER BY Curr";
            //Adp.Fill(ds, "tbl");        
        }


        ddlLocalCurr.DataSource = ds;
        ddlLocalCurr.DataTextField = "Curr";
        ddlLocalCurr.DataValueField = "Curr";
        ddlLocalCurr.DataBind();
        ddlLocalCurr.Items.Insert(0, new ListItem("< Select >", "0"));
        ddlLocalCurr.SelectedIndex = 0;
    }

    public void CurrencyConversion()
    {
        try
        {
            DateTime dt = Convert.ToDateTime(txtExpDate.Text);
        }

        catch (Exception ex)
        {
            lblMsgExpence.Text = "Expense date is invalid.";
            return;
        }

        DataSet ds = new DataSet();
        // To run on client side
        try
        {
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + ((txtExpDate.Text == "") ? DateTime.Today.ToShortDateString() : txtExpDate.Text.Trim()) + "' and For_Curr='" + ddlLocalCurr.SelectedValue + "' order by RateDate desc";
            ds = Budget.getTable(sql);
        }
        catch (Exception ex)
        {
             //To run on my side
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
            SqlDataAdapter Adp = new SqlDataAdapter();
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + ((txtExpDate.Text == "") ? DateTime.Today.ToShortDateString() : txtExpDate.Text.Trim()) + "' and For_Curr='" + ddlLocalCurr.SelectedValue + "' order by RateDate desc";
            Adp.SelectCommand = cmd;
            cmd.CommandText = sql;
            Adp.Fill(ds, "tbl");
        }


        if (ds != null)
        {
            DataTable Dt = ds.Tables[0];
            decimal CurrRate = 0;
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    CurrRate = Math.Round(Common.CastAsDecimal(Dt.Rows[0][0]), 2);
                }
            }
            txtRate.Text = CurrRate.ToString();
            if (txtAmount.Text != "")
            {
                if (CurrRate != 0)
                    lblTotalUSDoler.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtAmount.Text) / Convert.ToDecimal( txtRate.Text )), 2));
                else
                    lblTotalUSDoler.Text = "0";
            }
            else
                lblTotalUSDoler.Text = "0";
            

        }
    }

    public void BindExpences()
    {
        string sql = "SELECT ExpID,Row_number() over (order by ExpID )Row " +
                     " ,CaseID "+
                      " ,PolicyNo "+
                      " ,replace(convert(varchar,Date,106),' ','-')Date "+
                      " ,Description "+
                      " ,( case when len( convert(varchar(7000),Description))>50 then substring(Description,1,45)+'............'else  Description end ) as ShortComment" +
                      " ,Amount "+
                      " ,Rate " +
                      " ,(select sum(TotalUSDoler) from IRM_CaseExpenceDetails where caseID="+CaseID+")TotalCalaimAmount "+
                      " ,LocalCurr "+
                      " ,TotalUSDoler "+
                      " ,Claimed ,BoucherNo ,Status as StatusID " +
                      " ,(case when Status=1 then 'In Progeress' when Status=2 then 'Committed Cost' else '' End)Status" +
                      " ,replace(convert(varchar,ClaimedDate,106),' ','-') ClaimedDate" +
                      " ,ClaimFile " +
                  " FROM dbo.IRM_CaseExpenceDetails where CaseID=" + CaseID + " and claimed = 0";
        DataTable dtExpence = Budget.getTable(sql).Tables[0];
        
        rptExpences.DataSource = dtExpence;
        rptExpences.DataBind();
        lblTotalClaimedAmount.Text = FormatCurrency(dtExpence.Compute("sum(TotalUSDoler)", "").ToString());
        if (dtExpence.Rows.Count > 0)
        {
            //txtClaimAmmount.Text = FormatCurrency(dtExpence.Rows[0]["TotalCalaimAmount"].ToString());
        }
    }
    
    

    public void BindSynopsis()
    {
        
        string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy "+
            ",( case when len( convert(varchar(7000),SysComments))>80 then substring(SysComments,1,75)+'............'else  SysComments end ) as ShortComment" +

            " , * from IRM_Synopsis CS where CaseID=" + CaseID.ToString() + " And PolicyID='" + ddlPolicyNumber.SelectedValue + "' order by SynopsisDate DESC";
        DataTable dtSyn = Budget.getTable(strSQL).Tables[0];
        
        if (dtSyn.Rows.Count > 0)
        {
            rptSynopsis.DataSource = dtSyn;
            rptSynopsis.DataBind();
        }
        else
        {
            rptSynopsis.DataSource = null;
            rptSynopsis.DataBind();
        }
    }
    public void BindClosure()
    {
        string strSQL = "select ClosureComment ,replace(convert(varchar,ClosureDate,106),' ','-')ClosureDate from IRM_casemaster where CaseID=" + CaseID + "";
        DataTable dtClouser = Budget.getTable(strSQL).Tables[0];
        if (dtClouser.Rows.Count > 0)
        {
            if (dtClouser.Rows[0]["ClosureComment"].ToString() != "")
            {
                btnSaveCaseClosure.Visible = false;
                txtClosureComments.Text = dtClouser.Rows[0]["ClosureComment"].ToString();
                txtClosureDate.Text = dtClouser.Rows[0]["ClosureDate"].ToString();
            }
            else
            {
                btnSaveCaseClosure.Visible = true;
            }
        }
    }

    public void SetMTMCaseNumber()
    {
        if (ddl_Vessels.SelectedIndex != 0 && ddl_Groups.SelectedIndex != 0 && txtIncidentDate.Text.Trim() != "")
        {
            if (CaseID == 0)
            {
                int year = 0;
                string sqlSN = "select isnull(max(CompanySerialNo),0)+1 from IRM_CaseMaster where right(left(IncidentDate,11),4)=" + txtIncidentDate.Text.Trim().Substring(7) + ""; //year(dateadd(yy,1,getdate()))   GroupID=" + ddl_Groups.SelectedValue + " and  
                DataTable dt = Budget.getTable(sqlSN).Tables[0];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MTMSrNO = Common.CastAsInt32(dt.Rows[0][0]);
                        DataTable dtYear = Budget.getTable("select top 1 right(left(IncidentDate,11),4)Year   from IRM_CaseMaster order by caseid desc").Tables[0];

                        if (dtYear.Rows.Count > 0)
                            year = Common.CastAsInt32(dtYear.Rows[0][0]);

                        if (year == DateTime.Now.Year)
                            MTMSrNO = Common.CastAsInt32(dt.Rows[0][0]);
                        else
                            MTMSrNO = 1;
                    }
                }
                string VessCode = "";
                DataTable dtVess = Budget.getTable("select VesselCode from dbo.vessel V where V.VesselID=" + ddl_Vessels.SelectedValue + "").Tables[0];
                if (dtVess.Rows.Count > 0)
                    VessCode = dtVess.Rows[0][0].ToString();
                txtMTMCaseNumber.Text = VessCode + "/" + txtIncidentDate.Text.Trim().Substring(9) + "/" + ((ddl_Groups.SelectedItem.Text == "< Select >") ? "0" : ddl_Groups.SelectedItem.Text) + "/" + MTMSrNO.ToString();
            }
        }
        else
        {
            txtMTMCaseNumber.Text = "";
        }
    }
   
    public string FormatCurrency(object InValue)
    {
        string DecimalValue = "";
        string StrIn = InValue.ToString();
        int Index = StrIn.IndexOf('.');
        if(Index !=-1)
            DecimalValue = StrIn.Substring(Index );

        StrIn = Common.CastAsInt32(Math.Truncate(Common.CastAsDecimal( StrIn))).ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) 
        {
            OutValue = OutValue.Substring(0, OutValue.Length - 1); 
        }
        return OutValue + ((DecimalValue == "") ? ".00" : DecimalValue);
    }
    #endregion -------------------------------------------------
   
}
 