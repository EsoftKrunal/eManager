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

public partial class CTM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 34);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //*******************
        if (!Page.IsPostBack)
        {
            bindVesselNameddl();
            lbl_ctm_Message.Visible = false;
        }
    }
    protected void showPrevBalance(int VesselId)
    {
        DataTable dt1 = CashToMaster.selectPrevBalanceByVesselId(VesselId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                double dbl = Convert.ToDouble(dr["PreviousBalance"].ToString());
                txt_previosbalance.Text = Convert.ToString(Math.Round(dbl, 2));
            }
        }
        else
        {
            txt_previosbalance.Text = "0";
        }
    }
    protected void bindVesselNameddl()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselCode", Convert.ToInt32(Session["loginid"].ToString()));
        dp_vessel.DataSource = ds.Tables[0];
        dp_vessel.DataValueField = "VesselId";
        dp_vessel.DataTextField = "VesselCode";
        dp_vessel.DataBind();
        dp_vessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void bindMasterddl()
    {
        DataTable dt = CashToMaster.selectMastersAccordingToVessel(Convert.ToInt32(this.dp_vessel.SelectedValue));
        this.dpname.DataTextField = "mastername";
        this.dpname.DataValueField = "crewid";
        this.dpname.DataSource = dt;
        this.dpname.DataBind();
    }
    protected void dp_vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMasterddl();
        showPrevBalance(Convert.ToInt32(this.dp_vessel.SelectedValue));
    }
    protected void btnSaveCTM_Click(object sender, EventArgs e)
    {
        double d;
        double handle_charge;
        if (txt_previosbalance.Text != "")
        {
            d = Convert.ToDouble(txt_amtrcd.Text) + Convert.ToDouble(txt_previosbalance.Text);
        }
        else
        {
            d = Convert.ToDouble(txt_amtrcd.Text);
        }
        txt_total.Text = d.ToString();
        double Prevbal = d;
        int vesselid = Convert.ToInt32(dp_vessel.SelectedValue);
        int CrewId = Convert.ToInt32(dpname.SelectedValue);
        double req_amt = Convert.ToDouble(txtrequestamount.Text);
        string req_date = txtreqDt.Text;
        double amt_paid = Convert.ToDouble(txt_Amtpaid.Text);
        double amt_rec = Convert.ToDouble(txt_amtrcd.Text);
        if (txt_handlingcharge.Text.Trim() == "")
        {
            handle_charge = 0;
        }
        else
        {
            handle_charge = Convert.ToDouble(txt_handlingcharge.Text);
        }
        int createdby = Convert.ToInt32(Session["loginid"].ToString());
        string remarks = txt_remarks.Text;
        CashToMaster.insertUpdateCTMDetails("InsertUpdateCTMDetails",
                                            vesselid,
                                            Prevbal,
                                            CrewId,
                                            req_date,
                                            req_amt,
                                            amt_paid,
                                            amt_rec,
                                            handle_charge,
                                            remarks,
                                            createdby);
        lbl_ctm_Message.Visible = true;
    }
}
