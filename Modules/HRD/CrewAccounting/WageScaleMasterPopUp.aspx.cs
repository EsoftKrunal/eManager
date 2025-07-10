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

public partial class CrewOperation_WageScaleMasterPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
         //***********Code to check page acessing Permission
        this.txt_Seniority.Attributes.Add("onKeyPress","javascript:if(event.keyCode==13){document.getElementById('"+ btn_Show.UniqueID +"').focus();}");
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 225);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //******************* 
        lb_msg.Text = "";
        if (!Page.IsPostBack)
        {
            Load_WageScale();
            Load_Country();
        }
    }
    # region -------- Master Loading ----------
    private void Load_WageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        dp_WSname.DataSource = ds.Tables[0];
        dp_WSname.DataTextField = "WageScaleName";
        dp_WSname.DataValueField = "WageScaleID";
        dp_WSname.DataBind();
        dp_WSname.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_Country()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryID", "CountryName");
        dp_nationality.DataSource = ds.Tables[0];
        dp_nationality.DataTextField = "CountryName";
        dp_nationality.DataValueField = "CountryID";
        dp_nationality.DataBind();
        dp_nationality.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    # endregion
    private void Show_Data()
    {
        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        NationalityId = Convert.ToInt32(dp_nationality.SelectedValue);

        DataTable dt;
        dt = WagesMaster.get_HeaderDetails(WageScaleId, NationalityId, Seniority);
        if (dt.Rows.Count > 0)
        {
            hfwsid.Value = dt.Rows[0][2].ToString();
            if (Convert.IsDBNull(dt.Rows[0][0]))
            {
                Txt_WEF.Text = "";
                this.hwef.Value = "";
            }
            else
            {
                hwef.Value = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
            Txt_WEF.Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
            }
            dp_nationality.SelectedValue = dt.Rows[0][1].ToString();
        }
        else
        {
            Txt_WEF.Text = "";
            dp_nationality.SelectedIndex = 0;
        }

        bindgrid();
    }
    private void set_Eff_Rank()
    {

    }
    protected void Row_Editing(object sender, GridViewEditEventArgs e)
    {
        TextBox txt;
        int index;
        string RankCode;

        index = e.NewEditIndex;
        RankCode = Gd_Wage.Rows[index].Cells[1].Text;
        Gd_Wage.SelectedIndex = e.NewEditIndex;

        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        NationalityId = Convert.ToInt32(dp_nationality.SelectedValue);

        DataTable dt;
        dt = WagesMaster.WageComponentsDetails(WageScaleId, NationalityId, Seniority, RankCode);
        
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        string datastr, headerstr;
        string RankCode;
        double check;
        int WageScaleId, Seniority, NationalityId;
        HiddenField hfd;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        NationalityId = Convert.ToInt32(dp_nationality.SelectedValue);

        check = 0;
        RankCode = "";
        datastr = "";
        headerstr = "";
        if (this.Txt_WEF.Text == "")
        {
            lb_msg.Text = "WEF Date Can Not Be Blank "; 
            return;
        }
        if(hwef.Value!="" && Convert.ToDateTime(hwef.Value)>Convert.ToDateTime(this.Txt_WEF.Text))
        {
            lb_msg.Text = "WEF Date Can Not Be Less Than " + this.hwef.Value.ToString();
            return;
            }
        if(Convert.ToDateTime(this.Txt_WEF.Text).Day !=1)
        { 
            lb_msg.Text = "WEF date should be start date of month";
            return;
        }

        DataTable dt;
        try
        {
            //if (this.hfwsid.Value.ToString().Trim() != "")
            //{
            //    WagesMaster.Insert_WagerankHistory(Convert.ToInt16(this.hfwsid.Value.ToString()), Convert.ToDateTime(this.Txt_WEF.Text));
            //}
        dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);


        
        for (int i = 1; i <= 12; i++)
        {
            string str1 = "";
            str1 = "lbl" + i.ToString();
            Label lbl = ((Label)Gd_Wage.HeaderRow.FindControl(str1));
           
            headerstr = headerstr + ',' + lbl.Text.Trim();
        }
      
      
            if (dt.Columns.Count >= 1)
            {
                headerstr = headerstr.Substring(1).Replace("<br/>", " ");

                WagesMaster.InsertWageScalesRankDetails(WageScaleId, NationalityId, Seniority, Txt_WEF.Text.Trim(), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(Session["loginid"].ToString()));
                for (int count = 0; count <= Gd_Wage.Rows.Count-1; count++)
                {
                    RankCode = Gd_Wage.Rows[count].Cells[0].Text;
                    datastr = "";
                    check = 0;
                    for (int i = 1; i < dt.Columns.Count - 1; i++)
                    {
                        string strtxt = "txt";
                        strtxt = strtxt + i.ToString();

                        string str1 = "";
                        str1 = "chk" + i.ToString();
                        CheckBox chk = ((CheckBox)Gd_Wage.HeaderRow.FindControl(str1));
                        
                        

                        TextBox t1 = ((TextBox)Gd_Wage.Rows[count].FindControl(strtxt));
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
            lb_msg.Text = "Record Successfully Saved.";
            bindgrid();
        }
        catch { lb_msg.Text = "Record Can't Saved."; }



    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        int i, WageScaleId, VesselId;
        Session["PressMode"] = "Show";
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        bindgrid();
        btn_Save.Enabled = true;
    }
    protected void Gd_Wage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 1; i <= 12; i++)
            {
                //-------------------
                if (Session["PressMode"].ToString() == "Show")
                {
                dt = wagecomponent.selectWageScaleDetailsById(i, Convert.ToInt32(dp_WSname.SelectedValue), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(txt_Seniority.Text));
                }
                else if (Session["PressMode"].ToString() == "Copy")
                {
                    dt = wagecomponent.selectWageScaleDetailsById(i, Convert.ToInt32(Session["Copy_WageScaleId"]), Convert.ToInt32(Session["Copy_NationalityId"]), Convert.ToInt32(Session["Copy_Seniority"]));
                }
                else // - History
                {
                    dt = wagecomponent.selectWageScaleDetailsById_History(i, Convert.ToInt32(Session["History_WageComponentsId"]));
                }
                //-----------------
                foreach (DataRow dr in dt.Rows)
                {
                    string str1 = "";
                    string str2="";
                    str1 = "lbl" + i.ToString();
                    str2="chk"+i.ToString();
                    Label lbl = ((Label)e.Row.FindControl(str1));
                    lbl.Text = dr["ComponentName"].ToString().Replace(" ","<br/>");

                    CheckBox chk = ((CheckBox)e.Row.FindControl(str2));
                    chk.Checked =(dr["Status"].ToString().Trim().ToUpper()=="Y")?true:false; 
                  
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i <= 12; i++)
            {
                string str;
                str = "txt" + i.ToString();
                TextBox txt1 = ((TextBox)e.Row.FindControl(str));
                txt1.Text = Convert.ToDouble(txt1.Text).ToString("0.00");

            }
        }
    }
    #region GRID FILLING
        public void bindgrid()
        {
            int WageScaleId, Seniority, NationalityId;
            WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
            Seniority = Convert.ToInt32(txt_Seniority.Text);
            NationalityId = Convert.ToInt32(dp_nationality.SelectedValue);

            Session["Show_WageScaleId"] = WageScaleId.ToString();
            Session["Show_Seniority"] = Seniority.ToString();
            Session["Show_NationalityId"] = NationalityId.ToString();

            DataTable dt;
            dt = WagesMaster.get_HeaderDetails(WageScaleId, NationalityId, Seniority);
            if (dt.Rows.Count > 0)
            {
                hfwsid.Value = dt.Rows[0][2].ToString();
                if (Convert.IsDBNull(dt.Rows[0][0]))
                {
                    Txt_WEF.Text = "";
                    this.hwef.Value = "";
                }
                else
                {
                    Txt_WEF.Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
                    
                    this.hwef.Value = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
                }
            }
            else
            {
                hfwsid.Value = "";
                Txt_WEF.Text = "";
                this.hwef.Value = "";
            }
            Session["EffDate"] = Txt_WEF.Text;  
            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            Gd_Wage.DataSource = dt;
            Gd_Wage.DataBind();

        }
    #endregion
    protected void btn_ShowHistory_Click(object sender, EventArgs e)
    {
        DataTable dt;
        Session["PressMode"] = "History";
        dt = WagesMaster.WageComponents_FromHistory(Convert.ToInt32(txtHistory.Text));
        Session["History_WageComponentsId"] = txtHistory.Text;
        Gd_Wage.DataSource = dt;
        Gd_Wage.DataBind();
        btn_Save.Enabled = false;
        DataTable dt1= Budget.getTable("select wefdate from wagescalerankhistory where wagescalerankid=" + txtHistory.Text).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            Txt_WEF.Text = DateTime.Parse(dt1.Rows[0][0].ToString()).ToString("dd-MMM-yyyy");
            Session["EffDate"] = Txt_WEF.Text;
        }
    }
    protected void btn_ShowCopy_Click(object sender, EventArgs e)
        {
            int WageScaleId, Seniority, NationalityId;
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^WCId=([0-9]*)&Sen=([0-9]*)&Nat=([0-9]*)$");
            System.Text.RegularExpressions.Match m = re.Match(txtCopy.Text);
            if (m.Success)
            {
                Session["PressMode"] = "Copy";
                WageScaleId = int.Parse(m.Groups[1].Value);
                Seniority = int.Parse(m.Groups[2].Value);
                NationalityId = int.Parse(m.Groups[3].Value);

                Session["Copy_WageScaleId"]=WageScaleId.ToString();
                Session["Copy_Seniority"] = Seniority.ToString();
                Session["Copy_NationalityId"] = NationalityId.ToString();

                DataTable dt;
                dt = WagesMaster.get_HeaderDetails(WageScaleId, NationalityId, Seniority);
                //if (dt.Rows.Count > 0)
                //{
                //    if (Convert.IsDBNull(dt.Rows[0][0]))
                //    { Txt_WEF.Text = ""; }
                //    else
                //    { Txt_WEF.Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy"); }
                    Session["EffDate"] = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
                //}
                //else
                //{
                    Txt_WEF.Text = "";
                //}
                dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
                Gd_Wage.DataSource = dt;
                Gd_Wage.DataBind();
            }
        }
}