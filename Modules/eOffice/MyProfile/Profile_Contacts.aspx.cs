using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emtm_Profile_Contacts : System.Web.UI.Page
{
    public AuthenticationManager auth; 
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

    # region User Functions
    protected void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_ContactDetails WHERE EMPID=" + EmpId.ToString());
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    SelectedId = Common.CastAsInt32(dr["ContactId"].ToString());

                    string Number = dr["Telephone"].ToString();
                    string[] NumberColumn = Number.Split(new char[] { '-' });

                    string mobile = dr["Mobile"].ToString();
                    string[] mobileColumn = mobile.Split(new char[] { '-' });

                    string fax = dr["Fax"].ToString();
                    string[] faxColumn = fax.Split(new char[] { '-' });


                    string OfficeTelephone = dr["OfficeTelephone"].ToString();
                    string[] OfficeTel = OfficeTelephone.Split(new char[] { '-' });

                    string OfficeMob = dr["OfficeMobile"].ToString();
                    string[] MobFax = OfficeMob.Split(new char[] { '-' });

                    txtaddress1.Text = dr["Address1"].ToString();
                    txtaddress2.Text = dr["Address2"].ToString();
                    txtaddress3.Text = dr["Address3"].ToString();

                    ddlcountry.SelectedValue = dr["Country"].ToString();
                    txtstate.Text = dr["State"].ToString();

                    txtcity.Text = dr["City"].ToString();
                    txtzip.Text = dr["ZipCode"].ToString();
                    txtnumcntycode.Text = NumberColumn[0];
                    txtareacode.Text = NumberColumn[1];
                    txtnumber.Text = NumberColumn[2];
                    txtmobcntrycode.Text = mobileColumn[0];
                    txtmobno.Text = mobileColumn[1];
                    txtfaxcntrycode.Text = faxColumn[0];
                    txtfaxareacode.Text = faxColumn[1];
                    txtfax.Text = faxColumn[2];
                    txtPersonalEmail.Text = dr["Email1"].ToString();
                    txtOffEmail.Text = dr["Email2"].ToString();

                    if (OfficeTel.Length > 1)
                    {
                        txtOffCntryCode.Text = OfficeTel[0];
                        txtOffAreaCode.Text = OfficeTel[1];
                        txtOffNumber.Text = OfficeTel[2];
                    }

                    if (MobFax.Length > 1)
                    {
                        txtOffMobCntryCode.Text = MobFax[0];
                        txtOffMobNumber.Text = MobFax[1];
                    }


                    dr = dt.Rows[1];

                    string Numbers = dr["Telephone"].ToString();
                    string[] Number_Column = Numbers.Split(new char[] { '-' });

                    string mobiles = dr["Mobile"].ToString();
                    string[] mobile_Column = mobiles.Split(new char[] { '-' });

                    string faxs = dr["Fax"].ToString();
                    string[] fax_Column = faxs.Split(new char[] { '-' });

                    txt_address1.Text = dr["Address1"].ToString();
                    txt_address2.Text = dr["Address2"].ToString();
                    txt_address3.Text = dr["Address3"].ToString();

                    ddl_country.SelectedValue = dr["Country"].ToString();
                    txt_state.Text = dr["State"].ToString();

                    txt_city.Text = dr["City"].ToString();
                    txt_zip.Text = dr["ZipCode"].ToString();
                    txt_numcntycode.Text = Number_Column[0];
                    txt_areacode.Text = Number_Column[1];
                    txt_number.Text = Number_Column[2];
                    txt_mobcntrycode.Text = mobile_Column[0];
                    txt_mob_number.Text = mobile_Column[1];
                    txt_faxcntrycode.Text = fax_Column[0];
                    txt_fax_areacode.Text = fax_Column[1];
                    txt_fax_number.Text = fax_Column[2];
                    txt_email1.Text = dr["Email1"].ToString();
                    txt_email2.Text = dr["Email2"].ToString();


                }
        }
    }
        public void Disable_Controls()
        {
            txt_address1.Enabled = false;
            txt_address2.Enabled = false;
            txt_address3.Enabled = false;
            ddl_country.Enabled = false;
            txt_state.Enabled = false;
            txt_city.Enabled = false;
            txt_zip.Enabled = false;
            txt_numcntycode.Enabled = false;
            txt_areacode.Enabled = false;
            txt_number.Enabled = false;
            txt_mob_number.Enabled = false;
            txt_mobcntrycode.Enabled = false;
            txt_faxcntrycode.Enabled = false;
            txt_fax_areacode.Enabled = false;
            txt_fax_number.Enabled = false;
            txt_email1.Enabled = false;
            txt_email2.Enabled = false;

        }
        public void Enable_Controls()
        {
            txt_address1.Enabled = true;
            txt_address2.Enabled = true;
            txt_address3.Enabled = true;
            ddl_country.Enabled = true;
            txt_state.Enabled = true;
            txt_city.Enabled = true;
            txt_zip.Enabled = true;
            txt_numcntycode.Enabled = true;
            txt_areacode.Enabled = true;
            txt_number.Enabled = true;
            txt_mob_number.Enabled = true;
            txt_mob_number.Enabled = true;
            txt_faxcntrycode.Enabled = true;
            txt_fax_areacode.Enabled = true;
            txt_fax_number.Enabled = true;
            txt_email1.Enabled = true;
            txt_email2.Enabled = true;

        }
        public void Map_Controls_Data()
        {
            txt_address1.Text = txtaddress1.Text;
            txt_address2.Text = txtaddress2.Text;
            txt_address3.Text = txtaddress3.Text;
            ddl_country.SelectedValue = ddlcountry.SelectedValue;
            txt_state.Text = txtstate.Text;
            txt_city.Text = txtcity.Text;
            txt_zip.Text = txtzip.Text;
            txt_numcntycode.Text = txtnumcntycode.Text;
            txt_areacode.Text = txtareacode.Text;
            txt_number.Text = txtnumber.Text;
            txt_mob_number.Text = txtmobno.Text;
            txt_mobcntrycode.Text = txtmobcntrycode.Text;
            txt_faxcntrycode.Text = txtfaxcntrycode.Text;
            txt_fax_areacode.Text = txtfaxareacode.Text;
            txt_fax_number.Text = txtfax.Text;
            txt_email1.Text = txtPersonalEmail.Text;
            txt_email2.Text = txtOffEmail.Text;
        }
        public void Clear_permanent_Controls()
        {
            txt_address1.Text = "";
            txt_address2.Text = "";
            txt_address3.Text = "";
            ddl_country.SelectedIndex = 0;
            txt_state.Text = "";
            txt_city.Text = "";
            txt_zip.Text = "";
            txt_numcntycode.Text = "";
            txt_areacode.Text = "";
            txt_number.Text = "";
            txt_mob_number.Text = "";
            txt_mobcntrycode.Text = "";
            txt_faxcntrycode.Text = "";
            txt_fax_areacode.Text = "";
            txt_fax_number.Text = "";
            txt_email1.Text = "";
            txt_email2.Text = "";
        }
    #endregion

        
    #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //-----------------------------
            SessionManager.SessionCheck_New();
            //-----------------------------
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 256);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");
            }
            //*******************
            auth = new AuthenticationManager(256, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

            Session["CurrentModule"] = 2;
            if (!IsPostBack)
            {
                if (Common.CastAsInt32(Session["ProfileId"]) > 0)
                {
                    lbl_EmpName.Text = Session["ProfileName"].ToString();
                }

                ControlLoader.LoadControl(ddlcountry , DataName.country, "Select", "0");
                ControlLoader.LoadControl(ddl_country, DataName.country, "Select", "0");

                if (Session["ProfileMode"].ToString() == "Edit")
                {
                    ShowRecord();
                    btnsave.Visible = true && auth.IsUpdate; 
                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                Common.Set_Procedures("HR_InsertUpdateContactDetails");
                Common.Set_ParameterLength(31);

                Common.Set_Parameters(new MyParameter("@ContactId", SelectedId),
                                       new MyParameter("@EmpId", Session["ProfileId"]),
                                       new MyParameter("@Address1", txtaddress1.Text.Trim()),
                                       new MyParameter("@Address2", txtaddress2.Text.Trim()),
                                       new MyParameter("@Address3", txtaddress3.Text.Trim()),
                                       new MyParameter("@Country", ddlcountry.SelectedValue.Trim()),
                                       new MyParameter("@State", txtstate.Text.Trim()),
                                       new MyParameter("@City", txtcity.Text.Trim()),
                                       new MyParameter("@ZipCode", txtzip.Text.Trim()),
                                       new MyParameter("@Telephone", txtnumcntycode.Text + "-" + txtareacode.Text + "-" + txtnumber.Text),
                                       new MyParameter("@Mobile", txtmobcntrycode.Text + "-" + txtmobno.Text),
                                       new MyParameter("@Fax", txtfaxcntrycode.Text.Trim() + "-" + txtfaxareacode.Text + "-" + txtfax.Text),
                                       new MyParameter("@Email1", txtPersonalEmail.Text.Trim()),
                                       new MyParameter("@OfficeTelephone", txtOffCntryCode.Text + "-" + txtOffAreaCode.Text + "-" + txtOffNumber.Text),
                                       new MyParameter("@OfficeMobile", txtOffMobCntryCode.Text.Trim() + "-" + txtOffMobNumber.Text),
                                       new MyParameter("@Email2", txtOffEmail.Text.Trim()),
                                       new MyParameter("@ContactType", "C"),

                                       new MyParameter("@pmt_EmpId", Session["ProfileId"]),
                                       new MyParameter("@pmt_Address1", txt_address1.Text.Trim()),
                                       new MyParameter("@pmt_Address2", txt_address2.Text.Trim()),
                                       new MyParameter("@pmt_Address3", txt_address3.Text.Trim()),
                                       new MyParameter("@pmt_Country", ddl_country.SelectedValue.Trim()),
                                       new MyParameter("@pmt_State", txt_state.Text.Trim()),
                                       new MyParameter("@pmt_City", txt_city.Text.Trim()),
                                       new MyParameter("@pmt_ZipCode", txt_zip.Text.Trim()),
                                       new MyParameter("@pmt_Telephone", txt_numcntycode.Text + "-" + txt_areacode.Text + "-" + txt_number.Text),
                                       new MyParameter("@pmt_Mobile", txt_mobcntrycode.Text + "-" + txt_mob_number.Text),
                                       new MyParameter("@pmt_Fax", txt_faxcntrycode.Text.Trim() + "-" + txt_fax_areacode.Text + "-" + txt_fax_number.Text),
                                       new MyParameter("@pmt_Email1", txt_email1.Text.Trim()),
                                       new MyParameter("@pmt_Email2", txt_email2.Text.Trim()),
                                       new MyParameter("@pmt_ContactType", "P"));


                    DataSet ds = new DataSet();
                    if (Common.Execute_Procedures_IUD_CMS(ds))
                    {
                        ViewState["contactId"]  = Common.CastAsInt32(ds.Tables[0].Rows[0]["ContactId"]);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                        SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);   
                        string error;
                        error = Common.ErrMsg;
                        //0--------------------
                    }
            }
            catch (Exception ex) 
            {

            }
        }
        protected void chksameaddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chksameaddress.Checked == true)
            {
                Disable_Controls();
                Map_Controls_Data();
            }
            else
            {
                Enable_Controls();
                //Clear_permanent_Controls();
            }
        }
        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtnumcntycode.Text = "";
            txtmobcntrycode.Text = "";
            txtfaxcntrycode.Text = "";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select countrycode from country where countryid= " + ddlcountry.SelectedValue);
            if (dt != null)
            if (dt.Rows.Count >0)
            {
                txtnumcntycode.Text = "+" + dt.Rows[0][0].ToString();
                txtmobcntrycode.Text = "+" + dt.Rows[0][0].ToString();
                txtfaxcntrycode.Text = "+" + dt.Rows[0][0].ToString();
            }
        }
        protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_numcntycode.Text = "";
            txt_mobcntrycode.Text = "";
            txt_faxcntrycode.Text = "";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select countrycode from country where countryid= " + ddl_country.SelectedValue);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    txt_numcntycode.Text = "+" + dt.Rows[0][0].ToString();
                    txt_mobcntrycode.Text = "+" + dt.Rows[0][0].ToString();
                    txt_faxcntrycode.Text = "+" + dt.Rows[0][0].ToString();
                }
        }
    #endregion

}
