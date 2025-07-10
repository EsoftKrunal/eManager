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
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;


public partial class Emtm_TrainingMatrix : System.Web.UI.Page
{
    //Authority Auth;
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_msg.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 244);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training.aspx");
        }
        if (!IsPostBack)
        {
            if(Request.Form["mode"]!=null)
            {
                int tid = Common.CastAsInt32(Request.Form["tid"]);
                int gid = Common.CastAsInt32(Request.Form["gid"]);
                string ar =Convert.ToString(Request.Form["ar"]);
                //-----------------
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.HR_Assign_Remove_Training " + gid  + "," + tid +",'" + ar +"'");
                //-----------------
                Response.Clear();
                if(dt.Rows.Count>0)
                    Response.Write(dt.Rows[0][0]);
                else
                    Response.Write("N");
                Response.End();
            }
            else
                showData(false);
        }
    }

    void showData(bool EditAllow)
    {
        DataTable dtPosGroups = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingPositionGroup Order By GroupName");

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingPosition_Mapping");
        StringBuilder sbheader = new StringBuilder();
        DataTable dtPositions = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupName,tm.* FROM HR_TrainingMaster tm inner join HR_TrainingGroup tg on tm.TrainingGroupId=tg.TrainingGroupId where ShowInMatrix='Y' order by TrainingGroupName,TrainingName");
        {
            DataView dv = dtPositions.DefaultView;
            DataTable dtTrainings = dv.ToTable();
            sbheader.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse' class='bordered'>");
            sbheader.Append("<tr>");
            sbheader.Append("<td class='tgroup header'>Type</td>");
            sbheader.Append("<td class='tname header'>Training Name</td>");
            sbheader.Append("<td class='interval header'>Int (M)</td>");
            foreach (DataRow dr2 in dtPosGroups.Rows)
            {
                sbheader.Append("<td class='posgroup header'>");
                sbheader.Append(dr2["GroupName"].ToString());
                sbheader.Append("</td>");
            }
            sbheader.Append("</tr>");
            sbheader.Append("</table>");

            StringBuilder sb = new StringBuilder();
            if (dtTrainings.Rows.Count > 0)
            {
                sb.Append("<table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse' class='bordered'>");
                foreach (DataRow dr1 in dtTrainings.Rows)
                {

                    sb.Append("<tr>");
                    sb.Append("<td class='tgroup " + dr1["TrainingGroupName"].ToString()  + "'>");
                    sb.Append(dr1["TrainingGroupName"].ToString());
                    sb.Append("</td>");

                    sb.Append("<td class='tname'>");
                    sb.Append(dr1["TrainingName"].ToString());
                    sb.Append("</td>");

                    sb.Append("<td class='interval'>");
                    sb.Append( dr1["ValidityPeriod"].ToString());
                    sb.Append("</td>");
                    

                    foreach (DataRow dr2 in dtPosGroups.Rows)
                    {
                        int tid = Common.CastAsInt32(dr1["TrainingId"].ToString());
                        int gid = Common.CastAsInt32(dr2["GroupId"].ToString());
                        string activeclass= (dtdata.Select("GroupId=" + gid + " And TrainingId=" + tid).Length > 0)?"assigned":"";

                        if(EditAllow)
                            sb.Append("<td  class='" + ((EditAllow)?"action":"") +" posgroup data " + activeclass + "' tid='" + tid + "' gid='" + gid + "'>");
                        else
                            sb.Append("<td onclick='ShowDetails(" + tid + "," + gid + ")' class='" + ((EditAllow) ? "action" : "") + " posgroup data " + activeclass + "' tid='" + tid + "' gid='" + gid + "'>");
                        //sb.Append("<div style='height:100%; background-color:red;'>2</div>");

                        sb.Append("</td>");
                    }
                 
                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
            }
            litdata.Text = sb.ToString();
        }
        ltheader.Text = sbheader.ToString();
    }

    protected void btnAllowedit_Click(object sender, EventArgs e)
    {
        if (btnAllowedit.Text == "Edit")
        {
            showData(true);
            btnAllowedit.Text = "Back";
        }
        else
        {
            showData(false);
            btnAllowedit.Text = "Edit";
        }
    }
}
    
