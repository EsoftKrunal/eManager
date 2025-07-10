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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class CrewOperation_WageScaleMasterNew : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    private void Load_WageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        dp_WSname.DataSource = ds.Tables[0];
        dp_WSname.DataTextField = "WageScaleName";
        dp_WSname.DataValueField = "WageScaleID";
        dp_WSname.DataBind();
        dp_WSname.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lb_msg.Text = "";
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            Load_WageScale();
        }
        //this.txt_Seniority.Attributes.Add("onKeyPress","javascript:if(event.keyCode==13){document.getElementById('"+ btn_Show.UniqueID +"').focus();}");
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        string datastr, headerstr;
        string RankCode;
        double check;
        int WageScaleId, Seniority, NationalityId=0;
        HiddenField hfd;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        
        check = 0;
        RankCode = "";
        datastr = "";
        headerstr = "";
        if (this.Txt_WEF.Text == "")
        {
            ShowMessage("WEF Date Can Not Be Blank ", true); 
            return;
        }
        if(hwef.Value!="" && Convert.ToDateTime(hwef.Value)>Convert.ToDateTime(this.Txt_WEF.Text))
        {
            ShowMessage("WEF Date Can Not Be Less Than " + this.hwef.Value.ToString(),true);
            return;
        }
        if(Convert.ToDateTime(this.Txt_WEF.Text).Day !=1)
        {
            ShowMessage("WEF date should be start date of month.",true);
            return;
        }

        DataTable dt;
        try
        {
            //if (this.hfwsid.Value.ToString().Trim() != "")
            //{
            //   WagesMaster.Insert_WagerankHistory(Convert.ToInt16(this.hfwsid.Value.ToString()), Convert.ToDateTime(this.Txt_WEF.Text),);
                
            //}
            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            headerstr =
                Label1.Text.Trim() + "," +
                Label2.Text.Trim() + "," +
                Label3.Text.Trim() + "," +
                Label4.Text.Trim() + "," +
                Label5.Text.Trim() + "," +
                Label6.Text.Trim() + "," +
                Label7.Text.Trim() + "," +
                Label8.Text.Trim() + "," +
                Label9.Text.Trim() + "," +
                Label10.Text.Trim() + "," +
                Label11.Text.Trim() + "," +
                Label12.Text.Trim();
      
            if (dt.Columns.Count >= 1)
            {
                WagesMaster.InsertWageScalesRankDetails(WageScaleId, NationalityId, Seniority, Txt_WEF.Text.Trim(), 0, UserId);
                for (int count = 0; count <= rptWages.Items.Count-1; count++)
                {
                    RankCode = ((Label)rptWages.Items[count].FindControl("lblRankCode")).Text;
                    datastr = "";
                    check = 0;
                    for (int i = 1; i < dt.Columns.Count - 1; i++)
                    {
                        CheckBox chk = (CheckBox)(this.FindControl("CheckBox" + i.ToString()));
                        TextBox t1 = ((TextBox)rptWages.Items[count].FindControl("txtC" + i.ToString()));
                        if (chk.Checked)
                        {
                            datastr = datastr + "," + t1.Text;
                            check = check + Convert.ToDouble(t1.Text);
                        }
                        else
                        {
                            datastr = datastr + ",-1" ;
                            check = check + 0 ;
                        }
                    }
                    datastr = datastr.Substring(1);
                    WagesMaster.UpdateWageComponentsDetails(WageScaleId, NationalityId, Seniority, RankCode, headerstr, datastr,0);
                }
            }
            ShowMessage("Record Saved Successfully.", false);
        }
        catch (Exception ex) { ShowMessage("Record Can't Saved. Error :" + ex.Message, true); }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {   
        int WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        BindRepeater();
        btnModify.Visible = true;
        btn_Show_History.Visible = true;
        tbl_Save.Visible = false;
    }
    protected void btn_ShowHistory_Click(object sender, EventArgs e)
    {
        DataTable dt = WagesMaster.Get_Wage_Master_History(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
        rpt_History.DataSource = dt;
        rpt_History.DataBind();
        dv_History.Visible = true;
    }
    protected void btn_CloseHistory_Click(object sender, EventArgs e)
    {
        dv_History.Visible = false;
    }
    protected void btn_Open_Click(object sender, EventArgs e)
    {
        int HistoryId = Common.CastAsInt32(Request.Form["his"]);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "afd", "window.open('ViewWageScaleHistory.aspx?WageScaleRankId=" + HistoryId + "','');", true);
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptWages.Items)
        {
            foreach (Control c in ri.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Enabled = true;
                    ((TextBox)c).CssClass="ctltext";
                }
            }
        }
        tbl_Save.Visible = true;
    }
    protected void BindRepeater()
    {
        DataTable dt = WagesMaster.get_HeaderDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0,Common.CastAsInt32(txt_Seniority.Text));
        if (dt.Rows.Count > 0)
        {
           
            if (Convert.IsDBNull(dt.Rows[0][0]))
            {
                hfwsid.Value = "";
                Txt_WEF.Text = "";
                lblEffectiveFrom.Text = "";
                this.hwef.Value = "";
            }
            else
            {
                hfwsid.Value = dt.Rows[0][2].ToString();
                Txt_WEF.Text = Common.ToDateString(dt.Rows[0][0]);
                lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0][0]);
                hwef.Value = Common.ToDateString(dt.Rows[0][0]);
            }
        }
        else
        {
            hfwsid.Value = "";
            Txt_WEF.Text = "";
            lblEffectiveFrom.Text = "";
            hwef.Value = "";
        }
        for (int i=1;i<=12;i++)
        {
            dt = wagecomponent.selectWageScaleDetailsById(i, Convert.ToInt32(dp_WSname.SelectedValue), 0, Convert.ToInt32(txt_Seniority.Text));

            if (dt.Rows.Count > 0)
            {
                ((CheckBox)this.FindControl("CheckBox" + i.ToString())).Checked = (dt.Rows[0][0].ToString() == "Y");
                ((Label)this.FindControl("Label" + i.ToString())).Text = dt.Rows[0][1].ToString();
            }

           
        }

        dt = WagesMaster.WageComponents(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
        rptWages.DataSource = dt;
        rptWages.DataBind();
    }
    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }
    public void ShowMessage(string Message, bool Error)
    {
        lb_msg.Text = Message;
        lb_msg.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
}