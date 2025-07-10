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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Registers_CourseCertificate : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_CourseCertificate_Message.Text = "";
        lblcourse_certificate.Text = "";
   
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
      
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindOffCrewDropDown();
            BindOffGroupDropDown();
            BindStatusDropDown();
            BindGridcourseCertificates();
            Alerts.HidePanel(coursecertificatepanel);
            Alerts.HANDLE_AUTHORITY(1, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
        }
        
    }
    private void BindOffCrewDropDown()
    {
        DataTable dt1 = CourseCertificate.selectDataOffCrew();
        this.ddOffCrew.DataValueField = "OffCrewId";
        this.ddOffCrew.DataTextField = "OffCrewName";
        this.ddOffCrew.DataSource = dt1;
        this.ddOffCrew.DataBind();
    }
    private void BindOffGroupDropDown()
    {
        DataTable dt2 = CourseCertificate.selectDataOffGroup();
        this.ddOffGroup.DataValueField = "OffGroupId";
        this.ddOffGroup.DataTextField = "OffGroupName";
        this.ddOffGroup.DataSource = dt2;
        this.ddOffGroup.DataBind();
    }
    private void BindStatusDropDown()
    {
        DataTable dt3 = CourseCertificate.selectDataStatus();
        this.ddstatus.DataValueField = "StatusId";
        this.ddstatus.DataTextField = "StatusName";
        this.ddstatus.DataSource = dt3;
        this.ddstatus.DataBind();
    }
    private void BindGridcourseCertificates()
    {
        string s;
        s = txt_Course.Text.Trim(); 
        DataTable dt = CourseCertificate.selectDataCourseCertificateDetails(s);
        this.Gvcoursecertificate.DataSource = dt;
        this.Gvcoursecertificate.DataBind();
    }
    protected void btn_course_add_Click(object sender, EventArgs e)
    {
        Hiddencoursepk.Value = "";
        txtCourseName.Text = "";
        txtCourseType.Text = "";
        ddOffCrew.SelectedIndex = 0;
        ddOffGroup.SelectedIndex = 0;
        txtcreatedby.Text = "";
        txtcreatedon.Text = "";
        Gvcoursecertificate.SelectedIndex = -1;
        txtmodifiedby.Text = "";
        txtmodifiedon.Text = "";
        ddstatus.SelectedIndex = 0;
        Chkexpires.Checked = false;
        Chkmandatory.Checked = false;
       
        Alerts.ShowPanel(coursecertificatepanel);
        Alerts.HANDLE_AUTHORITY(2, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
       
    }
    protected void btn_course_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in Gvcoursecertificate.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddencourseName");
                hfd1 = (HiddenField)dg.FindControl("HiddencourseId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtCourseName.Text.ToUpper().Trim())
                {
                    if (Hiddencoursepk.Value.Trim() == "")
                    {
       
                        lbl_CourseCertificate_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (Hiddencoursepk.Value.Trim() != hfd1.Value.ToString())
                    {
       
                        lbl_CourseCertificate_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
       
                    lbl_CourseCertificate_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                char exp, man;
                int coursecertificateId = -1;
                int createdby = 0, modifiedby = 0;
       
                string strcoursetype = txtCourseType.Text;
                string strcourseName = txtCourseName.Text;
                char offcrew = Convert.ToChar(ddOffCrew.SelectedValue);
                char offgroup = Convert.ToChar(ddOffGroup.SelectedValue);
                char status = Convert.ToChar(ddstatus.SelectedValue);
                if (Chkexpires.Checked == true)
                {
                    exp = 'Y';
                }
                else
                {
                    exp = 'N';
                }
                if (Chkmandatory.Checked == true)
                {
                    man = 'Y';
                }
                else
                {
                    man = 'N';
                }
                if (Hiddencoursepk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    coursecertificateId = Convert.ToInt32(Hiddencoursepk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                CourseCertificate.insertUpdateCrewCertificateDetails("InsertUpdateCourseCertificatesDetails",
                                                                       coursecertificateId,
                                                                        strcoursetype,
                                                                        strcourseName,
                                                                        offcrew,
                                                                        offgroup,
                                                                        exp,
                                                                        man,
                                                                        createdby,
                                                                        modifiedby,
                                                                        status);
                BindGridcourseCertificates();
       
                Alerts.HidePanel(coursecertificatepanel);
                Alerts.HANDLE_AUTHORITY(3, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
       
                lbl_CourseCertificate_Message.Text = "Record Successfully Saved.";
            }
    }
    protected void btn_course_Cancel_Click(object sender, EventArgs e)
    {
        Gvcoursecertificate.SelectedIndex = -1;
       
        Alerts.HidePanel(coursecertificatepanel);
        Alerts.HANDLE_AUTHORITY(6, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
       
    }
    protected void btn_Print_CourseCertificate_Click(object sender, EventArgs e)
    {

    }
    protected void Gvcoursecertificate_SelectIndexChanged(object sender, EventArgs e)
    {
        
        HiddenField hfdcourse;
        hfdcourse = (HiddenField)Gvcoursecertificate.Rows[Gvcoursecertificate.SelectedIndex].FindControl("HiddencourseId");
        id =Convert.ToInt32(hfdcourse.Value.ToString());
        Show_Record_course_certificate(id);
       
        Alerts.ShowPanel(coursecertificatepanel);
        Alerts.HANDLE_AUTHORITY(4, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
    }
    protected void Show_Record_course_certificate(int courseid)
    {
       
        char a, b;
        Hiddencoursepk.Value = courseid.ToString();
        DataTable dt3 = CourseCertificate.selectDataCourseCertificateDetailsByCourseCertificateId(courseid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtCourseType.Text = dr["coursetype"].ToString();
            txtCourseName.Text = dr["coursename"].ToString();
            ddOffCrew.SelectedValue = dr["offcrew"].ToString();
            ddOffGroup.SelectedValue = dr["offgroup"].ToString();
            a = Convert.ToChar(dr["expires"].ToString());
            b = Convert.ToChar(dr["mandatory"].ToString());
            if (a == 'Y')
            {
                Chkexpires.Checked = true;
            }
            else
            {
                Chkexpires.Checked = false;
            }
            if (b == 'Y')
            {
                Chkmandatory.Checked = true;
            }
            else
            {
                Chkmandatory.Checked = false;
            }
            txtcreatedby.Text = dr["CreatedBy"].ToString();
            txtcreatedon.Text = dr["CreatedOn"].ToString();
            txtmodifiedby.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon.Text = dr["ModifiedOn"].ToString();
            ddstatus.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void Gvcoursecertificate_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdcourse;
        hfdcourse = (HiddenField)Gvcoursecertificate.Rows[e.NewEditIndex].FindControl("HiddencourseId");
        id = Convert.ToInt32(hfdcourse.Value.ToString());
        Show_Record_course_certificate(id);
        Gvcoursecertificate.SelectedIndex = e.NewEditIndex;
       
        Alerts.ShowPanel(coursecertificatepanel);
        Alerts.HANDLE_AUTHORITY(5, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
    }
    protected void Gvcoursecertificate_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)Gvcoursecertificate.Rows[e.RowIndex].FindControl("HiddencourseId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        CourseCertificate.deleteCourseCertificateDetails("deletecoursecertificate", id, modifiedby);
        BindGridcourseCertificates();
        if (Hiddencoursepk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_course_add_Click(sender, e);
        }
       
    }
    protected void Gvcoursecertificate_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(Gvcoursecertificate, Auth);
    }
    protected void Gvcoursecertificate_PreRender(object sender, EventArgs e)
    {
        if (this.Gvcoursecertificate.Rows.Count <= 0)
        {
            lblcourse_certificate.Text = "No Records Found..!";
        }
        else
        {
            lblcourse_certificate.Text = "";
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridcourseCertificates();
    }

    protected void Gvcoursecertificate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdcourse;
            hfdcourse = (HiddenField)Gvcoursecertificate.Rows[Rowindx].FindControl("hdnCourseCertificateId");
            id = Convert.ToInt32(hfdcourse.Value.ToString());
            Show_Record_course_certificate(id);
            Gvcoursecertificate.SelectedIndex = Rowindx;

            Alerts.ShowPanel(coursecertificatepanel);
            Alerts.HANDLE_AUTHORITY(5, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
        }

        }
    protected void btnEditCourseCertificate_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdcourse;
        hfdcourse = (HiddenField)Gvcoursecertificate.Rows[Rowindx].FindControl("hdnCourseCertificateId");
        id = Convert.ToInt32(hfdcourse.Value.ToString());
        Show_Record_course_certificate(id);
        Gvcoursecertificate.SelectedIndex = Rowindx;

        Alerts.ShowPanel(coursecertificatepanel);
        Alerts.HANDLE_AUTHORITY(5, btn_course_add, btn_course_save, btn_course_Cancel, btn_Print_CourseCertificate, Auth);
    }
    }
