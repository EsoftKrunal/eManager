using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using Ionic.Zip;
using System.Configuration;
using System.Text;

public partial class VIMS_SCM_SupdtVisit : System.Web.UI.Page
{
    public int ReportsPk
    {
        get{ return Common.CastAsInt32(ViewState["ReportsPk"]); }
        set{ ViewState["ReportsPk"] = value;}
    }
    public string Mode
    {
        get { return Convert.ToString(ViewState["Mode"]); }
        set { ViewState["Mode"] = value; }
    }
    public DataTable dtPresent
    {
        get
        {
            object o = ViewState["dtTempP"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTempP"] = value;
        }

    }
    public DataTable dtAbsent
    {
        get
        {
            object o = ViewState["dtTempA"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTempA"] = value;
        }

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsgHome.Text = "";
        lblMsg_Imp.Text = "";
        lblMsgSCM.Text = "";
        
        // -- THIS IS OFFICE PAGE
        btnSave.Visible = false;
        btnImport.Visible = false;
        btnImportCrew.Visible = false;

        if (!IsPostBack)
        {
            ReportsPk = Common.CastAsInt32(Request.QueryString["pk"]);
            Mode = Request.QueryString["Mode"].ToString();

            lblOccasion.Text = "Suptd. Visit";

            txtShip.Text = Request.QueryString["VC"].ToString();
            BindTime();
            CreateTable();
            if (ReportsPk > 0)
            {
                ShowSCMData();
            }
        }
    }

    private void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            ddlDTCommenced_H.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0')));
            ddlPlaceCommenced_H.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0')));
        }

        for (int j = 0; j < 60; j++)
        {
            ddlDTCommenced_M.Items.Add(new ListItem(j.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0')));
            ddlPlaceCommenced_M.Items.Add(new ListItem(j.ToString().PadLeft(2, '0'), j.ToString().PadLeft(2, '0')));
        }
    }        
    public void CreateTable()
    {
        dtPresent = new DataTable();

        dtPresent.Columns.Add("RANK", typeof(string));
        dtPresent.Columns.Add("NAME", typeof(string));
        dtPresent.Columns.Add("DESGINATION", typeof(string));

        dtPresent.AcceptChanges();

        
        dtAbsent = new DataTable();

        dtAbsent.Columns.Add("RANK", typeof(string));
        dtAbsent.Columns.Add("NAME", typeof(string));

        dtAbsent.AcceptChanges();

    }
    private void BindCrew()
    {
        string SQL =  "SELECT * FROM ( " +
                      "SELECT '' AS Name, 'SUPERINTENDENT' AS RANK, -1 AS RANKLEVEL, 'Chairman' AS SEL, 'N' As SHOW, 0 AS RHReq " +
	                  "UNION " + 
                      "SELECT CL.[CrewName] AS Name,[RANKNAME] AS RANK, R.RANKLEVEL,'' AS SEL,'Y' AS SHOW, " +
                      //"SELECT CL.[CrewName] AS Name,[RANKNAME] AS RANK, [SignOnDt],[SignOffDt], " +
                     //"SEL=(CASE WHEN CS.RANKID=48 then 'Chairman' WHEN CS.RANKID=2 THEN 'Safety Officer' WHEN CS.RANKID=12 THEN 'Committee Member' ELSE 'Attendee' END), " +
                     //SHOW=(CASE WHEN CS.RANKID=48 then 'N' WHEN CS.RANKID=2 THEN 'N' WHEN CS.RANKID=12 THEN 'N' ELSE 'Y' END), " +
                     "(SELECT COUNT([CrewNumber]) FROM [dbo].[CP_CrewDailyWorkRestHours] WHERE [CrewNumber]= CL.[CrewNumber] AND [TransDate]='" + txtDate.Text.Trim() + "' AND [WorkRest]='R' " + 
                     "AND(  " +
                     "	 ((" + ddlDTCommenced_H.SelectedValue + "+(" + Common.CastAsDecimal(ddlDTCommenced_M.SelectedValue) + "/60)) BETWEEN [FromTime] AND [ToTime] )  " +
                     "	 OR  " +
                     "	 ((" + ddlPlaceCommenced_H.SelectedValue + "+(" + Common.CastAsDecimal(ddlPlaceCommenced_M.SelectedValue) + "/60)) BETWEEN [FromTime] AND [ToTime] ) " +
                     "	 OR  " +
                     "	 (((" + ddlDTCommenced_H.SelectedValue + "+(" + Common.CastAsDecimal(ddlDTCommenced_M.SelectedValue) + "/60)) <= [FromTime] AND (" + ddlPlaceCommenced_H.SelectedValue + "+(" + Common.CastAsDecimal(ddlPlaceCommenced_M.SelectedValue) + "/60)) >= [ToTime])) " +
                     " ) ) AS RHReq " +
                     "FROM [dbo].[CP_VesselCrewList] CL  " +
                     "INNER JOIN  [dbo].[CP_VesselCrewSignOnOff] CS ON CL.[CrewNumber] = CS.[CrewNumber] " +
                     "INNER JOIN  [dbo].[CP_RANK] R ON R.[RankId] = CS.[RankId] " +
                     "WHERE dbo.getDatePart(GETDATE()) >= CS.[SignOnDt] AND ( dbo.getDatePart(GETDATE()) <= CS.[SignOffDt] OR CS.[SignOffDt] IS NULL ) " +
                     ") A ORDER BY RANKLEVEL ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptCrewList.DataSource = dt;
        rptCrewList.DataBind();

    } 
    protected void btnMenu_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        ClearMenuSelection();

        switch (Id)
        {
            case 1 : 
                      DHome.Visible = true;
                      btnHome.CssClass = "color_tab_sel";
                      break;
            case 2 : 
                      DSUPTD.Visible = true;
                      btnSCMAgenda.CssClass = "color_tab_sel";
                      break;
        }

    } 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateSections(1) || !ValidateSections(2))
        {
            return;
        }

        try
        {
            Common.Set_Procedures("[DBO].[SCM_INSERTUPDATE_SCM_MASTER_SUPTD]");
            Common.Set_ParameterLength(19);
            Common.Set_Parameters(                
                new MyParameter("@VESSELCODE", txtShip.Text),
                new MyParameter("@SDATE", txtDate.Text.Trim()),
                new MyParameter("@ShipPosFrom", txtShipPosFrom.Text),
                new MyParameter("@ShipPosTo", txtShipPosTo.Text),
                new MyParameter("@COMMTIME", ddlDTCommenced_H.SelectedValue + ":" + ddlDTCommenced_M.SelectedValue),
                new MyParameter("@COMPTIME", ddlPlaceCommenced_H.SelectedValue + ":" + ddlPlaceCommenced_M.SelectedValue),
                new MyParameter("@OCASSION", "S"),
                new MyParameter("@SHIPPOSITION", ddlShipPosition.SelectedValue),
                new MyParameter("@Str_MINOFPREVSAGECOM", 0),
                new MyParameter("@Str_ABSPREVSAGECOM", 0),
                new MyParameter("@Str_OFFCOMMPREVSAFECOM", 0),
                new MyParameter("@SUPTD_CompliancewithRegulations", txtSUPTD_CompliancewithRegulations.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_DeviationsfromSafety", txtSUPTD_DeviationsfromSafety.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_DetailsofTraining", txtSUPTD_DetailsofTraining.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_HealthSafetyMeasures", txtSUPTD_HealthSafetyMeasures.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_Suggestions", txtSUPTD_Suggestions.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_BPI", txtSUPTD_BPI.Text.Trim().Replace(",", "`")),
                new MyParameter("@SUPTD_AnyOtherTopic", txtSUPTD_AnyOtherTopic.Text.Trim().Replace(",", "`")),
                new MyParameter("@ReportsPK", ReportsPk)
            );
                
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ReportsPk > 0)
                {
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM dbo.SCM_RANKDETAILS WHERE ReportsPK=" + ReportsPk + " AND VesselCode='" + txtShip.Text.Trim() + "' ");
                }
                
               ReportsPk = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);

               
               foreach (DataRow dr in dtPresent.Rows)
               {
                   Common.Set_Procedures("[DBO].[SCM_INSERTUPDATE_SCM_RANKDETAILS]");
                   Common.Set_ParameterLength(7);
                   Common.Set_Parameters(
                       new MyParameter("@SCMID", 0),
                       new MyParameter("@RANKNAME", dr["RANK"].ToString()),
                       new MyParameter("@NAME", dr["NAME"].ToString()),
                       new MyParameter("@REMARKS", dr["DESGINATION"].ToString()),
                       new MyParameter("@ABSENT", 0),
                       new MyParameter("@ReportsPK", ReportsPk),
                       new MyParameter("@VesselCode", txtShip.Text)
                       );
                   DataSet ds1 = new DataSet();
                   ds1.Clear();
                   Boolean res1;
                   res1 = Common.Execute_Procedures_IUD(ds1);
                   if (res1)
                   {
                   }
                   else
                   {
                       lblMsgSCM.Text = "Unable to save record.Error : " + Common.getLastError();
                       return;
                   }
               }
               
               foreach (DataRow dr in dtAbsent.Rows)
               {
                   Common.Set_Procedures("[DBO].[SCM_INSERTUPDATE_SCM_RANKDETAILS]");
                   Common.Set_ParameterLength(7);
                   Common.Set_Parameters(
                       new MyParameter("@SCMID", 0),
                       new MyParameter("@RANKNAME", dr["RANK"].ToString()),
                       new MyParameter("@NAME", dr["NAME"].ToString()),
                       new MyParameter("@REMARKS", ""),
                       new MyParameter("@ABSENT", 1),
                       new MyParameter("@ReportsPK", ReportsPk),
                       new MyParameter("@VesselCode", txtShip.Text)
                       );
                   DataSet ds2 = new DataSet();
                   ds2.Clear();
                   Boolean res2;
                   res2 = Common.Execute_Procedures_IUD(ds2);
                   if (res2)
                   {
                   }
                   else
                   {
                       lblMsgSCM.Text = "Unable to save record.Error : " + Common.getLastError();
                       return;
                   }
               }
               lblMsgSCM.Text = "Record saved successfully.";
            }
            else
            {
                lblMsgSCM.Text = "Unable to save record.Error : " + Common.getLastError();
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsgSCM.Text = "Unable to save record.Error : " + ex.Message;
        }
    } 
    protected void btnNext_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        if (!ValidateSections(Id))
        {
            return;
        }

        ClearMenuSelection();

        switch (Id)
        {
            case 1:
                DSUPTD.Visible = true;
                btnSCMAgenda.CssClass = "color_tab_sel";
                break;
            //case 2:
            //    DSUPTD.Visible = true;
            //    btnSCMAgenda.CssClass = "color_tab_sel";
            //    break;            
        }
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        string sql = "select SCMID from dbo.SCM_Master  where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "' ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
            Response.Redirect("~/Modules/LPSQE/SCM/Reports/SCM.aspx?SCMID=" + DT.Rows[0]["SCMID"].ToString() + "");
    }
    public void ShowSCMData()
    {
        string sql = "select [ReportsPK],[VesselCode],replace(convert(varchar, SDate,106),' ','-') AS SDate,[ShipPosFrom],[ShipPosTo],[CommTime],[CompTime], (case Ocassion when 'M' then 'Routine' when 'N' then 'NON-Routine' when 'S' then 'SUPTD Visit' else '' end ) Ocassion ,[ShipPosition], " +                     
                     "SUPTD_CompliancewithRegulations, SUPTD_DeviationsfromSafety, SUPTD_DetailsofTraining, SUPTD_HealthSafetyMeasures, SUPTD_Suggestions, SUPTD_BPI, SUPTD_AnyOtherTopic, " +
                     "[ReceviedOn],[OfficeComments],UpdatedBy +' / '+ Replace( convert(varchar,UpdatedOn,106),' ','-') UpdateByOn, DocName " +
                     "from dbo.SCM_Master  where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "' ";

        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);

        if (DT.Rows.Count > 0)
        {
            lblOccasion.Text = DT.Rows[0]["Ocassion"].ToString();
            txtDate.Text = DT.Rows[0]["SDate"].ToString();
            txtShipPosFrom.Text = DT.Rows[0]["ShipPosFrom"].ToString();
            txtShipPosTo.Text = DT.Rows[0]["ShipPosTo"].ToString();
            ddlDTCommenced_H.SelectedValue = DT.Rows[0]["CommTime"].ToString().Split(':').GetValue(0).ToString();
            ddlDTCommenced_M.SelectedValue = DT.Rows[0]["CommTime"].ToString().Split(':').GetValue(1).ToString();
            ddlPlaceCommenced_H.SelectedValue = DT.Rows[0]["CompTime"].ToString().Split(':').GetValue(0).ToString();
            ddlPlaceCommenced_M.SelectedValue = DT.Rows[0]["CompTime"].ToString().Split(':').GetValue(1).ToString();
            ddlShipPosition.SelectedValue = DT.Rows[0]["ShipPosition"].ToString();
            ddlShipPosition_Click(new object(), new EventArgs());

            // SUPTD ------------------------------------------------
            txtSUPTD_CompliancewithRegulations.Text = DT.Rows[0]["SUPTD_CompliancewithRegulations"].ToString();
            txtSUPTD_DeviationsfromSafety.Text = DT.Rows[0]["SUPTD_DeviationsfromSafety"].ToString();
            txtSUPTD_DetailsofTraining.Text = DT.Rows[0]["SUPTD_DetailsofTraining"].ToString();
            txtSUPTD_HealthSafetyMeasures.Text = DT.Rows[0]["SUPTD_HealthSafetyMeasures"].ToString();
            txtSUPTD_Suggestions.Text = DT.Rows[0]["SUPTD_Suggestions"].ToString();
            txtSUPTD_BPI.Text = DT.Rows[0]["SUPTD_BPI"].ToString();
            txtSUPTD_AnyOtherTopic.Text = DT.Rows[0]["SUPTD_AnyOtherTopic"].ToString();
            if (Convert.ToString(DT.Rows[0]["DocName"]) != "")
            {
                ImgAttachment.Visible = true;
            }
            else
            {
                ImgAttachment.Visible = false;
            }
            sql = "select RankName AS RANK,Name,Remarks AS DESGINATION from dbo.SCM_rankdetails where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "'  and Absent=0 ";

            dtPresent = Common.Execute_Procedures_Select_ByQuery(sql);

            rptPresent.DataSource = dtPresent;
            rptPresent.DataBind();

            sql = "select RankName AS RANK,Name from dbo.SCM_rankdetails where ReportsPk=" + ReportsPk + " AND VesselCode='" + txtShip.Text + "'  and Absent=1 ";

            dtAbsent = Common.Execute_Procedures_Select_ByQuery(sql);

            rptAbsent.DataSource = dtAbsent;
            rptAbsent.DataBind();

            btnSave.Visible = ((Mode.Trim() != "V") && DT.Rows[0]["UpdateByOn"].ToString() == "");
            btnExport.Visible = ((Mode.Trim() != "V") && DT.Rows[0]["UpdateByOn"].ToString() != "");
        }
    } 
    protected void ddlShipPosition_Click(object sender, EventArgs e)
    {
        if (ddlShipPosition.SelectedIndex == 0)
        {
            lblPosition.Text = "Voy From/To :";
            txtShipPosTo.Visible = true;
            txtShipPosTo.Text = "";
        }
        else
        {
            lblPosition.Text = "Port/Anchorage :";
            txtShipPosTo.Visible = false;
            txtShipPosTo.Text = "";
        }
    }     
    public bool ValidateSections(int SectionId)
    {
        if (SectionId == 1)
        {
            if (txtDate.Text == "")
            {
                lblMsgHome.Text = "Please enter date.";
                txtDate.Focus();
                MoveToTab(SectionId);
                return false;
            }

            DateTime dt;

            if (!DateTime.TryParse(txtDate.Text.Trim(), out dt))
            {
                lblMsgHome.Text = "Please enter valid date.";
                txtDate.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if ((Convert.ToDateTime(txtDate.Text.Trim()).Month != DateTime.Today.Month) || Convert.ToDateTime(txtDate.Text.Trim()).Year != DateTime.Today.Year)
            {
                lblMsgHome.Text = "Please enter current month and current year date.";
                txtDate.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (Convert.ToDateTime(txtDate.Text.Trim()).Date >  DateTime.Today.Date)
            {
                lblMsgHome.Text = "Date can not be more than today.";
                txtDate.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (ddlDTCommenced_H.SelectedIndex == 0)
            {
                lblMsgHome.Text = "Please select commenced hour.";
                ddlDTCommenced_H.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (ddlDTCommenced_M.SelectedIndex == 0)
            {
                lblMsgHome.Text = "Please select commenced Minutes.";
                ddlDTCommenced_M.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (ddlPlaceCommenced_H.SelectedIndex == 0)
            {
                lblMsgHome.Text = "Please select completed hour.";
                ddlPlaceCommenced_H.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (ddlPlaceCommenced_M.SelectedIndex == 0)
            {
                lblMsgHome.Text = "Please select completed Minutes.";
                ddlPlaceCommenced_M.Focus();
                MoveToTab(SectionId);
                return false;
            }

            if (rptPresent.Items.Count < 5)
            {
                lblMsgHome.Text = "Please import crew list.";
                MoveToTab(SectionId);
                return false;
            }

            if (ReportsPk <= 0)
            {
                DataTable dtExists = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.SCM_Master WHERE VESSELCODE='" + txtShip.Text.Trim() + "' AND OCASSION='S' AND MONTH(SDATE)=" + dt.Month.ToString() + " AND YEAR(SDATE)=" + dt.Year.ToString());
                if (dtExists.Rows.Count > 0)
                {
                    lblMsgHome.Text = "Record already exists for same month.";
                    txtDate.Focus();
                    MoveToTab(SectionId);
                    return false;
                }
            }
        }

        if (SectionId == 2)
        {

        }

        return true;
    }

    protected void btnImportCrew_Click(object sender, EventArgs e)
    {
        BindCrew();
        dv_ImportCrew.Visible = true;
    } 
    protected void btnImport_Click(object sender, EventArgs e)
    {
        int CHCount = 1;
        int SOCount = 0;
        int CMCount = 0;
        int EOCount = 0;
        int ERCount = 0;

        foreach (RepeaterItem itm in rptCrewList.Items)
        {
            CheckBox chkSel = (CheckBox)itm.FindControl("chkSel");
            TextBox txt_SName = (TextBox)itm.FindControl("txt_SName");
            Label lblDesignation = (Label)itm.FindControl("lblDesignation");
            DropDownList ddlHDesignation = (DropDownList)itm.FindControl("ddlHDesignation");

            if (chkSel.Checked)
            {
                if (txt_SName.Visible && txt_SName.Text.Trim() == "")
                {
                    lblMsg_Imp.Text = "Please enter Supdt Name.";
                    txt_SName.Focus();
                    return;
                }

                if (ddlHDesignation.Visible && ddlHDesignation.SelectedIndex == 0)
                {
                    lblMsg_Imp.Text = "Please select designation.";
                    ddlHDesignation.Focus();
                    return;
                }

                //if (lblDesignation.Visible && lblDesignation.Text == "Chairman")
                //{
                //    CHCount += 1;
                //}
                
                if ((lblDesignation.Visible && lblDesignation.Text == "Safety Officer") || (ddlHDesignation.Visible && ddlHDesignation.SelectedValue == "Safety Officer"))
                {
                    SOCount += 1;
                }
                if ((lblDesignation.Visible && lblDesignation.Text == "Committee Member") || (ddlHDesignation.Visible && ddlHDesignation.SelectedValue == "Committee Member"))
                {
                    CMCount += 1;
                }
                if ((lblDesignation.Visible && lblDesignation.Text == "Elected Rep.(Officer)") || (ddlHDesignation.Visible && ddlHDesignation.SelectedValue == "Elected Rep.(Officer)"))
                {
                    EOCount += 1;
                }
                if ((lblDesignation.Visible && lblDesignation.Text == "Elected Rep.(Rating)") || (ddlHDesignation.Visible && ddlHDesignation.SelectedValue == "Elected Rep.(Rating)"))
                {
                    ERCount += 1;
                }
            }
        }

        if ((CHCount + SOCount + CMCount + EOCount + ERCount) < 5)
        {
            lblMsg_Imp.Text = "Chairman, Safety Officer, Committee Member, Elected Rep.(Officer), Elected Rep.(Rating) designations must be present in SCM meeting.";
            return;
        }

        if (SOCount > 1 || CMCount > 1 || EOCount > 1 || ERCount > 1)
        {
            lblMsg_Imp.Text = "SCM meeting can not have Multiple Safety Officer,Committee Member,Elected Rep.(Officer) and Elected Rep.(Rating) designations.";
            return;
        }

        dtPresent.Rows.Clear();
        dtAbsent.Rows.Clear();

        foreach (RepeaterItem itm in rptCrewList.Items)
        {
            CheckBox chkSel = (CheckBox)itm.FindControl("chkSel");
            TextBox txt_SName = (TextBox)itm.FindControl("txt_SName");
            Label lblRank = (Label)itm.FindControl("lblRank");
            Label lblHName = (Label)itm.FindControl("lblHName");
            Label lblDesignation = (Label)itm.FindControl("lblDesignation");
            DropDownList ddlHDesignation = (DropDownList)itm.FindControl("ddlHDesignation");

            if (chkSel.Checked)
            {
                DataRow dr = dtPresent.NewRow();

                dr["RANK"] = lblRank.Text.Trim();
                dr["NAME"] = (txt_SName.Visible ? txt_SName.Text : lblHName.Text.Trim());
                dr["DESGINATION"] = (lblDesignation.Visible ? lblDesignation.Text : ddlHDesignation.SelectedValue);

                dtPresent.Rows.Add(dr);
            }
            else
            {
                DataRow dr = dtAbsent.NewRow();

                dr["RANK"] = lblRank.Text.Trim();
                dr["NAME"] = lblHName.Text.Trim();

                dtAbsent.Rows.Add(dr);
            }
        }

        rptPresent.DataSource = dtPresent;
        rptPresent.DataBind();

        rptAbsent.DataSource = dtAbsent;
        rptAbsent.DataBind();

        lblMsg_Imp.Text = "Crew list imported successfully.";
    } 
    protected void btnCloseImport_Click(object sender, EventArgs e)
    {
        dv_ImportCrew.Visible = false;
    } 
    public void MoveToTab(int TabId)
    {
        ClearMenuSelection();

        switch (TabId)
        {
            case 1:
                DHome.Visible = true;
                btnHome.CssClass = "color_tab_sel";
                break;
            
            case 2:
                DSUPTD.Visible = true;
                btnSCMAgenda.CssClass = "color_tab_sel";
                break;
            
        }
    }
    public void ClearMenuSelection()
    {
        btnHome.CssClass = "color_tab";
        btnSCMAgenda.CssClass = "color_tab";
        
        DSUPTD.Visible = false;
        DHome.Visible = false;
    } 
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((Button)sender).CommandArgument);

        ClearMenuSelection();

        switch (Id)
        {
            case 1:
                DHome.Visible = true;
                btnHome.CssClass = "color_tab_sel";
                break;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        
        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM DBO.SCM_Master with(nolock) WHERE ReportsPK = " + ReportsPk + " AND VesselCode='" + txtShip.Text.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "SCM_Master";
        ds.Tables.Add(dt.Copy());

        string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCMSchema.xml");
        string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCMData.xml");
        string ZipFile = Server.MapPath("~/Modules/LPSQE/TEMP/SCM_O_" + txtShip.Text.Trim() + "_" + ReportsPk + ".zip");

        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();

        DataTable dtEMAIL = Common.Execute_Procedures_Select_ByQuery("SELECT Email,VesselEmailNew,VesselCode FROM [dbo].[Vessel] with(nolock) WHERE VesselCode='" + txtShip.Text.Trim() + "'");
        if (dtEMAIL.Rows.Count > 0)
        {
            //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
            //Response.BinaryWrite(buff);
            //Response.Flush();
            //Response.End();

            string ToEmail = ((dtEMAIL != null & dtEMAIL.Rows.Count > 0) ? dtEMAIL.Rows[0]["VesselEmailNew"].ToString() : "");
            string selfemail = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());


            List<string> CCEmails = new List<string>();
            string CCEmail = ((dtEMAIL != null & dtEMAIL.Rows.Count > 0) ? dtEMAIL.Rows[0]["Email"].ToString() : "");
            if (!string.IsNullOrEmpty(CCEmail))
            {
                CCEmails.Add(CCEmail);
            }
            if (!string.IsNullOrEmpty(selfemail))
            {
                CCEmails.Add(selfemail);
            }

            string[] NoEmails = { };
            string Subject = "SCM Report";
            string error = "";
            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            StringBuilder sb = new StringBuilder();
            sb.Append("Dear Captain, ");
            sb.Append("***********************************************************************");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<b>Please import the attached file for Office reply for SCM " + DateTime.Parse(txtDate.Text).ToString("MMM-yyyy") + ".</b>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("If you find any discrepancy please inform to emanager@energiossolutions.com");
            sb.Append("<br/>");
            sb.Append("Thank You, <br/>");
            sb.Append("***********************************************************************");
            sb.Append("<br/>");
            sb.Append("<i>Do not reply to this email as we do not monitor it.</i><br/>");
            string result = SendMail.SendSimpleMail(fromAddress, ToEmail, CCEmails.ToArray(), NoEmails.ToArray(), Subject, sb.ToString(), ZipFile);

            if (result == "SENT")
            {
                lblMsgHome.Text = "Mail sent successfully";
            }
            else
            {
                lblMsgHome.Text = "Unable to send mail. Error" + error;
            }
        }
    }
    protected void ImgAttachment_Click(object sender, EventArgs e)
    {
        if (ReportsPk > 0)
        {
            string sql = "SELECT top 1 DocName As FileName,Attachment,ContentType FROM [SCM_MASTER] with(nolock) WHERE [VesselCode] = '" + txtShip.Text.Trim() + "' AND  ReportsPK =" + ReportsPk;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    string contentType = "";
                    string FileName = "";

                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                    {
                        contentType = dt.Rows[0]["ContentType"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FileName"].ToString()))
                    {
                        FileName = dt.Rows[0]["FileName"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(contentType))
                    {

                        byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                        Response.BinaryWrite(latestFileContent);
                        Response.Flush();
                        Response.End();
                    }

                }
                catch (Exception ex)
                {

                    Response.Clear();
                    Response.Write("<center> Invalid File !</center>");
                    Response.End();
                }
            }
        }
    }
}