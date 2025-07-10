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

public partial class ShipDocManagement : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }

    }
    public string VessCode
    {
        set { ViewState["VessCode"] = value; }
        get { return Convert.ToString(ViewState["VessCode"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        if (Session["UserType"].ToString() == "S")
            tblAddDocs.Visible = false;
        else
            tblAddDocs.Visible = true;
        
        if (Page.Request.QueryString["CJID"] != null)
            CompJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
        if (Page.Request.QueryString["VesselCode"] != null)
            VessCode = Page.Request.QueryString["VesselCode"];

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



        Common.Set_Procedures("sp_InsertUpdateVSL_VesselComponentJobMaster_Attachments");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                new MyParameter("@VesselCode", VessCode),
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
            string path = Server.MapPath("UploadFiles/" + VessCode);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            FileName = "ShipDoc_"+ CompJobId.ToString() + "_" + FileID + "." + FileType;
            fupFile.SaveAs(Server.MapPath("UploadFiles/" + VessCode + "/" + FileName));
        }

        BindRepeater();
        txtDescription.Text = "";

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int Id = Common.CastAsInt32(btn.CommandArgument);
        HiddenField hfCompJobID = (HiddenField)btn.FindControl("hfCompJobID");
            //hfFileName

        //string sql = "delete from VSL_VesselComponentJobMaster_Attachments where TableID=" + Id.ToString() + " and VesselCode='" + VessCode + "'";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        Common.Set_Procedures("sp_DeleteVSL_VesselComponentJobMaster_Attachments");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
                new MyParameter("@TableID", Id),
                new MyParameter("@VESSELCODE", VessCode),
                new MyParameter("@CompJobId", hfCompJobID.Value)
            );
        DataSet ds = new DataSet();
        Common.Execute_Procedures_IUD(ds);
        
        BindRepeater();
    }
    
    public void BindRepeater()
    {
        //string sql = "select row_number() over(order by TableId) as Sno,*,('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName from VesselComponentJobMaster_Attachments where CompJobID=" + CompJobId.ToString() + " and VesselCode='" + Session["VC"].ToString() + "' order by TableId";

        string sql = "select row_number() over(order by TableId) as Sno ,* from ( " +
         " select ('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName ,row_number() over(order by TableId) as Sno,* from VSL_VesselComponentJobMaster_Attachments " +
         " WHERE VesselCode='" + VessCode + "' and CompJobID=" + CompJobId.ToString() + "  and Status='A'" +
         " Union " +
         " select ('OfficeDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName,row_number() over(order by TableId) as Sno,'' as VesselCode, * from ComponentsJobMapping_attachments  where CompJobID=" + CompJobId.ToString() + " and Status='A'" +
         " )tbl";


        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }
}
