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
using System.IO;

public partial class Popup_AddUnPlanJob : System.Web.UI.Page
{
    public string ComponentCode
    {
        set { ViewState["CC"] = value; }
        get { return ViewState["CC"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public bool Modify
    {
        set { ViewState["_Modify"] = value; }
        get { return Convert.ToBoolean(ViewState["_Modify"]); }
    }
    public int Mode_SpareAddEdit
    {
        set { ViewState["Mode_SpareAddEdit"] = value; }
        get { return Common.CastAsInt32(ViewState["Mode_SpareAddEdit"]); }
    }

    public void BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlRank.DataSource = dtRanks;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("< SELECT >", ""));
    }
    public void BindDepartments()
    {
        DataTable dtDepartments = new DataTable();
        string strSQL = "SELECT DeptId,DeptName FROM DeptMaster ORDER BY DeptName";
        dtDepartments = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlDepartment.DataSource = dtDepartments;
        ddlDepartment.DataTextField = "DeptName";
        ddlDepartment.DataValueField = "DeptId";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("< SELECT >", ""));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        VesselCode = Request.QueryString["VSL"];
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            BindRanks();
            BindDepartments();

            Modify = (("" + Page.Request.QueryString["ModifySpare"]) == "Y");

            if (Request.QueryString["CC"] != null)
            {
                ComponentCode = Request.QueryString["CC"].ToString();
                btnSave2.Visible = false;
                trOthers.Visible = false;
                trAttachments.Visible = false;
                trSpare.Visible = Modify; // false;
                BindComponentDetails();
                BindSpares();
            }
            if (Request.QueryString["UPId"] != null)
            {
                hfdUPId.Value = Request.QueryString["UPId"].ToString();
                

                btnSave.Visible = false;
                BindOtherDetails();
                p1.Enabled = false; 
                trOthers.Visible = true;
                trAttachments.Visible = true;
                trSpare.Visible = true;
                BindAttachments();
                BindSpares();
                if (txtDoneDate.Text.Trim() != "")
                {
                    trAttachments.Visible = false;
                    btnSave2.Visible = false;
                    tblAddSpareSection.Visible = false;
                }
                if(Modify)
                    tblAddSpareSection.Visible = true;
            }
            if (Session["UserType"].ToString() == "O")
            {
                btnSave.Visible = false;
                btnSave2.Visible = false;
                btnAddSpare.Visible = false;
                imgAddSpare.Visible = false;
                btnSaveAttachment.Visible = false;

            }
        }
    }
    public void BindComponentDetails()
    {
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName,ClassEquip,CriticalEquip FROM ComponentMaster WHERE ComponentCode = '" + ComponentCode.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        lblCompCode.Text = ComponentCode.Trim();
        lblCompName.Text = dt.Rows[0]["ComponentName"].ToString().Trim();
        ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());
        if (dt.Rows[0]["ClassEquip"].ToString() == "True")
        {
            chkCritical.SelectedValue = "1";
        }
        if (dt.Rows[0]["CriticalEquip"].ToString() == "True")
        {
            chkCritical.SelectedValue = "2";
        }
    }
    public void BindOtherDetails()
    {
        string strSQL = "SELECT UP.VESSELCODE,UP.UPID,UP.COMPONENTID,CM.COMPONENTCODE,CM.COMPONENTNAME,CLASSEQUIP,CRITICALEQUIP, " +
                        "SHORTDESCR,LONGDESCR,DEPTID,ASSIGNEDTO,DUEDATE,DONEBY,DONEBY_CODE,DONEBY_NAME,DONEDATE,SERVICEREPORT,CONDITIONBEFORE,CONDITIONAFTER,RQNNO,RQNDATE " +
                        "FROM  " +
                        "VSL_UnPlannedJobs UP INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID=UP.COMPONENTID WHERE UP.VESSELCODE='" + VesselCode + "' AND UP.UPID=" + hfdUPId.Value;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            lblCompCode.Text = dt.Rows[0]["COMPONENTCODE"].ToString().Trim();
            lblCompName.Text = dt.Rows[0]["ComponentName"].ToString().Trim();
            ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());
            if (dt.Rows[0]["ClassEquip"].ToString() == "True")
            {
                chkCritical.SelectedValue = "1";
            }
            if (dt.Rows[0]["CriticalEquip"].ToString() == "True")
            {
                chkCritical.SelectedValue = "2";
            }
            //----------------------------
            ddlDepartment.SelectedValue = dt.Rows[0]["DeptId"].ToString().Trim();
            ddlRank.SelectedValue = dt.Rows[0]["AssignedTo"].ToString().Trim();
            txtDueDate.Text = Common.ToDateString(dt.Rows[0]["DueDate"].ToString().Trim());
            txtShort.Text = dt.Rows[0]["ShortDescr"].ToString().Trim();
            txtLong.Text = dt.Rows[0]["LongDescr"].ToString().Trim();
            txtDoneDate.Text = Common.ToDateString(dt.Rows[0]["DoneDate"].ToString().Trim());
            txtEmpNo.Text = dt.Rows[0]["DoneBy_Code"].ToString().Trim();
            txtEmpName.Text = dt.Rows[0]["DoneBy_Name"].ToString().Trim();
            txtServiceReport.Text = dt.Rows[0]["ServiceReport"].ToString().Trim();
            txtCondBefore.Text = dt.Rows[0]["ConditionBefore"].ToString().Trim();
            txtCondAfter.Text = dt.Rows[0]["ConditionAfter"].ToString().Trim();
            txtRqnNo.Text = dt.Rows[0]["RQNNO"].ToString().Trim();
            txtRqnDt.Text = Common.ToDateString(dt.Rows[0]["RQNdate"].ToString().Trim());
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        int LoginId = Common.CastAsInt32(Session["loginid"]);
        Common.Set_Procedures("sp_InsertUnPlannedJob");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@UPId", Common.CastAsInt32(hfdUPId.Value)),
            new MyParameter("@ComponentId", ComponentId),
            new MyParameter("@ShortDescr", txtShort.Text.Trim()),
            new MyParameter("@LongDescr", txtLong.Text.Trim()),
            new MyParameter("@DeptId", ddlDepartment.SelectedValue),
            new MyParameter("@AssignedTo", ddlRank.SelectedValue),
            new MyParameter("@DueDate", txtDueDate.Text),
            new MyParameter("@UpdatedBy", LoginId));
        DataSet dsComponentSpare = new DataSet();
        dsComponentSpare.Clear();
        if (Common.Execute_Procedures_IUD(dsComponentSpare))
        {
            hfdUPId.Value = dsComponentSpare.Tables[0].Rows[0][0].ToString();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Job Created Successfully.');refreshparent();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Unable to Create Job.');", true);
        }
    }
    protected void Save2_Click(object sender, EventArgs e)
    {
        int LoginId = Common.CastAsInt32(Session["loginid"]);
        Common.Set_Procedures("sp_UpdateUnPlannedJob");
        Common.Set_ParameterLength(17);
        object DoneDt = DBNull.Value;
        object RDt = DBNull.Value;
        DateTime dt, rdt;
        if (txtDoneDate.Text.Trim() != "")
        {
            if (DateTime.TryParse(txtDoneDate.Text, out dt))
            {
                DoneDt = dt;
            }
        }
        if (txtRqnDt.Text.Trim() != "")
        {
            if (DateTime.TryParse(txtRqnDt.Text, out rdt))
            {
                RDt = rdt;
            }
        }
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@UPId", Common.CastAsInt32(hfdUPId.Value)),
            new MyParameter("@ComponentId", ComponentId),
            new MyParameter("@ShortDescr", txtShort.Text.Trim()),
            new MyParameter("@LongDescr", txtLong.Text.Trim()),
            new MyParameter("@DeptId", ddlDepartment.SelectedValue),
            new MyParameter("@AssignedTo", ddlRank.SelectedValue),
            new MyParameter("@DueDate", txtDueDate.Text),
            new MyParameter("@UpdatedBy", LoginId),
            new MyParameter("@DoneBy_Code", txtEmpNo.Text),
            new MyParameter("@DoneBy_Name", txtEmpName.Text),
            new MyParameter("@DoneDate", DoneDt),
            new MyParameter("@ServiceReport", txtServiceReport.Text),
            new MyParameter("@ConditionBefore", txtCondBefore.Text),
            new MyParameter("@ConditionAfter", txtCondAfter.Text),
            new MyParameter("@RQNNo", txtRqnNo.Text),
            new MyParameter("@RQNDate", RDt));

        DataSet dsComponentSpare = new DataSet();
        dsComponentSpare.Clear();
        if (Common.Execute_Procedures_IUD(dsComponentSpare))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Job Created Successfully.');refreshparent();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Unable to Create Job.');", true);
        }
    }
    //------------- Spares
    protected void ddlSparesReqd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesReqd.SelectedValue == "1")
        {
            BindCompSpares();
            //BindSpares();
            tblSpares.Visible = true;
        }
        else
        {
            tblSpares.Visible = false;
        }

    }
    protected void ddlSparesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesList.SelectedIndex > 0)
        {
            txtQtyRob.Text = ProjectCommon.getROB(ddlSparesList.SelectedValue, DateTime.Today).ToString();
        }
        else
        {
            txtQtyRob.Text = "";
        }
    }
    public void BindSpares()
    {
        string strSQL = "SELECT VSM.VESSELCODE+'#'+CONVERT(VARCHAR,VSM.COMPONENTID)+'#'+VSM.OFFICE_SHIP+'#'+CONVERT(VARCHAR,VSM.SPAREID) AS RowId, VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,VSM.SpareName,VSM.Maker,VSM.PartNo,VDD.QtyCons,VDD.QtyRob FROM VSL_UnPlannedJobs_SpareDetails VDD " +
                        "INNER JOIN VSL_VesselSpareMaster VSM ON VSM.VesselCode = VDD.VesselCode AND VSM.ComponentId = VDD.ComponentId AND VSM.Office_Ship = VDD.Office_Ship AND VSM.SpareId = VDD.SpareId " +
                        "WHERE VDD.VESSELCODE='" + VesselCode + "' AND UPId=" + Common.CastAsInt32( hfdUPId.Value);
        DataTable dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptComponentSpares.DataSource = dtSpareDetails;
        rptComponentSpares.DataBind();

        if (dtSpareDetails.Rows.Count > 0)
        {
            ddlSparesReqd.SelectedValue = "1";
            ddlSparesReqd_SelectedIndexChanged(new object(),new EventArgs());
        }
    }
    protected void imgAddSpare_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + lblCompCode.Text.Trim() + "','" + VesselCode + "',' ');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindCompSpares();
    }
    protected void btnAddSpare_Click(object sender, EventArgs e)
    {
        if (ddlSparesList.SelectedIndex == 0)
        {
            ProjectCommon.ShowMessage("Please select a spare.");
            ddlSparesList.Focus();
            return;
        }
        if (txtQtyCon.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter consumed quantity.");
            txtQtyCon.Focus();
            return;
        }
        if (txtQtyRob.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter rob quantity.");
            txtQtyRob.Focus();
            return;
        }

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;

        ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.VSL_UnPlannedJobs SET DONEDATE='" + txtDoneDate.Text + "' WHERE VESSELCODE='" + _VesselCode + "' AND UPID=" + hfdUPId.Value);
        Common.Set_Procedures("sp_InsertUPPlanJobsSpares");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", _VesselCode),
            new MyParameter("@ComponentId", _ComponentId),
            new MyParameter("@Office_Ship", _Office_Ship),
            new MyParameter("@SpareId", _SpareId),
            new MyParameter("@UPId", hfdUPId.Value),
            new MyParameter("@QtyCons", txtQtyCon.Text),
            new MyParameter("@QtyRob", txtQtyRob.Text),
            new MyParameter("@IsEdit", Mode_SpareAddEdit));
        DataSet dsComponentSpare = new DataSet();
        int retval = 0;
        if (Common.Execute_Procedures_IUD(dsComponentSpare))
        {
            retval = Common.CastAsInt32(dsComponentSpare.Tables[0].Rows[0][0]);
            if (retval < 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Spare Already Exists.');", true);
            }
            else
            {
                BindSpares();
                ddlSparesList.SelectedIndex = 0;
                txtQtyCon.Text = "";
                txtQtyRob.Text = "";
                Mode_SpareAddEdit = 0;
                ddlSparesList.Enabled = true;
                btnAddSpare.Text = "Add Spare";
            }
        }

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        string PKId = ((ImageButton)sender).CommandArgument.Trim();
        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;

        ProjectCommon.setSpareKeys(PKId, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
        DataSet dsComponentSpare = new DataSet();
        Common.Set_Procedures("sp_DeleteUPPlanJobsSpares");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", _VesselCode),
            new MyParameter("@ComponentId", _ComponentId),
            new MyParameter("@Office_Ship", _Office_Ship),
            new MyParameter("@SpareId", _SpareId),
            new MyParameter("@UPId", hfdUPId.Value));

        if (Common.Execute_Procedures_IUD(dsComponentSpare))
        {
            BindSpares();
        }
    }
    protected void imgEditSpare_OnClick(object sender, EventArgs e)
    {
        Mode_SpareAddEdit = 1;
        ddlSparesList.Enabled = false;
        btnAddSpare.Text = "Update Spare";

        ImageButton btn = (ImageButton)sender;
        string PKId = btn.CommandArgument.Trim();
        Label lblQtyCons=(Label)btn.Parent.FindControl("lblQtyCons");
        Label lblQtyRob = (Label)btn.Parent.FindControl("lblQtyRob");
        
        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;

        ProjectCommon.setSpareKeys(PKId, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

        ddlSparesList.SelectedValue = PKId;
        txtQtyCon.Text = lblQtyCons.Text;
        txtQtyRob.Text = lblQtyRob.Text;
    }
    protected void btmCancelSpareAddSection_Click(object sender, EventArgs e)
    {
        ddlSparesList.SelectedIndex = 0;
        txtQtyCon.Text = "";
        txtQtyRob.Text = "";
        Mode_SpareAddEdit = 0;
        ddlSparesList.Enabled = true;
        btnAddSpare.Text = "Add Spare";
    }
    

    public void BindCompSpares()
    {
        ddlSparesList.Items.Clear();
        int ParentId = ProjectCommon.getParentComponentId(ComponentId);
        string strSpares = "SELECT VESSELCODE+'#'+CONVERT(VARCHAR,COMPONENTID)+'#'+OFFICE_SHIP+'#'+CONVERT(VARCHAR,SPAREID) AS PKID ,SPARENAME + ' - ' + PARTNO AS SPARENAME from VSL_VesselSpareMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId IN (" + ComponentId + "," + ParentId.ToString() + ") order by SPARENAME ";
        DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(strSpares);
        if (dtspares.Rows.Count > 0)
        {
            ddlSparesList.DataSource = dtspares;
            ddlSparesList.DataTextField = "SPARENAME";
            ddlSparesList.DataValueField = "PKID";
            ddlSparesList.DataBind();
        }
        ddlSparesList.Items.Insert(0, new ListItem("< SELECT >", "0"));
    }
    //------------- Attachments
    protected void BindAttachments()
    {
        string strSQL = "SELECT * FROM VSL_UnPlannedJobs_Attachments WHERE VESSELCODE='" + VesselCode + "' AND UPId = " + hfdUPId.Value + " AND STATUS='A'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
    }
    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.VSL_UnPlannedJobs SET DONEDATE='" + txtDoneDate.Text + "' WHERE VESSELCODE='" + VesselCode + "' AND UPID=" + hfdUPId.Value);
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;
            FileName = VesselCode + "_" + hfdUPId.Value + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            Common.Set_Procedures("sp_InsertUPAttachment");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@UPId", hfdUPId.Value),
                new MyParameter("@AttachmentText", txtAttachmentText.Text.Trim()),
                new MyParameter("@FileName", FileName)

                );

            DataSet dsAttachment = new DataSet();
            dsAttachment.Clear();
            Boolean result;
            result = Common.Execute_Procedures_IUD(dsAttachment);
            if (result)
            {

                string path = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                flAttachDocs.SaveAs(path + FileName);
                trAttachments.Visible = true;
                BindAttachments();
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "Document uploaded successfully.", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "Unable to upload document.", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "Please select a file to upload.", true);
            img.Focus();
            return;
        }
    }
    protected void DeleteAttachment_OnClick(object sender, EventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        string VesselCode = imgbtn.CssClass;
        string TableId = imgbtn.CommandArgument;
        Common.Set_Procedures("sp_DeleteUPAttachment");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@TableId", TableId),
            new MyParameter("@Mode", "P")
            );
        DataSet ds1 = new DataSet();
        Boolean res = Common.Execute_Procedures_IUD(ds1);
        if (res)
        {
            string FileName = ds1.Tables[0].Rows[0][0].ToString();
            FileName = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)) + FileName);
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "Attachment deleted successfully.", false);
            BindAttachments();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "Unable to remove attachment.Error :" + Common.getLastError(), true);
        }
    }


}
