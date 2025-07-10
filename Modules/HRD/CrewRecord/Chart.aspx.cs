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

public partial class CrewRecord_Chart : System.Web.UI.Page
{
    System.Drawing.Font fnt_ChartHeading = new System.Drawing.Font("Verdana", 13, System.Drawing.FontStyle.Bold);
    int indiaid = 70;
    int myanmarid = 101;
    int philid = 116;

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
    }
    protected void ShowReport(int ReportType)
    {
        Chart_HSQE009.Legends.Add("LD");
        Chart_HSQE009.Legends["LD"].Title="Legend";

        if (ReportType == 1)
        {
            int[] newdata={0,0,0,0,0};
            DataTable dtindia_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            newdata[1]= Common.CastAsInt32(dtindia_new.Rows[0][0]);
            DataTable dtmyan_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            newdata[2]= Common.CastAsInt32(dtmyan_new.Rows[0][0]);
            DataTable dtphil_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            newdata[3]= Common.CastAsInt32(dtphil_new.Rows[0][0]);
            DataTable dtother_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            newdata[4]= Common.CastAsInt32(dtother_new.Rows[0][0]);

            newdata[0]= newdata[1]+newdata[2]+newdata[3]+newdata[4];

            int[] olddata={0,0,0,0,0};
            DataTable dtindia_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            olddata[1]= Common.CastAsInt32(dtindia_old.Rows[0][0]);
            DataTable dtmyan_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            olddata[2]= Common.CastAsInt32(dtmyan_old.Rows[0][0]);
            DataTable dtphil_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            olddata[3]= Common.CastAsInt32(dtphil_old.Rows[0][0]);
            DataTable dtother_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CURRENTRANKID NOT IN (48,49) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            olddata[4]= Common.CastAsInt32(dtother_old.Rows[0][0]);
            
            olddata[0]= olddata[1]+olddata[2]+olddata[3]+olddata[4];


            Chart_HSQE009.Series["New"].Points.Clear();

            Chart_HSQE009.Series["New"].Points.AddXY("ALL", newdata[0]);
            Chart_HSQE009.Series["New"].Points.AddXY("IND", newdata[1]);
            Chart_HSQE009.Series["New"].Points.AddXY("MYAMMAR", newdata[2]);
            Chart_HSQE009.Series["New"].Points.AddXY("PHIL", newdata[3]);
            Chart_HSQE009.Series["New"].Points.AddXY("OTHER", newdata[4]);

            Chart_HSQE009.Series["Old"].Points.Clear();

            Chart_HSQE009.Series["Old"].Points.AddXY("ALL", olddata[0]);
            Chart_HSQE009.Series["Old"].Points.AddXY("IND", olddata[1]);
            Chart_HSQE009.Series["Old"].Points.AddXY("MYAMMAR", olddata[2]);
            Chart_HSQE009.Series["Old"].Points.AddXY("PHIL", olddata[3]);
            Chart_HSQE009.Series["Old"].Points.AddXY("OTHER", olddata[4]);

            Chart_HSQE009.Titles.Add("Total No of Crew OnBoard - Old vs New");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
        }

        if (ReportType == 2)
        {
            int[] newdata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            newdata[1] = Common.CastAsInt32(dtindia_new.Rows[0][0]);
            DataTable dtmyan_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            newdata[2] = Common.CastAsInt32(dtmyan_new.Rows[0][0]);
            DataTable dtphil_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            newdata[3] = Common.CastAsInt32(dtphil_new.Rows[0][0]);
            DataTable dtother_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            newdata[4] = Common.CastAsInt32(dtother_new.Rows[0][0]);

            newdata[0] = newdata[1] + newdata[2] + newdata[3] + newdata[4];

            int[] olddata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            olddata[1] = Common.CastAsInt32(dtindia_old.Rows[0][0]);
            DataTable dtmyan_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            olddata[2] = Common.CastAsInt32(dtmyan_old.Rows[0][0]);
            DataTable dtphil_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            olddata[3] = Common.CastAsInt32(dtphil_old.Rows[0][0]);
            DataTable dtother_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            olddata[4] = Common.CastAsInt32(dtother_old.Rows[0][0]);

            olddata[0] = olddata[1] + olddata[2] + olddata[3] + olddata[4];


            Chart_HSQE009.Series["New"].Points.Clear();

            Chart_HSQE009.Series["New"].Points.AddXY("ALL", newdata[0]);
            Chart_HSQE009.Series["New"].Points.AddXY("IND", newdata[1]);
            Chart_HSQE009.Series["New"].Points.AddXY("MYAMMAR", newdata[2]);
            Chart_HSQE009.Series["New"].Points.AddXY("PHIL", newdata[3]);
            Chart_HSQE009.Series["New"].Points.AddXY("OTHER", newdata[4]);

            Chart_HSQE009.Series["Old"].Points.Clear();

            Chart_HSQE009.Series["Old"].Points.AddXY("ALL", olddata[0]);
            Chart_HSQE009.Series["Old"].Points.AddXY("IND", olddata[1]);
            Chart_HSQE009.Series["Old"].Points.AddXY("MYAMMAR", olddata[2]);
            Chart_HSQE009.Series["Old"].Points.AddXY("PHIL", olddata[3]);
            Chart_HSQE009.Series["Old"].Points.AddXY("OTHER", olddata[4]);

            Chart_HSQE009.Titles.Add("Officers OnBoard by Nationality - Old vs New");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
        }

        if (ReportType == 3)
        {
            int[] newdata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            newdata[1] = Common.CastAsInt32(dtindia_new.Rows[0][0]);
            DataTable dtmyan_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            newdata[2] = Common.CastAsInt32(dtmyan_new.Rows[0][0]);
            DataTable dtphil_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            newdata[3] = Common.CastAsInt32(dtphil_new.Rows[0][0]);
            DataTable dtother_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')=1) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            newdata[4] = Common.CastAsInt32(dtother_new.Rows[0][0]);

            newdata[0] = newdata[1] + newdata[2] + newdata[3] + newdata[4];

            int[] olddata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            olddata[1] = Common.CastAsInt32(dtindia_old.Rows[0][0]);
            DataTable dtmyan_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            olddata[2] = Common.CastAsInt32(dtmyan_old.Rows[0][0]);
            DataTable dtphil_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            olddata[3] = Common.CastAsInt32(dtphil_old.Rows[0][0]);
            DataTable dtother_old = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND ((SELECT COUNT(CREWEXPERIENCEID) FROM CREWEXPERIENCEDETAILS EX WHERE EX.CREWID=C.CREWID AND EXPFLAG='C')>1) AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            olddata[4] = Common.CastAsInt32(dtother_old.Rows[0][0]);

            olddata[0] = olddata[1] + olddata[2] + olddata[3] + olddata[4];


            Chart_HSQE009.Series["New"].Points.Clear();

            Chart_HSQE009.Series["New"].Points.AddXY("ALL", newdata[0]);
            Chart_HSQE009.Series["New"].Points.AddXY("IND", newdata[1]);
            Chart_HSQE009.Series["New"].Points.AddXY("MYAMMAR", newdata[2]);
            Chart_HSQE009.Series["New"].Points.AddXY("PHIL", newdata[3]);
            Chart_HSQE009.Series["New"].Points.AddXY("OTHER", newdata[4]);

            Chart_HSQE009.Series["Old"].Points.Clear();

            Chart_HSQE009.Series["Old"].Points.AddXY("ALL", olddata[0]);
            Chart_HSQE009.Series["Old"].Points.AddXY("IND", olddata[1]);
            Chart_HSQE009.Series["Old"].Points.AddXY("MYAMMAR", olddata[2]);
            Chart_HSQE009.Series["Old"].Points.AddXY("PHIL", olddata[3]);
            Chart_HSQE009.Series["Old"].Points.AddXY("OTHER", olddata[4]);

            Chart_HSQE009.Titles.Add("Ratings OnBoard by Nationality - Old vs New");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
        }
        if (ReportType == 4)
        {
            int[] newdata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            newdata[1] = Common.CastAsInt32(dtindia_new.Rows[0][0]);
            DataTable dtmyan_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            newdata[2] = Common.CastAsInt32(dtmyan_new.Rows[0][0]);
            DataTable dtphil_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            newdata[3] = Common.CastAsInt32(dtphil_new.Rows[0][0]);
            DataTable dtother_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='O') AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            newdata[4] = Common.CastAsInt32(dtother_new.Rows[0][0]);

            Chart_HSQE009.Series["New"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            Chart_HSQE009.Series["New"].Label = "#VALX #PERCENT \n ( #VALY ) ";
            Chart_HSQE009.Series["New"].Points.Clear();

            Chart_HSQE009.Series["New"].Points.AddXY("IND", newdata[1]);
            Chart_HSQE009.Series["New"].Points[0]["Exploded"] = "true";

            Chart_HSQE009.Series["New"].Points.AddXY("MYAMMAR", newdata[2]);
            Chart_HSQE009.Series["New"].Points[1]["Exploded"] = "true";

            Chart_HSQE009.Series["New"].Points.AddXY("PHIL", newdata[3]);
            Chart_HSQE009.Series["New"].Points[2]["Exploded"] = "true";

            Chart_HSQE009.Series["New"].Points.AddXY("OTHER", newdata[4]);
            Chart_HSQE009.Series["New"].Points[3]["Exploded"] = "true";

            Chart_HSQE009.Legends.RemoveAt(0);

            Chart_HSQE009.Series.Remove(Chart_HSQE009.Series["Old"]);

            Chart_HSQE009.Titles.Add("Officers OnBoard by Nationality");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
        }
        if (ReportType == 5)
        {
            int[] newdata = { 0, 0, 0, 0, 0 };
            DataTable dtindia_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND CREWSTATUSID=3 AND NATIONALITYID=" + indiaid);
            newdata[1] = Common.CastAsInt32(dtindia_new.Rows[0][0]);
            DataTable dtmyan_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND CREWSTATUSID=3 AND NATIONALITYID=" + myanmarid);
            newdata[2] = Common.CastAsInt32(dtmyan_new.Rows[0][0]);
            DataTable dtphil_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND CREWSTATUSID=3 AND NATIONALITYID=" + philid);
            newdata[3] = Common.CastAsInt32(dtphil_new.Rows[0][0]);
            DataTable dtother_new = Common.Execute_Procedures_Select_ByQueryCMS("SELECT count(*) FROM CREWPERSONALDETAILS C WHERE C.CURRENTRANKID IN (SELECT RANKID FROM RANK WHERE OFFCREW='R') AND CREWSTATUSID=3 AND NATIONALITYID NOT IN (" + indiaid + "," + myanmarid + "," + philid + ")");
            newdata[4] = Common.CastAsInt32(dtother_new.Rows[0][0]);

            Chart_HSQE009.Series["New"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
            Chart_HSQE009.Series["New"].Label = "#VALX #PERCENT \n ( #VALY ) ";
            //Chart_HSQE009.Series["New"]["PieLabelStyle"] = "outside";
            Chart_HSQE009.Series["New"].Points.Clear();

            Chart_HSQE009.Series["New"].Points.AddXY("IND", newdata[1]);
            Chart_HSQE009.Series["New"].Points[0]["Exploded"] = "true";
            
            Chart_HSQE009.Series["New"].Points.AddXY("MYAMMAR", newdata[2]);
            Chart_HSQE009.Series["New"].Points[1]["Exploded"] = "true";

            Chart_HSQE009.Series["New"].Points.AddXY("PHIL", newdata[3]);
            Chart_HSQE009.Series["New"].Points[2]["Exploded"] = "true";

            Chart_HSQE009.Series["New"].Points.AddXY("OTHER", newdata[4]);
            Chart_HSQE009.Series["New"].Points[3]["Exploded"] = "true";

            Chart_HSQE009.Legends.RemoveAt(0);

            Chart_HSQE009.Series.Remove(Chart_HSQE009.Series["Old"]);

            Chart_HSQE009.Titles.Add("Ratings OnBoard by Nationalit");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
        }
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        ShowReport(2);
    }
    protected void btn3_Click(object sender, EventArgs e)
    {
        ShowReport(3);
    }
    protected void btn4_Click(object sender, EventArgs e)
    {
        ShowReport(4);
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        ShowReport(5);
    }
}
