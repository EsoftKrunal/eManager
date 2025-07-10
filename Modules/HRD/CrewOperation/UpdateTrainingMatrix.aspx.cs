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
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;


public partial class UpdateTrainingMatrix : System.Web.UI.Page
{
    //Authority Auth;
    public AuthenticationManager Auth;
    public static bool gEditMode;
    public static bool gDeleteMode;
    protected int EditTrainingId
    {
        get { return Common.CastAsInt32(ViewState["TId"]); }
        set {ViewState["TId"]=value ;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessTraining.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 11);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training.aspx");
        }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        //Auth = OBJ.Authority;
        Auth = new AuthenticationManager(11, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        divAddTraining.Visible = Auth.IsAdd;
        btnEdit.Visible = Auth.IsUpdate;
        btnDelete.Visible = Auth.IsDelete;
        btnPrint.Visible = Auth.IsPrint;

        if (!Page.IsPostBack)
        {
            BindTraining();
            BindCategory();
            ddlCat_OnSelectedIndexChanged(sender, e); 
        }
    }
    // ------------ Events
    protected void ddlTraining_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        gEditMode = false;
        gDeleteMode= false;
        ShowMatrix();
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        
        ExportToPDF();
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        gEditMode = true;
        gDeleteMode = false;
        ShowMatrix();
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        if (btnDelete.Text.Trim().ToLower() == "delete")
        {
            gDeleteMode = true;    
        }
        if (btnDelete.Text.Trim().ToLower() == "cancel")
        {
            gDeleteMode = false;
        }
        gEditMode = false;
        ShowMatrix();
        if (gDeleteMode)
        {
            btnDelete.Text = "Cancel";
           // gDeleteMode = false;
        }
        if (!gDeleteMode && btnDelete.Text.Trim().ToLower() == "cancel")
        {
            btnDelete.Text = "Delete";
        }
    }
    

    // ------------ Function
    public void BindTraining()
    {
        string sql = "Select TrainingMatrixID,TrainingMatrixName from TrainingMatrixMaster";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlTraining.DataSource = dt;
        ddlTraining.DataTextField = "TrainingMatrixName";
        ddlTraining.DataValueField = "TrainingMatrixID";
        ddlTraining.DataBind();
        ddlTraining.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }
    public void BindCategory()
    {
        string sql = "select * from TRAININGTYPE ORDER BY TRAININGTYPENAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlCat.DataSource = dt;
        ddlCat.DataTextField = "TRAININGTYPENAME";
        ddlCat.DataValueField = "TRAININGTYPEID";
        ddlCat.DataBind();
        ddlCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Category >", "0"));  
    }
    protected void ddlCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select * from TRAINING WHERE TYPEOFTRAINING=" + ddlCat.SelectedValue + " ORDER BY TRAININGNAME";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlATraining.DataSource = dt;
        ddlATraining.DataTextField = "TRAININGNAME";
        ddlATraining.DataValueField = "TRAININGID";
        ddlATraining.DataBind();
        ddlATraining.Items.Insert(0,new System.Web.UI.WebControls.ListItem("< Training >", "0"));
        ddlATraining.Focus(); 
    }
    protected void SelModeChanged(object sender, EventArgs e)
    {
        int TrainingId = Common.CastAsInt32(hfdMTId.Value);
        int RankId = Common.CastAsInt32(hfdMRId.Value);
        int Value = 1;//(radNA.Checked)?0:(radMan.Checked)?1:2;

        string qry = "SELECT * FROM TrainingMatrixRankDetails where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString() + " and RankId=" + RankId.ToString();  

        if(Common.Execute_Procedures_Select_ByQueryCMS(qry).Rows.Count >0 )
            qry = "UPDATE TrainingMatrixRankDetails SET APPLYTYPE=" + Value.ToString() + " where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString() + " and RankId=" + RankId.ToString();  
        else
            qry = "INSERT INTO TrainingMatrixRankDetails VALUES(" + ddlTraining.SelectedValue + "," + TrainingId.ToString() + "," + RankId.ToString() + "," + Value.ToString() + ")";  
        Common.Execute_Procedures_Select_ByQueryCMS(qry);
        //radMan.Checked = false;
        //radRec.Checked = false;
        //radNA.Checked = false;
        ShowMatrix();
    }
    protected void SelModeCancelled(object sender, EventArgs e)
    {
        int TrainingId = Common.CastAsInt32(hfdMTId.Value);
        int RankId = Common.CastAsInt32(hfdMRId.Value);
        int Value = 0;// (radNA.Checked) ? 0 : (radMan.Checked) ? 1 : 2;

        string qry = "SELECT * FROM TrainingMatrixRankDetails where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString() + " and RankId=" + RankId.ToString();

        if (Common.Execute_Procedures_Select_ByQueryCMS(qry).Rows.Count > 0)
            qry = "UPDATE TrainingMatrixRankDetails SET APPLYTYPE=" + Value.ToString() + " where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString() + " and RankId=" + RankId.ToString();
        else
            qry = "INSERT INTO TrainingMatrixRankDetails VALUES(" + ddlTraining.SelectedValue + "," + TrainingId.ToString() + "," + RankId.ToString() + "," + Value.ToString() + ")";
        Common.Execute_Procedures_Select_ByQueryCMS(qry);
        //radMan.Checked = false;
        //radRec.Checked = false;
        //radNA.Checked = false;
        ShowMatrix();
    }
    protected void btnUpdateSch_Click(object sender, EventArgs e)
    {
        int TrainingId = Common.CastAsInt32(hfdTrainingId.Value);
        try
        {
            string qry = "update TrainingMatrixDetails set ScheduleCount=" + Common.CastAsInt32(txtSc1.Text) + " ,ScheduleType='" + ddlSchedule1.SelectedValue + "' where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString();
            Common.Execute_Procedures_Select_ByQueryCMS(qry); 
            lblMessTraining.ForeColor = System.Drawing.Color.Green;
            ShowMatrix();
            lblMessTraining.Text = "Schedule Updated Successfully.";
        }
        catch (Exception ex)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Green;
            lblMessTraining.Text = "Error Updating Schedule. Error : " + ex.Message;
        }
    }
    protected void ShowMatrix()
    {
        //Get All Trainings
        string sqlTrng = "Select T.TRAININGNAME,T.TRAININGID,TM.SCHEDULECOUNT,TM.ScheduleType,TT.TrainingTypeName from TrainingMatrixDetails TM inner join Training T on T.TRAININGID=TM.TRAININGID INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING where TrainingMatrixID=" + ddlTraining.SelectedValue + " order by TrainingName";
        DataTable dtTrng = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

        string sqlTotRank = "select * from rankgroup WHERE STATUSID='A' ORDER BY RANKGROUPID";
        DataTable DtTotRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTotRank);
        StringBuilder TBL = new StringBuilder();

        string sqlTrngRank = "Select * from TrainingMatrixRankDetails where TrainingMatrixID=" + ddlTraining.SelectedValue + "";
        DataTable DtTrngRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrngRank);

        TBL.Append("<table border='1' cellspacing='0' cellpadding='2' style='border-collapse:collapse;' width='100%'>");
        // Loop for TRs
        //TBL.Append("<tr><td></td><td class='hd1'>Training Name</td><td class='hd1'>Similier Trainings</td><td class='hd1'>Schedule</td>");
        TBL.Append("<tr class= 'headerstylegrid'><td></td><td class='hd1'>Training Name</td><td class='hd1'>Schedule</td>");
        foreach (DataRow drtd in DtTotRanks.Rows)
        {
            TBL.Append("<td class='hd1'>" + drtd["RANKGROUPNAME"].ToString() + "</td>");
        }
        //me
        TBL.Append("<td class='hd1' width='12px'></td>");
        TBL.Append("</tr>");
        foreach (DataRow dr in dtTrng.Rows)
        {

            int TrainingID = Common.CastAsInt32(dr["TrainingId"]);
            string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                      "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + TrainingID.ToString() + ") " +
                                      "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                      "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + TrainingID.ToString() + ")";
            DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
            SimilerTrainings = "";
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainings += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
            }
            if (SimilerTrainings != "")
                SimilerTrainings = SimilerTrainings.Substring(1);
            SimilerTrainings = SimilerTrainings.Replace(",", "<span style='color:red'> OR </span><br/>");
 
            bool EditMode = gEditMode;// (TrainingID == EditTrainingId);
            bool DeleteMode = gDeleteMode;

            string EditStr = ((EditMode) ?"onclick='goEdit(" + TrainingID.ToString() + ");'":"");
            string DelStr = ((DeleteMode) ? "onclick='DeleteTraining(" + TrainingID.ToString() + ");'" : "");


            TBL.Append("<tr><td><a " + EditStr + DelStr + " href='#'><img src='../Images/delete1.gif' style='width:12px;cursor:pointer;display:" + ((DeleteMode)?"block":"none") + "' title='Remove This Training.'/></a></td>");
            //TBL.Append("<td class='hd'>" + dr["TrainingName"].ToString() + "</td><td class='hd'>" + SimilerTrainings + "</td>");
            TBL.Append("<td class='hd'>" + dr["TrainingName"].ToString() + " [<i style='color:Blue;'>" + dr["TrainingTypeName"].ToString() + "</i>]" + ((SimilerTrainings.Trim() != "") ? "<span style='color:red'> OR </span><br/>" + SimilerTrainings : "") + "</td>");

            EditStr = ((EditMode) ? "onclick='showUpdate(" + TrainingID.ToString() + ");'" : ""); 
            TBL.Append("<td class='hd' style='text-align:center;' title='Update Schedule.' " + EditStr + "><a " + ((EditMode) ? "href='#'" : "") + ">" + dr["ScheduleCount"].ToString() + "-" + dr["ScheduleType"].ToString() + "</a></td>");
            //TBL.Append("<tr><td class='hd'>" + dr["TrainingTypeName"].ToString() + "</td><td class='hd'>" + dr["TrainingName"].ToString() + "</td>");
            // Loop  for td
            foreach (DataRow drtd in DtTotRanks.Rows)
            {
                int RankID = Common.CastAsInt32(drtd["RankGroupId"]);
                DataRow[] drs = DtTrngRanks.Select("TrainingId=" + TrainingID.ToString() + " AND RankId=" + RankID.ToString());
                EditStr = ((EditMode) ? "onclick='ShowMan(this," + TrainingID.ToString() + "," + RankID.ToString() + ");'" : "onclick='OpenViewTrainingMatricPopUp(" + TrainingID.ToString() + "," + RankID + ")' ");
                if (drs.Length > 0)
                {
                    string AppType = drs[0]["ApplyType"].ToString();
                    string BgColor = "White";
                    if (AppType == "1" || AppType == "2") { AppType = "M"; BgColor = "#FF8C00"; }
                    //else if (AppType == "2") { AppType = "R";BgColor="#BDB76B"; }
                    else AppType = "&nbsp;";
                    TBL.Append("<td style='background-color:" + BgColor + " ;text-align:center;cursor:pointer;' " + EditStr + ">&nbsp;</td>");
                }
                else
                    TBL.Append("<td " + EditStr + ">&nbsp;</td>");
            }
            TBL.Append("</tr>");
        }
        TBL.Append("</table>");
        litTreaining.Text = TBL.ToString();    
    }
    //protected void ddlATraining_OnSelectedIndexChanged(object sender, EventArgs e)
    //{ 
    //    DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("Select TRAININGNAME from CompanyTraining MT WHERE MT.TRAININGID IN (SELECT MTMTRAININGID FROM TRAINING T WHERE T.TRAININGID=" + ddlATraining.SelectedValue + ")");
    //    if(dt.Rows.Count >0)
    //        lblMTraining.Text=" MTM < " + dt.Rows[0][0].ToString() + " >";   
    //    else
    //        lblMTraining.Text=""; 
    //}
    protected void btnAddTraining_Click(object sender, EventArgs e)
    {
        if (ddlTraining.SelectedIndex <= 0)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Red;
            lblMessTraining.Text = "Please Select a Training Matrix First.";
            ddlTraining.Focus(); 
            return;
        }
        if (ddlCat.SelectedIndex <= 0)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Red;
            lblMessTraining.Text = "Please Select Category.";
            ddlCat.Focus();
            return;
        }
        if (ddlATraining.SelectedIndex <= 0)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Red;
            lblMessTraining.Text = "Please Select Training.";
            ddlATraining.Focus();
            return;
        }
        if (Common.CastAsInt32(txtSc.Text.Trim())<=0)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Red;
            lblMessTraining.Text = "Please Enter a Valid Schedule.";
            txtSc.Focus();
            return;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select * from TrainingMatrixDetails where TrainingId=" + ddlATraining.SelectedValue + " and TrainingMatrixId=" + ddlTraining.SelectedValue);
        if (dt.Rows.Count <= 0)
        {
            try
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO TrainingMatrixDetails VALUES(" + ddlTraining.SelectedValue + "," + ddlATraining.SelectedValue + "," + Common.CastAsInt32(txtSc.Text) + ",'" + ddlSchedule.SelectedValue + "')");
                lblMessTraining.ForeColor = System.Drawing.Color.Green;
                ShowMatrix();
                lblMessTraining.Text = "Training Added Successfully."; 
            }
            catch (Exception ex)
            {
                lblMessTraining.ForeColor = System.Drawing.Color.Green;
                lblMessTraining.Text = "Error Adding Training. Error : " + ex.Message;             
            }
        }
        else
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Red;
            lblMessTraining.Text = "Training Already Added."; 
        }
        

    }
    protected void btnEditTraining_Click(object sender, EventArgs e)
    {
        int TrainingId = Common.CastAsInt32(hfdEditId.Value);
        EditTrainingId = TrainingId;
        ShowMatrix();
    }
    protected void btnDelTraining_Click(object sender, EventArgs e)
    {
        int TrainingId = Common.CastAsInt32(hfdDelId.Value);
        try
        {
            string qry = "DELETE FROM TrainingMatrixRankDetails where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString() + 
                         ";DELETE FROM TrainingMatrixDetails where TrainingMatrixId=" + ddlTraining.SelectedValue + " and TrainingId=" + TrainingId.ToString();
            Common.Execute_Procedures_Select_ByQueryCMS(qry);
            lblMessTraining.ForeColor = System.Drawing.Color.Green;
            ShowMatrix();
            lblMessTraining.Text = "Training Deleted Successfully.";
        }
        catch (Exception ex)
        {
            lblMessTraining.ForeColor = System.Drawing.Color.Green;
            lblMessTraining.Text = "Error Deleting Training. Error : " + ex.Message;
        }
    }

    private void ExportToPDF()
    {
        try
        {
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate(), 10, 10, 10, 10);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("eMANAGER");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\Logo.png"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("\n\nENERGIOS MARITIME PVT. LTD. " + "", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            Phrase p3 = new Phrase();
            p3.Add(new Phrase("Training Matrix For - " + ddlTraining.SelectedItem.Text + "\n\n", FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD)));
            Cell c3 = new Cell(p3);
            c3.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c3);

            //HeaderFooter header = new HeaderFooter(new Phrase(" \n REPORT NO :" + dtMotorDetails.Rows[0]["ReportNo"].ToString()), false);
            //document.Header = header;

            //header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);

            // ================================= FRONT PAGE =================================

            //Get All Trainings
            string sqlTrng = "Select T.TRAININGNAME,T.TRAININGID,MT.TRAININGID,TM.ScheduleType,TM.ScheduleCount,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from TrainingMatrixDetails TM inner join Training T on T.TRAININGID=TM.TRAININGID inner join TrainingType TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING LEFT JOIN CompanyTraining MT ON MT.TRAININGID=T.MTMTRAININGID where TrainingMatrixID=" + ddlTraining.SelectedValue + " order by TrainingName";
            //,MT.TRAININGNAME AS MTMTRAININGNAME,TM.SCHEDULECOUNT
            DataTable dtTrng = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

            string sqlTotRank = "select * from rankgroup WHERE STATUSID='A' ORDER BY RANKGROUPID";
            DataTable DtTotRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTotRank);
            StringBuilder TBL = new StringBuilder();

            string sqlTrngRank = "Select * from TrainingMatrixRankDetails where TrainingMatrixID=" + ddlTraining.SelectedValue + "";
            DataTable DtTrngRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrngRank);


            iTextSharp.text.Font Heading = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_7 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapText_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fCapText_11 = FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD);

            float[] ArrItm = new float[DtTotRanks.Rows.Count + 2];
            ArrItm[0] = 30;
            int CellWidth =  70 / (DtTotRanks.Rows.Count + 1);

            for (int i = 1; i < ArrItm.Length ; i++)
            {
                ArrItm[i] = CellWidth;
            }
            iTextSharp.text.Table tbF = new iTextSharp.text.Table(DtTotRanks.Rows.Count+2);
            tbF.Width = 100;

            tbF.Widths = ArrItm;
            tbF.Alignment = Element.ALIGN_CENTER;
            tbF.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbF.Cellspacing = 1;
            tbF.Cellpadding = 1;
            tbF.Border = iTextSharp.text.Rectangle.NO_BORDER;

            Cell tc1 = new Cell(new Phrase("Training Name", Heading));
            tbF.AddCell(tc1);

            //Cell tc2 = new Cell(new Phrase("MTM Training", fCapText_7));
            //tbF.AddCell(tc2);
            Cell tc3 = new Cell(new Phrase("Schedule", Heading));
            tbF.AddCell(tc3);
            
            foreach (DataRow drtd in DtTotRanks.Rows)
            {
                Cell tc = new Cell(new Phrase(drtd["RANKGROUPNAME"].ToString(), Heading));
                tbF.AddCell(tc);
            }

            foreach (DataRow dr in dtTrng.Rows)
            {
                int TrainingID = Common.CastAsInt32(dr["TrainingId"]);
                //----------------------------------------------
                string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                     "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + TrainingID.ToString() + ") " +
                                     "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                     "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + TrainingID.ToString() + ")";
                DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
                SimilerTrainings = "";
                foreach (DataRow drs in dtSimiller.Rows)
                {
                    SimilerTrainings += "," + drs[1].ToString() + " [ " + drs[2].ToString() + " ]";
                }
                if (SimilerTrainings != "")
                    SimilerTrainings = SimilerTrainings.Substring(1);
                SimilerTrainings = SimilerTrainings.Replace(",", " - OR - ");
                //----------------------------------------------

                Cell tcT = new Cell(new Phrase(dr["TrainingName"].ToString() + " [ " + dr["TrainingTypeName"].ToString() + " ]" + ((SimilerTrainings.Trim() != "") ? " - OR - " + SimilerTrainings : ""), fCapText_7));
                tcT.HorizontalAlignment= Element.ALIGN_LEFT;
                tbF.AddCell(tcT);
                //Cell tcTM = new Cell(new Phrase(dr["MTMTrainingName"].ToString(), fCapText_7));
                //tbF.AddCell(tcTM);

                string Val = "";
                if (dr["ScheduleCount"].ToString().Trim() != "")
                    Val = dr["ScheduleType"].ToString().Trim() + "-" + dr["ScheduleCount"].ToString().Trim();
                else
                    Val = dr["ScheduleType"].ToString().Trim();

                Cell tcSC = new Cell(new Phrase(Val, fCapText_7));
                tbF.AddCell(tcSC);

                foreach (DataRow drtd in DtTotRanks.Rows)
                {
                    int RankID = Common.CastAsInt32(drtd["RankGroupId"]);
                    DataRow[] drs = DtTrngRanks.Select("TrainingId=" + TrainingID.ToString() + " AND RankId=" + RankID.ToString());

                    if (drs.Length > 0)
                    {
                        String BGColor = "";
                        string AppType = drs[0]["ApplyType"].ToString();
                        if (AppType == "1")
                        { AppType = "M"; BGColor = "R"; }
                        else if (AppType == "2") { AppType = "R"; BGColor = "G"; }
                        else AppType = "";

                        if (AppType == "")
                        {
                            Cell tcInn = new Cell(new Phrase("", fCapText_7));
                            tbF.AddCell(tcInn);
                        }
                        else
                        {
                            Cell tcInn = new Cell(new Phrase("", fCapText_7));
                            //if (BGColor=="R")
                            //    tcInn.BackgroundColor = iTextSharp.text.Color.RED;
                            //else
                                tcInn.BackgroundColor = iTextSharp.text.Color.ORANGE;
                            tbF.AddCell(tcInn);
                        }
                    }
                    else
                    {
                        Cell tcInn = new Cell(new Phrase("", fCapText_7));
                        tbF.AddCell(tcInn);
                    }
                }
            }
            document.Add(tbF);
            //document.Add(new Phrase("\nApplicable Vessel List :", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            //iTextSharp.text.Table tbFVessel = new iTextSharp.text.Table(5);
            //tbFVessel.Width = 100;
            //tbFVessel.Cellspacing = 1;
            //tbFVessel.Cellpadding = 1;
            //tbFVessel.Border = iTextSharp.text.Rectangle.NO_BORDER;

            //DataTable dtVsl = Common.Execute_Procedures_Select_ByQueryCMS("select vesselname from dbo.TrainingMatrixForVessel tm inner join vessel on vessel.vesselid=tm.vesselid where trainingmatrixid=" + ddlTraining.SelectedValue + " order by vesselname");
            //int Cnt = 1;
            //foreach (DataRow dr in dtVsl.Rows)
            //{
            //    Cell tcInn = new Cell(new Phrase( Cnt.ToString() +  ". " + dr[0].ToString(), fCapText_7));
            //    tcInn.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //    tbFVessel.AddCell(tcInn);
                
            //    Cnt++;
            //}
            //document.Add(new Phrase("\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            //document.Add(tbFVessel);
            // ==========================================================================================================
            document.NewPage();            
            document.Close();
            Random r = new Random();
            if (File.Exists(Server.MapPath("~/UserUploadedDocuments/TrainingPrograms.pdf")))
            {
                File.Delete(Server.MapPath("~/UserUploadedDocuments/TrainingPrograms.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~/UserUploadedDocuments/TrainingPrograms.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../UserUploadedDocuments/TrainingPrograms.pdf?rand" + r.Next().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {
            //lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divDate');", true);
    }
}
