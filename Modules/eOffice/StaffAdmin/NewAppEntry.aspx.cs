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
//using ApplicationFormWebService;

public partial class NewAppEntry : System.Web.UI.Page
{

   public int SelectedDisc 
    {
        get { return Common.CastAsInt32(ViewState["SelectedDisc "]); }
        set { ViewState["SelectedDisc "] = value; } 
    }
   public int ApplicantID
    {
        get { return Common.CastAsInt32(ViewState["ApplicantID"]); }
        set { ViewState["ApplicantID"] = value; } 
    }
   public int TABLEID
    {
        get { return Common.CastAsInt32(ViewState["TABLEID"]); }
        set { ViewState["TABLEID"] = value; } 
    }
    
   int LoginId = 0;
   protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblDateMess.Visible = false; 
        this.lbl_info.Text = "";
        try
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }
        catch 
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Your session has been expired.\\n Please login again.');window.close();", true);   
        } 
        if (!Page.IsPostBack)
        {
            BindNationality(); 
            BindRank(); 
            BindStatus();
            ApplicantID = Common.CastAsInt32(Request.QueryString["APID"]);
            if(ApplicantID >0)
                ShowRecord();
            SetControlVisibility();

            //int status = Common.CastAsInt32("0" + ViewState["STATUS"]);
        }
    }

   // ----------------------------  Function
   private void ShowRecord()
   {
       string Query = " SELECT Row_number() over(order by APID )RowNumber,APID,FirstName,MiddleName,LastName ,TelCountryCode ,TelAreaCode "+
                      " ,MobCountryCode ,MobAreaCode ,FaxCountryCode ,FaxAreaCode " +
                      " ,REPLACE (CONVERT(VARCHAR,DOB,106),' ','-')DOB " +
                      " ,PasportNumber " +
                      " ,PositionApplied Rankid " +
                      " ,(Select RankCode from dbo.rank where Rank.Rankid=AM.PositionApplied) as RankName " +
                      " ,REPLACE (CONVERT(VARCHAR,AvailableFrom,106),' ','-')AvailableFrom " +
                      " ,Nationality " +
                      " ,(select NationalityCode from dbo.Country where countryid=AM.Nationality) NationalityName " +
                      " ,Add1,Add2,Add3,Country,State,City,ZipCode,TelNumber,MobNumber,FaxNumber,Email1 ,Email2,'DISCUSSION' as DISCUSSION "+
                      " ,(select StatusName from HR_ApplicantStatus where HR_ApplicantStatus.StatusID=AM.Status ) Status " +
                      " ,REPLACE (CONVERT(VARCHAR,DOA,106),' ','-')DOA,FileName " +
                  " FROM DBO.HR_ApplicantMaster AM where AM.APID=" + ApplicantID.ToString();

       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Query);
       if (dt != null)
           if (dt.Rows.Count > 0)
           {
               DataRow dr = dt.Rows[0];
               txtFName.Text = dr["Firstname"].ToString();
               txtMName.Text = dr["Middlename"].ToString();
               txtLName.Text = dr["Lastname"].ToString();
               ddlNat.SelectedValue = dr["Nationality"].ToString();
               txtDOB.Text = dr["DOB"].ToString();
               ddlRank.SelectedValue = dr["Rankid"].ToString();
               txtAvalFrom.Text = dr["AvailableFrom"].ToString();

               txtTelCntCode.Text = dr["TelCountryCode"].ToString();
               txtTelAreaCode.Text = dr["TelAreaCode"].ToString();
               txtPhone.Text = dr["TelNumber"].ToString();

               txtMobCntCode.Text = dr["MobCountryCode"].ToString();
               txtMobAreaCode.Text = dr["MobAreaCode"].ToString();
               txtMobile.Text = dr["MobNumber"].ToString();

               txtEMail1.Text = dr["Email1"].ToString();               

               txtAddress1.Text = dr["Add1"].ToString();
               //txtAddress2.Text = dr["Add2"].ToString();
               //txtAddress3.Text = dr["Add3"].ToString();

               ddlCounty.SelectedValue = dr["Country"].ToString();
               txtState.Text = dr["State"].ToString();
               txtCity.Text = dr["City"].ToString();
               txtZipCode.Text = dr["ZipCode"].ToString();

               txtFaxCntCode.Text = dr["FaxCountryCode"].ToString();
               txtFaxAreaCode.Text = dr["FaxAreaCode"].ToString();
               txtFax.Text = dr["FaxNumber"].ToString();

               //ddlStatus.SelectedValue = dr["Status"].ToString();
               lblStatus.Text = dr["Status"].ToString();

               txtPassportNo.Text = dr["PasportNumber"].ToString();
               txtDoa.Text = dr["DOA"].ToString();

               ancCV.Visible = (dr["FileName"].ToString().Trim() != "");

               ViewState["STATUS"] = dr["Status"].ToString();

               if (ancCV.Visible)
               {
                   //ancCV.HRef = "http://emanagershore.energiosmaritime.com/shipboard/Resume/" + dr["FileName"].ToString();
                   ancCV.HRef = Server.MapPath("~/EMANAGERBLOB/HRD/Applicant/" + dr["FileName"].ToString());
               }
               //foreach (ListItem li in chkVeselType.Items)
               //{
               //    if (dr["VesselTypes"].ToString().Contains(li.Value))
               //        li.Selected = true;
               //}
               ShowDetails();
           }
   }
   private void ShowDetails()
   {
       DataTable dtdetails = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,(SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CALLEDBY) AS USERNAME,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM HR_ApplicantCommunication WHERE APID=" + ApplicantID.ToString() + " order by disc_date desc");
       if (dtdetails != null)
       {
           rptData.DataSource = dtdetails;
           rptData.DataBind();
       }
   }
   public void BindNationality()
   {
       string sql = "select CountryName,CountryId from Country ORDER BY CountryName";
       DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       this.ddlNat.DataTextField = "CountryName";
       this.ddlNat.DataValueField = "CountryId";
       this.ddlNat.DataSource = DT;
       this.ddlNat.DataBind();
       ddlNat.Items[0].Text = "<Select>";

       this.ddlCounty.DataTextField = "CountryName";
       this.ddlCounty.DataValueField = "CountryId";
       this.ddlCounty.DataSource = DT;
       this.ddlCounty.DataBind();
       ddlCounty.Items[0].Text = "<Select>";
   }
   
   public void BindRank()
   {
       //string sql = "SELECT RankCode,RankId FROM Rank Order by RankCode";
       string sql = "select PositionId,PositionName from Position  where PositionID not in(1)";
       DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);

       this.ddlRank.DataTextField = "PositionName";
       this.ddlRank.DataValueField = "PositionId";
       this.ddlRank.DataSource = DT;
       this.ddlRank.DataBind();
       ddlRank.Items.Insert(0, new ListItem("<Select>", "0"));
   }
   public void BindStatus()
   {
        //string sql = "select StatusId,StatusName from HR_ApplicantStatus";
        //DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //this.ddlStatus.DataTextField = "StatusName";
        //this.ddlStatus.DataValueField = "StatusId";
        //this.ddlStatus.DataSource = DT;
        //this.ddlStatus.DataBind();
        //ddlStatus.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    public void SetControlVisibility()
   {
       if (ApplicantID > 0)
       {
           //ddlStatus.Enabled = true;
           trStatus.Visible = true;
       }
       else
       {
           trStatus.Visible = false;
       }
   }

   protected void Validate(object sender, EventArgs e)
   {
       if (txtConDate.Text.Trim() != "")
       {
           DateTime dtControl = DateTime.Parse(txtConDate.Text);
           DateTime dtToday = DateTime.Today;
           if (dtToday < dtControl)
           {  lblDateMess.Visible = true;}
           else
           { lblDateMess.Visible = false; }

       }
   }
   public string getUserName(int UserId)
   {
       DataTable dtdetails = Common.Execute_Procedures_Select_ByQueryCMS("SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=" + UserId.ToString());
       if (dtdetails.Rows.Count > 0)
       {
           return dtdetails.Rows[0][0].ToString();
       }
       else
       {
           return "";
       }
   }
   public void RefreshParent()
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "ref", "window.opener.refreshPage();", true);
   }


   // ----------------------------  Events
   protected void btnSaveApplicant_Click(object sender, EventArgs e)
   {
       try
       {
           if (txtFName.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter First Name.";
               return;
           }
           if (txtLName.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter Last Name.";
               return;
           }
           if (txtDOB.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter Date of Birth.";
               return;
           }
           if (txtPassportNo.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter Passport#.";
               return;
           }
           //Validation for Passport Check
           string PassNo = "";
           if (ApplicantID <= 0)
               PassNo = "select PASPORTNUMBER from HR_ApplicantMaster where PASPORTNUMBER ='" + txtPassportNo.Text.Trim() + "'";
           else
               PassNo = "select PASPORTNUMBER from HR_ApplicantMaster where PASPORTNUMBER ='" + txtPassportNo.Text.Trim() + "' and APID <>" + ApplicantID + "";

           DataTable dtPassNo = Common.Execute_Procedures_Select_ByQueryCMS(PassNo);
           if (dtPassNo.Rows.Count > 0)
           {
               lbl_info.Text = "Passport# exist already.";
               return;
           }
           if (ddlRank.SelectedIndex == 0)
           {
               lbl_info.Text = "Please select Position Applied.";
               return;
           }
           if (ddlNat.SelectedIndex == 0)
           {
               lbl_info.Text = "Please select Nationality.";
               return;
           }
           if (ddlCounty.SelectedIndex == 0)
           {
               lbl_info.Text = "Please select Country.";
               return;
           }
           //-----------------------------------------------------------------
           //if (txtPhone.Text.Trim() == "" && txtMobile.Text.Trim() == "")
           //{
           //    lbl_info.Text = "Please enter Phone# OR Mobile#.";
           //    return; 
           //}
           //--------------------

           string Vesseltypes = "";
           //foreach (ListItem li in chkVeselType.Items)
           //{
           //    if (li.Selected)
           //        Vesseltypes = Vesseltypes + "," + li.Value;
           //}
           if (Vesseltypes.StartsWith(","))
           {
               Vesseltypes = Vesseltypes.Substring(1);
           }
           //------------------------
           string updFileName = "", updFileExt="",updFilePath = "", saveFile = "";
           if (FileUpload1.HasFile)
           {
               updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);  
               updFileExt = System.IO.Path.GetExtension(FileUpload1.FileName);  
               saveFile= updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
               updFilePath = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\" + saveFile);
               FileUpload1.SaveAs(updFilePath);  
           }
           //------------------------
           object oDOB, oAVAILABLEFROM, oDOA;
           if (txtDOB.Text.Trim() == "")oDOB = DBNull.Value;
           else oDOB = txtDOB.Text;

           if (txtAvalFrom.Text.Trim() == "") oAVAILABLEFROM = DBNull.Value;
           else oAVAILABLEFROM = txtAvalFrom.Text;

           if (txtDoa.Text.Trim() == "") oDOA = DBNull.Value;
           else oDOA = txtDoa.Text;

           Common.Set_Procedures("HR_InsertUpdateApplicantMaster");
           Common.Set_ParameterLength(30);
           Common.Set_Parameters(
               new MyParameter("@APID", ApplicantID),
               new MyParameter("@FIRSTNAME", txtFName.Text),
               new MyParameter("@MIDDLENAME", txtMName.Text),
               new MyParameter("@LASTNAME", txtLName.Text),
               new MyParameter("@DOB", oDOB),
               new MyParameter("@PASPORTNUMBER", txtPassportNo.Text),
               new MyParameter("@POSITIONAPPLIED", ddlRank.SelectedValue),
               new MyParameter("@AVAILABLEFROM", oAVAILABLEFROM),
               new MyParameter("@NATIONALITY", ddlNat.SelectedValue),
               new MyParameter("@ADD1", txtAddress1.Text),
               new MyParameter("@ADD2", ""),
               new MyParameter("@ADD3", ""),
               new MyParameter("@COUNTRY", ddlCounty.SelectedValue),
               new MyParameter("@STATE", txtState.Text),
               new MyParameter("@CITY", txtCity.Text),
               new MyParameter("@ZIPCODE", txtZipCode.Text),

               new MyParameter("@TelCountryCode", txtTelCntCode.Text),
               new MyParameter("@TelAreaCode", txtTelAreaCode.Text),
               new MyParameter("@TELNUMBER", txtPhone.Text),

                new MyParameter("@MobCountryCode", txtMobCntCode.Text),
                new MyParameter("@MobAreaCode", txtMobAreaCode.Text),
               new MyParameter("@MOBNUMBER", txtMobile.Text),

                new MyParameter("@FaxCountryCode", txtFaxCntCode.Text),
                new MyParameter("@FaxAreaCode", txtFaxAreaCode.Text),
               new MyParameter("@FAXNUMBER", txtFax.Text),
               new MyParameter("@EMAIL1", txtEMail1.Text),
               new MyParameter("@EMAIL2", ""),
               new MyParameter("@STATUS ", "1"),
               new MyParameter("@DOA", oDOA),
               new MyParameter("@FILENAME", saveFile)                   
               );
           DataSet DS=new DataSet();
           Boolean Res;
           Res = Common.Execute_Procedures_IUD_CMS(DS);
           if (Res)
           {
               ApplicantID = Common.CastAsInt32(DS.Tables[0].Rows[0][0]);
               lbl_info.Text = "Applicant data saved successfully.";
               //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Referesh", "RefereshParrentPageData()", true);
               Page.RegisterStartupScript("ssss", "<script>RefereshParrentPageData()</script>");
           }
                   
       }
       catch (Exception ex)
       {
           lbl_info.Text = "Unable to update record. System Messge : "  + ex.Message;
       }

   }
   
    //---- Comments
   protected void btnAddComSummary_Click(object sender, ImageClickEventArgs e)
   {
       if (ApplicantID <= 0)
       {
           lbl_info.Text = "First save candidate details.";
           return;
       }

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
       Common.Set_Procedures("HR_InsertUpdateApplicantCommunication");
       Common.Set_ParameterLength(6);
       Common.Set_Parameters(
            new MyParameter("@TABLEID", TABLEID),
            new MyParameter("@APID",ApplicantID),
            new MyParameter("@DISC_DATE", txtConDate.Text.Trim()),
            new MyParameter("@DISCUSSION", txtCon.Text.Trim()),
            new MyParameter("@CALLEDBY", LoginId),
            new MyParameter("@DISCTYPE", radCommType.SelectedItem.Value)
           );
       DataSet DS = new DataSet();
       Common.Execute_Procedures_IUD_CMS(DS);
       btnClear_Click(sender, new ImageClickEventArgs(0, 0));
       ShowDetails();
   }
   protected void btnDelComment_Click(object sender, EventArgs e)
   {
       ImageButton img = ((ImageButton)sender);
       int tableid = Common.CastAsInt32(img.CommandArgument);
       int CreatedBy = Common.CastAsInt32(img.CssClass);
       if (LoginId != CreatedBy && LoginId != 1)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Sorry !!\\nYour have not permission to delete this record.');", true);
           return;
       }
       try
       {
           DataTable dtdetails = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_ApplicantCommunication WHERE TABLEID=" + tableid.ToString());
           ShowDetails();
       }
       catch (Exception ex)
       {
       }
   }
   protected void btnClear_Click(object sender, ImageClickEventArgs e)
   {
       txtConDate.Text = "";
       txtCon.Text = "";
       SelectedDisc = 0;
       btnAddComSummary.ImageUrl = "~/Modules/HRD/Images/add_16.gif";
   }

   protected void btnRApprove_Click(object sender, EventArgs e)
   {
       try
       {
           Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CANDIDATEPERSONALDETAILS SET STATUS=2,APPSENTBY=" + LoginId.ToString() + ",APPSENTON=GETDATE() WHERE ApplicantID=" + ApplicantID.ToString());
           this.lbl_info.Text = "Record saved successfully.";
           RefreshParent();
       }
       catch 
       {
           this.lbl_info.Text = "Unable to save record.";
       } 
   }
   protected void btnApprove_Click(object sender, EventArgs e)
   {
       try
       {
           Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CANDIDATEPERSONALDETAILS SET STATUS=3,APPBY=" + LoginId.ToString() + ",APPON=GETDATE() WHERE ApplicantID=" + ApplicantID.ToString());
           this.lbl_info.Text = "Candidate approved successfully.";
           RefreshParent();
       }
       catch
       {
           this.lbl_info.Text = "Unable to approve candidate.";
       } 
   }
   protected void btnReject_Click(object sender, EventArgs e)
   {
       try
       {
           Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CANDIDATEPERSONALDETAILS SET STATUS=4,REJBY=" + LoginId.ToString() + ",REJON=GETDATE() WHERE ApplicantID=" + ApplicantID.ToString());
           this.lbl_info.Text = "Candidate rejected successfully.";
           RefreshParent();
       }
       catch
       {
           this.lbl_info.Text = "Unable to reject candidate.";
       } 
   }
   protected void btnTransfer_Click(object sender, EventArgs e)
   {
       try
       {
           TransferCandidateDetails objtrans = new TransferCandidateDetails();
           DataSet ds = objtrans.transfercandidatedata(ApplicantID, 1);
           if (ds.Tables.Count > 0)
           {
               string CrewNumber = ds.Tables[0].Rows[0]["CrewNumber"].ToString();
               //this.lbl_info.Text = "Candidate details transfered successfully.<br/> Employee number generated. </br>(If PassportNo is duplicate, records will not be transferred.) ";
               ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "window.opener.refreshPage();alert('Applicant [ " + CrewNumber + " ] transferred successfully.\\nIf PassportNo is duplicate, applicant will not be transferred.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
           }
       }
       catch (SystemException ex)
       {
           this.lbl_info.Text = "Unable to Transfer : " + ex.Message;
       }
       //DataTable dt = new DataTable();
       //dt.Columns.Add("Name");
       //dt.Columns.Add("DOB");
       //dt.Columns.Add("PassportNo");
       //dt.Columns.Add("CrewNumber");
       //DataSet ds;
       //try
       //{
       //    string str = "";
       //    int i;
       //    for (i = 0; i < gvcandidate.Rows.Count; i++)
       //    {
       //        CheckBox chk = ((CheckBox)gvcandidate.Rows[i].FindControl("chk1"));
       //        if (chk.Checked)
       //        {
       //            str = gvcandidate.DataKeys[i].Value.ToString();
       //            TransferCandidateDetails objtrans = new TransferCandidateDetails();
       //            ds = objtrans.transfercandidatedata(Convert.ToInt16(gvcandidate.DataKeys[i].Value.ToString()), 1);
       //            if (ds.Tables.Count > 0)
       //            {
       //                dt.Rows.Add(dt.NewRow());
       //                dt.Rows[dt.Rows.Count - 1]["Name"] = ds.Tables[0].Rows[0][0].ToString();
       //                dt.Rows[dt.Rows.Count - 1]["DOB"] = ds.Tables[0].Rows[0][1].ToString();
       //                dt.Rows[dt.Rows.Count - 1]["PassportNo"] = ds.Tables[0].Rows[0][2].ToString();
       //                dt.Rows[dt.Rows.Count - 1]["CrewNumber"] = ds.Tables[0].Rows[0][3].ToString();
       //            }
       //        }
       //    }
       //    if (str == "")
       //    {
       //        this.lbl_info.Text = "Please select atleast one record To transfer.";

       //    }
       //    else
       //    {
       //        gv_result.DataSource = dt;
       //        gv_result.DataBind();
       //        this.lbl_info.Text = "Candidate details transfered successfully.<br/> Employee number generated.</br>(If PassportNo is duplicate, records will not be transferred.) ";
       //    }
       //}
       //catch (SystemException es)
       //{
       //    this.lbl_License_Message.Text = es.Message;
       //}
       //binddata();
   }


   
   protected void rptEdit_Click(object sender, EventArgs e)
   {
       LinkButton li=((LinkButton)sender);
       int tableid = Common.CastAsInt32(li.CommandArgument);
       int CreatedBy = Common.CastAsInt32(li.CssClass);
       if (LoginId != CreatedBy && LoginId != 1)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Sorry !!\\nYour have not permission to edit this record.');", true);   
           return;
       }
       DataTable dtdetails = Common.Execute_Procedures_Select_ByQueryCMS("SELECT *,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM CANDIDATEDISCUSSION WHERE TABLEID=" + tableid.ToString());
       if (dtdetails != null)
       if (dtdetails.Rows.Count>0)  
       {
           SelectedDisc = tableid; 
           txtConDate.Text = dtdetails.Rows[0]["DISC_DATE_STR"].ToString();
           txtCon.Text = dtdetails.Rows[0]["DISC"].ToString();
           radCommType.SelectedValue = dtdetails.Rows[0]["DISCTYPE"].ToString();
           btnAddComSummary.ImageUrl = "~/Modules/HRD/Images/update.gif";
       }
       ShowDetails(); 
   }
   
   
   protected void btnArchive_Click(object sender, EventArgs e)
   {
       try
       {
           string str = ApplicantID.ToString();
           Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CandidatePersonalDetails " +
                                                    "SET " +
                                                    "STATUS=5,ARCHIVEBY=" + LoginId.ToString() + ",ARCHIVEON=GETDATE() " +
                                                    "WHERE ApplicantID=" + ApplicantID.ToString());
           lbl_info.Text = "Applicant archived successfully.";
           //ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr2", "alert('Applicant deleted successfully.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
       }
       catch (SystemException ex)
       {
           this.lbl_info.Text = "Unable to Delete : " + ex.Message;
       }
   }
   protected void btnEdit_Click(object sender, EventArgs e)
   {
       btnSaveApplicant.Visible = true;
   }
   protected void Page_PreRender(object sender, EventArgs e)
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_cdpopup');", true);   
   }
}
