using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class ShipJobExecAttachments : System.Web.UI.Page
{
    public int AttachmentId
    {
        set { ViewState["AttachmentId"] = value; }
        get { return Common.CastAsInt32(ViewState["AttachmentId"]); }
    }
    public int ComponentId
    {
        set { ViewState["ComponentId"] = value; }
        get { return Common.CastAsInt32(ViewState["ComponentId"]); }
    }
    public int componentJobId
    {
        set { ViewState["componentJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["componentJobId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        if (Page.Request.QueryString["CJID"] != null)
            componentJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
        if (Page.Request.QueryString["ComponentId"] != null)
            ComponentId = Common.CastAsInt32(Page.Request.QueryString["ComponentId"]);

        if (Session["UserType"].ToString() == "S")
            tblAddDocs.Visible = false;
        else
            tblAddDocs.Visible = true;
        if (!Page.IsPostBack)
        {
            ShowDetails();
            BindRepeater();
        }
    }
    public void ShowDetails()
    {
        string sql = "SELECT JM.JobName AS JobCode ,CJM.DescrSh AS JobName " +
                    " from ComponentsJobMapping CJM " +
                     "INNER JOIN ComponentMaster CM ON CJM.ComponentId = CM.ComponentId " +
                     "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                     "WHERE CJM.componentId=" + ComponentId + " And  CJM.CompJobId=" + componentJobId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lbljobcode.Text = dr["JobCode"].ToString();
            lbljobdetails.Text = dr["JobName"].ToString();
        }
    }
    public void BindRepeater()
    {
        string sql = "select ROW_NUMBER() OVER(ORDER BY AttachmentId) AS srno,* from JobExecAttachmentsMaster where compjobid=" + componentJobId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        AttachmentId = Common.CastAsInt32(btn.CommandArgument);
        txtDescription.Text = ((Label)btn.Parent.FindControl("lblDetails")).Text;
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Id = Common.CastAsInt32(btn.CommandArgument);
        Common.Set_Procedures("sp_DEL_VSL_JobExecAttachmentsMaster");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@AttachmentId", Id));
        DataSet ds = new DataSet();
        Common.Execute_Procedures_IUD(ds);
        
        BindRepeater();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim() == "")
        {
            lblMSG.Text = "Please enter attachment details.";
            txtDescription.Focus(); return;
        }

        Common.Set_Procedures("sp_IU_VSL_JobExecAttachmentsMaster");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                new MyParameter("@AttachmentId", AttachmentId),
                new MyParameter("@ComponentId", ComponentId),
                new MyParameter("@CompJobId", componentJobId),
                new MyParameter("@Descr", txtDescription.Text.Replace("'", "~"))
            );
        DataSet ds = new DataSet();
        Boolean Res;
        Res = Common.Execute_Procedures_IUD(ds);
        AttachmentId = 0;
        BindRepeater();
        txtDescription.Text = "";
    }
   
}
