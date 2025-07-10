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

public partial class VesselRecord_VesselSafeManning : System.Web.UI.UserControl
{
    #region Declare Property
    public int Vesselid
    {
        get {return Convert.ToInt32(ViewState["VesselId"].ToString()); }
        set { ViewState["VesselId"] = value; }
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
            Load_Grades();
            showdata();
            btn_Add.Visible = ((Mode == "New") || (Mode == "Edit")) && ((Auth.isAdd) || (Auth.isEdit));
        }
    }
    private void Load_Grades()
    {
        DataSet ds = cls_SearchReliever.getMasterData("ManningGradeMaster", "GradeName", "GradeID");
        dp_Rank.DataSource = ds.Tables[0];
        dp_Rank.DataTextField = "GradeName";
        dp_Rank.DataValueField = "GradeID";
        dp_Rank.DataBind();
        dp_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
     
    }
    private void showdata()
    {

        DataTable dt = Budget.getTable("select VESSELID,VSM.ManningGradeId,(select GRADENAME FROM MANNINGGRADEMASTER MGM WHERE MGM.GRADEID=VSM.MANNINGGRADEID) as GRADENAME,SAFEMANNING, " +
                                     "(Select COUNT(CREWID) from CrewPersonalDetails where crewstatusid=3 and currentvesselid=VSM.VesselId AND CURRENTRANKID IN(SELECT RANKID FROM MANNINGGRADEDETAILS MGD WHERE MGD.GRADEID=VSM.MANNINGGRADEID)) AS ACTUALMANNING, " +
                                     "(SELECT COUNTRYNAME FROM COUNTRY WHERE COUNTRYID IN(Select TOP 1 NATIONALITYID from CrewPersonalDetails where crewstatusid=3 and currentvesselid=VSM.VesselId  AND CURRENTRANKID IN(SELECT RANKID FROM MANNINGGRADEDETAILS MGD WHERE MGD.GRADEID=VSM.MANNINGGRADEID))) AS ACTUALNATIONALITY  " +
                                     "from vesselsafemanning VSM WHERE VSM.VESSELID=" + this.Vesselid.ToString()).Tables[0];
        this.gv_VDoc.DataSource = dt;
        this.gv_VDoc.DataBind();

        lbl_Total_Safe.Text = dt.Compute("Sum(SAFEMANNING)", "").ToString();
        lbl_Total_Actual.Text = dt.Compute("Sum(ACTUALMANNING)", "").ToString();
    }
    public void doselect(object sender, GridViewSelectEventArgs e)
    {
        gv_VDoc.SelectedIndex = e.NewSelectedIndex;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Budget.getTable("SELECT * FROM VESSELSAFEMANNING VBM WHERE VBM.VESSELID=" + this.Vesselid.ToString() + " AND MANNINGGRADEID=" + dp_Rank.SelectedValue).Tables[0];
            if (dt.Rows.Count <= 0)
            {

                Budget.getTable("Insert Into VESSELSAFEMANNING(VesselId,MANNINGGRADEID,SafeManning) values(" + this.Vesselid.ToString() + "," + dp_Rank.SelectedValue + "," + txt_budget.Text + ")");
            }
            else
            {
                Budget.getTable("Update VESSELSAFEMANNING set SafeManning=" + txt_budget.Text + " Where VESSELID=" + this.Vesselid.ToString() + " AND MANNINGGRADEID=" + dp_Rank.SelectedValue);
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
        Budget.getTable("DELETE FROM VESSELSAFEMANNING VBM WHERE VBM.VESSELID=" + this.Vesselid.ToString() + " AND MANNINGGRADEID=" + hfd.Value);
        showdata(); 
    }


    //=====================
    
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        int vesselID = Common.CastAsInt32(btn.CommandArgument);
        HiddenField hfManningGradeId = (HiddenField)btn.Parent.FindControl("hfManningGradeId");
        HiddenField hfSafeManning = (HiddenField)btn.Parent.FindControl("hfSafeManning");

        dp_Rank.SelectedValue = hfManningGradeId.Value;
        txt_budget.Text = hfSafeManning.Value;


    }
}

