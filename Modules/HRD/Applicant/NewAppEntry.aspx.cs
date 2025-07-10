using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class NewAppEntry : System.Web.UI.Page
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

            int status = Common.CastAsInt32("0" + ViewState["STATUS"]);
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
   private void ShowRecord()
    {
        DataTable dt=Budget.getTable("select Firstname,Middlename,Lastname, " +
                        "nationalityid as Nationality, " +
                        "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB, " +
                        "rankappliedid as Rank, " +
                        "replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom, " +
                        "ContactNo,ContactNo2,EmailId,VesselTypes,Address,PassportNo,City,FileName,isnull(status,1) as status,CreatedBy,CreatedOn,AppSentBy,AppSentOn,AppBy,AppOn,RejBy,RejOn,ArchiveBy,ArchiveOn " +
                        "from dbo.candidatepersonaldetails where candidateid=" + candidateid.ToString()).Tables[0]; 
        if(dt!=null)
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
                txtPassportNo.Text = dr["PassportNo"].ToString();
                txtCity.Text = dr["City"].ToString();
                //ancCV.Visible = (dr["FileName"].ToString().Trim() != "");
                ViewState["STATUS"] = dr["STATUS"].ToString();
                //if(ancCV.Visible)
                //{
                //    ancCV.HRef="http://emanagershore.energiosmaritime.com/shipboard/Resume/" + dr["FileName"].ToString();
                //}
                foreach (ListItem li in chkVeselType.Items)
                {
                    if (dr["VesselTypes"].ToString().Contains(li.Value))
                        li.Selected = true;
                }

               ShowDetails();
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
       DataTable dtdetails = Budget.getTable("SELECT *,(SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CALLEDBY) AS USERNAME,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM DBO.CANDIDATEDISCUSSION WHERE CANDIDATEID=" + candidateid.ToString() + " order by disc_date desc").Tables[0];
       if (dtdetails != null)
       {
           rptData.DataSource = dtdetails;
           rptData.DataBind();
       }
   }

   protected void btnRApprove_Click(object sender, EventArgs e)
   {
       try
       {
           Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET STATUS=2,APPSENTBY=" + LoginId.ToString() + ",APPSENTON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
           this.lbl_info.Text = "Applciant is send for approval successfully.";
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
           Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET STATUS=3,APPBY=" + LoginId.ToString() + ",APPON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
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
           Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET STATUS=4,REJBY=" + LoginId.ToString() + ",REJON=GETDATE() WHERE CANDIDATEID=" + candidateid.ToString());
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
           DataSet ds = objtrans.transfercandidatedata(candidateid, 1);
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
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try
       {
           if (txtPassportNo.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter Passport#.";
               return;
           }
           
           //Validation for Passport Check
           string PassNo = "";
           if (candidateid <= 0)
               PassNo = "select PassportNo from DBO.CandidatePersonalDetails where PassportNo ='" + txtPassportNo.Text.Trim() + "'";
           else
               PassNo = "select PassportNo from DBO.CandidatePersonalDetails where PassportNo ='" + txtPassportNo.Text.Trim() + "' and CandidateID <>" + candidateid + "";

           DataTable dtPassNo = Budget.getTable(PassNo).Tables[0];
           if (dtPassNo.Rows.Count > 0)
           {
               lbl_info.Text = "Passport# already exists.";
               return;
           }

            //Validation for email Check

            string Email = "";
            if (candidateid <= 0)
                Email = "select EmailId from DBO.CandidatePersonalDetails where EmailId ='" + txtEMail.Text.Trim() + "'";
            else
                Email = "select EmailId from DBO.CandidatePersonalDetails where EmailId ='" + txtEMail.Text.Trim() + "' and CandidateID <>" + candidateid + "";

            DataTable dtEmail = Budget.getTable(Email).Tables[0];
            if (dtEmail.Rows.Count > 0)
            {
                lbl_info.Text = "Email address already exists.";
                return;
            }

            //Validation for contact Check

            //string ContactNo = "";
            //if (candidateid <= 0)
            //    ContactNo = "select ContactNo from DBO.CandidatePersonalDetails where ContactNo ='" + txtPhone.Text.Trim() + "'";
            //else
            //    ContactNo = "select ContactNo from DBO.CandidatePersonalDetails where ContactNo ='" + txtPhone.Text.Trim() + "' and CandidateID <>" + candidateid + "";

            //DataTable dtContactNo = Budget.getTable(ContactNo).Tables[0];
            //if (dtContactNo.Rows.Count > 0)
            //{
            //    lbl_info.Text = "ContactNo already exists.";
            //    return;
            //}
            //-----------------------------------------------------------------

            if (txtMobile.Text.Trim() == "")
           {
               lbl_info.Text = "Please enter Mobile#.";
               return; 
           }           
           //--------------------
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
           string updFileName = "", updFileExt="",updFilePath = "", saveFile = "";
           if (FileUpload1.HasFile)
           {
               updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);  
               updFileExt = System.IO.Path.GetExtension(FileUpload1.FileName);  
               saveFile= updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
               updFilePath =Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\" + saveFile);
               FileUpload1.SaveAs(updFilePath);  
           }
           //------------------------
           string STR = "";
           if (candidateid > 0)
           {
               if (updFileName.Trim() == "")
               {
                   STR = "UPDATE DBO.CandidatePersonalDetails " +
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
                       "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                       "VESSELTYPES='" + Vesseltypes + "', " +
                       "[ModifiedById] = '" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                       "[ModifiedBy] = '" + Session["UserName"].ToString() + "', " +
                       "[ModifiedOn] = GETDATE() " + 
                       "WHERE CANDIDATEID=" + candidateid.ToString() + " ; SELECT " + candidateid.ToString();
               }
               else
               {
                   STR = "UPDATE DBO.CandidatePersonalDetails " +
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
                       "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                       "VESSELTYPES='" + Vesseltypes + "', " +
                       "FILENAME='" + saveFile.Trim() + "' " +
                       "[ModifiedById] = '" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                       "[ModifiedBy] = '" + Session["UserName"].ToString() + "', " +
                       "[ModifiedOn] = GETDATE() " +
                       "WHERE CANDIDATEID=" + candidateid.ToString() + " ; SELECT " + candidateid.ToString();
               }
             
           }
           else
           {
               STR = "INSERT INTO DBO.CandidatePersonalDetails( " +
                          "[RankAppliedId] " +
                          ",[AvailableFrom] " +
                          ",[FirstName] " +
                          ",[MiddleName] " +
                          ",[LastName] " +
                          ",[Gender] " +
                          ",[DateOfBirth] " +
                          ",[NationalityId] " +
                          ",[EmailId] " +
                          ",[ContactNo] " +
                          ",[ContactNo2] " +
                          ",[CreatedBy] " +
                          ",[CreatedOn] " +
                          ",[VesselTypes] " +
                          ",[FileName] " +
                          ",[Status] " +
                          ",[Address] " +
                          ",[City] " +
                          ",[PassportNo] " +

                          ",[ModifiedById] " +
                          ",[ModifiedBy] " +
                          ",[ModifiedOn] " +

                          ") VALUES(" + ddlRank.SelectedValue + ",'" + txtAvalFrom.Text.Trim() + "','" + txtFName.Text.Trim() 
                          + "','" + txtMName.Text.Trim() + "','" + txtLName.Text.Trim()
                          + "'," + radSex.SelectedValue + ",'" + txtDOB.Text.Trim() + "'," + ddlNat.SelectedValue
                          + ",'" + txtEMail.Text.Trim() + "','" + txtPhone.Text.Trim() + "','" + txtMobile.Text.Trim() 
                          + "'," + Common.CastAsInt32(Session["loginid"]).ToString() + ",GETDATE(),'" + Vesseltypes 
                          + "','" + saveFile.Trim() + "',1,'" + txtAddress.Text.Trim() + "','" + txtCity.Text.Trim() 
                          + "','" + txtPassportNo.Text.Trim() + "'," + Common.CastAsInt32(Session["loginid"]).ToString() 
                          + ",'" +Session["UserName"].ToString() +"',GETDATE()); SELECT MAX(CANDIDATEID) FROM DBO.CandidatePersonalDetails";
           }
           DataTable dt = Budget.getTable(STR).Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    lbl_info.Text = "Applicant Created successfully.";
                }
            }

            //if(dt!=null )
            //    if (dt.Rows.Count > 0)
            //    {
            //        candidateid = Common.CastAsInt32(dt.Rows[0][0].ToString());

            //        Budget.getTable("EXEC DBO.GENEREATEAPPROVALID " + candidateid + " "); 
            //        lbl_info.Text = "Applicant Created successfully.";
            //    }
        }
        catch (Exception ex)
       {
           lbl_info.Text = "Unable to update record. System Messge : "  + ex.Message;
       }

   }
   public void RefreshParent()
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "ref", "window.opener.refreshPage();", true);
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
       catch(Exception ex) 
       {
       }
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
       DataTable dtdetails = Budget.getTable("SELECT *,REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') AS DISC_DATE_STR,replace(discussion,'''''','''') AS DISC FROM DBO.CANDIDATEDISCUSSION WHERE TABLEID=" + tableid.ToString()).Tables[0];
       if (dtdetails != null)
       if (dtdetails.Rows.Count>0)  
       {
           SelectedDisc = tableid; 
           txtConDate.Text = dtdetails.Rows[0]["DISC_DATE_STR"].ToString();
           txtCon.Text = dtdetails.Rows[0]["DISC"].ToString();
           radCommType.SelectedValue = dtdetails.Rows[0]["DISCTYPE"].ToString();
                //btnAdd.ImageUrl = "~/Modules/HRD/Images/update.gif";
            }
        ShowDetails(); 
   }
   protected void btnAdd_Click(object sender, EventArgs e)
   {
       if (candidateid <= 0)
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

       if (radCommType.SelectedValue.Trim()=="")
       {
           lbl_info.Text = "Please select one call type.";
           return;
       }

       if(SelectedDisc>0)
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
           //ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr2", "alert('Applicant deleted successfully.');window.opener.document.getElementById('btnSearch').click();window.close();", true);   
       }
       catch (SystemException ex)
       {
           this.lbl_info.Text = "Unable to Delete : " + ex.Message;
       }
   }
   protected void btnEdit_Click(object sender, EventArgs e)
   {
       btnSave.Visible = true;
   }
   protected void Page_PreRender(object sender, EventArgs e)
   {
       ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_cdpopup');", true);   
   }




   protected void btnComm_Click(object sender, EventArgs e)
   {
       btnComm.CssClass = "btn11";
       btnAtt.CssClass = "btn11";

       pnlComm.Visible = true;
       pnlAtt.Visible = false;

       btnComm.CssClass = "btn11sel";
   }
   protected void btnAtt_Click(object sender, EventArgs e)
   {
       btnComm.CssClass = "btn11";
       btnAtt.CssClass = "btn11";

       pnlComm.Visible = false;
       pnlAtt.Visible = true;


       btnAtt.CssClass = "btn11sel";
   }

   protected void btnAddCommAtt_OnClick(object sender, EventArgs e)
   {
       if (candidateid == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "ss", " alert('Please Save record first.');", true); return;
       }
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
   protected void btnDeleteCommFile_OnClick(object sender, EventArgs e)
   {
       ImageButton btn = (ImageButton)sender;
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
