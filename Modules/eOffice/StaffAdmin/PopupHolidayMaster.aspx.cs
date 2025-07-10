using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_PopupHolidayMaster : System.Web.UI.Page
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
                string HolidayIdName,PageMode,PageName,OfficeName,YearName;
                HolidayIdName = Request.QueryString.GetKey(0);
                int HolidayId = Common.CastAsInt32(Request.QueryString[HolidayIdName].Trim());
                Session.Add("PopUpHolidayId", HolidayId);

                PageMode = Request.QueryString.GetKey(1);
                PageName = Request.QueryString[PageMode].Trim();
                Session["PopUpPageView"] = PageName.ToString().Trim();

                if (Session["PopUpPageView"].ToString() == "Add")
                {
                    OfficeName = Request.QueryString.GetKey(2);
                    int OfficeId = Common.CastAsInt32(Request.QueryString[OfficeName].Trim());
                    hdnOfficeId.Value = OfficeId.ToString();

                    YearName = Request.QueryString.GetKey(3);
                    int Year = Common.CastAsInt32(Request.QueryString[YearName].Trim());
                    hdnYear.Value = Year.ToString();
                }

                ShowRecord(HolidayId);

                setButtons(Session["PopUpPageView"].ToString());
            }
        }
    }

    # region --- User Defined Functions ---
    public void ShowRecord(int Id)
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select HolidayId,OfficeId, [Year],replace(convert(varchar,Holidayfrom,106),' ','-') as FromDate,replace(convert(varchar,HolidayTo,106),' ','-') as ToDate, HolidayReason from HR_HolidayMaster where HolidayId =" + Id);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                txtFromDate.Text = Convert.ToDateTime(dr["FromDate"]).ToString("dd-MMM-yyyy").Trim();
                txtToDate.Text = Convert.ToDateTime(dr["ToDate"]).ToString("dd-MMM-yyyy").Trim();
                txtReason.Text = dr["HolidayReason"].ToString();
                hdnOfficeId.Value = dr["OfficeId"].ToString().Trim();
                hdnYear.Value = dr["Year"].ToString().Trim(); 
            }
    }
    private void ClearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtReason.Text = "";
    }
    protected void setButtons(string Action)
    {
        switch (Action)
        {
            case "View":
                //tblview.Visible = true;
                
                btnsave.Visible = false;
                btnCancel.Visible = true;
                break;
            case "Add":
                //tblview.Visible = true;
                
                btnsave.Visible = true;
                btnCancel.Visible = true;
                break;
            case "Edit":
                //tblview.Visible = true;
                
                btnsave.Visible = true;
                btnCancel.Visible = true;
                break;
            default:
                //tblview.Visible = false;
                
                btnsave.Visible = false;
                btnCancel.Visible = false;
                break;
        }
    }
    #endregion

    # region --- Controls Events ---
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DateTime date_From, date_To;
        if (!(DateTime.TryParse(txtFromDate.Text, out date_From)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }

        if (txtToDate.Text != "")
        {
            if (!(DateTime.TryParse(txtToDate.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_From > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('From Date should Not be greater than To Date.');", true);
                return;
            }
        }

        object d1 = (txtFromDate.Text.Trim() == "") ? DBNull.Value : (object)txtFromDate.Text;
        object d2 = (txtToDate.Text.Trim() == "") ? DBNull.Value : (object)txtToDate.Text;

        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateHolidayMaster");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(new MyParameter("@HolidayId", Session["PopUpHolidayId"]),
            new MyParameter("@OfficeId", hdnOfficeId.Value.Trim()),
            new MyParameter("@Year", hdnYear.Value.Trim()),
            new MyParameter("@HolidayFrom", d1),
            new MyParameter("@HolidayTo", d2),
            new MyParameter("@HolidayReason", txtReason.Text.Trim()));

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Holiday Already Exist.');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Holiday Already Exist');", true);
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
    }
    #endregion
}
