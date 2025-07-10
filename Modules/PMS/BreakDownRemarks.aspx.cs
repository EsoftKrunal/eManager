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

public partial class BreakDownRemarks : System.Web.UI.Page
{
    public string DefectNo
    {
        set { ViewState["DefectNo"] = value; }
        get { return Convert.ToString(ViewState["DefectNo"]); }

    }
    public string vesselCode
    {
        set { ViewState["vesselCode"] = value; }
        get { return Convert.ToString(ViewState["vesselCode"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        

        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["DN"] != null)
            {
                DefectNo = Page.Request.QueryString["DN"].ToString();
                vesselCode = DefectNo.Split('/').GetValue(0).ToString().Trim();
                lblDefectNo.Text = DefectNo;
                BindRepeater();
                ShowEntrySection();
            }


        }

    }

    public void ShowEntrySection()
    {
        if (Session["UserType"].ToString() == "S")
        {
            tblAddDocs.Visible = false;
        }
        else
        {
            string SQL = "SELECT CompletionDt FROM  VSL_BreakDownMaster WHERE VesselCode = '" + vesselCode.Trim() + "' AND [BreakDownNo] = '" + DefectNo + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

            //if (dt.Rows[0][0].ToString() != "")
            //{
            //    tblAddDocs.Visible = false;
            //}
            //else
            //{
                tblAddDocs.Visible = true;
            //}
        }

    }
    protected void btnAddFiles_OnClick(object sender, EventArgs e)
    {

        if (txtRemarks.Text.Trim() == "")
        {
            lblMSG.Text = "Please enter remarks.";
            txtRemarks.Focus(); 
            return;
        }

        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (fupFile.HasFile && fupFile.PostedFile != null)
        {
            HttpPostedFile File = fupFile.PostedFile;
            FileName = DefectNo.Trim().Replace("/","_").ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);
            if (File.ContentLength > 102400)
            {
                lblMSG.Text = "File is too large.Maximum 100 kb files can be uploaded.";
            }
            imgByte = new Byte[File.ContentLength];

            File.InputStream.Read(imgByte, 0, File.ContentLength);
        }

        Common.Set_Procedures("sp_InsertBreakDownRemarks");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselCode.Trim()),
                new MyParameter("@DefectRemarkId", 0),
                new MyParameter("@DefectNo", DefectNo),
                new MyParameter("@Remarks", txtRemarks.Text.Trim()),
                new MyParameter("@FileName", FileName),
                new MyParameter("@File", imgByte),
                new MyParameter("@EnteredBy", Session["FullName"].ToString())
            );
        DataSet ds = new DataSet();
        Boolean Res;
        Res = Common.Execute_Procedures_IUD(ds);
        if (Res)
        {
            BindRepeater();
            txtRemarks.Text = "";

            lblMSG.Text = "Remarks added successfully.";
        }
        else
        {
            lblMSG.Text = "Unable to add remarks.";
        }

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        //int DefectRemarkId = Common.CastAsInt32(btn.CommandArgument);
        
                
        //Common.Set_Procedures("sp_DeleteVSL_VesselComponentJobMaster_Attachments");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //        new MyParameter("@TableID", Id),
        //        new MyParameter("@VESSELCODE", VessCode),
        //        new MyParameter("@CompJobId", hfCompJobID.Value)
        //    );
        //DataSet ds = new DataSet();
        //Common.Execute_Procedures_IUD(ds);

        //BindRepeater();
    }

    public void BindRepeater()
    {
        string sql = "SELECT row_number() over(order by DefectRemarkId) as Sno , DefectRemarkId,BreakDownNo,Remarks,[FileName],(EnteredBy + '/ ' + REPLACE(CONVERT(VARCHAR(11),EnteredOn,106), ' ', '-')) AS EnteredByON  FROM VSL_BreakDownRemarks WHERE VesselCode = '" + vesselCode.Trim() + "' AND BreakDownNo = '" + DefectNo + "' ";


        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }

    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int DefectRemarkId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openattachmentwindow('" + DefectRemarkId + "', '" + vesselCode.Trim() +"');", true);

    }
}