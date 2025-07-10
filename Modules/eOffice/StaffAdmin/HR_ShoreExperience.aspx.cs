using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Reflection;
using System.Globalization;

public partial class emtm_Emtm_HR_ShoreExperience : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                //---------------------------------------------
                ControlLoader.LoadControl(ddlmtmoffice, DataName.Office, "Select", "");
                ddlmtmoffice_SelectedIndexChanged(new object(), new EventArgs());
                //---------------------------------------------
                string CurrentIdName,CurrentViewMode,CurrentViewName, CurrentOfficeMode,CurrentOfficeName;
                CurrentIdName = Request.QueryString.GetKey(0);
                int CurrentId = Common.CastAsInt32(Request.QueryString[CurrentIdName].Trim());
                ViewState["CurrentExpId"] = CurrentId;

                CurrentViewMode = Request.QueryString.GetKey(1);
                CurrentViewName = Request.QueryString[CurrentViewMode].Trim();
                ViewState["CurrentPageView"] = CurrentViewName.ToString().Trim(); 
                 
                CurrentOfficeMode = Request.QueryString.GetKey(2);
                CurrentOfficeName = Request.QueryString[CurrentOfficeMode].Trim();

                if (CurrentOfficeName == "Mtm")
                {
                    rdomtmexp.Checked = true;
                    rdomtmexp_CheckedChanged(sender, e);
                    ShowMtmExpRecord(CurrentId);
                }
                else
                {
                    rdootherexp.Checked = true;
                    rdootherexp_CheckedChanged(sender, e);
                    ShowOtherExpRecord(CurrentId); 
                }
                if (CurrentId > 0)
                {
                    rdomtmexp.Visible = CurrentId <= 0;
                    rdootherexp.Visible = CurrentId <= 0;
                }
                setButtons(CurrentViewName, CurrentId);
            }
        }
    }
    #region  --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action, int Id)
    {
            string EmpMode = Session["EmpMode"].ToString();

            if (EmpMode == "View")
            {
                switch (Action)
                {
                    case "View":
                        btnMtmExpSave.Visible = false;
                        btnOtherExpSave.Visible = false;
                        break;
                    default:
                        btnMtmExpSave.Visible = false;
                        btnOtherExpSave.Visible = false;
                        break;
                }
            }
            if (EmpMode == "Edit")
            {
                switch (Action)
                {
                    case "View":
                        btnMtmExpSave.Visible = false;
                        btnOtherExpSave.Visible = false;
                        break;
                    case "Add":
                        btnMtmExpSave.Visible = true;
                        btnOhterExpCancel.Visible = true;
                        break;
                    case "Edit":
                        btnMtmExpSave.Visible = true;
                        btnOhterExpCancel.Visible = true;
                        break;
                    default:
                        btnMtmExpSave.Visible = false;
                        btnOhterExpCancel.Visible = false;
                        break;
                }
            }
    }
    public void ShowMtmExpRecord(int Id)
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_OfficeExperienceDetails WHERE MtmOfficeExpID=" + Id.ToString());
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                ddlmtmoffice.SelectedValue = dr["OfficeId"].ToString();
                ControlLoader.LoadControl(ddlmtmdesignation, DataName.Position, "Select", "0", "OfficeId=" + dr["OfficeId"].ToString());
                ddlmtmdesignation.SelectedValue = dr["Designation"].ToString();
                txtmtmfrmdate.Text = Convert.ToDateTime(dr["FromDate"]).ToString("dd-MMM-yyyy");
                try
                {
                    txtmtmtodate.Text = Convert.ToDateTime(dr["ToDate"]).ToString("dd-MMM-yyyy");
                }
                catch
                {
                    txtmtmtodate.Text = "";
                }
            }
    }
    public void ShowOtherExpRecord(int Id)
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ShoreDetails WHERE shoreId=" + Id.ToString());
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                txtOthercompany.Text = dr["Company"].ToString();
                txtOtherposition.Text = dr["Position"].ToString();
                txtOtherfrmdate.Text = Convert.ToDateTime(dr["FromDate"]).ToString("dd-MMM-yyyy");
                try
                {
                    txtOthertodate.Text = Convert.ToDateTime(dr["ToDate"]).ToString("dd-MMM-yyyy");
                }
                catch
                {
                    txtOthertodate.Text = "";
                }
                txtOtherlocation.Text = dr["location"].ToString();
            }
    }
    public void ClearMtmExpControls()
    {
        ddlmtmoffice.SelectedIndex = 0;
        ddlmtmoffice_SelectedIndexChanged(new object(), new EventArgs());  
        txtmtmfrmdate.Text = "";
        txtmtmtodate.Text = "";
    }
    public void ClearOtherExpControls()
    {
        txtOthercompany.Text = "";
        txtOtherposition.Text = "";
        txtOtherfrmdate.Text = "";
        txtOthertodate.Text = "";
        txtOtherlocation.Text = "";  
    }
    protected bool CheckDate(String date)
    {
        try
        {
            DateTime dt = DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region --- Control Events ---
    //-- OTHER EXPERIENCE
    protected void rdootherexp_CheckedChanged(object sender, EventArgs e)
    {
        ClearOtherExpControls();
        PnlMtmExp.Visible = false;    
        PnlOtherExp.Visible = true; 
    }
    protected void btnOtherExpSave_Click(object sender, EventArgs e)
    {

        DateTime date_from, date_To;
        object d2 = DBNull.Value;

        if (!(DateTime.TryParse(txtOtherfrmdate.Text, out date_from)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }
        if(txtOthertodate.Text.Trim() != "")
        {
            if (!(DateTime.TryParse(txtOthertodate.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_from > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('From Date should Not be greater than To Date.');", true);
                return;
            }
            d2=(object)date_To;
        }

        object d1 = (txtOtherfrmdate.Text.Trim() == "") ? DBNull.Value : (object)date_from;
       
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateShoreDetails");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@ShoreId", ViewState["CurrentExpId"]),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@Company", txtOthercompany.Text.Trim()),
            new MyParameter("@Position", txtOtherposition.Text.Trim()),
            new MyParameter("@FromDate", d1),
            new MyParameter("@ToDate", d2),
            new MyParameter("@Location", txtOtherlocation.Text.Trim()));

        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    //-- MTM EXPERIENCE
    protected void rdomtmexp_CheckedChanged(object sender, EventArgs e)
    {
        ClearMtmExpControls();
        PnlOtherExp.Visible = false; 
        PnlMtmExp.Visible = true;
    }
    protected void btnMtmExpSave_Click(object sender, EventArgs e)
    {
        DateTime date_from, date_To;
        object d2 = DBNull.Value;

        if (!(DateTime.TryParse(txtmtmfrmdate.Text, out date_from)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }
        if (txtmtmtodate.Text.Trim() != "")
        {
            if (!(DateTime.TryParse(txtmtmtodate.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_from > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('From Date should Not be greater than To Date.');", true);
                return;
            }
            d2 = (object)date_To;
        }

        object d1 = (txtmtmfrmdate.Text.Trim() == "") ? DBNull.Value : (object)date_from;
        
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateofficeExpDetails");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(new MyParameter("@MtmOfficeExpID", ViewState["CurrentExpId"]),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@OfficeId", ddlmtmoffice.SelectedValue.Trim()),
            new MyParameter("@Designation", ddlmtmdesignation.SelectedValue.Trim()),
            new MyParameter("@FromDate", d1),
            new MyParameter("@ToDate", d2));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            //SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            //BindGrid_MTMExp();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        }
    }
    protected void ddlmtmoffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlLoader.LoadControl(ddlmtmdesignation, DataName.Position, "Select", "", "OfficeId=" + Common.CastAsInt32(ddlmtmoffice.SelectedValue).ToString());
    }
    #endregion
}
