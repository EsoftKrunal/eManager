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
using System.Collections.Generic; 

public partial class UserControls_GraphicalPlanningTool : System.Web.UI.UserControl
{
    public int CurYear = DateTime.Today.Year;
    public int NowMonth = DateTime.Today.Month;
    public int[] MonthSum = {0,0,0,0,0,0,0,0,0,0,0,0};
    protected void Page_Load(object sender, EventArgs e)
    {
        lb_msg.Text = "";
        if (!Page.IsPostBack)
        {
            DataTable dt3 = VesselDetailsGeneral.selectDataOwnerName();
            this.chkOwner.DataValueField = "OwnerId";
            this.chkOwner.DataTextField = "OwnerShortName";
            dt3.Rows.RemoveAt(0);  
            this.chkOwner.DataSource = dt3;
            this.chkOwner.DataBind();

            Loadvessel();

            this.chkrank.DataTextField = "RankCode";
            this.chkrank.DataValueField = "RankId";
            this.chkrank.DataSource = SearchSignOff.getMasterData("Rank", "RankId", "RankCode");
            this.chkrank.DataBind();
       }
    }
    public void Loadvessel()
    {
        string owners = "";
        for (int i = 0; i <= chkOwner.Items.Count - 1; i++)
        {
            if (chkOwner.Items[i].Selected)
            {
                owners = owners + "," + chkOwner.Items[i].Value;
            }
        }
        if (owners.Trim() != "")
            owners = owners.Substring(1);

        DataSet dt8 ;
        if (owners.Trim() == "")
        {
            dt8 = Budget.getTable("SELECT vesselid,vesselname as name FROM VESSEL where VesselStatusid<>2 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Convert.ToInt32(Session["loginid"]) + ") Order By VesselName");
        }
        else
        {
            dt8 = Budget.getTable("SELECT vesselid,vesselname as name FROM VESSEL WHERE VesselStatusid<>2  And ownerID IN ( " + owners + ") and VesselId in (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Convert.ToInt32(Session["loginid"]) + ") Order By VesselName");
        }

        this.chkvessel.DataValueField = "VesselId";
        this.chkvessel.DataTextField = "Name";
        this.chkvessel.DataSource = dt8;
        this.chkvessel.DataBind();
    }
    protected void Chk_vessel_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem ls3 in this.chkvessel.Items)
        {
            ls3.Selected = Chk_vessel.Checked;
        }
    }
    protected void Chk_Owner_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem ls3 in this.chkOwner.Items)
        {
            ls3.Selected = chk_Owner.Checked;
        }
        OwnerSelected(sender,e);
        
    }
    protected void Chk_all_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem ls2 in this.chkrank.Items)
        {
            ls2.Selected = Chk_all.Checked;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (VesselList.Trim() == "")
        {
            lb_msg.Text = "Please select vessel.";
            return; 
        }
        if (RankList.Trim() == "")
        {
            lb_msg.Text = "Please select rank.";
            return;
        }
        DataSet ds = SearchSignOff.getCrewDetails(VesselList,RankList, "01/01/2001", "12/31/" + (CurYear+10).ToString());
        lblCount.Text = "Total Crew Count(" + ds.Tables[0].Rows.Count + ")";
        DataTable dtRes=new DataTable();

        dtRes.Columns.Add("VesselName");   
        dtRes.Columns.Add("CrewNumber");   
        dtRes.Columns.Add("CrewName");   
        dtRes.Columns.Add("RankName");   
        dtRes.Columns.Add("Nationality");   
        dtRes.Columns.Add("SignOnDate",typeof(DateTime));
        dtRes.Columns.Add("SignOffDate", typeof(DateTime));
        dtRes.Columns.Add("Relievers");
        dtRes.Columns.Add("Details");
        dtRes.Columns.Add("Mon1");
        dtRes.Columns.Add("Mon2");
        dtRes.Columns.Add("Mon3");
        dtRes.Columns.Add("Mon4");
        dtRes.Columns.Add("Mon5");
        dtRes.Columns.Add("Mon6");
        dtRes.Columns.Add("Mon7");
        dtRes.Columns.Add("Mon8");
        dtRes.Columns.Add("Mon9");
        dtRes.Columns.Add("Mon10");
        dtRes.Columns.Add("Mon11");
        dtRes.Columns.Add("Mon12");  

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            dtRes.Rows.Add(dtRes.NewRow());
            dtRes.Rows[dtRes.Rows.Count - 1]["VesselName"] = dr["VesselName"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["CrewNumber"] = dr["CrewNumber"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["CrewName"] = dr["CrewName"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["RankName"] = dr["RankName"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["Nationality"] = dr["Nationality"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["SignOnDate"] = dr["SignOnDate"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["SignOffDate"] = dr["SignOffDate"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["Relievers"] = dr["ReliverID"].ToString();
            dtRes.Rows[dtRes.Rows.Count - 1]["Details"] = dr["Details"].ToString();
            for (int i = 1; i <= 12; i++)
            {
                string color = getCellColor(dr["SignOffDate"].ToString(), dr["currentrankid"].ToString(), i);
                dtRes.Rows[dtRes.Rows.Count - 1]["Mon" + i.ToString()] = color;
            }
        }

        DataView dv = dtRes.DefaultView;
        dv.Sort = ddlSort.SelectedValue;
        DataTable dtRes2 = dv.ToTable();

        rptData.DataSource = dtRes2;
        Session["dtRes"] = dtRes2;  
        rptData.DataBind();  
    }
    public string VesselList
    {
        get
        {
            String vess;
            vess = "";
            for (int i = 0; i < this.chkvessel.Items.Count; i++)
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
            return vess;
        }
    }
    public string RankList
    {
        get
        {
            string rankk;
            rankk = "";
            for (int i = 0; i < this.chkrank.Items.Count; i++)
            {
                if (chkrank.Items[i].Selected)
                {
                    if (rankk == "")
                    {
                        rankk = chkrank.Items[i].Value;
                    }
                    else
                    {
                        rankk = rankk + ',' + chkrank.Items[i].Value;
                    }

                }
            }
            return rankk;
        }
    }
    public string getCellColor(string _ReliefDueDate, string _Rank, int CurMonth)
    {
        char[] splitter={'/'};
        bool overdue = false;
        string[] Period=ConfigurationManager.AppSettings["SignOnPeriodMAEOther"].Split(splitter);   
        int Rank=int.Parse(_Rank);
        DateTime RDueDate=DateTime.Parse(_ReliefDueDate);
        if (DateTime.Parse(RDueDate.Month.ToString() + "/01/" + RDueDate.Year.ToString()) < DateTime.Parse(NowMonth.ToString() + "/01/" + CurYear.ToString()))        
        {
            RDueDate = Convert.ToDateTime(NowMonth + "/01/" + CurYear.ToString());
            overdue = true;
        }
         
        if (RDueDate.Month == CurMonth && RDueDate.Year == CurYear)
        {
            MonthSum[CurMonth - 1] = MonthSum[CurMonth - 1] + 1;
            if (overdue)
                return "Coral";
            else
                return "Green";
        }
        else if (CurYear >= RDueDate.Year)
        {
            DateTime LastMonth = Convert.ToDateTime("01" + RDueDate.ToString("-MMM-yyyy"));
            List<string > Months = new List<string>();
            
            if (Rank == 1 || Rank == 12) // Master Or Chief Engineer 4 Month
            {
                while (LastMonth <= Convert.ToDateTime("01-Dec-" + CurYear.ToString()))
                {
                    LastMonth = LastMonth.AddMonths(int.Parse(Period[0]));
                    Months.Add(LastMonth.ToString("dd-MMM-yyyy"));   
                }
            }
            else if (Rank == 2 || Rank == 15) // Chief Officer Or Ist AE 6 Month
            {
                while (LastMonth <= Convert.ToDateTime("01-Dec-" + CurYear.ToString()))
                {
                    LastMonth = LastMonth.AddMonths(int.Parse(Period[1]));
                    Months.Add(LastMonth.ToString("dd-MMM-yyyy"));
                }
            }
            else // 9 Month
            {
                while (LastMonth <= Convert.ToDateTime("01-Dec-" + CurYear.ToString()))
                {
                    LastMonth = LastMonth.AddMonths(int.Parse(Period[2]));
                    Months.Add(LastMonth.ToString("dd-MMM-yyyy"));
                }
            }
            if(Months.Contains(Convert.ToDateTime(CurMonth.ToString() + "/01/" + CurYear.ToString()).ToString("dd-MMM-yyyy")))
            {
                MonthSum[CurMonth - 1] = MonthSum[CurMonth - 1]+1;
                if (overdue)
                    return "cornflowerblue";
                else
                {
                    if (MonthSum.Length <= 1)
                    {
                        return "Green";
                    }
                    else
                    {
                        return "cornflowerblue";
                    }
                }
            }
            else
            {
                return""; 
            }
        }
        else
        {
            return "";
        }
    }
    public string FormatDate(object inDate)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(inDate);    
            return dt.ToString("dd-MMM-yyyy");
        }
        catch 
        {
            return "";
        }
    }
    public string getPlanned(string _Relievers)
    {
        bool planned = false;
        if (int.Parse(_Relievers) > 0)
        {
            planned = true; 
        }
        if (planned)
            return "style='background-color:Yellow'";
        else
            return "style='background-color:Auto'";
    }
    protected void OwnerSelected(object sender, EventArgs e)
    {
        Loadvessel();
        if (Chk_vessel.Checked)
        {
            foreach (ListItem li in chkvessel.Items)
            {
                li.Selected = true;
            }
        }
    }
    protected void Sorting_Changed(object sender, EventArgs e)
    {
        Button2_Click(sender, e); 
    }
}
