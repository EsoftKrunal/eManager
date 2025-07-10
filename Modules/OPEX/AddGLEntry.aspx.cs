using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;




public partial class Modules_OPEX_AddGLEntry : System.Web.UI.Page
{
    public string vesselCode
    {
        get { return Convert.ToString(ViewState["vesselCode"]); }
        set { ViewState["vesselCode"] = value; }
    }

    public int glperiod
    {
        get { return Convert.ToInt32(ViewState["glperiod"]); }
        set { ViewState["glperiod"] = value; }
    }

    public int glyear
    {
        get { return Convert.ToInt32(ViewState["glyear"]); }
        set { ViewState["glyear"] = value; }
    }

    public int TempGLId
    {
        get { return Convert.ToInt32(ViewState["TempGLId"]); }
        set { ViewState["TempGLId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                BindVessel();
                BindYear();
                BindCurrency();
                BindAccount();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    public void BindVessel()
    {
        DataSet dsVessel = new DataSet();
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where 1=1 ";
        sql = sql + WhereClause + " and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME";
        dsVessel = VesselReporting.getTable(sql);
        ddlVessel.DataSource = dsVessel.Tables[0];

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));  

        ddlVesselGL.DataSource = dsVessel.Tables[0];
        ddlVesselGL.DataTextField = "Vesselname";
        ddlVesselGL.DataValueField = "VesselCode";
        ddlVesselGL.DataBind();
        ddlVesselGL.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    private void BindYear()
    {
        ddlYear.Items.Add(new ListItem("< Select >", "0"));
      //  ddlYearGL.Items.Add(new ListItem("< Select >", "0"));
        for (int i = DateTime.Today.Year; i >= 2023; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
          //  ddlYearGL.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVessel.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Vessel.');", true);
                ddlVessel.Focus();
                return;
            }

            if (ddlYear.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Year.');", true);
                ddlYear.Focus();
                return;
            }
            GetGLEntry(ddlVessel.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlPosted.SelectedValue));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    private void GetGLEntry(string vesselCode, int year, int IsPosted)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC Sp_GETtblGLEntry '" + vesselCode + "'," + year + " , " + IsPosted + "");
        if (dt.Rows.Count > 0)
        {
            RptGlEntry.DataSource = dt;
            RptGlEntry.DataBind();
        }
        else
        {
            RptGlEntry.DataSource = null;
            RptGlEntry.DataBind();
        }
    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        try
        {
            dv_GLDetails.Visible = false;
            clearGLData();
            TempGLId = 0;
            GetGLEntry(vesselCode, glyear, Convert.ToInt32(ddlPosted.SelectedValue));
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    public void BindAccount()
    {
        ddlAccount.Items.Clear();
        string sql = "select AccountID,convert(varchar, AccountNumber)+'-'+AccountName as AccountNumber from vw_sql_tblSMDPRAccounts where Active = 'Y' order by AccountNumber";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlAccount.DataSource = dsPrType;
        ddlAccount.DataTextField = "AccountNumber";
        ddlAccount.DataValueField = "AccountID";
        ddlAccount.DataBind();
        ddlAccount.Items.Insert(0, new ListItem("< Select >", "0"));
        ddlAccount.SelectedIndex = 0;
    }

    public void BindCurrency()
    {
        ddlCurrency.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM VW_tblWebCurr ORDER BY Curr");
        ddlCurrency.DataTextField = "Curr";
        ddlCurrency.DataValueField = "Curr";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("< Select >", ""));
        ddlCurrency.SelectedIndex = 0;
    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearGLData();
            int glId = 0;
            TempGLId = 0;

            glId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            TempGLId = glId;
            if (glId > 0)
            {
                dv_GLDetails.Visible = true;
                DataTable dt = new DataTable();
                dt = Common.Execute_Procedures_Select_ByQuery("EXEC Sp_GETtblGLEntry '" + ddlVessel.SelectedValue + "'," + ddlYear.SelectedValue + ","+ddlPosted.SelectedValue+","+ glId + " ");
                if (dt.Rows.Count > 0)
                {
                    btnPosted.Visible = true;
                    lbAddUpdateDoc.Visible = true;
                    ddlVesselGL.SelectedValue = dt.Rows[0]["VesselCode"].ToString();
                    //ddlMonthGL.SelectedValue = dt.Rows[0]["Period"].ToString();
                    //ddlYearGL.SelectedValue = dt.Rows[0]["Year"].ToString();
                  
                    lblGLRefNo.Text = dt.Rows[0]["GLRefNo"].ToString();
                    lblAmtUSD.Text = dt.Rows[0]["AmountUSD"].ToString();
                    txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
                    txtTransactionDate.Text = Common.ToDateString(dt.Rows[0]["TransDate"].ToString());
                    lblGLEntryDt.Text = Common.ToDateString(dt.Rows[0]["GLEntryDate"]);
                    lblAddedBy.Text = dt.Rows[0]["EnteredBy"].ToString();
                    lblAddedOn.Text = Common.ToDateString(dt.Rows[0]["EnteredOn"].ToString());
                    vesselCode = dt.Rows[0]["VesselCode"].ToString();
                    glperiod = Convert.ToInt32(dt.Rows[0]["Period"].ToString());
                    glyear = Convert.ToInt32(dt.Rows[0]["Year"].ToString());
                    
                    if (Convert.ToInt32(dt.Rows[0]["AccountId"].ToString()) > 0)
                    {
                        ddlAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                    }
                    GetDocCount(ddlVesselGL.SelectedValue, TempGLId);
                    if (dt.Rows[0]["IsPosted"].ToString() == "Yes")
                    {
                        trPosted.Visible = true;
                        btnSave.Visible = false;
                        btnPosted.Visible = false;
                        if (Convert.ToInt32(dt.Rows[0]["LinkGLId"].ToString()) > 0 || Convert.ToBoolean(dt.Rows[0]["IsVerifiedByOffice"]))
                        {
                            btnReverseGLEntry.Visible = false;
                        }
                        else
                        {
                            btnReverseGLEntry.Visible = true;
                        }
                        lblPostedStatus.Text = dt.Rows[0]["IsPosted"].ToString();
                        lblPostedBy.Text = dt.Rows[0]["PostedBy"].ToString();
                        lblPostedOn.Text = Common.ToDateString(dt.Rows[0]["IsPostedOn"].ToString());
                        ddlVesselGL.Enabled = false;
                        //ddlMonthGL.Enabled = false;
                        //ddlYearGL.Enabled = false;
                        txtAmount.ReadOnly = true;
                        ddlCurrency.Enabled = false;
                        ddlAccount.Enabled = false;
                       // txtTransactionDate.ReadOnly = true;
                    }
                    else
                    {
                        trPosted.Visible = false;
                        btnSave.Visible = true;
                        btnPosted.Visible = true;
                        lblPostedStatus.Text = dt.Rows[0]["IsPosted"].ToString();
                        lblPostedBy.Text = "";
                        lblPostedOn.Text = "";
                        ddlVesselGL.Enabled = true;
                        //ddlMonthGL.Enabled = true;
                        //ddlYearGL.Enabled = true;
                        txtAmount.ReadOnly = false;
                        ddlCurrency.Enabled = true;
                        ddlAccount.Enabled = true;
                    }
                }
                else
                {
                    ddlVesselGL.SelectedValue = ddlVessel.SelectedValue;

                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void clearGLData()
    {
        //ddlYearGL.SelectedIndex = 0;
        ddlVesselGL.SelectedIndex = 0;
        ddlAccount.SelectedIndex = 0;
        ddlCurrency.SelectedIndex = 0;
        txtAmount.Text = "";
        lblAmtUSD.Text = "";
        txtTransactionDate.Text = "";
        //ddlMonthGL.SelectedIndex = 0;
        txtRemark.Text = "";
        lblGLEntryDt.Text = "";
        lblAddedBy.Text = "";
        lblAddedOn.Text = "";
        lblGLRefNo.Text = "";
        btnPosted.Visible = false;
        trPosted.Visible = false;
        // ddlAccount.SelectedIndex = 0;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
       try
        {
            if (ddlVesselGL.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Vessel.');", true);
                ddlVesselGL.Focus();
                return;
            }
            //if (ddlMonthGL.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select GL Month.');", true);
            //    ddlMonthGL.Focus();
            //    return;
            //}
            //if (ddlYearGL.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select GL Year.');", true);
            //    ddlYearGL.Focus();
            //    return;
            //}
            if (ddlAccount.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Account.');", true);
                ddlAccount.Focus();
                return;
            }
          

            int currMonth = 0;
            int currYear = 0;
            currMonth = DateTime.Now.Month;
            currYear = DateTime.Now.Year;
            //if (Convert.ToInt32(ddlMonthGL.SelectedValue)  > currMonth && Convert.ToInt32(ddlYearGL.SelectedValue)  >= currYear)
            //{
            //    if (Convert.ToInt32(ddlMonthGL.SelectedValue)   > currMonth)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('GL Month should be less or equal current month.');", true);
            //        ddlMonthGL.Focus();
            //        return;
            //    }
            //    if (Convert.ToInt32(ddlYearGL.SelectedValue)  > currYear)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('GL Year should be less or equal current year.');", true);
            //        ddlYearGL.Focus();
            //        return;
            //    }
            //}

            if (string.IsNullOrEmpty(txtTransactionDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Please enter Transaction date.');", true);
                txtTransactionDate.Focus();
            }

            DateTime start = DateTime.Parse(txtTransactionDate.Text.Trim());
            DateTime today = DateTime.Today;
            if (start > today)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Transaction Date should be less or equal today's date.');", true);
                // txtTransactionDate.Text = "";
                txtTransactionDate.Focus();
            }

            Boolean isMonthClosed = false;
            string str = "Select ISNULL((SELECT top 1 perClosed  FROM Vessel v with(nolock) Inner join dbo.tblPeriodMaint  a  with(nolock) on a.Cocode = v.AccCode where a.rptYear=YEAR('" + start.ToString() + "') and v.VesselCode='" + ddlVesselGL.SelectedValue + "' AND a.rptPeriod = MONTH('" + start.ToString() + "')),0)";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(str);
            if (dt.Rows.Count > 0)
            {
                isMonthClosed = Convert.ToBoolean(dt.Rows[0][0]);
            }
            if (isMonthClosed)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('In Published Report Month is already closed, System should not allow to add GL Entry for Selected Transaction Date.');", true);
                //ddlMonthGL.Focus();
                txtTransactionDate.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter Remark.');", true);
                txtRemark.Focus();
                return;
            }
            Common.Set_Procedures("SP_InsertGLEntry");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                new MyParameter("@GLid", TempGLId),
                new MyParameter("@VesselCode", ddlVesselGL.SelectedValue),
                //new MyParameter("@Period", ddlMonthGL.SelectedValue.Trim()),
                //new MyParameter("@Year", ddlYearGL.SelectedValue.Trim()),
                new MyParameter("@AccountId", ddlAccount.SelectedValue.Trim()),
                new MyParameter("@Amount", Convert.ToDecimal(txtAmount.Text.Trim())),
                new MyParameter("@Currency", ddlCurrency.SelectedValue.Trim()),
                new MyParameter("@TransDate", txtTransactionDate.Text.Trim()),
                new MyParameter("@AddedBy", Session["Loginid"].ToString()),
                new MyParameter("@Remark", txtRemark.Text.Trim())
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                TempGLId = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
                lblGLRefNo.Text = ds.Tables[0].Rows[0][1].ToString();
                lblAmtUSD.Text = ds.Tables[0].Rows[0][2].ToString(); 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('GL Entry saved successfully.');", true);
                // dv_GLDetails.Visible = false;
                lbAddUpdateDoc.Visible = true;
                GetGLEntry(ddlVessel.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlPosted.SelectedValue));
                // btnPosted.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record.');", true);  
            }

        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record. " + ex.Message.ToString() + "');", true);
        }
       
    }

    protected void btnAddGLENtry_Click(object sender, EventArgs e)
    {
        clearGLData();
        TempGLId = 0;
        dv_GLDetails.Visible = true;
        btnPosted.Visible = false;
        btnReverseGLEntry.Visible = false;
        btnSave.Visible = true;
        ddlVesselGL.Enabled = true;
        //ddlMonthGL.Enabled = true;
        //ddlYearGL.Enabled = true;
        txtAmount.ReadOnly = false;
        ddlCurrency.Enabled = true;
        ddlAccount.Enabled = true;
    }

    protected void btnPosted_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAccount.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Account.');", true);
                ddlYear.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter Remark.');", true);
                txtRemark.Focus();
                return;
            }
            if (TempGLId > 0)
            {
                Common.Execute_Procedures_Select_ByQuery("Update tblGLEntry SET AccountId = " + ddlAccount.SelectedValue + ",Remark = '" + txtRemark.Text.Trim() + "' ,IsPosted = 1,IsPostedBy = " + Convert.ToInt32(Session["LoginId"].ToString()) + " ,  IsPostedOn = getdate() where  GLId = " + TempGLId + "");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('GL Entry posted successfully.');", true);
                dv_GLDetails.Visible = false;
                GetGLEntry(ddlVessel.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlPosted.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record. " + Common.getLastError() + "');", true);
        }
    }

    //protected void txtTransactionDate_TextChanged(object sender, EventArgs e)
    //{


    //}

    protected void btnReverseGLEntry_Click(object sender, EventArgs e)
    {
        try
        {
            //int currMonth = 0;
            //int currYear = 0;
            //currMonth = DateTime.Now.Month;
            //currYear = DateTime.Now.Year;
            //if (Convert.ToInt32(ddlMonthGL.SelectedValue) > currMonth && Convert.ToInt32(ddlYearGL.SelectedValue) >= currYear)
            //{
            //    if (Convert.ToInt32(ddlMonthGL.SelectedValue) > currMonth)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('GL Month should be less or equal current month.');", true);
            //        ddlMonthGL.Focus();
            //        return;
            //    }
            //    if (Convert.ToInt32(ddlYearGL.SelectedValue) > currYear)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('GL Year should be less or equal current year.');", true);
            //        ddlYearGL.Focus();
            //        return;
            //    }
            //}
            if (TempGLId == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('In Vallid GL Entry. Please try again.');", true);
                return;
            }

            if (String.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please enter Remark.');", true);
                txtRemark.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTransactionDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Please enter Transaction date.');", true);
                txtTransactionDate.Focus();
            }

            Boolean isMonthClosed = false;
            DateTime start = DateTime.Parse(txtTransactionDate.Text.Trim());
            DateTime today = DateTime.Today;
            string str = "Select ISNULL((SELECT top 1 perClosed  FROM Vessel v with(nolock) Inner join dbo.tblPeriodMaint  a  with(nolock) on a.Cocode = v.AccCode where a.rptYear=YEAR(" + start.ToString() + ") and v.VesselCode='" + ddlVesselGL.SelectedValue + "' AND a.rptPeriod = MONTH(" + start.ToString() + ")),0)";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(str);
            if (dt.Rows.Count > 0)
            {
                isMonthClosed = Convert.ToBoolean(dt.Rows[0][0]);
            }
            if (isMonthClosed)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('In Published Report Month is already closed, System should not allow to add GL Entry for Selected Transaction date.');", true);
                // ddlMonthGL.Focus();
                txtTransactionDate.Focus();
                return;
            }

            
            if (start > today)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Transaction Date should be less or equal today's date.');", true);
                // txtTransactionDate.Text = "";
                txtTransactionDate.Focus();
            }
            int diffDays = 0;
            string str1 = "Select DATEDIFF(DAY, TransDate, '"+ start + "') from tblGLEntry with(nolock) where GLid =  "+ TempGLId + " AND VesselCode ='" + ddlVesselGL.SelectedValue + "'";
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(str1);
            if (dt1.Rows.Count > 0)
            {
                diffDays = Convert.ToInt32(dt1.Rows[0][0]);
            }
            if (diffDays < 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Transaction date should be between Previous Transaction date and Current Transaction date.');", true);
                txtTransactionDate.Focus();
                return;
            }


           

            Common.Set_Procedures("SP_InsertReverseGLEntry");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@GLid", TempGLId),
                new MyParameter("@VesselCode", ddlVesselGL.SelectedValue),
                new MyParameter("@TransDate", txtTransactionDate.Text.Trim()),
                new MyParameter("@Remark", txtRemark.Text.Trim()),
                new MyParameter("@AddedBy", Session["Loginid"].ToString())
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('GL Entry reversed successfully.');", true);
                dv_GLDetails.Visible = false;
                GetGLEntry(ddlVessel.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlPosted.SelectedValue));
                // btnPosted.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record. " + Common.getLastError() + "');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record. " + Common.getLastError() + "');", true);
        }
    }

    protected void ibClose_Click(object sender, ImageClickEventArgs e)
    {
        divAttachment.Visible = false;
    }

    protected void lbAddUpdateDoc_Click(object sender, EventArgs e)
    {
        divAttachment.Visible = true;
        ShowAttachment();
    }

    protected void btnPopupAttachment_Click(object sender, EventArgs e)
    {
        divAttachment.Visible = false;
    }

    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            string sql = "";
            if (TempGLId > 0)
            {
                sql = "Update tblGLEntry_Documents SET  Status = 0, ModifiedBy = " + Convert.ToInt32(Session["loginid"].ToString()) + ", ModifiedOn = GETDATE()   WHERE [VesselCode] = '" + ddlVesselGL.SelectedValue + "' AND  GLId =" + TempGLId + " AND DocId = " + DocId + " AND  Status = 1";
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
                GetDocCount(ddlVesselGL.SelectedValue, TempGLId);
            }
            
            ShowAttachment();
            
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }

    }

    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVesselGL.SelectedIndex == 0)
            {
                ProjectCommon.ShowMessage("Please select vessel.");
                ddlVesselGL.Focus();
                return;
            }
            if (rptDocuments.Items.Count >= 4)
            {
                ProjectCommon.ShowMessage("Maximum 4 documents allowed, 200KB each.");
                return;
            }
            //string FileName = "";
            //    byte[] ImageAttachment ;
            if (FU.HasFile)
            {
                if (FU.PostedFile.ContentLength > (1024 * 1024 * 0.2))
                {
                    ProjectCommon.ShowMessage("File Size is Too big! Maximum Allowed is 200KB...");
                    FU.Focus();
                    return;
                }
                //else
                //{
                //    FileName = FU.FileName;
                //    // ImageAttachment = FU.FileBytes;
                //}

                int GLId = TempGLId > 0 ? TempGLId : 0;
                string FileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileContent = FU.PostedFile.ContentType;
                Stream fs = FU.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                Common.Set_Procedures("[dbo].[InsertUpdate_tblGLEntryDocuments]");
                Common.Set_ParameterLength(6);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", ddlVesselGL.SelectedValue),
                    new MyParameter("@GLId", GLId),
                    new MyParameter("@LoginId", Convert.ToInt32(Session["loginid"].ToString())),
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
                    GetDocCount(ddlVesselGL.SelectedValue, GLId);
                }
                else
                {
                    ProjectCommon.ShowMessage("Unable to add Document.Error :" + Common.getLastError());
                }
            }
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }
    }

    protected void ShowAttachment()
    {
        string sql = "";
        if (TempGLId > 0)
        {
            sql = "SELECT DocId, DocName As FileName, GLId As GLId, VesselCode FROM [tblGLEntry_Documents] GL with(nolock) WHERE GL.[VesselCode] = '" + ddlVesselGL.SelectedValue + "' AND  GL.GLId =" + TempGLId + " AND Status = 1 ";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            rptDocuments.DataSource = DT;
            rptDocuments.DataBind();
        }
    }

    protected void GetDocCount(string Vesselcode, int GLId)
    {
        string sql = "";
        if (GLId > 0)
        {
            sql = "SELECT Count(DocId) As DocumentCount FROM [tblGLEntry_Documents] with(nolock) WHERE [VesselCode] = '" + ddlVesselGL.SelectedValue + "' AND  GLId =" + GLId + " AND Status = 1 ";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblAttchmentCount.Text = DT.Rows[0]["DocumentCount"].ToString();
        }
       
    }

}