using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Threading; 

public partial class emtm_StaffAdmin_Emtm_Hr_EmployeewiseTrainingReport : System.Web.UI.Page
{
    static Random R = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string _NameOnly = "Training_EmpWise.pdf";
        string FileName = Server.MapPath(_NameOnly);
        
        if (!IsPostBack)
        {
            if (Request.QueryString["Year"] != null && Request.QueryString["Year"].ToString() != "")
            {
                CreateReport(FileName);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.open('" + _NameOnly + "?rand=" + R.NextDouble().ToString() + "');self.close();", true);
            }
        }
    }

    public void CreateReport(string FileName)
    {
        Document document = new Document(PageSize.A3.Rotate(), 10, 10, 30, 30);
        System.IO.MemoryStream msReport = new System.IO.MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, msReport);
        Phrase p1 = new Phrase();


        HeaderFooter header = new HeaderFooter(p1, false);
        document.Header = header;
        header.Alignment = Element.ALIGN_LEFT;
        header.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //'Adding Footer in document
        string foot_Txt = "";
        //foot_Txt = foot_Txt + "Approved By  : " + ApprejBy + "              Approved On : " + ApprejOn + "";
        HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 8, iTextSharp.text.Color.DARK_GRAY)), false);
        footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
        footer.Alignment = Element.ALIGN_LEFT;
        document.Footer = footer;
        //'-----------------------------------

        document.Open();
        //------------ TABLE HEADER FONT 
        iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fCapTextTitle = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fCapTextDetails = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
        iTextSharp.text.Font fCapTextCirNum = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.ITALIC);
        iTextSharp.text.Font fCapTextHeading = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
        iTextSharp.text.Font fCapTextCompheading = FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD);

        iTextSharp.text.Table tbHeader = new iTextSharp.text.Table(1);
        tbHeader.Border = Rectangle.NO_BORDER;
        tbHeader.Width = 100;
        tbHeader.DefaultCellBorderColor = Color.BLACK;
        tbHeader.DefaultCellBorderWidth = 1;
        tbHeader.Alignment = Element.ALIGN_BOTTOM;
        tbHeader.Cellspacing = 0;
        tbHeader.Cellpadding = 2;

        Cell tch = new Cell(new Phrase("List of Assigned Trainings \n < Year- " + Request.QueryString["Year"].ToString() + " >" , fCapTextHeading));
        tch.SetHorizontalAlignment("Center");
        tch.SetVerticalAlignment("Middle");
        tch.BackgroundColor = Color.LIGHT_GRAY;

        tbHeader.AddCell(tch);

        //iTextSharp.text.Table tb1 = new iTextSharp.text.Table(14);
        //tb1.Border = Rectangle.NO_BORDER;
        //tb1.Width = 100;
        //float[] ws1 = { 14, 14, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
        ////float[] ws1 = { 14, 14, 6, 6, 6 };
        //tb1.Widths = ws1;
        //tb1.DefaultCellBorderColor = Color.BLACK;
        //tb1.DefaultCellBorderWidth = 1;
        //tb1.Alignment = Element.ALIGN_BOTTOM;
        //tb1.Cellspacing = 0;
        //tb1.Cellpadding = 2;
        
        //Cell tcr11 = new Cell(new Phrase("Employee Name", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Position", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Jan", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Feb", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Mar", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Apr", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("May", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Jun", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Jul", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Aug", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Sep", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Oct", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Nov", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Dec", fCapText));
        //tb1.AddCell(tcr11);
        
        //string sql = "select empid,empcode,Firstname+ ' ' + middlename+ ' ' + familyname  as empname,pp.positionname " +
        //        "from Hr_PersonalDetails e inner join position pp on pp.positionid=e.position " +
        //        "where empid in  " +
        //        "( select distinct empid from dbo.HR_TrainingPlanningDetails d inner join dbo.HR_TrainingPlanning p on p.trainingplanningid=d.trainingplanningid where  year(startdate)=2012 ) " +
        //        "order by firstname + middlename ";
        //DataTable dtEmp = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //foreach (DataRow dr in dtEmp.Rows)
        //{
        //    int _EmpId = Common.CastAsInt32(dr["empid"].ToString());
        //    Cell tcr1 = new Cell(new Phrase(dr["EmpName"].ToString(), fCapTextDetails));
        //    tb1.AddCell(tcr1);
        //    tcr1 = new Cell(new Phrase(dr["positionname"].ToString(), fCapTextDetails));
        //    tb1.AddCell(tcr1);

        //    for (int i = 1; i <= 12; i++)
        //    {
        //        Cell tc = new Cell(new Phrase("", fCapTextDetails));
        //        iTextSharp.text.Table tb_inner = new iTextSharp.text.Table(1);
        //        tb_inner.Border = Rectangle.NO_BORDER;
        //        tb_inner.Width = 90;
        //        float[] ws11 = { 90 };
        //        tb_inner.Widths = ws11;
        //        string in_str = "select shortname,p.status from  " +
        //                        "dbo.HR_TrainingPlanningDetails d  " +
        //                        "inner join dbo.HR_TrainingPlanning p on p.trainingplanningid=d.trainingplanningid  " +
        //                        "inner join HR_TrainingMaster tm on tm.trainingid=p.trainingid " +
        //                        "where   " +
        //                        "year(startdate)=" + Request.QueryString["Year"].ToString() + " and month(startdate)=" + i.ToString() + " and empid=" + _EmpId.ToString() +
        //                        " order by p.status,shortname ";
        //        DataTable dtIn = Common.Execute_Procedures_Select_ByQueryCMS(in_str);

        //        foreach (DataRow dr1 in dtIn.Rows)
        //        {
        //            Cell tc1 = new Cell(new Phrase(dr1["shortname"].ToString().Trim(), fCapTextDetails));
        //            Color c;
        //            if (dr1["status"].ToString().ToUpper().Trim() == "E")
        //            {
        //                c = Color.GREEN;
        //            }
        //            else if (dr1["status"].ToString().ToUpper().Trim() == "C")
        //            {
        //                c = Color.RED;
        //            }
        //            else
        //            {
        //                c = Color.YELLOW;
        //            }
        //            tc1.Leading = 8;
        //            tc1.BackgroundColor = c;
        //            tb_inner.AddCell(tc1);
        //        }
        //        tc.AddElement(tb_inner);
        //        tb1.AddCell(tc);
        //    }
        //}


        iTextSharp.text.Table tb1 = new iTextSharp.text.Table(3);
        tb1.Border = Rectangle.NO_BORDER;
        tb1.Width = 100;
        float[] ws1 = { 14, 14, 72 };
        //float[] ws1 = { 14, 14, 6, 6, 6 };
        tb1.Widths = ws1;
        tb1.DefaultCellBorderColor = Color.BLACK;
        tb1.DefaultCellBorderWidth = 1;
        tb1.Alignment = Element.ALIGN_BOTTOM;
        tb1.Cellspacing = 0;
        tb1.Cellpadding = 2;

        Cell tcr11 = new Cell(new Phrase("Employee Name", fCapText));
        tb1.AddCell(tcr11);
        tcr11 = new Cell(new Phrase("Position", fCapText));
        tb1.AddCell(tcr11);
        tcr11 = new Cell(new Phrase("Trainings", fCapText));
        tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Feb", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Mar", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Apr", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("May", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Jun", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Jul", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Aug", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Sep", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Oct", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Nov", fCapText));
        //tb1.AddCell(tcr11);
        //tcr11 = new Cell(new Phrase("Dec", fCapText));
        //tb1.AddCell(tcr11);

        string sql = "select empid,empcode,Firstname+ ' ' + middlename+ ' ' + familyname  as empname,pp.positionname " +
                "from Hr_PersonalDetails e inner join position pp on pp.positionid=e.position " +
                "where empid in  " +
                 
                "( " +
                    "select distinct empid from HR_TrainingRecommended d where year(duedate)= " + Request.QueryString["Year"].ToString() + " "
                     + " union " +
                    "select distinct empid from dbo.HR_TrainingPlanningDetails d inner join dbo.HR_TrainingPlanning p on p.trainingplanningid=d.trainingplanningid where  year(startdate)= " + Request.QueryString["Year"].ToString()  +
                ") " +
                "order by firstname + middlename ";
        DataTable dtEmp = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        foreach (DataRow dr in dtEmp.Rows)
        {
            int _EmpId = Common.CastAsInt32(dr["empid"].ToString());
            Cell tcr1 = new Cell(new Phrase(dr["EmpName"].ToString(), fCapTextDetails));
            tb1.AddCell(tcr1);
            tcr1 = new Cell(new Phrase(dr["positionname"].ToString(), fCapTextDetails));
            tb1.AddCell(tcr1);

            //for (int i = 1; i <= 12; i++)
            //{
                Cell tc = new Cell(new Phrase("", fCapTextDetails));
                iTextSharp.text.Table tb_inner = new iTextSharp.text.Table(7);
                tb_inner.Border = Rectangle.NO_BORDER;
                tb_inner.Width = 100;
                float[] ws11 = { 10,10,10,10,10,10,12 };
                tb_inner.Widths = ws11;
                //string in_str = "select shortname,p.status,CASE Month(startdate) WHEN 1 THEN 'Jan' WHEN 2 THEN 'Feb' WHEN 3 THEN 'Mar' WHEN 4 THEN 'Apr' WHEN 5 THEN 'May' WHEN 6 THEN 'Jun' WHEN 7 THEN 'Jul' WHEN 8 THEN 'Aug' WHEN 9 THEN 'Sep' WHEN 10 THEN 'Oct' WHEN 11 THEN 'Nov' WHEN 12 THEN 'Dec' ELSE '' END AS [Month] from  " +
                //                "dbo.HR_TrainingPlanningDetails d  " +
                //                "inner join dbo.HR_TrainingPlanning p on p.trainingplanningid=d.trainingplanningid  " +
                //                "inner join HR_TrainingMaster tm on tm.trainingid=p.trainingid " +
                //                "where   " +
                //                "year(startdate)=" + Request.QueryString["Year"].ToString() + " and empid=" + _EmpId.ToString() +
                //                " order by p.status,shortname ";
                string in_str = "select *,left(datename(m,StatusDate),3) as StatusMonth " +
                                "from (  " +
	                            "   select empid,shortname,isnull(aa.status,'') as Status,  " +
	                            "   StatusDate=case when aa.status is null then Duedate when aa.status = 'E' THEN Startdate1 when aa.status = 'C' THEN CancelledOn else Startdate end  " +
                                "        from  dbo.HR_TrainingRecommended r  " +
		                        "        left join   " +
		                        "        (      " +
			                    "            select d.TrainingRecommId,startdate,startdate1,p.status,p.CancelledOn from dbo.HR_TrainingPlanningDetails d  " +
				                "                inner join dbo.HR_TrainingPlanning p on p.trainingplanningid=d.trainingplanningid    " +
		                        "        )  aa on r.TrainingRecommId=aa.TrainingRecommId  " +
                                "    inner join HR_TrainingMaster tm on tm.trainingid=r.trainingid  " +
                                ") tab " + 
                                "where   " +
                                "year(StatusDate)=" + Request.QueryString["Year"].ToString() + " and empid=" + _EmpId.ToString() +
                                " order by StatusDate,shortname ";
                DataTable dtIn = Common.Execute_Procedures_Select_ByQueryCMS(in_str);

                foreach (DataRow dr1 in dtIn.Rows)
                {
                    Cell tc1 = new Cell(new Phrase(dr1["shortname"].ToString().Trim() + " - " + dr1["StatusMonth"].ToString().Trim(), fCapTextDetails));
                    Color c;
                    if (dr1["status"].ToString().ToUpper().Trim() == "E")
                    {
                        c = Color.GREEN;
                    }
                    else if (dr1["status"].ToString().ToUpper().Trim() == "C")
                    {
                        c = Color.RED;
                    }
                    else if (dr1["status"].ToString().ToUpper().Trim() == "A")
                    {
                        c = Color.YELLOW;
                    }
                    else
                    {
                        c = Color.ORANGE;
                    }
                    tc1.Leading = 8;
                    tc1.BackgroundColor = c;
                    tb_inner.AddCell(tc1);
                }
                tc.AddElement(tb_inner);
                tb1.AddCell(tc);
            //}
        }


        //iTextSharp.text.Table tb_main = new iTextSharp.text.Table(2);
        //tb_main.Border = Rectangle.NO_BORDER;
        //tb_main.DefaultCellBorderColor = Color.BLACK;
        //tb_main.DefaultCellBorderWidth = 1;
        //Cell C1 = new Cell("SS");
        //tb_main.AddCell(C1);
        //tb_main.AddCell(C1);
        //tb_main.AddCell(C1);
        //tb_main.AddCell(C1);

        document.Add(tbHeader);

        document.Add(tb1);
        document.NewPage();
        document.Close();

        if (File.Exists(FileName))
        {
            File.Delete(FileName);
        }

        FileStream fs = new FileStream(FileName, FileMode.Create);

        byte[] bb = msReport.ToArray();
        fs.Write(bb, 0, bb.Length);
        fs.Flush();
        fs.Close();
  
    }
}