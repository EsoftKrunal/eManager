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

public partial class Transactions_ResponseDescPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            //if (Page.Request.QueryString["Ques"] != null)
            //{
                string QuestNo = Page.Request.QueryString["Ques"].ToString();
                string Version = Page.Request.QueryString["Ver"].ToString();
                string InspId =Convert.ToString(Request.QueryString["InspId"]);
                if (InspId.Trim() == "")
                {
                    DataTable dtQues = Budget.getTable("SELECT DESCRIPTION FROM M_QUESTIONS WHERE QuestionNo='" + QuestNo + "' And VersionId=" + Version + " And VesselType<>0").Tables[0];
                    bool datapresent = false;
                    if (dtQues.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtQues.Rows)
                        {
                            if (dr["Description"].ToString().Trim() != "") { datapresent = true; }
                            string Descptn = dr["Description"].ToString().Replace("\n", "<br />");
                            Response.Write(Descptn);
                            break;
                        }
                    }
                    if(!datapresent)
                    {
                        dtQues = Budget.getTable("SELECT DESCRIPTION FROM M_QUESTIONS WHERE QuestionNo='" + QuestNo + "' And VersionId=" + Version + " And VesselType=0").Tables[0];
                        foreach (DataRow dr in dtQues.Rows)
                        {
                            string Descptn = dr["Description"].ToString().Replace("\n", "<br />");
                            Response.Write(Descptn);
                            break;
                        }
                    }
                }
                else
                {
                    DataTable dt=Budget.getTable("SELECT VESSELTYPEID FROM DBO.VESSEL V WHERE V.VESSELID IN (select VESSELID from DBO.t_inspectiondue where ID=" + InspId.Trim() + ")").Tables[0]; 
                    int VesselTypeId=0;
                    if (dt.Rows.Count > 0)
                        VesselTypeId = Common.CastAsInt32(dt.Rows[0][0]);
                    DataTable dtQues = Budget.getTable("SELECT DESCRIPTION FROM M_QUESTIONS WHERE QuestionNo='" + QuestNo + "' And VersionId=" + Version + " And VesselType=" + VesselTypeId.ToString()).Tables[0];
                    bool datapresent = false;
                    if (dtQues.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtQues.Rows)
                        {
                            if (dr["Description"].ToString().Trim() != "") { datapresent = true; }
                            string Descptn = dr["Description"].ToString().Replace("\n", "<br />");
                            Response.Write(Descptn);
                            break;
                        }
                    }
                    if (!datapresent)
                    {
                        dtQues = Budget.getTable("SELECT DESCRIPTION FROM M_QUESTIONS WHERE QuestionNo='" + QuestNo + "' And VersionId=" + Version + " And VesselType=0").Tables[0];
                        foreach (DataRow dr in dtQues.Rows)
                        {
                            string Descptn = dr["Description"].ToString().Replace("\n", "<br />");
                            Response.Write(Descptn);
                            break;
                        }
                    }
                }
                //DataTable dtQues = Inspection_Response.ResponseDetails(ObId, "", "", "QUESBYOID");
                
            //}
            //else if (Page.Request.QueryString["QuesId"] != null)
            //{
            //    int QuId = Convert.ToInt32(Page.Request.QueryString["QuesId"].ToString());
            //    DataTable dt1 = Inspection_Checklist.InspectionCheckListDetails(QuId, 0, "", "", "", 0, "", "", 0, "", 0, 0, "ById");
            //    if (dt1.Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dt1.Rows)
            //        {
            //            string Descptn = dr["Description"].ToString().Replace("\n", "<br />");
            //            Response.Write(Descptn);
            //        }
            //    }
            //}
        }
        catch { }
    }
}
