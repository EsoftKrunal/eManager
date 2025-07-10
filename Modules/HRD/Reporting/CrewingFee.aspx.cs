using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Reporting_CrewingFee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 205);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
       
        if (!(IsPostBack))
        {
            ddl_Owner.DataSource = cls_SearchReliever.getMasterData("Owner", "OwnerId", "OwnerName");
            ddl_Owner.DataTextField = "OwnerName";
            ddl_Owner.DataValueField = "OwnerId";
            ddl_Owner.DataBind();
            ddl_Owner.Items.Insert(0, new ListItem("< All >", "0"));
        }
        else
        {
            ShowData();
        }
    }
    public void ShowData()
    {
        IFRAME1.Attributes.Add("src", "CrewingFeeContainer.aspx?ownerid=" + ddl_Owner.SelectedValue + "&fdt=" + txtfromdate.Text.Trim() + "&tdt=" + txttodate.Text.Trim() + "&mgtfee=" + txtMgtFee.Text.Trim() + "&mstfee=" + txt_Mustfee.Text.Trim());  
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ownerid = ddl_Owner.SelectedValue;
        string fdt = txtfromdate.Text;
        string tdt = txttodate.Text;
        int SummaryCount = 0;
        string oldOwercode="", newOwnerCode="";
        string mgtfee = txtMgtFee.Text;
        string mstfee = txt_Mustfee.Text;
        StringBuilder sb=new StringBuilder(); 
        DataTable dt = Budget.getTable("exec dbo.CrewingFeeReport " + ownerid + ",'" + fdt + "','" + tdt + "'," + mgtfee + "," + mstfee + "").Tables[0];
        DataTable dtSummary = Budget.getTable("exec dbo.CrewingFeeReport_Summary " + ownerid + ",'" + fdt + "','" + tdt + "'," + mgtfee + "," + mstfee + ",0").Tables[0];
        DataTable dtFullSummary = Budget.getTable("exec dbo.CrewingFeeReport_Summary " + ownerid + ",'" + fdt + "','" + tdt + "'," + mgtfee + "," + mstfee + ",1").Tables[0];
        Response.Clear();
        Response.ContentType = "application/xls";
        Response.AddHeader("Content-Disposition", "attachment;filename=Crewing Fee.xls");
        sb.Append("<table border='1' bordercolor='black' >");
        //------------------ COMPANY NAME ROW
        sb.Append("<tr style='font-weight:bold;'>");
        sb.Append("<td colspan='5'>MTM SHIP MANAGEMENT PTE LTD.</td><td colspan='9'>&nbsp;</td>");
        sb.Append("</tr>");
        //------------------ REPORT NAME ROW
        sb.Append("<tr style='font-weight:bold;'>");
        sb.Append("<td colspan='5'>CREWING / MUSTERING FEE/TRAINING FEE</td><td colspan='9'>&nbsp;</td>");
        sb.Append("</tr>");
        //------------------ PERIOD ROW
        sb.Append("<tr style='font-weight:bold;'>");
        sb.Append("<td colspan='5'>Period : " + fdt + " - " + tdt + "</td><td colspan='9'>&nbsp;</td>");
        sb.Append("</tr>");
        //------------------ HEADER ROW
        sb.Append("<tr style='background-color:gray;font-weight:bold;'>");
        for (int i = 1; i <= dt.Columns.Count - 1; i++)
        {
            sb.Append("<td>");
            sb.Append(dt.Columns[i].ColumnName);
            sb.Append("</td>");
        }
        sb.Append("</tr>");
        if (dt.Rows.Count > 0) { oldOwercode = dt.Rows[0]["ownercode"].ToString();}
        foreach (DataRow dr in dt.Rows)
        {
            newOwnerCode = dr["ownercode"].ToString();
            if (newOwnerCode != oldOwercode)
            {
                //------------------ GROUP SUMMARY ROW
                sb.Append("<tr style='background-color:gray'>");
                DataRow dr1 = dtSummary.Rows[SummaryCount];
                sb.Append("<td>TOTAL [ " + dr1["ownercode"].ToString() + " ]</td><td>&nbsp;</td>");
                for (int i = 1; i <= dtSummary.Columns.Count - 1; i++)
                {
                    sb.Append("<td>");
                    sb.Append(dr1[i].ToString());
                    sb.Append("</td>");
                }
                SummaryCount++;
                sb.Append("</tr>");
            }
            //------------------ DATA ROW
            sb.Append("<tr>");
            for (int i = 1; i <= dt.Columns.Count - 1; i++)
            {
                sb.Append("<td>");
                sb.Append(dr[i].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");    
            oldOwercode=dr["ownercode"].ToString();  
        }
        //------------------ LAST GROUP SUMMARY ROW
        sb.Append("<tr style='background-color:gray'>");
        DataRow dr2 = dtSummary.Rows[SummaryCount];
        sb.Append("<td>TOTAL [ " + dr2["ownercode"].ToString() + " ]</td><td>&nbsp;</td>");
        for (int i = 1; i <= dtSummary.Columns.Count - 1; i++)
        {
            sb.Append("<td>");
            sb.Append(dr2[i].ToString());
            sb.Append("</td>");
        }
        SummaryCount++;
        sb.Append("</tr>");
        //------------------
        //------------------ AGGRIGATE SUMMARY ROW
        sb.Append("<tr style='background-color:gray;font-weight:bold;'>");
        DataRow dr3 = dtFullSummary.Rows[0];
        sb.Append("<td>AGGRIGATE TOTAL</td><td>&nbsp;</td>");
        for (int i = 0; i <= dtFullSummary.Columns.Count - 1; i++)
        {
            sb.Append("<td>");
            sb.Append(dr3[i].ToString());
            sb.Append("</td>");
        }
        SummaryCount++;
        sb.Append("</tr>");
        //------------------
        sb.Append("</table>"); 
        Response.Write(sb.ToString());
        Response.End();  
    }
}
