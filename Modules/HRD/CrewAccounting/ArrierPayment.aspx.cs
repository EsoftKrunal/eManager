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

public partial class ArrierPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_ctm_Message.Text = ""; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 118);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*******************
        if (!Page.IsPostBack)
        {
            ddl_Year.Items.Add(new ListItem("< Select>", "0"));
            ddl_Year.Items.Add(new ListItem((DateTime.Today.Year - 1).ToString(), (DateTime.Today.Year - 1).ToString()));
            ddl_Year.Items.Add(new ListItem(DateTime.Today.Year.ToString(), DateTime.Today.Year.ToString()));
            ddl_Year.Items.Add(new ListItem((DateTime.Today.Year + 1).ToString(), (DateTime.Today.Year + 1).ToString()));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int res;
        if (!(cls_ArearPayment.IS_CREWNUMBER_EXISTS(txt_EmPNo.Text)))
        {
            lbl_ctm_Message.Text = "Crew Member Do Not Exists.";
        }
        try
        {
            res = cls_ArearPayment.INSERTDATA(txt_EmPNo.Text, Convert.ToInt32(ddl_Month.SelectedValue), Convert.ToInt32(ddl_Year.SelectedValue), Convert.ToInt32(Session["loginid"].ToString()));
            if (res==0)
            {
                lbl_ctm_Message.Text="Record Successfully Saved." ;
            }
            else
            {
                lbl_ctm_Message.Text="Please save greater tahn last month." ;
            }
        }
        catch 
        {
            lbl_ctm_Message.Text = "Record Can't Saved.";
        }
        btn_Show_Click(sender, e); 

    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        DataTable dt;
        if(! (cls_ArearPayment.IS_CREWNUMBER_EXISTS(txt_EmPNo.Text)))
        {
            lbl_ctm_Message.Text = "Crew Member Do Not Exists."; 
        }
        dt = cls_ArearPayment.GET_AREARPAYMENT(txt_EmPNo.Text);
        gv_Main.DataSource = dt;
        gv_Main.DataBind(); 
    }
    protected void gv_Main_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt;
        HiddenField hf;
        int a;
        hf =(HiddenField ) gv_Main.Rows[e.RowIndex].FindControl("hfd_Id");
        a = Convert.ToInt32(hf.Value);
        try
        {
           cls_ArearPayment.DeleteData(a);
           lbl_ctm_Message.Text = "Record Successfully Deleted.";
        }
        catch 
        {
            lbl_ctm_Message.Text = "Record Can't Deleted.";
        }
        dt = cls_ArearPayment.GET_AREARPAYMENT(txt_EmPNo.Text);
        gv_Main.DataSource = dt;
        gv_Main.DataBind(); 

    }
}
