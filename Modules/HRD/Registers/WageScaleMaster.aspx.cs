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

public partial class CrewOperation_WageScaleMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
       
        this.txt_Seniority.Attributes.Add("onKeyPress","javascript:if(event.keyCode==13){document.getElementById('"+ btn_Show.UniqueID +"').focus();}");
        this.txtcopyseniority.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + btncopyshow.UniqueID + "').focus();}");
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 

        lb_msg.Text = "";
        if (!Page.IsPostBack)
        {
            Loadrank();
            loadVessel();
            wage_MasterName();
            bind_country();
          
        }
    }
    public void Loadrank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankName");
      
    }
    public void loadVessel()
    {
        //DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselCode", Convert.ToInt32(Session["loginid"].ToString()));
        //chklst_Vessel.DataSource = ds.Tables[0];
        //chklst_Vessel.DataTextField = "VesselCode";
        //chklst_Vessel.DataValueField = "VesselId";
        //chklst_Vessel.DataBind();
    }
    private void wage_MasterName()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        dp_WSname.DataSource = ds.Tables[0];
        dp_WSname.DataTextField = "WageScaleName";
        dp_WSname.DataValueField = "WageScaleID";
        dp_WSname.DataBind();
        dp_WSname.Items.Insert(0, new ListItem("< Select >", "0"));


        ddcopywage.DataSource = ds.Tables[0];
        ddcopywage.DataTextField = "WageScaleName";
        ddcopywage.DataValueField = "WageScaleID";
        ddcopywage.DataBind();
        ddcopywage.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bind_country()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryID", "CountryName");
        dp_nationality.DataSource = ds.Tables[0];
        dp_nationality.DataTextField = "CountryName";
        dp_nationality.DataValueField = "CountryID";
        dp_nationality.DataBind();
        dp_nationality.Items.Insert(0, new ListItem("< Select >", "0"));

        ddcopynationality.DataSource = ds.Tables[0];
        ddcopynationality.DataTextField = "CountryName";
        ddcopynationality.DataValueField = "CountryID";
        ddcopynationality.DataBind();
        ddcopynationality.Items.Insert(0, new ListItem("< Select >", "0"));
    }
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
    protected void Row_Deleting(object sender, GridViewDeleteEventArgs e)
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
                headerstr = headerstr.Substring(1);

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

            //datastr = "";
            //for (int count = 0; count < chklst_Vessel.Items.Count; count++)
            //{
            //    if (chklst_Vessel.Items[count].Selected)
            //    {
            //        datastr = datastr + ',' + chklst_Vessel.Items[count].Value;
            //    }
            //}
            //if (datastr != "") { datastr = datastr.Substring(1); };
            //WagesMaster.InsertVessels(WageScaleId, datastr);
            lb_msg.Text = "Record Successfully Saved.";
            bindgrid();
            LoadHistory();
        }
        catch { lb_msg.Text = "Record Can't Saved."; }



    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        int i, WageScaleId, VesselId;
        Session["PressMode"] = "Show";
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        //ViewState.Add("WageScaleId", WageScaleId);
        //ViewState.Add("nationalityid", Convert.ToInt32(this.dp_nationality.SelectedValue));
        //ViewState.Add("Seniority", Convert.ToInt32(txt_Seniority.Text));
        bindgrid();
        LoadHistory();
        //for (i = 0; i < chklst_Vessel.Items.Count; i++)
        //{
        //    VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
        //    chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(WageScaleId, VesselId);
        //}
        btn_Save.Enabled = true;
    }
    public void LoadHistory()
    {
        DataTable dt;
        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        NationalityId = Convert.ToInt32(dp_nationality.SelectedValue);
        dt=WagesMaster.Get_Wage_Master_History(WageScaleId, NationalityId, Seniority);
        ddl_History.DataSource = dt;
        ddl_History.DataTextField ="wefdate";
        ddl_History.DataValueField = "wagescalerankid";
        ddl_History.DataBind();
        ddl_History.Items.Insert(0, "< Select >");  
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
                    dt = wagecomponent.selectWageScaleDetailsById(i, Convert.ToInt32(ddcopywage.SelectedValue), Convert.ToInt32(ddcopynationality.SelectedValue), Convert.ToInt32(txtcopyseniority.Text));
                }
                else // - History
                {
                    dt = wagecomponent.selectWageScaleDetailsById_History(i,Convert.ToInt32(ddl_History.SelectedValue));
                }
                //-----------------
                foreach (DataRow dr in dt.Rows)
                {
                    string str1 = "";
                    string str2="";
                    str1 = "lbl" + i.ToString();
                    str2="chk"+i.ToString();
                    Label lbl = ((Label)e.Row.FindControl(str1));
                    lbl.Text = dr["ComponentName"].ToString();

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
    public void docheck()
    { 

    }
    protected void btncopyshow_Click(object sender, EventArgs e)
    {
        int i, WageScaleId, VesselId;
        Session["PressMode"] = "Copy";
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        //ViewState.Add("WageScaleId", WageScaleId);
        //ViewState.Add("nationalityid", Convert.ToInt32(this.ddcopynationality.SelectedValue));
        //ViewState.Add("Seniority", Convert.ToInt32(this.txtcopyseniority.Text));
        bindgrid_copy();

        //for (i = 0; i < chklst_Vessel.Items.Count; i++)
        //{
        //    VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
        //    chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(WageScaleId, VesselId);
        //}
    }
    protected void dp_WSname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_Seniority.Text.Trim() == "")
        {
            txt_Seniority.Text = "0"; 
        }
        btn_Show_Click(sender, e); 
    }
    protected void dp_nationality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txt_Seniority.Text.Trim() == "")
        {
            txt_Seniority.Text = "0";
        }
        btn_Show_Click(sender, e);
    }
    protected void txt_Seniority_TextChanged(object sender, EventArgs e)
    {
        if (txt_Seniority.Text.Trim() == "")
        {
            txt_Seniority.Text = "0";
        }
        
        btn_Show_Click(sender, e);
    }

    #region GRID FILLING

        public void bindgrid()
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
            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            Gd_Wage.DataSource = dt;
            Gd_Wage.DataBind();

        }
        public void bindgrid_copy()
        {
            int WageScaleId, Seniority, NationalityId;
            WageScaleId = Convert.ToInt32(this.ddcopywage.SelectedValue);
            Seniority = Convert.ToInt32(this.txtcopyseniority.Text);
            NationalityId = Convert.ToInt32(this.ddcopynationality.SelectedValue);

            DataTable dt;
            dt = WagesMaster.get_HeaderDetails(WageScaleId, NationalityId, Seniority);
            if (dt.Rows.Count > 0)
            {
                if (Convert.IsDBNull(dt.Rows[0][0]))
                { Txt_WEF.Text = ""; }
                else
                { Txt_WEF.Text = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy"); }

            }
            else
            {
                Txt_WEF.Text = "";

            }

            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            Gd_Wage.DataSource = dt;
            Gd_Wage.DataBind();
        }
        protected void ddl_History_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;
            if (ddl_History.SelectedIndex == 0)
            {
                int i, WageScaleId, VesselId;
                Session["PressMode"] = "Show";
                WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
                //ViewState.Add("WageScaleId", WageScaleId);
                //ViewState.Add("nationalityid", Convert.ToInt32(this.dp_nationality.SelectedValue));
                //ViewState.Add("Seniority", Convert.ToInt32(txt_Seniority.Text));
                bindgrid();
                //for (i = 0; i < chklst_Vessel.Items.Count; i++)
                //{
                //    VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
                //    chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(WageScaleId, VesselId);
                //}
                btn_Save.Enabled = true;
            }
            else
            {
                Session["PressMode"] = "History";
                dt = WagesMaster.WageComponents_FromHistory(Convert.ToInt32(ddl_History.SelectedValue));
                Gd_Wage.DataSource = dt;
                Gd_Wage.DataBind();
                btn_Save.Enabled = false;
            }
        }
      
    #endregion
}