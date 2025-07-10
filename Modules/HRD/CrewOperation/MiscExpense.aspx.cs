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

public partial class CrewAccounting_MiscExpense : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Label1.Text = "";
        Session["PageName"] = " - Misc P.O."; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 25);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindVendorDropDown();
            bindVesselNameddl();
            Bindgrid();
            ddl_acccode1.Items.Insert(0, new ListItem("< Select >", "0"));
            ddl_acccode2.Items.Insert(0, new ListItem("< Select >", "0"));
            ddl_acccode3.Items.Insert(0, new ListItem("< Select >", "0"));
            ddl_acccode4.Items.Insert(0, new ListItem("< Select >", "0"));
            ddl_acccode5.Items.Insert(0, new ListItem("< Select >", "0"));
            Handle_Authority();
        } 
    }
    private void Handle_Authority()
    {
        gvsecond.Columns[1].Visible = Auth.isEdit;
        gvsecond.Columns[2].Visible = Auth.isDelete;
    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceSearchScreen.selectDataVendor();
        this.ddl_MISC_Vendor.DataValueField = "VendorId";
        this.ddl_MISC_Vendor.DataTextField = "VendorName";
        this.ddl_MISC_Vendor.DataSource = dt;
        this.ddl_MISC_Vendor.DataBind();
    }
    protected void bindVesselNameddl()
    {
        
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddvessel.DataSource = ds.Tables[0];
        ddvessel.DataValueField = "VesselId";
        ddvessel.DataTextField = "Name";
        ddvessel.DataBind();
        ddvessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bindacccodeddl(int budgetyear)
    {
       
        DataSet ds = MiscExpense.selectaccounthead(Convert.ToInt16(this.ddvessel.SelectedValue), budgetyear);

        if (ds.Tables.Count > 0)
        {
          
                ddl_acccode1.DataSource = ds.Tables[0];
                ddl_acccode1.DataTextField = "accountheadname";
                ddl_acccode1.DataValueField = "AccountHeadId";
                ddl_acccode1.DataBind();
                ddl_acccode1.Items.Insert(0, new ListItem("< Select >", "0"));

                ddl_acccode2.DataSource = ds.Tables[0];
                ddl_acccode2.DataTextField = "accountheadname";
                ddl_acccode2.DataValueField = "AccountHeadId";
                ddl_acccode2.DataBind();
                ddl_acccode2.Items.Insert(0, new ListItem("< Select >", "0"));

                ddl_acccode3.DataSource = ds.Tables[0];
                ddl_acccode3.DataTextField = "accountheadname";
                ddl_acccode3.DataValueField = "AccountHeadId";
                ddl_acccode3.DataBind();
                ddl_acccode3.Items.Insert(0, new ListItem("< Select >", "0"));

                ddl_acccode4.DataSource = ds.Tables[0];
                ddl_acccode4.DataTextField = "accountheadname";
                ddl_acccode4.DataValueField = "AccountHeadId";
                ddl_acccode4.DataBind();
                ddl_acccode4.Items.Insert(0, new ListItem("< Select >", "0"));

                ddl_acccode5.DataSource = ds.Tables[0];
                ddl_acccode5.DataTextField = "accountheadname";
                ddl_acccode5.DataValueField = "AccountHeadId";
                ddl_acccode5.DataBind();
                ddl_acccode5.Items.Insert(0, new ListItem("< Select >", "0"));
            }
            else
            {
                ddl_acccode1.Items.Clear();
                ddl_acccode2.Items.Clear();
                ddl_acccode3.Items.Clear();
                ddl_acccode4.Items.Clear();
                ddl_acccode5.Items.Clear();
                ddl_acccode1.Items.Insert(0, new ListItem("< Select >", "0"));
                ddl_acccode2.Items.Insert(0, new ListItem("< Select >", "0"));
                ddl_acccode3.Items.Insert(0, new ListItem("< Select >", "0"));
                ddl_acccode4.Items.Insert(0, new ListItem("< Select >", "0"));
                ddl_acccode5.Items.Insert(0, new ListItem("< Select >", "0"));



            }
        }
    
    private void Bindgrid()
    {
        DataTable dt1 = MiscExpense.selectDataMiscCostDetails();
        this.gvsecond.DataSource = dt1;
        this.gvsecond.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
         DataTable dt2 = MiscExpense.selectCrewIdCrewNumberInMiscCost(txt_empno.Text.Trim());
         //if (dt2.Rows.Count == 0)
         //{
         //    Label1.Visible = true;
         //}
         //else
         //{
             //Label1.Visible = false;
             int MiscCostId = -1;
             int createdby = 0, modifiedby = 0;
             int vesselId = Convert.ToInt32(ddvessel.SelectedValue);
             int vendorid = Convert.ToInt32(ddl_MISC_Vendor.SelectedValue);
             string empno = txt_empno.Text;
             int ac_headid1 = Convert.ToInt32(ddl_acccode1.SelectedValue);
             int ac_headid2 = Convert.ToInt32(ddl_acccode2.SelectedValue);
             int ac_headid3 = Convert.ToInt32(ddl_acccode3.SelectedValue);
             int ac_headid4 = Convert.ToInt32(ddl_acccode4.SelectedValue);
             int ac_headid5 = Convert.ToInt32(ddl_acccode5.SelectedValue);
             string desc1 = txt_desc1.Text;
             string desc2 = txt_desc2.Text;
             string desc3 = txt_desc3.Text;
             string desc4 = txt_desc4.Text;
             string desc5 = txt_desc5.Text;
             double tot_amt1 = Convert.ToDouble(txttotamt1.Text);
             double tot_amt2, tot_amt3, tot_amt4, tot_amt5;
             if (txttotamt2.Text != "")
             {
                tot_amt2 = Convert.ToDouble(txttotamt2.Text);
             }
             else
             {
                 tot_amt2 = 0.00;
             }
             if (txttotamt3.Text != "")
             {
                 tot_amt3 = Convert.ToDouble(txttotamt3.Text);
             }
             else
             {
                 tot_amt3 = 0.00;
             }
             if (txttotamt4.Text != "")
             {
                 tot_amt4 = Convert.ToDouble(txttotamt4.Text);
             }
             else
             {
                 tot_amt4 = 0.00;
             }
             if (txttotamt5.Text != "")
             {
                 tot_amt5 = Convert.ToDouble(txttotamt5.Text);
             }
             else
             {
                 tot_amt5 = 0.00;
             }
             double d;
             d = tot_amt1 + tot_amt2 + tot_amt3 + tot_amt4 + tot_amt5;
             lbl_totamt.Text = d.ToString();
             if (HiddenMiscCostpk.Value.Trim() == "")
             {
                 createdby = Convert.ToInt32(Session["loginid"].ToString());
             }
             else
             {
                 MiscCostId = Convert.ToInt32(HiddenMiscCostpk.Value);
                 modifiedby = Convert.ToInt32(Session["loginid"].ToString());
             }
             int refno;
             MiscExpense.insertUpdateMiscCostDetails("InsertUpdateMiscCostDetails",
                                                         out refno,
                                                         MiscCostId,
                                                         vesselId,
                                                         empno,
                                                         ac_headid1,
                                                         desc1,
                                                         tot_amt1,
                                                         ac_headid2,
                                                         desc2,
                                                         tot_amt2,
                                                         ac_headid3,
                                                         desc3,
                                                         tot_amt3,
                                                         ac_headid4,
                                                         desc4,
                                                         tot_amt4,
                                                         ac_headid5,
                                                         desc5,
                                                         tot_amt5,
                                                         d,
                                                         createdby,
                                                         modifiedby,
                                                         vendorid);
             lbl_ref.Text = refno.ToString();
             Bindgrid();
         //}
         clearTextboxes();
         txt_empno.ReadOnly = false;
         ddvessel.Enabled = true;
         //Label1.Visible = true;
         Label1.Text = "Record Saved Successfully";
    }
    protected void clearTextboxes()
    {
        HiddenMiscCostpk.Value = "";
        lbl_ref.Text = "";
        ddvessel.SelectedIndex = 0;
        txt_empno.Text = "";
        ddl_acccode1.SelectedIndex = 0;
        ddl_acccode2.SelectedIndex = 0;
        ddl_acccode3.SelectedIndex = 0;
        ddl_acccode4.SelectedIndex = 0;
        ddl_acccode5.SelectedIndex = 0;
        ddl_MISC_Vendor.SelectedIndex = 0;
        txt_desc1.Text = "";
        txt_desc2.Text = "";
        txt_desc3.Text = "";
        txt_desc4.Text = "";
        txt_desc5.Text = "";
        txttotamt1.Text = "";
        txttotamt2.Text = "";
        txttotamt3.Text = "";
        txttotamt4.Text = "";
        txttotamt5.Text = "";
        lbl_totamt.Text = "";
        gvsecond.SelectedIndex = -1;
    }
    protected void gvsecond_SelectIndexChanged(object sender, EventArgs e)
    {
        int id;
        HiddenField hfdcost;
        hfdcost = (HiddenField)gvsecond.Rows[gvsecond.SelectedIndex].FindControl("HiddenMiscCostId");
        id = Convert.ToInt32(hfdcost.Value.ToString());
        Show_Record_Cost(id);
        btn_Save.Visible = false;
        btn_Print_Po.Visible = true && Auth.isPrint;
    }
    protected void Show_Record_Cost(int MiscCostid)
    {
        string Mess = "";
        HiddenMiscCostpk.Value = MiscCostid.ToString();
        DataTable dt3 = MiscExpense.selectDataMiscCostDetailsByMiscCostId(MiscCostid);
        foreach (DataRow dr in dt3.Rows)
        {
            lbl_ref.Text = dr["MiscCostRefNo"].ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddvessel, dr["VesselId"].ToString(), "Vessel");
            //========
            HiddenFieldVesselIdMISC.Value = dr["VesselId"].ToString();
            //========
            Mess = Mess + Alerts.Set_DDL_Value(ddl_MISC_Vendor, dr["VendorId"].ToString(), "Vendor");
            //========
            HiddenFieldVendorIdMISC.Value = dr["VendorId"].ToString();
            //========
            int budgetyear;
            budgetyear = Convert.ToDateTime(dr["CreatedOn"].ToString()).Year;
            bindacccodeddl(budgetyear); 
            txt_empno.Text = dr["CrewNumber"].ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddl_acccode1, dr["AccountHeadId1"].ToString(), "AccountHead1");
            txt_desc1.Text = dr["Desc1"].ToString();
            double dbl = Convert.ToDouble(dr["Amount1"].ToString());
            txttotamt1.Text = Convert.ToString(Math.Round(dbl, 2));
            Mess = Mess + Alerts.Set_DDL_Value(ddl_acccode2, dr["AccountHeadId2"].ToString(), "AccountHead2");
            txt_desc2.Text = dr["Desc2"].ToString();
            double db2 = Convert.ToDouble(dr["Amount2"].ToString());
            txttotamt2.Text = Convert.ToString(Math.Round(db2, 2));
            Mess = Mess + Alerts.Set_DDL_Value(ddl_acccode3, dr["AccountHeadId3"].ToString(), "AccountHead3");
            txt_desc3.Text = dr["Desc3"].ToString();
            double db3 = Convert.ToDouble(dr["Amount3"].ToString());
            txttotamt3.Text = Convert.ToString(Math.Round(db3, 2));
            Mess = Mess + Alerts.Set_DDL_Value(ddl_acccode4, dr["AccountHeadId4"].ToString(), "AccountHead4");
            txt_desc4.Text = dr["Desc4"].ToString();
            double db4 = Convert.ToDouble(dr["Amount4"].ToString());
            txttotamt4.Text = Convert.ToString(Math.Round(db4, 2));
            Mess = Mess + Alerts.Set_DDL_Value(ddl_acccode5, dr["AccountHeadId5"].ToString(), "AccountHead5");
            txt_desc5.Text = dr["Desc5"].ToString();
            double db5 = Convert.ToDouble(dr["Amount5"].ToString());
            txttotamt5.Text = Convert.ToString(Math.Round(db5, 2));
            lbl_totamt.Text = dr["Total"].ToString();
        }
        txt_empno.ReadOnly = true;
        ddvessel.Enabled = false;
        ddl_MISC_Vendor.Enabled = false;
        //-------------------------
        if (Mess.Length > 0)
        {
           
        }
    }
    protected void gvsecond_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        int id;
        HiddenField hfdcost;
        hfdcost = (HiddenField)gvsecond.Rows[e.NewEditIndex].FindControl("HiddenMiscCostId");
        id = Convert.ToInt32(hfdcost.Value.ToString());
        Show_Record_Cost(id);
        gvsecond.SelectedIndex = e.NewEditIndex;
        btn_Save.Visible = true && Auth.isAdd;
    }
    protected void gvsecond_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int id;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)gvsecond.Rows[e.RowIndex].FindControl("HiddenMiscCostId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        MiscExpense.deleteMiscCostDetails("deleteMiscCost", id, intModifiedBy);
        Bindgrid();
        clearTextboxes();
        bindacccodeddl(System.DateTime.Today.Year);
        
    }
    protected void gvsecond_PreRender(object sender, EventArgs e)
    {
        if (this.gvsecond.Rows.Count <= 0)
        {
            lblCost.Text = "No Records Found..!";
        }
        else
        {
            lblCost.Text = "";
        }
    }
    protected void ddvessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindacccodeddl(System.DateTime.Today.Date.Year); 
    }
    protected void btn_Print_Po_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Add("MiscId_PrintPO", Convert.ToInt32(gvsecond.DataKeys[gvsecond.SelectedIndex].Value.ToString()));
            Session.Add("VesselId_Print",Convert.ToInt32(HiddenFieldVesselIdMISC.Value));
            //========
            Session.Add("VendorId_Print",Convert.ToInt32(HiddenFieldVendorIdMISC.Value));
            Session.Add("PoDate_Print",Convert.ToDateTime(System.DateTime.Now.ToString()));
            //========
            Response.Redirect("../Reporting/PrintMiscReport.aspx");
        }
        catch
        {

        }
    }
}
