using System;
using System.Data;
using System.Configuration;
using System.Reflection;
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

public partial class CrewAvailablity : System.Web.UI.Page
{
    public int PageSlot
    {
        get { return Common.CastAsInt32(ViewState["PageSlot"]); }
        set { ViewState["PageSlot"] = value; }

    }
    public int MaxPageSlot
    {
        get { return Common.CastAsInt32(ViewState["MaxPageSlot"]); }
        set { ViewState["MaxPageSlot"] = value; }

    }
    public int Day
    {
        get { return Common.CastAsInt32(ViewState["_days"]); }
        set { ViewState["_days"] = value; }

    }
    string sql51_crew = "SELECT row_number() over(ORDER BY RANKLEVEL,SignOffDate desc) as Sno,CREWNUMBER ,CPD.CrewId,SignOffDate,AvalRemark,UL.Firstname+' '+UL.Lastname as Avl_By,AvlOn,RANKLEVEL,VesselCode,DATEDIFF(DD,SignOffDate,GETDATE()) as DAYS_ON_LEAVE,AvailableFrom " +
                       " , CPD.FIRSTNAME + ' ' + MIDDLENAME + ' ' + CPD.LASTNAME AS CREWNAME,VESSELNAME,RANKCODE,C.COUNTRYNAME,EXPECTEDJOINDATE,C1.COUNTRYCODE + '- '  + MobileNumber AS CONTACTNO,Email1,CREWSTATUSNAME,RECRUITINGOFFICENAME  FROM " +
                       "DBO.CREWPERSONALDETAILS CPD  " +
                       "INNER JOIN RANK R ON CPD.CURRENTRANKID=R.RANKID " +
                       "INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NationalityId " +
                       "LEFT JOIN CREWCONTACTDETAILS CCD ON CCD.CREWID=CPD.CREWID AND CCD.ADDRESSTYPE='C' " +
                       "LEFT JOIN COUNTRY C1 ON C1.COUNTRYID=CCD.MobileCountryId " +
                       "LEFT JOIN VESSEL V ON V.VESSELID=CPD.LASTVESSELID " +
                       "LEFT JOIN UserLogin UL on UL.loginid=CPD.AvlBy " +
                       "LEFT JOIN CrewStatus CS on CS.CREWSTATUSID=CPD.CREWSTATUSID " +
                       "LEFT JOIN RECRUITINGOFFICE RO ON RO.RECRUITINGOFFICEID=cpd.RecruitmentOfficeId " +
                       "WHERE cpd.CREWSTATUSID=2 AND CPD.CURRENTRANKID NOT IN (48,49) AND CPD.CREWID NOT IN ( SELECT DISTINCT C1.FirstRelieverId FROM CREWPERSONALDETAILS C1 WHERE ISNULL(C1.FirstRelieverId,0)>0 )";

    string sql51_app = "SELECT CANDIDATEID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,'' AS VESSELNAME,R.RANKCODE,C.COUNTRYNAME,AVAILABLEFROM AS EXPECTEDJOINDATE " +
                       "FROM DBO.candidatepersonaldetails CPD " +
                       "INNER JOIN RANK R ON CPD.RANKAPPLIEDID=R.RANKID  " +
                       "INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NationalityId WHERE STATUS<>5 ";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();

        if (!Page.IsPostBack)
        {
            PageSlot = 1;
            txtExpireIn.Text = "" + Request.QueryString["AD"];
            MaxPageSlot = 20;
            //if (Common.CastAsInt32(Page.Request.QueryString["AD"].ToString()) > 0)
            //{
            //    lblAvailabilityDays.Text = Page.Request.QueryString["AD"].ToString();
            //    txtAvailability.Text = Page.Request.QueryString["AD"].ToString();
            //}
            BindReport();
            BindRO();
            BindRankDropDown();
            BindNationalityDropDown();

        }
        
            
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if(PageSlot > 1)
            PageSlot--;

        BindReport();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (PageSlot < MaxPageSlot)
            PageSlot++;

        BindReport();
    }
    //protected void chkTXT_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    txtAvailability.Enabled = chkTXT.Checked;
    //    if (!(chkTXT.Checked))
    //    {
    //        txtAvailability.Text = "";
    //        chkExludenotUpdate.Checked = false;
    //    }

    //    chkExludenotUpdate.Enabled = chkTXT.Checked;
    //}
    protected void btn_Show51_Click(object sender, EventArgs e)
    {
        BindReport();
        {
            //string crewwhereclause = "";
            //if (txtCrewNo.Text.Trim() != "")
            //{
            //    crewwhereclause += " AND CPD.CREWNUMBER='" + txtCrewNo.Text.Trim() + "'";
            //}
            //if (txtCrewName.Text.Trim() != "")
            //{
            //    crewwhereclause += " AND (FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME) LIKE '%" + txtCrewName.Text.Trim() + "%'";
            //}
            //if (ddlRank.SelectedIndex > 0)
            //{
            //    crewwhereclause += " AND CPD.CURRENTRANKID=" + ddlRank.SelectedValue + "";
            //}
            //if (ddlNat.SelectedIndex > 0)
            //{
            //    crewwhereclause += " AND CPD.NationalityId=" + ddlNat.SelectedValue + "";
            //}
            //if (Common.CastAsInt32(txtNextDays.Text) > 0)
            //{
            //    crewwhereclause += " AND EXPECTEDJOINDATE <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtNextDays.Text))) + "'";
            //}

            //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql51_crew + crewwhereclause + " ORDER BY RANKLEVEL,EXPECTEDJOINDATE");

            //rpt51.DataSource = dt;
            //lblRcount51.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
            //rpt51.DataBind();
        }
    }
    protected void rad_CA_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Show51_Click(sender, e);
    }
    protected void lnlEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton nlk =(ImageButton )sender;
        HiddenField hfCrewId = (HiddenField)nlk.FindControl("hfCrewId");
        HiddenField hfAvailablefrom = (HiddenField)nlk.FindControl("hfAvailablefrom");
        HiddenField hfAvalRemark = (HiddenField)nlk.FindControl("hfAvalRemark");

        txt_AvailableDt.Text = hfAvailablefrom.Value;
        txtAvlRem.Text = hfAvalRemark.Value;

        HiddenPK.Value = hfCrewId.Value;
        dv_Pop51.Visible = true;
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dv_Pop51.Visible = false;
        BindReport();
    }
    protected void btn_Update_AvailabelDate_Click(object sender, EventArgs e)
    {
        if (txt_AvailableDt.Text.Trim() == "")
        {
            lblMessage.Text = "Available date is required.";
            txt_AvailableDt.Focus();
            return;
        }
        if (txt_AvailableDt.Text.Trim() != "")
        {
            if (DateTime.Today >= DateTime.Parse(txt_AvailableDt.Text))
            {
                lblMessage.Text = "Available date must be more than today.";
                return;
            }
        }
        if (txtAvlRem.Text.Trim() == "")
        {
            lblMessage.Text = "Remark field is required.";
            txtAvlRem.Focus();
            return;
        }
        try
        {
            Alerts.Update_AvailableDate(Convert.ToInt32(HiddenPK.Value), DateTime.Parse(txt_AvailableDt.Text), txtAvlRem.Text, Convert.ToInt32(Session["loginid"].ToString()));
            lblMessage.Text = "Updated Successfully.";
        }
        catch
        {
            lblMessage.Text = "Cant Updated.";
        }

    }

    //-- Function ----------------------------------------------------------------------------------------
    public void BindReport()
    {
        {
            lblAvailabilityDays.Text = txtExpireIn.Text;

            string crewwhereclause = "";
            if (txtCrewNo.Text.Trim() != "")
            {
                crewwhereclause += " AND CPD.CREWNUMBER='" + txtCrewNo.Text.Trim() + "'";
            }
            if (txtCrewName.Text.Trim() != "")
            {
                crewwhereclause += " AND (FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME) LIKE '%" + txtCrewName.Text.Trim() + "%'";
            }
            if (ddlRank.SelectedIndex > 0)
            {
                crewwhereclause += " AND CPD.CURRENTRANKID=" + ddlRank.SelectedValue + "";
            }
            if (ddlNat.SelectedIndex > 0)
            {
                crewwhereclause += " AND CPD.NationalityId=" + ddlNat.SelectedValue + "";
            }
            
            if (Common.CastAsInt32(lblAvailabilityDays.Text) > 0)
            {
               crewwhereclause += " AND ( AvailableFrom <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtExpireIn.Text.Trim()))) + "' or AvailableFrom is null )";
            }
            
            if (ddlRO.SelectedIndex > 0)
            {
                crewwhereclause += " AND cpd.RecruitmentOfficeId=" + ddlRO.SelectedValue + "";
            }

            string finalsql = "SELECT count(*) FROM ( " + sql51_crew + crewwhereclause + " ) A ";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(finalsql);
            if (dt.Rows.Count > 0)
                MaxPageSlot = Common.CastAsInt32(Math.Ceiling((decimal)(Common.CastAsInt32(dt.Rows[0][0]) / 20)));

            lblRcount51.Text = " ( " + Common.CastAsInt32(dt.Rows[0][0]).ToString() + " Records )";

            finalsql = "SELECT TOP 20 * FROM ( " +  sql51_crew + crewwhereclause + " ) A WHERE SNO >"  + (20*(PageSlot-1)).ToString() + " ORDER BY SNO";
            dt = Common.Execute_Procedures_Select_ByQueryCMS(finalsql);
            lblCounter.Text=" Records " + (((PageSlot-1)*20)+1) + " - " + (((PageSlot) * 20));
            

            rpt51.DataSource = dt;
            
            rpt51.DataBind();

            

        }
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddlRank.DataSource = obj.ResultSet.Tables[0];
        ddlRank.DataTextField = "RankName";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();

    }
    private void BindRO()
    {
        ddlRO.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM RECRUITINGOFFICE ");
        ddlRO.DataTextField = "RECRUITINGOFFICENAME";
        ddlRO.DataValueField = "RECRUITINGOFFICEID";
        ddlRO.DataBind();
        ddlRO.Items.Insert(0, new ListItem("All", "0"));
    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddlNat.DataSource = obj.ResultSet.Tables[0];
        ddlNat.DataTextField = "CountryName";
        ddlNat.DataValueField = "CountryId";
        ddlNat.DataBind();
    }
}
