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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.Data.OleDb;
using System.IO;

public partial class Transactions_MTMCheckList : System.Web.UI.Page
{
    
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
            showdata();
        }
        try
        {
            
        }
        catch { }       
    }

    public void showdata()
    {
        int intInspId = Common.CastAsInt32(Request.QueryString["InspId"]);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM T_INSPECTIONDUE WHERE ID=" + intInspId);
        if(dt.Rows.Count>0)
        {
            lblInspectionNo.Text = dt.Rows[0]["InspectionNo"].ToString();
            rptchecklist.DataSource = Common.Execute_Procedures_Select_ByQuery("select ROW_NUMBER() OVER(ORDER BY len(questionno),questionno)  AS sno,* from t_company_observations o inner join tblInternalInspectionCheckList c on o.QuestionId=c.QuestionId where InspDueId=" + intInspId + " ORDER BY len(questionno),questionno");
            rptchecklist.DataBind();
        }
    }

    public DataTable Attachments(int InspDueID, int QuestionId)
    {
        string sql = " select InspDueId,QuestionId,imageid,filename from t_company_observations_attachments --where InspDueID="+ InspDueID + " and QuestionId="+ QuestionId + " ";
        DataTable Dt= Common.Execute_Procedures_Select_ByQuery(sql);
        return Dt;


    }
}
