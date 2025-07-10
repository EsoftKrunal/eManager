using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text; 
using System.Collections.Generic;

public partial class VerifyWC : System.Web.UI.Page
{
    public int fleetid
    {
        set
        {
            ViewState["fleetid"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["fleetid"]);
        }
    }
    public int vesseltypeid
    {
        set
        {
            ViewState["vesseltypeid"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["vesseltypeid"]);
        }
    }
    public int vesselid
    {
        set
        {
            ViewState["vesselid"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["vesselid"]);
        }
    }
    public int reason
    {
        set
        {
            ViewState["reason"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["reason"]);
        }
    }
    public string fromdate
    {
        set
        {
            ViewState["fromdate"] = value;
        }
        get
        {
            return ViewState["fromdate"].ToString();
        }
    }
    public string QueryTYpe
    {
        set
        {
            ViewState["QueryTYpe"] = value;
        }
        get
        {
            return ViewState["QueryTYpe"].ToString();
        }
    }
    
    public string todate
    {
        set
        {
            ViewState["todate"] = value;
        }
        get
        {
            return ViewState["todate"].ToString();
        }
    }
    public string VNC
    {
        set
        {
            ViewState["VNC"] = value;
        }
        get
        {
            return ViewState["VNC"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            //----------------------------------------------------------
            fleetid = Common.CastAsInt32(Request.QueryString["fleet"]);
            vesseltypeid = Common.CastAsInt32(Request.QueryString["vtype"]);
            reason = Common.CastAsInt32(Request.QueryString["reason"]);
            vesselid = Common.CastAsInt32(Request.QueryString["vesselid"]);
            QueryTYpe = Request.QueryString["QueryTYpe"].ToString();
            VNC = Request.QueryString["VNC"].ToString();

            DateTime dt = new DateTime(Common.CastAsInt32(Request.QueryString["fy"]),Common.CastAsInt32(Request.QueryString["fm"]),1);
            fromdate = dt.ToString("dd-MMM-yyyy");
            dt = new DateTime(Common.CastAsInt32(Request.QueryString["ty"]), Common.CastAsInt32(Request.QueryString["tm"]), 1);
            todate = dt.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            ShowData();
        }
    }
    public void ShowData()
    {
    //----------------------------------------------------------
            string whereclause = " where 1=1  ";
            //-----------
            if (QueryTYpe == "NCType1")
            {
                whereclause = whereclause + " and c.NCType=2";
            }
            if (QueryTYpe == "NCType2")
            {
                whereclause = whereclause + "and  c.NCType=6";
            }
            if (QueryTYpe == "NCType3")
            {
                whereclause = whereclause + "and c.NCType=7";
            }
            if (QueryTYpe == "NCType4")
            {
                whereclause = whereclause + "and c.NCType=24";
            }
            if (QueryTYpe == "UnVerified")
            {
                whereclause = whereclause + "and v.VERIFIEDBY IS NULL ";
            }
            if (QueryTYpe == "Verified")
            {
                whereclause = whereclause + "and v.VERIFIEDBY IS NOT NULL ";
            }
            

            //-----------
            
            if (fleetid > 0)
            {
                whereclause += "and v1.fleetid=" + fleetid.ToString();
            }
            if (vesseltypeid > 0)
            {
                whereclause += "and v1.vesseltypeid=" + vesseltypeid.ToString();
            }
            if (vesselid > 0)
            {
                whereclause += "and c.vesselid=" + vesselid.ToString();
            }
            if (reason > 0)
            {
                whereclause += "and r.reason=" + reason.ToString();
            }
            if (fromdate != "")
            {
                whereclause += "and c.NCDATE >='" + fromdate + "' ";
            }
            if (todate != "")
            {
                whereclause += "and c.NCDATE <='" + todate + "' ";
            }
            if (VNC == "true")
            {
                whereclause += "and v.VERIFIEDBY IS NULL ";
            }
        
            string sql = "select V1.VESSELCode,v.Remarks "+
                         ", (case When v.VERIFIEDBY IS NULL THEN ' Un-Verifed ' ELSE ' Verified ' END )Status " +
                         ", (case r.Reason When 1 Then 'At Sea Cargo operations' When 2 Then 'At Sea Navigation' When 3 Then 'At Sea Ship Maintenance' When 4 Then 'In Port Cargo Operations' When 5 Then 'In Port Ship Maintenance'  When 6 then 'In Port Maneuvering' end)ReasonName " +
                         " ,cpd.crewnumber,cpd.firstname + ' ' + cpd.middlename + ' ' + cpd.lastname as CrewName,c.NCDate,TP.NCTYPENAME,v.VerifiedBy,V.VerifiedOn,V.Remarks,rm.NCReasonName,C.VESSELID,C.CREWID,C.NCTYPE,isnull(R.REASON,0) as REASON,rk.rankcode " +
                         "from " +
                         " ( select distinct vesselid,crewid,ncdate,nctype from CP_NONCONFORMANCE ) c " +
                         "inner join CP_NCTYPE TP ON TP.NCTYPEID=C.NCTYPE " +
                         "inner join vessel v1 on v1.vesselid=c.vesselid " +
                         "inner join crewpersonaldetails cpd on cpd.crewid=c.crewid " +
                         "inner join rank rk on rk.rankid=cpd.currentrankid " +
                         "left join CP_NONCONFORMANCE_verification v on c.vesselid=v.vesselid and c.crewid=v.crewid and c.ncdate=v.ncdate and c.nctype=v.nctype  " +
                         "left join CP_NonConformanceReason r on c.vesselid=r.vesselid and c.crewid=r.crewid and c.ncdate=r.ncdate  " +
                         "left join cp_NCREASON rm on r.reason=rm.NCReasonId " + whereclause + " order by v1.VESSELCode,c.ncdate";

            DataTable dtRes = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //Response.Write(sql);
            rptVesselList.DataSource = dtRes;
            rptVesselList.DataBind();

            lblRC.Text = "[ " + dtRes.Rows.Count.ToString() + " ] Records Found.";
        }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //bool sel = false;
        //foreach (RepeaterItem ri in rptVesselList.Items)
        //{
        //    CheckBox chk = (CheckBox)ri.FindControl("chkSel");
        //    if (chk.Checked)
        //    {
        //        sel = true;
        //        string reason = ((DropDownList)ri.FindControl("ddlReason")).SelectedValue;
        //        string vesselid = ((HiddenField)ri.FindControl("hfdVessel")).Value;
        //        string crewid = ((HiddenField)ri.FindControl("hfdCrew")).Value;
        //        string ncdate = ((HiddenField)ri.FindControl("hfdNCDate")).Value;
        //        string nctype = ((HiddenField)ri.FindControl("hfdNCType")).Value;

        //        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_NonConformanceReason WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "'");
        //        if(DT.Rows.Count <=0) 
        //            Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CP_NonConformanceReason VALUES(" + vesselid + "," + crewid + ",'" + ncdate + "'," + reason + ")");
        //        else
        //            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CP_NonConformanceReason SET REASON=" + reason + " WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "'"); 
        //    }
        //}
        //if (sel)
        //{
        //    lblMessage.Text = "Record saved successfully.";
        //    lblMessage.ForeColor = System.Drawing.Color.Green;
        //}
        //else
        //{
        //    lblMessage.Text = "Please select at least one record to save.";
        //    lblMessage.ForeColor = System.Drawing.Color.Red;
        //}
        //ShowData();
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter remarks to verify.";  
            return;
        }
        bool sel=false;
        foreach (RepeaterItem ri in rptVesselList.Items)
        {
            CheckBox chk = (CheckBox)ri.FindControl("chkSel");
            if (chk.Checked)
            {
                sel = true;
                string reason = ((DropDownList)ri.FindControl("ddlReason")).SelectedValue;
                string vesselid = ((HiddenField)ri.FindControl("hfdVessel")).Value;
                string crewid = ((HiddenField)ri.FindControl("hfdCrew")).Value;
                string ncdate = ((HiddenField)ri.FindControl("hfdNCDate")).Value;
                string nctype = ((HiddenField)ri.FindControl("hfdNCType")).Value;
                //---------------------------------
                DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_NonConformanceReason WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "'");
                if (DT.Rows.Count <= 0)
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CP_NonConformanceReason VALUES(" + vesselid + "," + crewid + ",'" + ncdate + "'," + reason + ")");
                else
                    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CP_NonConformanceReason SET REASON=" + reason + " WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "'"); 
                //---------------------------------
                DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_NONCONFORMANCE_verification WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "' AND NCTYPE=" + nctype );
                if (DT.Rows.Count > 0)
                    Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CP_NONCONFORMANCE_verification WHERE VESSELID=" + vesselid + " AND CREWID=" + crewid + " AND NCDATE='" + ncdate + "' AND NCTYPE=" + nctype);
                
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CP_NONCONFORMANCE_verification VALUES(" + vesselid + "," + crewid + ",'" + ncdate + "'," + nctype + ",'" + Session["UserName"].ToString() + "',getdate(),'" + txtRemarks.Text + "')");
                //---------------------------------
            }
        }
        if (sel)
        {
            lblMessage.Text = "Record verified successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green; 
        }
        else
        {
            lblMessage.Text = "Please select at least one record to verify.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        ShowData();
    }
}

