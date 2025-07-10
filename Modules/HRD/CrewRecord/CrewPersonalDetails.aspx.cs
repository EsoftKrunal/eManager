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
using ShipSoft.CrewManager.BusinessObjects;
using System.Reflection;
using System.Net;
using System.Net.Mail; 
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

public partial class CrewPersonalDetails : System.Web.UI.Page
{
    //TempService.Temp cd = new TempService.Temp();
    ShipSoftWebService cd = new ShipSoftWebService(); 
   // ShipSoftService.Service cd = new ShipSoftService.Service();
        //ShipsoftService.ShipSoftWebService cd = new ShipsoftService.ShipSoftWebService();
    
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CompareValidator4.ValueToCompare = DateTime.Now.ToString("MM/dd/yyyy");
        this.lbl_otherdoc_message.Text = "";
        if (!Page.IsPostBack)
        {

            this.dd_nationality.DataTextField = "CountryName";
            this.dd_nationality.DataValueField = "CountryId";
            this.dd_nationality.DataSource = cd.getData("selectNationality");
            this.dd_nationality.DataBind();

            this.ddl_Rank.DataTextField = "RankName";
            this.ddl_Rank.DataValueField = "RankId";
            this.ddl_Rank.DataSource = cd.getData("SelectRankAppliedId");
            this.ddl_Rank.DataBind();
 
            this.ddl_Qualification.DataTextField = "QualificationName";
            this.ddl_Qualification.DataValueField = "QualificationId";
            this.ddl_Qualification.DataSource = cd.getData("SelectQualification");
            this.ddl_Qualification.DataBind();

            this.ddl_Rank1.DataTextField = "RankName";
            this.ddl_Rank1.DataValueField = "RankId";
            this.ddl_Rank1.DataSource = cd.getData("SelectRankAppliedId");
            this.ddl_Rank1.DataBind();

            this.ddl_VesselType.DataTextField = "VesselTypeName";
            this.ddl_VesselType.DataValueField = "VesselTypeId";
            this.ddl_VesselType.DataSource = cd.getData("SelectVesselTypeName");
            this.ddl_VesselType.DataBind();

            this.ddl_Sign_Off_Reason.DataTextField = "SignOffReason";
            this.ddl_Sign_Off_Reason.DataValueField = "SignOffReasonId";
            this.ddl_Sign_Off_Reason.DataSource = cd.getData("SelectSignOffReason");
            this.ddl_Sign_Off_Reason.DataBind();

            //this.ddl_cargoname.DataTextField = "CargoName";
            //this.ddl_cargoname.DataValueField = "CargoId";
            //this.ddl_cargoname.DataSource = cd.getData("SelectCargoName");
            //this.ddl_cargoname.DataBind();

            this.ddDCEcargoname.DataTextField = "CargoName";
            this.ddDCEcargoname.DataValueField = "CargoId";
            this.ddDCEcargoname.DataSource = cd.getData("SelectCargoName");
            this.ddDCEcargoname.DataBind();

            this.ddDCEnationality.DataTextField = "CountryName";
            this.ddDCEnationality.DataValueField = "CountryId";
            this.ddDCEnationality.DataSource = cd.getData("selectNationality");
            this.ddDCEnationality.DataBind();
                     
            DataSet  ds = cd.getdatabyid("selectCandidateExperienceDetails", -1);
            DataSet dsdce = cd.getdatabyid("selectCandidateDCEDetails", -1);

            Session.Add("ce",ds);
            Session.Add("dce", dsdce); 
            if (gvexperience.Rows.Count == 0)
            {
                lblgvexp.Text = "No Record Found";
            }
            else
            {
                lblgvexp.Text = "";
            }
            trexperience.Visible = false;

            if (GvDCE.Rows.Count == 0)
            {
                lbl_GridView_cargo.Text = "No Record Found";
            }
            else
            {
                lbl_GridView_cargo.Text = "";
            }
            trdce.Visible = false;
        }
        CompareValidator12.ValueToCompare = DateTime.Now.ToString("MM/dd/yyyy");
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        DataTable dtpassport = cls_SearchReliever.SelectPassportNoforWebApplications(txt_PassportNo.Text);
        foreach (DataRow dr in dtpassport.Rows)
        {
            if(Convert.ToInt32(dr[0].ToString()) > 0)
            {
                lbl_otherdoc_message.Text="Passport No. Already Exists.";
                Duplicate = 1;
                break;
            }
        }
        if (Duplicate == 0)
        {
            try
            {
                int Id = 0;
                //-----------------------------
                int cal_age;
                DateTime date_today;
                TimeSpan t1;
                date_today = System.DateTime.Now.Date;
                t1 = date_today - Convert.ToDateTime(txtdob.Text);
                cal_age = (Convert.ToInt32(t1.TotalDays) / 365);
                if (cal_age <= 18)
                {
                    lbl_otherdoc_message.Text = "Crew Must be at least 18 Years Old.";
                    return;
                }
                //-----------------------------
                if (FileUpload1.PostedFile.ContentLength > 2097152) //--- : 2 Mb = 2097152 bytes
                {
                    lbl_otherdoc_message.Text = "Max Attachment Size is 2 Mb.";
                    return;
                }
                //-----------------------------
                int RankAppliedfor = Convert.ToInt16(this.ddl_Rank.SelectedValue);
                string availfrom = this.txt_AvailableFrom.Text;
                string strFirstName = txt_fname.Text;
                string strLastName = txt_lname.Text;
                string strMiddlename = this.txt_mname.Text;
                string strdob = this.txtdob.Text;
                string passportno = this.txt_PassportNo.Text;
                string issuedate = this.txt_IssueDt.Text;
                string expirydate = this.txt_ExpiryDt.Text;
                int nationality;
                if (this.dd_nationality.SelectedValue != "")
                {
                    nationality = Convert.ToInt16(this.dd_nationality.SelectedValue);
                }
                else
                {
                    nationality = 0;
                }
                string emailid = this.txt_emailid.Text;
                string contactno = this.txt_ccode.Text + "-" + this.txt_acode.Text + "-" + this.txt_telno.Text;
                string pob = this.txt_POB.Text;
                int quali = Convert.ToInt16(this.ddl_Qualification.SelectedValue);
                int DCE;
                //if (this.ddl_cargoname.SelectedValue != "")
                //{
                //    DCE = Convert.ToInt16(this.ddl_cargoname.SelectedValue);
                //}
                //else
                //{
                //    DCE = 0;
                //}
                int gender;
                gender = Convert.ToInt16(this.ddl_gender.SelectedValue);
                string str;
                DataSet ds = ((DataSet)Session["ce"]);
                DataSet dsdce = ((DataSet)Session["dce"]);
                str = "";
                str = cd.SavePersonalDetails("InsertCandidatePersonalDetails", RankAppliedfor, availfrom, strFirstName, strMiddlename, strLastName, strdob, emailid, nationality, contactno, pob, quali, 1, gender, ds, dsdce, passportno, issuedate, expirydate);

                clearall();
                cleardata();
                clearcargodata();
                trexperience.Visible = false;
                trdce.Visible = false;
                this.lbl_otherdoc_message.Text = str;
                //********* Code To Send Mail
                if (this.FileUpload1.HasFile)
                {
                    sendmail();
                }
                //***************



            }
            catch (Exception es)
            {

            }
        }
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        clearall();
        cleardata();
        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        Session.Add("ce", ds);
        //binddata();
        this.lbl_otherdoc_message.Text = "";
    }
    private void clearall()
    {
        this.ddl_Rank.SelectedIndex = 0;
        this.txt_AvailableFrom.Text="";
        txt_fname.Text = "";
        this.txt_lname.Text = "";
        this.txt_mname.Text="";
        this.txtdob.Text="";
        this.dd_nationality.SelectedIndex=0;
        this.txt_emailid.Text="";
        this.txt_ccode.Text = "";
        this.txt_acode.Text = "";
        this.txt_telno.Text="";
        this.txt_POB.Text="";
        this.txt_PassportNo.Text = "";
        this.txt_IssueDt.Text = "";
        this.txt_ExpiryDt.Text = "";
        this.ddl_Qualification.SelectedIndex=0;
        this.ddl_gender.SelectedIndex = 0;
        //this.ddl_cargoname.SelectedIndex=0;
        DataSet ds = cd.getdatabyid("selectCandidateExperienceDetails", -1);
        Session.Add("ce", ds);
        DataSet dsdce = cd.getdatabyid("selectCandidateDCEDetails", -1);
        Session.Add("dce", dsdce);
        binddata();
        bindcargodata();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow dr;
            DataSet ds;
            DataTable dt;
            ds = ((DataSet)Session["ce"]);
            dt = ds.Tables[0];
            if (this.hcandidateid.Value == "")
            {
                dr = dt.NewRow();
                if (dt.Rows.Count == 0)
                {
                    dr["CandidateExperienceId"] = 1;
                    ViewState.Add("canid", 1);
                }
                else
                {
                    dr["CandidateExperienceId"] = Convert.ToInt16(ViewState["canid"]);
                }
            }
            else
            {
                dr = ((DataRow)Session["drc"]);
                dr["CandidateExperienceId"] = Convert.ToInt16(this.hcandidateid.Value);
            }


            dr["CandidateId"] = 1;
            dr["CompanyName"] = this.txt_compname.Text;
            dr["RankId"] = this.ddl_Rank1.SelectedValue;
            dr["SignOn"] = this.txtfromdate.Text;
            dr["SignOff"] = this.txttodate.Text;
            dr["Duration"] = 0;
            dr["SignOffReasonId"] = this.ddl_Sign_Off_Reason.SelectedValue;
            dr["VesselName"] = this.txt_vesselname.Text;
            dr["VesselTypeId"] = this.ddl_VesselType.SelectedValue;
            dr["Registry"] = this.txtregistry.Text;
            dr["DWT"] = this.txtdwt.Text;
            dr["GWT"] = this.txtgwt.Text;
            dr["BHP"] = this.txtbhp.Text;
            dr["CreatedBy"] = 1;
            if (ddl_Rank1.SelectedIndex == 0)
            {
                dr["RankName"] ="";
            }
            else
            {
                dr["RankName"] = this.ddl_Rank1.SelectedItem.Text;
            }
            dr["VesselType"] = this.ddl_VesselType.SelectedItem.Text;
            dr["SOFReason"] = this.ddl_Sign_Off_Reason.SelectedItem.Text;
            if (dr.RowState == DataRowState.Detached)
            {
                dt.Rows.Add(dr);
            }
                        
            
            Session.Add("ce", ds);
            
            binddata();
            if (this.hcandidateid.Value == "")
            {
                if (ViewState["canid"] == null)
                {
                    ViewState.Add("canid", 2);
                }
                else
                {
                    int newcanid = 0;
                    newcanid = Convert.ToInt16(ViewState["canid"]) + 1;
                    ViewState.Add("canid", newcanid);
                }
            }
            hcandidateid.Value = "";
            cleardata();
            btn_Reset1_Click(sender, e); 
         }
        catch
        {

        }
    }
    private void binddata()
    {
        //DataTable dt;
        DataSet ds;
        ds = ((DataSet)Session["ce"]);
        this.gvexperience.DataSource = ds;
        gvexperience.DataBind();
        if (gvexperience.Rows.Count == 0)
        {
            lblgvexp.Text = "No Record Found";
        }
        else
        {
            lblgvexp.Text = "";
        }
       
        
    }
    protected void gvexperience_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt;
        DataRow dr;
        DataSet ds;
        ds = ((DataSet)Session["ce"]);
        dt = ds.Tables[0];
        dr = dt.Rows[e.RowIndex];
        dr.Delete();
        Session.Add("ce", ds);
        binddata();
    }
    private void showdata(int i)
    {
        DataTable dt;
        DataRow dr;
        DataSet ds;
        ds = ((DataSet)Session["ce"]);
        dt = ds.Tables[0];
        dr = dt.Rows[i];

        this.txt_compname.Text = dr["CompanyName"].ToString();
        this.ddl_Rank1.SelectedValue = dr["RankId"].ToString();
        this.txtfromdate.Text = dr["SignOn"].ToString();
        this.txttodate.Text = dr["SignOff"].ToString();
        this.ddl_Sign_Off_Reason.SelectedValue=dr["SignOffReasonId"].ToString();
        this.txt_vesselname.Text = dr["VesselName"].ToString();
        this.ddl_VesselType.SelectedValue = dr["VesselTypeId"].ToString();
        this.txtregistry.Text = dr["Registry"].ToString();
        this.txtdwt.Text = dr["DWT"].ToString();
        this.txtgwt.Text = dr["GWT"].ToString();
        this.txtbhp.Text = dr["BHP"].ToString();
        trexperience.Visible = true;
       Session.Add("drc", dr);
    }
    private void cleardata()
    {
        
        this.txt_compname.Text = "";
        this.ddl_Rank1.SelectedIndex  = 0;
        this.txtfromdate.Text = "";
        this.txttodate.Text = "";
        this.ddl_Sign_Off_Reason.SelectedIndex  = 0;
        this.txt_vesselname.Text = "";
        this.ddl_VesselType.SelectedIndex = 0;
        this.txtregistry.Text = "";
        this.txtdwt.Text = "";
        this.txtgwt.Text = "";
        this.txtbhp.Text = "";
    }
    protected void btn_Reset1_Click(object sender, EventArgs e)
    {
        cleardata();
        this.hcandidateid.Value = "";
        trexperience.Visible = false;
    }
    protected void gvexperience_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.hcandidateid.Value = gvexperience.DataKeys[e.NewEditIndex].Value.ToString();
        showdata(e.NewEditIndex);
        Button1.Visible = true;
        
    }
    protected void gvexperience_SelectedIndexChanged(object sender, EventArgs e)
    {
        showdata(gvexperience.SelectedIndex);
        Button1.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        trexperience.Visible = true;
        this.hcandidateid.Value = "";
        Button1.Visible = true;
        cleardata();

    }
    
    protected void  btn_cargo_save_Click(object sender, EventArgs e)
{
try
        {
            DataRow dr;
            DataSet ds;
            DataTable dt;
            ds = ((DataSet)Session["dce"]);
            dt = ds.Tables[0];
            if (this.Hidden_cargo.Value == "")
            {
                dr = dt.NewRow();
                if (dt.Rows.Count == 0)
                {
                    dr["CandidateDCEId"] = 1;
                    ViewState.Add("cancargoid", 1);
                }
                else
                {
                    dr["CandidateDCEId"] = Convert.ToInt16(ViewState["cancargoid"]);
                }
            }
            else
            {
                dr = ((DataRow)Session["drdce"]);
                dr["CandidateDCEId"] = Convert.ToInt16(this.Hidden_cargo.Value);
            }


            dr["CandidateId"] = 1;
            dr["CargoId"] = Convert.ToInt16(this.ddDCEcargoname.SelectedValue);
            dr["Number"] = this.txt_cargo_number.Text ;
            dr["NationalityId"] = Convert.ToInt16(this.ddDCEnationality.SelectedValue);
            dr["GradeLevel"] = this.txt_cargo_gradelevel.Text ;
            dr["PlaceOfIssue"] = this.txt_cargo_PlaceOfIssue.Text ;
            dr["DateOfIssue"] =this.txt_Cargo_DOI.Text;
            dr["ExpiryDate"] = this.txt_cargo_expiry.Text ;
            dr["nationality"] = this.ddDCEnationality.SelectedItem.Text ;
            dr["DCE"] = this.ddDCEcargoname.SelectedItem.Text; 
            if (dr.RowState == DataRowState.Detached)
            {
                dt.Rows.Add(dr);
            }
                        
            
            Session.Add("dce", ds);
            
            bindcargodata();
            if (this.Hidden_cargo.Value == "")
            {
                if (ViewState["cancargoid"] == null)
                {
                    ViewState.Add("cancargoid", 2);
                }
                else
                {
                    int newcanid = 0;
                    newcanid = Convert.ToInt16(ViewState["cancargoid"]) + 1;
                    ViewState.Add("cancargoid", newcanid);
                }
            }
            Hidden_cargo.Value = "";
            clearcargodata();
            btn_cargo_cancel_Click(sender, e);
         }
        catch
        {

        }
    }
    private void bindcargodata()
    {
        //DataTable dt;
        DataSet dsdce;
        dsdce = ((DataSet)Session["dce"]);
        this.GvDCE.DataSource = dsdce;
        GvDCE.DataBind();
        if (GvDCE.Rows.Count == 0)
        {
            lbl_GridView_cargo.Text = "No Record Found";
        }
        else
        {
             lbl_GridView_cargo.Text = "";
        }
       
        
    }
    private void clearcargodata()
    {
        this.ddDCEcargoname.SelectedIndex = 0;
        this.txt_cargo_number.Text = "";
        this.ddDCEnationality.SelectedIndex = 0;
        this.txt_cargo_gradelevel.Text ="";
        this.txt_cargo_PlaceOfIssue.Text="" ;
        this.txt_Cargo_DOI.Text="";
        this.txt_cargo_expiry.Text="" ;
    }
    protected void GvDCE_SelectedIndexChanged(object sender, EventArgs e)
    {
        showcargodata(GvDCE.SelectedIndex);
        btn_cargo_save.Visible = false;
    }
    protected void GvDCE_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt;
        DataRow dr;
        DataSet ds;
        ds = ((DataSet)Session["dce"]);
        dt = ds.Tables[0];
        dr = dt.Rows[e.RowIndex];
        dr.Delete();
        Session.Add("dce", ds);
        bindcargodata();
    }
    protected void GvDCE_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.Hidden_cargo.Value = GvDCE.DataKeys[e.NewEditIndex].Value.ToString();
        showcargodata(e.NewEditIndex);
        btn_cargo_save.Visible = true;
    }
    protected void GvDCE_PreRender(object sender, EventArgs e)
    {

    }
    protected void btn_AddCargo_Click(object sender, EventArgs e)
    {
        trdce.Visible = true;
        this.Hidden_cargo.Value = "";
        btn_cargo_save.Visible = true;
        clearcargodata();
    }
    private void showcargodata(int i)
    {
        DataTable dt;
        DataRow dr;
        DataSet ds;
        ds = ((DataSet)Session["dce"]);
        dt = ds.Tables[0];
        dr = dt.Rows[i];


        this.ddDCEcargoname.SelectedValue = dr["CargoId"].ToString();
        this.txt_cargo_number.Text = dr["Number"].ToString();
        this.ddDCEnationality.SelectedValue = dr["NationalityId"].ToString();
        this.txt_cargo_gradelevel.Text = dr["GradeLevel"].ToString();
        this.txt_cargo_PlaceOfIssue.Text = dr["PlaceOfIssue"].ToString();
        this.txt_Cargo_DOI.Text = dr["DateOfIssue"].ToString();
        this.txt_cargo_expiry.Text = dr["ExpiryDate"].ToString();
                trdce.Visible = true;
        Session.Add("drdce", dr);
    }
    protected void btn_cargo_cancel_Click(object sender, EventArgs e)
    {
        clearcargodata();
        this.Hidden_cargo.Value = "";
        trdce.Visible = false;
    }
    private void sendmail()
    { 
         
        try
        {
            MailMessage message=new MailMessage(); 
            SmtpClient smtpclient=new SmtpClient();
            MailAddress fromaddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString());
           // MailAddress fromaddress = new MailAddress("neeraj.d@esoftech.com", ConfigurationManager.AppSettings["FromName"]);
           // MailAddress fromaddress = new MailAddress(MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
            message.From = fromaddress;
            if (System.IO.Directory.Exists(Server.MapPath("Temp")))
            {
                System.IO.Directory.Delete(Server.MapPath("Temp"), true);
                System.IO.Directory.CreateDirectory(Server.MapPath("Temp"));
            }
            this.FileUpload1.PostedFile.SaveAs(Server.MapPath("./Temp/") + this.FileUpload1.PostedFile.FileName.ToString().Trim());
            message.To.Add(ConfigurationManager.AppSettings["ToAddress"].ToString());
            message.Subject = "Candidate Resume";
            message.Attachments.Add(new Attachment(Server.MapPath("./Temp/") + this.FileUpload1.PostedFile.FileName.ToString().Trim()));
            
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtpclient.Send(message);
            }
        catch(SystemException es)
        {
            string str= es.Message; 
        }
        
    
    }
}



