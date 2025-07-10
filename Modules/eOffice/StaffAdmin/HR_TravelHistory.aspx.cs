using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_TravelHistory : System.Web.UI.Page
{
    DataSet ds;
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Session["HrDocs"] != null)
        {
            if (Session["HrDocs"].ToString() != "")
            {
                UserControl uc = new UserControl();
                uc = LoadControl("Emtm_HR_TravelDocument.ascx") as UserControl;

                PlaceHolder1.Controls.Clear();
                PlaceHolder1.Controls.Add(uc);

                Session["Current"] = 1;
                Session["CurrentModule"] = 3;
             
                tblview.Visible = false;
                ds = new DataSet();
                BindGrid();

            }
            else
            {
                PlaceHolder1.Controls.Clear();

                lblUserFullName.Text = "Pankaj K. Verma";
                Session["CurrentModule"] = 3;

                tblview.Visible = false;
                ds = new DataSet();
                BindGrid();
            }
        }
        else
        {
            lblUserFullName.Text = "Pankaj K. Verma";
            Session["CurrentModule"] = 3;

            tblview.Visible = false;
            ds = new DataSet();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        ds.Clear();
        ds.ReadXml(MapPath("TravelHistory.xml"));
        rptRunningHour.DataSource = ds;
        rptRunningHour.DataBind();
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        tblview.Visible = true;
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
    }

}
