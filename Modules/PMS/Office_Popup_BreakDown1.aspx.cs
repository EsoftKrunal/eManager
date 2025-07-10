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
using System.Drawing;
using System.Drawing.Imaging;

public partial class Office_Popup_BreakDown1 : System.Web.UI.Page
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
    public string vesselCode
    {
        set { ViewState["vesselCode"] = value; }
        get { return Convert.ToString(ViewState["vesselCode"]); }

    }
    public static DataTable dtSpareDetails;

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //btnAddRemarks.Text = "Office Remarks";
        if (!Page.IsPostBack)
        {
            Session["SparesAdded"] = null;
            dtSpareDetails = null;
            if (Request.QueryString["DN"] != null)
            {
                lblNo.Text = Request.QueryString["DN"].ToString().Trim();
                vesselCode = lblNo.Text.Split('/').GetValue(0).ToString().Trim();
                BindBreakDownDetails();
                BindAttachment();
                btnPrint.Visible = true;
            }            
        }
    }
    public void OfficeRemark()
    {
        //string sql = "SELECT row_number() over(order by DefectRemarkId) as Sno , DefectRemarkId,DefectNo,Remarks,[FileName],(EnteredBy + '/ ' + REPLACE(CONVERT(VARCHAR(11),EnteredOn,106), ' ', '-')) AS EnteredByON  FROM VSL_DefectRemarks WHERE VesselCode = '" + vesselCode.Trim() + "' AND DefectNo = '" + lblNo.Text + "' ";
        //DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //rptOfficeComments.DataSource = Dt;
        //rptOfficeComments.DataBind();
    }
    public void BindDetails()
    {
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName,ClassEquip,CriticalEquip FROM ComponentMaster WHERE ComponentCode = '" + ComponentCode.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        lblCompCode.Text = ComponentCode.Trim();
        lblCompName.Text = dt.Rows[0]["ComponentName"].ToString().Trim();
        ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());

        if (dt.Rows[0]["ClassEquip"].ToString() == "True")
        {
            lblCStatus.Text = "Class Equipment";
        }
        if (dt.Rows[0]["CriticalEquip"].ToString() == "True")
        {
            lblCStatus.Text += " | Critical Equipment";
        }
    }
    public void BindBreakDownDetails()
    {
        string strSQL = "SELECT VDM.VesselCode,VDM.ComponentId,CM.ComponentCode,CM.ComponentName,CM.ClassEquip,CM.CriticalEquip,BreakDownNo,CASE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') END  AS ReportDt , CASE REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE  REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') END AS TargetDt, CASE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') END AS CompletionDt,Vessel,Spares,ShoreAssist,Drydock,Guarentee,DefectDetails,RepairsDetails,SparesOnBoard,RqnNo,CASE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') END AS  RqnDate,SparesOrdered,SparesRequired,Supdt,ChiefOfficer,ChiefEngg,VDM.[Status],VDM.CompStatus,CJM.DescrSh, " +
                        "CASE WHEN (VDM.TargetDt < getdate() AND VDM.CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [DueStatus] " +
                        "FROM VSL_BreakDownMaster VDM " +
                        "LEFT JOIN ComponentMaster CM ON CM.ComponentId = VDM.ComponentId " +
                        "LEFT JOIN VSL_VesselJobUpdateHistory JH ON VDM.VesselCode = JH.VesselCode AND VDM.ComponentId = JH.ComponentId AND VDM.HistoryId = JH.HistoryId " +
                        "LEFT JOIN ComponentsJobMapping CJM ON CJM.ComponentId = JH.ComponentId AND CJM.CompJobId = JH.CompJobId " +
                        "WHERE BreakDownNo = '" + lblNo.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());
            lblCompCode.Text = dt.Rows[0]["ComponentCode"].ToString();
            lblCompName.Text = dt.Rows[0]["ComponentName"].ToString();

            lblCStatus.Text = (dt.Rows[0]["ClassEquip"].ToString() == "True") ? "Class Equipment" : "";
            lblCStatus.Text += (dt.Rows[0]["CriticalEquip"].ToString() == "True") ? " | Critical Equipment" : "";

            txtReportDt.Text = dt.Rows[0]["ReportDt"].ToString();
            txtTargetDt.Text = dt.Rows[0]["TargetDt"].ToString();
            txtCompletionDt.Text = dt.Rows[0]["CompletionDt"].ToString();
            lblCompDt.Text = txtCompletionDt.Text;
            chkVessel.Checked = (dt.Rows[0]["Vessel"].ToString() == "Y");
            chkSpares.Checked = (dt.Rows[0]["Spares"].ToString() == "Y");
            chkShAssist.Checked = (dt.Rows[0]["ShoreAssist"].ToString() == "Y");
            ChkDrydock.Checked = (dt.Rows[0]["Drydock"].ToString() == "Y");
            chkGuarantee.Checked = (dt.Rows[0]["Guarentee"].ToString() == "Y");
            txtDefectdetails.Text = dt.Rows[0]["DefectDetails"].ToString();
            txtRepairsCarriedout.Text = dt.Rows[0]["RepairsDetails"].ToString();
            lblCompStatus.Text = (dt.Rows[0]["CompStatus"].ToString() == "W" ? "Equipment Working" : "Equipment Not Working");
            lblSOB.Text = (dt.Rows[0]["SparesOnBoard"].ToString() == "Y" ? "Yes" : "No");
            txtRqnNo.Text = dt.Rows[0]["RqnNo"].ToString();
            txtRqnDt.Text = dt.Rows[0]["RqnDate"].ToString();

            lblSparesReqd.Text = (dt.Rows[0]["SparesRequired"].ToString() == "Y" ? "Yes" : "No");
            txtSupdt.Text = dt.Rows[0]["Supdt"].ToString();
            txtCE.Text = dt.Rows[0]["ChiefEngg"].ToString();
            txtCO.Text = dt.Rows[0]["ChiefOfficer"].ToString();
            lblJob.Text = dt.Rows[0]["DescrSh"].ToString();

            //if (dt.Rows[0]["DueStatus"].ToString() == "OD")
            //{
            //    txtTargetDt.Attributes.Add("class", "highlightrow");
            //}

            BindSpares();
            ShowClosureRead();
            ShowOfficeClosureDetails();
        }
    }
  
    public void BindSpares()
    {
        string strSQL = "SELECT VSM.VESSELCODE+'#'+CONVERT(VARCHAR,VSM.COMPONENTID)+'#'+VSM.OFFICE_SHIP+'#'+CONVERT(VARCHAR,VSM.SPAREID) AS RowId, VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,VSM.SpareName,VSM.Maker,VSM.PartNo,VDD.QtyCons,VDD.QtyRob FROM VSL_BreakDownSpareDetails VDD " +
                        "INNER JOIN VSL_VesselSpareMaster VSM ON VSM.VesselCode = VDD.VesselCode AND VSM.ComponentId = VDD.ComponentId AND VSM.Office_Ship = VDD.Office_Ship AND VSM.SpareId = VDD.SpareId " +
                        "WHERE VDD.BreakDownNo = '" + lblNo.Text.Trim() + "' ";
        dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptComponentSpares.DataSource = dtSpareDetails;
        rptComponentSpares.DataBind();
    }

    public void ShowJobDetails()
    {
        string strSQL = "SELECT DescrSh FROM ComponentsJobMapping WHERE CompJobId =" + Request.QueryString["JID"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        lblJob.Text = dt.Rows[0]["DescrSh"].ToString();
    }

    public void ShowOfficeClosureDetails()
    {
        string sql = "SELECT Classification,RCARequired,RCANo,BreakDownNo,Remarks FROM VSL_BreakDownRemarks WHERE VesselCode = '" + vesselCode.Trim() + "' AND BreakDownNo = '" + lblNo.Text + "' ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            btnAddRemarks.Visible = false;
            pnlofficecomments.Visible = true;
            int classid = Common.CastAsInt32(Dt.Rows[0]["Classification"]);
            switch (classid)
            {
                case 1:
                    lblclassification.Text = "Minor";
                    break;
                case 2:
                    lblclassification.Text = "Major";
                    break;
                case 3:
                    lblclassification.Text = "Severe";
                    break;
                default:
                    break;
            }

            lblRCARequried.Text = (Dt.Rows[0]["RCARequired"].ToString() == "True") ? "Yes" : "No";
            lblRemarks.Text = Dt.Rows[0]["Remarks"].ToString();
            lblRCANo.Text = Dt.Rows[0]["RCANo"].ToString();
        }
    }

    public DataTable SparesDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowId");
        dt.Columns.Add("VesselCode");
        dt.Columns.Add("ComponentId");
        dt.Columns.Add("Office_Ship");
        dt.Columns.Add("SpareId");
        dt.Columns.Add("SpareName");
        dt.Columns.Add("Maker");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("QtyCons");
        dt.Columns.Add("QtyRob");

        DataRow dr = dt.NewRow();
        dr["RowId"] = "";
        dr["VesselCode"] = "";
        dr["ComponentId"] = "";
        dr["Office_Ship"] = "";
        dr["SpareId"] = "";
        dr["SpareName"] = "";
        dr["Maker"] = "";
        dr["PartNo"] = "";
        dr["QtyCons"] = "";
        dr["QtyRob"] = "";

        dt.Rows.Add(dr);
        return dt;
    }
    public void dataForUpdate()
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("DefectNo");
        dtData.Columns.Add("ReportDt");
        dtData.Columns.Add("TargetDt");
        dtData.Columns.Add("CompletionDt");
        dtData.Columns.Add("Vessel");
        dtData.Columns.Add("Spares");
        dtData.Columns.Add("ShoreAssist");
        dtData.Columns.Add("Drydock");
        dtData.Columns.Add("Guarentee");
        dtData.Columns.Add("DefectDetails");
        dtData.Columns.Add("RepairsDetails");
        dtData.Columns.Add("SparesOnBoard");
        dtData.Columns.Add("RqnNo");
        dtData.Columns.Add("RqnDate");
        dtData.Columns.Add("SparesRequired");
        dtData.Columns.Add("Supdt");
        dtData.Columns.Add("ChiefOfficer");
        dtData.Columns.Add("ChiefEngg");

        DataRow dr = dtData.NewRow();
        dr["DefectNo"] = lblNo.Text.Trim();
        dr["ReportDt"] = txtReportDt.Text.Trim();
        dr["TargetDt"] = txtTargetDt.Text.Trim();
        dr["CompletionDt"] = null;
        dr["Vessel"] = chkVessel.Checked ? "Y" : "N";
        dr["Spares"] = chkSpares.Checked ? "Y" : "N";
        dr["ShoreAssist"] = chkShAssist.Checked ? "Y" : "N";
        dr["Drydock"] = ChkDrydock.Checked ? "Y" : "N";
        dr["Guarentee"] = chkGuarantee.Checked ? "Y" : "N";
        dr["DefectDetails"] = txtDefectdetails.Text.Trim();
        dr["RepairsDetails"] = txtRepairsCarriedout.Text.Trim();
        dr["SparesOnBoard"] = lblSOB.Text == "Yes" ? "Y" : "N";
        dr["RqnNo"] = txtRqnNo.Text.Trim();
        dr["RqnDate"] = txtRqnDt.Text.Trim();
        dr["SparesRequired"] = lblSparesReqd.Text == "Yes" ? "Y" : "N";
        dr["Supdt"] = txtSupdt.Text.Trim();
        dr["ChiefOfficer"] = txtCO.Text.Trim();
        dr["ChiefEngg"] = txtCE.Text.Trim();

        dtData.Rows.Add(dr);

        Session.Add("defectsData", dtData);
    }
    protected void ddlSOB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSOB.SelectedValue == "1")
        //{
        //    txtRqnNo.Attributes.Add("required", "yes");
        //    txtRqnDt.Attributes.Add("required", "yes");
        //}
        //else
        //{
        //    txtRqnNo.Attributes.Remove("required");
        //    txtRqnDt.Attributes.Remove("required");
        //}
    }
    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int DefectRemarkId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openattachmentwindow('" + DefectRemarkId + "', '" + vesselCode.Trim() + "');", true);

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('Reports/Office_BreakdownDefectReport.aspx?DN=" + Request.QueryString["DN"].ToString() + "', '', '');", true);

        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "OpenReport(" + Request.QueryString["DN"].ToString() + ")", true);
    }

    #region ATTACHMENT
    private void BindAttachment()
    {
        string strSQL = "SELECT AttachmentId,BreakDownNo,AttachmentText,[FileName] FROM VSL_BreakDown_Attachments WHERE VesselCode = '" + vesselCode + "' AND BreakDownNo = '" + lblNo.Text.Trim() + "' AND ISNULL([Status],'A') <> 'D'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
            //dvAttachment.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
    }

    #endregion
    protected void ShowClosureRead()
    {
        trClosure.Visible = true;
        lblCompDt.Visible = true;
        txtCompletionDt.Visible = false;
    }
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvOfficeComments.Visible = false;
    }
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        string SQL = "SELECT * FROM VSL_BreakDownMaster WHERE BreakDownNo = '" + lblNo.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            dvOfficeComments.Visible = true;
        }
    }
    protected void btnOfficeClosure_Click(object sender, EventArgs e)
    {
        if (ddlclassification.SelectedIndex < 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsa", "alert('Please select breakdown classification.');", true);
            return;
        }
        else
        {
            Common.Set_Procedures("sp_InsertBreakDownRemarks");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
                    new MyParameter("@VesselCode", vesselCode.Trim()),
                    new MyParameter("@DefectNo", lblNo.Text.Trim()),
                    new MyParameter("@Classification", ddlclassification.SelectedValue),
                    new MyParameter("@RCARequired", chkrequried.Checked?"1":"0"),
                    new MyParameter("@RCANo", txtremarks.Text.Trim()),
                    new MyParameter("@Remarks", txtremarks.Text.Trim()),
                    new MyParameter("@FileName", ""),
                    new MyParameter("@EnteredBy", Session["FullName"].ToString())
                );
            DataSet ds = new DataSet();
            Boolean Res;
            Res = Common.Execute_Procedures_IUD(ds);
            if (Res)
            {
                ShowOfficeClosureDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsa", "alert('Closure done sucessfully.');", true);
                dvOfficeComments.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsa", "alert('Closure not done.');", true);
            }
        }
    }
    protected void btntab1_OnClick(object sender, EventArgs e)
    {
        ShowTab(1);
    }
    protected void btntab2_OnClick(object sender, EventArgs e)
    {
        ShowTab(2);
    }
    protected void btntab3_OnClick(object sender, EventArgs e)
    {
        ShowTab(3);
    }

    public void ShowTab(int i)
    {
        divTab1.Visible = false; divTab2.Visible = false; divTab3.Visible = false;
        btntab1.CssClass = "btn";
        btntab2.CssClass = "btn";
        btntab3.CssClass = "btn";
        if (i == 1)
        {
            divTab1.Visible = true;
            btntab1.CssClass = "btnsel";
        }
        if (i == 2)
        {
            divTab2.Visible = true;
            btntab2.CssClass = "btnsel";
        }
        if (i == 3)
        {
            divTab3.Visible = true;
            btntab3.CssClass = "btnsel";
        }

    }

    protected void btn_Reduce_Image_Click(object sender, EventArgs e)
    {
        int jpegQuality = 50;

        string strSQL = "SELECT DefectRemarkId,[FileName],[File] FROM VSL_DefectRemarks WHERE Vesselcode = '" + vesselCode + "' AND DefectNo = '" + lblNo.Text.Trim() + "' ";
        DataTable dtRemarkDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);

        foreach(DataRow dr in dtRemarkDetails.Rows)
        {
            int DefectRemarkId = Common.CastAsInt32(dr["DefectRemarkId"].ToString());
            string filename=dr["FileName"].ToString().ToLower();
            if (filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
            {
                byte[] buff = (byte[])dr["File"];
                using (MemoryStream ms = new MemoryStream(buff))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    int h = img.Height;
                    int w = img.Width;

                    double h1 = img.Height;
                    double w1 = img.Width;

                    //------------------ w 800 X h 600
                    if (w > h)
                    {
                        w1 = 600;
                        h1 = h * (w1 / w);
                    }
                    else
                    {
                        h1 = 800;
                        w1 = w * (h1 / h);
                    }

                    //------------------

                    System.Drawing.Bitmap resizedImg = new System.Drawing.Bitmap((int)w1, (int)h1);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(resizedImg))
                    {
                        g.DrawImage(img, 0, 0, (int)w1, (int)h1);
                    }

                    //resizedImg.Save(@"c:\temp\2.jpg"); -- size change only

                    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;

                    foreach (ImageCodecInfo codec in codecs)
                    {
                        if (codec.MimeType == "image/jpeg")
                            ici = codec;
                    }
                    // Create an Encoder object based on the GUID
                    // for the Quality parameter category.
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);

                    resizedImg.Save(@"c:\temp\3.jpg", ici, encoderParameters);

                    Byte[] outputBytes;
                    using (MemoryStream mm = new MemoryStream())
                    {
                        resizedImg.Save(mm, ici, encoderParameters);
                        outputBytes = mm.ToArray();                        
                    }

                   

                    //---------------------------- UPDATE BACK TO DATABASE

                    Common.Set_Procedures("sp_UpdateDefectRemarkAttachment");
                    Common.Set_ParameterLength(4);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", vesselCode.Trim()),
                        new MyParameter("@DefectNo", lblNo.Text.Trim()),
                        new MyParameter("@DefectRemarkId", DefectRemarkId),
                        new MyParameter("@FileContent", outputBytes)
                        );
                    DataSet ds1 = new DataSet();
                    Boolean res = Common.Execute_Procedures_IUD(ds1);

                    if (res)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Attachment size changed.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Attachment size not changed.');", true);
                    }   

                }
            }
        }
    }
}

