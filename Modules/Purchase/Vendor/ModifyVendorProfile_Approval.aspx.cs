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
using Ionic.Zip;
using System.Drawing;
using System.Web.UI.HtmlControls;


/// <summary>
/// Page Name            : ModifyVendorProfile.aspx
/// Purpose              : Editing vendor Request
/// Author               : Laxmi Verma
/// Developed on         : 24 September 2015
/// </summary>


public partial class Docket_ModifyVendorProfile : System.Web.UI.Page
{
    Random r = new Random();
    public int RequestId
    {
        get { return Common.CastAsInt32(ViewState["RequestId"]); }
        set { ViewState["RequestId"] = value; }
    }

    public int SecondedTo
    {
        get { return Convert.ToInt32("0" + ViewState["SecondedTo"]); }
        set { ViewState["SecondedTo"] = value.ToString(); }
    }
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfSID.Value); }
        set { hfSID.Value = value.ToString(); }
    }
    public int RequestAprovalStatus
    {
        get { return Common.CastAsInt32(ViewState["RequestAprovalStatus"]); }
        set { ViewState["RequestAprovalStatus"] = value; }
    }
    public int ApprovalStageDone
    {
        get { return Common.CastAsInt32(ViewState["ApprovalStageDone"]); }
        set { ViewState["ApprovalStageDone"] = value; }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------

        ClearMessage();
        if (!IsPostBack)
        {
            int VRID_Edit = Common.CastAsInt32(Request.QueryString["VRID"].ToString());



            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VRID FROM dbo.tbl_VenderRequest WHERE VRID='" + VRID_Edit + "'");
            if (dt != null)
            {
                chkFleets.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
                chkFleets.DataTextField="FleetName";
                chkFleets.DataValueField="FleetId";
                chkFleets.DataBind();

                chkVessels.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME");
                chkVessels.DataTextField = "VesselName";
                chkVessels.DataValueField = "VesselId";
                chkVessels.DataBind();
 
                //for seconded to users
                DataTable dtSecondedTo = Common.Execute_Procedures_Select_ByQuery("SELECT LOGINID,FIRSTNAME + ' ' + LASTNAME AS EMPNAME from UserLogin u with(nolock)  Where u.StatusId = 'A' ORDER BY EMPNAME");
                ddl_SecondedTo.DataSource = dtSecondedTo;
                ddl_SecondedTo.DataTextField = "empname";
                ddl_SecondedTo.DataValueField = "LOGINID";
                ddl_SecondedTo.DataBind();
                ddl_SecondedTo.Items.Insert(0, new ListItem(" < Select IInd Proposer > ", "0"));

                DataTable dtAppTo = Common.Execute_Procedures_Select_ByQuery("SELECT LOGINID,FIRSTNAME + ' ' + LASTNAME AS EMPNAME from UserLogin u with(nolock) Inner Join POS_invoice_mgmt pim with(nolock) On u.LoginId = pim.UserId Where Approval3 = 1 and u.StatusId = 'A' ORDER BY EMPNAME");
                ddlFwdAppTo.DataSource = dtAppTo;
                ddlFwdAppTo.DataTextField = "EMPNAME";
                ddlFwdAppTo.DataValueField = "LOGINID";
                ddlFwdAppTo.DataBind();
                ddlFwdAppTo.Items.Insert(0, new ListItem(" < Select > ", "0"));
                
                RequestId = Common.CastAsInt32(dt.Rows[0]["VRID"]);
                ShowRecord();
            }
            else
            {

            }

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool AnyChecked = false;
        bool ContactedVendors = false;

        foreach (System.Web.UI.WebControls.ListItem li in chk_justificationVendors.Items)
        {
            if (li.Selected)
            {
                AnyChecked = true;
                if (li.Value == "2")
                    ContactedVendors = true;
            }
        }
        if (ContactedVendors)
        {
            if (rpt_VendorsNameForJustification.Items.Count <= 0)
            {
                ShowMessage("Please select which vendor you contacted for supply.", true);
                return;
            }
        }

        //=====================================
        if (AnyChecked)
        {
            Save_Data("1P");

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid=" + ddl_SecondedTo.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                string message = "Please accept the IInded user in the POS System";
                //SendMail("IInded User Approval", message, dt.Rows[0]["email"].ToString());
                ShowMessage("Record saved successfully.", false);
            }
        }
        else
        {
            ShowMessage("Please select Justification for new vendor.", true);
        }
    }
    //for 2nd approval
    protected void btnSave_IIndProposer_Click(object sender, EventArgs e)
    {
        if (chk_SecondedTo_save.Checked == true)
        {
            Save_Data("2P");
            btnSendForApproval.Visible = true;
        }
        else
        {
            ShowMessage("Please check the checkbox (I Agree) to continue.", true);
        }
    }
    //for 
    protected void btnSave_IstApproval_Click(object sender, EventArgs e)
    {
        Save_Data("1A");
    }
    //for second approval
    protected void btnSave_IIndApproval_Click(object sender, EventArgs e)
    {
        Save_Data("2A");
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        dv_SendForApproval.Visible = true;
    }
    protected void btnPOPSendForApp_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("update [dbo].[tbl_VenderRequest] set RequestApprovalStatus=3,FirstAppFwdTo=" + ddlFwdAppTo.SelectedValue + " WHERE VRId=" + RequestId);
        DataTable dt=Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid=" + ddlFwdAppTo.SelectedValue );
        if(dt.Rows.Count>0)
        {
            string message = "Please approve the requested vendor in the POS System";
            //SendMail("Vendor Approval Request",message,dt.Rows[0]["email"].ToString());
        }
        ShowRecord();
        modalBox.Visible = false;
        dv_SendForApproval.Visible = false;
    }
    protected void btnClosePOPSendForApp_Click(object sender, EventArgs e)
    {
        modalBox.Visible = false;
        dv_SendForApproval.Visible = false;
    }
    protected void Save_Data(string status)
    {
       if (status == "1P")
        {
            //for vendor proposal saving
            Common.Set_Procedures("sp_ProposalSubmission");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
            new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
            new MyParameter("@Justification_New_Vendor", GetCheckBoxListSelections(chk_justificationVendors)),
            new MyParameter("@SupplierIds_UnavailableToDeliver", GetRepeatorListSupplier(rpt_VendorsNameForJustification)),
            new MyParameter("@ProposedById", Common.CastAsInt32(Session["loginid"])),
            new MyParameter("@ProposedByName", Session["FullName"].ToString()),
            new MyParameter("@ProposedByPosition", Session["PositionName"].ToString()),
            new MyParameter("@ProposedOn", System.DateTime.Now),
            new MyParameter("@SecondedUserId", Common.CastAsInt32(ddl_SecondedTo.SelectedValue)),
            new MyParameter("@PropRemarks",txtPropRemarks.Text.Trim()));
            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                string ToEmail=ProjectCommon.getUserEmailByID(ddl_SecondedTo.SelectedValue);
                //SendMail("New Vendor Proposal", "Hi,<br/>Please accept the new proposed vendor...<br/><br/>Vendor Name :" + lblCompanyname.Text + "<br/><br/>Remarks :" + txtPropRemarks.Text.Trim() + "<br/><br/><br/>" + Session["FullName"].ToString(), ToEmail);
                ShowMessage("Record Saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }
        if (status == "2P")
        {
            if (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(ddl_SecondedTo.SelectedValue))
            {
                //for vendor proposal saving
                Common.Set_Procedures("sp_SecenodedToSubmission");
                Common.Set_ParameterLength(5);
                Common.Set_Parameters(
                new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
                new MyParameter("@SecondedBy", Session["FullName"].ToString()),
                new MyParameter("@SecondedPosition", Session["PositionName"].ToString()),
                new MyParameter("@SecondedOn", System.DateTime.Now.Date),
                new MyParameter("@SecRemarks", txtSecRemarks.Text.Trim())
                );
                DataSet dsprofile = new DataSet();
                if (Common.Execute_Procedures_IUD(dsprofile))
                {
                    ShowMessage("Record Saved successfully.", false);
                }
                else
                {
                    ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
                }
            }
        }
        if (status == "1A")
        {
            string AppServices = GetCheckBoxListSelections(chkAppServices);
            Common.Set_Procedures("sp_FirstApproval");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
               new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
               new MyParameter("@ApprovalStatus", rd_ApprovalAcrion.SelectedValue),
               new MyParameter("@FirstApprovedBy", Session["FullName"].ToString()),
               new MyParameter("@FirstApprovedPosition", Session["PositionName"].ToString()),
               new MyParameter("@Validity1", txt_ValidityDate.Text),
               new MyParameter("@FirstApprovalType", Common.CastAsInt32(ddlApprovalType.SelectedValue)),
               new MyParameter("@FirstApprovedRemarks", txt_ApprovedRemakrs.Text.Trim().Replace("'", "`")),
               new MyParameter("@ApprovedBusinesses", AppServices));
            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                string ToEmail = ProjectCommon.getUserEmailByID(ddl_SecondedTo.SelectedValue);
                //SendMail("New Vendor Proposal", "Dear Recipient,<br/>Please accept the new proposed vendor.", ToEmail);
                ShowMessage("Record Saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }
        if (status == "2A")
        {
            Common.Set_Procedures("sp_SecondApproval");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
               new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
               new MyParameter("@ApprovalStatus", rd_ApprovalAcrion_2.SelectedValue),
               new MyParameter("@SecondApprovedBy", Session["FullName"].ToString()),
               new MyParameter("@SecondApprovedPosition", Session["PositionName"].ToString()),
               new MyParameter("@Validity2nd", txt_ValidityDate_2.Text),
               new MyParameter("@SecondApporvalType", Common.CastAsInt32(ddlApprovalType_2.SelectedValue)),
               new MyParameter("@SecondAppprovedRemarks", txt_ApprovedRemakrs_2.Text.Trim().Replace("'", "`"))
               );
            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                int SupplierId = Common.CastAsInt32(dsprofile.Tables[0].Rows[0]);
                //if(SupplierId<=0)
                //    SendMailToAccount();

                ShowMessage("Record Saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }

        ShowRecord();
       
    }
    private string GetCheckBoxListSelections(CheckBoxList chklist)
    {
        string[] cblItems;
        ArrayList cblSelections = new ArrayList();
        foreach (ListItem item in chklist.Items)
        {
            if (item.Selected)
            {
                cblSelections.Add(item.Value);
            }
        }

        cblItems = (string[])cblSelections.ToArray(typeof(string));
        return string.Join(",", cblItems);
    }
    //protected void SendMailToAccount()
    //{
    //    string[] ToAdd = { "emanager@energiossolutions.com" };
    //    string[] NoAdd = { "" };
    //    string[] BccAdd = { "emanager@energiossolutions.com" };
    //    string Message = "Dear Account Department,<br><br>Please process this vendor in traverse.<br>Vendor Name :" + lblCompanyname.Text + "<br>E-mail Address : " + lblEmailAddress + "<br><br><br>Thanks<br>";
    //    ProjectCommon.SendMailAsync(ToAdd, NoAdd, "Vendor Approved", Message, NoAdd);
    //}

    public void ClearMessage()
    {
        lblMessage.InnerHtml = "";
        lblMessage.Visible = false;
    }

   private string GetRepeatorListSupplier(Repeater rpt_Vlist)
    {
        string[] rptlItems;
        ArrayList cblSelections = new ArrayList();
        foreach (RepeaterItem item in rpt_Vlist.Items)
        {
            HiddenField hdsupid = (HiddenField)item.FindControl("hdn_SupplierID");
            cblSelections.Add(hdsupid.Value);
        }

        rptlItems = (string[])cblSelections.ToArray(typeof(string));
        return string.Join(",", rptlItems);
    }

    //end
    protected void ShowRecord()
   {
       DataTable dt = Common.Execute_Procedures_Select_ByQuery(" SELECT *  FROM dbo.tbl_VenderRequest WHERE VRID=" + RequestId + "");
       if (dt.Rows.Count > 0)
       {
           DataRow dr = dt.Rows[0];
           lblCompanyname.Text = dr["COMPANYNAME"].ToString();
           lblEmailAddress.Text = dr["EmailAddress"].ToString();
           //------------------------------------------
               //for justification
               Bind_Vendors();

               if (dr["COMPANYBUSINESSES"].ToString().Trim() != "")
               {
                   DataTable dtVendorList1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList where vendorlistid in(" + dr["COMPANYBUSINESSES"].ToString() + ") ORDER BY Vendorlistname");
                   chkAppServices.DataSource = dtVendorList1;
                   chkAppServices.DataTextField = "Vendorlistname";
                   chkAppServices.DataValueField = "Vendorlistid";
                   chkAppServices.DataBind();
               }

               SecondedTo = Common.CastAsInt32(dr["SecondedUserId"].ToString());
               chk_justificationVendors.ClearSelection();

               string strJustification = "," + dr["Justification_New_Vendor"].ToString() + ",";
               foreach (ListItem li in chk_justificationVendors.Items)
               {
                   if (strJustification.Contains("," + li.Value + ","))
                   {
                       li.Selected = true;
                   }
               }

               //for proposed by       
               if (!Convert.IsDBNull(dr["ProposedOn"]))
               {
                   txt_ProposedBy.Text = dr["ProposedByName"].ToString();
                   txt_ProposedOn.Text = Common.ToDateString(dr["ProposedOn"]);
                   txt_proposedPosition.Text = dr["ProposedByPosition"].ToString();
                   ddl_SecondedTo.SelectedValue = dr["SecondedUserId"].ToString();
                   txtPropRemarks.Text = dr["PropRemarks"].ToString();
                   ApprovalStageDone = 1;
               }

               // fro IInded Person
               if (!Convert.IsDBNull(dr["SecondedOn"]))
               {
                   lblSecondedBy.Text = dr["SecondedBy"].ToString();
                   lblSecondedByPos.Text = dr["SecondedPosition"].ToString();
                   lblSecondedOn.Text = Common.ToDateString(dr["SecondedOn"]);
                   txtSecRemarks.Text = dr["SecRemarks"].ToString();
                   ApprovalStageDone = 2;
               }

               //for first approval    
               if (!Convert.IsDBNull(dr["FirstApprovalResult"]))
               {
                   rd_ApprovalAcrion.SelectedValue = dr["FirstApprovalResult"].ToString();
                   txt_ApprovedBy.Text = dr["FirstApprovedBy"].ToString() + " / " + Common.ToDateString(dr["FirstApprovedOn"]);
                   txt_ApprovedPosition.Text = dr["FirstApprovedPosition"].ToString();
                   txt_ValidityDate.Text = Common.ToDateString(dr["ValidityDate"]);
                   ddlApprovalType.SelectedValue = dr["FirstApprovalType"].ToString();
                   txt_ApprovedRemakrs.Text = dr["FirstApprovedRemarks"].ToString();
                   ddlApprovalType_OnSelectedIndexChanged(new object(), new EventArgs());
                   ApprovalStageDone = 3;

                   string ApprovedBusinesses = dr["ApprovedBusinesses"].ToString();
                   chkAppServices.ClearSelection();
                   string strDescription = "," + ApprovedBusinesses + ",";
                   foreach (ListItem li in chkAppServices.Items)
                   {
                       if (strDescription.Contains("," + li.Value + ","))
                       {
                           li.Selected = true;
                       }
                   }

               }

               //for 2nd approval
               if (!Convert.IsDBNull(dr["SecondApprovalResult"]))
               {
                   txt_ApprovedBy_2.Text = dr["SecondApprovedBy"].ToString() + " / " + Common.ToDateString(dr["SecondAppprovedOn"]);

                   txt_ValidityDate_2.Text = Common.ToDateString(dr["ValidityDate"]);
                   txt_ApprovedPosition_2.Text = dr["SecondApprovedPosition"].ToString();
                   rd_ApprovalAcrion_2.SelectedValue = dr["SecondApprovalResult"].ToString();
                   ddlApprovalType_2.SelectedValue = dr["SecondApporvalType"].ToString();
                   txt_ApprovedRemakrs_2.Text = dr["SecondAppprovedRemarks"].ToString();
                   ddlApprovalType_2_OnSelectedIndexChanged(new object(), new EventArgs());
                   ApprovalStageDone = 4;
               }

               //======================================
               int RequestApprovalStatus = Common.CastAsInt32(dr["RequestApprovalStatus"]);
               btnSubmit.Visible = false;

               btnSave_IIndProposer.Visible = false;
               btnSendForApproval.Visible = false;
               btnSave_IstApproval.Visible = false;
               btnSave_IIndApproval.Visible = false;

               btn_selctvendors.Visible = false;

               switch (RequestApprovalStatus)
               {
                   case 2: // Record Submitted 
                       if (Convert.IsDBNull(dr["ProposedOn"]))
                       {
                           btn_selctvendors.Visible = true;
                           btnSubmit.Visible = true;
                       }
                       else if (Convert.IsDBNull(dr["SecondedOn"]))
                       {
                           btn_selctvendors.Visible = true;
                        //btnSubmit.Visible = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["ProposedById"]));
                        //btnSave_IIndProposer.Visible = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["SecondedUserId"]));
                        btnSubmit.Visible = true;
                        btnSave_IIndProposer.Visible = true;
                    }
                       else
                       {
                           chk_SecondedTo_save.Checked = true;
                        //btnSendForApproval.Visible = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["ProposedById"])) || (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["SecondedUserId"]));
                        btnSendForApproval.Visible = true;
                       }
                       break;
                   case 3: // Waiting for Ist Approval
                       btnSave_IstApproval.Visible = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["FirstAppFwdTo"]));
                       break;
                   case 4: // Waiting for IInd Approval
                       btnSave_IIndApproval.Visible = (Session["PositionName"].ToString().Trim() == "Managing Director");
                       break;
                   case 5: // Approved
                       break;
                   case 6: // rejected
                       break;
                   default:
                       break;

               }

               btnSaveNomination.Visible = btnSave_IstApproval.Visible || btnSave_IIndApproval.Visible;

               if (!chk_SecondedTo_save.Checked)
                   chk_SecondedTo_save.Checked = (RequestApprovalStatus > 2);
               //======================================
               if (RequestApprovalStatus >= 2)
               {
                   foreach (RepeaterItem rptv in rpt_VendorsNameForJustification.Items)
                   {
                       ImageButton imgbutton = rptv.FindControl("imgBtnDelete") as ImageButton;
                       imgbutton.Visible = false;
                   }

               }
               rd_ApprovalAcrion_SelectedIndexChanged(this, new EventArgs());
               rd_ApprovalAcrion_2_SelectedIndexChanged(this, new EventArgs());

               if (btnSubmit.Visible)
               {
                   hid_TabNo.Value = "1";
                   pnlA1.Visible = true;
               }
               if (btnSave_IIndProposer.Visible)
               {
                   hid_TabNo.Value = "2";
                   pnlA2.Visible = true;
               }
               if (btnSave_IstApproval.Visible)
               {
                   hid_TabNo.Value = "3";
                   pnlA3.Visible = true;
               }
               if (btnSave_IIndApproval.Visible)
               {
                   hid_TabNo.Value = "4";
                   pnlA4.Visible = true;
               }

           }


       //btnSubmit.Visible = true;
       //btnSave_IIndProposer.Visible = true;
       //btnSave_IstApproval.Visible = true;
       //btnSave_IIndApproval.Visible = true;
   }

    public string GetRating(object rat)
    {
        switch (Common.CastAsInt32(rat))
        {
            case -2:
                return "Poor";
            case -1:
                return "Below Avg.";
            case 0:
                return "Average";
            case 1:
                return "Good";
            case 2:
                return "Outstanding";
            default:
                return "";
        }
    }
    
    protected void Bind_Vendors()
    {
        //Vendors
        DataTable dtVendors = Common.Execute_Procedures_Select_ByQuery(" select sj.VRID,s.SupplierID, s.SupplierName ,s.SupplierTel,s.SupplierEmail ,s.SupplierFax " +
                                                                        " ,s.SupplierContact from dbo.tbl_SelectedVendors_Forjustification sj " +
                                                                        " left join DBO.tblSMDSuppliers s on s.supplierid=sj.supplierid where sj.vrid=" + RequestId.ToString());
        DataView dv = dtVendors.DefaultView;

        rpt_VendorsNameForJustification.DataSource = dv.ToTable();
        rpt_VendorsNameForJustification.DataBind();
    }

    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Visible = true;
        if (Error && Common.ErrMsg != "")
            lblMessage.InnerHtml = Msg + " Error :" + Common.ErrMsg;
        else
            lblMessage.InnerHtml = Msg;
        lblMessage.Attributes.Add("class", (Error) ? "msgbox error" : "msgbox success");
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dtFiles = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.tbl_VendorRequestDocuments WHERE TABLEID=" + TableId);
        if (dtFiles.Rows.Count > 0)
        {
            string FileName = dtFiles.Rows[0]["FileName"].ToString();
            string ExtFileName = Path.GetExtension(FileName).Substring(1);
            Response.ContentType = "application/" + ExtFileName;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            byte[] buffer = (byte[])dtFiles.Rows[0]["FileContents"];
            Response.BinaryWrite(buffer);
            Response.Flush();
            //Response.Close();
            //Response.End();
        }
    }
   
    //for justification vendors
    protected void btn_selectvendors_Click(object sender, EventArgs e)
    {
        BindVendor();
        modalBox.Visible = true;
        modalframe_SelectVendors.Visible = true;
    }
    protected void btn_savemodel3_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem i in rpt_VendorList.Items)
        {
            //Retrieve the state of the CheckBox
            CheckBox cb = (CheckBox)i.FindControl("chk_VendorList");
            if (cb.Checked)
            {
                //Retrieve the value associated with that CheckBox
                HiddenField hdnsupplierid = (HiddenField)i.FindControl("hdn_SupplierID");
                Label lblsuppliername = (Label)i.FindControl("lblSupplierName");

                //----------------------------------------------      
                Common.Set_Procedures("sp_InsertUpdateVendorForJustification");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
                    new MyParameter("@SupplierID", Common.CastAsInt32(hdnsupplierid.Value)),
                    new MyParameter("@SupplierName", lblsuppliername.Text.Trim().Replace("'", "`"))
                    );
                DataSet ds = new DataSet();
                Common.Execute_Procedures_IUD(ds);
                
            }
        }
        Bind_Vendors();
        modalBox.Visible = false;
        modalframe_SelectVendors.Visible = false;
    }
    protected void btnCloseModal3_Click(object sender, EventArgs e)
    {
        modalBox.Visible = false;
        modalframe_SelectVendors.Visible = false;
    }
    public void BindVendor()
    {
        DataTable DTVendor = GetSuplierData("", "", "");
        if (DTVendor != null)
        {
            rpt_VendorList.DataSource = DTVendor;
            rpt_VendorList.DataBind();
        }
    }
    protected void rpt_VendorsNameForJustification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("delete FROM tbl_SelectedVendors_Forjustification where SupplierId=" + Common.CastAsInt32(e.CommandArgument));
            Bind_Vendors();
        }
    }
    public DataTable GetSuplierData(string SortBy, string SortType, string WhereClause)
    {
        string sql = "SELECT Row_Number() over(order by <SORTKEY>) as srno,tblSMDSuppliers.SupplierID, tblSMDSuppliers.SupplierName, tblSMDSuppliers.SupplierPort, replace(tblSMDSuppliers.SupplierTel,';','</br>')SupplierTel " +
            " , tblSMDSuppliers.SupplierEmail, tblSMDSuppliers.SupplierContact,tblSMDSuppliers.TravID FROM DBO.tblSMDSuppliers ";


        if (WhereClause != "")
            sql = sql + WhereClause;
        else
            sql = sql + "WHERE tblSMDSuppliers.Active=1 ";

        if (SortBy != "")
        {
            sql = sql + " ORDER BY " + SortBy + " " + SortType + "";
            sql = sql.Replace("<SORTKEY>", SortBy + " " + SortType);
        }
        else
        {
            sql = sql + " ORDER BY tblSMDSuppliers.SupplierName";
            sql = sql.Replace("<SORTKEY>", "tblSMDSuppliers.SupplierName");
        }


        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql);
        return DTVendor;
    }

    protected void rd_ApprovalAcrion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_ApprovalAcrion.SelectedValue == "A")
        {
            tr_apprvalType.Visible = true;
            tr_Validity1.Visible = true;
        }
        else if (rd_ApprovalAcrion.SelectedValue == "R")
        {
            tr_apprvalType.Visible = false;
            tr_Validity1.Visible = false;
        }
    }
    protected void rd_ApprovalAcrion_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_ApprovalAcrion_2.SelectedValue == "A")
        {
            tr_apprvalType_2.Visible = true;
            tr_validity_2.Visible = true;
        }
        else if (rd_ApprovalAcrion.SelectedValue == "R")
        {
            tr_apprvalType_2.Visible = false;
            tr_validity_2.Visible = false;
        }

    }
    protected void ddlApprovalType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkApp1.Visible = ddlApprovalType.SelectedValue == "6";
    }
    protected void ddlApprovalType_2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkApp2.Visible = ddlApprovalType_2.SelectedValue == "6";
        
    }
    //public void SendMail(string Title,string message, string emailid)
    //{
    //    string[] ToAdd = { emailid };
    //    string[] NoAdd = { "" };
    //    string[] BccAdd = { "emanager@energiossolutions.com" };
    //    string Message = "Dear User,<br><br>" + message + ".<br><br><br>Thanks<br>";
    //    ProjectCommon.SendMailAsync(ToAdd, NoAdd, "MTM Ship Management : " + Title, Message, NoAdd);
    //}
   
    
    protected void lnkApp1_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "1";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=1");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }

        btnSaveNomination.Visible = btnSave_IstApproval.Visible || btnSave_IIndApproval.Visible;
    }
    protected void lnkApp2_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "2";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=2");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }
    }

    protected void btnSaveNomination_Click(object sender, EventArgs e)
    {
        if (hfdNominationStage.Value == "1")
        {
            string Fleets = "";
            foreach (ListItem li in chkFleets.Items)
                if (li.Selected)
                    Fleets += "," + li.Value;

            string Vessels = "";
            foreach (ListItem li in chkVessels.Items)
                if (li.Selected)
                    Vessels += "," + li.Value;

            if (Fleets.StartsWith(","))
                Fleets = Fleets.Substring(1);

            if (Vessels.StartsWith(","))
                Vessels = Vessels.Substring(1);

            Common.Execute_Procedures_Select_ByQuery("Exec dbo.Update_Vemndor_Nominations " + RequestId + ",1,'" + Fleets + "','" + Vessels + "'");
        }
        if (hfdNominationStage.Value == "2")
        {
            string Fleets = "";
            foreach (ListItem li in chkFleets.Items)
                if (li.Selected)
                    Fleets += "," + li.Value;

            string Vessels = "";
            foreach (ListItem li in chkVessels.Items)
                if (li.Selected)
                    Vessels += "," + li.Value;

            if (Fleets.StartsWith(","))
                Fleets = Fleets.Substring(1);

            if (Vessels.StartsWith(","))
                Vessels = Vessels.Substring(1);


            Common.Execute_Procedures_Select_ByQuery("Exec dbo.Update_Vemndor_Nominations " + RequestId + ",2,'" + Fleets + "','" + Vessels + "'");
        }
        modalBox.Visible = false;
        dvNomination.Visible = false;

        ShowMessage("Record saved successfully.",false);
    }
    protected void btnCloseNomination_Click(object sender, EventArgs e)
    {
        modalBox.Visible = false;
        dvNomination.Visible = false;
    }
   protected void btntest_Click(object sender, EventArgs e)
   {
       modalBox.Visible = true;
       modalframe_SelectVendors.Visible = true;
   }
    
   protected void btnTabNo_Click(object sender, EventArgs e)
   {
       int _PageNo = Common.CastAsInt32(hid_TabNo.Value);
       pnlA1.Visible = false;
       pnlA2.Visible = false;
       pnlA3.Visible = false;
       pnlA4.Visible = false;
       ((Panel)this.FindControl("pnlA" + _PageNo)).Visible = true;
       ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetTab2();SetActiveStageTab(" + ApprovalStageDone + ");", true);
   }
   protected void fd1_OnPreRender(object sender, EventArgs e)
   {
      //ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetActiveStageTab(" + ApprovalStageDone + ");", true);
   }
   protected void Page_PreRender(object sender, EventArgs e)
   {
       RestTopMenu();
   }
   protected void RestTopMenu()
   {
       ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetTab2();SetActiveStageTab(" + ApprovalStageDone + ");", true);
   }
}
