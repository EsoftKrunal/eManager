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
using System.Configuration;

public partial class Modules_HRD_HRDDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateTable();
    }

    private void CreateTable()
    {
        

        string strSQL = "EXEC usp_GetDashboardMenuBy_UserID "+ Convert.ToInt32(Session["loginid"]) + ",1";
        
        DataTable dtModules = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtModules.Rows.Count > 0)
        {

           
                double total = dtModules.Rows.Count;
                double cellcount = 4;
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
                    for (int j = 1; j <= 4; j++)

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
                            string text = dtModules.Rows[dbcount]["MenuName"].ToString();
                            string id = dtModules.Rows[dbcount]["MenuID"].ToString();
                            string imageURL = dtModules.Rows[dbcount]["ImageUrl"].ToString();
                            string pageURL = dtModules.Rows[dbcount]["PageUrl"].ToString();

                            /* dynamically Create  Image button */
                            ImageButton img = new ImageButton();
                            img.ID = "ib" + id.ToString();
                            img.ImageUrl = imageURL;
                            img.CommandArgument = id;
                            // img.Width = Unit.Pixel(25);
                            // img.Height = Unit.Pixel(33);
                            img.ImageAlign = ImageAlign.Middle;
                            // img.BackColor = Color.Black;
                            img.Attributes.Add("style", "padding:10px;background-color: #4c7a6f");
                            img.ToolTip = text.ToUpper();
                            //img.OnClientClick = "return false";
                            img.Attributes.Add("AutoPostback", "true");
                            img.Click += new ImageClickEventHandler(ImageButton1_Click);
                            // img.OnClientClick += new ImageClickEventHandler(Open_Candidate);
                            /* dynamically Create  Lable */
                            Label c_lable = new Label();
                            c_lable.Visible = true;
                            c_lable.Text = text.ToUpper();
                            c_lable.ID = "lbl" + id.ToString();
                            c_lable.ForeColor = System.Drawing.ColorTranslator.FromHtml("#206020");
                            c_lable.Width = Unit.Pixel(135);
                            c_lable.Attributes.Add("style", "text-align:center;Font-Family:Arial;font-size: 16px;font-weight:bold;");
                            /* dynamically Create  DIV tag */
                            var div = new HtmlGenericControl("div");
                            div.ID = "div" + id.ToString();
                            div.Attributes.Add("style", "border: 1px solid #4C7A6F; border-radius: 10px;width:125;height:125px;padding:5px;");
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

    private bool IsHavingMenuItemRights(string pageurl)
    {
        energiosSecurity.User usr = new energiosSecurity.User();
        DataTable dtActionRights = new DataTable();


        int uid = Convert.ToInt32(Session["loginid"]);
        dtActionRights = usr.GetPageRightsByUserID(uid, pageurl);
        if (dtActionRights.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtActionRights.Rows[0]["CanView"]) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
        else
        {
            return false;
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton Imgbtn = (ImageButton)sender;
            String sql = "Select PageUrl, mo.ModuleName As AppNamel from appmstr_Page p with(nolock) Inner Join appmstr_Menu m with(nolock) on P.PageID = m.PageID inner join appmstr_Module mo with(nolock) on m.ModuleID  = mo.ModuleID where m.MenuID = '" + Imgbtn.CommandArgument + "'";
            DataTable dtModules = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dtModules.Rows.Count > 0)
            {
                string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                string pageURL = appname + "/" + dtModules.Rows[0]["PageUrl"].ToString();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('/" + pageURL.ToString() + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('../Admin/UnderConst.aspx');", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}