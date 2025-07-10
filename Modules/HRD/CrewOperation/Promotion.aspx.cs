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

public partial class CrewOperation_Promotion : System.Web.UI.Page
{
    int EmpNumber;
    int rank;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Promotion"; 
        Label1.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 22);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        lbl_promotion_message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            EmpNumber = 0;
            EmpNumber = Convert.ToInt32(Session["Promotion_CrewID_Planning"].ToString());
        }
        catch
        {
               
        }
        if (!Page.IsPostBack)
        {
            show_record(EmpNumber);
            bindRank(rank);
            if (rank == 1)
            {
                dprank.Items.Clear();
                dprank.Items.Add(new ListItem("MSTR", "1"));
            }                
        }
    }
    protected void txtEmpNumber_TextChanged(object sender, EventArgs e)
    {
        if (txtEmpNumber.Text.Trim() == "")
        {
            btnCheckPromotionCriteria.Enabled = false;
        }
        else
        {
            btnCheckPromotionCriteria.Enabled = true;
        }
        int i;
        int returnvalue;
        Promotion.checkcrewnumber("check_crewnumber", txtEmpNumber.Text.Trim(), out returnvalue);
        if (returnvalue != 0)
        {
            cleardata();
            dprank.Items.Clear();
            lbl_promotion_message.Text = "This Crew Member Does Not Exists.";
            btn_SavePromotion.Enabled = false;
            btnCheckPromotionCriteria.Enabled = false;
        }
        else
        {
            string Status;
            DataTable dtstatus = Promotion.SelectCrewStatusByCrewNumber(txtEmpNumber.Text.Trim());
            Status = dtstatus.Rows[0][0].ToString();
            if (Status != "2" && Status != "3")
            {
                lbl_promotion_message.Text = "This Crew Member is Not On Leave/On Board.";
                lbl_Name.Text = "";
                lblPresentRank.Text = "";
                lbl_Status.Text = "";
                lblVessel.Text = "";
                HiddenVessel.Value = "";
                HiddenVesselType.Value = "";
                lblSignedOff.Text = "";
                lblAvailableDate.Text = "";
                HiddenCurrentRank.Value = "";
                HiddenSignOnDate.Value = "";
                dprank.Items.Clear();
                btn_SavePromotion.Enabled = false;
                return;
            }

            DataTable dt = Promotion.selectCrewMembersByEmpNo(Convert.ToString(txtEmpNumber.Text));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lbl_Name.Text = dr["EmpName"].ToString();
                    lblPresentRank.Text = dr["rankname"].ToString();
                    lbl_Status.Text = dr["Status"].ToString();
                    i = Convert.ToInt32(dr["StatusId"].ToString());

                    if (i == 3)
                    {
                        lblVessel.Text = dr["CurrentVesselId"].ToString();
                        HiddenVessel.Value = dr["Currentvessel_id"].ToString();
                        HiddenVesselType.Value = dr["VesselTypeId_C"].ToString();
                    }
                    if (i == 1 || i == 2)
                    {
                        lblVessel.Text = dr["LastVesselId"].ToString();
                        HiddenVessel.Value = dr["LastVessel_Id"].ToString();
                        HiddenVesselType.Value = dr["VesselTypeId_L"].ToString();
                    }
                    lblSignedOff.Text = dr["SignedOff"].ToString();
                    try
                    {
                        lblAvailableDate.Text = Convert.ToDateTime(dr["AvailableDate"].ToString()).ToString("dd-MMM-yyyy") ;
                    }
                    catch
                    { lblAvailableDate.Text = ""; }
                    rank = Convert.ToInt32(dr["CurrentRankId"].ToString());
                    HiddenCurrentRank.Value = Convert.ToString(rank);
                    HiddenSignOnDate.Value = dr["SignOnDate"].ToString();
                    bindRank(rank);

                    if (rank == 1)
                    {
                        dprank.Items.Clear();
                        dprank.Items.Add(new ListItem("MSTR", "1"));
                        lbl_promotion_message.Text = "Master Can't Promoted.";
                        btnCheckPromotionCriteria.Enabled = false;
                    }
                    else
                    {
                        btnCheckPromotionCriteria.Enabled = true;
                    }
                    btn_SavePromotion.Enabled = false;
                }
            }
        }
    }
    public void bindRank(int rk)
    {
        //DataTable dt2 = Promotion.SelectNextRankDetails(rk);
        //this.dprank.DataValueField = "RankId";
        //this.dprank.DataTextField = "RankCode";
        //this.dprank.DataSource = dt2;
        //this.dprank.DataBind();

        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        dprank.DataSource = obj.ResultSet.Tables[0];
        dprank.DataTextField = "RankName";
        dprank.DataValueField = "RankId";
        dprank.DataBind();
        dprank.Items.RemoveAt(0);  
    }
    protected void show_record(int CrewId)
    {
        int i;
        HiddenPromotion.Value = EmpNumber.ToString();
        DataTable dt1 = Promotion.SelectPromotionDetailsById(Convert.ToInt32(HiddenPromotion.Value));
        foreach (DataRow dr in dt1.Rows)
        {
            txtEmpNumber.Text = dr["CrewNumber"].ToString();
            lbl_Name.Text = dr["EmpName"].ToString();
            lblPresentRank.Text=dr["rankname"].ToString();
            lbl_Status.Text=dr["Status"].ToString();
            i = Convert.ToInt32(dr["StatusId"].ToString());
           
            if(i==3)
            {
                lblVessel.Text=dr["CurrentVesselId"].ToString();
                HiddenVessel.Value = dr["Currentvessel_id"].ToString();
                HiddenVesselType.Value = dr["VesselTypeId_C"].ToString();
            }
            if(i==1 || i==2)
            {
                lblVessel.Text=dr["LastVesselId"].ToString();
                HiddenVessel.Value = dr["LastVessel_Id"].ToString();
                HiddenVesselType.Value = dr["VesselTypeId_L"].ToString();

            }

            try
            {
                lblSignedOff.Text = Convert.ToDateTime(dr["SignedOff"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch
            { lblSignedOff.Text = ""; }

            try
            {
                lblAvailableDate.Text = Convert.ToDateTime(dr["AvailableDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch
            { lblAvailableDate.Text = ""; }
            rank = Convert.ToInt32(dr["CurrentRankId"].ToString());
            HiddenCurrentRank.Value = Convert.ToString(rank);
            HiddenSignOnDate.Value=dr["SignOnDate"].ToString();
        }
    }
    protected void btnCheckPromotionCriteria_Click(object sender, EventArgs e)
    {
        //DataTable dt11 = Promotion.SelectAppraisal(txtEmpNumber.Text.Trim());
        //if (dt11.Rows.Count >= 2)
        //{
        //    btn_SavePromotion.Enabled = true && Auth.isAdd;
        //}
        //else
        //{
        //    lbl_promotion_message.Text = "This Member is not Recommended Twice.";
        //    btn_SavePromotion.Enabled = false;
        //}
        
        btn_SavePromotion.Enabled = true && Auth.isAdd;

        show_record(EmpNumber);
        DataTable dttable = Promotion.Chk_ContractLicense(txtEmpNumber.Text.Trim(), Convert.ToInt32(HiddenCurrentRank.Value), Convert.ToInt32(dprank.SelectedValue));
        foreach (DataRow dr in dttable.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                Label1.Text = "Do not Possess Adequate Licenses.";
                btn_SavePromotion.Enabled = false;
                return;
            }
        }
    }
    protected void btn_SavePromotion_Click(object sender, EventArgs e)
    {
        int companyid = 1;
        int promotionrank;
        int createdby = Convert.ToInt32(Session["loginid"].ToString());
        int modifiedby = 0;
        string crewnumber = txtEmpNumber.Text.Trim();
        ////*********** CODE TO CHECK FOR BRANCHID ***********
        //DataTable dt2 = MiscExpense.selectCrewIdCrewNumberInMiscCost(txtEmpNumber.Text.Trim());
        //foreach (DataRow dr in dt2.Rows)
        //{
        //    string xpc = Alerts.Check_BranchId(Convert.ToInt32(dr["CrewId"].ToString()));
        //    if (xpc.Trim() != "")
        //    {
        //        cleardata();
        //        lbl_promotion_message.Text = xpc;
        //        return;
        //    }
        //}
        ////************
        promotionrank = Convert.ToInt32(dprank.SelectedValue);
        DateTime promotiondate = Convert.ToDateTime(txt_promotiodate.Text);
        int vesseltypeid;
        if (HiddenVesselType.Value.ToString() == "")
        {
            vesseltypeid = 0;
        }
        else
        {
            vesseltypeid = Convert.ToInt32(HiddenVesselType.Value);
        }

        Promotion.InsertPromotionDetails("InsertPromotionDetails",
                                             crewnumber,
                                             companyid,
                                             Convert.ToInt32(HiddenCurrentRank.Value),
                                             promotionrank,
                                             promotiondate,
                                             Convert.ToInt32(HiddenVessel.Value),
                                             vesseltypeid,
                                             createdby,
                                             modifiedby);
        lbl_promotion_message.Text = "Record Successfully Saved.";
        btn_SavePromotion.Enabled = false;  
    }
    private void cleardata()
    {
        lbl_Name.Text = "";
        lblPresentRank.Text = "";
        lbl_Status.Text = "";
        lblVessel.Text = "";
        lblSignedOff.Text = "";
        lblAvailableDate.Text = "";
        txt_promotiodate.Text = "";
        txtEmpNumber.Focus();
        btn_SavePromotion.Enabled = false;
    }
}
