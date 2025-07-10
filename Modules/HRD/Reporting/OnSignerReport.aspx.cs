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
using System.Text; 

public partial class OnSignerReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 182);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime dt1 = DateTime.Parse(txt_from.Text);
        DateTime dt2 = DateTime.Parse(txt_to.Text);
        StringBuilder sb = new StringBuilder();  
        string Query = "select ROW_NUMBER() OVER(order by FirstName + ' ' + MiddleName + ' ' + LastName) AS 'Sr. No.', " +
        "FirstName + ' ' + MiddleName + ' ' + LastName as 'Name of Seafarer', " +
        "isnull((select top 1 documentnumber from crewcoursecertificatedetails where crewid=gch.crewid and coursecertificateid=268),'') as 'INDOS No', " +
        "isnull((select top 1 documentnumber from crewtraveldocument where documenttypeid=2 and crewid=gch.crewid),'') as 'CDC No.', " +
        "(select Rank_Mum from rank where rank.rankid=CurrentRankId)as Rank, " +
        "(select vesselname from vessel where vessel.vesselid=gch.vesselid) as 'Name of Vessel', " +
        "(select Flag_code from flagstate where flagstateid in (select flagstateid from vessel where vessel.vesselid=gch.vesselid )) as 'Flag of the Vessel', " +
        "left(convert(varchar,gch.signondate,113),11) as 'Date Of Commencement of Contract', " +
        "left(convert(varchar,isnull(gch.singoffdate,'           '),113),11) as 'Date Of Signing ''Off''', " +
        "left(convert(varchar,isnull(dateadd(day,1,gch.singoffdate),'           '),113),11) as 'Date of  Completion of Contract/Arriving India','' as 'Remarks if any'" +
        "from get_crew_history gch inner join crewpersonaldetails cpd on gch.crewid=cpd.crewid " +
        "WHERE (gch.signondate between '" + txt_from.Text + "' and '" + txt_to.Text + "' and cpd.nationalityid in (select countryid from country where countrycode='91')) " +
        "order by FirstName + ' ' + MiddleName + ' ' + LastName";
        DataTable dt = Budget.getTable(Query).Tables[0];
        Response.Clear();
        Response.ContentType = "application/CSV";
        Response.AddHeader("Content-Disposition", "attachment;filename=OnSigner " + dt1.ToString("MMM yyyy") + "  .csv");
        sb.Append("\"REPORT TO BE FORWARDED TO DIRECTOR, SEAMENS EMLOYMENT OFFICE BY THE RECRUITMENT AND PLACEMENT AGENCIES (FORM-IIIA)\",,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\"Rule 4(3)(a) and 4(6)(a) clause viii of Form-III of notification vide G.S.R. 182(E) dated 18.3.2005 regarding M.S. (R&PS) Rules, 2005\",,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("1,Name of RPS Agency:,,Energios Maritime India Pvt. LTD.,,,,Licence No.:,RPSL-MUM-162110,,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("2,Period,From:," + dt1.ToString("dd-MMM-yyyy") + ",,,,To:," + dt2.ToString("dd-MMM-yyyy") + ",,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("3,Details of Seafarers engaged ,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\n");
        for (int i = 0; i <= dt.Columns.Count - 1; i++)
        {
            sb.Append(((i == 0) ? "" : ",") + dt.Columns[i].ColumnName);
        }
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("\n");
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                sb.Append(((i == 0) ? "" : ",") + ((i<=7)?dr[i].ToString():""));
            }
        }

        Response.Write(sb.ToString());
        Response.End();  
   }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime dt1 = DateTime.Parse(txt_from.Text);
        DateTime dt2 = DateTime.Parse(txt_to.Text);
        StringBuilder sb = new StringBuilder();
        string Query = "select ROW_NUMBER() OVER(order by FirstName + ' ' + MiddleName + ' ' + LastName) AS 'Sr. No.', " +
        "FirstName + ' ' + MiddleName + ' ' + LastName as 'Name of Seafarer', " +
        "isnull((select top 1 documentnumber from crewcoursecertificatedetails where crewid=gch.crewid and coursecertificateid=268),'') as 'INDOS No', " +
        "isnull((select top 1 documentnumber from crewtraveldocument where documenttypeid=2 and crewid=gch.crewid),'') as 'CDC No.', " +
        "(select Rank_Mum from rank where rank.rankid=CurrentRankId)as Rank, " +
        "(select vesselname from vessel where vessel.vesselid=gch.vesselid) as 'Name of Vessel', " +
        "(select Flag_code from flagstate where flagstateid in (select flagstateid from vessel where vessel.vesselid=gch.vesselid )) as 'Flag of the Vessel', " +
        "left(convert(varchar,gch.signondate,113),11) as 'Date Of Commencement of Contract', " +
        "left(convert(varchar,isnull(gch.singoffdate,'           '),113),11) as 'Date Of Signing ''Off''', " +
        "left(convert(varchar,isnull(dateadd(day,1,gch.singoffdate),'           '),113),11) as 'Date of  Completion of Contract/Arriving India','' as 'Remarks if any'" +
        "from get_crew_history gch inner join crewpersonaldetails cpd on gch.crewid=cpd.crewid " +
        "WHERE gch.singoffdate between '" + txt_from.Text + "' and '" + txt_to.Text + "' and cpd.nationalityid in (select countryid from country where countrycode='91') " +
        "order by FirstName + ' ' + MiddleName + ' ' + LastName";
        DataTable dt = Budget.getTable(Query).Tables[0];
        Response.Clear();
        Response.ContentType = "application/CSV";
        Response.AddHeader("Content-Disposition", "attachment;filename=OffSigner " + dt1.ToString("MMM yyyy") + "  .csv");
        sb.Append("\"REPORT TO BE FORWARDED TO DIRECTOR, SEAMENS EMLOYMENT OFFICE BY THE RECRUITMENT AND PLACEMENT AGENCIES (FORM-IIIA)\",,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\"Rule 4(3)(a) and 4(6)(a) clause viii of Form-III of notification vide G.S.R. 182(E) dated 18.3.2005 regarding M.S. (R&PS) Rules, 2005\",,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("1,Name of RPS Agency:,,Energios Maritime India Pvt. LTD.,,,,Licence No.:,RPSL-MUM-162110,,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("2,Period,From:," + dt1.ToString("dd-MMM-yyyy") + ",,,,To:," + dt2.ToString("dd-MMM-yyyy") + ",,");
        sb.Append("\n");
        sb.Append("\n");
        sb.Append("3,Details of Seafarers engaged ,,,,,,,,,");
        sb.Append("\n");
        sb.Append("\n");
        for (int i = 0; i <= dt.Columns.Count - 1; i++)
        {
            sb.Append(((i == 0) ? "" : ",") + dt.Columns[i].ColumnName);
        }
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("\n");
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                sb.Append(((i == 0) ? "" : ",") + dr[i].ToString());
            }
        }

        Response.Write(sb.ToString());
        Response.End();
    }
}
