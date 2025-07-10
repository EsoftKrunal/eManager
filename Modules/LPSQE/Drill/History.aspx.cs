using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class Drill_History : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }

    public string DTRecordType
    {
        get { return ViewState["DTRecordType"].ToString(); }
        set { ViewState["DTRecordType"] = value; }
    }

    public int DTId
    {
        get { return Common.CastAsInt32(ViewState["DTId"]); }
        set { ViewState["DTId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            DTId = Common.CastAsInt32(Request.QueryString["DTId"]);
            DTRecordType = Request.QueryString["RT"].ToString();

            HeaderDetails();
            Bindgrid();
        }
    }

    public void HeaderDetails()
    {
        string TableName = (DTRecordType == "T" ? "[dbo].DT_TrainingMaster" : "[dbo].DT_DrillMaster");
        string ColumnName = (DTRecordType == "T" ? "TraininingName" : "DrillName");

        string SQL = "SELECT " + ColumnName + " As Name FROM " + TableName + "  WHERE " + (DTRecordType == "T" ? "TrainingId" : "DrillId") + " = " + DTId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt != null)
        {
            lblHeader.Text = (DTRecordType == "T" ? "Training : " : "Drill : ") + dt.Rows[0]["Name"].ToString();
        }
    }
 
    protected void Bindgrid()
    {
        string SQL = "SELECT * FROM [dbo].[DT_VSL_DrillTrainingsHistory] WHERE VESSELCODE='" + CurrentVessel + "' AND DTID=" + DTId + " AND DTRecordType='" + DTRecordType + "' order by DoneId desc";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt != null)
        {
            rptSCMLIST.DataSource = dt;
            rptSCMLIST.DataBind();
        }
        else
        {
            rptSCMLIST.DataSource = null;
            rptSCMLIST.DataBind();
        }
    }

    protected void btnViewHistory_Click(object sender, EventArgs e)
    {
        int DoneId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "History", "window.open('PopupExecuteDrillTraining.aspx?DoneId=" + DoneId + "', '');", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        int DoneId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "History", "window.open('PrintDrillTraining.aspx?DoneId=" + DoneId + "', '');", true);
    }
    
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int DoneId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 MatrixId FROM DBO.DT_MatrixMaster WHERE ApplicableDate<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' ORDER BY ApplicableDate Desc");
        int MatrixId = Common.CastAsInt32(dt.Rows[0]["MatrixId"]);

        string sql = "SELECT count(*) FROM [dbo].[DT_MatrixDetailsAttachments] WHERE [MatrixId] = " + MatrixId + " AND [DTId] = " + DTId + " AND [RecordType] = '" + DTRecordType + "'";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        int Required = Common.CastAsInt32(dt1.Rows[0][0]);

        sql = "SELECT Count(*) FROM [dbo].[DT_VSL_DrillTrainingHistoryAttachments] WHERE DoneId = " + DoneId;
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(sql);
        int Done = Common.CastAsInt32(dt2.Rows[0][0]);

        if (Required > 0 && Done < Required)
        {
            lblMsg.Text = " Please Upload the photo required to complete the drill.";
            return;
        }


        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "Drill"),
                new MyParameter("@RecordId", DoneId),
                new MyParameter("@RecordNo", ""),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    lblMsg.Text = "Sent for export successfully.";
                }
                
            }
            else
            {
                lblMsg.Text = "Unable to send for export.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to send for export.Error : " + ex.Message;
        }


        //DataTable dtDrillTrainings = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DT_VSL_DrillTrainings] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        //dtDrillTrainings.TableName = "DT_VSL_DrillTrainings";

        //DataTable dtDrillTrainingsHistory = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DT_VSL_DrillTrainingsHistory] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        //dtDrillTrainingsHistory.TableName = "DT_VSL_DrillTrainingsHistory";

        //DataTable dtDrillTrainingHistoryRanks = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DT_VSL_DrillTrainingHistoryRanks] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        //dtDrillTrainingHistoryRanks.TableName = "DT_VSL_DrillTrainingHistoryRanks";

        //DataTable dtDrillTrainingHistoryDetails = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DT_VSL_DrillTrainingHistoryDetails] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        //dtDrillTrainingHistoryDetails.TableName = "DT_VSL_DrillTrainingHistoryDetails";

        //DataTable dtDrillTrainingHistoryAttachments = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[DT_VSL_DrillTrainingHistoryAttachments] WHERE VesselCode='" + CurrentVessel + "' AND DoneId=" + DoneId);
        //dtDrillTrainingHistoryAttachments.TableName = "DT_VSL_DrillTrainingHistoryAttachments";

        //DataSet ds = new DataSet();
        //ds.Tables.Add(dtDrillTrainings.Copy());
        //ds.Tables.Add(dtDrillTrainingsHistory.Copy());

        //ds.Tables.Add(dtDrillTrainingHistoryRanks.Copy());
        //ds.Tables.Add(dtDrillTrainingHistoryDetails.Copy());
        //ds.Tables.Add(dtDrillTrainingHistoryAttachments.Copy());

        //string SchemaFile = Server.MapPath("~/VIMS/TEMP/SCHEMA_Matrix.xml");
        //string DataFile = Server.MapPath("~/VIMS/TEMP/DATA_Matrix.xml");

        //ds.WriteXml(DataFile);
        //ds.WriteXmlSchema(SchemaFile);

        //string ZipData = Server.MapPath("~/VIMS/TEMP/DTM_" + CurrentVessel + "_S_" + DoneId.ToString().PadLeft(5, '0') + ".zip");
        //if (File.Exists(ZipData)) { File.Delete(ZipData); }

        //using (ZipFile zip = new ZipFile())
        //{
        //    zip.AddFile(SchemaFile);
        //    zip.AddFile(DataFile);
        //    zip.Save(ZipData);
        //    Response.Clear();
        //    Response.ContentType = "application/zip";
        //    Response.AddHeader("Content-Type", "application/zip");
        //    Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
        //    Response.WriteFile(ZipData);
        //    Response.End();
        //}
    }

}   
