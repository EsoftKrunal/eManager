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

public partial class CandidateDetailPopUp : System.Web.UI.Page
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
   protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblDateMess.Visible = false;
        lblCommentsMsg.Text = "";
        this.lbl_info.Text = "";
        this.lblMessage.Text = "";
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
            this.ddlNat.DataTextField = "CountryName";
            this.ddlNat.DataValueField = "CountryId";
            this.ddlNat.DataSource = Budget.getTable("SELECT CountryId,CountryName FROM COUNTRY").Tables[0];
            this.ddlNat.DataBind();
            ddlNat.Items[0].Text = "<All>";

            this.ddlRank.DataTextField = "RankCode";
            this.ddlRank.DataValueField = "RankId";
            this.ddlRank.DataSource = Budget.getTable("Select RankId,RankCode from DBO.Rank where statusid='A' and RankId Not In(48,49) order By RankLevel").Tables[0];
            this.ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("<All>", "0"));

            string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
            this.chkVeselType.DataTextField = "VesselTypeName";
            this.chkVeselType.DataValueField = "VesselTypeId";
            this.chkVeselType.DataSource = Budget.getTable("Select VesselTypeId,VesselTypeName from DBO.VesselType Where VesselTypeid in ("+ vesseltypes + ") order By VesselTypeName").Tables[0];
            this.chkVeselType.DataBind();
            candidateid = Common.CastAsInt32(Request.QueryString["candidate"]);
            ShowRecord();
            ShowAttachment();
            int status = Common.CastAsInt32("0" + ViewState["STATUS"]);

            DataTable dtTrans = Common.Execute_Procedures_Select_ByQueryCMS("SELECT candidateid FROM DBO.candidatepersonaldetails where status=3 and candidateid=" + candidateid.ToString());
            bool candidatetransferred=dtTrans.Rows.Count>0;

            bool ReqAppMode = (status == 1 || status == 4 || status == 5);
            if (ReqAppMode) 
            {
                //Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.SET_CANDIDATE_DOCS_CHECKLIST " + candidateid.ToString());
                //DataTable DT=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.Candidate_Doc_CheckList where candidateid=" + candidateid.ToString());
                //if(DT.Rows.Count >0)
                //{
                //    btnDocChk.Visible = true;
                //}
                //else
                //{
                    btnDocChk.Visible = false;
                //}
                ////----------------------------
                //DataTable DT1 = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.Candidate_Doc_CheckList where candidateid=" + candidateid.ToString() + " and CHECKEDBY IS NULL");
                //if (DT1.Rows.Count > 0)
                //{
                //    btnReqForApprove.Enabled = false;
                //}
                //else
                //{
                    btnReqForApprove.Enabled = true;
                    divMain.Visible = true;
                //}
            }
            else
            {
                
                btnReqForApprove.Enabled = false;
                divMain.Visible = false;
                btnDocChk.Visible = false;
                btnViewDocChk.Visible = false; 
            }

            btnArchive.Visible = (status == 1 || status == 4);
            
            if (status == 2  && Request.QueryString["M"].Trim().ToLower() == "app")
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
            

            

            btnEdit.Visible = (!btnApprove.Visible) && (!candidatetransferred);

            if (status == 3 || status == 4 || status == 5)
            {
                divMain.Visible = false;
                btnNotify.Visible = true; divnotify.Visible = true;
            }
           
            else
            {
                btnNotify.Visible = false;
                divnotify.Visible = false;
            }
                

            //if (Page.Request.QueryString["M"].ToString() == "App")
            //    btnAddAtt.Visible = false;
            //else
                btnAddAtt.Visible = true;
            
            //btnTransfer.Visible = (status == 3);
        }
       //lblActivity.InnerHtml=ViewState["ACT"].ToString();
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
   private void ShowRecord()
    {
        string Md =Convert.ToString(Request.QueryString["M"]);
        tr_Comm.Visible = (Md=="App");
        DataTable dt = Budget.getTable("select Firstname,Middlename,Lastname,ModifiedBy,ModifiedOn, " +
                        "nationalityid as Nationality, " +
                        "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB, " +
                        "rankappliedid as Rank, " +
                        "replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom, " +
                        "ContactNo,ContactNo2,EmailId,VesselTypes,Address,City,PassportNo,FileName,isnull(status,1) as status,CreatedBy,CreatedOn,AppSentBy,AppSentOn,AppBy,AppOn,RejBy,RejOn,ArchiveBy,ArchiveOn,ApprovalId,ReqRemarks " +
                        "from DBO.candidatepersonaldetails with(nolock) where candidateid=" + candidateid.ToString()).Tables[0];
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtFName.Text = dr["Firstname"].ToString();
                txtMName.Text = dr["Middlename"].ToString();
                txtLName.Text = dr["Lastname"].ToString();
                ddlNat.SelectedValue = dr["Nationality"].ToString();
                txtDOB.Text = dr["DOB"].ToString();
                ddlRank.SelectedValue = dr["Rank"].ToString();
                txtAvalFrom.Text = dr["AvailableFrom"].ToString();
                txtPhone.Text = dr["ContactNo"].ToString();
                txtMobile.Text = dr["ContactNo2"].ToString();
                txtEMail.Text = dr["EmailId"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtPassportNo.Text = dr["PassportNo"].ToString();
                ancCV.Visible = (dr["FileName"].ToString().Trim() != "");
                ViewState["STATUS"] = dr["STATUS"].ToString();
                txtComm.Text = dr["ReqRemarks"].ToString();

                if (Convert.IsDBNull(dr["ModifiedBy"]))
                    lblUpdatedByOn.Text = "Website / " + Common.ToDateString(dr["ModifiedOn"]);
                else 
                    lblUpdatedByOn.Text = dr["ModifiedBy"].ToString() + " / " + Common.ToDateString(dr["ModifiedOn"]);
                

                if (ancCV.Visible)
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    ancCV.HRef = "/"+ appname + "/EMANAGERBLOB/HRD/Applicant/CV/" + dr["FileName"].ToString();
                    
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
                    txtApprovalRemarks.Text = txtComm.Text;
                    txtApprovalRemarks.ToolTip = txtComm.Text;
                    lblStatus.Text = "Awaiting Approval";
                }
                   
                
                    //lblActivity.InnerHtml = lblActivity.InnerHtml + "<div style='padding:5px;line-height:20px;'> <b> Requested for App. By/On </b> </br> " + temp + " / " + DateTime.Parse(dr["AppSentOn"].ToString()).ToString("dd-MMM-yyyy") + "</div> ";

                temp1 = getUserName(Common.CastAsInt32(dr["AppBy"].ToString()));
                if (temp1.Trim() != "" || !string.IsNullOrEmpty(temp1.Trim()) )
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

                ShowDetails();
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
   private void ShowDetails()
   {
       DataTable dtdetails = Budget.getTable("SELECT *,(SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CALLEDBY) AS USERNAME,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM dbo.CANDIDATEDISCUSSION WHERE CANDIDATEID=" + candidateid.ToString() + " order by disc_date desc").Tables[0];
       if (dtdetails != null)
       {
           rptData.DataSource = dtdetails;
           rptData.DataBind();
       }
   }
   protected void btnNotify_Click(object sender, EventArgs e)
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "OpenPrint(" + candidateid + ");", true);
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
   //protected void btnTransfer_Click(object sender, EventArgs e)
   //{
   //    try
   //    {
   //        TransferCandidateDetails objtrans = new TransferCandidateDetails();
   //        DataSet ds = objtrans.transfercandidatedata(candidateid, 1);
   //        if (ds.Tables.Count > 0)
   //        {
   //            string CrewNumber = ds.Tables[0].Rows[0]["CrewNumber"].ToString();
   //            //this.lbl_info.Text = "Candidate details transfered successfully.<br/> Employee number generated. </br>(If PassportNo is duplicate, records will not be transferred.) ";
   //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "window.opener.refreshPage();alert('Applicant [ " + CrewNumber + " ] transferred successfully.\\nIf PassportNo is duplicate, applicant will not be transferred.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
   //        }
   //    }
   //    catch (SystemException ex)
   //    {
   //        this.lbl_info.Text = "Unable to Transfer : " + ex.Message;
   //    }
   //    //DataTable dt = new DataTable();
   //    //dt.Columns.Add("Name");
   //    //dt.Columns.Add("DOB");
   //    //dt.Columns.Add("PassportNo");
   //    //dt.Columns.Add("CrewNumber");
   //    //DataSet ds;
   //    //try
   //    //{
   //    //    string str = "";
   //    //    int i;
   //    //    for (i = 0; i < gvcandidate.Rows.Count; i++)
   //    //    {
   //    //        CheckBox chk = ((CheckBox)gvcandidate.Rows[i].FindControl("chk1"));
   //    //        if (chk.Checked)
   //    //        {
   //    //            str = gvcandidate.DataKeys[i].Value.ToString();
   //    //            TransferCandidateDetails objtrans = new TransferCandidateDetails();
   //    //            ds = objtrans.transfercandidatedata(Convert.ToInt16(gvcandidate.DataKeys[i].Value.ToString()), 1);
   //    //            if (ds.Tables.Count > 0)
   //    //            {
   //    //                dt.Rows.Add(dt.NewRow());
   //    //                dt.Rows[dt.Rows.Count - 1]["Name"] = ds.Tables[0].Rows[0][0].ToString();
   //    //                dt.Rows[dt.Rows.Count - 1]["DOB"] = ds.Tables[0].Rows[0][1].ToString();
   //    //                dt.Rows[dt.Rows.Count - 1]["PassportNo"] = ds.Tables[0].Rows[0][2].ToString();
   //    //                dt.Rows[dt.Rows.Count - 1]["CrewNumber"] = ds.Tables[0].Rows[0][3].ToString();
   //    //            }
   //    //        }
   //    //    }
   //    //    if (str == "")
   //    //    {
   //    //        this.lbl_info.Text = "Please select atleast one record To transfer.";

   //    //    }
   //    //    else
   //    //    {
   //    //        gv_result.DataSource = dt;
   //    //        gv_result.DataBind();
   //    //        this.lbl_info.Text = "Candidate details transfered successfully.<br/> Employee number generated.</br>(If PassportNo is duplicate, records will not be transferred.) ";
   //    //    }
   //    //}
   //    //catch (SystemException es)
   //    //{
   //    //    this.lbl_License_Message.Text = es.Message;
   //    //}
   //    //binddata();
   //}
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try{
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
           string updFileName = "", updFileExt = "", updFilePath = "", saveFile = "";
           if (FileUpload1.HasFile)
           {
               updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
               updFileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
               saveFile = updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
               updFilePath = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\" + saveFile);
               FileUpload1.SaveAs(updFilePath);
           }
           //------------------------
           if (updFileName.Trim() == "")
           {
               Budget.getTable("UPDATE DBO.CandidatePersonalDetails " +
                            "SET " +
                            "FIRSTNAME='" + txtFName.Text.Trim() + "', " +
                            "MIDDLENAME='" + txtMName.Text.Trim() + "', " +
                            "LASTNAME='" + txtLName.Text.Trim() + "', " +
                            "NATIONALITYID=" + ddlNat.SelectedValue + ", " +
                            "DATEOFBIRTH='" + txtDOB.Text.Trim() + "', " +
                            "RANKAPPLIEDID=" + ddlRank.SelectedValue + ", " +
                            "CONTACTNO='" + txtPhone.Text.Trim() + "', " +
                            "CONTACTNO2='" + txtMobile.Text.Trim() + "', " +
                            "EMAILID='" + txtEMail.Text.Trim() + "', " +
                            "ADDRESS='" + txtAddress.Text.Trim() + "', " +
                            "CITY='" + txtCity.Text.Trim() + "', " +
                            "AvailableFrom='" + txtAvalFrom.Text.Trim() + "', " +
                            "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                             
                            //"ModifiedBy='" + Session["UserName"].ToString() + "', " +
                            //"ModifiedOn=Getdate(), " +

                            "VESSELTYPES='" + Vesseltypes + "', " +
                            "ModifiedById='" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                             "ModifiedBy= '" + Session["UserName"].ToString() + "', " +
                             "ModifiedOn = GETDATE() " +
                            "WHERE CANDIDATEID=" + candidateid.ToString());
           }
           else
           {
               Budget.getTable("UPDATE DBO.CandidatePersonalDetails " +
                              "SET " +
                              "FIRSTNAME='" + txtFName.Text.Trim() + "', " +
                              "MIDDLENAME='" + txtMName.Text.Trim() + "', " +
                              "LASTNAME='" + txtLName.Text.Trim() + "', " +
                              "NATIONALITYID=" + ddlNat.SelectedValue + ", " +
                              "DATEOFBIRTH='" + txtDOB.Text.Trim() + "', " +
                              "RANKAPPLIEDID=" + ddlRank.SelectedValue + ", " +
                              "CONTACTNO='" + txtPhone.Text.Trim() + "', " +
                              "CONTACTNO2='" + txtMobile.Text.Trim() + "', " +
                              "EMAILID='" + txtEMail.Text.Trim() + "', " +
                              "ADDRESS='" + txtAddress.Text.Trim() + "', " +
                              "CITY='" + txtCity.Text.Trim() + "', " +
                              "AvailableFrom='" + txtAvalFrom.Text.Trim() + "', " +
                              "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                              "FILENAME='" + saveFile.Trim() + "', " +

                              //"ModifiedBy='" + Session["UserName"].ToString() + "', " +
                              //"ModifiedOn=Getdate(), " +

                              "VESSELTYPES='" + Vesseltypes + "', " +
                             "ModifiedById='" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                             "ModifiedBy= '" + Session["UserName"].ToString() + "', " +
                             "ModifiedOn = GETDATE() " +
                              "WHERE CANDIDATEID=" + candidateid.ToString());
           }

           lblMessage.Text = "Record updated successfully.";
           btnSave.Visible = false;
           btnEdit.Visible = true;
           RefreshParent();

       }
       catch (Exception ex)
       {
            lblMessage.Text = "Unable to update record. System Messge : "  + ex.Message;
        }

   }
   protected void rptDel_Click(object sender, EventArgs e)
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
           DataTable dtdetails = Budget.getTable("DELETE FROM DBO.CANDIDATEDISCUSSION WHERE TABLEID=" + tableid.ToString()).Tables[0];
           ShowDetails();
       }
       catch (Exception ex)
       {
       }
   }
   protected void rptEdit_Click(object sender, EventArgs e)
   {
       LinkButton li = ((LinkButton)sender);
       int tableid = Common.CastAsInt32(li.CommandArgument);
       int CreatedBy = Common.CastAsInt32(li.CssClass);
       if (LoginId != CreatedBy && LoginId != 1)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Sorry !!\\nYour have not permission to edit this record.');", true);
           return;
       }
       DataTable dtdetails = Budget.getTable("SELECT *,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM DBO.CANDIDATEDISCUSSION WHERE TABLEID=" + tableid.ToString()).Tables[0];
       if (dtdetails != null)
           if (dtdetails.Rows.Count > 0)
           {
               SelectedDisc = tableid;
               txtConDate.Text = dtdetails.Rows[0]["DISC_DATE_STR"].ToString();
               txtCon.Text = dtdetails.Rows[0]["DISC"].ToString();
               radCommType.SelectedValue = dtdetails.Rows[0]["DISCTYPE"].ToString();
                //btnAdd.ImageUrl = "~/Modules/HRD/Images/update.gif";
            }
        ShowDetails();
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
            ShowRecord();
           //ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr2", "alert('Applicant deleted successfully.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
       }
       catch (SystemException ex)
       {
           this.lbl_info.Text = "Unable to Delete : " + ex.Message;
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
       ShowDetails();
   }
   protected void btnClear_Click(object sender, ImageClickEventArgs e)
   {
       txtConDate.Text = "";
       txtCon.Text = "";
       SelectedDisc = 0;
        //btnAdd.ImageUrl = "~/Modules/HRD/Images/add_16.gif";
    }
    public void RefreshParent()
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "ref", "window.opener.refreshPage();", true);
   }
   
   protected void btnComm_Click(object sender, EventArgs e)
   {
       btnComm.CssClass = "btn11";
       btnAtt.CssClass = "btn11";
       btnHist.CssClass = "btn11";

       pnlComm.Visible = true;
       pnlAtt.Visible = false;
       pnlHist.Visible = false;

       btnComm.CssClass = "btn11sel";
   }
   protected void btnAtt_Click(object sender, EventArgs e)
   {
       btnComm.CssClass = "btn11";
       btnAtt.CssClass = "btn11";
        btnHist.CssClass = "btn11";

        pnlComm.Visible = false;
       pnlAtt.Visible = true;
        pnlHist.Visible = false;


        btnAtt.CssClass = "btn11sel";
   }
   protected void btnHist_Click(object sender, EventArgs e)
    {
        btnComm.CssClass = "btn11";
        btnAtt.CssClass = "btn11";
        btnHist.CssClass = "btn11";

        pnlComm.Visible = false;
        pnlAtt.Visible = false;
        pnlHist.Visible = true;


        btnHist.CssClass = "btn11sel";
    }

   protected void btnAddCommAtt_OnClick(object sender, EventArgs e)
   {
       if (txtAttDesc.Text.Trim() == "")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", " alert('Please enter description.');", true);return;
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
   public void ShowAttachment()
   {
       DataTable DT = Budget.getTable("Select * from Dbo.candidatepersonaldetailsAttachments  Where CandidateID="+candidateid+"").Tables[0];
       rptCommAtt.DataSource = DT;
       rptCommAtt.DataBind();       
   }


   protected void btnDownLoadFile_OnClick(object sender, EventArgs e)
   {
       int TableID = Common.CastAsInt32(hfTableID.Value);
       string FileName = hfFileName.Value;
       DownloadAttachment(TableID, FileName);
   }
   protected void btnDeleteCommFile_OnClick(object sender, EventArgs e)
   {
       ImageButton btn=(ImageButton )sender;
       int TableID = Common.CastAsInt32(btn.CommandArgument);

       DataSet RetValue = new DataSet();
       RetValue = Budget.getTable("Delete from Dbo.candidatepersonaldetailsAttachments Where TableID=" + TableID + "");
       ShowAttachment();
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

   protected void btnSaveComments_OnClick(object sender, EventArgs e)   
   {
       if (btnSaveComments.CommandArgument == "Req")
       {
           try
           {
               Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET ReqRemarks='"+txtComments.Text.Trim().Replace("'","''")+"', STATUS=2,APPSENTBY=" + LoginId.ToString() + ",APPSENTON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
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
                ShowRecord();
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
                ShowRecord();
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
                ShowRecord();
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

   protected void btnDocCheck_Click(object sender, EventArgs e)   
   {
       dvDocCheckList.Visible = true;

       rptL.DataSource=Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (SELECT LICENSENAME FROM License L WHERE LICENSEID=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=1 AND CANDIDATEID=" + candidateid.ToString()) ; 
       rptL.DataBind();

       rptC.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select CourseName from CourseCertificate where CourseCertificateId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=2 AND CANDIDATEID=" + candidateid.ToString());
       rptC.DataBind();

       rptE.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select CargoName from DangerousCargoEndorsement where cargoId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=3 AND CANDIDATEID=" + candidateid.ToString());
       rptE.DataBind();

       rptT.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (Case when DC.DocumentNameId=0 then 'Passport' when DC.DocumentNameId=1 then 'Visa' else 'Seaman-Book' end) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=4 AND CANDIDATEID=" + candidateid.ToString());
       rptT.DataBind();

       rptM.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select MedicaldocumentName from Medicaldocuments where MedicaldocumentId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=5 AND CANDIDATEID=" + candidateid.ToString());
       rptM.DataBind();

       DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN WHERE LOGINID=CHECKEDBY) AS CHECKEDBY,CHECKEDON from DBO.Candidate_Doc_CheckList CDC WHERE CDC.CANDIDATEID=" + candidateid.ToString());
       if (DT.Rows.Count > 0)
       {
           lblMess.Text = DT.Rows[0]["CHECKEDBY"].ToString() + " / " + Common.ToDateString(DT.Rows[0]["CHECKEDON"].ToString()); 
       }
   }
   protected void btnCloseCC_OnClick(object sender, EventArgs e)   
   {
       dvDocCheckList.Visible = false;
   }
   protected void btnSaveCC_OnClick(object sender, EventArgs e)   
   {
       Repeater[] RPS = { rptL, rptC ,rptE , rptT , rptM  };

       foreach (Repeater R in RPS)
       {
           foreach (RepeaterItem ri in R.Items)
           {
               CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
               char[] c = { '|' };
               string[] ps = ch.ToolTip.Split(c);
               TextBox tx = (TextBox)ri.FindControl("txt_Rems");
               if(!(ch.Checked))
               {
                   if (tx.Text.Trim() == "")
                   {
                       lblMess.Text = "&nbsp;Remarks are manditory if not checked.";
                       tx.Focus(); 
                       return; 
                   }
               }
           }
       }

       foreach (Repeater R in RPS)
       {
           foreach (RepeaterItem ri in R.Items)
           {
               CheckBox ch = (CheckBox)ri.FindControl("ckh_L");
               char[] c = { '|' };
               string[] ps = ch.ToolTip.Split(c);
               TextBox tx = (TextBox)ri.FindControl("txt_Rems");

               if (ch.Checked)
               {
                   Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.Candidate_Doc_CheckList SET STATUS='Y',REMARK='" + tx.Text.Trim().Replace("'", "''") + "', CHECKEDBY=" + LoginId.ToString() + " ,CHECKEDON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString() + " AND DOCUMENTTYPEID=" + ps[0] + " AND DOCUMENTNAMEID=" + ps[1]);
               }
               else
               {
                   Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.Candidate_Doc_CheckList SET STATUS='N',REMARK='" + tx.Text.Trim().Replace("'", "''") + "', CHECKEDBY=" + LoginId.ToString() + " ,CHECKEDON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString() + " AND DOCUMENTTYPEID=" + ps[0] + " AND DOCUMENTNAMEID=" + ps[1]);
               }
           }
       }
       //--------------------------------
       btnReqForApprove.Enabled = true;
        divMain.Visible = true;
       lblMess.Text = "&nbsp;Record Saved successfully."; 
   }

   protected void btnViewDocCheck_Click(object sender, EventArgs e)   
   {
       ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('ViewAppCheckList.aspx?_C=" + candidateid + "');", true); 
   }

    protected void btnAddComm_Click(object sender, EventArgs e)
    {
        dvAddComm.Visible = true;
    }
    protected void btnAddAtt_Click(object sender, EventArgs e)
    {
        dvAddAttachment.Visible = true;
    }
    protected void btnClose1_OnClick(object sender, EventArgs e)
    {
        dvAddAttachment.Visible = false;
    }
    protected void btnClose2_OnClick(object sender, EventArgs e)
    {
        dvAddComm.Visible = false;
    }
    
}
