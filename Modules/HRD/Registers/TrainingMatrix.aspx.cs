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
using System.Text;

public partial class TrainingMatrix : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessMain.Text = ""; 
        lblMessVessel.Text = ""; 
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 290);//45
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");
        }

        Auth = new AuthenticationManager(290, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        btnAddTraining.Visible = Auth.IsAdd;
        btnUpdTraining.Visible = Auth.IsUpdate;
        if (!Page.IsPostBack)
        {
            BindTraining();
            BindVessel();
        }
    }
    // ------------ Events
    protected void ddlTraining_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SelectVessel();
    }
    // ------------ Function
    public void SelectVessel()
    {
        string qry = "SELECT * FROM TrainingMatrixForVessel where TrainingMatrixId=" + ddlTraining.SelectedValue;
        DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS(qry);
        for (int i = 0; i <= chklstVessel.Items.Count - 1; i++)
        {
            chklstVessel.Items[i].Selected=(dt.Select("VesselId=" + chklstVessel.Items[i].Value).Length>0);  
        }
    }
    public void BindTraining()
    {
        string sql = "Select TrainingMatrixID,TrainingMatrixName from TrainingMatrixMaster WHERE STATUS='A'";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlTraining.DataSource = dt;
        ddlTraining.DataTextField = "TrainingMatrixName";
        ddlTraining.DataValueField = "TrainingMatrixID";
        ddlTraining.DataBind();
        ddlTraining.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    public void BindVessel()
    {
        string sql = "select VesselID,VesselName+ ' ( ' + isnull((select (select TrainingMatrixName  from  dbo.TrainingMatrixMaster T where T.TrainingMatrixID =TM.TrainingMatrixID )TrainingMatrixID from  dbo.TrainingMatrixForVessel TM where TM.VesselID =V.VesselID ),'--')+ ' )' as VesselName from Vessel V where VesselStatusid<>2  ORDER BY VESSELNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        chklstVessel.DataSource = dt;
        chklstVessel.DataTextField = "VesselName";
        chklstVessel.DataValueField = "VesselID";
        chklstVessel.DataBind();
    }
    protected void btnSaveVessel_Click(object sender, EventArgs e)
    {
        if (ddlTraining.SelectedIndex <= 0)
        {
            lblMessVessel.ForeColor = System.Drawing.Color.Red;
            lblMessVessel.Text = "Please Select a Training Matrix First.";
            ddlTraining.Focus();
            return;
        }

        try
        {
            for (int i = 0; i <= chklstVessel.Items.Count - 1; i++)
            {
                if (chklstVessel.Items[i].Selected)
                {
                    string qry = "DELETE FROM TrainingMatrixForVessel where VESSELID=" + chklstVessel.Items[i].Value;
                    Common.Execute_Procedures_Select_ByQueryCMS(qry);
                    qry = "INSERT INTO TrainingMatrixForVessel VALUES(" + ddlTraining.SelectedValue + "," + chklstVessel.Items[i].Value + ")";
                    Common.Execute_Procedures_Select_ByQueryCMS(qry);
                }
            }

            lblMessVessel.ForeColor = System.Drawing.Color.Green;
            lblMessVessel.Text = "Vessel List Updated Successfully.";
        }
        catch (Exception ex )
        {
            lblMessVessel.ForeColor = System.Drawing.Color.Red;
            lblMessVessel.Text = "Error Updaing Vessel List. Error : " + ex.Message;
        }
    }
    protected void btnEditVessel_Click(object sender, EventArgs e)
    {
        if (ddlTraining.SelectedIndex <= 0)
        {
            lblMessVessel.ForeColor = System.Drawing.Color.Red;
            lblMessMain.Text = "Please Select a Training Matrix First.";
            ddlTraining.Focus();
            return;
        }
        trVlist.Visible = true;
    }
    protected void btnSaveTMat_Click(object sender, EventArgs e)
    {
        string qry = "insert into trainingmatrixmaster select isnull(max(trainingmatrixid),0)+1,'" + txtMatName.Text + "','A' from trainingmatrixmaster;select max(trainingmatrixid) from trainingmatrixmaster";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(qry);
        ddlTraining.SelectedIndex = 0;
        BindTraining();
        ddlTraining.SelectedValue = dt.Rows[0][0].ToString();
        ddlTraining_OnSelectedIndexChanged(sender, e);
        txtMatName.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trVlist.Visible = false;
    }
    
}
