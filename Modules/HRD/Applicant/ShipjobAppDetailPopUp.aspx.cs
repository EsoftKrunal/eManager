using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;
using ShipSoft.CrewManager.DataAccessLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Modules_HRD_Applicant_ShipjobAppDetailPopUp : System.Web.UI.Page
{
    public int SelectedDisc
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelComm"]);
        }
        set
        {
            ViewState["SelComm"] = value;
        }
    }
    public int candidateid
    {
        get { return Common.CastAsInt32(ViewState["candidateid"]); }
        set { ViewState["candidateid"] = value; }
    }
    int LoginId = 0;
    public bool IsApproved;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        // lblDateMess.Visible = false;
        this.lbl_info.Text = "";
        try
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Your session has been expired.\\n Please login again.');window.close();", true);
        }
        try
        {
            if (!IsPostBack)
            {

                CalendarExtender1.StartDate = DateTime.Now;
                //CalendarExtender11.StartDate = DateTime.Now;
                //CalendarExtender11.EndDate = DateTime.Now;
                //DataTable DT = Common.Execute_Procedures_Select_ByQuery(" Select top 1 CompanyName,LogoImageName from Company where DefaultCompany = 'Y'");
                //if (DT != null && DT.Rows.Count > 0)
                //{
                //    //lblCompanyName.Text = DT.Rows[0]["CompanyName"].ToString();
                //    string imgName = DT.Rows[0]["LogoImageName"].ToString();
                //    string logoPath = ConfigurationManager.AppSettings["Logopath"].ToString();
                //    string imgFullpath = logoPath + imgName;
                //    imgbtn.ImageUrl = imgFullpath;
                //}
                BindddlNationality();
                BindddlRank();
                BindVesselType();
                BindShoeSizeDropDown();
                BindShirtSizeDropDown();
                BindMaritalStatusDropDown();
                candidateid = Common.CastAsInt32(Request.QueryString["candidate"]);
                ShowRecord(sender, e);

                tblSeaExp.Visible = true;
                tblAttachment.Visible = true;
                ShowAttachment();
               
                int status = Common.CastAsInt32("0" + ViewState["STATUS"]);
                DataTable dtTrans = Common.Execute_Procedures_Select_ByQueryCMS("SELECT candidateid FROM DBO.candidatepersonaldetails where status=3 and candidateid=" + candidateid.ToString());
                bool candidatetransferred = dtTrans.Rows.Count > 0;

                bool ReqAppMode = (status == 1 || status == 4 || status == 5);
                if (ReqAppMode)
                {
                    btnReqForApprove.Enabled = true;
                    divMain.Visible = true;
                }
                else
                {
                    btnReqForApprove.Enabled = false;
                    divMain.Visible = false;
                }

                btnArchive.Visible = (status == 1 || status == 4);

                if (status == 2 && Request.QueryString["M"].Trim().ToLower() == "app")
                {
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    divAppRej.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    divAppRej.Visible = false;
                }
                IsApproved = false;
                btnEdit.Visible = (!btnApprove.Visible) && (!candidatetransferred);
                imgbtnAddSeaExp.Visible = btnEdit.Visible;
                IsApproved = btnEdit.Visible;
                if (status == 3 || status == 4 || status == 5)
                {
                    divMain.Visible = false;
                    btnNotify.Visible = true; 
                    divnotify.Visible = true;
                }
                else
                {
                    btnNotify.Visible = false;
                    divnotify.Visible = false;
                }
                BindSeaExpRepeter();
                BindSeaExpDtlsRepeter(candidateid);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Error : " + ex.Message.ToString() + "');window.close();", true);
        }
    }

    protected void BindddlNationality()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT convert(varchar,CountryId) as CountryId,NationalityName as CountryName from Country where statusid='A'  \r\n order by CountryName");
        this.ddlNat.DataTextField = "CountryName";
        this.ddlNat.DataValueField = "CountryId";
        this.ddlNat.DataSource = dt;
        this.ddlNat.DataBind();
        ddlNat.Items.Insert(0, new ListItem("< Select >", "0"));


        DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery(" SELECT CountryId,CountryName,CountryCode from Country where statusid='A' order by CountryName");
        this.ddl_P_Country.DataTextField = "CountryName";
        this.ddl_P_Country.DataValueField = "CountryId";
        this.ddl_P_Country.DataSource = dtCountry;
        this.ddl_P_Country.DataBind();
        ddl_P_Country.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void BindddlRank()
    {
        this.ddlRank.DataTextField = "RankCode";
        this.ddlRank.DataValueField = "RankId";
        this.ddlRank.DataSource = Common.Execute_Procedures_Select_ByQuery("Select RankId,LTRIM(RTRIM(RankCode)) As RankCode from DBO.Rank where statusid='A' and RankId Not In (48,49) order By RankLevel");
        this.ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void BindVesselType()
    {
        string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
        this.chkVeselType.DataTextField = "VesselTypeName";
        this.chkVeselType.DataValueField = "VesselTypeId";
        this.chkVeselType.DataSource = Common.Execute_Procedures_Select_ByQuery("Select VesselTypeId,VesselTypeName from DBO.VesselType Where VesselTypeid in (" + vesseltypes + ") order By VesselTypeName");
        this.chkVeselType.DataBind();
    }

    protected void BindNearestAirport(int countryid)
    {
        this.dd_nearest_airport.DataTextField = "NearestAirportName";
        this.dd_nearest_airport.DataValueField = "NearestAirportId";
        this.dd_nearest_airport.DataSource = Common.Execute_Procedures_Select_ByQuery(" SELECT NearestAirportId,NearestAirportName from NearestAirport where CountryId=" + countryid + " and statusid='A'");
        this.dd_nearest_airport.DataBind();

    }

    protected void BindCountryCode(int countryid)
    {
        this.ddl_P_CountryCode_Mobile.DataTextField = "CountryCode";
        this.ddl_P_CountryCode_Mobile.DataValueField = "CountryId";
        this.ddl_P_CountryCode_Mobile.DataSource = Common.Execute_Procedures_Select_ByQuery(" SELECT CountryId,CountryCode from Country where CountryId=" + countryid + " and statusid='A'");
        this.ddl_P_CountryCode_Mobile.DataBind();

    }

    private void BindShoeSizeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(ShoeSize).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddl_Shoes.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    private void BindShirtSizeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(ShirtSize).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddl_Shirt.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    private void BindMaritalStatusDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(MaritalStatus).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddmaritalstatus.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    protected void ddl_P_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNearestAirport(Convert.ToInt32(ddl_P_Country.SelectedValue));
        ddl_P_CountryCode_Mobile.Items.Clear();
        BindCountryCode(Convert.ToInt32(ddl_P_Country.SelectedValue));
        if (ddl_P_Country.SelectedItem.Text.ToUpper().Trim() == "INDIA")
        {
            trIndos.Visible = true;
            rfvIndocNo.Visible = true;
            rfvIndocIssueDt.Visible = true;
            rfvIndocIssuePlace.Visible = true;
            revIndocExpiry.Visible = true;
        }
        else
        {
            trIndos.Visible = false;
            rfvIndocNo.Visible = false;
            rfvIndocIssueDt.Visible = false;
            rfvIndocIssuePlace.Visible = false;
            revIndocExpiry.Visible = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_cdpopup');", true);
    }

    protected void btnRApprove_Click(object sender, EventArgs e)
    {
        btnSaveComments.CommandArgument = "Req";
        dvComments.Visible = true;
        txtComments.Text = "";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        btnSaveComments.CommandArgument = "App";
        dvComments.Visible = true;
        txtComments.Text = "";
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        btnSaveComments.CommandArgument = "Rej";
        dvComments.Visible = true;
        txtComments.Text = "";
    }

    protected void btnArchive_Click(object sender, EventArgs e)
    {
        try
        {
            string str = candidateid.ToString();
            Budget.getTable("UPDATE DBO.CandidatePersonalDetails " +
                                                     "SET " +
                                                     "STATUS=5,ARCHIVEBY=" + LoginId.ToString() + ",ARCHIVEON=GETDATE() " +
                                                     "WHERE CANDIDATEID=" + candidateid.ToString());
            lbl_info.Text = "Applicant archived successfully.";
            divMain.Visible = false;
            div1.Visible = false;
            divAppRej.Visible = false;
            div2.Visible = false;
            divnotify.Visible = false;
            div3.Visible = false;
            div4.Visible = true;
            ShowRecord(sender,e);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr2", "alert('Applicant deleted successfully.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
        }
        catch (SystemException ex)
        {
            this.lbl_info.Text = "Unable to Delete : " + ex.Message;
        }
    }

    private void ShowRecord(object sender, EventArgs e)
    {
        string Md = Convert.ToString(Request.QueryString["M"]);
       // tr_Comm.Visible = (Md == "App");
        string strqry = "select Firstname,Middlename,Lastname,ModifiedBy,ModifiedOn, " +
                        "nationalityid as Nationality, " +
                        "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB, " +
                        "rankappliedid as Rank, " +
                        "replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom, " +
                        "ContactNo,ContactNo2,EmailId,VesselTypes,Address,City,PassportNo,FileName,isnull(status,1) as status,CreatedBy,CreatedOn,AppSentBy,AppSentOn,AppBy,AppOn,RejBy,RejOn,ArchiveBy,ArchiveOn,ApprovalId,ReqRemarks " +
                        " , Address2,Address3,CountryId,State,PINCode,MobileCountryId,Height,Weight,Waist,BolierSuitSize,ShoeSizeId,ShirtSizeId " +
                        ",NearestAirportCountryId,PassportIssuePlace,CDCNo,replace(convert(varchar,CDCIssueDate,106) ,' ','-') as CDCIssueDate,replace(convert(varchar,CDCExpiryDate,106) ,' ','-') as CDCExpiryDate,CDCIssuePlace " +
                        ",IndosNo,replace(convert(varchar,IndosIssueDate,106) ,' ','-') as IndosIssueDate,replace(convert(varchar,IndosExpiryDate,106) ,' ','-') as IndosExpiryDate,IndosIssuePlace,PhotoPath,MaritalStatusId ,replace(convert(varchar,IssueDate,106) ,' ','-') as IssueDate,replace(convert(varchar,ExpiryDate,106) ,' ','-') as ExpiryDate, PlaceOfBirth " +
                       ",ModifiedBy, ModifiedOn " +
                        " from DBO.candidatepersonaldetails with(nolock) where candidateid=" + candidateid.ToString();
        DataTable dt = Budget.getTable(strqry).Tables[0];
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtFirstName.Text = dr["Firstname"].ToString();
                txtMiddleName.Text = dr["Middlename"].ToString();
                txtLastName.Text = dr["Lastname"].ToString();
                ddlNat.SelectedValue = dr["Nationality"].ToString();
                txtDOB.Text = dr["DOB"].ToString();
                txtPOB.Text = dr["PlaceOfBirth"].ToString(); 
                ddlRank.SelectedValue = dr["Rank"].ToString();
                txtAvalFrom.Text = dr["AvailableFrom"].ToString();
                txtMobileNoPE.Text = dr["ContactNo"].ToString();
                
                //txtComm.Text = dr["ReqRemarks"].ToString();
                // txtMobile.Text = dr["ContactNo2"].ToString();
                
                if (!string.IsNullOrEmpty(dr["EmailId"].ToString()))
                {
                    txt_P_EMail1.Text = dr["EmailId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Address"].ToString()))
                {
                    txtAddressPE1.Text = dr["Address"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Address2"].ToString()))
                {
                    txtAddressPE2.Text = dr["Address2"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Address3"].ToString()))
                {
                    txtAddressPE3.Text = dr["Address3"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["PINCode"].ToString()))
                {
                    txtPincode.Text = dr["PINCode"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["PINCode"].ToString()))
                {
                    txtPincode.Text = dr["PINCode"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["State"].ToString()))
                {
                    txtStatePE.Text = dr["State"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CountryId"].ToString()))
                {
                    ddl_P_Country.SelectedValue = dr["CountryId"].ToString();
                } 
                ddl_P_Country_SelectedIndexChanged(sender, e);
                if (!string.IsNullOrEmpty(dr["MobileCountryId"].ToString()))
                {
                    ddl_P_CountryCode_Mobile.SelectedValue = dr["MobileCountryId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ShirtSizeId"].ToString()))
                {
                    ddl_Shirt.SelectedValue = dr["ShirtSizeId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ShoeSizeId"].ToString()))
                {
                    ddl_Shoes.SelectedValue = dr["ShoeSizeId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Height"].ToString()))
                {
                    txtHeight.Text = dr["Height"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Waist"].ToString()))
                {
                    txtwaist.Text = dr["Waist"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["BolierSuitSize"].ToString()))
                {
                    txtSuitSize.Text = dr["BolierSuitSize"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Weight"].ToString()))
                {
                    txtWeight.Text = dr["Weight"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["City"].ToString()))
                {
                    txtCityPE.Text = dr["City"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["PassportNo"].ToString()))
                {
                    txtPassportNo.Text = dr["PassportNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["PassportIssuePlace"].ToString()))
                {
                    txtPassportIssuePlace.Text = dr["PassportIssuePlace"].ToString();
                }
                if (! string.IsNullOrEmpty(dr["ExpiryDate"].ToString()))
                {
                    txtPassportExpiry.Text = DateTime.Parse(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrEmpty(dr["IssueDate"].ToString()))
                {
                    txtPassportIssueDt.Text = DateTime.Parse(dr["IssueDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                if (!string.IsNullOrEmpty(dr["IndosNo"].ToString()))
                {
                    txtIndocNo.Text = dr["IndosNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["IndosIssuePlace"].ToString()))
                {
                    txtIndocIssuePlace.Text = dr["IndosIssuePlace"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["IndosExpiryDate"].ToString()))
                {
                    txtIndocExpiry.Text = DateTime.Parse(dr["IndosExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrEmpty(dr["IndosIssueDate"].ToString()))
                {
                    txtIndocIssueDt.Text = DateTime.Parse(dr["IndosIssueDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrEmpty(dr["CDCNo"].ToString()))
                {
                    txtCdcNo.Text = dr["CDCNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CDCIssuePlace"].ToString()))
                {
                    txtCDCIssuePlace.Text = dr["CDCIssuePlace"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CDCExpiryDate"].ToString()))
                {
                    txtCDCExpiry.Text = DateTime.Parse(dr["CDCExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (!string.IsNullOrEmpty(dr["CDCIssueDate"].ToString()))
                {
                    txtCDCIssueDate.Text = DateTime.Parse(dr["CDCIssueDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                if (!string.IsNullOrEmpty(dr["NearestAirportCountryId"].ToString()))
                {
                    dd_nearest_airport.SelectedValue = dr["NearestAirportCountryId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["MaritalStatusId"].ToString()))
                {
                    ddmaritalstatus.SelectedValue = dr["MaritalStatusId"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["MobileCountryId"].ToString()))
                {
                    ddl_P_CountryCode_Mobile.SelectedValue = dr["MobileCountryId"].ToString();
                }
                ancCV.Visible = (dr["FileName"].ToString().Trim() != "");
                ViewState["STATUS"] = dr["STATUS"].ToString();
                //txtComm.Text = dr["ReqRemarks"].ToString();

                
                lblUpdatedByOn.Text = dr["ModifiedBy"].ToString() + " / " + Common.ToDateString(dr["ModifiedOn"]);


                if (ancCV.Visible)
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    ancCV.HRef = "/" + appname + "/EMANAGERBLOB/HRD/Applicant/CV/" + dr["FileName"].ToString();
                    //string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    //ancCV.HRef = "/EMANAGERBLOB/HRD/Applicant/CV/" + dr["FileName"].ToString();
                }
                UtilityManager um = new UtilityManager();
                if (! string.IsNullOrWhiteSpace(dr["PhotoPath"].ToString()))
                {
                    img_Crew.ImageUrl = um.DownloadFileFromServer(dr["PhotoPath"].ToString(), "C");
                }
                else
                {
                    img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
                }
                foreach (ListItem li in chkVeselType.Items)
                {
                    if (dr["VesselTypes"].ToString().Contains(li.Value))
                        li.Selected = true;
                }

                string temp = "";
                string temp1 = "";
                string temp2 = "";
                string temp3 = "";
                lblAppRecivedOn.Text = DateTime.Parse(dr["CreatedOn"].ToString()).ToString("dd-MMM-yyyy");

                //lblActivity.InnerHtml = "<div style='padding:5px;line-height:20px;'> <b> App. Received On</b> </br> " + DateTime.Parse(dr["CreatedOn"].ToString()).ToString("dd-MMM-yyyy") + "</div>";

                temp = getUserName(Common.CastAsInt32(dr["AppSentBy"].ToString()));
                if (temp.Trim() != "" || !string.IsNullOrEmpty(temp.Trim()))
                {
                    div1.Visible = true;
                    if (dr["AppSentOn"].ToString() != "")
                    {
                        lblReqforAppBy.Text = temp + "/" + DateTime.Parse(dr["AppSentOn"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblReqforAppBy.Text = temp + "/" + "";
                    }
                    txtApprovalRemarks.Text = dr["ReqRemarks"].ToString(); ;
                    txtApprovalRemarks.ToolTip = dr["ReqRemarks"].ToString(); 
                    lblStatus.Text = "Awaiting Approval";
                }


                //lblActivity.InnerHtml = lblActivity.InnerHtml + "<div style='padding:5px;line-height:20px;'> <b> Requested for App. By/On </b> </br> " + temp + " / " + DateTime.Parse(dr["AppSentOn"].ToString()).ToString("dd-MMM-yyyy") + "</div> ";

                temp1 = getUserName(Common.CastAsInt32(dr["AppBy"].ToString()));
                if (temp1.Trim() != "" || !string.IsNullOrEmpty(temp1.Trim()))
                {
                    div2.Visible = true;
                    if (dr["AppOn"].ToString() != "")
                    {
                        lblApprovedBy.Text = temp1 + " / " + DateTime.Parse(dr["AppOn"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblApprovedBy.Text = temp1 + "/" + "";
                    }
                    lblApprovalId.Text = dr["ApprovalId"].ToString();
                    lblStatus.Text = "Approved for Employment";
                }


                //lblActivity.InnerHtml = lblActivity.InnerHtml + "<div style='padding:5px;line-height:20px;'> <b> Approved By/On </b> </br> " + temp + " / " + DateTime.Parse(dr["AppOn"].ToString()).ToString("dd-MMM-yyyy") + "</br> " + "<div style='color:#FF0066;font-size:15px;margin-top:10px;'>Approval No : " + dr["ApprovalId"].ToString() + "</div></div> ";

                temp2 = getUserName(Common.CastAsInt32(dr["RejBy"].ToString()));
                if (temp2.Trim() != "" || !string.IsNullOrEmpty(temp2.Trim()))
                {
                    div3.Visible = true;
                    if (dr["RejOn"].ToString() != "")
                    {
                        lblRejBy.Text = temp2 + " / " + DateTime.Parse(dr["RejOn"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblRejBy.Text = temp2 + " / " + "";
                    }
                    lblStatus.Text = "Rejected for Employment";
                }


                //lblActivity.InnerHtml = lblActivity.InnerHtml + "<div style='padding:5px;line-height:20px;'> <b> Rejected By/On </b> </br> " + temp + " / " + DateTime.Parse(dr["RejOn"].ToString()).ToString("dd-MMM-yyyy") + "</div> ";

                temp3 = getUserName(Common.CastAsInt32(dr["ArchiveBy"].ToString()));
                if (temp3.Trim() != "" || !string.IsNullOrEmpty(temp3.Trim()))
                {
                    div4.Visible = true;
                    if (dr["ArchiveOn"].ToString() != "")
                    {
                        lblArchivedby.Text = temp + " / " + DateTime.Parse(dr["ArchiveOn"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblArchivedby.Text = temp + " / " + "";
                    }
                }


                //lblActivity.InnerHtml = lblActivity.InnerHtml + "<div style='padding:5px;line-height:20px;'> <b> Archived By/On </b> </br> " + temp + " / " + DateTime.Parse(dr["ArchiveOn"].ToString()).ToString("dd-MMM-yyyy") + "</div> ";

                //ShowDetails();
                //ViewState["ACT"] = lblActivity.InnerHtml;
            }
    }

    public string getUserName(int UserId)
    {
        DataTable dtdetails = Budget.getTable("SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=" + UserId.ToString()).Tables[0];
        if (dtdetails.Rows.Count > 0)
        {
            return dtdetails.Rows[0][0].ToString();
        }
        else
        {
            return "";
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Vesseltypes = "";
            foreach (ListItem li in chkVeselType.Items)
            {
                if (li.Selected)
                    Vesseltypes = Vesseltypes + "," + li.Value;
            }
            if (Vesseltypes.StartsWith(","))
            {
                Vesseltypes = Vesseltypes.Substring(1);
            }
            //------------------------
            //string updFileName = "", updFileExt = "", updFilePath = "", saveFile = "";
            //if (FileUpload1.HasFile)
            //{
            //    updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            //    updFileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            //    saveFile = updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
            //    updFilePath = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\" + saveFile);
            //    FileUpload1.SaveAs(updFilePath);
            //}
            //------------------------
            if (candidateid > 0)
            {

                string str =  "UPDATE DBO.CandidatePersonalDetails " +
                    "SET " +
                        "FIRSTNAME='" + txtFirstName.Text.Trim() + "', " +
                        "MIDDLENAME='" + txtMiddleName.Text.Trim() + "', " +
                        "LASTNAME='" + txtLastName.Text.Trim() + "', " +
                        "NATIONALITYID=" + ddlNat.SelectedValue + ", " +
                        "DATEOFBIRTH='" + DateTime.Parse(txtDOB.Text.Trim()) + "', " +
                        "PlaceOfBirth='" + txtPOB.Text.Trim() + "', " +
                        "RANKAPPLIEDID=" + ddlRank.SelectedValue + ", " +
                        "Gender='" + radSex.SelectedValue + "', " +
                        "CONTACTNO='" + txtMobileNoPE.Text.Trim() + "', " +
                        "State='" + txtStatePE.Text.Trim() + "', " +
                        "PINCode='" + txtPincode.Text.Trim() + "', " +
                        "EMAILID='" + txt_P_EMail1.Text.Trim() + "', " +
                        "ADDRESS='" + txtAddressPE1.Text.Trim() + "', " +
                        "Address2='" + txtAddressPE2.Text.Trim() + "', " +
                        "Address3='" + txtAddressPE3.Text.Trim() + "', " +
                        "CITY='" + txtCityPE.Text.Trim() + "', " +
                        "CountryId=" + ddl_P_Country.SelectedValue.Trim() + ", " +
                        "MobileCountryId=" + ddl_P_CountryCode_Mobile.SelectedValue.Trim() + ", " +
                        "Height='" + txtHeight.Text.Trim() + "', " +
                        "Weight='" + txtWeight.Text.Trim() + "', " +
                        "Waist='" + txtwaist.Text.Trim() + "', " +
                        "BolierSuitSize='" + txtSuitSize.Text.Trim() + "', " +
                         "ShoeSizeId=" + ddl_Shoes.SelectedValue.Trim() + ", " +
                         "ShirtSizeId=" + ddl_Shirt.SelectedValue.Trim() + ", " +
                          "NearestAirportCountryId=" + dd_nearest_airport.SelectedValue.Trim() + ", " +
                           "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                           "IssueDate='" + DateTime.Parse(txtPassportIssueDt.Text.Trim()) + "', " +
                           "ExpiryDate='" + DateTime.Parse(txtPassportExpiry.Text.Trim()) + "', " +
                          "PassportIssuePlace='" + txtPassportIssuePlace.Text.Trim() + "', " +
                          "CDCNo='" + txtCdcNo.Text.Trim() + "', " +
                          "CDCIssueDate='" + DateTime.Parse(txtCDCIssueDate.Text.Trim()) + "', " +
                          "CDCExpiryDate='" + DateTime.Parse(txtCDCExpiry.Text.Trim()) + "', " +
                          "CDCIssuePlace='" + txtCDCIssuePlace.Text.Trim() + "', " +
                           "IndosNo='" + txtIndocNo.Text.Trim() + "', " +
                          "IndosIssueDate='" + DateTime.Parse(txtIndocIssueDt.Text.Trim()) + "', " +
                          "IndosExpiryDate='" + DateTime.Parse(txtIndocExpiry.Text.Trim()) + "', " +
                          "IndosIssuePlace='" + txtIndocIssuePlace.Text.Trim() + "', " +
                        "VESSELTYPES='" + Vesseltypes + "', " +
                        "[ModifiedById] = '" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                        "[ModifiedBy] = '" + Session["UserName"].ToString() + "', " +
                        "[ModifiedOn] = GETDATE(), " +
                         "[MaritalStatusId] = '" + ddmaritalstatus.SelectedValue + "' " +
                        "WHERE CANDIDATEID=" + candidateid.ToString();
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(str);
                SaveSeaExperience();
            }

            lbl_info.Text = "Record updated successfully.";
            btnSave.Visible = false;
            btnEdit.Visible = true;
            RefreshParent();

        }
        catch (Exception ex)
        {
            lbl_info.Text = "Unable to update record. System Messge : " + ex.Message;
        }
    }
    public void RefreshParent()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ref", "window.opener.refreshPage();", true);
    }

    protected void btnSaveComments_OnClick(object sender, EventArgs e)
    {
        if (btnSaveComments.CommandArgument == "Req")
        {
            try
            {
                Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET ReqRemarks='" + txtComments.Text.Trim().Replace("'", "''") + "', STATUS=2,APPSENTBY=" + LoginId.ToString() + ",APPSENTON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
                lblCommentsMsg.Text = "Applicant is send for approval successfully.";
                divMain.Visible = false;
                div1.Visible = true;
                divAppRej.Visible = true;
                div2.Visible = false;

                div3.Visible = false;
                divnotify.Visible = false;
                div4.Visible = false;
                btnApprove.Visible = true;
                btnReject.Visible = true;
                ShowRecord(sender, e);
                RefreshParent();
            }
            catch
            {
                lblCommentsMsg.Text = "Unable to save record.";
            }
        }
        else if (btnSaveComments.CommandArgument == "App")
        {
            try
            {
                Budget.getTable("EXEC DBO.GENEREATEAPPROVALID " + candidateid + " ");
                Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET AppRemarks='" + txtComments.Text.Trim().Replace("'", "''") + "', STATUS=3,APPBY=" + LoginId.ToString() + ",APPON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
                DataTable dtA = Budget.getTable("select ApprovalId from DBO.CANDIDATEPERSONALDETAILS where CandidateId=" + candidateid.ToString()).Tables[0];

                lblCommentsMsg.Text = "Candidate approved successfully. Approval No : <span style='font-size:18px;color:blue'>" + dtA.Rows[0][0].ToString() + "</span>";
                divMain.Visible = false;
                div1.Visible = true;
                divAppRej.Visible = false;
                div2.Visible = true;
                divnotify.Visible = true;
                btnNotify.Visible = true;
                ShowRecord(sender, e);
                RefreshParent();
            }
            catch
            {
                lblCommentsMsg.Text = "Unable to approve candidate.";
            }
        }
        else if (btnSaveComments.CommandArgument == "Rej")
        {
            try
            {
                // code for rejeection
                Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET RejRemarks='" + txtComments.Text.Trim().Replace("'", "''") + "', STATUS=4,REJBY=" + LoginId.ToString() + ",REJON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());

                // code for archive
                Budget.getTable("UPDATE DBO.CandidatePersonalDetails " +
                                                       "SET " +
                                                       "STATUS=5,ARCHIVEBY=" + LoginId.ToString() + ",ARCHIVEON=GETDATE() " +
                                                       "WHERE CANDIDATEID=" + candidateid.ToString());
                lblCommentsMsg.Text = "Applicant rejected & archived successfully.";
                divMain.Visible = false;
                div1.Visible = false;
                divAppRej.Visible = false;
                div2.Visible = false;
                div3.Visible = true;
                divnotify.Visible = true;
                btnNotify.Visible = true;
                ShowRecord(sender, e);
                RefreshParent();
            }
            catch
            {
                lblCommentsMsg.Text = "Unable to reject candidate.";
            }
        }

    }
    protected void btnCloseSaveComments_OnClick(object sender, EventArgs e)
    {
        dvComments.Visible = false;
    }
    protected void Validate(object sender, EventArgs e)
    {
        if (txtConDate.Text.Trim() != "")
        {
            DateTime dtControl = DateTime.Parse(txtConDate.Text);
            DateTime dtToday = DateTime.Today;
            if (dtToday < dtControl)
            { lblDateMess.Visible = true; }
            else
            { lblDateMess.Visible = false; }

        }
    }

    protected void btnNotify_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "OpenPrint(" + candidateid + ");", true);
    }
    protected void btnClose2_OnClick(object sender, EventArgs e)
    {
        dvAddComm.Visible = false;
    }

    protected void btnClose1_OnClick(object sender, EventArgs e)
    {
        dvAddAttachment.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Parse(txtConDate.Text);
        if (dt > DateTime.Now)
        {
            lbl_info.Text = "Date should be less than today.";
            txtConDate.Focus();
            return;
        }

        if (radCommType.SelectedValue.Trim() == "")
        {
            lbl_info.Text = "Please select one call type.";
            return;
        }

        if (SelectedDisc > 0)
            Budget.getTable("UPDATE DBO.CANDIDATEDISCUSSION SET DISC_DATE='" + txtConDate.Text + "',CALLEDBY=" + Session["loginid"].ToString() + ",DISCUSSION='" + txtCon.Text.Replace("'", "''") + "',DISCTYPE='" + radCommType.SelectedValue + "' WHERE TABLEID=" + SelectedDisc.ToString() + "");
        else
            Budget.getTable("INSERT INTO DBO.CANDIDATEDISCUSSION(CANDIDATEID,DISC_DATE,DISCUSSION,CALLEDBY,DISCTYPE) VALUES(" + candidateid.ToString() + ",'" + txtConDate.Text + "','" + txtCon.Text.Replace("'", "''") + "'," + Session["loginid"].ToString() + ",'" + radCommType.SelectedValue + "')");
        btnClear_Click(sender, new ImageClickEventArgs(0, 0));
        //ShowDetails();
    }
    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtConDate.Text = "";
        txtCon.Text = "";
        SelectedDisc = 0;
        //btnAdd.ImageUrl = "~/Modules/HRD/Images/add_16.gif";
    }

    protected void btnAddCommAtt_OnClick(object sender, EventArgs e)
    {
        if (txtAttDesc.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", " alert('Please enter description.');", true); return;
        }

        string FileName = "";
        if (fuCommAttachment.HasFile)
            FileName = fuCommAttachment.FileName;
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", " alert('Please browse file.');", true); return;
        }


        Common.Set_Procedures("Dbo.InsertUpdatecandidatepersonaldetailsAttachments");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
             new MyParameter("@candidateid", candidateid),
             new MyParameter("@Descr", txtAttDesc.Text.Trim()),
             new MyParameter("@FileName", FileName),
             new MyParameter("@FileContent", fuCommAttachment.FileBytes)
            );


        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD(Ds);
        ShowAttachment();
        txtAttDesc.Text = "";
    }

    //public void ShowAttachment()
    //{
    //    DataTable DT = Budget.getTable("Select * from Dbo.candidatepersonaldetailsAttachments  Where CandidateID=" + candidateid + "").Tables[0];
    //    rptCommAtt.DataSource = DT;
    //    rptCommAtt.DataBind();
    //}

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        dvMailSend.Visible = true;
        GetMaildetail(candidateid);
    }

    private void GetMaildetail(int candidateId)
    {
        string candidateFullName = txtLastName.Text.Trim() + " " + txtFirstName.Text.Trim() + " " + txtMiddleName.Text.Trim();
        string strcandidateId = Convert.ToString(candidateId);
        txtFromAddress.Text = ConfigurationManager.AppSettings["FromAddress"];
        txtToEmail.Text = txt_P_EMail1.Text.Trim();
        txtSubject.Text = "Update application Form - " + candidateFullName;
        string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/HRD/Applicant/SendSeaExperience.htm"));
        string SendSeaExpMailURL = ConfigurationManager.AppSettings["SendSeaExpMail"];
        string URl = SendSeaExpMailURL + candidateId.ToString();
        MailContent = MailContent.Replace("$SendSeaExpLINK$", URl);
        MailContent = MailContent.Replace("$crewFullName$", candidateFullName);
        litMessage.Text = MailContent;
    }

    protected void btnSendMailforExp_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (txtToEmail.Text == "" || string.IsNullOrWhiteSpace(txtToEmail.Text))
            {
                lblMessage.Text = "Please fill To Email field."; return;
            }

            if (txtSubject.Text.Trim() == "")
            {
                lblMessage.Text = "Please fill subject field."; return;
            }
            char[] Sep = { ';' };
            Int32 loginId = Common.CastAsInt32(Session["loginid"]);
            string ccmail = string.Empty;
            DataTable dtEmail = Budget.getTable("SELECT top 1 Email FROM DBO.UserMaster WHERE LOGINID=" + loginId).Tables[0];
            if (dtEmail.Rows.Count > 0)
            {
                ccmail = dtEmail.Rows[0][0].ToString();
            }
            string[] ToAdds = txtToEmail.Text.Split(Sep);
            string[] CCAdds = ccmail.Split(Sep);
            string[] BCCAdds = txtBCCEmail.Text.Split(Sep);
            //------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";
           
            if (SendEmail.SendeMail(Common.CastAsInt32(Session["loginid"]), txtFromAddress.Text.Trim(), txtFromAddress.Text.Trim(), ToAdds, CCAdds, BCCAdds, txtSubject.Text.Trim(), litMessage.Text.Trim(), out ErrMsg, AttachmentFilePath))
            {
                Budget.getTable("UPDATE DBO.CandidatePersonalDetails SET MailSentBy="+ Common.CastAsInt32(Session["loginid"]) + ", MailSentOn = GETDATE(), IsUpdated = 0 WHERE CandidateId=" + candidateid.ToString());
                lblMessage.Text = "Mail sent successfully.";
            }
        }
        catch (SystemException ex)
        {
            this.lblMessage.Text = "Unable to send mail : " + ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvMailSend.Visible = false;
        txtFromAddress.Text = "";
        txtToEmail.Text = "";
        txtBCCEmail.Text = "";
        litMessage.Text = "";
        txtSubject.Text = "";
    }

    protected void ibClose_Click(object sender, ImageClickEventArgs e)
    {
        btnCancel_Click(sender, e);
    }
    public void BindSeaExpRepeter()
    {
        string sql = "select * from  CandidateExpDetails with(nolock) where 1=2";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsSeaExpDtls = new DataSet();
        dsSeaExpDtls.Clear();
        dsSeaExpDtls = Common.Execute_Procedures_Select();

        if (dsSeaExpDtls != null)
        {
            DataRow dr = dsSeaExpDtls.Tables[0].NewRow();
            dr["RankId"] = "0";
            dr["VesselTypeId"] = "0";
            dr["VesselId"] = "0";
            dsSeaExpDtls.Tables[0].Rows.InsertAt(dr, 0);
            rptData.DataSource = dsSeaExpDtls;
            Session["CandidateExpDtls"] = dsSeaExpDtls;
            rptData.DataBind();

            // HFTotalGrdRow.Value = rptData.Items.Count.ToString();
        }

    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    public void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DropDownList ddlRank1 = (DropDownList)e.Item.FindControl("ddl_Rank1");
        DropDownList ddlVesselType = (DropDownList)e.Item.FindControl("ddl_VesselType");
        TextBox txtFromDt = (TextBox)e.Item.FindControl("txtfromdate");
        AjaxControlToolkit.CalendarExtender  calendarExtender = (AjaxControlToolkit.CalendarExtender)e.Item.FindControl("CalendarExtender8");
        //calendarExtender.TargetControlID = txtFromDt.ClientID;
        DataRowView drv = (DataRowView)e.Item.DataItem;
        string rankId = drv.Row["RankId"].ToString();
        string VesseltypeId = drv.Row["VesselTypeId"].ToString();
        ImageButton imgDelete = (ImageButton)e.Item.FindControl("imgDelete");

        if (IsApproved)
            imgDelete.Visible = true;
        else
            imgDelete.Visible = false;
        //ListItem li = ddlRank1.Items.FindByValue(rankId);
        //ListItem liVesseltype = ddlRank1.Items.FindByValue(VesseltypeId);

        ddlRank1.DataTextField = "RankCode";
        ddlRank1.DataValueField = "RankId";
        ddlRank1.DataSource = Common.Execute_Procedures_Select_ByQuery("Select RankId,LTRIM(RTRIM(RankName)) +' ('+ LTRIM(RTRIM(RankCode)) + ')' As RankCode from DBO.Rank where statusid='A' and RankId Not In (48,49) order By RankLevel");
        ddlRank1.DataBind();
        ddlRank1.Items.Insert(0, new ListItem("< Select >", "0"));

        string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
        ddlVesselType.DataTextField = "VesselTypeName";
        ddlVesselType.DataValueField = "VesselTypeId";
        ddlVesselType.DataSource = Common.Execute_Procedures_Select_ByQuery(" Select VesselTypeId,VesselTypeName from DBO.VesselType where StatusId = 'A' order By VesselTypeName ");
        ddlVesselType.DataBind();
        ddlVesselType.Items.Insert(0, new ListItem("< Select >", "0"));

        ddlRank1.SelectedValue = rankId;
        ddlVesselType.SelectedValue = VesseltypeId;
    }

    protected void SaveSeaExperience()
    {
        if (ItemValidation() == false) return; else lbl_info.Text = "";
        string strDel = "Delete from CandidateExpDetails  where CandidateId = " + candidateid;
        DataTable dtDeleteSeaExp = Common.Execute_Procedures_Select_ByQuery(strDel);
        foreach (RepeaterItem RptItm in rptData.Items)
        {
            HiddenField hdnExpId = (HiddenField)RptItm.FindControl("hfExpID");
            TextBox txtfromDt = (TextBox)RptItm.FindControl("txtfromdate");
            TextBox txttoDt = (TextBox)RptItm.FindControl("txtToDate");
            TextBox txtCompany = (TextBox)RptItm.FindControl("txtcompname");
            TextBox txtVessel = (TextBox)RptItm.FindControl("txtvesselname");
            DropDownList ddlRank1 = (DropDownList)RptItm.FindControl("ddl_Rank1");
            DropDownList ddlVesselType = (DropDownList)RptItm.FindControl("ddl_VesselType");
            TextBox txtGRT = (TextBox)RptItm.FindControl("txtGRT");
            TextBox txtbhp = (TextBox)RptItm.FindControl("txtbhp1");

            int CanExpId = 0;
            
            if (hdnExpId.Value != "")
            {
                CanExpId = Convert.ToInt32(hdnExpId.Value);
            }
            string STR = String.Empty;
            if (CanExpId > 0)
            {
                STR = "UPDATE DBO.CandidateExpDetails " +
                        "SET [CompanyName] = '" + txtCompany.Text.Trim() + "'  " +
                        ",[RankId] = " + Convert.ToInt32(ddlRank1.SelectedValue) + " " +
                        ",[SignOn] = '" + DateTime.Parse(txtfromDt.Text) + "' " +
                        ",[SignOff] = '" + DateTime.Parse(txttoDt.Text) + "' " +
                        ",[Duration] = NULL" +
                        ",[SignOffReasonId] = NULL" +
                        ",[VesselName] = '" + txtVessel.Text.Trim() + "' " +
                        ",[VesselId] = 0 " +
                        ",[VesselTypeId] = " + Convert.ToInt32(ddlVesselType.SelectedValue) + " " +
                        ",[BHP] = '" + txtGRT.Text + "'" +
                        ",[ExpFlag] = 'O' " +
                        ",[BHP1] = '" + txtbhp.Text + "'" +
                        ",[ModifiedBy] = '"+ Session["FullName"] + "' " +
                        ",[ModifiedOn] = GETDATE() " +
                        " where CandidateExpId = " + CanExpId + " AND CandidateId = " + candidateid;
            }
            else
            {
                STR = "INSERT INTO DBO.CandidateExpDetails( " +
                         "[CandidateId] " +
                         ",[CompanyName] " +
                         ",[RankId] " +
                         ",[SignOn] " +
                         ",[SignOff] " +
                         ",[Duration] " +
                         ",[SignOffReasonId] " +
                         ",[VesselName] " +
                         ",[VesselId] " +
                         ",[VesselTypeId] " +
                         ",[BHP] " +
                         ",[ExpFlag] " +
                         ",[BHP1] " +
                         ",[CreatedBy] " +
                         ",[CreatedOn] " +
                         ") VALUES(" + Convert.ToInt32(candidateid) + ",'" + txtCompany.Text.Trim() + "'," + Convert.ToInt32(ddlRank1.SelectedValue)
                         + ",'" + DateTime.Parse(txtfromDt.Text) + "','" + DateTime.Parse(txttoDt.Text)
                         + "',NULL,NULL,'" + txtVessel.Text.Trim() + "',0," + Convert.ToInt32(ddlVesselType.SelectedValue) + ",'" + txtGRT.Text + "','O','" + txtbhp.Text
                         + "','"+ Session["FullName"] + "',GETDATE() )";
            }

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(STR);
        }
    }
    protected void imgbtnAddSeaExp_Click(object sender, EventArgs e)
    {

        // Item Validation
        if (ItemValidation() == false) return; else lbl_info.Text = "";

        //Fill data set by the values entered in the textbox
        FillDataINDataSet();

        // Add a row to dataset
        DataSet CanExpDtlsDataSet = (DataSet)Session["CandidateExpDtls"];
        DataRow dr = CanExpDtlsDataSet.Tables[0].NewRow();
        dr["RankId"] = "0";
        dr["VesselTypeId"] = "0";
        dr["CandidateExpId"] = "0";
        CanExpDtlsDataSet.Tables[0].Rows.InsertAt(dr, 0);
        rptData.DataSource = CanExpDtlsDataSet;

        //BindUOM(ddlUOM);
        Session["CandidateExpDtls"] = CanExpDtlsDataSet;
        rptData.DataBind();

        SetSerialNumberToRptData();
    }
    public bool ItemValidation()
    {
        if (rptData.Items.Count > 0)
        {
            RepeaterItem RptItm = (RepeaterItem)rptData.Items[0];

            TextBox txtcompname = (TextBox)RptItm.FindControl("txtcompname");
            TextBox txtvesselname = (TextBox)RptItm.FindControl("txtvesselname");
            DropDownList ddl_VesselType = (DropDownList)RptItm.FindControl("ddl_VesselType");
            TextBox txtfromdate = (TextBox)RptItm.FindControl("txtfromdate");
            TextBox txtToDate = (TextBox)RptItm.FindControl("txtToDate");

            DropDownList ddl_Rank1 = (DropDownList)RptItm.FindControl("ddl_Rank1");
            //Description
            if (txtcompname.Text.Trim() == "")
            {
                lbl_info.Text = "Company Name is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + lbl_info.ClientID + "').focus()", true);
                return false;
            }
            //Part No
            if (txtvesselname.Text.Trim() == "")
            {
                lbl_info.Text = "Vessel Name field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtvesselname.ClientID + "').focus()", true);
                return false;
            }
            //txtDrawingNo
            if (ddl_VesselType.SelectedIndex == 0)
            {
                lbl_info.Text = "Vessel Type field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + ddl_VesselType.ClientID + "').focus()", true);
                return false;
            }
            //Code No
            if (txtfromdate.Text.Trim() == "")
            {
                lbl_info.Text = "From Date field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtfromdate.ClientID + "').focus()", true);
                return false;
            }
            //Quantity 
            if (txtToDate.Text.Trim() == "")
            {
                lbl_info.Text = "To Date field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + txtToDate.ClientID + "').focus()", true);
                return false;
            }

            if (ddl_Rank1.SelectedIndex == 0)
            {
                lbl_info.Text = "Rank field is empty ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "a", "document.getElementById('" + ddl_Rank1.ClientID + "').focus()", true);
                return false;
            }

        }
        return true;
    }
    public void FillDataINDataSet()
    {
        int ItmIndex = 0;
        DataSet CanExpDtlsDataSet = (DataSet)Session["CandidateExpDtls"];

        foreach (RepeaterItem SeaExpDtls in rptData.Items)
        {

            TextBox txtCompany = (TextBox)SeaExpDtls.FindControl("txtcompname");
            TextBox txtVessel = (TextBox)SeaExpDtls.FindControl("txtvesselname");
            DropDownList ddlVesselType = (DropDownList)SeaExpDtls.FindControl("ddl_VesselType");
            TextBox txtfromDt = (TextBox)SeaExpDtls.FindControl("txtfromdate");
            TextBox txttoDt = (TextBox)SeaExpDtls.FindControl("txtToDate");
            DropDownList ddlRank1 = (DropDownList)SeaExpDtls.FindControl("ddl_Rank1");
            TextBox txtGRT = (TextBox)SeaExpDtls.FindControl("txtGRT");
            TextBox txtbhp = (TextBox)SeaExpDtls.FindControl("txtbhp1");

            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["CompanyName"] = txtCompany.Text;
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["VesselName"] = txtVessel.Text;
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["VesselTypeId"] = ddlVesselType.SelectedValue;
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["SignOn"] = Common.CastAsDate(txtfromDt.Text);
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["SignOff"] = Common.CastAsDate(txttoDt.Text);
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["RankId"] = ddlRank1.SelectedValue;
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["BHP1"] = txtbhp.Text;
            CanExpDtlsDataSet.Tables[0].Rows[ItmIndex]["BHP"] = txtGRT.Text;
            //PrDataSet.Tables[0].Rows[ItmIndex]["targetCompDate1"] = DtTargetDate;
            ItmIndex = ItmIndex + 1;
        }
        Session["CandidateExpDtls"] = CanExpDtlsDataSet;
    }
    public void SetSerialNumberToRptData()
    {
        int RptLnt = rptData.Items.Count;
        int sno = 1;
        for (int i = RptLnt - 1; i >= 0; i--)
        {
            Label lblRowNumber = (Label)rptData.Items[i].FindControl("lblRowNumber");
            lblRowNumber.Text = Convert.ToString(i + 1);
        }
    }

    public void BindSeaExpDtlsRepeter(int candidateid)
    {
        string sql = "select * from  CandidateExpDetails where CandidateId = " + candidateid;
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsSeaExpDtls = new DataSet();
        dsSeaExpDtls.Clear();
        dsSeaExpDtls = Common.Execute_Procedures_Select();
        if (dsSeaExpDtls != null)
        {

            if (dsSeaExpDtls.Tables[0].Rows.Count != 0)
            {
                Session["CandidateExpDtls"] = dsSeaExpDtls;
                rptData.DataSource = dsSeaExpDtls;
                rptData.DataBind();
                SetSerialNumberToRptData();
            }
            else
            {
                lbl_info.Text = "No Data Found ";
            }
        }
        else
        {
            lbl_info.Text = "No Data Found ";
        }

    }

    protected void btnAddAtt_Click(object sender, ImageClickEventArgs e)
    {
        dvAddAttachment.Visible = true;
    }

   
    public void ShowAttachment()
    {
        DataTable DT = Budget.getTable("Select * from Dbo.candidatepersonaldetailsAttachments  Where CandidateID=" + candidateid + "").Tables[0];
        rptCommAtt.DataSource = DT;
        rptCommAtt.DataBind();
    }


    protected void btnDownLoadFile_OnClick(object sender, EventArgs e)
    {
        int TableID = Common.CastAsInt32(hfTableID.Value);
        string FileName = hfFileName.Value;
        DownloadAttachment(TableID, FileName);
    }
   
    public void DownloadAttachment(int TableID, string FileName)
    {
        try
        {
            string extension = Path.GetExtension(FileName).Substring(1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + extension;
            Response.BinaryWrite(getFormAttachment(TableID));
        }
        catch { }
    }
    public static byte[] getFormAttachment(int TableID)
    {
        byte[] ret = null;
        DataSet RetValue = new DataSet();
        RetValue = Budget.getTable("Select FileName,FileContent from Dbo.candidatepersonaldetailsAttachments Where TableID=" + TableID + "");
        if (RetValue.Tables[0].Rows.Count > 0)
        {
            ret = (byte[])RetValue.Tables[0].Rows[0]["FileContent"];
        }
        return ret;
    }

    protected void btnDeleteCommFile_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int TableID = Common.CastAsInt32(btn.CommandArgument);

        DataSet RetValue = new DataSet();
        RetValue = Budget.getTable("Delete from Dbo.candidatepersonaldetailsAttachments Where TableID=" + TableID + "");
        ShowAttachment();
    }

    public void imgDelete_Click(object sender, EventArgs e)
    {
        //Fill data set by the values entered in the textbox
        FillDataINDataSet();

        ImageButton imgDelete = (ImageButton)sender;
        int repeaterItemIndex = ((RepeaterItem)imgDelete.NamingContainer).ItemIndex;
        DataSet CanExpDtlsDataSet = (DataSet)Session["CandidateExpDtls"];
        CanExpDtlsDataSet.Tables[0].Rows.RemoveAt(repeaterItemIndex);

        Session["CandidateExpDtls"] = CanExpDtlsDataSet;
        rptData.DataSource = CanExpDtlsDataSet;
        rptData.DataBind();

        SetSerialNumberToRptData();
    }
}