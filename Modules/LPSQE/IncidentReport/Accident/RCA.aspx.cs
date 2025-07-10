using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class eReports_S115_eReport_S115 : System.Web.UI.Page
{
    string FormNo = "S115";
    public int ReportId
    {       
        get{ return Common.CastAsInt32(ViewState["ReportId"]);}
        set {ViewState["ReportId"]=value ;}
    }
    public int Stage
    {
        get { return Common.CastAsInt32(ViewState["_Stage"]); }
        set { ViewState["_Stage"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgAddMembers.Text = "";
        lblMsg.Text = "";
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        if (!Page.IsPostBack)
        {
            ReportId = Common.CastAsInt32(Page.Request.QueryString["ReportId"]);
            VesselCode = Convert.ToString(Page.Request.QueryString["VesselCode"]);

            ShowMasterData();
            BindTeammembers();
        }
        //------------------------------------
    }
    protected void btnSaveRcaAssigne_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (rdoListSeverity.SelectedIndex < 0)
            {
                lblMsg.Text = "Please select severity";
                return;
            }
            if (txtFocalPoint.Text.Trim()=="")
            {
                lblMsg.Text = "Please enter focal point";
                return;
            }
            if (rptTeammembers.Items.Count==0)
            {
                lblMsg.Text = "Please select team member";
                return;
            }
            if (hfdTeamLeadValue.Value=="")
            {
                lblMsg.Text = "Please select team leader";
                return;
            }
            if (txtTargetClosureDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter target closure date";
                return;
            }

            Common.Set_Procedures("[DBO].[ER_S115_IP_ER_S115_Report_RCA]");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                new MyParameter("@ReportId", ReportId),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@Severity", rdoListSeverity.SelectedValue),
                new MyParameter("@FocalPoint", txtFocalPoint.Text.Trim()),
                new MyParameter("@TargetClosureDate", txtTargetClosureDate.Text.Trim()),
                new MyParameter("@Stage", Stage),
                new MyParameter("@TeamLeader", hfdTeamLeadValue.Value),
                new MyParameter("@InitiatedBy", Convert.ToInt32(Session["loginid"].ToString()))
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Record saved successfully.";
                ShowMasterData();
                BindTeammembers();
            }
            else
            {
                lblMsg.Text = "Unable to save record. " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save record." + ex.Message;
        }
    }
    protected void btnDeleterTeamMember_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string SQL = "  Delete  from dbo.ER_S115_Report_RCA_Team where ReportID=" + ReportId+" and VesselCode='"+VesselCode+"' and EmpID="+ btn .CommandArgument+ "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        BindTeammembers();
    }
    

    public void ShowMasterData()
    {
        Stage = 1;
        string SQL = " select ReportId,VesselCode,FocalPoint,TargetClosureDate,Stage,TeamLeader from dbo.ER_S115_Report_RCA where ReportID=" + ReportId+" and VesselCode='"+VesselCode+"' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            txtFocalPoint.Text = dt.Rows[0]["FocalPoint"].ToString();
            txtTargetClosureDate.Text = Common.ToDateString(dt.Rows[0]["TargetClosureDate"]);
            Stage = Common.CastAsInt32(dt.Rows[0]["Stage"])+1;
            hfdTeamLeadValue.Value = Convert.ToString( dt.Rows[0]["TeamLeader"]);
        }

        SQL = " select AccidentSeverity from dbo.[ER_S115_Report] where ReportID=" + ReportId + " and VesselCode='" + VesselCode + "' ";
        DataTable dtac = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dtac.Rows.Count > 0)
        {
            try
            {
                rdoListSeverity.SelectedValue = dtac.Rows[0][0].ToString();
            }
            catch
            {
                rdoListSeverity.ClearSelection();
            }
        }
    }
    public void BindTeammembers()
    {
        string SQL = " select * from dbo.ER_S115_Report_RCA_Team T with(nolock) "+
                     //"  INNER JOIN DBO.position PO ON PO.positionid = T.Position " +
                     "  Where ReportID ="+ReportId+" and VesselCode = '"+VesselCode+"' order by T.EmpName";
        DataTable dtTeamMembers = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptTeammembers.DataSource = dtTeamMembers;
        rptTeammembers.DataBind();
    }


    //-- Add Team Member -------------------------------------------------------------------------------
    protected void lnkAddTeamMember_OnClick(object sender, EventArgs e)
    {
        ShowTeamMemberToAdd();
        divAddTeamMember.Visible = true;
    }
    protected void btnSaveTeamMember_OnClick(object sender, EventArgs e)
    {
        try
        {
            string empIDs = "";            
            //foreach (ListItem item in chkListFleetManager.Items)
            //{
            //    if (item.Selected)
            //        empIDs = empIDs + ',' + item.Value;
            //}
            //foreach (ListItem item in chkListTechnicalSuptd.Items)
            //{
            //    if (item.Selected)
            //        empIDs = empIDs + ',' + item.Value;
            //}
            //foreach (ListItem item in chkListMaringSuptd.Items)
            //{
            //    if (item.Selected)
            //        empIDs = empIDs + ',' + item.Value;
            //}
            foreach (ListItem item in chkListManagement.Items)
            {
                if (item.Selected)
                    empIDs = empIDs + ',' + item.Value;
            }



            Common.Set_Procedures("[DBO].[ER_S115_IP_ER_S115_Report_RCA_Team]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@ReportId", ReportId),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@EmpIDs", empIDs)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsgAddMembers.Text = "Record saved successfully.";
                BindTeammembers();
            }
            else
            {
                lblMsgAddMembers.Text = "Unable to save record. " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsgAddMembers.Text = "Unable to save record." + ex.Message;
        }
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        divAddTeamMember.Visible = false;
    }

    public void ShowTeamMemberToAdd()
    {
        //string SQL = " select PD.EmpId,PD.EmpCode,PD.FirstName + ' ' + PD.MiddleName + ' ' + PD.FamilyName as EmpName ,case when Te.EmpName is null then 0 else 1 end EmpSelected "+
        //             "  , PO.vesselpositions from DBO.Hr_PersonalDetails PD " +
        //             "  left join dbo.ER_S115_Report_RCA_Team Te on Te.EmpID = PD.EmpID " +
        //             "  INNER JOIN DBO.position PO ON PO.positionid = PD.Position " +
        //             "  WHERE DRC IS NULL order by PO.vesselpositions,EmpName ";
        //DataTable dtEmployee = Common.Execute_Procedures_Select_ByQuery(SQL);

        ////dtEmployee.AsEnumerable().Where(row => row.Field<int?>("vesselpositions") == 3).DefaultIfEmpty();
        //DataView dv = dtEmployee.DefaultView;dv.RowFilter = "vesselpositions=3";

        //chkListFleetManager.DataSource = dv;
        //chkListFleetManager.DataTextField = "EmpName";
        //chkListFleetManager.DataValueField = "EmpId";
        //chkListFleetManager.DataBind();
        //foreach (ListItem itm in chkListFleetManager.Items)
        //{
        //    if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32( itm.Value)).Count() > 0)
        //        itm.Selected = true;
        //}

        //dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=1";
        //chkListTechnicalSuptd.DataSource = dv;
        //chkListTechnicalSuptd.DataTextField = "EmpName";
        //chkListTechnicalSuptd.DataValueField = "EmpId";
        //chkListTechnicalSuptd.DataBind();
        //foreach (ListItem itm in chkListTechnicalSuptd.Items)
        //{
        //    if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
        //        itm.Selected = true;
        //}


        //dv = dtEmployee.DefaultView; dv.RowFilter = "vesselpositions=2";
        //chkListMaringSuptd.DataSource = dv;
        //chkListMaringSuptd.DataTextField = "EmpName";
        //chkListMaringSuptd.DataValueField = "EmpId";
        //chkListMaringSuptd.DataBind();
        //foreach (ListItem itm in chkListMaringSuptd.Items)
        //{
        //    if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
        //        itm.Selected = true;
        //}

        string SQL = " select PD.LoginId As EmpId,PD.UserId As EmpCode,PD.FirstName + ' ' + PD.LastName as EmpName ,case when Te.EmpName is null then 0 else 1 end EmpSelected from DBO.UserMaster PD with(nolock) left join dbo.ER_S115_Report_RCA_Team Te with(nolock) on Te.EmpID = PD.LoginId WHERE StatusId = 'A'  order by EmpName  ";

        DataTable dtEmployee = Common.Execute_Procedures_Select_ByQuery(SQL);

        DataView dv = dtEmployee.DefaultView; 
        chkListManagement.DataSource = dv;
        chkListManagement.DataTextField = "EmpName";
        chkListManagement.DataValueField = "EmpId";
        chkListManagement.DataBind();
        foreach (ListItem itm in chkListManagement.Items)
        {
            if (dv.ToTable().AsEnumerable().Where(row => row.Field<int>("EmpSelected") == 1 && row.Field<int>("EmpID") == Common.CastAsInt32(itm.Value)).Count() > 0)
                itm.Selected = true;
        }
    }

}