using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_InvoiceManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            BindOffice();
            bindData();
        }
    }
    public void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT OfficeId,OfficeName  FROM [dbo].[Office] order by OfficeName");

        ddlOffice.DataSource = dt;
        ddlOffice.DataValueField = "OfficeId";
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataBind();

        ddlOffice.Items.Insert(0, new ListItem("< All >", ""));

    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPosition.Items.Clear();
        if (ddlOffice.SelectedIndex > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [PositionId],[PositionName] FROM [dbo].[Position] WHERE [OfficeId] = " + ddlOffice.SelectedValue.Trim());

            ddlPosition.DataSource = dt;
            ddlPosition.DataValueField = "PositionId";
            ddlPosition.DataTextField = "PositionName";
            ddlPosition.DataBind();

            ddlPosition.Items.Insert(0, new ListItem("< All >", ""));
        }
        else
        {
            ddlPosition.Items.Insert(0, new ListItem("< All >", ""));
        }

        bindData();

    }
    protected void ddlPosition_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void bindData()
    {
        string Where = "";
        if (ddlOffice.SelectedIndex > 0 )
        {
            Where = " AND E.[Office]=" + ddlOffice.SelectedValue.Trim();  
        }
        if (ddlPosition.SelectedIndex > 0)
        {
            Where = " AND E.[position]=" + ddlPosition.SelectedValue.Trim();
        }
        string sql = "select LoginId,um.FirstName , um.LastName , m.* from dbo.usermaster um with(nolock) " +
                     "inner join [dbo].[Office] f with(nolock) on f.[OfficeId] = um.[OfficeId] " +
                     "inner join [dbo].[Position] p with(nolock) on p.[positionId] = um.[positionId] " +
                     "left join pos_invoice_mgmt m with(nolock) on m.UserId=um.LoginId " +
                     "where um.statusiD='A'  " + Where + 
                     "order by firstname,lastname ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptUsers.DataSource = dt;
        rptUsers.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            foreach(RepeaterItem ri in rptUsers.Items)
            {
                int UserId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdUserId")).Value);
                int v1 = ((CheckBox)ri.FindControl("chk_Entry")).Checked?1:0;
                int v2 = ((CheckBox)ri.FindControl("chk_App")).Checked ? 1 : 0;
                int v3 = ((CheckBox)ri.FindControl("chk_Verify")).Checked ? 1 : 0;
               
                int v4 = ((CheckBox)ri.FindControl("chkPayment")).Checked ? 1 : 0;
                int v5 = ((CheckBox)ri.FindControl("chkCancel")).Checked ? 1 : 0;
                int v6 = ((CheckBox)ri.FindControl("chk_App3")).Checked ? 1 : 0;
                int v7 = ((CheckBox)ri.FindControl("chk_App4")).Checked ? 1 : 0;

                int VA1 = ((CheckBox)ri.FindControl("chkVendorApp1")).Checked ? 1 : 0;
                int VA2 = ((CheckBox)ri.FindControl("chkVendorApp2")).Checked ? 1 : 0;
                Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Inv_ManageAccessRights " + UserId + "," + v1 + "," + v2 + "," + v3 + "," + v4 + "," + v5 + "," + v6 + "," + v7 + "," + VA1 + "," + VA2);
            }
            lbl_inv_Message.Text = "Saved Successfully.";
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
        bindData();
    }

}