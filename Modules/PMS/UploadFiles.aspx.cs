using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFiles : System.Web.UI.Page
{
    public int DocketId
    {
        set { ViewState["DocketId"] = value; }
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
    }
    public int CatId
    {
        set { ViewState["CatId"] = value; }
        get { return Common.CastAsInt32(ViewState["CatId"]); }
    }
    //public string SNO 
    //{
    //    set { ViewState["SNO"] = value; }
    //    get { return ViewState["SNO"].ToString(); }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            CatId = Common.CastAsInt32(Request.QueryString["CatId"]);
            //SNO=Request.QueryString["SNO"];
        }
    }
    protected void ShowFiles()
    {
        string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Temp"));
        rptTempFiles.DataSource = Files;
        rptTempFiles.DataBind();
    }
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        ShowFiles();    
    }
    protected void btnDeleteAll_OnClick(object sender, EventArgs e)
    {
        string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Temp"));
        foreach (string fl in Files)
        {
            System.IO.File.Delete(fl);
        }
        ShowFiles();
    }
    protected void DeleteImage(object sender, EventArgs e)
    {
        string Fl = ((ImageButton)sender).CommandArgument;
        System.IO.File.Delete( Server.MapPath("~\\Temp\\" + Fl));
        ShowFiles();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        foreach (DataListItem di in rptTempFiles.Items)
        {
            string Caption = ((TextBox)(di.FindControl("txtCaption"))).Text.Trim().Replace("'", "`");
            string FileName = ((ImageButton)(di.FindControl("btnDelImg"))).CommandArgument;
            FileName = Path.GetFileName(FileName);
            File.Move(Server.MapPath("~\\Temp\\" + FileName), Server.MapPath("~\\DryDock\\UploadFiles\\" + FileName));
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO DD_DocketReportImages (DocketId,CatId,[FileName],Caption) VALUES( " + DocketId + "," + CatId + ",'" + FileName + "','" + Caption + "')");
                                                        
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Files uploaded successfully.');", true);
        ShowFiles();
    }
}