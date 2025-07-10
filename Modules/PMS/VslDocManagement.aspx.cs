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

public partial class VslDocManagement : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        if (Page.Request.QueryString["CJID"] != null)
            CompJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
        if (!Page.IsPostBack)
        {
            BindRepeater();
        }
    }

    protected void btnAddFiles_OnClick(object sender, EventArgs e)
    {
        String FileType = "";
        String FileName = "";
        int FileID =0;

        if (txtDescription.Text.Trim() == "")
        {
            lblMSG.Text = "Please enter description.";
            txtDescription.Focus(); return;
        }
        if (!fupFile.HasFile)
        {
            lblMSG.Text = "Please select file.";
            fupFile.Focus(); return;
        }
        else
        {
            FileType = Path.GetExtension(fupFile.FileName);
            FileType = FileType.Substring(1);
        }



        Common.Set_Procedures("sp_InsertUpdateVesselComponentJobMaster_Attachments");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                new MyParameter("@VesselCode", Session["VC"].ToString()),
                new MyParameter("@CompJobId", CompJobId),
                new MyParameter("@Descr", txtDescription.Text.Replace("'", "~")),
                new MyParameter("@DocumentType", FileType)
            );
        DataSet ds=new DataSet();
        Boolean Res;
        Res=Common.Execute_Procedures_IUD(ds);
        if (ds.Tables[0].Rows.Count > 0)
            FileID = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        if (fupFile.HasFile)
        {
            string path = Server.MapPath("UploadFiles/" + Session["VC"].ToString());
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            FileName = "ShipDoc_"+ CompJobId.ToString() + "_" + FileID + "." + FileType;
            fupFile.SaveAs(Server.MapPath("UploadFiles/" + Session["VC"].ToString() + "/" + FileName));
        }

        BindRepeater();
        txtDescription.Text = "";

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Id = Common.CastAsInt32(btn.CommandArgument);
        HiddenField hfFile = (HiddenField)btn.FindControl("hfFileName");

        string sql = "delete from VesselComponentJobMaster_Attachments where TableID=" + Id.ToString() + " and VesselCode='" + Session["VC"].ToString() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);


        String FilePath = "UploadFiles/" + Session["VC"].ToString() + "/" + hfFile.Value;
        if (File.Exists(Server.MapPath(FilePath)))
        {
            try
            {
                File.Delete(Server.MapPath(FilePath));
            }
            catch (Exception ed) { }
        }


        BindRepeater();
    }
    
    public void BindRepeater()
    {
        //string sql = "select row_number() over(order by TableId) as Sno,*,('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName from VesselComponentJobMaster_Attachments where CompJobID=" + CompJobId.ToString() + " and VesselCode='" + Session["VC"].ToString() + "' order by TableId";

        string sql = "select row_number() over(order by TableId) as Sno ,* from ( " +
         " select ('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName ,row_number() over(order by TableId) as Sno,*,'' as Status from VesselComponentJobMaster_Attachments " +
         " WHERE VesselCode='" + Session["VC"].ToString() + "' and CompJobID=" + CompJobId.ToString() + "" +
         " Union " +
         " select ('OfficeDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName,row_number() over(order by TableId) as Sno,'' as VesselCode, * from ComponentsJobMapping_attachments  where CompJobID=" + CompJobId.ToString() + " and Status='A'" +
         " )tbl";


        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }
}
