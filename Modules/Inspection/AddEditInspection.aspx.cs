using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Inspection_AddEditInspection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            Session["InspectionAddEdit"] = "1";
            //if (Session["Insp_Id"] != null)
            //{
            //    btnInsTravelSchedule.Visible = true;
            //    btnInsResponse.Visible = true;
            //    btnInsDocs.Visible = true;
            //    btnInsCloser.Visible = true;
            //}
        }
        int TmpInspid = Common.CastAsInt32(Session["Insp_Id"]);
        if (TmpInspid > 0)
        {
            DataTable dts = Common.Execute_Procedures_Select_ByQuery("SELECT INSPECTIONNO FROM T_INSPECTIONDUE WHERE ID=" + TmpInspid.ToString());
            if (dts.Rows.Count > 0)
            {
                lblInspectionNo.Text = dts.Rows[0][0].ToString();
            }
        }
        btnInsCloser.CssClass = "btn1";
        btnInsDocs.CssClass = "btn1";
        btnInsExpenses.CssClass = "btn1";
        btnInsPlanning.CssClass = "btn1";
        btnInsResponse.CssClass = "btn1";
        btnInsTravelSchedule.CssClass = "btn1";
        if (Session["InspectionAddEdit"] == null)
        {
            Session["InspectionAddEdit"] = 1;
        }
        switch (Common.CastAsInt32(Session["InspectionAddEdit"]))
        {
            case 1:
                btnInsPlanning.CssClass = "selbtn";
                break;
            case 2:
                btnInsTravelSchedule.CssClass = "selbtn";
                break;
            case 3:
                btnInsResponse.CssClass = "selbtn";
                break;
            case 4:
                btnInsDocs.CssClass = "selbtn";
                break;
            case 5:
                btnInsExpenses.CssClass = "selbtn";
                break;
            case 6:
                btnInsCloser.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }

    protected void btnTabs_Click(object sender, EventArgs e)
    {
        btnInsCloser.CssClass = "btn1";
        btnInsDocs.CssClass = "btn1";
        btnInsExpenses.CssClass = "btn1";
        btnInsPlanning.CssClass = "btn1";
        btnInsResponse.CssClass = "btn1";
        btnInsTravelSchedule.CssClass = "btn1";

        LinkButton btn = (LinkButton)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["InspectionAddEdit"] = i;

        switch (i)
        {
            case 1:
                btnInsPlanning.CssClass = "selbtn";
                Session["InspectionAddEdit"] = 1;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                break;
            case 2:
                if (Session["Insp_Id"] != null)
                {
                    btnInsTravelSchedule.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 2;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionTravelSchedule.aspx");
                }
                else
                {
                    btnInsPlanning.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 1;
                    
                   
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please fill Inspection Planning details and save  first.');", true);
                }
                break;
            case 3:
                if (Session["Insp_Id"] != null)
                {
                    btnInsResponse.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 3;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionResponse.aspx");
                   
                }
                else
                {
                    btnInsPlanning.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 1;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please fill Inspection Planning details and save  first.');", true);
                }
                break;
            case 4:
                if (Session["Insp_Id"] != null)
                {
                    btnInsDocs.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 4;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionDocuments.aspx");
                }
                else
                {
                    btnInsPlanning.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 1;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please fill Inspection Planning details and save  first.');", true);
                }
                break;
            case 5:
                btnInsExpenses.CssClass = "selbtn";
                Session["InspectionAddEdit"] = 5;
                frm.Attributes.Add("src", "~/Modules/Inspection/InspectionExpenses.aspx");
                break;
            case 6: 
                if (Session["Insp_Id"] != null)
                {
                    btnInsCloser.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 6;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionCloser.aspx");
                }
                else
                {
                    btnInsPlanning.CssClass = "selbtn";
                    Session["InspectionAddEdit"] = 1;
                    frm.Attributes.Add("src", "~/Modules/Inspection/InspectionPlanning.aspx");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please fill Inspection Planning details and save  first.');", true);
                }
                break;
            default:
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Mode"] = null;
        Session["Insp_Id"] = null;
        Response.Redirect("~/Modules/Inspection/InspectionSearch.aspx");
    }
}