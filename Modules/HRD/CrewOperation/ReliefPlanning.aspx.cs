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

public partial class CrewOperation_ReliefPlanning : System.Web.UI.Page
{
    Authority Auth;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        AuthenticationManager Auth = new AuthenticationManager(152,Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if(!Auth.IsView)
        {
         //   Response.Redirect("~/AuthorityError.aspx",true);
        }
        if (!Page.IsPostBack)
        {
            #region --------------- SignOff ----------------
            this.txt_to.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            BindVessel();
            BindRank();
            #endregion
        }
    }

    #region PageLoaderControl-SignOff
    protected void BindVessel()
    {
        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.chkvessel.DataValueField = "VesselId";
        this.chkvessel.DataTextField = "Name";
        this.chkvessel.DataSource = dt8;
        this.chkvessel.DataBind();
    }
    protected void BindRank()
    {
        this.chkrank.DataTextField = "RankCode";
        this.chkrank.DataValueField = "RankId";
        this.chkrank.DataSource = SearchSignOff.getMasterData("Rank", "RankId", "RankCode");
        this.chkrank.DataBind();
    }
    #endregion

    

    #region --------------- SignOff ----------------
    protected void BindSignOffGrid()
    {
        string vessels, ranks;
        vessels = "";
        ranks = "";
        for (int i = 0; i < this.chkvessel.Items.Count; i++)
        {
            if (chkvessel.Items[i].Selected)
            {
                if (vessels == "")
                {
                    vessels = chkvessel.Items[i].Value;
                }
                else
                {
                    vessels = vessels + ',' + chkvessel.Items[i].Value;
                }
            }
        }
        for (int i = 0; i < this.chkrank.Items.Count; i++)
        {
            if (chkrank.Items[i].Selected)
            {
                if (ranks == "")
                {
                    ranks = chkrank.Items[i].Value;
                }
                else
                {
                    ranks = ranks + ',' + chkrank.Items[i].Value;
                }
            }
        }
        if (vessels.Trim() == "" || ranks.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select rank and vessel to search.');", true);
            return;
        }
        else
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.SelectCrewSignOff_ReliefPlanning '" + vessels + "' , '" + ranks + "','" + txt_from.Text + "','" + txt_to.Text + "'");
            rpt_SignOffList.DataSource = dt;
            rpt_SignOffList.DataBind();
        }
    }
    protected void btnSearchSignOff_Click(object sender, EventArgs e)
    {
        BindSignOffGrid();
    }
    protected void imgReliver_Remove_Click(object sender, EventArgs e)
    {
        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                ImageButton btn=((ImageButton)sender);
                int CrewId = Convert.ToInt32(btn.CommandArgument);
                int RelieverId = Convert.ToInt32(btn.Attributes["RelieverId"]);
                //****************** Code to Check Deletion of Crew Member
                DataTable dtck = SearchSignOff.DeleteCrewfromPlanning(RelieverId);
                foreach (DataRow drd in dtck.Rows)
                {
                    if (Convert.ToInt32(drd[0].ToString()) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Crew Member Exists in an Open Port Call.');", true);
                        return;
                    }
                }
                //******************
                SearchSignOff.UpdReliver_Tempplanning(CrewId, RelieverId, 0, 1);
                BindSignOffGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this,this.GetType(),"a","alert('ReadOnly Users Are Not Authorized to Delete.');",true);
            }
        }
    }
    protected void imgReliver_Plan_Click(object sender, EventArgs e)
    {
        ImageButton btn=((ImageButton)sender);
        int CrewId = Convert.ToInt32(btn.CommandArgument);
        frmSignOn.Attributes.Add("src", "ReliefPlanning_SignOn.aspx?CrewId=" + CrewId);
        dv_SignOn.Visible = true;
    }
    protected void btn_Close_Search_Click(object sender, EventArgs e)
    {
        BindSignOffGrid();
        dv_SignOn.Visible = false;
    }
    #endregion

    

}