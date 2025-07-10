using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class VPR_EEOI : System.Web.UI.Page
{
    Color EEOI_Color = Color.FromArgb(127, 179, 225);
    Color EEOI_ROLL_Color = Color.Green;
    Color CO2_Color = Color.FromArgb(222, 134, 136);

    Brush EEOI_Brush;
    Brush EEOI_ROLL_Brush;
    Brush CO2_Brush;

    StringFormat drawFormat_C = new StringFormat();
    StringFormat drawFormat_R = new StringFormat();
    StringFormat drawFormat_L = new StringFormat();

    DataTable dt;
    static Random R = new Random();
    public string VoyageNo
    {
        get  { return Convert.ToString(ViewState["VoyageNo"]);}
        set { ViewState["VoyageNo"] = value; }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_A');SetLastFocus('dvscroll_S');SetLastFocus('dvscroll_D');", true);
    }
    protected void BindVesselDDL()
    {
        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselCode";
        this.ddl_Vessel.DataSource = Inspection_Master.getMasterDataforInspection("Vessel", "VesselCode", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataBind();
        this.ddl_Vessel.Items.Insert(0, new ListItem("< SELECT >", "0"));
        this.ddl_Vessel.Items[0].Value = "0";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        // INITLIZATION
        EEOI_Brush = new SolidBrush(EEOI_Color);
        EEOI_ROLL_Brush = Brushes.Green;
        CO2_Brush = new SolidBrush(CO2_Color);

        drawFormat_C.Alignment = StringAlignment.Center;
        drawFormat_R.Alignment = StringAlignment.Far;
        drawFormat_L.Alignment = StringAlignment.Near;
        //-------------------------
        //VesselPositionReporting mp = (VesselPositionReporting)this.Master;
        //mp.ShowMenu = false;
        //mp.ShowHeaderbar = false;
        if (Session["UserName"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Sesion Expired. Please login again.');", true);
            return;
        }

        if (!IsPostBack)
        {
            txtFromDate.Text = "01-JAN-" + DateTime.Today.Year.ToString();
            txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            BindVesselDDL();
            ddl_Vessel.SelectedValue = Request.QueryString["CurrentShip"].Trim();
        }
        
    }
    protected void DrawLegend()
    {
        Font F_Date = new Font("ARIAL", 8);
        Bitmap b = new Bitmap(200, 400);
        Graphics g = Graphics.FromImage(b);
        g.FillRectangle(Brushes.White, 1, 1,198, 398 );

        decimal MaxValue_EEOI = Common.CastAsDecimal(dt.Compute("MAX(EEOI)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_EEOI = Common.CastAsInt32(MaxValue_EEOI);
        if (MaxValue_EEOI <= 0)
        {
            return;
        }

        decimal MaxValue_EEOI_ROLL = Common.CastAsDecimal(dt.Compute("MAX(EEOI_ROLL)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_EEOI_ROLL = Common.CastAsInt32(MaxValue_EEOI_ROLL);
        if (MaxValue_EEOI_ROLL <= 0)
        {
            return;
        }

        decimal MaxValue_CO2 = Common.CastAsDecimal(dt.Compute("MAX(CO2)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_CO2 = Common.CastAsInt32(MaxValue_CO2);
        if (MaxValue_CO2 <= 0)
        {
            return;
        }

        // Drawing Legend Bars

        if (chkCo2.Checked)
        {
            Point P1 = new Point(50, 50);
            Point P2 = new Point(50, 350);
            P2.X = P2.X + P1.X;
            P2.Y = P2.Y + P1.Y;
            Brush B_CO2 = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), CO2_Color);
            g.FillRectangle(B_CO2, P1.X, P1.Y, 10, 300);
        }
        if (chkEEOI.Checked)
        {
            Point P1 = new Point(100, 50);
            Point P2 = new Point(100, 350);
            P2.X = P2.X + P1.X;
            P2.Y = P2.Y + P1.Y;
            Brush B_EEOI = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), EEOI_Color);
            g.FillRectangle(B_EEOI, P1.X, P1.Y, 10, 300);
        }
        if (chkEEOI_R.Checked)
        {
            Point P1 = new Point(150, 50);
            Point P2 = new Point(150, 350);
            P2.X = P2.X + P1.X;
            P2.Y = P2.Y + P1.Y;
            Brush B_EEOI_R = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), EEOI_ROLL_Color);
            g.FillRectangle(B_EEOI_R, P1.X, P1.Y, 10, 300);
        }

        // DRAWING MIN VALUE
        if (chkCo2.Checked)
            g.DrawString("0", F_Date, CO2_Brush, 50, 344, drawFormat_R);
        if (chkEEOI.Checked)
            g.DrawString("0", F_Date, EEOI_Brush, 100, 344, drawFormat_R);
        if (chkEEOI_R.Checked)
            g.DrawString("0", F_Date, EEOI_ROLL_Brush, 150, 344, drawFormat_R);

        // DRAWING MIDDLE VALUE
        if (chkCo2.Checked)
            g.DrawString(Common.CastAsInt32(MaxValue_CO2 / 2).ToString(), F_Date, CO2_Brush, 50, 194 , drawFormat_R);
        if (chkEEOI.Checked)
            g.DrawString(Common.CastAsInt32(MaxValue_EEOI / 2).ToString(), F_Date, EEOI_Brush, 100, 194, drawFormat_R);
        if (chkEEOI_R.Checked)
            g.DrawString(Common.CastAsInt32(MaxValue_EEOI_ROLL / 2).ToString(), F_Date, EEOI_ROLL_Brush, 150, 194, drawFormat_R);

        // DRAWING MAX VALUE
        if (chkCo2.Checked)
            g.DrawString(MaxValue_CO2 + " ", F_Date, CO2_Brush, 50, 44, drawFormat_R);
        if (chkEEOI.Checked)
            g.DrawString(MaxValue_EEOI + " ", F_Date, EEOI_Brush, 100,44, drawFormat_R);
        if (chkEEOI_R.Checked)
            g.DrawString(MaxValue_EEOI_ROLL + " ", F_Date, EEOI_ROLL_Brush, 150, 44, drawFormat_R);


        if (chkCo2.Checked)
        {
            g.TranslateTransform(35, 344);
            g.RotateTransform(-90);
            g.DrawString("Co2 Emisssion (t) ", F_Date, CO2_Brush, 0, 0, drawFormat_L);
            g.ResetTransform();
        }
        if (chkEEOI.Checked)
        {
            g.TranslateTransform(85,344);
            g.RotateTransform(-90);
            g.DrawString("EEOI (g/t-mile)", F_Date, EEOI_Brush, 0, 0, drawFormat_L);
            g.ResetTransform();
        }
        if (chkEEOI_R.Checked)
        {
            g.TranslateTransform(135,344);
            g.RotateTransform(-90);
            g.DrawString("EEOI-Rolling (g/t-mile)", F_Date, EEOI_ROLL_Brush, 0, 0, drawFormat_L);
            g.ResetTransform();
        }

        b.Save(Server.MapPath("~\\VPR\\EEOI_LEGEND.png"), System.Drawing.Imaging.ImageFormat.Png);
        imgChart.ImageUrl = "EEOI_LEGEND.png?" + R.NextDouble().ToString();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lnkVoyClick(sender,e);
        // Draw Legend 
         DrawLegend();

        // NOW dt ALREADY HAVE DATA FROM LAST FUNCTION....
        decimal ImageHeight = 400;
        decimal Margin = 20;
        decimal BarWidth = 70;
        decimal BaseLineY = ImageHeight - 50;
        decimal HeaderHeight = 100;
        decimal TextHeight = 14;
        int PointRadius = 5;
        decimal LegendBarWidth = 10;
        decimal LegendBarsGap = 50;
        decimal FixLegendGapFromChart = 40;

        //---------------------------------
        
        decimal MaxValue_EEOI = Common.CastAsDecimal(dt.Compute("MAX(EEOI)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_EEOI = Common.CastAsInt32(MaxValue_EEOI);
        if (MaxValue_EEOI <= 0)
        {
            return;
        }

        decimal MaxValue_EEOI_ROLL = Common.CastAsDecimal(dt.Compute("MAX(EEOI_ROLL)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_EEOI_ROLL = Common.CastAsInt32(MaxValue_EEOI_ROLL);
        if (MaxValue_EEOI_ROLL <= 0)
        {
            return; 
        }

        decimal MaxValue_CO2 = Common.CastAsDecimal(dt.Compute("MAX(CO2)", "")) * Common.CastAsDecimal(1.5);
        MaxValue_CO2 = Common.CastAsInt32(MaxValue_CO2);
        if (MaxValue_CO2 <= 0)
        {
            return;
        }

        decimal ImageWidth = 0;
        decimal BarHeight = ImageHeight - HeaderHeight;
        decimal HeaderLineY = BaseLineY - BarHeight;
        

        //StringFormat drawFormat_L_Upward = new StringFormat();
        //drawFormat_L_Upward.Alignment = StringAlignment.Near;
        //drawFormat_L_Upward.

        Font F_ReportName=new Font("ARIAL",20);
        Font F_ReportHeading=new Font("ARIAL",12);
        Font F_Date = new Font("ARIAL", 8);

        ImageWidth = dt.Rows.Count * BarWidth + (Margin * 2);
        Bitmap b = new Bitmap(_ToInt(ImageWidth), _ToInt(ImageHeight));
        Graphics g = Graphics.FromImage(b);
        
        // FILLING BACKGROUND WITH WHITE COLOR
        g.FillRectangle(Brushes.White, 1, 1, _ToInt(ImageWidth - 2), _ToInt(ImageHeight - 2));

        // WRITING HEADER

        //g.DrawString("EEOI - Port to Port Voyage Basis", F_ReportName, Brushes.Red,_ToInt( ImageWidth / 2), 20, drawFormat_C);
        //g.DrawString(ddl_Vessel.SelectedItem.Text, F_ReportHeading, Brushes.Red, _ToInt(ImageWidth / 2), 60, drawFormat_C);

        Point Start_EEOI = new Point(0, 0);
        Point Start_EEOI_ROLL = new Point(0, 0);
        Point Start_CO2 = new Point(0, 0);
        
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            Decimal CO2_VALUE = Math.Round(Common.CastAsDecimal(dt.Rows[i - 1]["CO2"]), 2);
            Decimal EEOI_VALUE = Math.Round(Common.CastAsDecimal(dt.Rows[i - 1]["EEOI"]), 2);
            Decimal EEOI_ROLL_VALUE=Math.Round(Common.CastAsDecimal(dt.Rows[i - 1]["EEOI_ROLL"]),2);

            //DRAWING BARS
            //g.DrawLine(Pens.Black, _ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), _ToInt(BaseLineY), _ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), _ToInt(BaseLineY - BarHeight));
            g.FillRectangle(new SolidBrush(Color.FromArgb(219,240,193)), _ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2))-10, _ToInt(HeaderLineY), 20,_ToInt(BarHeight));

            //DRAWING DATES
            g.DrawString(dt.Rows[i - 1]["VOYAGENO"].ToString(), F_Date, Brushes.Black, _ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), _ToInt(BaseLineY + 8), drawFormat_C);

            //DRAWING CHART LINE
            if (i == 1)
            {
                int PixelY;

                // Start_EEOI
                if (chkEEOI.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + (MaxValue_EEOI - EEOI_VALUE) * (BarHeight / MaxValue_EEOI));
                    Start_EEOI = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(EEOI_Brush, _ToInt(Start_EEOI.X - PointRadius), _ToInt(Start_EEOI.Y - PointRadius), PointRadius * 2, PointRadius * 2);
                    //g.DrawString(EEOI_VALUE.ToString(), F_Date, Brushes.Black, Start_EEOI, drawFormat_R);
                }

                // Start_EEOI_ROLL
                if (chkEEOI_R.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + (MaxValue_EEOI_ROLL - EEOI_ROLL_VALUE) * (BarHeight / MaxValue_EEOI_ROLL));
                    Start_EEOI_ROLL = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(EEOI_ROLL_Brush, Start_EEOI_ROLL.X - PointRadius, Start_EEOI_ROLL.Y - PointRadius, PointRadius * 2, PointRadius * 2);
                    //g.DrawString(EEOI_ROLL_VALUE.ToString(), F_Date, Brushes.Black, Start_EEOI_ROLL, drawFormat_R);
                }

                // Start_CO2
                if (chkCo2.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + (MaxValue_CO2 - CO2_VALUE) * (BarHeight / MaxValue_CO2));
                    Start_CO2 = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(CO2_Brush, Start_CO2.X - PointRadius, Start_CO2.Y - PointRadius, PointRadius * 2, PointRadius * 2);
                }
            }
            else
            if (i != 1)
            {
                int PixelY;

                // Start_EEOI
                if (chkEEOI.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + ((MaxValue_EEOI - EEOI_VALUE) * (BarHeight / MaxValue_EEOI)));
                    Point CurrentPoint = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.DrawLine(new Pen(EEOI_Color,2), Start_EEOI, CurrentPoint);
                    Start_EEOI = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(EEOI_Brush, Start_EEOI.X - PointRadius, Start_EEOI.Y - PointRadius, PointRadius * 2, PointRadius * 2);
                    //g.DrawString(EEOI_VALUE.ToString(), F_Date, Brushes.Black, Start_EEOI, drawFormat_R);
                }
                // Start_EEOI_ROLL
                if (chkEEOI_R.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + ((MaxValue_EEOI_ROLL - EEOI_ROLL_VALUE) * (BarHeight / MaxValue_EEOI_ROLL)));
                    Point CurrentPoint = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.DrawLine(new Pen(EEOI_ROLL_Color, 2), Start_EEOI_ROLL, CurrentPoint);
                    Start_EEOI_ROLL = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(EEOI_ROLL_Brush, Start_EEOI_ROLL.X - PointRadius, Start_EEOI_ROLL.Y - PointRadius, PointRadius * 2, PointRadius * 2);
                    //g.DrawString(EEOI_ROLL_VALUE.ToString(), F_Date, Brushes.Black, Start_EEOI_ROLL, drawFormat_R);
                }

                // Start_CO2
                if (chkCo2.Checked)
                {
                    PixelY = _ToInt(HeaderLineY + ((MaxValue_CO2 - CO2_VALUE) * (BarHeight / MaxValue_CO2)));
                    Point CurrentPoint = new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.DrawLine(new Pen(CO2_Color, 2), Start_CO2, CurrentPoint);
                    Start_CO2= new Point(_ToInt(Margin + (i * (BarWidth)) - (BarWidth / 2)), PixelY);
                    g.FillEllipse(CO2_Brush, Start_CO2.X - PointRadius, Start_CO2.Y - PointRadius, PointRadius * 2, PointRadius * 2);
                }
            }
            
        }

        // CREATING BASELINE 
        g.DrawLine(Pens.Black, _ToInt(Margin), _ToInt(BaseLineY), _ToInt(ImageWidth - Margin), _ToInt(BaseLineY));

        // CREATING HEADER LINE 
        g.DrawLine(Pens.Black, _ToInt(Margin), _ToInt(HeaderLineY), _ToInt(ImageWidth - Margin), _ToInt(HeaderLineY));

        // DRAWING MIN VALUE
        //if (chkCo2.Checked)
        //{
        //    g.DrawString("0 ", F_Date, CO2_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap), _ToInt(BaseLineY - (TextHeight / 2)), drawFormat_R);

        //    g.TranslateTransform(_ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap - 15), _ToInt(BaseLineY - (TextHeight / 2)));
        //    g.RotateTransform(-90);
        //    g.DrawString("Co2 Emisssion (t) ", F_Date, CO2_Brush, 0, 0, drawFormat_L);
        //    g.ResetTransform();
        //}
        //if (chkEEOI.Checked)
        //{
        //    g.DrawString("0 ", F_Date, EEOI_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap), _ToInt(BaseLineY - (TextHeight / 2)), drawFormat_R);

        //    g.TranslateTransform(_ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - 15), _ToInt(BaseLineY - (TextHeight / 2)));
        //    g.RotateTransform(-90);
        //    g.DrawString("EEOI (g/t-mile)", F_Date, EEOI_Brush, 0, 0, drawFormat_L);
        //    g.ResetTransform();
        //}
        //if (chkEEOI_R.Checked)
        //{
        //    g.DrawString("0 ", F_Date, EEOI_ROLL_Brush, _ToInt(Margin - FixLegendGapFromChart), _ToInt(BaseLineY - (TextHeight / 2)), drawFormat_R);

        //    g.TranslateTransform(_ToInt(Margin - FixLegendGapFromChart - 15), _ToInt(BaseLineY - (TextHeight / 2)));
        //    g.RotateTransform(-90);
        //    g.DrawString("EEOI-Rolling (g/t-mile)", F_Date, EEOI_ROLL_Brush, 0, 0, drawFormat_L);
        //    g.ResetTransform();
        //}

        //// DRAWING MIDDLE VALUE
        //if (chkCo2.Checked)
        //    g.DrawString(Common.CastAsInt32(MaxValue_CO2 / 2).ToString(), F_Date, CO2_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap), _ToInt(BaseLineY - (BarHeight / 2) - (TextHeight / 2)), drawFormat_R);
        //if (chkEEOI.Checked)
        //    g.DrawString(Common.CastAsInt32(MaxValue_EEOI / 2).ToString(), F_Date, EEOI_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap), _ToInt(BaseLineY - (BarHeight / 2) - (TextHeight / 2)), drawFormat_R);
        //if (chkEEOI_R.Checked)
        //    g.DrawString(Common.CastAsInt32(MaxValue_EEOI_ROLL / 2).ToString(), F_Date, EEOI_ROLL_Brush, _ToInt(Margin - FixLegendGapFromChart), _ToInt(BaseLineY - (BarHeight / 2) - (TextHeight / 2)), drawFormat_R);

        //// DRAWING MAX VALUE
        //if (chkCo2.Checked)
        //    g.DrawString(MaxValue_CO2 + " ", F_Date, CO2_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap), _ToInt(HeaderLineY - (TextHeight / 2)), drawFormat_R);
        //if (chkEEOI.Checked)
        //    g.DrawString(MaxValue_EEOI + " ", F_Date, EEOI_Brush, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap), _ToInt(HeaderLineY - (TextHeight / 2)), drawFormat_R);
        //if (chkEEOI_R.Checked)
        //    g.DrawString(MaxValue_EEOI_ROLL + " ", F_Date, EEOI_ROLL_Brush, _ToInt(Margin - FixLegendGapFromChart), _ToInt(HeaderLineY - (TextHeight / 2)), drawFormat_R);

        // DRAWING LEGEND
        
        //if (chkEEOI.Checked)
        //{
        //    g.FillRectangle(EEOI_Brush, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY), 100, _ToInt(LegendBarWidth));
        //    g.DrawString("EEOI (g/t-mile)", F_Date, Brushes.Red, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY - TextHeight), drawFormat_L);
        //}
        //if (chkEEOI_R.Checked)
        //{
        //    g.FillRectangle(EEOI_ROLL_Brush, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY + LegendBarsGap), 100, _ToInt(LegendBarWidth));
        //    g.DrawString("EEOI-Rolling (g/t-mile)", F_Date, Brushes.Blue, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY - TextHeight + LegendBarsGap), drawFormat_L);
        //}
        //if (chkCo2.Checked)
        //{
        //    g.FillRectangle(CO2_Brush, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY + LegendBarsGap + LegendBarsGap), 100, _ToInt(LegendBarWidth));
        //    g.DrawString("Co2 Emisssion (t)", F_Date, Brushes.Red, _ToInt(ImageWidth - Margin + FixLegendGapFromChart), _ToInt(HeaderLineY - TextHeight + LegendBarsGap + LegendBarsGap), drawFormat_L);
        //}


        //if (chkCo2.Checked)
        //{
        //    Point P1=new Point(_ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap), _ToInt(HeaderLineY));
        //    Point P2=new Point(_ToInt(LegendBarWidth), _ToInt(BarHeight));
        //    P2.X=P2.X+P1.X;
        //    P2.Y=P2.Y+P1.Y;
        //    Brush B_CO2 = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), CO2_Color);
        //    g.FillRectangle(B_CO2, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap - LegendBarsGap), _ToInt(HeaderLineY), _ToInt(LegendBarWidth), _ToInt(BarHeight));
        //}
        //if (chkEEOI.Checked)
        //{
        //    Point P1 = new Point(_ToInt(Margin - FixLegendGapFromChart - LegendBarsGap), _ToInt(HeaderLineY));
        //    Point P2 = new Point(_ToInt(LegendBarWidth), _ToInt(BarHeight));
        //    P2.X = P2.X + P1.X;
        //    P2.Y = P2.Y + P1.Y;
        //    Brush B_EEOI = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), EEOI_Color);
        //    g.FillRectangle(B_EEOI, _ToInt(Margin - FixLegendGapFromChart - LegendBarsGap), _ToInt(HeaderLineY), _ToInt(LegendBarWidth), _ToInt(BarHeight));
        //}
        //if (chkEEOI_R.Checked)
        //{
        //    Point P1 = new Point(_ToInt(Margin - FixLegendGapFromChart), _ToInt(HeaderLineY));
        //    Point P2 = new Point(_ToInt(LegendBarWidth), _ToInt(BarHeight));
        //    P2.X = P2.X + P1.X;
        //    P2.Y = P2.Y + P1.Y;
        //    Brush B_EEOI_R = new LinearGradientBrush(P1, P2, Color.FromArgb(248, 248, 255), EEOI_ROLL_Color);
        //    g.FillRectangle(B_EEOI_R, _ToInt(Margin - FixLegendGapFromChart), _ToInt(HeaderLineY), _ToInt(LegendBarWidth), _ToInt(BarHeight));
        //}

        b.Save(Server.MapPath("~\\VPR\\EEOI.png"), System.Drawing.Imaging.ImageFormat.Png);
        if(radChart.Checked)
        {
            dvscroll_D.Visible=false;
            dvscroll_I.Visible=true;
        }
        else
        {
            dvscroll_D.Visible=true;
            dvscroll_I.Visible=false;
        }

        imgChart1.ImageUrl = "EEOI.png?" + R.NextDouble().ToString();
    }
    
    protected void btnVDA_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VoyageDataAnalysis.aspx?" + Request.QueryString);
    }
    protected void btnEEOI_OnClick(object sender, EventArgs e)
    {
        
    }
    protected void lnkVoyClick(object sender, EventArgs e)
    {
        string DateWhereClause="";
        string PositionWhereClause = "";

        if (txtFromDate.Text.Trim() != "")
            DateWhereClause = " DEPDATE >='" + txtFromDate.Text.Trim() + "'";
        if (txtToDate.Text.Trim() != "")
            DateWhereClause = DateWhereClause + ((DateWhereClause.Trim() == "") ? "" : " AND ") + " DEPDATE <='" + txtToDate.Text.Trim() + "' ";

        if (ddlVoyCon.SelectedIndex>0)
            DateWhereClause = DateWhereClause + ((DateWhereClause.Trim() == "") ? "" : " AND ") + " VOYCONDITION =" + ddlVoyCon.SelectedValue + " ";

        if (ddlPos.SelectedIndex > 0)
            PositionWhereClause = " AND SS.REPORTTYPECODE IN " +ddlPos.SelectedValue;
        

        //string str="SELECT *, " +
        //           "(SELECT SUM(DISTANCEMADEGOOD) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'TOTALDISTANCE', " +
        //           "(SELECT SUM(MEIFO45+AEIFO45+CargoHeatingIFO45) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'HFO', " +
        //           "(SELECT SUM(MEIFO1+AEIFO1+CargoHeatingIFO1) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'LFO', " +
        //           "(SELECT SUM(MEMGO1+AEMGO1+CargoHeatingMGO1) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MGO1_0', " +
        //           "(SELECT SUM(MEMGO5+AEMGO5+CargoHeatingMGO5) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MGO0_5', " +
        //           "(SELECT SUM(MEMDO+AEMDO+CargoHeatingMDO) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MDO', " +
        //           "(SELECT SUM(MEIFO45+AEIFO45+CargoHeatingIFO45 + MEIFO1+AEIFO1+CargoHeatingIFO1 + MEMGO1+AEMGO1+CargoHeatingMGO1 + MEMGO5+AEMGO5+CargoHeatingMGO5 + MEMDO+AEMDO+CargoHeatingMDO) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'TotalFuel', " +
        //           "(SELECT MAX(TotalCargoWeight+BallastWeight) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'CARGO', " +
        //           "WEATHER=(CASE WHEN (SELECT AVG(SeaState) FROM VW_VPR_ALLREPORTS SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTTYPECODE='N' AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE)>=6 THEN 'Yes' ELSE 'No' END) " +
        //           "FROM " +
        //           "( " +
        //           "SELECT 'V-' + CONVERT(VARCHAR,YEAR(REPORTDATE)) + '-' + CONVERT(VARCHAR,REPLACE(STR(ROW_NUMBER() OVER (ORDER BY REPORTDATE,DEPARTUREID),3),' ','0')) AS VOYAGENO, " +
	       //        "D.DEPARTUREID,D.VOYCONDITION,D.VESSELID,D.DepPort AS DEPPORT,D.REPORTDATE AS DEPDATE, " +
	       //        "(SELECT TOP 1 DEPARTUREID FROM dbo.VPRDepartureReport D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.DEPARTUREID<>D.DEPARTUREID) AS NEXTDEPID,  " +
	       //        "(SELECT TOP 1 REPORTDATE FROM dbo.VPRDepartureReport D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.DEPARTUREID<>D.DEPARTUREID) AS NEXTDEPDATE,  " +
	       //        "(SELECT TOP 1 DepArrivalPort FROM dbo.VPRDepartureReport D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.DEPARTUREID<>D.DEPARTUREID) AS NEXTDEPPORT " +
        //           "FROM VPRDepartureReport D WHERE D.VESSELID='" + ddl_Vessel.SelectedValue + "' AND YEAR(REPORTDATE)=" + Convert.ToDateTime(txtFromDate.Text).Year.ToString() + " " +
        //           ") M WHERE 1=1 AND " + DateWhereClause + "ORDER BY DEPDATE,DEPARTUREID";

        string str = "SELECT *, " +
                  "(SELECT SUM(DISTANCEMADEGOOD) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'TOTALDISTANCE', " +
                  "(SELECT SUM(MEIFO45+AEIFO45+CargoHeatingIFO45) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'HFO', " +
                  "(SELECT SUM(MEIFO1+AEIFO1+CargoHeatingIFO1) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'LFO', " +
                  "(SELECT SUM(MEMGO1+AEMGO1+CargoHeatingMGO1) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MGO1_0', " +
                  "(SELECT SUM(MEMGO5+AEMGO5+CargoHeatingMGO5) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MGO0_5', " +
                  "(SELECT SUM(MEMDO+AEMDO+CargoHeatingMDO) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'MDO', " +
                  "(SELECT SUM(MEIFO45+AEIFO45+CargoHeatingIFO45 + MEIFO1+AEIFO1+CargoHeatingIFO1 + MEMGO1+AEMGO1+CargoHeatingMGO1 + MEMGO5+AEMGO5+CargoHeatingMGO5 + MEMDO+AEMDO+CargoHeatingMDO) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'TotalFuel', " +
                  "(SELECT MAX(TotalCargoWeight+BallastWeight) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE " + PositionWhereClause + ") AS 'CARGO', " +
                  "WEATHER=(CASE WHEN (SELECT AVG(SeaState) FROM VW_VSL_VPRNoonReport_New SS WHERE SS.VESSELID=M.VESSELID AND SS.ACTIVITY_CODE='N' AND SS.REPORTDATE >=DEPDATE AND SS.REPORTDATE<=NEXTDEPDATE)>=6 THEN 'Yes' ELSE 'No' END) " +
                  "FROM " +
                  "( " +
                  "SELECT 'V-' + CONVERT(VARCHAR,YEAR(REPORTDATE)) + '-' + CONVERT(VARCHAR,REPLACE(STR(ROW_NUMBER() OVER (ORDER BY REPORTDATE,ReportsPK),3),' ','0')) AS VOYAGENO, " +
                  "D.ReportsPK,D.VOYCONDITION,D.VESSELID,D.DepPort AS DEPPORT,D.REPORTDATE AS DEPDATE, " +
                  "(SELECT TOP 1 ReportsPK FROM VSL_VPRNoonReport_New D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.ReportsPK<>D.ReportsPK AND activity_code='D') AS NEXTDEPID,  " +
                  "(SELECT TOP 1 REPORTDATE FROM VSL_VPRNoonReport_New D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.ReportsPK<>D.ReportsPK AND activity_code='D') AS NEXTDEPDATE,  " +
                  "(SELECT TOP 1 DEPPORT FROM VSL_VPRNoonReport_New D1 WHERE D1.VESSELID=D.VESSELID AND D1.REPORTDATE>=D.REPORTDATE AND D1.ReportsPK<>D.ReportsPK AND activity_code='D') AS NEXTDEPPORT " +
                  "FROM VSL_VPRNoonReport_New D WHERE D.VESSELID='" + ddl_Vessel.SelectedValue + "' AND D.ACTIVITY_CODE='D' AND YEAR(REPORTDATE)=" + Convert.ToDateTime(txtFromDate.Text).Year.ToString() + " " +
                  ") M WHERE 1=1 AND " + DateWhereClause + "ORDER BY DEPDATE,ReportsPK";


        dt = Common.Execute_Procedures_Select_ByQuery(str);
        dt.Columns.Add(new DataColumn("SNO", typeof(Int32)));
        dt.Columns.Add(new DataColumn("CO2",typeof(Decimal)));
        dt.Columns.Add(new DataColumn("CO2_ROLL",typeof(Decimal)));
        dt.Columns.Add(new DataColumn("NM_X_MT",typeof(Decimal)));
        dt.Columns.Add(new DataColumn("NM_X_MT_ROLL", typeof(Decimal)));
        dt.Columns.Add(new DataColumn("EEOI", typeof(Decimal)));
        dt.Columns.Add(new DataColumn("EEOI_ROLL", typeof(Decimal)));
        for(int i=1;i<=dt.Rows.Count;i++)
        {
            dt.Rows[i-1]["SNO"]=i.ToString();
        }

        Decimal CO2SUM = 0;
        Decimal NMMTSUM = 0;
        int P2P = Common.CastAsInt32(txtPPY.Text);
        foreach(DataRow dr in dt.Rows)
        {
            dr["CO2"] = Common.CastAsDecimal(dr["TotalFuel"]) * Common.CastAsDecimal(3.1);
            dr["NM_X_MT"] = Common.CastAsDecimal(dr["TOTALDISTANCE"]) * Common.CastAsDecimal(dr["CARGO"]);

            if (P2P <= 0)
            {
                dr["CO2_ROLL"] = dr["CO2"];
                dr["NM_X_MT_ROLL"] = dr["NM_X_MT"];
            }
            else
            {
                int NowSNO = Common.CastAsInt32(dr["SNO"]);
                string LastSumClause = "SNO<=" + NowSNO.ToString() + " AND SNO>=" + (NowSNO - P2P).ToString();
                CO2SUM = Common.CastAsDecimal(dt.Compute("SUM(CO2)", LastSumClause));
                dr["CO2_ROLL"] = CO2SUM;
                NMMTSUM = Common.CastAsDecimal(dt.Compute("SUM(NM_X_MT)", LastSumClause)); 
                dr["NM_X_MT_ROLL"] = NMMTSUM;
            }

            decimal NM_X_MT = 0;
            NM_X_MT = Common.CastAsDecimal(dr["NM_X_MT"]);
            if (NM_X_MT != 0)
            {
                dr["EEOI"] = (Common.CastAsDecimal(dr["CO2"]) * 1000000) / NM_X_MT;
                if (P2P <= 0)
                {
                    dr["EEOI_ROLL"] = dr["EEOI"];
                }
                else
                {
                    dr["EEOI_ROLL"] = (Common.CastAsDecimal(dr["CO2_ROLL"]) * 1000000) / NM_X_MT;
                }
            }
        }
        rpt_Data.DataSource = dt;
        rpt_Data.DataBind();

    }
    public int _ToInt(decimal value)
    {
        return Common.CastAsInt32(value);
    }
}
