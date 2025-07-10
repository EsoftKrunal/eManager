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
        //if (!Page.IsPostBack)
        //{
        //    Session["ContractAddEdit"] = "1";
        //    //if (Session["Insp_Id"] != null)
        //    //{
        //    //    btnInsTravelSchedule.Visible = true;
        //    //    btnInsResponse.Visible = true;
        //    //    btnInsDocs.Visible = true;
        //    //    btnInsCloser.Visible = true;
        //    //}
        //}
        int TmpContractId = Common.CastAsInt32(Session["ContractId"]);
        if (TmpContractId > 0)
        {
            DataTable dts = Common.Execute_Procedures_Select_ByQuery("select v.VesselCode +'-'+CAST(Year(R.CreatedOn) As varchar(4))+'-'+replace(str(ISNULL(convert(int,right(ContractId,3)),0),3),' ','0') as New from RV_VesselContractDetails R Inner join Vessel v with(nolock) on R.VesselId = v.VesselId WHERE ContractId=" + TmpContractId.ToString());
            if (dts.Rows.Count > 0)
            {
                lblContractNo.Text = dts.Rows[0][0].ToString();
            }
        }
        //btnInsCloser.CssClass = "btn1";
        //btnInsDocs.CssClass = "btn1";
        //btnInsExpenses.CssClass = "btn1";
        //btnInsPlanning.CssClass = "btn1";
        //btnInsResponse.CssClass = "btn1";
        //btnInsTravelSchedule.CssClass = "btn1";
        //if (Session["ContractAddEdit"] == null)
        //{
        //    Session["ContractAddEdit"] = 1;
        //}
        //switch (Common.CastAsInt32(Session["ContractAddEdit"]))
        //{
        //    case 1:
        //        btnContractDetails.CssClass = "selbtn";
        //        break;
            
            
        //    //case 6:
        //    //    btnInsCloser.CssClass = "selbtn";
        //    //    break;
        //    default:
        //        break;
        //}
    }

    //protected void btnTabs_Click(object sender, EventArgs e)
    //{
    //    btnContractDetails.CssClass = "btn1";
        
       

    //    LinkButton btn = (LinkButton)sender;
    //    int i = Common.CastAsInt32(btn.CommandArgument);
    //    Session["ContractAddEdit"] = i;

    //    switch (i)
    //    {
    //        case 1:
    //            btnContractDetails.CssClass = "selbtn";
    //            Session["ContractAddEdit"] = 1;
    //            frm.Attributes.Add("src", "~/Modules/REVENUE/VesselContract.aspx");
    //            break;
            
           
           
    //        default:
    //            break;
    //    }
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Mode"] = null;
        Session["ContractId"] = null;
        Response.Redirect("~/Modules/REVENUE/VesselContractSearch.aspx");
    }
}