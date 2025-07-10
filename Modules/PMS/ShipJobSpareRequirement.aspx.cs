using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class ShipJobSpareRequirement : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }
    }
    public int ComponentId
    {
        set { ViewState["ComponentId"] = value; }
        get { return Common.CastAsInt32(ViewState["ComponentId"]); }

    }    
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return Convert.ToString(ViewState["VesselCode"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgAddSpares.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMSG.Text = "";
        
        if (Page.Request.QueryString["CJID"] != null)
            CompJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
        if (Page.Request.QueryString["ComponentId"] != null)
            ComponentId = Common.CastAsInt32(Page.Request.QueryString["ComponentId"]);
        if (Page.Request.QueryString["VesselCode"] != null)
            VesselCode = Page.Request.QueryString["VesselCode"];

        tblAddSpare.Visible = (Session["UserType"].ToString() == "O");
        
        if (!Page.IsPostBack)
        {
            //string Parents = ProjectCommon.getParentComponents_Chain(ComponentId);
            //string strSpares = "SELECT VESSELCODE+'#'+CONVERT(VARCHAR,COMPONENTID)+'#'+OFFICE_SHIP+'#'+CONVERT(VARCHAR,SPAREID) AS PKID ,SPARENAME + ' - ' + PARTNO AS SPARENAME from VSL_VesselSpareMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId IN (" + ComponentId + "," + Parents + ") order by SPARENAME ";
            //DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(strSpares);
            //if (dtspares.Rows.Count > 0)
            //{
            //    ddlSparesList.DataSource = dtspares;
            //    ddlSparesList.DataTextField = "SPARENAME";
            //    ddlSparesList.DataValueField = "PKID";
            //    ddlSparesList.DataBind();
            //}
            //ddlSparesList.Items.Insert(0, new ListItem("< SELECT >", "0"));
            
            ShowJobDescription();
            BindRepeater();
        }
    }
    public void ShowJobDescription()
    {
        string sql = " SELECT  (LTRIM(RTRIM(CM.ComponentCode))+' : '+ CM.ComponentName) As CompName,CM.ComponentCode,CM.CriticalEquip AS IsCritical,DM.DeptName,RM.RankCode,JM.JobCode " +
                    " ,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.DescrM AS LongDescr,vcjm.JobCost from ComponentsJobMapping CJM  " +
                    " INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                    " INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId  " +
                    " INNER JOIN Rank RM ON RM.RankId = CJM.AssignTo  " +
                    " INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId  " +
                    " INNER JOIN JobIntervalMaster JIM ON CJM.IntervalId = JIM.IntervalId  " +
                    " INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode= '" + VesselCode + "'  and VCJM.CompJobID=CJM.CompJobID" +
                    " WHERE CJM.CompJobId = " + CompJobId + "";
        DataTable dtJobDesc = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtJobDesc != null)
        {
            if (dtJobDesc.Rows.Count > 0)
            {
                DataRow Dr = dtJobDesc.Rows[0];

                ViewState["ComponentCode"]= Dr["ComponentCode"].ToString(); ;
                lblCompName.Text = Dr["CompName"].ToString();
                lblShortDesc.Text = Dr["JobName"].ToString();
                lblLongDesc.Text = Dr["LongDescr"].ToString().Replace("\n", "<br/>");
                lblJobCost.Text = Dr["JobCost"].ToString() + " (US$)";
            }
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    if ((Session["UserType"].ToString() == "O"))
    //    {
    //        if (ddlSparesList.SelectedIndex <= 0)
    //        {
    //            lblMSG.Text = "Please select a Spare.";
    //            ddlSparesList.Focus();
    //            return;
    //        }
    //        else if (ddlSparesList.SelectedIndex <= 0)
    //        {
    //            lblMSG.Text = "Please select Qty.";
    //            txtQty.Focus();
    //            return;
    //        }
    //        else
    //        {
    //            string _VesselCode = "";
    //            int _ComponentId = 0;
    //            string _Office_Ship = "";
    //            int _SpareId = 0;
    //            ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

    //            Common.Set_Procedures("sp_InsertUpdateJobSpareRequirement");
    //            Common.Set_ParameterLength(7);
    //            Common.Set_Parameters(
    //                    new MyParameter("@VesselCode", VesselCode),
    //                    new MyParameter("@ForCompJobId", CompJobId),
    //                    new MyParameter("@ComponentId", _ComponentId),
    //                    new MyParameter("@Office_Ship", _Office_Ship),
    //                    new MyParameter("@SpareId", _SpareId),
    //                    new MyParameter("@Qty", Common.CastAsDecimal(txtQty.Text)),
    //                    new MyParameter("@Comments", "")
    //                );
    //            DataSet ds = new DataSet();
    //            if (Common.Execute_Procedures_IUD(ds))
    //            {

    //                BindRepeater();
    //            }
    //        }
    //    }
    //}
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString() == "O")
        {
            ImageButton btn = (ImageButton)sender;
            string[] parts = btn.CommandArgument.Split('$');
            HiddenField hfCompJobID = (HiddenField)btn.FindControl("hfCompJobID");
            Common.Execute_Procedures_Select_ByQuery("delete from dbo.VSL_VesselComponentJobMaster_Spares where vesselcode='" + VesselCode + "' and ForCompJobId=" + CompJobId + " and ComponentId=" + parts[0] + " and Office_Ship='" + parts[1] + "' and SpareId=" + parts[2]);
            BindRepeater();
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        divAddSpares.Visible = true;
        string Parents = ProjectCommon.getParentComponents_Chain(ComponentId);
        string sql = "SELECT ROW_NUMBER()over(order by SPARENAME )Sno,m.VESSELCODE,m.ComponentId,m.SpareId,m.Office_Ship,d.qty, " +
            " m.VESSELCODE+'#'+CONVERT(VARCHAR,m.COMPONENTID)+'#'+m.OFFICE_SHIP+'#'+CONVERT(VARCHAR,m.SPAREID) AS PKID ,SPARENAME + ' - ' + PARTNO AS SPARENAME " +
            " from VSL_VesselSpareMaster m left join VSL_VesselComponentJobMaster_Spares d on m.VesselCode=d.vesselcode and m.ComponentId=d.ComponentId and m.Office_Ship=d.Office_Ship and m.SpareId=d.SpareId " +
            " WHERE m.VesselCode = '" + VesselCode + "' AND m.ComponentId IN (" + ComponentId + "," + Parents + ") order by SPARENAME ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSpeareDetails_popup.DataSource = Dt;
        rptSpeareDetails_popup.DataBind();
    }
    
    public void BindRepeater()
    {
        string sql = "select row_number() over ( order by sparename) as sno,* from vw_JobSpareRequirement where vesselcode='" + VesselCode + "' and ForCompJobId=" + CompJobId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }

    //------------------------
    protected void btnAssign_PopupAddSpares_OnClick(object sender, EventArgs e)
    {
        if (!tblAddSpare.Visible)
            return;

            string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;

        string err = "no";
        int selRowcount = 0;
        foreach (RepeaterItem itm in rptSpeareDetails_popup.Items)
        {
            CheckBox chk = (CheckBox)itm.FindControl("chkSelect");
            if (chk.Checked)
            {
                selRowcount++;
                TextBox txtQty = (TextBox)itm.FindControl("txtQty");
                if (Common.CastAsInt32(txtQty.Text) <= 0)
                    err = "yes";
            }
        }
        if (selRowcount == 0)
        {
            lblMsgAddSpares.Text = "Please select any record to save.";
            return;
        }
        if (err == "yes")
        {
            lblMsgAddSpares.Text = "Please enter valid quantity.";
            return;
        }

        foreach (RepeaterItem itm in rptSpeareDetails_popup.Items)
        {
            CheckBox chk = (CheckBox)itm.FindControl("chkSelect");
            if (chk.Checked)
            {
                HiddenField hfdPKID = (HiddenField)itm.FindControl("hfdPKID");
                TextBox txtQty = (TextBox)itm.FindControl("txtQty");

                ProjectCommon.setSpareKeys(hfdPKID.Value, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

                Common.Set_Procedures("sp_InsertUpdateJobSpareRequirement");
                Common.Set_ParameterLength(8);
                Common.Set_Parameters(
                        new MyParameter("@VesselCode", VesselCode),
                        new MyParameter("@ForCompJobId", CompJobId),
                        new MyParameter("@ComponentId", _ComponentId),
                        new MyParameter("@Office_Ship", _Office_Ship),
                        new MyParameter("@SpareId", _SpareId),
                        new MyParameter("@Qty", Common.CastAsDecimal(txtQty.Text)),
                        new MyParameter("@Comments", ""),
                        new MyParameter("@AssignedBy", Session["UserType"].ToString())
                    );
                DataSet ds = new DataSet();
                Common.Execute_Procedures_IUD(ds);
            }
        }
        BindRepeater();
    }
    protected void btnClose_PopupAddSpares_OnClick(object sender, EventArgs e)
    {
        divAddSpares.Visible = false;
    }
}
