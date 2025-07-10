using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;


public class RandomColor
{
    Random _random;
    public RandomColor()
    {
        _random = new Random();
    }
    public Color GetNext()
    {
        return Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
    }
}

public partial class Vetting_Chart : System.Web.UI.Page
{
    Random r = new Random();
    RandomColor rc=new RandomColor();
    System.Drawing.Font fnt_ChartHeading = new System.Drawing.Font("Verdana", 13, System.Drawing.FontStyle.Bold);
    StringFormat sf_Center = new StringFormat();        

    public string ReportType
    {
        set { ViewState["ReportType"] = value; }
        get { return ViewState["ReportType"].ToString(); }
    }
    public string Critera
    {
        set { ViewState["Critera"] = value; }
        get { return ViewState["Critera"].ToString(); }
    }
    public int InspectionGroupId
    {
        set { ViewState["InspectionGroup"] = value; }
        get { return Common.CastAsInt32(ViewState["InspectionGroup"]); }
    }
    public int ReportLevel
    {
        set { ViewState["ReportLevel"] = value; }
        get { return Common.CastAsInt32(ViewState["ReportLevel"]); }
    }
    public int InspectionId
    {
        set { ViewState["InspectionId"] = value; }
        get { return Common.CastAsInt32(ViewState["InspectionId"]); }
    }
    public string VesselIds
    {
        set { ViewState["VesselIds"] = value; }
        get { return ViewState["VesselIds"].ToString(); }
    }
    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return ViewState["Mode"].ToString(); }
    }

    Font f_count = new Font("Arial", 20, FontStyle.Regular, GraphicsUnit.Pixel);
    Font f_month = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel);
    Font f_header = new Font(FontFamily.GenericMonospace, 22, FontStyle.Bold, GraphicsUnit.Pixel);
    Font f_header_large = new Font(FontFamily.GenericMonospace, 35, FontStyle.Bold, GraphicsUnit.Pixel);

    Font f_header_ARIAL = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);

    public string Year
    {
        set { ViewState["Year"] = value; }
        get { return ViewState["Year"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        sf_Center.Alignment = StringAlignment.Center;

        ReportType =Request.QueryString["ReportType"];
        Critera = Request.QueryString["Critera"];
        InspectionGroupId = Common.CastAsInt32(Request.QueryString["InspectionGroupId"]);
        ReportLevel =Common.CastAsInt32(Request.QueryString["ReportLevel"]);
        InspectionId = Common.CastAsInt32(Request.QueryString["InspectionId"]);
        VesselIds=Request.QueryString["VesselIds"];
        Year = Request.QueryString["Year"];
        Mode = Request.QueryString["Mode"];

        //FromDate = Request.QueryString["FromDate"];
        //ToDate = Request.QueryString["ToDate"];
        if (ReportType == "S1")
        {
            BindChart1();
        }
        if (ReportType == "S2")
        {
            BindChart2();
        }
        if (ReportType == "S3")
        {
            BindChart3();
        }
        if (ReportType == "S4")
        {
            BindChart4();
        }
        if (ReportType == "S5")
        {
            BindChart5();
        }
        if (ReportType == "S6")
        {
            BindChart6();
        }
        if (ReportType == "S7")
        {
            BindChart7();
        }
    }

    public void BindChart1()
    {
        pnl_Chart1.Visible = true;
        string ReportLevelText = "";
        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();
        //if (FromDate.Trim()!="")
        //DateClause=" ActualDate >='" + FromDate + "' ";

        //if (ToDate.Trim() != "")
        //{
        //    DateClause += ((FromDate.Trim() == "") ? "" : " AND ") + " ActualDate <='" + ToDate + "' ";
        //}
        //if (DateClause.Trim() != "")
        //    DateClause = " AND ( " + DateClause + " )";
        //===================
        DataTable dt_Ins = Common.Execute_Procedures_Select_ByQuery("select " + Year + " as Year,month(actualdate) as InspMonth,Code,Color,count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null group by month(actualdate),Code,Color order by month(actualdate)");
        if (Mode == "D")
        {
            if (dt_Ins.Columns.Contains("Color"))
                dt_Ins.Columns.Remove("Color");
            ProjectCommon.ExportDatatable(Response, dt_Ins, "Data");
            return; 
        }
        List<string> Lst_X_Axis=new List<string>();
        if(ReportLevel==0)
        {
            Lst_X_Axis.Add("JAN");
            Lst_X_Axis.Add("FEB");
            Lst_X_Axis.Add("MAR");
            Lst_X_Axis.Add("APR");
            Lst_X_Axis.Add("MAY");
            Lst_X_Axis.Add("JUN");
            Lst_X_Axis.Add("JUL");
            Lst_X_Axis.Add("AUG");
            Lst_X_Axis.Add("SEP");
            Lst_X_Axis.Add("OCT");
            Lst_X_Axis.Add("NOV");
            Lst_X_Axis.Add("DEC");

            ReportLevelText = "[ " +Year + " ] Monthly ";
           
        }
        if(ReportLevel==1)
        {
            Lst_X_Axis.Add("Q1");
            Lst_X_Axis.Add("Q2");
            Lst_X_Axis.Add("Q3");
            Lst_X_Axis.Add("Q4");

            ReportLevelText = "[ " + Year + " ] Quarterly - ";
        }
        if (ReportLevel == 2)
        {
            Lst_X_Axis.Add("H1");
            Lst_X_Axis.Add("H2");

            ReportLevelText = "[ " + Year + " ] Half - Yearly";
        }
        if (ReportLevel == 3)
        {
            Lst_X_Axis.Add(Year);
            ReportLevelText = "[ " + Year + " ] Yearly";
        }
        
        //===================
        DataView dv = dt_Ins.DefaultView;
        int[][] Data =  new int[Lst_X_Axis.Count][];
        string[][] Data_Text = new string[Lst_X_Axis.Count][];
        string[][] Color = new string[Lst_X_Axis.Count][];
        
        if (ReportLevel == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                dv.RowFilter = "InspMonth=" + (i+1).ToString();
                DataTable dt = dv.ToTable();
                List<string> InspNames = new List<string>();
                List<int> InspCount = new List<int>();
                List<string> InspColor = new List<string>();

                foreach (DataRow dr1 in dt.Rows)
                {
                    InspNames.Add(dr1["Code"].ToString());
                    InspCount.Add(Common.CastAsInt32(dr1["Insp_Count"]));
                    InspColor.Add(dr1["Color"].ToString());
                }

                Data[i]=InspCount.ToArray();
                Data_Text[i] = InspNames.ToArray();
                Color[i] = InspColor.ToArray();
            }
        }
        else if (ReportLevel == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 3).ToString() + " And InspMonth <=" + ((i * 3) + 3).ToString();
                DataTable dt = getDistinct_Inspection_Sum(dv.ToTable());
                List<string> InspNames = new List<string>();
                List<int> InspCount = new List<int>();
                List<string> InspColor = new List<string>();

                foreach (DataRow dr1 in dt.Rows)
                {
                    InspNames.Add(dr1["Code"].ToString());
                    InspCount.Add(Common.CastAsInt32(dr1["Insp_Count"]));
                    InspColor.Add(dr1["Color"].ToString());
                }

                Data[i] = InspCount.ToArray();
                Data_Text[i] = InspNames.ToArray();
                Color[i] = InspColor.ToArray();
            }
        }
        else if (ReportLevel == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
                DataTable dt = getDistinct_Inspection_Sum(dv.ToTable());
                List<string> InspNames = new List<string>();
                List<int> InspCount = new List<int>();
                List<string> InspColor = new List<string>();

                foreach (DataRow dr1 in dt.Rows)
                {
                    InspNames.Add(dr1["Code"].ToString());
                    InspCount.Add(Common.CastAsInt32(dr1["Insp_Count"]));
                    InspColor.Add(dr1["Color"].ToString());
                }

                Data[i] = InspCount.ToArray();
                Data_Text[i] = InspNames.ToArray();
                Color[i] = InspColor.ToArray();
            }
        }
        else if (ReportLevel == 3)
        {
            //dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
            DataTable dt = getDistinct_Inspection_Sum(dt_Ins);//dv.ToTable();
            List<string> InspNames = new List<string>();
            List<int> InspCount = new List<int>();
            List<string> InspColor = new List<string>();

            foreach (DataRow dr1 in dt.Rows)
            {
                InspNames.Add(dr1["Code"].ToString());
                InspCount.Add(Common.CastAsInt32(dr1["Insp_Count"]));
                InspColor.Add(dr1["Color"].ToString());
            }

            Data[0] = InspCount.ToArray();
            Data_Text[0] = InspNames.ToArray();
            Color[0] = InspColor.ToArray();
        } 

        

        //{
        //                    new int[] { 1, 8, 1 },
        //                    new int[] { 1, 1 },
        //                    new int[] { 1 },
        //                };
        
        //string[][] Data_Text =  {
        //                    new string[] { "SHELL", "BP", "ADNOC" },
        //                    new string[] { "ADNOC", "SHELL" },
        //                    new string[] { "SHELL" },
        //                };

        string[] X_Axis = Lst_X_Axis.ToArray();


        int TotalInspections = 0;
        float MaxRecords = 0;
        for (int i = 0; i < Data.Length; i++)
        {
            int Sum_Inner = Data[i].Sum(a => a);
            if (Sum_Inner > MaxRecords)
                MaxRecords = Sum_Inner;

            TotalInspections += Sum_Inner;
        }

        if (TotalInspections == 0)
        {
            ProjectCommon.ShowMessage("No data found under this criteria.");
            return;
        }

        //===================
      
        //const int dotsPerInch = 100;    // define the quality in DPI
        //const double widthInInch = 12;   // width of the bitmap in INCH
        //const double heightInInch = 8;


        //int Width =(int) widthInInch * dotsPerInch;
        //int Height = (int) heightInInch * dotsPerInch;

        int TopMargin = 120;
        int LeftMargin = 30;
        int RightMargin = 30;
        int BottomMargin = 70;
        
        int ColWidth = 140;

        int Height =(int)(30*MaxRecords) + TopMargin + BottomMargin;
        if(Height<900)
            Height = 900;

        int Width = 2400;

      

        Bitmap b = new Bitmap(Width, Height);
        //b.SetResolution(300, 300);
        Graphics g = Graphics.FromImage(b);

        g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
        RectangleF r_Content = new RectangleF(0 + LeftMargin, 0 + TopMargin, Width - RightMargin - LeftMargin, Height - BottomMargin - TopMargin);

       
        //g.FillRectangle(Brushes.Green, r_Content);
        int NoOfCols = Data.Length;
        int TotalSepWidth = (NoOfCols) * ColWidth ;

        float ColSpacing = (r_Content.Width - TotalSepWidth) / (NoOfCols+1);
        //ColWidth = 140;

        //float ColWidth = (r_Content.Width - TotalSepWidth )/ NoOfCols;
        //ColWidth = 140;

        g.DrawString("Total No of " + ((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Inspections", f_header_large, Brushes.Black, new PointF((Width - RightMargin - LeftMargin) / 2, 10), sf_Center);
        g.DrawString(ReportLevelText + " Report ( " + Critera + " )", f_header, Brushes.Black, new PointF((Width - RightMargin - LeftMargin) / 2, 50), sf_Center);
        g.DrawString(" ( " + TotalInspections.ToString() + " ) Inspections", f_header, Brushes.Black, new PointF((Width - RightMargin - LeftMargin) / 2, 80), sf_Center);

        decimal No_Of_GridLines = (decimal)MaxRecords;// Math.Ceiling((decimal)(r_Content.Height / MaxRecords));
        decimal V_Gap_Gridlines = ((decimal)r_Content.Height / No_Of_GridLines);
        for( int c=0;c<=No_Of_GridLines;c++)
        {
            g.DrawLine(Pens.LightGray, r_Content.X, TopMargin + (c * (float)V_Gap_Gridlines), r_Content.Width, TopMargin + (c * (float)V_Gap_Gridlines));
            g.DrawString((No_Of_GridLines - c).ToString(), f_count, Brushes.Black, new PointF(r_Content.X, TopMargin + (c * (float)V_Gap_Gridlines)));
        }

        for( int c=0;c<NoOfCols;c++)
        {
            RectangleF rCol = new RectangleF(LeftMargin + ((c + 1) * ColSpacing) + (c * ColWidth), TopMargin, ColWidth, Height-BottomMargin-TopMargin);
            //g.FillRectangle(Brushes.Blue,rCol);

            int ArraySum = 0;
            ArraySum = (Data[c].Sum(a => a));
            decimal EndPoint = Height - BottomMargin;
            
            EndPoint=EndPoint- ( ArraySum * V_Gap_Gridlines );

            float ystart= (float)EndPoint;

            for (int c1 = 0; c1 < Data[c].Length; c1++)
            {
                float height = (float)(V_Gap_Gridlines * Data[c][c1]);
                string InspColor = "#" + Color[c][c1];
                if (InspColor == "#")
                    InspColor = "#000000";
                g.FillRectangle(new SolidBrush(System.Drawing.ColorTranslator.FromHtml(InspColor)), LeftMargin + ((c + 1) * ColSpacing) + (c * ColWidth), ystart, (float)ColWidth, height);
                int BoxMiddle = (int)(ystart + (height/2)) - 13;
                g.DrawString(Data_Text[c][c1] + "-" + Data[c][c1].ToString() , f_count, Brushes.White, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), BoxMiddle), sf_Center);
                ystart=ystart+height;
            }

            g.DrawString(X_Axis[c], f_month, Brushes.Blue, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), Height - BottomMargin), sf_Center);
            g.DrawString("(" + ArraySum.ToString() + ") ", f_month, Brushes.Blue, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), Height - BottomMargin + 30), sf_Center);
            
        }

        imgChart.ImageUrl = "chart.jpg";
        b.Save(Server.MapPath("~\\Vetting\\chart.jpg"), ImageFormat.Jpeg);
        string s = r.NextDouble().ToString();
        imgChart.ImageUrl = "chart.jpg?" + s;
    }
    public void BindChart2()
    {
        pnl_Chart2.Visible = true;

        string ReportLevelText = "";
        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();

        DataTable dt_Ins = Common.Execute_Procedures_Select_ByQuery("select " + Year + " as Year,month(actualdate) as InspMonth,Code,Color,count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null group by month(actualdate),Code,Color order by month(actualdate)");
        if (Mode == "D")
        {
            ProjectCommon.ExportDatatable(Response, dt_Ins, "Data");
            return;
        }
        List<string> Lst_X_Axis = new List<string>();
        List<int> Lst_Y_Axis = new List<int>();
        if (ReportLevel == 0)
        {
            Lst_X_Axis.Add("JAN");
            Lst_X_Axis.Add("FEB");
            Lst_X_Axis.Add("MAR");
            Lst_X_Axis.Add("APR");
            Lst_X_Axis.Add("MAY");
            Lst_X_Axis.Add("JUN");
            Lst_X_Axis.Add("JUL");
            Lst_X_Axis.Add("AUG");
            Lst_X_Axis.Add("SEP");
            Lst_X_Axis.Add("OCT");
            Lst_X_Axis.Add("NOV");
            Lst_X_Axis.Add("DEC");

            ReportLevelText = "[ " + Year + " ] Monthly ";

        }
        if (ReportLevel == 1)
        {
            Lst_X_Axis.Add("Q1");
            Lst_X_Axis.Add("Q2");
            Lst_X_Axis.Add("Q3");
            Lst_X_Axis.Add("Q4");

            ReportLevelText = "[ " + Year + " ] Quarterly - ";
        }
        if (ReportLevel == 2)
        {
            Lst_X_Axis.Add("H1");
            Lst_X_Axis.Add("H2");

            ReportLevelText = "[ " + Year + " ] Half - Yearly";
        }
        if (ReportLevel == 3)
        {
            Lst_X_Axis.Add(Year);
            ReportLevelText = "[ " + Year + " ] Yearly";
        }

        //===================
        DataView dv = dt_Ins.DefaultView;
        
        if (ReportLevel == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                dv.RowFilter = "InspMonth=" + (i + 1).ToString();
                DataTable dt = dv.ToTable();
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("SUM(INSP_COUNT)", "")));
            }
        }
        else if (ReportLevel == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 3).ToString() + " And InspMonth <=" + ((i * 3) + 3).ToString();
                DataTable dt = getDistinct_Inspection_Sum(dv.ToTable());
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("SUM(INSP_COUNT)", "")));

            }
        }
        else if (ReportLevel == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
                DataTable dt = getDistinct_Inspection_Sum(dv.ToTable());
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("SUM(INSP_COUNT)", "")));

          }
        }
        else if (ReportLevel == 3)
        {
            //dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
            DataTable dt = getDistinct_Inspection_Sum(dt_Ins);//dv.ToTable();
            Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("SUM(INSP_COUNT)", "")));
       }

        string[] X_Axis = Lst_X_Axis.ToArray();

        //===================

       

        Chart2.Series["Series1"].Points.Clear();
        Chart2.Series["Series1"].LegendText = "No of Inspections";
        int c=0;
        foreach (string Item in Lst_X_Axis)
        {
            Chart2.Series["Series1"].Points.AddXY(Item, Lst_Y_Axis[c].ToString());
            c++;
        }

        //Chart2.Series.Add("TrendLine");
        //Chart2.Series["TrendLine"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        //Chart2.Series["TrendLine"].BorderWidth = 1;
        //Chart2.Series["TrendLine"].Color = Color.Green;
        //// Line of best fit is linear
        //string typeRegression = "Linear";//"Exponential";//
        //// The number of days for Forecasting
        //string forecasting = "1";
        //// Show Error as a range chart.
        //string error = "false";
        //// Show Forecasting Error as a range chart.
        //string forecastingError = "false";
        //// Formula parameters
        //string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
        //Chart2.Series[0].Sort(System.Web.UI.DataVisualization.Charting.PointSortOrder.Ascending, "X");
        //// Create Forecasting Series.
        //Chart2.DataManipulator.FinancialFormula(System.Web.UI.DataVisualization.Charting.FinancialFormula.Forecasting, parameters, Chart2.Series[0], Chart2.Series["TrendLine"]);


        Chart2.Titles.Add("No Of " + ((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Inspections");
        Chart2.Titles.Add(ReportLevelText + " Report ( " + Critera + " )");
        Chart2.Titles[0].Font = fnt_ChartHeading;
    }
    public void BindChart3()
    {
        pnl_Chart3.Visible = true;

        string ReportLevelText = "";
        //if (ReportLevel == 0)
        //{
        //    ReportLevelText = "[ " + Year + " ] Monthly ";
        //}
        //if (ReportLevel == 1)
        //{
        //    ReportLevelText = "[ " + Year + " ] Quarterly - ";
        //}
        //if (ReportLevel == 2)
        //{
        //    ReportLevelText = "[ " + Year + " ] Half - Yearly";
        //}
        //if (ReportLevel == 3)
        //{
        //    ReportLevelText = "[ " + Year + " ] Yearly";
        //}

        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();

        DataTable dt_Ins_Cleared = Common.Execute_Procedures_Select_ByQuery("select count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null and INSPECTIONCLEARED=1");
        DataTable dt_Ins_Pending = Common.Execute_Procedures_Select_ByQuery("select count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null and INSPECTIONCLEARED is NULL");
        DataTable dt_Ins_Failed = Common.Execute_Procedures_Select_ByQuery("select count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null and INSPECTIONCLEARED=0");


        int Completed=Common.CastAsInt32(dt_Ins_Cleared.Rows[0][0]);
        int Pending = Common.CastAsInt32(dt_Ins_Pending.Rows[0][0]);
        int Failed = Common.CastAsInt32(dt_Ins_Failed.Rows[0][0]);

        int Total = Completed + Pending + Failed;

        if (Total == 0)
        {
            ProjectCommon.ShowMessage("No data found under this criteria.");
            return;
        }

        double CompletedPer =Math.Round(( Completed * 100.0 / Total),2);
        double FailedPer = Math.Round((Failed * 100.0 / Total),2);
        double PendingPer = 100.0 - CompletedPer - FailedPer;

        float PendingAngle = 0;
        float FailedAngle = 0;

        try { PendingAngle = (float)(360 * PendingPer / 100); }
        catch { }
        try { FailedAngle = (float)(360 * FailedPer / 100); }
        catch { }

        int TopMargin = 70;
        int LeftMargin = 120;
        int RightMargin = 120;
        int BottomMargin = 50;

        int Height = 600;
        int Width = 1200;

        int InnerWidth = Width - LeftMargin - RightMargin;
        int InnerHeight = Height - TopMargin - BottomMargin;

        Bitmap b = new Bitmap(Width, Height);
        Graphics g = Graphics.FromImage(b);
        g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
        Point p_center = new Point(LeftMargin + (InnerWidth) / 2, TopMargin + (InnerHeight) / 2);
        
        int Diameter=350;
        int Radius = Diameter/2;

        Rectangle r_circle=new Rectangle(p_center.X - Radius, p_center.Y - Radius, Diameter, Diameter);
        g.FillEllipse(Brushes.Green,r_circle );

        if (PendingAngle > 0)
            g.FillPie(Brushes.Orange, r_circle, 0, PendingAngle);
        if (FailedAngle > 0)
        {
            if(PendingAngle>0)
                g.FillPie(Brushes.Red, r_circle, PendingAngle, FailedAngle);
            else
                g.FillPie(Brushes.Red, r_circle, PendingAngle, FailedAngle);
        }

        CompletedPer = Math.Round(CompletedPer);
        PendingPer = Math.Round(PendingPer);
        FailedPer = Math.Round(FailedPer);

        g.DrawString(((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Inspections Results", f_header_large, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 10), sf_Center);
        g.DrawString(ReportLevelText + " Report ( " + Critera + " )", f_header, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 50), sf_Center);
        g.DrawString(" ( " + Total.ToString() + " ) Inspections", f_header, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 80), sf_Center);

        g.DrawString(CompletedPer + "% - " + Completed + " Inspections [ APPROVED ] ", f_header_ARIAL, Brushes.Green, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight -50), sf_Center);
        g.DrawString(PendingPer + "% - " + Pending + " Inspections [ AWAITING RESULTS ]", f_header_ARIAL, Brushes.Orange, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight - 20), sf_Center);
        g.DrawString(FailedPer + "% - " + Failed + " Inspections [ NOT APPROVED ]", f_header_ARIAL, Brushes.Red, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight + 10), sf_Center);

        imgChart.ImageUrl = "chart.jpg";
        b.Save(Server.MapPath("~\\Vetting\\chart.jpg"), ImageFormat.Jpeg);
        string s = r.NextDouble().ToString();
        imgChart3.ImageUrl = "chart.jpg?" + s;



        //Chart2.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
        //Chart2.Series["Series1"].Points.Clear();

        //Chart2.Series["Series1"].Points.AddXY("Pass", dt_Ins_Cleared.Rows[0][0]);
        //Chart2.Series["Series1"].Points.AddXY("Awaiting Results", dt_Ins_Pending.Rows[0][0]);
        ////int c = 0;
        ////foreach (string Item in Lst_X_Axis)
        ////{
        ////    Chart2.Series["Series1"].Points.AddXY(Item, Lst_Y_Axis[c].ToString());
        ////    c++;
        ////}


        //Chart2.Titles.Add(((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Inspections Results");
        //Chart2.Titles.Add(ReportLevelText + " Report ( " + Critera + " )");
        //Chart2.Titles[0].Font = fnt_ChartHeading;
    }
    public void BindChart4()
    {
        pnl_Chart2.Visible = true;

        string ReportLevelText = "";
        if (ReportLevel == 0)
        {
            ReportLevelText = "[ " + Year + " ] Monthly ";
        }
        if (ReportLevel == 1)
        {
            ReportLevelText = "[ " + Year + " ] Quarterly - ";
        }
        if (ReportLevel == 2)
        {
            ReportLevelText = "[ " + Year + " ] Half - Yearly";
        }
        if (ReportLevel == 3)
        {
            ReportLevelText = "[ " + Year + " ] Yearly";
        }

        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();
        string SQL = 
            
            "SELECT [InspMonth],COUNT(ID) AS INSP_COUNT, SUM(OBS_COUNT) OBS_COUNT,SUM(OBS_COUNT)/COUNT(ID)  AS OBS_PER_INSP " +
                   "FROM " +
                   "( " +
                   "select MONTH(ACTUALDATE) AS [InspMonth],t.id,COUNT(*) AS OBS_COUNT  " +
                   "from  " +
                   "t_inspectiondue t  " +
                   "inner join m_Inspection m on m.id=t.inspectionid  " +
                   "left join t_observations o on t.id=o.inspectiondueid " +
                   "where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null " +
                   "GROUP BY MONTH(ACTUALDATE),t.id " +
                   ") A " +
                   "GROUP BY [InspMonth]";

        DataTable dt_Ins = Common.Execute_Procedures_Select_ByQuery(SQL);

        List<string> Lst_X_Axis = new List<string>();
        List<int> Lst_Y_Axis = new List<int>();
        int CurrMonth = DateTime.Today.Month;
        int CurrQtr =(int)Math.Ceiling(DateTime.Today.Month / 3.0) ;
        int CurrHalf = (int)Math.Ceiling(DateTime.Today.Month / 6.0);
        if (ReportLevel == 0)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (Year == DateTime.Today.Year.ToString())
                {
                    if(i<=CurrMonth)
                        Lst_X_Axis.Add(ProjectCommon.GetMonthName(i.ToString()));
                }
                else
                    Lst_X_Axis.Add(ProjectCommon.GetMonthName(i.ToString()));
            }
            ReportLevelText = "[ " + Year + " ] Monthly ";

        }
        if (ReportLevel == 1)
        {
            string[] Qtrs = { "Q1", "Q2", "Q3", "Q4" };
            for (int i = 1; i <= 4; i++)
            {
                if (Year == DateTime.Today.Year.ToString())
                {
                    if (i <= CurrQtr)
                        Lst_X_Axis.Add(Qtrs[i - 1]);
                }
                else
                    Lst_X_Axis.Add(Qtrs[i - 1]);
            }
            ReportLevelText = "[ " + Year + " ] Quarterly - ";
        }
        if (ReportLevel == 2)
        {
            string[] Halfs = { "H1", "H2"};

            for (int i = 1; i <= 2; i++)
            {
                if (Year == DateTime.Today.Year.ToString())
                {
                    if (i <= CurrHalf)
                        Lst_X_Axis.Add(Halfs[i - 1]);
                }
                else
                    Lst_X_Axis.Add(Halfs[i - 1]);
            }

            ReportLevelText = "[ " + Year + " ] Half - Yearly";
        }
        if (ReportLevel == 3)
        {
            Lst_X_Axis.Add(Year);
            ReportLevelText = "[ " + Year + " ] Yearly";
        }

        //===================
        DataView dv = dt_Ins.DefaultView;

        if (ReportLevel == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                dv.RowFilter = "InspMonth=" + (i + 1).ToString();
                DataTable dt = dv.ToTable();
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("AVG(OBS_PER_INSP)", "")));
            }
        }
        else if (ReportLevel == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 3).ToString() + " And InspMonth <=" + ((i * 3) + 3).ToString();
                DataTable dt = dv.ToTable();
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("AVG(OBS_PER_INSP)", "")));

            }
        }
        else if (ReportLevel == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
                DataTable dt = dv.ToTable();
                Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("AVG(OBS_PER_INSP)", "")));

            }
        }
        else if (ReportLevel == 3)
        {
            //dv.RowFilter = "InspMonth > " + (i * 6).ToString() + " And InspMonth <=" + ((i * 6) + 6).ToString();
            DataTable dt = dt_Ins;//dv.ToTable();
            Lst_Y_Axis.Add(Common.CastAsInt32(dt.Compute("AVG(OBS_PER_INSP)", "")));
        }

        string[] X_Axis = Lst_X_Axis.ToArray();

        //===================



        Chart2.Series["Series1"].Points.Clear();
        Chart2.Series["Series1"].LegendText= "Avg. No of Obs.";
        int c = 0;
        foreach (string Item in Lst_X_Axis)
        {
            Chart2.Series["Series1"].Points.AddXY(Item, Lst_Y_Axis[c].ToString());
            c++;
        }

        if (Lst_X_Axis.Count >= 2)
        {
            Chart2.Series.Add("TrendLine");
            Chart2.Series["TrendLine"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
            Chart2.Series["TrendLine"].BorderWidth = 1;
            Chart2.Series["TrendLine"].Color = Color.Red;
            // Line of best fit is linear
            string typeRegression = "Linear";//"Exponential";//
            // The number of days for Forecasting
            string forecasting = "1";
            // Show Error as a range chart.
            string error = "false";
            // Show Forecasting Error as a range chart.
            string forecastingError = "false";
            // Formula parameters
            string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
            //Chart2.Series[0].Sort(System.Web.UI.DataVisualization.Charting.PointSortOrder.Ascending, "X");
            // Create Forecasting Series.
            Chart2.DataManipulator.FinancialFormula(System.Web.UI.DataVisualization.Charting.FinancialFormula.Forecasting, parameters, Chart2.Series[0], Chart2.Series["TrendLine"]);
        }

        Chart2.Titles.Add(((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Avg. Observations Trend Line");
        Chart2.Titles.Add(ReportLevelText + " Report ( " + Critera + " )");
        Chart2.Titles[0].Font = fnt_ChartHeading;
      
    }
    public void BindChart5()
    {
        pnl_Chart2.Visible = true;
        Chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
        string ReportLevelText = "";
        //if (ReportLevel == 0)
        //{
        //    ReportLevelText = "[ " + Year + " ] Monthly ";
        //}
        //if (ReportLevel == 1)
        //{
        //    ReportLevelText = "[ " + Year + " ] Quarterly - ";
        //}
        //if (ReportLevel == 2)
        //{
        //    ReportLevelText = "[ " + Year + " ] Half - Yearly";
        //}
        //if (ReportLevel == 3)
        //{
        //    ReportLevelText = "[ " + Year + " ] Yearly";
        //}

        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();

        string SQL =    "select MC.ID,MC.CHAPTERNO,CONVERT(VARCHAR,MC.CHAPTERNO) + '-' + MC.CHAPTERNAME AS CHAPTERNAME,count(t.id) AS NO_OBS from " +
                        "m_Chapters mc  " +
                        "inner join m_SubChapters sc on mc.Id=sc.ChapterId  " +
                        "inner join m_questions q on sc.Id=q.SubChapterId  " +
                        "left join " +
                        "( " +
	                    "    SELECT t1.actualdate,t1.vesselid,t1.issire,o.id,o.Questionid FROM t_inspectiondue t1  " +
	                    "    inner join m_Inspection m on m.id=t1.inspectionid  " +
	                    "    inner join t_observations o on t1.id=o.inspectiondueid " +
                        "    where m.inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and actualdate is not null " +
                        ") t on q.Id=t.QuestionId  " +
                        "WHERE MC.inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") " +
                        "GROUP BY MC.ID,MC.CHAPTERNO,CONVERT(VARCHAR,MC.CHAPTERNO) + '-' + MC.CHAPTERNAME ORDER BY CHAPTERNO ";

        //string SQL = "select MC.ID,MC.CHAPTERNO,CONVERT(VARCHAR,MC.CHAPTERNO) + '-' + MC.CHAPTERNAME AS CHAPTERNAME,COUNT(*) AS NO_OBS from " +
        //            "t_inspectiondue t " +
        //            "inner join m_Inspection m on m.id=t.inspectionid " +
        //            "left join t_observations o on t.id=o.inspectiondueid " +
        //            "inner join m_questions q on q.Id=o.QuestionId " +
        //            "inner join m_SubChapters sc	on sc.Id=q.SubChapterId " +
        //            "inner join m_Chapters mc on mc.Id=sc.ChapterId " +
        //            "WHERE isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and m.inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null " +
        //            "GROUP BY MC.ID,MC.CHAPTERNO,CONVERT(VARCHAR,MC.CHAPTERNO) + '-' + MC.CHAPTERNAME " +
        //            "ORDER BY CHAPTERNO ";

        DataTable dt_Ins = Common.Execute_Procedures_Select_ByQuery(SQL);
        //===================
        //===================


        Chart2.Series["Series1"].Points.Clear();
        Chart2.Series["Series1"].LegendText = "No of Obs.";
        int c = 0;
        foreach (DataRow dr in dt_Ins.Rows)
        {
            Chart2.Series["Series1"].Points.AddXY(dr["CHAPTERNAME"].ToString(), dr["NO_OBS"].ToString());
            c++;
        }

        //Chart2.Series.Add("TrendLine");
        //Chart2.Series["TrendLine"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
        //Chart2.Series["TrendLine"].BorderWidth = 1;
        //Chart2.Series["TrendLine"].Color = Color.Green;
        //// Line of best fit is linear
        //string typeRegression = "Linear";//"Exponential";//
        //// The number of days for Forecasting
        //string forecasting = "1";
        //// Show Error as a range chart.
        //string error = "false";
        //// Show Forecasting Error as a range chart.
        //string forecastingError = "false";
        //// Formula parameters
        //string parameters = typeRegression + ',' + forecasting + ',' + error + ',' + forecastingError;
        ////Chart2.Series[0].Sort(System.Web.UI.DataVisualization.Charting.PointSortOrder.Ascending, "X");
        //// Create Forecasting Series.
        //Chart2.DataManipulator.FinancialFormula(System.Web.UI.DataVisualization.Charting.FinancialFormula.Forecasting, parameters, Chart2.Series[0], Chart2.Series["TrendLine"]);


        Chart2.Titles.Add("No of Observations per " + ((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Chapter");
        Chart2.Titles.Add(ReportLevelText + " Report ( " + Critera + " )");
        Chart2.Titles[0].Font = fnt_ChartHeading;

    }
    public void BindChart6()
    {
        pnl_Chart1.Visible = true;
        string ReportLevelText = "";
        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();
        //if (FromDate.Trim()!="")
        //DateClause=" ActualDate >='" + FromDate + "' ";

        //if (ToDate.Trim() != "")
        //{
        //    DateClause += ((FromDate.Trim() == "") ? "" : " AND ") + " ActualDate <='" + ToDate + "' ";
        //}
        //if (DateClause.Trim() != "")
        //    DateClause = " AND ( " + DateClause + " )";
        decimal Ins_Fleet_Avg=0;
        //===================
        DataTable dt_Ins = Common.Execute_Procedures_Select_ByQuery("select " + Year + " as Year,VesselName,count(distinct t.id) as InsId ,count(obs.id) as ObsId,Round(cast(count(obs.id) as float)/count(distinct t.id),1) as Obs_Per_Ins from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid inner join dbo.vessel v on t.vesselid=v.vesselid left join t_observations obs on t.id=obs.InspectionDueId where t.isSire<>'N' and t.vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and m.inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null group by VesselName order by VesselName");
        if (Mode == "D")
        {
            ProjectCommon.ExportDatatable(Response, dt_Ins, "Data");
            return;
        }
        
        List<string> Lst_X_Axis = new List<string>();
        for (int i = 0; i <= dt_Ins.Rows.Count - 1; i++)
        {
            Lst_X_Axis.Add(dt_Ins.Rows[i]["VesselName"].ToString());
        }
        
        //===================
        DataView dv = dt_Ins.DefaultView;
        decimal[][] Data = new decimal[Lst_X_Axis.Count][];

        for (int i = 0; i <= dt_Ins.Rows.Count - 1; i++)
        {
            decimal[] str_Data = { Common.CastAsDecimal(dt_Ins.Rows[i]["InsId"]), Common.CastAsDecimal(dt_Ins.Rows[i]["ObsId"]),Common.CastAsDecimal( dt_Ins.Rows[i]["Obs_Per_Ins"]) };
            Data[i] = str_Data;
        }

        
        string[] X_Axis = Lst_X_Axis.ToArray();
        Ins_Fleet_Avg = Common.CastAsDecimal(dt_Ins.Compute("AVG(Obs_Per_Ins)",""));

        decimal TotalInspections = 0;
        decimal MaxRecords = 0;
        for (int i = 0; i < Data.Length; i++)
        {
            decimal Sum_Inner = Data[i].Sum(a => a);
            if (Sum_Inner > MaxRecords)
                MaxRecords = Sum_Inner;

            TotalInspections += Sum_Inner;
        }

        if (TotalInspections == 0)
        {
            ProjectCommon.ShowMessage("No data found under this criteria.");
            return;
        }

        //===================

        //const int dotsPerInch = 100;    // define the quality in DPI
        //const double widthInInch = 12;   // width of the bitmap in INCH
        //const double heightInInch = 8;


        //int Width =(int) widthInInch * dotsPerInch;
        //int Height = (int) heightInInch * dotsPerInch;

        //MaxRecords = MaxRecords * 10;

        int TopMargin = 120;
        int LeftMargin = 30;
        int RightMargin = 30;
        int BottomMargin = 350;

        int ColWidth = 20;

        int Ratio =(int)(840.0 /(double)MaxRecords);

        int Height = (int)((Ratio * (double)MaxRecords) + TopMargin + BottomMargin);
        if (Height < 900)
            Height = 900;

        int Width = 2400;



        Bitmap b = new Bitmap(Width, Height);
        //b.SetResolution(300, 300);
        Graphics g = Graphics.FromImage(b);

        g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
        RectangleF r_Content = new RectangleF(0 + LeftMargin, 0 + TopMargin, Width - RightMargin - LeftMargin, Height - BottomMargin - TopMargin);


        g.DrawRectangle(Pens.Green, r_Content.X,r_Content.Y,r_Content.Width,r_Content.Height);
        //g.DrawLine(Pens.Red, new PointF(r_Content.X, r_Content.Y + Height-TopMargin-BottomMargin), new PointF(r_Content.X + Width, r_Content.Y + Height-TopMargin-BottomMargin));

        int NoOfCols = Data.Length;
        int TotalSepWidth = (NoOfCols) * ColWidth;

        float ColSpacing = (r_Content.Width - TotalSepWidth) / (NoOfCols + 1);
        //ColWidth = 140;

        //float ColWidth = (r_Content.Width - TotalSepWidth )/ NoOfCols;
        //ColWidth = 140;
        g.DrawString("Fleet Avg. : " + Math.Round(Ins_Fleet_Avg,2).ToString(),f_month, Brushes.Black, LeftMargin,TopMargin-40);
        
        g.DrawString("Avg. Observations per Vessel ( " + ((InspectionGroupId == 1) ? "SIRE" : "CDI") + " ) ", f_header_large, Brushes.Black, new PointF((Width - RightMargin - LeftMargin) / 2, 10), sf_Center);
        if(Critera.Trim()!="")
            g.DrawString(ReportLevelText + "( " + Critera + " )", f_header, Brushes.Black, new PointF((Width - RightMargin - LeftMargin) / 2, 50), sf_Center);

        g.FillRectangle(Brushes.Blue, new RectangleF(800, 80,50,20));
        g.DrawString(" Inspections ", f_header, Brushes.Black, new PointF(860, 80));

        g.FillRectangle(Brushes.Orange, new RectangleF(1150, 80, 50, 20));
        g.DrawString(" Observations ", f_header, Brushes.Black, new PointF(1210, 80));

        g.FillRectangle(Brushes.Green, new RectangleF(1500, 80, 50, 20));
        g.DrawString(" Obs. / Insp. ", f_header, Brushes.Black, new PointF(1560, 80));    

        decimal No_Of_GridLines = (decimal)MaxRecords;// Math.Ceiling((decimal)(r_Content.Height / MaxRecords));
        decimal V_Gap_Gridlines = ((decimal)r_Content.Height / No_Of_GridLines);

        decimal factor = MaxRecords / 10;
        for (int c = 0; c <= No_Of_GridLines; c +=(int)factor)
        {
            g.DrawLine(Pens.LightGray, r_Content.X, TopMargin + (c * (float)V_Gap_Gridlines), r_Content.Width, TopMargin + (c * (float)V_Gap_Gridlines));
            //g.DrawString(((int)(No_Of_GridLines - c)).ToString(), f_count, Brushes.Black, new PointF(r_Content.X, TopMargin + (c * (float)V_Gap_Gridlines)));
        }

        for (int c = 0; c < NoOfCols; c++)
        {
            RectangleF rCol = new RectangleF(LeftMargin + ((c + 1) * ColSpacing) + (c * ColWidth), TopMargin, ColWidth, Height - BottomMargin - TopMargin);
            //g.FillRectangle(Brushes.Blue,rCol);

            decimal ArraySum = 0;
            //ArraySum = (Data[c].Sum(a => a));
            
            decimal EndPoint = Height - BottomMargin;
            EndPoint = EndPoint - (ArraySum * V_Gap_Gridlines);

            float ystart = (float)EndPoint;

            //for (int c1 = 0; c1 < Data[c].Length; c1++)
            //{
            //    float height = (float)(V_Gap_Gridlines * Data[c][c1]);
            //    string[] InspColors = { "#FF0000", "#00FF00", "#0000FF" };

                decimal BarHeight = Common.CastAsDecimal(Data[c][0]) * Ratio;
                RectangleF r_Box=new RectangleF(LeftMargin + ((c + 1) * ColSpacing) + (c * ColWidth), (float)(Height - BottomMargin - BarHeight), (float)ColWidth, (float)BarHeight);
                g.FillRectangle(Brushes.Blue, r_Box);
                g.DrawString(Data[c][0].ToString(), f_count, Brushes.Blue, r_Box.X + 25,r_Box.Y-20);

            
                BarHeight = Common.CastAsDecimal(Data[c][1]) * Ratio;
                if (BarHeight > 0)
                {
                    r_Box = new RectangleF(r_Box.X, (float)(r_Box.Y - (float)BarHeight), (float)ColWidth, (float)BarHeight);
                    g.FillRectangle(Brushes.Orange, r_Box);
                    g.DrawString(Data[c][1].ToString(), f_count, Brushes.Red, r_Box.X + 25, r_Box.Y - 10);
                }

                BarHeight = Common.CastAsDecimal(Data[c][2]) * Ratio;
                if (BarHeight > 0)
                {
                    r_Box = new RectangleF(r_Box.X, (float)(r_Box.Y - (float)BarHeight), (float)ColWidth, (float)BarHeight);
                    g.FillRectangle(Brushes.Green, r_Box);
                    g.DrawString(Data[c][2].ToString(), f_count, Brushes.Green, r_Box.X + 25, r_Box.Y - 0);
                }

            //    int BoxMiddle = (int)(ystart + (height / 2)) - 13;
            //    g.DrawString(Data[c][c1].ToString(), f_count, Brushes.White, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), BoxMiddle), sf_Center);
            //    ystart = ystart + height;
            //}

                g.TranslateTransform(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2) + (ColWidth/2) + 5 , Height - BottomMargin);
            g.RotateTransform(90);
            g.DrawString(X_Axis[c], f_header_ARIAL, Brushes.Black, 0, 0);
            //g.DrawString(X_Axis[c], f_header_ARIAL, Brushes.Blue, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), Height - BottomMargin),sf_Center);
            g.ResetTransform();
            //g.DrawString("(" + ArraySum.ToString() + ") ", f_month, Brushes.Blue, new PointF(r_Content.X + ((c + 1) * ColSpacing) + (c * ColWidth) + (ColWidth / 2), Height - BottomMargin + 30), sf_Center);

        }

        imgChart.ImageUrl = "chart.jpg";
        b.Save(Server.MapPath("~\\Vetting\\chart.jpg"), ImageFormat.Jpeg);
        string s = r.NextDouble().ToString();
        imgChart.ImageUrl = "chart.jpg?" + s;
    }
    public void BindChart7()
    {
        pnl_Chart3.Visible = true;

        string InspectionIdClause = "";
        if (InspectionId > 0)
            InspectionIdClause = " And InspectionId=" + InspectionId.ToString();

        DataTable dt_Ins_yes = Common.Execute_Procedures_Select_ByQuery("select count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null and t.id in (select inspectiondueid from t_InspSupt where Attending=1)");
        DataTable dt_Ins_all = Common.Execute_Procedures_Select_ByQuery("select count(*) as Insp_Count from t_inspectiondue t inner join m_Inspection m on m.id=t.inspectionid where isSire<>'N' and vesselid in (" + VesselIds + ") " + InspectionIdClause + " and year(actualdate)=" + Year + " and inspectiongroup in (select id from [dbo].[m_InspectionGroup] where id=" + InspectionGroupId.ToString() + ") and actualdate is not null ");
        
        int yes = Common.CastAsInt32(dt_Ins_yes.Rows[0][0]);
        int all = Common.CastAsInt32(dt_Ins_all.Rows[0][0]);
        
        if (all == 0)
        {
            ProjectCommon.ShowMessage("No data found under this criteria.");
            return;
        }

        double yesper = Math.Round((yes * 100.0 / all), 2);
        double noper = 100- yesper;
        
        float yesAngle = 0;
        
        try { yesAngle = (float)(360 * yesper / 100); }
        catch { }
        
        int TopMargin = 70;
        int LeftMargin = 120;
        int RightMargin = 120;
        int BottomMargin = 50;

        int Height = 600;
        int Width = 1200;

        int InnerWidth = Width - LeftMargin - RightMargin;
        int InnerHeight = Height - TopMargin - BottomMargin;

        Bitmap b = new Bitmap(Width, Height);
        Graphics g = Graphics.FromImage(b);
        g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
        Point p_center = new Point(LeftMargin + (InnerWidth) / 2, TopMargin + (InnerHeight) / 2);

        int Diameter = 350;
        int Radius = Diameter / 2;

        Rectangle r_circle = new Rectangle(p_center.X - Radius, p_center.Y - Radius, Diameter, Diameter);
        g.FillEllipse(Brushes.Blue, r_circle);

        if (yes > 0)
            g.FillPie(Brushes.Green, r_circle, 0, yesAngle);
        
        yesper = Math.Round(yesper);
        noper = Math.Round(noper);

        g.DrawString("( " + ((InspectionGroupId == 1) ? "SIRE" : "CDI") + "  ) Suptd. Attend. for Insps.", f_header_large, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 10), sf_Center);
        
        if(Critera.Trim()!="")
            g.DrawString(" Report ( " + Critera + " )", f_header, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 50), sf_Center);

        g.DrawString(" ( " + all.ToString() + " ) Inspections", f_header, Brushes.Black, new PointF(LeftMargin + (InnerWidth / 2), 80), sf_Center);

        g.DrawString(yesper  + "% - " + yes + " [ Attended : Yes ] ", f_header_ARIAL, Brushes.Green, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight - 50), sf_Center);
        g.DrawString(noper + "% - " + (all - yes) + " [ Attended : No ] ", f_header_ARIAL, Brushes.Orange, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight - 20), sf_Center);
        //g.DrawString(FailedPer + "% - " + Failed + " Inspections [ NOT APPROVED ]", f_header_ARIAL, Brushes.Red, new PointF(LeftMargin + (InnerWidth / 2), TopMargin + InnerHeight + 10), sf_Center);

        imgChart.ImageUrl = "chart.jpg";
        b.Save(Server.MapPath("~\\Vetting\\chart.jpg"), ImageFormat.Jpeg);
        string s = r.NextDouble().ToString();
        imgChart3.ImageUrl = "chart.jpg?" + s;



        //Chart2.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
        //Chart2.Series["Series1"].Points.Clear();

        //Chart2.Series["Series1"].Points.AddXY("Pass", dt_Ins_Cleared.Rows[0][0]);
        //Chart2.Series["Series1"].Points.AddXY("Awaiting Results", dt_Ins_Pending.Rows[0][0]);
        ////int c = 0;
        ////foreach (string Item in Lst_X_Axis)
        ////{
        ////    Chart2.Series["Series1"].Points.AddXY(Item, Lst_Y_Axis[c].ToString());
        ////    c++;
        ////}


        //Chart2.Titles.Add(((InspectionGroupId == 1) ? "SIRE" : "CDI") + " Inspections Results");
        //Chart2.Titles.Add(ReportLevelText + " Report ( " + Critera + " )");
        //Chart2.Titles[0].Font = fnt_ChartHeading;
    }
    protected DataTable getDistinct_Inspection_Sum(DataTable dt)
    {
        List<string> Codes = new List<string>();
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Code");
        dtResult.Columns.Add("Color");
        dtResult.Columns.Add("INSP_COUNT",typeof ( Int32));
        int RowsCount=0;

        if (dt.Columns.Contains("Code"))
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (!Codes.Contains(dr["Code"]))
                {
                    Codes.Add(dr["Code"].ToString());
                    dtResult.Rows.Add(dtResult.NewRow());
                    dtResult.Rows[RowsCount]["Code"] = dr["Code"];
                    dtResult.Rows[RowsCount]["Color"] = dr["Color"];
                    dtResult.Rows[RowsCount]["INSP_COUNT"] = dt.Compute("SUM(INSP_COUNT)", "CODE='" + dr["Code"] + "'");
                    RowsCount++;
                }
            }
        }
        else
        {

        }
        return dtResult;
    }
}



