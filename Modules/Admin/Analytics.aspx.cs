using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Activities.Expressions;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_AdminPanel_Analytics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateTable();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton Imgbtn = (ImageButton)sender;
            if (Imgbtn.ToolTip == "HRD")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('../HRD/CrewRecord/Chart.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('../Admin/UnderConst.aspx');", true);
            }
        }
        catch(Exception ex)
        {

        } 
    }
    private void CreateTable()
    {
        string strSQL = "SELECT ModuleId,ModuleName,ImageURL,PURL  FROM DBO.Appmstr_Module WHERE IsActive = 1 and ModuleName not in ( 'MASTERS','ADMIN','ANALYTIC')  order by DisOrder ASC";
        DataTable dtModules = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtModules.Rows.Count > 0)
        {
            double total = dtModules.Rows.Count;
            double cellcount = 3;
            double rowcount = Math.Ceiling(total / cellcount);

            Table tb = new Table();
            tb.Width = Unit.Percentage(75);
            tb.Height = Unit.Pixel(450);
            tb.HorizontalAlign = HorizontalAlign.Center;
            // tb.BorderWidth = Unit.Pixel(1);
            // tb.BorderColor = System.Drawing.Color.Gray;
            int dbcount = 0;
            Font LargeFont = new Font("Arial", 14);
            for (int i = 1; i <= Convert.ToInt32(rowcount); i++)

            {
                TableRow tr = new TableRow();
                tr.Height = Unit.Pixel(150);
                for (int j = 1; j <= 3; j++)

                {
                    TableCell td = new TableCell();
                    td.Width = Unit.Pixel(150);
                    td.Height = Unit.Pixel(75);
                    td.Attributes.Add("style", "padding: 10px 20px 15px 0px;");
                    //td.BorderWidth = Unit.Pixel(2);
                    //td.BorderColor = System.Drawing.ColorTranslator.FromHtml("#4c7a6f"); 
                    td.HorizontalAlign = HorizontalAlign.Center;
                    td.VerticalAlign = VerticalAlign.Middle;

                    if (dbcount < total)
                    {
                        string text = dtModules.Rows[dbcount]["ModuleName"].ToString();
                        string id = dtModules.Rows[dbcount]["ModuleId"].ToString();
                        string imageURL = dtModules.Rows[dbcount]["ImageURL"].ToString();
                        string pageURL = dtModules.Rows[dbcount]["PURL"].ToString();
                        /* dynamically Create  Image button */
                        ImageButton img = new ImageButton();
                        img.ID = "ib" + id.ToString();
                        img.ImageUrl = imageURL;
                        img.Width = Unit.Pixel(100);
                        img.Height = Unit.Pixel(100);
                        img.ImageAlign = ImageAlign.Middle;
                        img.Attributes.Add("style", "padding:10px;");
                        img.ToolTip = text;
                        //img.OnClientClick = "return false";
                        img.Attributes.Add("AutoPostback", "true");
                        img.Click += new ImageClickEventHandler(ImageButton1_Click);
                        // img.OnClientClick += new ImageClickEventHandler(Open_Candidate);
                        /* dynamically Create  Lable */
                        Label c_lable = new Label();
                        c_lable.Visible = true;
                        c_lable.Text = text;
                        c_lable.ID = "lbl" + id.ToString();
                        c_lable.ForeColor = System.Drawing.ColorTranslator.FromHtml("#206020");
                        c_lable.Width = Unit.Pixel(100);
                        c_lable.Attributes.Add("style", "text-align:center;Font-Family:Arial;font-size: 16px;font-weight:bold;");
                        /* dynamically Create  DIV tag */
                        var div = new HtmlGenericControl("div");
                        div.ID = "div" + id.ToString();
                        div.Attributes.Add("style", "border: 1px solid #4C7A6F; border-radius: 10px;width:125;height:125px;");
                        div.Controls.Add(img);
                        div.Controls.Add(new LiteralControl("<br />"));
                        div.Controls.Add(c_lable);

                        td.Controls.Add(div);
                        //td.Controls.Add(new LiteralControl("<br />"));
                        //td.Controls.Add(c_lable);
                        dbcount = dbcount + 1;
                    }
                    tr.Cells.Add(td);
                    tb.Rows.Add(tr);
                    Panel1.Controls.Add(tb);
                }

            }
        }
        else
        {
            lblMsg.Text = "No Record available";
        }

    }
}