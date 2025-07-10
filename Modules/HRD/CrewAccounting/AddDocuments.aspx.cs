using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CrewAccounting_AddDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            ShowCrewDetails();
            ShowAttachment();
            if (Page.Request.QueryString["Mode"].ToString() == "V")
                tblAddPanel.Visible = false;
            else
                tblAddPanel.Visible = true;
        }
    }

    public void ShowCrewDetails()
    {
        string sql = "select * from CrewPortageBillHeader with(nolock) where PayrollID=" + Page.Request.QueryString["PayrollID"] .ToString()+ "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (Dt.Rows.Count > 0)
        {
            lblCrewNo.Text = Dt.Rows[0]["CrewNumber"].ToString();
            lblCrewName.Text = Dt.Rows[0]["CrewName"].ToString();
        }

    }
    public void ShowAttachment()
    {
        string sql = "select * from CrewPayrollDocuments where PayrollID=" + Page.Request.QueryString["PayrollID"].ToString() + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptDocuments.DataSource=DT ;
        rptDocuments.DataBind();
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        string FileName = "";
        if (FU.HasFile)
        {
            FileName = FU.FileName;
        }
        else
        {   lblmsg.Text = "Please select file.";return;}

        if (Common.CastAsInt32(Page.Request.QueryString["PayrollID"]) > 0)
        {

            Common.Set_Procedures("SP_INSERTINTOCrewPayrollDocuments");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                    new MyParameter("@PayrollId", Common.CastAsInt32(Page.Request.QueryString["PayrollID"])),
                    new MyParameter("@DocumentName", txtDocName.Text),
                    new MyParameter("@FileName", FileName),
                    new MyParameter("@Attachment", FU.FileBytes)
                );
            Boolean Res;
            DataSet ds = new DataSet();
            Res = Common.Execute_Procedures_IUD_CMS(ds);
            if (Res)
            {
                lblmsg.Text = "File uploaded successfully.";
                ShowAttachment();
            }
            else
            {
                lblmsg.Text = "File could not be uploaded.";
            }
        }

    }

    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfTableID = (HiddenField)btn.Parent.FindControl("hfTableID");
        string sql = "delete from  CrewPayrollDocuments where TableID=" + hfTableID .Value+ "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        lblmsg.Text="File deleted successfully.";

        ShowAttachment();
    }
    
}
