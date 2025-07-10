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

public partial class Registers_WageScale : System.Web.UI.Page
{
    public int count_components;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 
        if (!Page.IsPostBack)
        {
            bindwagegrid();
            bindwagecomponentsgrid();
            Session["mode"] = "N";
        }
    }
    public void bindwagegrid()
    {
        DataTable dt2;
        dt2 = wagecomponent.w_componentgdsearch();
        Gd_search.DataSource = dt2;
        Gd_search.DataBind();
        DataTable dt12;
        dt12 = wagecomponent.selectWageScaleComponentDetails();
        count_components = dt12.Rows.Count;
           
            foreach (GridViewRow gr in Gd_search.Rows)
            {
                HiddenField hfdwageId;
                hfdwageId = (HiddenField)gr.FindControl("hiddenwagescaleId");
                int wageid = Convert.ToInt32(hfdwageId.Value.ToString());
               
                DataTable dt15 = wagecomponent.selectWageScaleDetails(wageid);
                foreach (DataRow dr in dt12.Rows)
                {
                    foreach (DataRow dr1 in dt15.Rows)
                    {
                        if (dr["WageScaleComponentId"].ToString() == dr1["WageScaleComponentId"].ToString())
                        {
                            if (Convert.ToInt32(dr1["WageScaleId"].ToString()) == wageid && dr1["Status"].ToString()=="A")
                            {
                                Label lbl = new Label();
                                lbl = (Label)gr.FindControl("lbcomponent");
                                if (lbl.Text == "")
                                {
                                    lbl.Text = dr["ComponentName"].ToString();
                                }
                                else
                                {
                                    lbl.Text = lbl.Text + "&nbsp&nbsp&nbsp,&nbsp&nbsp&nbsp" + dr["ComponentName"].ToString();
                                }
                            }
                            break;
                        }
                    }                 
                }
            }
    }
    public void bindwagecomponentsgrid()
    {
        DataTable dt12;
        dt12 = Budget.getTable("SELECT WageScaleComponentId,ComponentName from WageScaleComponents with(nolock) where ComponentType in ('E','D') order by ComponentName").Tables[0];
        this.chkcomponent.DataValueField = "WageScaleComponentId";
        this.chkcomponent.DataTextField = "ComponentName";
        this.chkcomponent.DataSource = dt12;
        this.chkcomponent.DataBind(); 


        //***** Code To Disable that components whose status is inactive
        for(int i=0;i<this.chkcomponent.Items.Count;i++)
        {
            DataTable dt112 = wagecomponent.selectWageScaleComponentDetailsById(Convert.ToInt16(chkcomponent.Items[i].Value.ToString()));
            foreach (DataRow dr in dt112.Rows)
            {
                if (dr["StatusId"].ToString() == "A")
                {
                    chkcomponent.Items[i].Enabled = true;
                }
                else
                {
                    chkcomponent.Items[i].Enabled = false;
                }
            }

        }


        
    }
    protected void Gd_search_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lb_msg.Text = "";
        bindwagecomponentsgrid();
        HiddenField hfd,hfd1;
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
        hfd = (HiddenField)Gd_search.Rows[row.RowIndex].FindControl("hiddenwagescaleId");
        Session["wgid"] = hfd.Value.ToString();
        hfd1 = (HiddenField)Gd_search.Rows[row.RowIndex].FindControl("hiddenwagescaleName");
        Gd_search.SelectedIndex = row.RowIndex;
        txt_wgscale.Text = hfd1.Value.ToString();
        Session["mode"] = "M";
        DataTable dt15 = wagecomponent.selectWageScaleDetails(Convert.ToInt32(hfd.Value.ToString()));
        foreach (DataRow dr1 in dt15.Rows)
        {
            for (int i = 0; i < this.chkcomponent.Items.Count; i++)
            {
                if (dr1["WageScaleComponentId"].ToString() == chkcomponent.Items[i].Value.ToString() && dr1["Status"].ToString() == "A")
                {
                    chkcomponent.Items[i].Selected = true;
                }
            }
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Gd_search.SelectedIndex = -1;
        lb_msg.Text = "";
        Session["mode"] = null;
        Session["mode"] = "N";
        txt_wgscale.Text = "";
        bindwagegrid();
        bindwagecomponentsgrid();
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in Gd_search.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("hiddenwagescaleName");
            hfd1 = (HiddenField)dg.FindControl("hiddenwagescaleId");

            if (hfd.Value.ToString().ToUpper().Trim() == txt_wgscale.Text.ToUpper().Trim())
            {
                if (Session["mode"] == "N")
                {
                    lb_msg.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (Session["wgid"].ToString().Trim() != hfd1.Value.ToString())
                {
                    lb_msg.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lb_msg.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            if (Session["mode"] == "M")
            {
                wagecomponent.deletewagedetail(Convert.ToInt32(Session["wgid"]));
            }

           string rt = "";
           string status="";
           for (int i = 0; i < this.chkcomponent.Items.Count; i++)
                {
               if(this.chkcomponent.Items[i].Selected==true)
               {
                    if (rt == "")
                    {
                        rt = chkcomponent.Items[i].Value.ToString();
                        
                    }
                    else
                    {
                        rt = rt + ',' + chkcomponent.Items[i].Value.ToString();
                       
                    }
           }
                      }
                try
                {
                    if (Session["mode"] == "M")
                    {
                        wagecomponent.insertdata1(txt_wgscale.Text, rt, Convert.ToInt32(Session["loginid"]),status,Convert.ToInt32(Session["wgid"]));
                    }
                    else
                    {
                        wagecomponent.insertdata1(txt_wgscale.Text, rt, Convert.ToInt32(Session["loginid"]),status,-1);
                    }
                    lb_msg.Text = "Record Saved Successfully.";

                }
                catch
                {
                    lb_msg.Text = "Record Can't Saved.";
                }
           
          
              
            }
            bindwagegrid();
        
        btn_clear_Click(sender, e);
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        Gd_search.SelectedIndex = -1;
        lb_msg.Text = "";
        Session["mode"] = "N";
        txt_wgscale.Text = "";
        bindwagegrid();
        bindwagecomponentsgrid();
    }
    
}
