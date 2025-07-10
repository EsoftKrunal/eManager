using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFiles : System.Web.UI.Page
{
    public int MainId
    {
        set { ViewState["MainId"] = value; }
        get { return Common.CastAsInt32(ViewState["MainId"]); }
    }
    public int InspId
    {
        set { ViewState["InspId"] = value; }
        get { return Common.CastAsInt32(ViewState["InspId"]); }
    }
    public string SNO 
    {
        set { ViewState["SNO"] = value; }
        get { return ViewState["SNO"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            InspId=Common.CastAsInt32(Request.QueryString["INSP"]);
            MainId = Common.CastAsInt32(Request.QueryString["MainId"]);
            SNO=Request.QueryString["SNO"];
        }
    }
    protected void ShowFiles()
    {
        //------------------------
        string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Modules/Inspection/Temp"));
        List<String> s= new List<string>();
        //------------------------
        foreach(string f in Files)
        {
            if (f.ToLower().EndsWith(".png") || f.ToLower().EndsWith(".jpg") || f.ToLower().EndsWith(".jpeg"))
            {
                s.Add(f);
            }
        }
        //------------------------
        rptTempFiles.DataSource = s.ToArray();
        rptTempFiles.DataBind();
        //------------------------
    }
    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        ShowFiles();    
    }
    protected void btnDeleteAll_OnClick(object sender, EventArgs e)
    {
        string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Modules/Inspection/Temp"));
        foreach (string fl in Files)
        {
            System.IO.File.Delete(fl);
        }
        ShowFiles();
    }
    protected void DeleteImage(object sender, EventArgs e)
    {
        string Fl = ((ImageButton)sender).CommandArgument;
        System.IO.File.Delete( Server.MapPath("~\\Modules\\Inspection\\Temp\\" + Fl));
        ShowFiles();
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        foreach (DataListItem di in rptTempFiles.Items)
        {
            string Caption = ((TextBox)(di.FindControl("txtCaption"))).Text.Trim().Replace("'","`");
            string FileName = ((ImageButton)(di.FindControl("btnDelImg"))).CommandArgument;
            FileName = Path.GetFileName(FileName);
            File.Move(Server.MapPath("~\\Modules\\Inspection\\Temp\\" + FileName), Server.MapPath("~\\Modules\\Inspection\\UserUploadedDocuments\\Transaction_Reports\\" + FileName));
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO INSPREPORT_CHILD(MAINTABLEID,INSPECTIONDUEID,SRNO,PICCAPTION,FILEPATH,CREATEDBY,CREATEDON) VALUES(" + MainId + "," + InspId + ",'" + SNO + "','" + Caption + "','" + FileName + "',1,GetDate())");
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Files uploaded successfully.');", true);
        ShowFiles();
    }
}