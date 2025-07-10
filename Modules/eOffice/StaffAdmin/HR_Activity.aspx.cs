using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class emtm_StaffAdmin_Emtm_HR_Activity : System.Web.UI.Page
{
   public AuthenticationManager auth;
   # region User Functions
   protected void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql = "select replace(convert(varchar,DRC ,106),' ','-')DRC ,EmpCode,FirstName,MiddleName,FamilyName " +
                    " ,(select (FirstName+' '+LastName  ) from UserLogin UL where UL.LoginID=PD.UpdatedBy)UpdatedBy " +
                    " ,replace(convert(varchar,UpdatedOn,106),' ','-')UpdatedOn,Remarks from Hr_PersonalDetails PD where EmpID="+EmpId+"";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //select DRC,UpdatedBy,UpdatedOn,Remarks from Hr_PersonalDetails where empid="+EmpId+"
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    Session["EmpName"] = lbl_EmpName.Text.ToString();

                    txtResignedDate.Text = dr["DRC"].ToString();
                    txt_InactiveBy.Text = dr["UpdatedBy"].ToString();
                    txt_InactiveOn.Text = Common.ToDateString(dr["UpdatedOn"]);
                    txtRemarks.Text = dr["Remarks"].ToString();
                }
        }
    }
   #endregion
   # region Events
   protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 245);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(245, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);
        Session["CurrentModule"] = 1;        
        //---------------------------
        if (!IsPostBack)
        {
            btnsave.Visible = true && auth.IsUpdate;
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "0");
            ddlOffice_OnSelectedIndexChanged(sender, e);
            ShowRecord();
            ShowTransferedData();

        }
        //---------------------------
    }
   protected void btnsave_Click(object sender, EventArgs e)
   {
       if (txtResignedDate.Text.Trim() == "")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter resign date.');", true);
           return;
       }
       else
       {
           try
           {
               DateTime dt = Convert.ToDateTime(txtResignedDate.Text.Trim());
           }
           catch(Exception ex)
           {
               ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Envalid date fromat.');", true);
               return;
           }
       }

       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       Common.Set_Procedures("HR_UPdate_PersonalDetailsForResign");
       Common.Set_ParameterLength(4);
       Common.Set_Parameters
           (
               new MyParameter("@EmpId", EmpId),
               new MyParameter("@DRC", txtResignedDate.Text.Trim()),
               new MyParameter("@UpdatedBy", Convert.ToInt32(Session["loginid"].ToString())),
               new MyParameter("@Remarks", txtRemarks.Text.Trim())
           );
       DataSet ds = new DataSet();

       if (Common.Execute_Procedures_IUD_CMS(ds))
       {
           //Session["EmpMode"] = "Edit";
           //Session["EmpId"] = ds.Tables[0].Rows[0]["EmpId"];
            ShowRecord();        
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
       }
   }
   protected void btnConfirm_Save_OnClick(object sender, EventArgs e)
   {
       if (txtConfirmDate.Text.Trim() == "")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter confirmation date.');", true);
           return;
       }
       byte[] file;
       string fLNAME=""; 
       if (flpC.HasFile)
       {
           file = flpC.FileBytes;
           fLNAME=System.IO.Path.GetFileName(flpC.FileName);
       }
       else
       {
           file = new byte[0];
       }

       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       Common.Set_Procedures("AddUpdateEmployeeConfiramtion");
       Common.Set_ParameterLength(5);
       Common.Set_Parameters
           (
               new MyParameter("@EmpId", EmpId),
               new MyParameter("@ConfirmationDate", txtConfirmDate.Text.Trim()),
               new MyParameter("@ConfirmedBy", Convert.ToInt32(Session["loginid"].ToString())),
               new MyParameter("@FileName",  fLNAME),
               new MyParameter("@Attachment", file)
           );
       DataSet ds = new DataSet();

       if (Common.Execute_Procedures_IUD_CMS(ds))
       {
           ShowEmpDetails_Confirmation();
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
       }
   }
   protected void lnlAttachment_Click(object sender, EventArgs e)
   {
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       string sql = "SELECT * FROM Hr_ConfirmationDetails d inner join userlogin u on u.loginid=d.ConfirmedBy WHERE EMPID=" + EmpId + "";
       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       if (dt.Rows.Count > 0)
       {
           string filename = dt.Rows[0]["FileName"].ToString();
           byte[] res = (byte[])dt.Rows[0]["Attachment"];

           Response.Clear();
           Response.ContentType = "image/JPEG";
           Response.AddHeader("content-disposition", "attachment;filename=" + filename);
           Response.Buffer = true;
           Response.OutputStream.Write(res, 0, res.Length);
           Response.OutputStream.Flush();
           Response.End();
       }
   }
   //--------------------
   protected void btnResign_Click(object sender, EventArgs e)
   {
       btnResign.CssClass = "btn11";
       btnTranster.CssClass = "btn11";
       btnConfirm.CssClass = "btn11";
       btnPromotion.CssClass = "btn11";
       btnReJoin.CssClass = "btn11";

       pnlResign.Visible = true;
       pnlTranster.Visible = false;
       pnlConfirmation.Visible = false;
       pnlPromotiom.Visible = false;
       pnlRejoin.Visible = false; 

       btnResign.CssClass = "btn11sel";
   }
   protected void btnTranster_Click(object sender, EventArgs e)
   {
       btnResign.CssClass = "btn11";
       btnTranster.CssClass = "btn11";
       btnConfirm.CssClass = "btn11";
       btnPromotion.CssClass = "btn11";
       btnReJoin.CssClass = "btn11";

       pnlResign.Visible = false;
       pnlTranster.Visible = true;
       pnlConfirmation.Visible = false;
       pnlPromotiom.Visible = false;
       pnlRejoin.Visible = false;

       btnTranster.CssClass = "btn11sel";
       ShowEmpDetails_Transter();
   }
   protected void btnConfirmation_Click(object sender, EventArgs e)
   {
       btnResign.CssClass = "btn11";
       btnTranster.CssClass = "btn11";
       btnConfirm.CssClass = "btn11";
       btnPromotion.CssClass = "btn11";
       btnReJoin.CssClass = "btn11";

       pnlResign.Visible = false;
       pnlTranster.Visible = false;
       pnlConfirmation.Visible = true;
       pnlPromotiom.Visible = false;
       pnlRejoin.Visible = false;

       btnConfirm.CssClass = "btn11sel";
       ShowEmpDetails_Confirmation();
   }
   protected void btnPromotion_Click(object sender, EventArgs e)
   {
       btnResign.CssClass = "btn11";
       btnTranster.CssClass = "btn11";
       btnConfirm.CssClass = "btn11";
       btnPromotion.CssClass = "btn11";
       btnReJoin.CssClass = "btn11";

       pnlResign.Visible = false;
       pnlTranster.Visible = false;
       pnlConfirmation.Visible = false;
       pnlPromotiom.Visible = true;
       pnlRejoin.Visible = false;

       btnPromotion.CssClass = "btn11sel";
       ShowEmpDetails_Promotion();
   }
   protected void btnReJoin_Click(object sender, EventArgs e)
   {
       btnResign.CssClass = "btn11";
       btnTranster.CssClass = "btn11";
       btnConfirm.CssClass = "btn11";
       btnPromotion.CssClass = "btn11";
       btnReJoin.CssClass = "btn11";

       pnlResign.Visible = false;
       pnlTranster.Visible = false;
       pnlConfirmation.Visible = false;
       pnlPromotiom.Visible = false;
       pnlRejoin.Visible = true;

       btnReJoin.CssClass = "btn11sel";
       ShowEmpDetails_Rejoin();
   }
   protected void btnSaveTransfer_OnClick(object sender, EventArgs e)
   {
       if (ddlOffice.SelectedIndex == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please select Office.');", true);
           ddlOffice.Focus();           return;
       }
       if (ddlDepartment.SelectedIndex == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please select Department.');", true);
           ddlDepartment.Focus(); return;
       }
       if (ddlPosition.SelectedIndex == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please select Position.');", true);
           ddlPosition.Focus(); return;
       }
       if (txtTansferDate.Text.Trim()=="")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Transter Date.');", true);
           txtTansferDate.Focus(); return;
       }
       if (txtTransferComments.Text.Trim() == "")
       {
           txtTransferComments.Focus();
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Comments.');", true);
            return;
       }



       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       Common.Set_Procedures("HR_TranferEmployee");
       Common.Set_ParameterLength(6);
       Common.Set_Parameters
           (
               new MyParameter("@EmpId", EmpId),
               new MyParameter("@ToOffice", ddlOffice.SelectedValue),
               new MyParameter("@ToDept", ddlDepartment.SelectedValue),
               new MyParameter("@ToPosition",ddlPosition.SelectedValue ),
               new MyParameter("@TransferDate", txtTansferDate.Text.Trim()),
               new MyParameter("@TransferBy", Session["UserName"].ToString())
               
           );
       DataSet ds = new DataSet();
       if (Common.Execute_Procedures_IUD_CMS(ds))
       {
           ShowTransferedData();
           ClearTransterControls();
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
       }
   }

   protected void btnSavePromotion_OnClick(object sender, EventArgs e)
   {   
       if (txtPromotionDate.Text.Trim()=="")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Promotion Date.');", true);
           txtPromotionDate.Focus(); return;
       }
       if (ddlNewPosition.SelectedIndex == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please select New Position.');", true);
           ddlNewPosition.Focus(); return;
       }
    
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       Common.Set_Procedures("HR_PromoteEmployee");
       Common.Set_ParameterLength(6);
       Common.Set_Parameters
           (
               new MyParameter("@EmpId", EmpId),
               new MyParameter("@FromOffice", Common.CastAsInt32(ViewState["OfficeId"])),
               new MyParameter("@FromPosition", Common.CastAsInt32(ViewState["PositionId"])),
               new MyParameter("@ToPosition",ddlNewPosition.SelectedValue ),
               new MyParameter("@PromotionDate", txtPromotionDate.Text.Trim()),
               new MyParameter("@PromotedBy", Session["UserName"].ToString())
           );
       DataSet ds = new DataSet();
       if (Common.Execute_Procedures_IUD_CMS(ds))
       {
           ShowEmpDetails_Promotion();
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
       }
   }
   protected void btnSaveRejoin_OnClick(object sender, EventArgs e)
   {   
       if (txtRejoinDate.Text.Trim()=="")
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Rejoin Date.');", true);
           txtRejoinDate.Focus(); return;
       }
       if (ddlNewPosition1.SelectedIndex == 0)
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please select New Position.');", true);
           ddlNewPosition1.Focus(); return;
       }
    
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       Common.Set_Procedures("HR_RejoinEmployee");
       Common.Set_ParameterLength(6);
       Common.Set_Parameters
           (
               new MyParameter("@EmpId", EmpId),
               new MyParameter("@FromOffice", Common.CastAsInt32(ViewState["OfficeId"])),
               new MyParameter("@FromPosition", Common.CastAsInt32(ViewState["PositionId"])),
               new MyParameter("@ToPosition",ddlNewPosition1.SelectedValue ),
               new MyParameter("@RejoinDate", txtRejoinDate.Text.Trim()),
               new MyParameter("@RejoinBy", Session["UserName"].ToString())
           );
       DataSet ds = new DataSet();
       if (Common.Execute_Procedures_IUD_CMS(ds))
       {
           ShowEmpDetails_Rejoin();
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
       }
       else
       {
           ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
       }
   }
    
   public void ShowTransferedData()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        string sql =" SELECT TRANSFERDATE, "+
                    " (SELECT OFFICENAME FROM OFFICE WHERE OFFICEID=fromoffice) AS FROMOFFICE, "+
                    " (SELECT DEPTNAME FROM EMTM_DEPARTMENT WHERE DEPTID=FROMDEPT) AS FROMDEPT, "+
                    " (SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=FROMPOSITION) AS FROMPOSITION, "+
                    " (SELECT OFFICENAME FROM OFFICE WHERE OFFICEID=TOoffice)  AS TOOFFICE, "+
                    " (SELECT DEPTNAME FROM EMTM_DEPARTMENT WHERE DEPTID=TODEPT) AS TODEPT, "+
                    " (SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=TOPOSITION) AS TOPOSITION,TRANSFERBY " +
                    " FROM HR_EmployeeTransfers WHERE EMPID=" + EmpId + " ORDER BY TRANSFERDATE";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        
        rptTransferedData.DataSource = dt;
        rptTransferedData.DataBind();
    }
   public void ShowEmpDetails_Transter()
   {
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       string sql = "SELECT (SELECT OFFICENAME FROM OFFICE WHERE OFFICEID=OFFICE) AS FROMOFFICE,(SELECT DEPTNAME FROM EMTM_DEPARTMENT WHERE DEPTID=DEPARTMENT) AS FROMDEPT,(SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=POSITION) AS POSITION FROM Hr_PersonalDetails WHERE EMPID=" + EmpId + "";
       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       if (dt != null)
       {
           lblOffice.Text = dt.Rows[0]["FROMOFFICE"].ToString();
           lblDepartment.Text = dt.Rows[0]["FROMDEPT"].ToString();
           lblPosition.Text = dt.Rows[0]["POSITION"].ToString();
       }
   }
   public void ShowEmpDetails_Confirmation()
   {
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       string sql = "SELECT * FROM Hr_ConfirmationDetails d inner join userlogin u on u.loginid=d.ConfirmedBy WHERE EMPID=" + EmpId + "";
       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       lnlAttachment.Visible = false; 
       if (dt != null )
           if (dt.Rows.Count > 0)
           {
               lblCUpdatedOn.Text = dt.Rows[0]["UserId"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["ConfirmedoN"]);
               txtConfirmDate.Text = Common.ToDateString(dt.Rows[0]["ConfirmationDate"]);
               if (Convert.IsDBNull(dt.Rows[0]["Attachment"]))
               {
                   lnlAttachment.Visible = false;
               }
               else
               {
                   byte[] res = (byte[])dt.Rows[0]["Attachment"];
                   if (res.Length > 0)
                       lnlAttachment.Visible = true;
                   else
                       lnlAttachment.Visible = false;
               }
           }
           else
           {
               lblCUpdatedOn.Text = "";
               txtConfirmDate.Text = "";
           }

       btnConfirm_Save.Visible = false;
       sql = "SELECT status from Hr_PersonalDetails WHERE EMPID=" + EmpId + "";
       dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       if (dt.Rows.Count > 0)
       {
           if (dt.Rows[0][0].ToString().Trim() == "R")
           {
               btnConfirm_Save.Visible = true;
           }
       }
    
   }

   public void ShowEmpDetails_Promotion()
   {
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       string sql = "SELECT Office,POSITION,(SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=POSITION) AS POSITIONName,Status FROM Hr_PersonalDetails WHERE EMPID=" + EmpId + "";
       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       btnSavePromotion.Visible = false; 
       if (dt != null)
       {
           lblCurrPosition.Text = dt.Rows[0]["POSITIONName"].ToString();
           ControlLoader.LoadControl(ddlNewPosition, DataName.Position, "Select", "0", "officeid=" + dt.Rows[0]["Office"].ToString());
           ViewState["OfficeId"] = dt.Rows[0]["Office"].ToString();
           ViewState["PositionId"] = dt.Rows[0]["Position"].ToString();
           btnSavePromotion.Visible = dt.Rows[0]["Status"].ToString().Trim() != "I"; 
       }

       sql="select EMPID,PROMOTIONDATE,p1.PositionName as FromPosition,p2.PositionName as ToPosition,PromotedBy from " +
           "HR_EmployeePromotion P " +
           "inner join pOSITION p1 ON p1.PositionId=P.FromPosition " +
           "inner join pOSITION p2 ON p2.PositionId=P.ToPosition " +
           "where EmpId=" + EmpId.ToString();
       dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       rptPromotion.DataSource = dt;
       rptPromotion.DataBind();
   }
   public void ShowEmpDetails_Rejoin()
   {
       int EmpId = Common.CastAsInt32(Session["EmpId"]);
       string sql = "SELECT Office,POSITION,(SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=POSITION) AS POSITIONName,Status FROM Hr_PersonalDetails WHERE EMPID=" + EmpId + "";
       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       btnSaveRejoin.Visible = false; 
       if (dt != null)
       {
           lblLastPosition.Text = dt.Rows[0]["POSITIONName"].ToString();
           ControlLoader.LoadControl(ddlNewPosition1, DataName.Position, "Select", "0", "officeid=" + dt.Rows[0]["Office"].ToString());
           ViewState["OfficeId"] = dt.Rows[0]["Office"].ToString();
           ViewState["PositionId"] = dt.Rows[0]["Position"].ToString();
           btnSaveRejoin.Visible = dt.Rows[0]["Status"].ToString().Trim()=="I"; 
       }

       sql = "select EMPID,REJOINDATE,p1.PositionName as LastPosition,p2.PositionName as ToPosition,RejoinBy from " +
           "HR_EmployeeRejoin P " +
           "inner join pOSITION p1 ON p1.PositionId=P.FromPosition " +
           "inner join pOSITION p2 ON p2.PositionId=P.ToPosition " +
           "where EmpId=" + EmpId.ToString();
       dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
       rptRejoin.DataSource = dt;
       rptRejoin.DataBind();

       

   }

   public void ClearTransterControls()
   {
       ddlOffice.SelectedIndex = 0;
       ddlDepartment.SelectedIndex = 0;
       ddlPosition.SelectedIndex = 0;
       txtTansferDate.Text = "";
       txtTransferComments.Text = "";
   }
   protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
   {
       ControlLoader.LoadControl(ddlDepartment, DataName.HR_Department, "Select", "0", "OfficeId=" + ddlOffice.SelectedValue);
       ControlLoader.LoadControl(ddlPosition, DataName.Position, "Select", "0", "officeid=" + ddlOffice.SelectedValue);
   }
    //protected void btnsave_Click(object sender, EventArgs e)
    //{
    //    //DateTime date_DOB, date_DJC;
    //    //int cal_age;
    //    //DateTime date_today;
    //    //TimeSpan t1;
    //    //date_today = System.DateTime.Now.Date;
    //    //t1 = date_today - Convert.ToDateTime(txt_DOB.Text);
    //    //cal_age = (Convert.ToInt32(t1.TotalDays) / 365);
    //    //if (cal_age <= 18)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Employee Must be at least 18 Years Old.');", true);
    //    //    return;
    //    //}

    //    //date_DOB = Convert.ToDateTime(txt_DOB.Text);
    //    //date_DJC = Convert.ToDateTime(txtdatefirstjoin.Text);


    //    //TimeSpan ts = date_DOB - date_DJC;

    //    //int days = ts.Days;


    //    //if (days > 0)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('DJC can not be less than DOB');", true);
    //    //    return;
    //    //}
    //    //if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
    //    //{
    //    //    HttpPostedFile file = FileUpload1.PostedFile;

    //    //    string ext = Path.GetExtension(FileUpload1.FileName);
    //    //    if (ext == ".jpg")
    //    //    { }
    //    //    else
    //    //    {
    //    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "zz", "alert('Uploading file type should be jpg only.');", true);
    //    //        return;
    //    //    }

    //    //}


    //    //int EmpId = Common.CastAsInt32(Session["EmpId"]);
    //    //Common.Set_Procedures("HR_InsertUpdatePersonalDetails");
    //    //Common.Set_ParameterLength(21);
    //    //Common.Set_Parameters(new MyParameter("@EmpId", EmpId),
    //    //    new MyParameter("@FirstName", txt_FirstName.Text.Trim()),
    //    //    new MyParameter("@MiddleName", txt_Middlename.Text.Trim()),
    //    //    new MyParameter("@FamilyName", txt_familyName.Text.Trim()),
    //    //    new MyParameter("@PassportNo", txt_Passport.Text.Trim()),
    //    //    new MyParameter("@DateOfBirth", txt_DOB.Text.Trim()),
    //    //    new MyParameter("@Age", txt_Age.Text.Trim()),
    //    //    new MyParameter("@POB", txt_placeofbirth.Text.Trim()),
    //    //    new MyParameter("@COB", ddlcob.SelectedValue.Trim()),
    //    //    new MyParameter("@Nationality", ddlnationality.SelectedValue.Trim()),
    //    //    new MyParameter("@Gender", ddlgender.SelectedValue.Trim()),
    //    //    new MyParameter("@CivilStatus", ddlcivilstatus.SelectedValue.Trim()),
    //    //    new MyParameter("@BloodGroup", ddlbloodgroup.SelectedValue.Trim()),
    //    //    new MyParameter("@Height", txtheight.Text.Trim()),
    //    //    new MyParameter("@Weight", txtweight.Text.Trim()),
    //    //    new MyParameter("@BMI", txt_Bmi.Text.Trim()),
    //    //    new MyParameter("@DJC ", txtdatefirstjoin.Text.Trim()),
    //    //    new MyParameter("@Position ", ddlposition.Text.Trim()),
    //    //    new MyParameter("@Office", ddloffice.SelectedValue.Trim()),
    //    //    new MyParameter("@NRIC_FIN ", txt_nricfin.Text.Trim()),
    //    //    new MyParameter("@Department ", ddldepartment.SelectedValue.Trim()));
    //    //DataSet ds = new DataSet();

    //    //if (Common.Execute_Procedures_IUD_CMS(ds))
    //    //{
    //    //    Session["EmpMode"] = "Edit";
    //    //    Session["EmpId"] = ds.Tables[0].Rows[0]["EmpId"];
    //    //    lbl_EmpName.Text = "[ " + ds.Tables[0].Rows[0]["EmpCode"].ToString() + " ] " + txt_FirstName.Text.Trim() + " " + txt_Middlename.Text.Trim() + " " + txt_familyName.Text.Trim();

    //    //    if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
    //    //    {
    //    //        HttpPostedFile file = FileUpload1.PostedFile;
    //    //        FileUpload1.SaveAs(Server.MapPath("~/emtm/EmpImage/Emtm_" + EmpId.ToString() + ".jpg"));
    //    //        ShowRecord();
    //    //    }

    //    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
    //    //}
    //    //else
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
    //    //}
    //}
    #endregion
}
