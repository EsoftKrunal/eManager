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

public partial class CrewOperation_CrewInactiveDetails : System.Web.UI.Page
{
    int crewid;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = "Active/InActive"; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 151);
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
            BinddropdownReason();
            Label1.Visible = false;
            btn_save.Enabled = true && Auth.isAdd;
        }
    }
    #region PageLoaderControl
    private void BinddropdownReason()
    {
        DataSet ds = cls_SearchReliever.getMasterData("NTBRReason", "NTBRReasonId", "NTBRReasonName");
        dd_NTBRReason.DataSource = ds.Tables[0];
        dd_NTBRReason.DataTextField = "NTBRReasonName";
        dd_NTBRReason.DataValueField = "NTBRReasonId";
        dd_NTBRReason.DataBind();
        dd_NTBRReason.Items.RemoveAt(dd_NTBRReason.Items.Count - 2);
        dd_NTBRReason.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    #endregion
    protected void txt_empno_TextChanged(object sender, EventArgs e)
    {
        txt_NTBRDate.Text = "";
        dd_NTBRReason.SelectedIndex = 0;
        ddntbr.SelectedIndex = 0;
        txt_Remarks.Text = "";
        txt_empno.Focus();

        DataTable dt = CrewNTBRDetails.selectNTBRDetailsByEmpNo(txt_empno.Text);
        if (dt.Rows.Count > 0)
        {
            Label1.Visible = true;
            Label1.Text = "";
            btn_save.Enabled = true && Auth.isAdd;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["CrewStatusId"].ToString()) == 3)
                {
                    Label1.Visible = true;
                    Label1.Text = "Crew Member is On Board.";
                    btn_save.Enabled = false;
                    return;
                }
                Session["CrewID_Planning"] = dr["Crewid"].ToString();
                txt_Name.Text = dr["CrewName"].ToString();
                txt_Nationality.Text = dr["nationality"].ToString();
                txt_PresentRank.Text = dr["rankname"].ToString();
                txt_LastVessel.Text = dr["VesselName"].ToString();
                try
                {
                    txt_SignedOff.Text = Convert.ToDateTime(dr["SignOffDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txt_SignedOff.Text = ""; }
                try
                {
                    txt_AvailableDate.Text = Convert.ToDateTime(dr["AvailableFrom"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txt_AvailableDate.Text = ""; }

                int sid=int.Parse(dr["CrewStatusId"].ToString());
                lblLastStatus.Text = (sid==1)?"New":(sid==2)?"On Leave":(sid==3)?"On Board":(sid==4)?"In Active":"NTBR";
                DataTable dt1 = Budget.getTable("Select Top 1 * From CrewInActiveDetails Where CrewId In (select crewid from crewpersonaldetails cpd where cpd.crewnumber='" + txt_empno.Text + "') Order By InActiveid Desc").Tables[0];   
                if (dt1==null || dt1.Rows.Count  <=0 )
                {
                    txt_NTBRDate.Text = "";
                    dd_NTBRReason.SelectedIndex =0 ;
                }
                else
                {
                    
                    txt_NTBRDate.Text = Convert.ToDateTime(dt1.Rows[0]["InActiveDate"].ToString()).ToString("dd-MMM-yyyy");
                    dd_NTBRReason.SelectedValue = dt1.Rows[0]["InActiveReasonid"].ToString();
                }
                lblLastReason.Text = (dd_NTBRReason.SelectedIndex==0)?"":dd_NTBRReason.SelectedItem.Text;      
                if (sid==4)
                {
                    ddntbr.SelectedValue = "2";
                }
                else
                {
                    ddntbr.SelectedValue = "4";  
                }
                ddntbr.Enabled = false; 
                dd_NTBRReason.SelectedIndex = 0; 
                ddntbr_SelectedIndexChanged(sender, e);  
            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Crew Member does not exist";
            callclear();
            btn_save.Enabled = false;
        }
    }
    protected void ddntbr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddntbr.SelectedValue) == 1)
        {
            dd_NTBRReason.Enabled = true;
            RangeValidator2.Enabled = true;
        }
        else if (Convert.ToInt32(ddntbr.SelectedValue) == 2)
        {
            dd_NTBRReason.SelectedValue = Convert.ToString(0);
            dd_NTBRReason.Enabled = false;
            RangeValidator2.Enabled = false;
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int crewId=0;
        try
        {
            crewId = Convert.ToInt32(Session["CrewID_Planning"].ToString());
        }
        catch
        {
        
        }
        string Ntbrdate = txt_NTBRDate.Text;
        if (DateTime.Parse(Ntbrdate) > DateTime.Today)
        {
            Label1.Visible = true;
            Label1.Text = "Inactive date must be less or equal today.";
            return; 
        }
        int Ntbrreasonid = Convert.ToInt32(dd_NTBRReason.SelectedValue);
        int login_id=Convert.ToInt32(Session["loginid"].ToString());
        string remarks = txt_Remarks.Text;
        try
        {
            Budget.getTable("exec InsertUpdateCrewInactiveDetails " + crewId.ToString() + ",'" + Ntbrdate + "'," + ddntbr.SelectedValue + "," + Ntbrreasonid + ",'" + remarks + "'," + login_id);
            Label1.Visible = true;
            Label1.Text = "Record Saved Successfully.";
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Record Not Saved.";
        }
    }
    private void callclear()
    {
        txt_empno.Text = "";
        txt_Name.Text = "";
        txt_Nationality.Text = "";
        txt_PresentRank.Text = "";
        txt_LastVessel.Text = "";
        txt_SignedOff.Text = "";
        txt_AvailableDate.Text = "";

        txt_NTBRDate.Text = "";
        dd_NTBRReason.SelectedIndex =0;
        ddntbr.SelectedIndex = 0;
        txt_Remarks.Text = "";
        txt_empno.Focus();

        btn_save.Enabled = false;
    }
}
