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

public partial class CrewOperation_SearchSignOff : System.Web.UI.UserControl
{
    Authority Auth;
    private string _vessel, _rank;
    private int _vesselid, _rankid;
    private int _selectedindex;
    private int _crewid;
    string st;
    int temp;

    public Label Error_Label
    {
        get { return lb_msg; }
    }
    public string Vessel
    {
        get { return _vessel; }
        set { _vessel = value; }
    }
    public string Rank
    {
        get { return _rank; }
        set { _rank = value; }
    }
    public int VesselId
    {
        get { return _vesselid; }
        set { _vesselid = value; }
    }
    public int RankId
    {
        get { return _rankid; }
        set { _rankid = value; }
    }
    public int SelectedIndex
    {
        get { return _selectedindex; }
        set { _selectedindex = value; }
    }
    public int crewid
    {
        get { return _crewid; }
        set { _crewid = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        temp = 0;
        this.txt_from.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + Button2.UniqueID + "').focus();}");
        this.txt_to.Attributes.Add("onKeyPress", "javascript:if(event.keyCode==13){document.getElementById('" + Button2.UniqueID + "').focus();}");
        lb_msg.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            this.txt_to.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.chkvessel.DataValueField = "VesselId";
            this.chkvessel.DataTextField = "Name";
            this.chkvessel.DataSource = dt8;
            this.chkvessel.DataBind();
            
            this.chkrank.DataTextField = "RankCode";
            this.chkrank.DataValueField = "RankId";
            this.chkrank.DataSource = SearchSignOff.getMasterData("Rank", "RankId", "RankCode");
            this.chkrank.DataBind();
        }
        if (gvsearch.SelectedIndex >= 0)
        {
            SelectedIndex = Convert.ToInt32(gvsearch.SelectedIndex.ToString());
            VesselId = Convert.ToInt32(((Label)gvsearch.Rows[gvsearch.SelectedIndex].FindControl("lblvesselid")).Text);
            RankId = Convert.ToInt32(((Label)gvsearch.Rows[gvsearch.SelectedIndex].FindControl("lblrankid")).Text);
            crewid = Convert.ToInt32(((Label)gvsearch.Rows[gvsearch.SelectedIndex].FindControl("Lb_CrewID")).Text);
        }
        else
        {
            SelectedIndex = -1;
        }
    }

    protected void Chk_vessel_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem ls3 in this.chkvessel.Items)
        {
            ls3.Selected = Chk_vessel.Checked;
        }
    }
    protected void Chk_all_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem ls2 in this.chkrank.Items)
        {
            ls2.Selected = Chk_all.Checked;
        }
    }
    public void Show_Grid()
    {
        Button2_Click(new Object(), new EventArgs());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (chkvessel.SelectedIndex == -1 || chkrank.SelectedIndex == -1)
        {
            lb_msg.Text = "Please Select Vessel & Rank !";
            gvsearch.DataSource = null;
            gvsearch.DataBind();
            gvsearch.SelectedIndex = -1;       
            return;
        }

        string vess, rankk;
        vess = "";
        rankk = "";
        for (int i = 0;i< this.chkvessel.Items.Count; i++)
        {
            if (chkvessel.Items[i].Selected)
            {
                if (vess == "")
                {
                    vess = chkvessel.Items[i].Value;
                }
                else
                {
                    vess = vess + ',' + chkvessel.Items[i].Value;
                }
            }
        }
        for (int i = 0;i<this.chkrank.Items.Count; i++)
        { 
            if(chkrank.Items[i].Selected)
            {
                if (rankk == "")
                {
                    rankk=chkrank.Items[i].Value;
                }
                else
                {
                    rankk=rankk+','+ chkrank.Items[i].Value;
                }
            }
        }

        Rank=rankk;
        Vessel=vess;
        binddata(gvsearch.Attributes["MySort"]);
    }
    
    private void binddata(String Sort)
    {
        DataSet ds = SearchSignOff.getCrewDetails(this.Vessel, this.Rank, this.txt_from.Text, this.txt_to.Text);
        ds.Tables[0].DefaultView.Sort = Sort;
        this.gvsearch.DataSource = ds.Tables[0];
        this.gvsearch.DataBind();
        gvsearch.Attributes.Add("MySort", Sort);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        binddata(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {
    }

    protected void GV_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label L1 = (Label)e.Row.FindControl("Lb_ReliverRankId");
            int Rank = Common.CastAsInt32(L1.Text);

            L1 = (Label)e.Row.FindControl("lb_ReliverID");
            int Rid = Common.CastAsInt32(L1.Text);

            L1 = (Label)e.Row.FindControl("lblvesselid");
            int Vid = Common.CastAsInt32(L1.Text);

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PlanningId FROM DBO.CREWVESSELPLANNINGHISTORY WHERE RELIEVERID=" + Rid.ToString() + " AND RELIEVERRANKID=" + Rank.ToString() + " AND STATUS='A' AND PLANTYPE='R' AND VESSELID=" + Vid.ToString());
            if (dt.Rows.Count > 0)
            {
                ImageButton b1 = (ImageButton)e.Row.FindControl("btnCL");
                b1.CommandArgument = dt.Rows[0][0].ToString();
            }
        }
    }
    protected void gvsearch_RowCreated(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgbut = (ImageButton)e.Row.FindControl("img_rel");  //e.Row.Cells[0].Controls[0];
            imgbut.CommandArgument = e.Row.RowIndex.ToString();

            //ImageButton imgbut1 = (ImageButton)e.Row.FindControl("img_rel1");  //e.Row.Cells[0].Controls[0];
            //imgbut1.CommandArgument = e.Row.RowIndex.ToString();
        }

    }
    protected void gvsearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Label Lb_CrewID, Lb_ReliverID, Lb_ReliverID1, lb_R_ID, Lb_ReliverRankId, Lb_ReliverRankId1;
        int _upd;
        _upd = 0;

        if (e.CommandName == "Select")
        {
           Label hfdvessel, hfdrank, hfdcrewid;
           GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
           hfdcrewid = (Label)gvsearch.Rows[row.RowIndex].FindControl("Lb_CrewID");
           hfdvessel = (Label)gvsearch.Rows[row.RowIndex].FindControl("lblvesselid");
           hfdrank = (Label)gvsearch.Rows[row.RowIndex].FindControl("lblrankid");
           VesselId = Convert.ToInt32(hfdvessel.Text);
           Session["S_VesselId"] = VesselId;
           
           RankId = Convert.ToInt32(hfdrank.Text);
           Session["S_RankId"] = RankId;
        }
                
        if (e.CommandName == "img_reliver")
        {
           DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
           foreach (DataRow dr in dtroleid.Rows)
           {
             if (Convert.ToInt32(dr["RoleId"]) != 4)
              {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow r = gvsearch.Rows[index];
                Lb_CrewID = (Label)r.FindControl("Lb_CrewID");
                lb_R_ID = (Label)r.FindControl("lb_R_ID");
                Lb_ReliverID = (Label)r.FindControl("Lb_ReliverID");
                Lb_ReliverID1 = (Label)r.FindControl("Lb_ReliverID1");
                Lb_ReliverRankId = (Label)r.FindControl("Lb_ReliverRankId");
                Lb_ReliverRankId1 = (Label)r.FindControl("Lb_ReliverRankId1");
                _upd = 1;

                //****************** Code to Check Deletion of Crew Member
                DataTable dtck = SearchSignOff.DeleteCrewfromPlanning(Convert.ToInt32(Lb_ReliverID.Text));
                foreach (DataRow drd in dtck.Rows)
                {
                    if(Convert.ToInt32(drd[0].ToString()) <= 0)
                    {
                        lb_msg.Text = "Crew Member Exists in an Open Port Call.";
                        return;
                    }
                }
                //******************
                SearchSignOff.UpdReliver_Tempplanning(Convert.ToInt32(Lb_CrewID.Text), Convert.ToInt32(Lb_ReliverID.Text), Convert.ToInt32(Lb_ReliverID1.Text), _upd);
              }
              else
              {
                  lb_msg.Text = "ReadOnly Users Are Not Authorized to Delete.";
              }
           }
       }
       else if (e.CommandName == "img_reliver1")
       {
         DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
         foreach (DataRow dr in dtroleid.Rows)
         {
           if (Convert.ToInt32(dr["RoleId"]) != 4)
             {
                 _upd = 2;
                 int index = Convert.ToInt32(e.CommandArgument);
                 GridViewRow r = gvsearch.Rows[index];
                 Lb_CrewID = (Label)r.FindControl("Lb_CrewID");
                 lb_R_ID = (Label)r.FindControl("lb_R_ID");
                 Lb_ReliverID = (Label)r.FindControl("Lb_ReliverID");
                 Lb_ReliverID1 = (Label)r.FindControl("Lb_ReliverID1");
                 Lb_ReliverRankId = (Label)r.FindControl("Lb_ReliverRankId");
                 Lb_ReliverRankId1 = (Label)r.FindControl("Lb_ReliverRankId1");

                 //****************** Code to Check Deletion of Crew Member
                 DataTable dtck = SearchSignOff.DeleteCrewfromPlanning(Convert.ToInt32(Lb_ReliverID1.Text));
                 foreach (DataRow drr in dtck.Rows)
                 {
                     if (Convert.ToInt32(drr[0].ToString()) <= 0)
                     {
                         lb_msg.Text = "Crew Member Exists in an Open Port Call.";
                         return;
                     }
                 }
                 //******************
                 SearchSignOff.UpdReliver_Tempplanning(Convert.ToInt32(Lb_CrewID.Text), Convert.ToInt32(Lb_ReliverID.Text), Convert.ToInt32(Lb_ReliverID1.Text), _upd);
             }
             else
             {
                 lb_msg.Text = "ReadOnly Users Are Not Authorized to Delete.";
             }
        }
      }
      Show_Grid();  
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
    }
    protected void btnCL_Click(object sender, EventArgs e)
    {
        string PlanningId = ((ImageButton)sender).CommandArgument;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "c", "window.open('ViewCrewCheckList.aspx?_P=" + PlanningId  + "');", true); 
    }
    
    protected void gvsearch_prerender(object sender, EventArgs e)
    {
        DateTime k;
        HiddenField hfd;
        int i;
        for (i = 0; i <= gvsearch.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)gvsearch.Rows[i].FindControl("hiddenreliefduedate");
            DateTime nowdate = System.DateTime.Now;
            if (hfd != null)
            {
                k = Convert.ToDateTime(hfd.Value);
                if (Convert.ToDateTime(k) < nowdate)
                {
                    gvsearch.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                }
            }
        }

        if (temp == 1)
        {
            gvsearch.SelectedIndex = -1;
        }
        else
        {
            if (gvsearch.SelectedIndex >= 0)
            {
                gvsearch.Rows[gvsearch.SelectedIndex].BackColor = System.Drawing.Color.FromName("#8fafdb");
            }
        }
    }
}