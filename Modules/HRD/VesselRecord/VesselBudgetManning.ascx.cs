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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class VesselRecord_VesselBudgetManning : System.Web.UI.UserControl
{
    #region Declare Property
    private int _vesselid;
    public int Vesselid
    {
        get { return Convert.ToInt32(ViewState["VesselId"].ToString()); }
        set { ViewState["VesselId"] = value; }
    }
    public int BudgetYear
    {
        get { return Convert.ToInt32(ViewState["BudgetYear"].ToString()); }
        set { ViewState["BudgetYear"] = value; }
    }

    #endregion
    Authority Auth;
    string Mode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        

        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        Obj.Invoke();
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        Session["Authority"] = Obj.Authority;
        Auth =(Authority) Session["Authority"];

        MiningScale vsm = new MiningScale();
        if (!Page.IsPostBack)
        {
            Load_Rank();
            Load_Nationality();
            showdata();
            btn_Add.Visible = ((Mode == "New") || (Mode == "Edit")) && ((Auth.isAdd) || (Auth.isEdit));
        }
    }
    private void Load_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankCode", "RankID");
        dp_Rank.DataSource = ds.Tables[0];
        dp_Rank.DataTextField = "RankCode";
        dp_Rank.DataValueField = "RankID";
        dp_Rank.DataBind();
        dp_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_Nationality()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
        dp_nationlty.DataSource = ds.Tables[0];
        dp_nationlty.DataTextField = "CountryName";
        dp_nationlty.DataValueField = "CountryId";
        dp_nationlty.DataBind();
        dp_nationlty.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
     
    }
    public void showdata()
    {

        DataTable dt = Budget.getTable("SELECT VESSELID,VBM.RANKID,(SELECT RANKCODE FROM RANK WHERE RANK.RANKID=VBM.RANKID) AS RANKNAME,BYear,Wages,BUDGETNATIONALITY as BUDGETNATIONALITYID, " +
                                                    "BUDGETMANNING,(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID=BUDGETNATIONALITY) as BUDGETNATIONALITY, " +
                                                    "(Select COUNT(CREWID) from CrewPersonalDetails where crewstatusid=3 and currentvesselid=VBM.VesselId AND VBM.RANKID=CURRENTRANKID) AS ACTUALMANNING, " +
                                                    "(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID IN(Select TOP 1 NATIONALITYID from CrewPersonalDetails where crewstatusid=3 and currentvesselid=VBM.VesselId AND VBM.RANKID=CURRENTRANKID)) AS ACTUALNATIONALITY " +
                                                    "FROM VESSELBUDGETMANNING VBM WHERE VBM.VESSELID=" + this.Vesselid.ToString() + " and BYear="+BudgetYear).Tables[0];
        this.gv_VDoc.DataSource = dt;
        this.gv_VDoc.DataBind();

        lbl_Total_budget.Text = dt.Compute("Sum(BUDGETMANNING)", "").ToString();
        
    }
    public void doselect(object sender, GridViewSelectEventArgs e)
    {
        gv_VDoc.SelectedIndex = e.NewSelectedIndex;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Budget.getTable("SELECT * FROM VESSELBUDGETMANNING VBM WHERE VBM.VESSELID=" + this.Vesselid.ToString() + " AND RANKID=" + dp_Rank.SelectedValue +" and Byear="+BudgetYear).Tables[0];
            if (dt.Rows.Count <= 0)
            {

                Budget.getTable("Insert Into VesselBudgetManning(VesselId,RankId,BudgetManning,Budgetnationality,BYear,Wages) values(" + this.Vesselid.ToString() + "," + dp_Rank.SelectedValue + "," + txt_budget.Text + "," + dp_nationlty.SelectedValue + "," + BudgetYear + "," + txtWeages.Text + ")");
            }
            else
            {
                Budget.getTable("Update VesselBudgetManning set BudgetManning=" + txt_budget.Text + ",Budgetnationality=" + dp_nationlty.SelectedValue + " ,BYear=" + BudgetYear + " ,Wages=" +Common.CastAsDecimal( txtWeages.Text )+ " Where VESSELID=" + this.Vesselid.ToString() + " AND RANKID=" + dp_Rank.SelectedValue);
            }
            showdata();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "re", "refreshParent();", true);
        }
        catch
        {
            lbl_message_manning.Text = "Record Can't Saved.";
        }
   
    }
    protected void Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[e.RowIndex].FindControl("hfd_VesselManningScaleId");
        Budget.getTable("DELETE FROM VESSELBUDGETMANNING WHERE VESSELID=" + this.Vesselid.ToString() + " AND RANKID=" + hfd.Value);
        showdata(); 
    }

    //--------------------------
    
    protected void btnEdit_OnClick(object sender,EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int BUDGETNATIONALITYID = Common.CastAsInt32(btn.CommandArgument);
        HiddenField hfdBUDGETMANNING = (HiddenField)btn.Parent.FindControl("hfdBUDGETMANNING");
        HiddenField hfdWages = (HiddenField)btn.Parent.FindControl("hfdWages");
        HiddenField hfd_VesselManningScaleId = (HiddenField)btn.Parent.FindControl("hfd_VesselManningScaleId");

        dp_Rank.SelectedValue = hfd_VesselManningScaleId.Value;
        txt_budget.Text = hfdBUDGETMANNING.Value;
        dp_nationlty.SelectedValue = BUDGETNATIONALITYID.ToString();
        txtWeages.Text = hfdWages.Value;

    }
    
}

