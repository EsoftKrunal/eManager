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
using Exl = Microsoft.Office.Interop.Excel;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
public partial class CrewOperation_PopupTMatrix : System.Web.UI.Page
{
    int CrewId = 0;
    int LoginId = 0;
    public System.Data.DataTable TrainingMatrix
    {
        get
        {
            DataTable dt = null;
            try
            {
                dt = (DataTable)Session["TrainingMatrix"];
            }
            catch
            { }
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo", typeof(int)));
                dt.Columns.Add(new DataColumn("TReqId", typeof(int)));
                dt.Columns.Add(new DataColumn("TrainingId", typeof(int)));
                dt.Columns.Add(new DataColumn("TrainingName", typeof(string)));
                dt.Columns.Add(new DataColumn("NextDue", typeof(DateTime)));
            }
            Session["TrainingMatrix"] = dt;
            return dt;
        }
        set { Session["TrainingMatrix"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrewId = Common.CastAsInt32(Request.QueryString["c"]);
        LoginId = Common.CastAsInt32(Session["loginid"]);
        //--------------
        if(! IsPostBack)
        {
            ddl_TrainingReq_Training.DataSource = Budget.getTable("Select InstituteId,InstituteName from TrainingInstitute");
            ddl_TrainingReq_Training.DataTextField = "InstituteName";
            ddl_TrainingReq_Training.DataValueField = "InstituteId";
            ddl_TrainingReq_Training.DataBind();
            ddl_TrainingReq_Training.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", ""));
            BindTrainingMatrix();
        }
    }
    # region Tab-5 Training Matrix
    protected void BindTrainingMatrix()
    {
        TrainingMatrix = null;
        StringBuilder TBL = new StringBuilder();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,FIRSTNAME+' '+MIDDLENAME+' '+LASTNAME AS CREWNAME FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewId.ToString());
        if(dt.Rows.Count >0)
        {
            lblCrew.Text = " [ " + dt.Rows[0][0].ToString() + " : " + dt.Rows[0][1].ToString() + " ]";   
        }

        dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC sp_getTrainingMatrix " + CrewId.ToString());
        TBL.Append("<div style='width:100%;overflow-x:hidden;overflow-y:hidden;'>");
        TBL.Append("<table border='1' cellspacing='0' cellpadding='2' style='border-collapse:collapse;' width='100%'>");
        TBL.Append("<tr class= 'headerstylegrid' style='font-weight:bold;height:20px;'>");
        TBL.Append("<td class='hd' style='width:323px;'>Training Name</td>");
        TBL.Append("<td class='hd' style='width:80px;'>Schedule</td>");
        TBL.Append("<td class='hd' style='width:80px;text-align:center;'>Source</td>");
        TBL.Append("<td class='hd' style='width:80px;text-align:center;'>Next Due Dt</td>");
        TBL.Append("<td class='hd' style=''>Records of Training Done</td>");
        TBL.Append("</tr>");
        TBL.Append("</table>");
        TBL.Append("</div>");
        TBL.Append("<div style='width:100%;overflow-x:hidden;overflow-y:scroll;height:600px;'>");
        TBL.Append("<table border='1' cellspacing='0' cellpadding='2' style='border-collapse:collapse;' width='100%'>");
        foreach (DataRow dr in dt.Rows)
        {
            string color = "green";
            DateTime dtND;
            try
            {
                dtND = Convert.ToDateTime(dr["NEXTDUE"]);
                if (dtND < DateTime.Today)
                {
                    color = "red";
                }
                //if (Convert.ToDateTime(dtND.ToString("dd-MMM-yyyy")) <= DateTime.Today.AddYears(1))
                //{
                    DataRow dr1 = TrainingMatrix.NewRow();
                    dr1["SNo"] = TrainingMatrix.Rows.Count + 1;
                    dr1["TReqId"] = dr["PK"].ToString();
                    dr1["TrainingId"] = dr["TRAININGID"].ToString();
                    dr1["TrainingName"] = dr["trainingname"].ToString();
                    dr1["NextDue"] = dtND.ToString("dd-MMM-yyyy");
                    TrainingMatrix.Rows.Add(dr1);
                //}
            }
            catch { }
            string SCH = dr["SCHCOUNT"].ToString() + "-" + dr["SCHTYPE"].ToString();
            if(SCH.Trim()=="0-")
                SCH="";
    
            //------------------------------ GET SIMILER TRAININGS
            string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                     "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + dr["TRAININGID"].ToString() + ") " +
                                     "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                     "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + dr["TRAININGID"].ToString() + ")";
            DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
            SimilerTrainings = dr["TRAININGID"].ToString();
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainings += "," + drs[0].ToString();
            }
            //-----
            string SimilerTrainingsName = "";
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainingsName += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
            }
            if (SimilerTrainingsName != "")
                SimilerTrainingsName = SimilerTrainingsName.Substring(1);
            SimilerTrainingsName = "<span style='color:red'> OR </span><br/>" + SimilerTrainingsName.Replace(",", "<span style='color:red'> OR </span><br/>");

            //------------------------------------
            TBL.Append("<tr>");
            TBL.Append("<td class='hd' style='text-align:left;padding-left:5px;width:320px;'>" + dr["trainingname"].ToString() + " [<i style='color:Blue;'>" + dr["TRAININGTYPENAME"].ToString() + "</i>]" + SimilerTrainingsName + "</td>");
            TBL.Append("<td class='hd' style='text-align:center;width:80px;'>" + SCH + "</td>");
            TBL.Append("<td class='hd' style='text-align:center;width:80px;'>" + dr["source"].ToString() + "</td>");
            TBL.Append("<td style='text-align:center;width:80px;color:white;background-color:" + color + "'>" + Common.ToDateString(dr["NEXTDUE"]) + "</td>");
            //----------------------------------
            DataTable dtdone = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TRAININGREQUIREMENTID,REPLACE(convert(varchar,TODATE,106),' ','-') AS DONEDATE,N_CrewTrainingStatus,REPLACE(ISNULL(CONVERT(VARCHAR,PlannedFor,106),''),' ','-') AS PlannedFor " +
                    " FROM CREWTRAININGREQUIREMENT WHERE CREWID=" + CrewId.ToString() + " AND TRAININGID IN(" + SimilerTrainings + ") AND (N_CREWTRAININGSTATUS='C' OR PlannedFor IS NOT NULL) AND STATUSID='A' ORDER BY TODATE desc");
          
            string str = "";
            foreach (DataRow dr1 in dtdone.Rows)
            {
                string Mode = ((dr1["N_CrewTrainingStatus"].ToString() == "C") ? "D" : "P");
                str += "<div onclick=\"showUpdate(this," + dr1["TRAININGREQUIREMENTID"].ToString() + "," + dr["TRAININGID"].ToString() + ",'" + Common.ToDateString(dr["NEXTDUE"]) + "','" + Mode + "'," + dr1["TRAININGREQUIREMENTID"].ToString() + ")\" data='" + dr1[2].ToString() + "' style='cursor:pointer;margin:1px;border: solid 1px black;" + ((Mode == "P") ? "background-color:yellow" : "") + ";width:80px;float:left;text-align:center;'>" + ((Mode == "D") ? dr1["DONEDATE"].ToString() : dr1["PlannedFor"].ToString()) + "&nbsp;</div>";
            }
            TBL.Append("<td>" + str + "</td>");
            TBL.Append("</tr>");
        }
        TBL.Append("</table>");
        TBL.Append("</div>");
        litTraining.Text = TBL.ToString();
    }
    //protected void TrainingExcel_Click(object sender, EventArgs e)
    //{
    //    object mis = Type.Missing;
    //    object misValue = System.Reflection.Missing.Value;
    //    //--------------------------------
    //    string source = Server.MapPath("~\\UserUploadedDocuments\\Template\\Training_Template.xls");
    //    //string dest = "C:\\Windows\\System32\\config\\systemprofile\\Desktop\\Training_Template.xls";
    //    string dest = "C:\\Windows\\SysWOW64\\config\\systemprofile\\Desktop\\Training_Template.xls";
    //    if (System.IO.File.Exists(dest))
    //        System.IO.File.Delete(dest);
    //    System.IO.File.Copy(source, dest, true);
    //    //--------------------------------
    //    source = dest;
    //    dest = Server.MapPath("~\\Training_Template.xls");
    //    if (System.IO.File.Exists(dest))
    //        System.IO.File.Delete(dest);

    //    Exl.Application xlApp; xlApp = new Exl.ApplicationClass();
    //    Exl.Workbook File = xlApp.Workbooks.Open(source, 0, false, 5, "", "", true, Exl.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
    //    Exl.Worksheet xlWorkSheet = (Exl.Worksheet)File.Worksheets[1];//.get_Item(1);

    //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select crewnumber,firstname + ' ' + middlename + ' ' + lastname as CrewName , (select vesselcode from vessel where vesselid=currentvesselid) as vessel, (select rankcode from rank where rankid=currentrankid) as rank from crewpersonaldetails where crewid=" + CrewId.ToString());
    //    if (dt.Rows.Count > 0)
    //    {
    //        xlWorkSheet.Cells[2, 4] = dt.Rows[0]["vessel"].ToString();
    //        xlWorkSheet.Cells[3, 4] = dt.Rows[0]["crewnumber"].ToString();
    //        xlWorkSheet.Cells[4, 4] = dt.Rows[0]["CrewName"].ToString();
    //        xlWorkSheet.Cells[5, 4] = dt.Rows[0]["rank"].ToString();
    //    }
    //    for (int i = 0; i <= TrainingMatrix.Rows.Count - 1; i++)
    //    {
    //        xlWorkSheet.Cells[i + 7, 1] = TrainingMatrix.Rows[i]["TReqId"].ToString();
    //        xlWorkSheet.Cells[i + 7, 2] = TrainingMatrix.Rows[i]["TrainingId"].ToString();
    //        xlWorkSheet.Cells[i + 7, 3] = TrainingMatrix.Rows[i]["SNo"].ToString();
    //        xlWorkSheet.Cells[i + 7, 4] = TrainingMatrix.Rows[i]["TrainingName"].ToString();
    //        xlWorkSheet.Cells[i + 7, 5] = TrainingMatrix.Rows[i]["NextDue"].ToString();
    //    }
    //    File.SaveAs(dest, Exl.XlFileFormat.xlWorkbookNormal, mis, mis, mis, mis, Exl.XlSaveAsAccessMode.xlNoChange, mis, mis, mis, mis, mis);
    //    File.Close(mis, mis, mis);
    //    xlApp.Quit();
    //    releaseObject(File);
    //    releaseObject(xlApp);
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('../Training_Template.xls');", true);
    //}

    protected String GetdataRow(string Sno,string TID,string Tname,string NextDue )
    {
        string DataRow = "<tr id='tr" + Sno + "'> " +
                        "<td> <label name='lblSrNo'>"+ Sno +"</label> </td> " +
                        "<td>  " +
                           "<label name='lblTrainingName' >" + GetSimilarTrainings(TID) + "</label>  " +
                           "<input type='hidden' id='hfTrainingID" + Sno + "' value='" + TID + "' /> " +
                        "</td> " +
                        "<td> <label id='lblDueDate" + Sno + "' >" + NextDue + "</label> </td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblFromDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date ' /> " +
                        "</td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblToDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date '  /> " +
                        "</td> " +
                        "<td> " +
                            "<select style='width:130px; font-weight:bold;' id='ddlInstitute" + Sno + "' > " +
                                "<option value='0'  >Select</option> " +
                                "<option value='1'  >MTM YGM</option> " +
                                "<option value='2'  >DMA YGN(JV)</option> " +
                                "<option value='3' selected='selected' >ONBOARD</option> " +
                                "<option value='4'  >MMMC YGN(JV)</option> " +
                                "<option value='5'  >MOSA YGN(JV)</option> " +
                                "<option value='6'  >MTM INDIA</option> " +
                            "</select> " +
                        "</td> " +
                    "</tr>     ";
         return DataRow;
    }
    protected string GetSimilarTrainings(string Tid)
    {
        //------------------------------ GET SIMILER TRAININGS
        string SimilerTrainings = " select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t1.typeoftraining) as TrainingTypeName from Training T1 where T1.trainingid= " + Tid +
                                 "Union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                 "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + Tid + ") " +
                                 "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                 "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + Tid + ")";
        DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);

        string SimilerTrainingsName = "";
        foreach (DataRow drs in dtSimiller.Rows)
        {
            SimilerTrainingsName += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
        }
        if (SimilerTrainingsName != "")
            SimilerTrainingsName = SimilerTrainingsName.Substring(1);
        SimilerTrainingsName = SimilerTrainingsName.Replace(",", "<span style='color:red'> OR </span><br/>");
        return SimilerTrainingsName;

    }
    protected void TrainingExcel_Click(object sender, EventArgs e)
    {
        string DataRows = "";
       string HTMLFile = File.ReadAllText(Server.MapPath("~/CrewOperation/CrewTraining.htm"));

       DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select crewnumber,firstname + ' ' + middlename + ' ' + lastname as CrewName , (select vesselcode from vessel where vesselid=currentvesselid) as vessel, (select rankcode from rank where rankid=currentrankid) as rank from crewpersonaldetails where crewid=" + CrewId.ToString());
        if (dt.Rows.Count > 0)
        {
            HTMLFile = HTMLFile.Replace("$VESSELCODE$",dt.Rows[0]["vessel"].ToString());
            HTMLFile = HTMLFile.Replace("$CREWNUMBER$", dt.Rows[0]["crewnumber"].ToString());
            HTMLFile = HTMLFile.Replace("$CREWNAME$", dt.Rows[0]["CrewName"].ToString());
            HTMLFile = HTMLFile.Replace("$RANK$", dt.Rows[0]["rank"].ToString());
        }
        int SrNo = 1;
        for (int i = 0; i <= TrainingMatrix.Rows.Count - 1; i++)
        {
            //if (Convert.ToDateTime(TrainingMatrix.Rows[i]["NextDue"].ToString()) <= DateTime.Today.AddDays(60))
            //{
                DataRows = DataRows + GetdataRow(SrNo.ToString(), TrainingMatrix.Rows[i]["TrainingId"].ToString(), TrainingMatrix.Rows[i]["TrainingName"].ToString(),Convert.ToDateTime( TrainingMatrix.Rows[i]["NextDue"].ToString()).ToString("dd-MMM-yyyy"));
                SrNo = SrNo + 1;
            //}
        }

        HTMLFile = HTMLFile.Replace("$MaxRowCount$", TrainingMatrix.Rows.Count.ToString());
        HTMLFile = HTMLFile.Replace("$DATAROWS$", DataRows);

        File.WriteAllText(Server.MapPath("~/CrewOperation/CrewTrainingRequirement.htm"), HTMLFile);
        aDownLoadFile.Visible = true;
        aDownLoadFile.Attributes.Add("href", "~/CrewOperation/CrewTrainingRequirement.htm");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('CrewTrainingDataFile.htm');", true);
    }
    private static void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch
        {
            obj = null;
        }
        finally
        {
            GC.Collect();
        }
    }
    protected void btn_UpdateTraining_Click(object sender, EventArgs e)
    {
        int PkId = Common.CastAsInt32(hfdPKId.Value);
        int Tid = Common.CastAsInt32(hfdTId.Value);
        string DD = hfdDD.Value;
        string sql = "EXEC DBO.Update_Training " + PkId.ToString() + "," + CrewId.ToString() + "," + Tid.ToString() + ",'" + DD + "','" + txt_FromDate.Text + "','" + txt_ToDate.Text + "'," + ddl_TrainingReq_Training.SelectedValue + "," + LoginId.ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        txt_FromDate.Text = "";
        txt_ToDate.Text = "";
        ddl_TrainingReq_Training.SelectedIndex = 0;
        BindTrainingMatrix();
    }
    protected void Load_Lit(object sender, EventArgs e)
    {
        litSummary.Text = getTrainingSummary(true, txtTrId.Text);
    }
    protected string getTrainingSummary(bool Update, string PK)
    {
        StringBuilder sb = new StringBuilder();
        string str = "select TRAININGTYPENAME,TRAININGNAME,N_DUEDATE,PLANNEDFOR,UL.FIRSTNAME+ ' '+ UL.LASTNAME AS PLANNEDBY,PLANNEDON,TI.InstituteName AS PlanInstitute,FROMDATE,TODATE,TI1.InstituteName AS DoneInstitute,Remark  " +
            " ,dbo.fn_getSimilerTrainingsName(T.TRAININGID) SimilarTraining " +       
            " from CREWTRAININGREQUIREMENT CTR  " +
                   "INNER JOIN TRAINING T ON CTR.TRAININGID=T.TRAININGID " +
                   "INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING " +
                   "LEFT JOIN TRAININGINSTITUTE TI ON TI.InstituteId=CTR.PlannedInstitute " +
                   "LEFT JOIN TRAININGINSTITUTE TI1 ON TI1.InstituteId=CTR.TrainingPlanningId " +
                   "LEFT JOIN USERLOGIN UL ON UL.LOGINID=CTR.PLANNEDBY where TRAININGREQUIREMENTID=" + PK;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt.Rows.Count > 0)
        {
            sb.Append("<table cellpadding='2' cellspacing='0' border='0' width='100%' style='border-collapse:collapse;text-align:right'>");
            sb.Append("<col align='right' width='130px;' />");
            sb.Append("<col align='left' width='130px;' />");
            sb.Append("<col align='right' width='130px;'/>");
            sb.Append("<col align='left'  width='130px;'/>");
            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:center'><b>" + dt.Rows[0]["SimilarTraining"].ToString().Replace(",", "<span style='color:red;'> OR </span>") + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Due On : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["N_DUEDATE"]) + "</td>");
            sb.Append("<td>Planned For : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["PLANNEDFOR"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Planned Institute : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PlanInstitute"].ToString() + "</td>");
            sb.Append("<td>Planned By/On : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PLANNEDBY"].ToString() + " / " + dt.Rows[0]["PLANNEDON"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>From Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["FROMDATE"]) + "</td>");
            sb.Append("<td>To Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["TODATE"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Institute : </td>");
            sb.Append("<td colspan='3' style='text-align:left'>" + dt.Rows[0]["DoneInstitute"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }
        return sb.ToString();
    }
    protected void Export_PDF(object sender, EventArgs e)
    {
        try
        {
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
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
            
            Font f_head = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            Font f_7 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);

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
            p2.Add(new Phrase("\n\nMTM SHIP MANAGEMENT PTE LTD " + "", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Font BlackText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD,iTextSharp.text.Color.BLACK);
            Font BlueText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);

            Font RedText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.RED);

            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            Phrase p3 = new Phrase();
            p3.Add(new Phrase("CREW TRAINING MATRIX " + "\n", FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD)));
            Cell c3 = new Cell(p3);
            c3.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c3);

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            ////header.Border = iTextSharp.text.Rectangle.NO_BORDER;
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

            iTextSharp.text.Table tb_crew = new iTextSharp.text.Table(6);
            tb_crew.Width = 100;
            tb_crew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_crew.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_crew.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_crew.BorderWidth = 0;
            tb_crew.BorderColor = iTextSharp.text.Color.WHITE;
            tb_crew.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb_crew.Cellspacing = 1;
            tb_crew.Cellpadding = 1;

            DataTable dt_crew = Common.Execute_Procedures_Select_ByQueryCMS("select CrewNumber,firstname + ' ' + middlename + ' ' + lastname as CrewName,(select rankcode from rank where rankid=currentrankid) as Rank, (select vesselname from vessel where vesselid=currentvesselid) as Vessel,CrewStatusName,'" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") + "' as PrintedOn from crewpersonaldetails inner join crewstatus cs on cs.crewstatusid=crewpersonaldetails.crewstatusid where crewid=" + CrewId.ToString());
            string[] Cap = { "Crew # : ", "Crew Name : ", "Rank : ", "Vessel : ", "Crew Status : ", "Printed On : " };
            for (int i = 0; i <= 5; i++)
            {
                Cell c_1 = new Cell(new Phrase(Cap[i]));
                c_1.HorizontalAlignment = Element.ALIGN_RIGHT;
                c_1.VerticalAlignment = Element.ALIGN_TOP;
                tb_crew.AddCell(c_1);

                Cell c_2 = new Cell(new Phrase(dt_crew.Rows[0][i].ToString()));
                c_2.HorizontalAlignment = Element.ALIGN_LEFT;
                c_1.VerticalAlignment = Element.ALIGN_TOP;
                tb_crew.AddCell(c_2);
            }
            document.Add(tb_crew);
            document.Add(new Phrase("\n"));
            //// ================================= DATA =================================
            DataTable dt_data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC sp_getTrainingMatrix " + CrewId.ToString());

            iTextSharp.text.Table tb_DATA = new iTextSharp.text.Table(6);
            tb_DATA.Width = 100;
            float[] ws = { 25, 7, 7, 7,7, 57 };
            tb_DATA.Widths = ws;
            //tb_crew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb_crew.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_DATA.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb_DATA.BorderWidth = 0;
            tb_DATA.BorderColor = iTextSharp.text.Color.BLACK;
            tb_DATA.Cellspacing = 0;
            tb_DATA.Cellpadding = 1;
            string[] Cap1 = { "Training Name","Schedule", "Source", "Next Due Dt.","Plan Dt.", "Records of Training Done" };
            for (int i = 0; i <= 5; i++)
            {
                Cell c_1 = new Cell(new Phrase(Cap1[i], f_head));
                c_1.BackgroundColor = Color.LIGHT_GRAY;
                c_1.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_1);
            }
            for (int i = 0; i <= dt_data.Rows.Count - 1; i++)
            {
                //------------------------------ GET SIMILER TRAININGS
                string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                         "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + dt_data.Rows[i]["TRAININGID"].ToString() + ") " +
                                         "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                         "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + dt_data.Rows[i]["TRAININGID"].ToString() + ")";
                DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
                SimilerTrainings = dt_data.Rows[i]["TRAININGID"].ToString();
                foreach (DataRow drs in dtSimiller.Rows)
                {
                    SimilerTrainings += "," + drs[0].ToString();
                }
                //-----
                string SimilerTrainingsName = "";
                foreach (DataRow drs in dtSimiller.Rows)
                {
                    SimilerTrainingsName += "," + drs[1].ToString() + " [ " + drs[2].ToString() + " ]";
                }
                if (SimilerTrainingsName != "")
                    SimilerTrainingsName = SimilerTrainingsName.Substring(1);
                SimilerTrainingsName = SimilerTrainingsName.Replace(",", " - OR - \n");
                //------------------------------------

                Cell c_1 = new Cell(new Phrase(dt_data.Rows[i][1].ToString() + " - OR - \n" + SimilerTrainingsName, f_7));
                c_1.HorizontalAlignment = Element.ALIGN_LEFT;
                tb_DATA.AddCell(c_1);

                string val = "";
                if (dt_data.Rows[i][4].ToString().Trim() != "")
                    val = "-" + dt_data.Rows[i][4].ToString();

                Cell c_41 = new Cell(new Phrase(dt_data.Rows[i][3].ToString() + val, f_7));
                c_41.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_41);

                Cell c_2 = new Cell(new Phrase(dt_data.Rows[i][2].ToString(), f_7));
                c_2.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_2);

                DateTime dtp = Convert.ToDateTime(dt_data.Rows[i]["NEXTDUE"]);
                 Cell c_3;
                if(dtp<DateTime.Today.AddYears(4))
                    c_3 = new Cell(new Phrase(Common.ToDateString(dt_data.Rows[i]["NEXTDUE"]), RedText));
                else
                    c_3 = new Cell(new Phrase(Common.ToDateString(dt_data.Rows[i]["NEXTDUE"]), f_7));
               
                c_3.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_3);
                string Plandate = "";
                DataTable dt_Plan = Common.Execute_Procedures_Select_ByQueryCMS("select dbo.sp_getNextPlanDate(" + CrewId.ToString() + "," + dt_data.Rows[i]["TRAININGID"].ToString() + ")");
                if (dt_Plan.Rows.Count > 0)
                {
                    try
                    {
                        Plandate = Convert.ToDateTime(dt_Plan.Rows[0][0]).ToString("dd-MMM-yyyy");
                    }
                    catch { }
                }

                Cell c_31 = new Cell(new Phrase(Plandate, f_7));
                c_31.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_31);

                DataTable dtdone = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(convert(varchar,TODATE,106),' ','-') AS DONEDATE,N_CrewTrainingStatus,REPLACE(ISNULL(CONVERT(VARCHAR,PlannedFor,106),''),' ','-') AS PlannedFor " +
                    " FROM CREWTRAININGREQUIREMENT WHERE CREWID=" + CrewId.ToString() + " AND TRAININGID IN(" + SimilerTrainings + ") AND (N_CREWTRAININGSTATUS='C' OR PlannedFor IS NOT NULL) AND STATUSID='A' ORDER BY TODATE desc");
                string Res = "";
                Cell c_4 = new Cell();
                c_4.HorizontalAlignment = Element.ALIGN_LEFT;
                bool start = true;
                if (dtdone.Rows.Count > 0)
                {
                    foreach (DataRow drd in dtdone.Rows)
                    {
                        string Mode = ((drd[1].ToString() == "C") ? "D" : "P");
                        Res = ((start)?"":",") + ((Mode == "P") ? drd[2].ToString() : drd[0].ToString());
                        if(Mode=="P")
                            c_4.Add(new Phrase(Res, BlackText));
                        else
                            c_4.Add(new Phrase(Res, BlackText));
                        start = false;
                    }
                }
                tb_DATA.AddCell(c_4);
            }
            document.Add(tb_DATA);
            // ==========================================================================================================
            document.NewPage();
            document.Close();
            if (File.Exists(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf")))
            {
                File.Delete(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            Random r = new Random();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../UserUploadedDocuments/CrewTrainings.pdf?rand" + r.Next().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {
            //lblmessage.Text = ex.Message.ToString();
        }
    }
    # endregion
}
