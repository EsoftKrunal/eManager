using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;

public partial class Reporting_PrintTemplateContract : System.Web.UI.Page
{
    public int ContractId
    {
        get { return Common.CastAsInt32(ViewState["_ContractId"]); }
        set { ViewState["_ContractId"] = value; }
    }
    public int CrewId
    {
        get { return Common.CastAsInt32(ViewState["_CrewId"]); }
        set { ViewState["_CrewId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 19);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        else
        {
            ContractId = Common.CastAsInt32(txtcontractid.Text);
            System.Data.DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWID FROM DBO.CREWCONTRACTHEADER WHERE CONTRACTID=" + ContractId);
            if (dt.Rows.Count > 0)
            {
                CrewId = Common.CastAsInt32(dt.Rows[0][0]);               
            }
        }
    }
    private void findreplace(Microsoft.Office.Interop.Word.Application app, object findtext, object replacetext)
    {
        object matchCase = true;
        object matchwholeword = true;
        object matchwildcard = false;
        object matchsoundlike = false;
        object matchappwordforms = false;
        object forward = false;
        object format = false;
        object matchkashida = false;
        object matchdia = false;
        object matchjamza = false;
        object macthcontrol = false;
        object read_only = false;
        object visible = true;
        object replace = 2;
        object wrap = 1;

        app.Selection.Find.Execute(ref findtext,
            ref matchCase, ref matchwholeword,
            ref matchwildcard, ref matchsoundlike,
            ref matchappwordforms, ref forward,
            ref wrap, ref format, ref replacetext, ref replace, ref matchkashida, ref matchdia, ref matchjamza, ref macthcontrol);

    }
    public void ShowReport()
    {
        string sql_personaldetails = "select top 1 crewnumber,firstname,middlename,lastname,firstname + ' ' + middlename + ' ' + lastname as fullname,replace(convert(varchar,dateofbirth,106),' ','-') as dob,placeofbirth,RankCode,NationalityName from  CrewPersonalDetails cpd left join rank r on cpd.CurrentRankId=r.rankid inner join country c on cpd.nationalityid = c.CountryId where cpd.crewid=" + CrewId;
        string sql_contactdetails = "select top 1 FullAddress=Address1 + ' ' + Address2 + ' ' + Address3 +  ' '  + city + ' '  + [State] + ' ' + c.CountryName + ' ' + PINCode from crewcontactdetails cd left join country c on cd.countryid=c.CountryId where AddressType='P' and cd.crewid=" + CrewId;
        string sql_travel = "select top 1 DocumentNumber as P_DocumentNumber,replace(convert(varchar, issuedate, 106), ' ', '-') as p_issuedate,replace(convert(varchar, expirydate, 106), ' ', '-') as p_expirydate,placeofissue as  p_placeofissue from crewtraveldocument where crewid = " + CrewId + " and documenttypeid = 0  order by traveldocumentid desc";
        string sql_travel1 = "select top 1 DocumentNumber as S_DocumentNumber,replace(convert(varchar, issuedate, 106), ' ', '-') as s_issuedate,replace(convert(varchar, expirydate, 106), ' ', '-') as s_expirydate,placeofissue as s_placeofissue from crewtraveldocument where crewid = " + CrewId + " and documenttypeid = 2  order by traveldocumentid desc";
        string sql_co = "select top 1 issuedate as c_issuedate,startdate as c_startdate,enddate as enddate,duration as c_duration,rankcode as c_rankcode from crewcontractheader c inner join rank r on c.newRankId=r.RankId where c.contractid=" + ContractId;
        string sql_w = "SELECT WageScaleComponentId,Amount FROM DBO.CrewContractDetails WHERE CONTRACTID=" + ContractId;
        Dictionary<String, string> args = new Dictionary<String, string>();

        System.Data.DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_personaldetails);
        foreach (DataColumn dc in dt.Columns)
        {args.Add("$$" + dc.ColumnName.ToLower() + "$$",dt.Rows[0][dc.ColumnName].ToString());}

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_contactdetails);
        foreach (DataColumn dc in dt.Columns)
        {args.Add("$$" + dc.ColumnName.ToLower() + "$$", dt.Rows[0][dc.ColumnName].ToString());}

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_travel);
        foreach (DataColumn dc in dt.Columns)
        { args.Add("$$" + dc.ColumnName.ToLower() + "$$", dt.Rows[0][dc.ColumnName].ToString()); }

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_travel1);
        foreach (DataColumn dc in dt.Columns)
        { args.Add("$$" + dc.ColumnName.ToLower() + "$$", dt.Rows[0][dc.ColumnName].ToString()); }

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_co);
        foreach (DataColumn dc in dt.Columns)
        { args.Add("$$" + dc.ColumnName.ToLower() + "$$", dt.Rows[0][dc.ColumnName].ToString()); }
        
        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql_w);
        foreach (DataRow dr in dt.Rows)
        { args.Add("$$wage_" + dr[0].ToString().ToLower() + "$$", dr[1].ToString()); }

        //===================================================


        object filename = @"F:\MTM\CMS\Web\UserUploadedDocuments\template\contract\template.doc";
        object filename1 = @"F:\MTM\CMS\Web\UserUploadedDocuments\template\contract\output.doc";

        object readOnly = false;
        object isVisible = false;

        object missing = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
        app.Visible = false;
        Microsoft.Office.Interop.Word.Document d = app.Documents.Open(ref filename, ref missing,
                                                                    ref readOnly, ref missing, ref missing, ref missing,
                                                                    ref missing, ref missing, ref missing, ref missing
                                                                    , ref missing, ref isVisible, ref missing, ref missing
                                                                    , ref missing, ref missing);
        d.Activate();
        foreach(System.Collections.Generic.KeyValuePair<string,string> i in args)
        {
            findreplace(app, i.Key,i.Value);
        }        
        d.SaveAs(ref filename1, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing
            , ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

        txtcontractid.Text = "Contract print done.";
    }
    protected void btnGo_Click(object sender,EventArgs e)
    {
        ShowReport();
    }
}
