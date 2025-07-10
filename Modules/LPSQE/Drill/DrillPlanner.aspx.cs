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

public partial class Drill_DrillPlanner : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
  
    protected void radioButton_OnCheckedChanged(object sender, EventArgs e)
    {
        if (r1.Checked)
            Response.Redirect("DrillPlanner.aspx");
        else if (r2.Checked)
            Response.Redirect("Home.aspx");
        else if (r3.Checked)
            Response.Redirect("DrillPlannerW.aspx");
    }
    protected void ddlDT_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDrillTrainings();
    }
    protected void LoadDrillTrainings()
    {
        if (ddlDT.SelectedIndex == 0)
        {
            lblDTName.Text = "";
            ddlTraininggroups.Visible = false;
            ddlDrillgroups.Visible = false;
        }
        else if (ddlDT.SelectedIndex == 1) // drill
        {
            lblDTName.Text = "Drill Group : ";
            ddlTraininggroups.Visible = false;
            ddlTraininggroups.SelectedIndex = 0;

            ddlDrillgroups.Visible = true;
        }
        else if (ddlDT.SelectedIndex == 2)
        {
            lblDTName.Text = "Training Group : ";
            ddlTraininggroups.Visible = true;
            ddlDrillgroups.Visible = false;
            ddlDrillgroups.SelectedIndex = 0;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        Session["MHSS"] = 0;
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            ProjectCommon.LoadYear(ddlYear);

            ddlDrillgroups.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[DT_DrillMaster] WHERE RECORDTYPE='S' ORDER BY DRILLNAME");
            ddlDrillgroups.DataTextField = "dRILLnAME";
            ddlDrillgroups.DataValueField = "drillid";
            ddlDrillgroups.DataBind();
            ddlDrillgroups.Items.Insert(0, new ListItem("< ALL >", "0"));

            ddlTraininggroups.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].DT_TrainingMaster WHERE RecordType='G' ORDER BY TraininingName");
            ddlTraininggroups.DataTextField = "TraininingName";
            ddlTraininggroups.DataValueField = "TrainingId";
            ddlTraininggroups.DataBind();
            ddlTraininggroups.Items.Insert(0, new ListItem("< ALL >", "0"));

            Bindgrid();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void Bindgrid()
    {
        //----------------------------------

        string whereclause = "";
        if (ddlDT.SelectedIndex != 0)
            whereclause += " AND M.RecordType='" + ddlDT.SelectedValue + "'";

        if (ddlTraininggroups.Visible && ddlTraininggroups.SelectedIndex > 0)
            whereclause += " AND M.PARENTID=" + ddlTraininggroups.SelectedValue + "";

        if (ddlDrillgroups.Visible && ddlDrillgroups.SelectedIndex > 0)
            whereclause += " AND M.PARENTID=" + ddlDrillgroups.SelectedValue + "";

        //---------------------------
        string SQL ="SELECT P.TableId,M.VESSELCODE,M.DTID,M.RecordType,M.TraininingName,M.GroupName,m.ValidityValue,m.ValidityType,M.DoneDate, " +
                    "" + ddlYear.SelectedValue + " as PYear,MON1,MON2,MON3,MON4,MON5,MON6,MON7,MON8,MON9,MON10,MON11,MON12,CreatedBy,CreatedOn, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=1 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON1V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=2 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON2V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=3 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON3V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=4 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON4V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=5 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON5V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=6 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON6V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=7 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON7V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=8 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON8V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=9 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON9V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=10 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON10V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=11 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON11V, " +
                    "(SELECT TOP 1 DONEDATE FROM DBO.DT_VSL_DrillTrainingsHistory H WHERE H.VesselCode=M.VESSELCODE AND M.DTID=H.DTID AND M.RecordType=H.DTRECORDTYPE AND MONTH(DONEDATE)=12 AND YEAR(DONEDATE)=" + ddlYear.SelectedValue + ") AS MON12V " +
                    "FROM DBO.vw_VSL_DrillTrainings_ALL M LEFT JOIN DBO.DT_DrillMatrixPlanner P ON M.DTId=P.DTId AND M.VESSELCODE=P.VESSELCODE AND m.recordtype=p.recordtype and P.Pyear=" + ddlYear.SelectedValue +
                    "WHERE ValidityType<>'W' AND M.VESSELCODE='" + CurrentVessel + "' " + whereclause + " AND ( PYEAR=" + ddlYear.SelectedValue + " OR PYEAR IS NULL ) ORDER BY M.RecordType,M.TraininingName";
        //---------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        for(int i=1;i<=12;i++)
            dt.Columns.Add(new DataColumn("Class" + i.ToString()));
        //---------------------------
        foreach (DataRow dr in dt.Rows)
        {
            for(int i=1;i<=12;i++)
            {
                DateTime dtDate = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), i, DateTime.DaysInMonth(Common.CastAsInt32(ddlYear.SelectedValue), i));
                bool Assgined = (dr["MON" + i.ToString()].ToString()=="True");
                DateTime? dtForDate=null;
                if(!Convert.IsDBNull(dr["MON" + i.ToString() +  "V"]))
                {
                    dtForDate = Convert.ToDateTime(dr["MON" + i.ToString() + "V"]);
                }

                if (Assgined)
                {
                    if (dtDate <= DateTime.Today)
                    {
                        // red
                        // green
                        dr["Class" + i.ToString()] = (dtForDate == null) ? "error" : "success";
                    }
                    else
                    {  // yellow
                       // green
                        dr["Class" + i.ToString()] = (dtForDate == null) ? "planned" : "success";
                    }
                }
                else
                {
                    dr["Class" + i.ToString()] = (dtForDate == null) ? "" : "done-already";
                }
            }
        }
        //----------------------------------
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
    public string getDate(object arg)
    {
        try
        {
            return Common.ToDateString(arg).Substring(0, 2);
        }
        catch (Exception ex)
        {
            return "";
        }

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "re", "window.open('PrintDrillPlanner.aspx?VSL=" + CurrentVessel + "&Year=" + ddlYear.SelectedValue + "&DT=" + ddlDT.SelectedValue + "')", true);
    }

}
