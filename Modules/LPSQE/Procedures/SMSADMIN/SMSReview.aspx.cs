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
using Ionic.Zip; 
using System.IO;

public partial class SMS_SMSReview : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }


        if (!IsPostBack)
        {
            Session["MM1"] = "R";
            txtFromDate.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            Load_vessel();
            Load_office();
            btnSearch_Click(sender, e);
        }
    }

    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();

        ddlVessel.Items.Insert(0, new ListItem(" < All >", ""));
    }
    private void Load_office()
    {
        DataSet dt = Budget.getTable("SELECT OfficeId, OfficeName, OfficeCode FROM [dbo].[Office] Order By OfficeName");
        ddlOffice.DataSource = dt;
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataValueField = "OfficeId";
        ddlOffice.DataBind();

        ddlOffice.Items.Insert(0, new ListItem(" < All >", ""));
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Where = " WHERE 1=1 ";
        string SQL = "SELECT * FROM [dbo].[vw_SMS_GetComments] ";

        string LocationSQL = "";

        if (ddlVessel.SelectedIndex != 0)
        {
            LocationSQL = " Location='" + ddlVessel.SelectedValue.Trim() + "' ";
        }
        if (ddlOffice.SelectedIndex != 0)
        {
            LocationSQL = LocationSQL + ((LocationSQL.Trim()=="")?"":" OR ") + " OfficeId=" + ddlOffice.SelectedValue.Trim() + " ";
        }
        
        if (ddlStatus.SelectedIndex != 0)
        {
            Where = Where + " AND Status='" + ddlStatus.SelectedValue.Trim() + "' ";
        }
        if (ddlChReq.SelectedIndex > 0)
        {
            Where = Where + " AND isnull(ChangeRequested,'N')='" + ddlChReq.SelectedValue.Trim() + "' ";
        }
        if (ddlStage.SelectedIndex != 0)
        {
            Where = Where + " AND Stage='" + ddlStage.SelectedValue.Trim() + "' ";
        }
        if (txtFromDate.Text.Trim() != "")
        {
            Where = Where + " AND dbo.getDatePart(CommentOn) >= '" + txtFromDate.Text.Trim() + "' ";
        }
        if (txtToDate.Text.Trim() != "")
        {
            Where = Where + " AND dbo.getDatePart(CommentOn) <= '" + txtToDate.Text.Trim() + "' ";
        }

        if (LocationSQL != "")
            Where = Where + " AND ( " + LocationSQL + " ) ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " ORDER BY [CommentOn] DESC ");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptComments.DataSource = dt;
            lblNOR.Text = "( " + dt.Rows.Count.ToString() + " ) Records found.";
            rptComments.DataBind();
        }
        else
        {
            rptComments.DataSource = null;
            rptComments.DataBind();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex <= 0)
        {
            ProjectCommon.ShowMessage("Please select vessel to export.");
            ddlVessel.Focus();
            return;
        }

        string SQL ="SELECT S.* FROM [dbo].[SMS_ShipComments] S " +
                    "INNER JOIN [dbo].SMS_APP_ManualMaster MM ON MM.MANUALID=S.MANUALID " +
                    "INNER JOIN [dbo].SMS_APP_ManualDetails MD ON MD.MANUALID=S.MANUALID AND MD.SectionId=S.SectionId ";
        string Where = "WHERE VESSELCODE='" + ddlVessel.SelectedValue.Trim() + "' ";

        if (ddlChReq.SelectedIndex > 0)
        {
            Where = Where + " AND isnull(ChangeRequested,'N')='" + ddlChReq.SelectedValue.Trim() + "' ";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            Where = Where + " AND isnull(Status,'O')='" + ddlStatus.SelectedValue.Trim() + "' ";
        }
        if (ddlStage.SelectedIndex != 0)
        {
            Where = Where + " AND isnull(Stage,'P')='" + ddlStage.SelectedValue.Trim() + "' ";
        }
        if (txtFromDate.Text.Trim() != "")
        {
            Where = Where + " AND dbo.getDatePart(ReqDate) >= '" + txtFromDate.Text.Trim() + "' ";
        }
        if (txtToDate.Text.Trim() != "")
        {
            Where = Where + " AND dbo.getDatePart(ReqDate) <= '" + txtToDate.Text.Trim() + "' ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " ORDER BY [CommentOn] DESC ");
        if(dt.Rows.Count>0)
        {
            
            string VessslCode = ddlVessel.SelectedValue;
            string SchemaFileName = Server.MapPath("SMSR_Schema.xml");
            string DataFileName = Server.MapPath("SMSR_Data.xml");
            dt.TableName = "SMS_ShipComments";
            dt.DataSet.WriteXmlSchema(SchemaFileName);
            dt.DataSet.WriteXml(DataFileName);

            string ZipData = Server.MapPath("SMSR_O_" + VessslCode + ".zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFileName);
                zip.AddFile(DataFileName);
                zip.Save(ZipData);
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Type", "application/zip");
                Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
                Response.WriteFile(ZipData);
                Response.End();
            }
        }
        else
        {
            ProjectCommon.ShowMessage("Nothing to export.");
        }
    }
    
    protected void btnShowComments_Click(object sender, EventArgs e)
    {
        string Key = ((ImageButton)sender).CommandArgument.Trim();
        string[] CommArr = Key.Split(':');
        string MODE = CommArr[0].ToString();
        string Location = CommArr[1].ToString();
        int CommentId = Common.CastAsInt32(CommArr[2]);
        string SQL = "";

        ViewState["MODE"] = MODE;
        ViewState["VSL"] = Location;
        ViewState["COMMENTID"] = CommentId;

        if (MODE.Trim() == "O")
        {
            SQL = "SELECT isnull(STATUS,'O') AS STATUS,MANUALNAME,HEADING,COMMENTBY,POSITIONNAME,OFFICENAME, OFFICEID,COMMENTTEXT,COMMENTON,ClosureBy, ClosedOn, ClosureRemarks,isnull(ChangeRequested,'N') AS ChangeRequested FROM DBO.[vw_SMS_GetComments] WHERE MODE='O' AND COMMENTID=" + CommentId;
        }
        else
        {
            SQL = "SELECT isnull(STATUS,'O') AS STATUS,MANUALNAME,HEADING,COMMENTBY,POSITIONNAME,OFFICENAME,0 AS OFFICEID,COMMENTTEXT,COMMENTON,ClosureBy, ClosedOn, ClosureRemarks,isnull(ChangeRequested,'N') AS ChangeRequested FROM DBO.[vw_SMS_GetComments]  WHERE MODE='S' AND [LOCATION] = '" + Location + "' AND [CommentId]=" + CommentId;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblMname.Text = dt.Rows[0]["ManualName"].ToString();
        lblSName.Text = dt.Rows[0]["Heading"].ToString();

        lblByOn.Text = dt.Rows[0]["COMMENTBY"].ToString() + " <span style='color:blue;'><i> ( " + dt.Rows[0]["POSITIONNAME"].ToString() + " ) </i></span>" + " / " + Common.ToDateString(dt.Rows[0]["COMMENTON"]);
        lblOfficeByOn.Text = dt.Rows[0]["ClosureBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["ClosedOn"]);
        
        advComments.Text = dt.Rows[0]["CommentText"].ToString();
        txtComments.Text = dt.Rows[0]["ClosureRemarks"].ToString();

        dvComments.Visible = true;
        string Status = dt.Rows[0]["STATUS"].ToString();

        
        ViewState["OfficeId"] = dt.Rows[0]["OFFICEID"].ToString();        ViewState["ReqBy"] = dt.Rows[0]["COMMENTBY"].ToString();
        ViewState["ReqOn"] = Common.ToDateString(dt.Rows[0]["COMMENTON"]);

        //btnApprove.Visible = (Status == "O" || Status == "");
        //btnReject.Visible = (Status == "O" || Status == "");
        //btnOnHold.Visible = (Status == "O" || Status == "");

        btnApprove.Visible = (Status == "O" || Status == "") && (dt.Rows[0]["ChangeRequested"].ToString() == "Y");
        btnReject.Visible = (Status == "O" || Status == "") && (dt.Rows[0]["ChangeRequested"].ToString() == "Y");
        btnOnHold.Visible = (Status == "O" || Status == "") && (dt.Rows[0]["ChangeRequested"].ToString() == "Y");

        sp_SMSChangeReq.Visible = (Status == "O" || Status == "") && (dt.Rows[0]["ChangeRequested"].ToString() == "Y");

        btnSave.Visible = (Status == "O" || Status == "") && (dt.Rows[0]["ChangeRequested"].ToString() == "N");
    }
    protected void btnShowSection_Click(object sender, EventArgs e)
    {
        string Key = ((ImageButton)sender).CommandArgument.Trim();
        string[] CommArr = Key.Split(':');
        string ManualId = CommArr[0].ToString();
        string SectionId= CommArr[1].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "as", "window.open(' ReadManualSection1.aspx?ManualId=" + ManualId + "&amp;SectionId=" + SectionId + "');", true);
    }
  
    protected void btnCloseComment_Click(object sender, EventArgs e)
    {
        advComments.Text = "";
        dvComments.Visible = false;
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        string Action = ((Button)sender).CommandArgument;
        string SQL = "";
        string Status = "C";
        if (Action == "H")
            Status = "O";
        if (Action == "S")
        {
            if (ViewState["MODE"].ToString().Trim() == "O")
            {
                SQL = "UPDATE [dbo].[SMS_OfficeComments] SET STATUS='" + Status + "',STAGE='',ClosedBy=" + Session["loginid"].ToString() + ",ClosedOn=getdate(),ClosureRemarks='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE [CommentId]=" + ViewState["COMMENTID"].ToString();
            }
            else
            {
                SQL = "UPDATE [dbo].[SMS_ShipComments] SET STATUS='" + Status + "',STAGE='',ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn=getdate(),ClosureRemarks='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE [VesselCode] = '" + ViewState["VSL"] + "' AND [CommentId]=" + ViewState["COMMENTID"].ToString();
            }
        }
        else
        {
            if (ViewState["MODE"].ToString().Trim() == "O")
            {
                SQL = "UPDATE [dbo].[SMS_OfficeComments] SET STATUS='" + Status + "',STAGE='" + Action + "',ClosedBy=" + Session["loginid"].ToString() + ",ClosedOn=getdate(),ClosureRemarks='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE [CommentId]=" + ViewState["COMMENTID"].ToString();
            }
            else
            {
                SQL = "UPDATE [dbo].[SMS_ShipComments] SET STATUS='" + Status + "',STAGE='" + Action + "',ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn=getdate(),ClosureRemarks='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE [VesselCode] = '" + ViewState["VSL"] + "' AND [CommentId]=" + ViewState["COMMENTID"].ToString();
            }

            if (Action == "A")
            {
                try
                {
                    Common.Set_Procedures("dbo.MOC_CreateMocRequest");
                    Common.Set_ParameterLength(12);
                    Common.Set_Parameters(
                        new MyParameter("@Topic", "SMS Review"),
                        new MyParameter("@Source", ((ViewState["MODE"].ToString().Trim() == "S") ? "Vessel" : "Office")),
                        new MyParameter("@MOCDate", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
                        new MyParameter("@VesselCode", ((ViewState["MODE"].ToString().Trim() == "S") ? ViewState["VSL"].ToString() : "")),
                        new MyParameter("@OfficeId", ((ViewState["MODE"].ToString().Trim() == "O") ? ViewState["OfficeId"].ToString() : "")),
                        new MyParameter("@RequestBy", Common.CastAsInt32(Session["loginid"])),
                        new MyParameter("@RequestOn", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
                        new MyParameter("@Impact", ""),
                        new MyParameter("@ReasonForChange", ""),
                        new MyParameter("@ProposedTimeline", ""),
                        new MyParameter("@DescrOfChange", ""),
                        new MyParameter("@ReferenceKey", ((ViewState["MODE"].ToString().Trim() == "S") ? ViewState["VSL"].ToString() + "~" + ViewState["COMMENTID"].ToString() : ViewState["COMMENTID"].ToString()))
                        );

                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(ds);

                    if (res){}
                    else
                    {
                        ProjectCommon.ShowMessage("Unable to create MOC Request.Error :" + Common.getLastError());
                    }

                }
                catch (Exception ex)
                {

                    ProjectCommon.ShowMessage("Unable to create MOC Request.Error :" + ex.Message.ToString());
                }
            }

        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        advComments.Text = "";
        dvComments.Visible = false;
    }    
    
}