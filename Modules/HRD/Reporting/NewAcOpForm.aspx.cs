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

public partial class NewAcOpForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 183);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        if (!IsPostBack)
        {
            for (int i = 0; i < 10; i++)
            {
                ddl_Year.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - i), Convert.ToString(DateTime.Today.Year - i)));
            }
        }
        //*******************
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime dt1 = DateTime.Today, dt2 = DateTime.Today;
        switch (ddl_Quarter.SelectedIndex)
        {
            case 0:
                dt1 = DateTime.Parse("01/01/" + ddl_Year.SelectedValue);
                dt2 = DateTime.Parse("03/31/" + ddl_Year.SelectedValue);
                break;
            case 1:
                dt1 = DateTime.Parse("04/01/" + ddl_Year.SelectedValue);
                dt2 = DateTime.Parse("06/30/" + ddl_Year.SelectedValue);
                break;
            case 2:
                dt1 = DateTime.Parse("07/01/" + ddl_Year.SelectedValue);
                dt2 = DateTime.Parse("09/30/" + ddl_Year.SelectedValue);
                break;
            case 3:
                dt1 = DateTime.Parse("10/01/" + ddl_Year.SelectedValue);
                dt2 = DateTime.Parse("12/31/" + ddl_Year.SelectedValue);
                break;
            default:
                break; 
        }
        StringBuilder sb = new StringBuilder();  
        //string Query = "select FirstName + ' ' + MiddleName + ' ' + LastName as UnionMemberName,'' as ThriftID,'' as UnionNo, " +
        //                "(select Smou_code from rank where rank.rankid=CurrentRankId)as UnionCode, " +
        //                "'' as iSPFNo,cpd.CrewNumber as CrewId, " +
        //                "(select LRIMONumber from vessel where vessel.vesselid=gch.vesselid) as IMONo, " +
        //                "(select left(convert(varchar,startdate,113),11) from crewcontractheader where contractid=gch.contractid) as SignIn, " +
        //                "left(convert(varchar,isnull(gch.singoffdate,'           '),113),11) as SignOut, " +
        //                "isnull((select top 1 documentnumber from crewtraveldocument where documenttypeid=2 and crewid=gch.crewid),'') as PassportNo, " +
        //                "(select Address1 + ' '  + Address2 + ' ' + Address3 + ' ' + City + (case when ltrim(rtrim(pinCode))='' then '' else '-' + pinCode end) + ', ' + (select countryname from country where country.countryid=ccd.countryid) from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as UnionMemberAddress, " +
        //                "(select Email1 from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as EmailAddress, " +
        //                "(case when cpd.SexType=1 then 'M' else 'F' end) as  Gender, " +
        //                "replace(convert(Varchar,DateOfBirth,106),' ','-') as DOB, " +
        //                "(select NationalityCode from Country where Country.CountryId=cpd.NationalityId)as NationalityCode, " +
        //                "(select RankName from rank where rank.rankid=CurrentRankId)as RankDescription " +
        //                "from get_crew_history gch inner join crewpersonaldetails cpd on gch.crewid=cpd.crewid " +
        //                "WHERE  " +
        //                "((select startdate from crewcontractheader where contractid=gch.contractid) between '" + txt_from.Text + "' and '" + txt_to.Text + "')  " +
        //                "And " +
        //                "gch.VesselId In(select Vesselid from vessel Where FlagStateId=128) " +
        //                "order by FirstName + ' ' + MiddleName + ' ' + LastName";

         string Query = "select FirstName + ' ' + MiddleName + ' ' + LastName as UnionMemberName,'' as ThriftID,'' as UnionNo, " +
                        "isnull((select Smou_code from rank where rank.rankid=CurrentRankId),'') as UnionCode, " +
                        "'' as iSPFNo,cpd.CrewNumber as CrewId, " +
                        "(select LRIMONumber from vessel where vessel.vesselid=gch.vesselid) as IMONo, " +
                        "case when startdate >  convert(smalldatetime,'" + dt1.ToString("MM/dd/yyyy") + "') then  " +
                        "replace(left(convert(varchar,gch.startdate,113),11),' ','-') " +
                        "else " +
                        "replace(left(convert(varchar,convert(smalldatetime,'" + dt1.ToString("MM/dd/yyyy") + "'),113),11),' ','-') end as StartDate, " +

                        "case " +
                        "when gch.singoffdate is null then replace(left(convert(varchar,convert(smalldatetime,'" + dt2.ToString("MM/dd/yyyy") + "'),113),11),' ','-') " +
                        "when gch.singoffdate > convert(smalldatetime,'" + dt2.ToString("MM/dd/yyyy") + "') then replace(left(convert(varchar,convert(smalldatetime,'" + dt2.ToString("MM/dd/yyyy") + "'),113),11),' ','-') " +
                        "else " +
                        "isnull(replace(convert(varchar,gch.singoffdate,113),'00:00:00:000',''),replace(left(convert(varchar,'" + dt2.ToString("MM/dd/yyyy") + "',113),11),' ','-')) end as SignOut, " +

                        "isnull((select top 1 documentnumber from crewtraveldocument where documenttypeid=0 and crewid=gch.crewid),'') as PassportNo, " +
                        "(select Address1 + ' '  + Address2 + ' ' + Address3 + ' ' + City + (case when ltrim(rtrim(pinCode))='' then '' else '-' + pinCode end) + ', ' + (select countryname from country where country.countryid=ccd.countryid) from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as UnionMemberAddress, " +
                        "(select Email1 from crewContactdetails ccd where addresstype='C' and ccd.crewid=cpd.crewid) as EmailAddress, " +
                        "(case when cpd.SexType=1 then 'M' else 'F' end) as  Gender, " +
                        "replace(convert(Varchar,DateOfBirth,106),' ','-') as DOB, " +
                        "(select NationalityCode from Country where Country.CountryId=cpd.NationalityId)as NationalityCode, " +
                        "(select RankName from rank where rank.rankid=CurrentRankId)as RankDescription " +
                        "from  " +
                        "(select *,(select startdate from crewcontractheader where crewcontractheader.contractid=get_crew_history.contractid) as StartDate " +
                        "from get_crew_history) gch  " +
                        "inner join crewpersonaldetails cpd on gch.crewid=cpd.crewid " +
                        "WHERE  " +
                        "(('" + dt1.ToString("MM/dd/yyyy") + "' between gch.StartDate and gch.SingOffDate) or ('" + dt1.ToString("MM/dd/yyyy") + "' between gch.StartDate and gch.SingOffDate) or ('" + dt1.ToString("MM/dd/yyyy") + "' <=gch.SignOnDate and '" + dt2.ToString("MM/dd/yyyy") + "'>=gch.SingOffDate) or ('" + dt2.ToString("MM/dd/yyyy") + "' >= gch.StartDate and gch.SingOffDate is NULL)) " +
                        "And " +
                        "gch.VesselId In(select Vesselid from vessel Where FlagStateId=128) " +
                        "order by FirstName + ' ' + MiddleName + ' ' + LastName";
        DataTable dt = Budget.getTable(Query).Tables[0];
        Response.Clear();
        Response.ContentType = "application/xls";
        Response.AddHeader("Content-Disposition", "attachment;filename=New Account Opening Form.xls");
        sb.Append("<table border='1'>");
        sb.Append("<tr>");
        for (int i = 0; i <= dt.Columns.Count - 1; i++)
        {
            sb.Append("<td>");
            sb.Append(dt.Columns[i].ColumnName);
            sb.Append("</td>");
        }
        sb.Append("</tr>");
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                sb.Append("<td>");
                if (i == 7 || i == 8 || i == 13) sb.Append("&nbsp;");
                sb.Append(dr[i].ToString());
                sb.Append("</td>");
            }
            sb.Append("</tr>");    
        }
        sb.Append("</table>"); 
        Response.Write(sb.ToString());
        Response.End();  
   }
}
