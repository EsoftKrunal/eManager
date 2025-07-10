using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json;

public partial class RCAanalysis : System.Web.UI.Page
{
    public DataTable DtData
    {
        get {
            if (ViewState["_data"] != null)
            {
                return (DataTable)ViewState["_data"];
            }
            else
            {
                string SQL = "";
                SQL = " select *,(select count(caid) from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions c where c.analysisid=a.analysisid) as noca,(select count(commentid) from dbo.VSL_BreakDownRemarks_Analysis_Comments c where c.analysisid=a.analysisid) as noc from dbo.VSL_BreakDownRemarks_RCA_Analysis a where BreakDownNo='" + BreakDownNo + "' and VesselCode='" + VesselCode + "' ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                ViewState["_data"] = dt;
                return dt;
            }
        }
    }

    public string BreakDownNo
    {
        get { return ViewState["BreakDownNo"].ToString(); }
        set { ViewState["BreakDownNo"] = value; }
    }
    public string VesselCode
    {
        get { return Convert.ToString(ViewState["_VesselCode"]); }
        set { ViewState["_VesselCode"] = value; }
    }
    public string UserName
    {
        get { return Convert.ToString(ViewState["_UserName"]); }
        set { ViewState["_UserName"] = value; }
    }
    public bool IsActionAllowed
    {
        get { return Convert.ToBoolean (ViewState["_IsActionAllowed"]); }
        set { ViewState["_IsActionAllowed"] = value; }
    }
    public StringBuilder myHtml = new StringBuilder();
    bool first45ul = true;
    public string RcaStatus
    {
        get { return Convert.ToString(ViewState["_RcaStatus"]); }
        set { ViewState["_RcaStatus"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ProjectCommon.SessionCheck();
        BreakDownNo = ""+Page.Request.QueryString["BreakDownNo"];
        VesselCode = Convert.ToString(Page.Request.QueryString["VesselCode"]);
        litFocalPoint.Text = Convert.ToString(Page.Request.QueryString["focalpoint"]);
        UserName = Convert.ToString(Session["UserName"]);
        string sql = "exec dbo.VSL_BreakDownRemarks_RCA_InitAnalysis '" + BreakDownNo + "','" + VesselCode + "'";
        Common.Execute_Procedures_Select_ByQuery(sql);
        SetIsActionAllowed();

        ShowHeaderData();

        SHow(0,"");
        //ShowRootCauses();
        litTree.Text = myHtml.ToString();
        if (RcaStatus == "C")
            btnSendForApproval.Visible = false;
        else
            btnSendForApproval.Visible = true;
    }
   
    protected void SetRootCause(object sender, EventArgs e)
    {
        int AnalysisID = 0;
        string sql = "update VSL_BreakDownRemarks_RCA_Analysis set Status='R' Where AnalysisID=" + AnalysisID;
        Common.Execute_Procedures_Select_ByQuery(sql);

    }
    protected void btnTempClick_Click(object sender, EventArgs e)
    {
        ViewState["_data"] = null;
        myHtml.Length = 0;
        SHow(0, "");
        litTree.Text = myHtml.ToString();        
    }
    
    protected void RemoveCause(object sender, EventArgs e)
    {
    }
    protected void btnSendForApproval_OnClick(object sender, EventArgs e)
    {
        string sql = " Select* from dbo.VSL_BreakDownRemarks_RCA_Analysis A where BreakDownNo = '" + BreakDownNo + "' and VesselCode = '"+ VesselCode + "' and status = 'R' "+
                     "  and not exists(select * from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions ca where ca.analysisID = A.analysisID) ";
        DataTable dt= Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "aaa1", "alert('Please add corrective action in all root cause to continue.')", true);
            return;
        }
        else
        {


            sql = " Update dbo.VSL_BreakDownRemarks set Stage=3,FwdByApproval='" + UserName + "',FwdByApprovalOn=getdate() " +
                         "   where BreakDownNo = '" + BreakDownNo + "' and VesselCode = '" + VesselCode + "' ";
            Common.Execute_Procedures_Select_ByQuery(sql);
            myHtml.Length = 0;
            SetIsActionAllowed();
            SHow(0, "");
            litTree.Text = myHtml.ToString();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "aaa", "alert('Approval request has been send successfully.')", true);
        }
    }
    

    public void ShowHeaderData()
    {
        string sql = " select V.VesselName,R.BreakDownNo,ReportDt as ReportDate from dbo.VSL_BreakDownMaster  R " +
                     "   inner join dbo.Vessel V on V.VesselCode = R.VesselCode where BreakDownNo = '" + BreakDownNo + "' and V.VesselCode='" + VesselCode+"'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            litVessel.Text = Dt.Rows[0]["VesselName"].ToString();
            litReportNo.Text = "<b>BreakDownNo# :</b> " + Dt.Rows[0]["BreakDownNo"].ToString();
            litReportDate.Text = "<b>BreakDown Date :</b> " + Common.ToDateString(Dt.Rows[0]["ReportDate"]);
            //litPort.Text = "<b>Port :</b> " + Dt.Rows[0]["Port"].ToString();
        }
    }
    public void SetIsActionAllowed()
    {
        string SQL1 = "select 1 from dbo.VSL_BreakDownRemarks where BreakDownNo = '" + BreakDownNo + "' and VesselCode='" + VesselCode + "' and ExportedOn is not null";
        DataTable dt111 = Common.Execute_Procedures_Select_ByQuery(SQL1);
        bool exported = (dt111.Rows.Count > 0);
        IsActionAllowed = !exported;

        string sql = " select Stage,RcaDocument from dbo.VSL_BreakDownRemarks  R where R.VesselCode = '" + VesselCode + "' and R.BreakDownNo = '" + BreakDownNo + "'";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        //if (Dt.Rows[0]["Stage"].ToString()=="2")
        //    IsActionAllowed = true;
        //else
        //    IsActionAllowed = false;

        //if (Dt.Rows[0]["Stage"].ToString() == "4")
        //{
        //    if (Dt.Rows[0]["RcaDocument"].ToString() == "")            
        //        RcaStatus = "O";            
        //    else
        //        RcaStatus = "C";
        //}
        RcaStatus = (IsActionAllowed)?"O":"C";
    }
    
    public void SHow(int ParentAnalysisID ,string ParentName)
    {
        
        DataView dv = DtData.DefaultView;
        dv.RowFilter = "ParentAnalysisID=" + ParentAnalysisID;
        if (dv.ToTable().Rows.Count != 0)
        {
            if (first45ul)
            {
                myHtml.Append("<ul class='notopmargin'>");
                first45ul = false;
            }
            else
                myHtml.Append("<ul>");
            foreach (DataRow Dr in dv.ToTable().Rows)
            {
                if (ParentAnalysisID == 0)
                {
                    ParentName = Dr["Cause"].ToString().ToLower();
                }

                myHtml.Append("<li >");
                myHtml.Append("<a href='#' " + ((Dr["Status"].ToString() == "R") ? "class='a_rootcause'" : "class='" + ParentName + "'") + ">");
                myHtml.Append("<span class='fa " + StatusClass(Dr["Status"].ToString()) + "'></span>");
                if (Common.CastAsInt32(Dr["noc"])>0)
                {
                    myHtml.Append("<span class='fa fa-comments' style='float:right;margin-right: 9px;margin-top: 2px;color:#595a5a' onclick='showcomments(" + Dr["AnalysisID"].ToString() + ");'/></span>");
                }
                myHtml.Append("<div class='causeheading'>" + ((Dr["HasChilds"].ToString()=="True")?"WHY ?":((Dr["Status"].ToString() == "R")?"Root Cause": "Cause")) + "</div>");
                myHtml.Append("<div class='causesummary'>" + Dr["Cause"]  + "</div>");
                if (Dr["Status"].ToString() == "R")
                {
                    myHtml.Append("<div class='caheading' onclick='showca(" + Dr["AnalysisID"].ToString() + ");'>View Corrective Actions ( " + Dr["noca"] + " Items )</div>");
                    
                }
                myHtml.Append("</a>");
                if (IsActionAllowed)
                {
                    myHtml.Append("<div class='action' key='" + Dr["AnalysisID"].ToString() + "' paid='" + Dr["ParentAnalysisID"].ToString() + "' status='" + Dr["Status"].ToString() + "'>");
                    myHtml.Append("<i class='fa fa-chevron-down'></i>");
                    myHtml.Append("</div>");
                }
                SHow(Common.CastAsInt32(Dr["AnalysisID"]), ParentName);
                myHtml.Append("</li>");
            }
            myHtml.Append("</ul>");
        }
        
    }

    public string StatusClass(string Cause)
    {
        string ret = "";
        switch (Cause)
        {
            case "C":
                ret = "causestatus";
                break;
            case "R":
                ret = "rootcause";
                break;
            case "T":
                ret = "terminated";
                break;
            default:
                ret = "causestatus";
                break;
        }
        return ret;
    }
    
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    dvFrame.Visible = false;
    //    string sql = "UPDATE dbo.ER_S115_Report_RCA_Analysis SET CorrectiveAction='" + txtcorraction.Text.Replace("'","`") + "',TargetClosureDate='" + txtTCLDate.Text + "',Responsibility='" + ((radO.Checked)?"O":"V") + "'  where vesselcode='" + VesselCode + "' AND REPORTID=" + ReportID + " and AnalysisID=" + ViewState["aid"].ToString();
    //    Common.Execute_Procedures_Select_ByQuery(sql);
    //    //ShowRootCauses();
    //}
    //protected void btnClose_Click(object sender,EventArgs e)
    //{
    //    dvFrame.Visible = false;
    //}
    //protected void ancEdit_Click(object sender, EventArgs e)
    //{
    //    ViewState["aid"] =Common.CastAsInt32(((System.Web.UI.HtmlControls.HtmlAnchor)sender).Attributes["key"]);
    //    string sql = "select * from dbo.ER_S115_Report_RCA_Analysis where vesselcode='" + VesselCode + "' AND REPORTID=" + ReportID + " and Status='R' and AnalysisID=" + ViewState["aid"].ToString();
    //    DataTable dt= Common.Execute_Procedures_Select_ByQuery(sql);
    //    if(dt.Rows.Count>0)
    //    {
    //        dvFrame.Visible = true;
    //        //---------------------------
    //        lblrootcause.Text = dt.Rows[0]["Cause"].ToString();
    //        txtcorraction.Text = dt.Rows[0]["CorrectiveAction"].ToString();
    //        txtTCLDate.Text = Common.ToDateString(dt.Rows[0]["TargetClosureDate"]);
    //        radO.Checked = (dt.Rows[0]["Responsibility"].ToString()=="O");
    //        radV.Checked = (dt.Rows[0]["Responsibility"].ToString() == "V");
    //        //---------------------------
    //    }
       
    //}
    
    //-----------------------------------------------------------------------------------------------------------------------------------
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string terminatecause(int aid)
    {
        string sql = "update dbo.VSL_BreakDownRemarks_RCA_Analysis set Status='T' Where AnalysisID=" + aid;        
        Common.Execute_Procedures_Select_ByQuery(sql);

        sql = " exec dbo.VSL_BreakDownRemarks_RemoveChilds " + aid;
        Common.Execute_Procedures_Select_ByQuery(sql);

        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string setrootcause(int aid)
    {
        string sql = "update dbo.VSL_BreakDownRemarks_RCA_Analysis set Status='R' Where AnalysisID=" + aid;
        //string sql = "update ER_S115_Report_RCA_Analysis set Status='T' Where AnalysisID=999";
        Common.Execute_Procedures_Select_ByQuery(sql);

        sql = " exec dbo.VSL_BreakDownRemarks_RemoveChilds " + aid;
        Common.Execute_Procedures_Select_ByQuery(sql);

        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string updatecause(int aid,string cause)
    {
        string sql = "update dbo.VSL_BreakDownRemarks_RCA_Analysis set cause='" + cause.Replace("'","`") + "' Where AnalysisID=" + aid + " and ParentAnalysisID<>0";
        //string sql = "update ER_S115_Report_RCA_Analysis set Status='T' Where AnalysisID=999";
        Common.Execute_Procedures_Select_ByQuery(sql);
        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string removecause(int aid)
    {
        string sql = "exec dbo.VSL_BreakDownRemarks_RemoveRootCause " + aid;        
        Common.Execute_Procedures_Select_ByQuery(sql);
        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string savecause(int aid,string breakdownno,string vesselcode,string cause,string mode, string username)
    {
        
        string ret = "error";
        try
        {
            Common.Set_Procedures("[DBO].VSL_BreakDownRemarks_SAVE_RCA_Analysis");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@AnalysisID", aid),
                new MyParameter("@BreakDownNo", breakdownno),
                new MyParameter("@VesselCode", vesselcode),                
                new MyParameter("@Cause", cause),
                new MyParameter("@mode", mode),
                new MyParameter("@UserName", username)


                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ret = "OK";
            }
        }
        catch (Exception ex)
        {
            
        }

        return ret;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string savecomments(int aid,string comments)
    {
        string ret = "error";
        try
        {
            Common.Set_Procedures("[DBO].VSL_BreakDownRemarks_RCA_Analysis_Comments");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@AnalysisID", aid),
                new MyParameter("@Comments", comments),
                new MyParameter("@CommentBy", HttpContext.Current.Session["UserName"].ToString())
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ret = "OK";
            }
        }
        catch (Exception ex)
        {

        }

        return ret;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string updatecomments(int aid, string comments,int commentid)
    {
        string ret = "error";
        try
        {
            string sql = " Update dbo.VSL_BreakDownRemarks_Analysis_Comments set Comments='" + comments.Replace("'","~") + "' where AnalysisID="+ aid + " and CommentId="+ commentid;
            Common.Execute_Procedures_Select_ByQuery(sql);
            return aid.ToString();
        }
        catch (Exception ex)
        {

        }

        return ret;
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string saveca(int caid,int aid, string catext,string tcldate,string resp)
    {
        string ret = "error";
        try
        {
            Common.Set_Procedures("[DBO].VSL_BreakDownRemarks_RCA_Analysis_CorrectiveActions");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(                
                new MyParameter("@CAID", caid),
                new MyParameter("@AnalysisID", aid),
                new MyParameter("@catext", catext),
                new MyParameter("@Responsibility", resp),
                new MyParameter("@TargetClosureDate", tcldate),
                new MyParameter("@CreatedBy", HttpContext.Current.Session["UserName"].ToString())
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ret = "OK";
            }
        }
        catch (Exception ex)
        {

        }

        return ret;
    }

    

        [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string loadcauselist(int aid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = "select * from (select 1 as kk,cause from dbo.ER_S115_Report_CauseMaster where maingroup in (select MainGroup from dbo.VSL_BreakDownRemarks_RCA_Analysis  where AnalysisID=" + aid + ") union select -1 as kk,'Other' ) k order by kk desc";
            DataTable dt= Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject(dt);
            return json;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return myHtml.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string loadcomments(int aid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = " select commentid,comments,commentby,convert(varchar,commentdate,106) as commentdate from DBO.VSL_BreakDownRemarks_Analysis_Comments where analysisid=" + aid + " order by commentdate desc";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            return json;

        }
        catch (Exception ex)
        {
            return "";
        }

        return myHtml.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string deletecomments(int commentid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = " delete from DBO.VSL_BreakDownRemarks_Analysis_Comments where CommentId=" + commentid;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject("ok", Formatting.None);
            return json;

        }
        catch (Exception ex)
        {
            string json = JsonConvert.SerializeObject("err", Formatting.None);
            return json;            
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string deleteca(int caid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = " delete from DBO.VSL_BreakDownRemarks_Analysis_CorrectiveActions where caid=" + caid;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject("ok", Formatting.None);
            return json;

        }
        catch (Exception ex)
        {
            string json = JsonConvert.SerializeObject("err", Formatting.None);
            return json;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string loadca(int aid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = "select caid,correctiveaction,responsibility=case when responsibility='B' then 'Office, Vessel' when responsibility='O' then 'Office' else 'Vessel' End,convert(varchar,targetclosuredate,106) as targetclosuredate from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions where analysisid=" + aid + " order by targetclosuredate";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            return json;

        }
        catch (Exception ex)
        {
            return "";
        }

        return myHtml.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string loadcasingle(int caid)
    {
        StringBuilder myHtml = new StringBuilder();
        try
        {
            string sql = "select caid,correctiveaction,responsibility,replace(convert(varchar,targetclosuredate,106),' ','-') as targetclosuredate from dbo.VSL_BreakDownRemarks_Analysis_CorrectiveActions where caid=" + caid;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            return json;

        }
        catch (Exception ex)
        {
            return "";
        }

        return myHtml.ToString();
    }


}