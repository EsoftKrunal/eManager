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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class SeaIcon : System.Web.UI.Page
{
    int width = 400, height = 400;
    string WindText = "",CurrText="";
    Point OldPoint;
    int outercircleradius = 100, innercircleradius = 50;
    public static Random rnd=new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        string sql = "";
        sql = "select * from VW_VSL_VPRNoonReport_New WHERE VesselID='" + Request.QueryString["VSLCode"] + "' and ReportsPK=" + Request.QueryString["ReportId"];
            
        DataSet DataSource = Budget.getTable(sql);
        DataTable dt ;//= VesselReporting.getTable(sql).Tables[0];
        if (DataSource != null)
        {
            if (DataSource.Tables[0].Rows.Count > 0)
            {
                dt = DataSource.Tables[0];
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
        
        //DataTable dtRep = VesselReporting.getTable("SELECT ReportTime From REPORTSMASTER WHERE VSLCode='" + Request.QueryString["VSLCode"] + "' And REPORTID=" + "0" + Request.QueryString["ReportId"]).Tables[0];


        string Mode = "" + Request.QueryString["Mode"];
        int RCourse = 0;
        int RWind = 0;
        int RSea = 0;
        int RCurr = 0;
        if (dt.Rows.Count > 0)
        {
            lblVesselName.Text = getVesselName(dt.Rows[0]["VesselID"].ToString());

            //DataSet ds1 = VesselReporting.getTable("SELECT *,dbo.getPortName(DI_FromPort) as FPName,dbo.getPortName(DI_ToPort) as TPName,dbo.getPortName(DI_FromPort) as DFPName,dbo.getPortName(DI_ToPort) as DTPName,dbo.getPortName(AI_FromPort) as AFPName,dbo.getPortName(AI_ToPort) as ATPName FROM VOYAGEINFO WHERE VSLCode='" + Request.QueryString["VSLCode"] + "' And REPORTID=" + Request.QueryString["ReportId"]);
            DataRow dr1 = dt.Rows[0];
            lblHeader.Text = "Noon Report - (" + dr1["DepPort"].ToString() + "  To  " + dr1["DepArrivalPort"].ToString() + ") " + DateTime.Parse(dr1["ReportDate"].ToString()).ToString("dd-MMM-yyyy");


            RCourse = Common.CastAsInt32( dt.Rows[0]["CourceT"].ToString());
            RWind = Common.CastAsInt32(dt.Rows[0]["WindDirectionT"].ToString()); 
            RSea = Common.CastAsInt32(dt.Rows[0]["SeaDirection"].ToString());
            RCurr = Common.CastAsInt32(dt.Rows[0]["CurrentDirection"].ToString());
            WindText = dt.Rows[0]["WindForce"].ToString() + " BF";
            CurrText = dt.Rows[0]["CurrentStrength"].ToString() + " KTS";
            divRem.InnerHtml = dt.Rows[0]["weatherRemarks"].ToString();
        }        
        if (Mode.Trim() == "") Mode = "Image";
        Response.Clear();
        if (Mode == "Image")
        {
            Response.ContentType = "image/jpeg";
        }
        Bitmap b ;
        if (Mode == "Page")
            b = new Bitmap(width, height);
        else
            b = new Bitmap(width, height);

        Graphics g = Graphics.FromImage(b);
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;

        if (Mode == "Page")
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));
        else
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));


        g.DrawImage(new Bitmap(Server.MapPath("~/Modules/HRD/Images/legend.jpg")),new Point(240,350));      

        g.DrawEllipse(Pens.LightGray, ((width / 2) - outercircleradius), ((height / 2) - outercircleradius), (outercircleradius * 2), (outercircleradius * 2));
        g.DrawEllipse(Pens.LightGray, ((width / 2) - innercircleradius), ((height / 2) - innercircleradius), (innercircleradius * 2), (innercircleradius * 2));

        g.DrawLine(Pens.LightGray, (width / 2), 0, (height / 2), height);
        g.DrawLine(Pens.LightGray, 0, (width/2), height, (height/2) );

        
        g.DrawString("N", new Font("Arial", 8), Brushes.Black, width / 2 , 10 , sf);
        g.DrawString("W", new Font("Arial", 8), Brushes.Black, 10,height / 2, sf);
        g.DrawString("E", new Font("Arial", 8), Brushes.Black, width-10 , height/2 , sf);
        g.DrawString("S", new Font("Arial", 8), Brushes.Black, width / 2, height -10, sf);

        DrawCurve(g, RCourse, Brushes.Green);
        RWind = RWind + 180;
        if (RWind >= 360) { RWind = RWind - 360; } 
        DrawArrow(g, RWind, Brushes.Red);
        DrawArrow2(g, RCurr, Brushes.Blue);
        
        if (Mode == "Image")
        {
            imgData.Visible = false; 
            b.Save(Response.OutputStream, ImageFormat.Jpeg);
            Response.OutputStream.Close();
            Response.Flush();
            Response.End();
        }
        else
        {
            this.Title = "Ship Location Indicator";
            imgData.Visible = true;
            b.Save(Server.MapPath("~\\Modules\\LPSQE\\PositionReport\\ImageIcon.jpeg"));
            //imgData.ImageUrl = "UserUploadedDocuments\\ImageIcon.jpeg" ;
            imgData.ImageUrl = "~\\Modules\\LPSQE\\PositionReport\\ImageIcon.jpeg?" + rnd.Next();
                
            lblCourse.Text = dt.Rows[0]["CourceT"].ToString() + "º (T)";
            lblWDir.Text = dt.Rows[0]["WindDirectionT"].ToString() + "º (T)";
            lblWForce.Text = dt.Rows[0]["WindForce"].ToString() + " BF";
            lblSeaDir.Text = dt.Rows[0]["SeaDirection"].ToString() + "º (T)";
            lblSeaState.Text = dt.Rows[0]["SeaState"].ToString();
            lblCurrDir.Text = dt.Rows[0]["CurrentDirection"].ToString() + "º (T)";
            lblCurrStr.Text = dt.Rows[0]["CurrentStrength"].ToString() + " KTS";
            divRem.InnerHtml = dt.Rows[0]["weatherRemarks"].ToString();
        }
    }
    private void DrawCurve(Graphics g, int Course, Brush col)
    {
        g.FillRectangle(Brushes.Transparent, 0, 0, 200, 200);
        Point pb1,p1,pb2, p2, pTip;
        double radians = 22.0 / (7 * (180.0 / Course));
        double x, y;
        x = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
        y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
        setSignForScreen(Course, ref x, ref y);
        pTip = new Point(35, 0);
        pb1 = new Point(0, 12);
        p1 = new Point(-30, 8);
        pb2 = new Point(0, -12);
        p2 = new Point(-30, -8);

        RotatePoint(Course, ref pTip);
        RotatePoint(Course, ref pb1);
        RotatePoint(Course, ref p1);
        RotatePoint(Course, ref pb2);
        RotatePoint(Course, ref p2);

        //double Mover = 0;
        //pb1 = new Point(pb1.X - Convert.ToInt32(pTip.X * Mover), pb1.Y - Convert.ToInt32(pTip.Y * Mover));
        //p1 = new Point(p1.X - Convert.ToInt32(pTip.X * Mover), p1.Y - Convert.ToInt32(pTip.Y * Mover));

        //pb2 = new Point(pb2.X - Convert.ToInt32(pTip.X * Mover), pb2.Y - Convert.ToInt32(pTip.Y * Mover));
        //p2 = new Point(p2.X - Convert.ToInt32(pTip.X * Mover), p2.Y - Convert.ToInt32(pTip.Y * Mover));
        //pTip = new Point(pTip.X - Convert.ToInt32(pTip.X * Mover), pTip.Y - Convert.ToInt32(pTip.Y * Mover));

        pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
        pb1 = new Point((width / 2) + pb1.X, (height / 2) + pb1.Y);
        p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);

        pb2 = new Point((width / 2) + pb2.X, (height / 2) + pb2.Y);
        p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);

        Point[] points = { pTip, pb1, p1, p2, pb2,pTip};
        g.DrawCurve(Pens.Black, points);
        
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Near;
        //if (Course >= 0 && Course < 90)
        //    g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 20, (height / 2) + 20), sf);
        //else if (Course >= 90 && Course < 180)
        //    g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 25, (height / 2) - 25), sf);
        //else if (Course >= 180 && Course < 270)
        //    g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) - 25, (height / 2) - 25), sf);
        //else
        //    g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 25, (height / 2) - 25), sf);

        int seperator = 5;

        if (Course >= 0 && Course < 90)
            g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((pTip.X) + seperator, (pTip.Y) - seperator), sf);
        else if (Course >= 90 && Course < 180)
            g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((pTip.X) + seperator, (pTip.Y) - seperator), sf);
        else if (Course >= 180 && Course < 270)
            g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((pTip.X) - seperator - 15, (pTip.Y) + seperator), sf);
        else
            g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((pTip.X) + seperator - 30 , (pTip.Y) + seperator - 15), sf);
        
    }
    private void DrawArrow(Graphics g, int Course, Brush col)
    {
        g.FillRectangle(Brushes.Transparent, 0, 0, width, height);
        Point pTip, p1, p2, p3, p4, p5,p5p1,p5p2,p5p3,p51,p51p1,p51p2,p51p3, p6, degp, degp1;
        double radians = 22.0 / (7 * (180.0 / Course));
        double x, y;
        x = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
        y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));

        // rotate 90* more

        setSignForScreen(Course, ref x, ref y);

        pTip = new Point(10, -5);
        p1 = new Point(15, 0);
        p2 = new Point(10, 5);
        p3 = new Point(10, 1);
        p4 = new Point(-15, 1);
        p5 = new Point(-14, -1);

        p5p1 = new Point(-20, -10);
        p5p2 = new Point(-19, -10);
        p5p3 = new Point(-13, -1);

        p51 = new Point(-10, -1);
        p51p1 = new Point(-16, -10);
        p51p2 = new Point(-15, -10);
        p51p3 = new Point(-9, -1);

        p6 = new Point(10, -1);

        degp = p5;
        degp1 = p5;
        
        RotatePoint(Course, ref pTip);
        RotatePoint(Course, ref p1);
        RotatePoint(Course, ref p2);
        RotatePoint(Course, ref p3);
        RotatePoint(Course, ref p4);
        RotatePoint(Course, ref p5);
        RotatePoint(Course, ref p5p1);
        RotatePoint(Course, ref p5p2);
        RotatePoint(Course, ref p5p3);
        RotatePoint(Course, ref p51);
        RotatePoint(Course, ref p51p1);
        RotatePoint(Course, ref p51p2);
        RotatePoint(Course, ref p51p3);
        RotatePoint(Course, ref p6);

        RotatePoint(Course, ref degp);
       
        double Mover = 5.0;
        double Mover1 = 7.0;
        double Mover2 = 10.4;

        pTip = new Point(pTip.X - Convert.ToInt32(p1.X * Mover), pTip.Y - Convert.ToInt32(p1.Y * Mover));
        p2 = new Point(p2.X - Convert.ToInt32(p1.X * Mover), p2.Y - Convert.ToInt32(p1.Y * Mover));
        p3 = new Point(p3.X - Convert.ToInt32(p1.X * Mover), p3.Y - Convert.ToInt32(p1.Y * Mover));
        p4 = new Point(p4.X - Convert.ToInt32(p1.X * Mover), p4.Y - Convert.ToInt32(p1.Y * Mover));
        p5 = new Point(p5.X - Convert.ToInt32(p1.X * Mover), p5.Y - Convert.ToInt32(p1.Y * Mover));
        p5p1 = new Point(p5p1.X - Convert.ToInt32(p1.X * Mover), p5p1.Y - Convert.ToInt32(p1.Y * Mover));
        p5p2 = new Point(p5p2.X - Convert.ToInt32(p1.X * Mover), p5p2.Y - Convert.ToInt32(p1.Y * Mover));
        p5p3 = new Point(p5p3.X - Convert.ToInt32(p1.X * Mover), p5p3.Y - Convert.ToInt32(p1.Y * Mover));

        p51 = new Point(p51.X - Convert.ToInt32(p1.X * Mover), p51.Y - Convert.ToInt32(p1.Y * Mover));
        p51p1 = new Point(p51p1.X - Convert.ToInt32(p1.X * Mover), p51p1.Y - Convert.ToInt32(p1.Y * Mover));
        p51p2 = new Point(p51p2.X - Convert.ToInt32(p1.X * Mover), p51p2.Y - Convert.ToInt32(p1.Y * Mover));
        p51p3 = new Point(p51p3.X - Convert.ToInt32(p1.X * Mover), p51p3.Y - Convert.ToInt32(p1.Y * Mover));

        p6 = new Point(p6.X - Convert.ToInt32(p1.X * Mover), p6.Y - Convert.ToInt32(p1.Y * Mover));
        degp = new Point(degp.X - Convert.ToInt32(p1.X * Mover1), degp.Y - Convert.ToInt32(p1.Y * Mover1));
        degp1 = new Point(degp1.X - Convert.ToInt32(p1.X * Mover2), degp1.Y - Convert.ToInt32(p1.Y * Mover2));

        p1 = new Point(p1.X - Convert.ToInt32(p1.X * Mover), p1.Y - Convert.ToInt32(p1.Y * Mover));


        pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
        p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);
        p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);
        p3 = new Point((width / 2) + p3.X, (height / 2) + p3.Y);
        p4 = new Point((width / 2) + p4.X, (height / 2) + p4.Y);
        p5 = new Point((width / 2) + p5.X, (height / 2) + p5.Y);
        p5p1 = new Point((width / 2) + p5p1.X, (height / 2) + p5p1.Y);
        p5p2 = new Point((width / 2) + p5p2.X, (height / 2) + p5p2.Y);
        p5p3 = new Point((width / 2) + p5p3.X, (height / 2) + p5p3.Y);

        p51 = new Point((width / 2) + p51.X, (height / 2) + p51.Y);
        p51p1 = new Point((width / 2) + p51p1.X, (height / 2) + p51p1.Y);
        p51p2 = new Point((width / 2) + p51p2.X, (height / 2) + p51p2.Y);
        p51p3 = new Point((width / 2) + p51p3.X, (height / 2) + p51p3.Y);

        p6 = new Point((width / 2) + p6.X, (height / 2) + p6.Y);

        degp = new Point((width / 2) - 12 + degp.X, (height / 2) -5 + degp.Y);
        degp1 = new Point((width / 2) - 5 + degp1.X, (height / 2) - 5 + degp1.Y);

        Point[] points = { pTip, p1, p2, p3, p4, p5, p5p1, p5p2, p5p3, p51, p51p1, p51p2, p51p3, p6 };
        g.FillPolygon(col, points);
        Course = Course - 180;
        if (Course < 0) Course = 360 + Course;  
        WriteDeg(Course.ToString() + "º" + " (" + WindText + ")", degp, g, col);
        //WriteValue("(" + WindText + ")", degp1, g,col);
    }
    private void DrawArrow2(Graphics g, int Course, Brush col)
    {
        g.FillRectangle(Brushes.Transparent, 0, 0, width, height );
        Point pTip, p1, p2, p3, p4, p5, p6, degp, degp1;
        // p31, p32, p33, 
        // p51, p52, p53, 
        double radians = 22.0 / (7 * (180.0 / Course));
        double x, y;
        x = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
        y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
        setSignForScreen(Course, ref x, ref y);

        pTip = new Point(10, -5);
        p1 = new Point(15, 0);
        p2 = new Point(10, 5);
        p3 = new Point(10, 1);

        //p31 = new Point(6, 1);
        //p32 = new Point(1, 5);
        //p33 = new Point(1, 1);

        p4 = new Point(-15, 1);
        p5 = new Point(-15, -1);
        degp = p5;
        degp1 = p5;
        //p51 = new Point(1, -1);
        //p52 = new Point(1, -5);
        //p53= new Point(6, -1);

        p6 = new Point(10, -1);

        RotatePoint(Course, ref pTip);
        RotatePoint(Course, ref p1);
        RotatePoint(Course, ref p2);
        RotatePoint(Course, ref p3);
        //RotatePoint(Course, ref p31);
        //RotatePoint(Course, ref p32);
        //RotatePoint(Course, ref p33);

        RotatePoint(Course, ref p4);
        RotatePoint(Course, ref p5);
        //RotatePoint(Course, ref p51);
        //RotatePoint(Course, ref p52);
        //RotatePoint(Course, ref p53);

        RotatePoint(Course, ref p6);
        RotatePoint(Course, ref degp);
        
        double Mover = 5.0;
        double Mover1 = 7.0;
        double Mover2 = 10.4;

        pTip = new Point(pTip.X - Convert.ToInt32(p1.X * Mover), pTip.Y - Convert.ToInt32(p1.Y * Mover));
        p2 = new Point(p2.X - Convert.ToInt32(p1.X * Mover), p2.Y - Convert.ToInt32(p1.Y * Mover));
        p3 = new Point(p3.X - Convert.ToInt32(p1.X * Mover), p3.Y - Convert.ToInt32(p1.Y * Mover));

        //p31 = new Point(p31.X - Convert.ToInt32(p1.X * Mover), p31.Y - Convert.ToInt32(p1.Y * Mover));
        //p32 = new Point(p32.X - Convert.ToInt32(p1.X * Mover), p32.Y - Convert.ToInt32(p1.Y * Mover));
        //p33 = new Point(p33.X - Convert.ToInt32(p1.X * Mover), p33.Y - Convert.ToInt32(p1.Y * Mover));

        p4 = new Point(p4.X - Convert.ToInt32(p1.X * Mover), p4.Y - Convert.ToInt32(p1.Y * Mover));
        p5 = new Point(p5.X - Convert.ToInt32(p1.X * Mover), p5.Y - Convert.ToInt32(p1.Y * Mover));
        //p51 = new Point(p51.X - Convert.ToInt32(p1.X * Mover), p51.Y - Convert.ToInt32(p1.Y * Mover));
        //p52 = new Point(p52.X - Convert.ToInt32(p1.X * Mover), p52.Y - Convert.ToInt32(p1.Y * Mover));
        //p53 = new Point(p53.X - Convert.ToInt32(p1.X * Mover), p53.Y - Convert.ToInt32(p1.Y * Mover));

        p6 = new Point(p6.X - Convert.ToInt32(p1.X * Mover), p6.Y - Convert.ToInt32(p1.Y * Mover));
        degp = new Point(degp.X - Convert.ToInt32(p1.X * Mover1), degp.Y - Convert.ToInt32(p1.Y * Mover1));
        degp1 = new Point(degp1.X - Convert.ToInt32(p1.X * Mover2), degp1.Y - Convert.ToInt32(p1.Y * Mover2));

        p1 = new Point(p1.X - Convert.ToInt32(p1.X * Mover), p1.Y - Convert.ToInt32(p1.Y * Mover));

        pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
        p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);
        p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);
        p3 = new Point((width / 2) + p3.X, (height / 2) + p3.Y);

        //p31 = new Point((width / 2) + p31.X, (height / 2) + p31.Y);
        //p32 = new Point((width / 2) + p32.X, (height / 2) + p32.Y);
        //p33 = new Point((width / 2) + p33.X, (height / 2) + p33.Y);

        p4 = new Point((width / 2) + p4.X, (height / 2) + p4.Y);
        p5 = new Point((width / 2) + p5.X, (height / 2) + p5.Y);

        //p51 = new Point((width / 2) + p51.X, (height / 2) + p51.Y);
        //p52 = new Point((width / 2) + p52.X, (height / 2) + p52.Y);
        //p53 = new Point((width / 2) + p53.X, (height / 2) + p53.Y);

        p6 = new Point((width / 2) + p6.X, (height / 2) + p6.Y);
        degp = new Point((width / 2) -15 + degp.X, (height / 2) -5 + degp.Y);
        degp1 = new Point((width / 2) - 11 + degp1.X, (height / 2) -5 + degp1.Y);

        Point[] points = { pTip, p1, p2, p3,p4, p5,p6 };
        g.FillPolygon(col, points);
        WriteDeg2(Course.ToString() + "º" + " (" + CurrText + ")", degp, g, col);
        //WriteValue("(" + CurrText + ")", degp1, g,col);
    }
    //private void DrawShip(Graphics g, int Course)
    //{
    //    Point pTip,p1,p2,p3,p4,p5,p6;
    //    double radians = 22.0 / (7 * (180.0 / Course));
    //    double x, y;
    //    x = Math.Sqrt(Math.Pow(innercircleradius,2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
    //    y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
    //    setSignForScreen(Course, ref x, ref y);

    //    if ((Course >= 0 && Course <= 90) || (Course > 270 && Course <= 360))
    //    {
    //        pTip = new Point(-30, 10);
    //        p1 = new Point(-24, 2);
    //        p2 = new Point(40, 2);
    //        p3 = new Point(31, -7);
    //        p4 = new Point(-30, -7);
    //        p5 = new Point(-39, 2);
    //        p6 = new Point(-30, 2);
    //    }
    //    else
    //    {
    //        pTip = new Point(-30, -10);
    //        p1 = new Point(-24, -2);
    //        p2 = new Point(40, -2);
    //        p3 = new Point(31, 7);
    //        p4 = new Point(-30, 7);
    //        p5 = new Point(-39, -2);
    //        p6 = new Point(-30, -2);
    //    }

    //    RotatePointShip(Course, ref pTip);
    //    RotatePointShip(Course, ref p1);
    //    RotatePointShip(Course, ref p2);
    //    RotatePointShip(Course, ref p3);
    //    RotatePointShip(Course, ref p4);
    //    RotatePointShip(Course, ref p5);
    //    RotatePointShip(Course, ref p6);

    //    pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
    //    p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);
    //    p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);
    //    p3 = new Point((width / 2) + p3.X, (height / 2) + p3.Y);
    //    p4 = new Point((width / 2) + p4.X, (height / 2) + p4.Y);
    //    p5 = new Point((width / 2) + p5.X, (height / 2) + p5.Y);
    //    p6 = new Point((width / 2) + p6.X, (height / 2) + p6.Y);

    //    Point[] points = { pTip, p1, p2,p3,p4,p5,p6};
    //    g.FillPolygon(Brushes.Black, points);

    //    StringFormat sf=new StringFormat();
    //    sf.Alignment=StringAlignment.Center ;  
    //    if (Course >=0 && Course <=90)
    //        g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 20, (height / 2) + 10), sf);
    //    else if (Course >90 && Course <=180)
    //        g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 20, (height / 2) - 20), sf);
    //    else if (Course >180 && Course <=270)
    //        g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) - 20, (height / 2) - 20), sf);
    //    else
    //        g.DrawString(Course + "º", new Font("Arial", 8), Brushes.Black, new Point((width / 2) + 20, (height / 2) - 20), sf);
    //}
    //private void DrawTriangle(Graphics g, int Course,Brush col)
    //{
    //    g.FillRectangle(Brushes.Transparent, 0, 0, 200, 200);
    //    Point p1, p2, pTip;
    //    double radians = 22.0 / (7 * (180.0 / Course));
    //    double x, y;
    //    x = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
    //    y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
    //    setSignForScreen(Course, ref x, ref y);
    //    pTip = new Point(30, 0);
    //    p1 = new Point(-30, 15);
    //    p2 = new Point(-30, -15);

    //    RotatePoint(Course, ref pTip);
    //    RotatePoint(Course, ref p1);
    //    RotatePoint(Course, ref p2);

    //    double Mover = 0;

    //    p1 = new Point(p1.X - Convert.ToInt32(pTip.X * Mover), p1.Y - Convert.ToInt32(pTip.Y * Mover));
    //    p2 = new Point(p2.X - Convert.ToInt32(pTip.X * Mover), p2.Y - Convert.ToInt32(pTip.Y * Mover));
    //    pTip = new Point(pTip.X - Convert.ToInt32(pTip.X * Mover), pTip.Y - Convert.ToInt32(pTip.Y * Mover));

    //    pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
    //    p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);
    //    p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);

    //    Point[] points = { pTip, p1, p2 };
    //    g.FillPolygon(col, points);
    //}
    //private void DrawArrow3(Graphics g, int Course, Brush col)
    //{
    //    g.FillRectangle(Brushes.Transparent, 0, 0, width, height);
    //    Point pTip, p1, p2, p3, p31, p32, p33, p34, p35, p36, p4, p5, p51, p52, p53, p54, p55, p56, p6, degp;
    //    double radians = 22.0 / (7 * (180.0 / Course));
    //    double x, y;
    //    x = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
    //    y = Math.Sqrt(Math.Pow(innercircleradius, 2) / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
    //    setSignForScreen(Course, ref x, ref y);

    //    pTip = new Point(10, -5);
    //    p1 = new Point(15, 0);
    //    p2 = new Point(10, 5);
    //    p3 = new Point(10, 1);

    //    p31 = new Point(6, 1);
    //    p32 = new Point(1, 5);
    //    p33 = new Point(1, 1);

    //    p34 = new Point(-3, 1);
    //    p35 = new Point(-8, 5);
    //    p36 = new Point(-8, 1);

    //    p4 = new Point(-15, 1);
    //    p5 = new Point(-15, -1);
    //    degp = p5;

    //    p51 = new Point(1, -1);
    //    p52 = new Point(1, -5);
    //    p53 = new Point(6, -1);

    //    p54 = new Point(-8, -1);
    //    p55 = new Point(-8, -5);
    //    p56 = new Point(-3, -1);

    //    p6 = new Point(10, -1);

    //    RotatePoint(Course, ref pTip);
    //    RotatePoint(Course, ref p1);
    //    RotatePoint(Course, ref p2);
    //    RotatePoint(Course, ref p3);
    //    RotatePoint(Course, ref p31);
    //    RotatePoint(Course, ref p32);
    //    RotatePoint(Course, ref p33);
    //    RotatePoint(Course, ref p34);
    //    RotatePoint(Course, ref p35);
    //    RotatePoint(Course, ref p36);

    //    RotatePoint(Course, ref p4);
    //    RotatePoint(Course, ref p5);
    //    RotatePoint(Course, ref p51);
    //    RotatePoint(Course, ref p52);
    //    RotatePoint(Course, ref p53);
    //    RotatePoint(Course, ref p54);
    //    RotatePoint(Course, ref p55);
    //    RotatePoint(Course, ref p56);

    //    RotatePoint(Course, ref p6);

    //    double Mover = 4.6;
    //    double Mover1 = 7.7;

    //    pTip = new Point(pTip.X - Convert.ToInt32(p1.X * Mover), pTip.Y - Convert.ToInt32(p1.Y * Mover));
    //    p2 = new Point(p2.X - Convert.ToInt32(p1.X * Mover), p2.Y - Convert.ToInt32(p1.Y * Mover));
    //    p3 = new Point(p3.X - Convert.ToInt32(p1.X * Mover), p3.Y - Convert.ToInt32(p1.Y * Mover));

    //    p31 = new Point(p31.X - Convert.ToInt32(p1.X * Mover), p31.Y - Convert.ToInt32(p1.Y * Mover));
    //    p32 = new Point(p32.X - Convert.ToInt32(p1.X * Mover), p32.Y - Convert.ToInt32(p1.Y * Mover));
    //    p33 = new Point(p33.X - Convert.ToInt32(p1.X * Mover), p33.Y - Convert.ToInt32(p1.Y * Mover));
    //    p34= new Point(p34.X - Convert.ToInt32(p1.X * Mover), p34.Y - Convert.ToInt32(p1.Y * Mover));
    //    p35 = new Point(p35.X - Convert.ToInt32(p1.X * Mover), p35.Y - Convert.ToInt32(p1.Y * Mover));
    //    p36 = new Point(p36.X - Convert.ToInt32(p1.X * Mover), p36.Y - Convert.ToInt32(p1.Y * Mover));

    //    p4 = new Point(p4.X - Convert.ToInt32(p1.X * Mover), p4.Y - Convert.ToInt32(p1.Y * Mover));
    //    p5 = new Point(p5.X - Convert.ToInt32(p1.X * Mover), p5.Y - Convert.ToInt32(p1.Y * Mover));
    //    p51 = new Point(p51.X - Convert.ToInt32(p1.X * Mover), p51.Y - Convert.ToInt32(p1.Y * Mover));
    //    p52 = new Point(p52.X - Convert.ToInt32(p1.X * Mover), p52.Y - Convert.ToInt32(p1.Y * Mover));
    //    p53 = new Point(p53.X - Convert.ToInt32(p1.X * Mover), p53.Y - Convert.ToInt32(p1.Y * Mover));
    //    p54 = new Point(p54.X - Convert.ToInt32(p1.X * Mover), p54.Y - Convert.ToInt32(p1.Y * Mover));
    //    p55 = new Point(p55.X - Convert.ToInt32(p1.X * Mover), p55.Y - Convert.ToInt32(p1.Y * Mover));
    //    p56 = new Point(p56.X - Convert.ToInt32(p1.X * Mover), p56.Y - Convert.ToInt32(p1.Y * Mover));

    //    p6 = new Point(p6.X - Convert.ToInt32(p1.X * Mover), p6.Y - Convert.ToInt32(p1.Y * Mover));
    //    degp = new Point(degp.X - Convert.ToInt32(p1.X * Mover1), degp.Y - Convert.ToInt32(p1.Y * Mover1));

    //    p1 = new Point(p1.X - Convert.ToInt32(p1.X * Mover), p1.Y - Convert.ToInt32(p1.Y * Mover));

    //    pTip = new Point((width / 2) + pTip.X, (height / 2) + pTip.Y);
    //    p1 = new Point((width / 2) + p1.X, (height / 2) + p1.Y);
    //    p2 = new Point((width / 2) + p2.X, (height / 2) + p2.Y);
    //    p3 = new Point((width / 2) + p3.X, (height / 2) + p3.Y);

    //    p31 = new Point((width / 2) + p31.X, (height / 2) + p31.Y);
    //    p32 = new Point((width / 2) + p32.X, (height / 2) + p32.Y);
    //    p33 = new Point((width / 2) + p33.X, (height / 2) + p33.Y);
    //    p34 = new Point((width / 2) + p34.X, (height / 2) + p34.Y);
    //    p35 = new Point((width / 2) + p35.X, (height / 2) + p35.Y);
    //    p36 = new Point((width / 2) + p36.X, (height / 2) + p36.Y);

    //    p4 = new Point((width / 2) + p4.X, (height / 2) + p4.Y);
    //    p5 = new Point((width / 2) + p5.X, (height / 2) + p5.Y);

    //    p51 = new Point((width / 2) + p51.X, (height / 2) + p51.Y);
    //    p52 = new Point((width / 2) + p52.X, (height / 2) + p52.Y);
    //    p53 = new Point((width / 2) + p53.X, (height / 2) + p53.Y);
    //    p54 = new Point((width / 2) + p54.X, (height / 2) + p54.Y);
    //    p55 = new Point((width / 2) + p55.X, (height / 2) + p55.Y);
    //    p56 = new Point((width / 2) + p56.X, (height / 2) + p56.Y);

    //    p6 = new Point((width / 2) + p6.X, (height / 2) + p6.Y);
    //    degp = new Point((width / 2)-30 + degp.X, (height / 2) + degp.Y);

    //    Point[] points = { pTip, p1, p2, p3, p31, p32, p33,p34, p35, p36, p4, p5, p51, p52, p53,p54, p55, p56, p6 };
    //    g.FillPolygon(col, points);
    //    WriteDeg(Course, degp,g);
    //}
    public void WriteDeg(string Course, Point p, Graphics g, Brush col)
    {
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Near;
        if (p.X < width / 2)
        {
            int v=Math.Abs(p.Y-(width/2));
            v = v / 5;
            p.X = p.X -(35-v);  
        }
        g.DrawString(Course, new Font("Arial", 8), col, p, sf);
        OldPoint = p;
    }
    public void WriteDeg2(string Course, Point p, Graphics g, Brush col)
    {
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Near;
        if (p.X < width / 2)
        {
            int v = Math.Abs(p.Y - (width / 2));
            v = v / 5;
            p.X = p.X - (35 - v);
        }
        if (Math.Abs(OldPoint.X - p.X) < 40)
        {

        }
        if (Math.Abs(OldPoint.Y - p.Y) < 10)
        {
            p.Y = p.Y + 20; 
        }
        g.DrawString(Course, new Font("Arial", 8), col, p, sf);
    }
    public void WriteValue(string Val, Point p, Graphics g,Brush col)
    {
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Near;
        g.DrawString(Val, new Font("Arial", 8), col, p, sf);
    }
    //private void DrawCurrent(Graphics g, int Course)
    //{
    //    Point p1, p2, pTip;
    //    double radians = 22.0 / (7 * (180.0 / Course));
    //    double x, y;
    //    x = Math.Sqrt(8100 / (1 + ((Math.Tan(radians)) * (Math.Tan(radians)))));
    //    y = Math.Sqrt(8100 / (1 + (1.0 / ((Math.Tan(radians)) * (Math.Tan(radians))))));
    //    setSignForScreen(Course, ref x, ref y);
    //    pTip = new Point(80, 0);
    //    p1 = new Point(-80, 40);
    //    p2 = new Point(-80, -40);

    //    RotatePoint(Course, ref pTip);
    //    RotatePoint(Course, ref p1);
    //    RotatePoint(Course, ref p2);

    //    pTip = new Point(500 + pTip.X, 100 + pTip.Y);
    //    p1 = new Point(500 + p1.X, 100 + p1.Y);
    //    p2 = new Point(500 + p2.X, 100 + p2.Y);
    //    Point[] points = { pTip, p1, p2 };
    //    g.FillPolygon(Brushes.Blue, points);
    //}
    public void RotatePoint(int Angle,ref Point p)
    {
         double x, y;
         x = p.X;
         y = p.Y;
         double radians = 22.0 / (7 * (180.0 / (Angle+90)));
         p.X =- Convert.ToInt16(x * Math.Cos(radians) - y * Math.Sin(radians));
         p.Y =- Convert.ToInt16(x * Math.Sin(radians) + y * Math.Cos(radians));
    }
    public void RotatePointShip(int Angle, ref Point p)
    {
        double x, y;
        x = p.X;
        y = p.Y;
        double radians = 22.0 / (7 * (180.0 / Angle));
        p.X = Convert.ToInt16(x * Math.Cos(radians) - y * Math.Sin(radians));
        p.Y = Convert.ToInt16(x * Math.Sin(radians) + y * Math.Cos(radians));
        p.Y =- p.Y;
    }
    private void setSignForScreen(int Course, ref double CordinateX, ref double CordinateY)
    {
        if (Course >= 0 && Course <= 90)
        {
            CordinateY = -CordinateY;
        }
        else if (Course > 90 && Course <= 180)
        {
            CordinateX = -CordinateX;
            CordinateY = -CordinateY;
        }
        else if (Course > 180 && Course <= 270)
        {
            CordinateX = -CordinateX;
        }
        else if (Course > 270 && Course <= 360)
        {
            
        }
   }
    private void addCordinates(ref Point source,double x,double y)
    {
        //if (source.X >= 0 && source.Y >= 0) 
        //{
        //    source.X =source.X   
        //}
        //if (source.X < 0 && source.Y >= 0)
        //{
        //}
        //if (source.X < 0 && source.Y < 0)
        //{
        //    source.X =source.X - x;
        //    source.Y =source.Y + y;
        //}
        //if (source.X >= 0 && source.Y < 0)
        //{
        //}
    }
    private string getVesselName(string VesselCode)
    {
        string Query = "select VesselName from Vessel Where VesselCode='" + VesselCode + "'";
        DataSet ds = VesselReporting.getTable(Query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["VesselName"].ToString();
        }
        else
        {
            return "";
        }
    }
}   
