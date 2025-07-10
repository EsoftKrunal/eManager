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

public partial class Reporting_VesselMatrix_CrewApproval : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_matrix.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.lblmessage.Text = "";
        if (!Page.IsPostBack)
            BindMatrixDDL();
        else
            Show_Report();
    }
    private void BindMatrixDDL()
    {
        DataSet dtmtrx = cls_SearchReliever.getMasterData("MatrixHeader", "MatrixId", "MatrixName");
        this.ddl_matrix.DataTextField = "MatrixName";
        this.ddl_matrix.DataValueField = "MatrixId";
        this.ddl_matrix.DataSource = dtmtrx;
        this.ddl_matrix.DataBind();
        this.ddl_matrix.Items.Insert(0, new ListItem("<Select>", "0"));
    }
    private void Show_Report()
    {
        string vesselname = "";
        try
        {
            int relieve_id, reliever_id, vessel_id;
            if (Page.Request.QueryString["relieveid"] != null && Page.Request.QueryString["relieveid"]!="")
            {
                relieve_id = Convert.ToInt32(Page.Request.QueryString["relieveid"].ToString());
            }
            else
            {
                relieve_id = 0;
            }
            if (Page.Request.QueryString["reliverid"] != null && Page.Request.QueryString["reliverid"]!="")
            {
                reliever_id = Convert.ToInt32(Page.Request.QueryString["reliverid"].ToString());
            }
            else
            {
                reliever_id = 0;
            }
            if (Page.Request.QueryString["vesselid"] != null && Page.Request.QueryString["vesselid"]!="")
            {
                vessel_id = Convert.ToInt32(Page.Request.QueryString["vesselid"].ToString());
                DataTable dt=VesselSearch.selectDataVesselDetailsByVesselId(vessel_id);  
                if (dt.Rows.Count >0)
                {
                    vesselname = dt.Rows[0]["VesselName"].ToString() ;
                }
            }
            else
            {
                vessel_id = 0;
            }
            string matrixname = ddl_matrix.SelectedItem.Text;
            DataTable dtvm1 = Vessel_Matrix_CrewApproval.selectVesselMatrixDetails1forCrewApproval(relieve_id, reliever_id, Convert.ToInt32(ddl_matrix.SelectedValue),vessel_id);
            DataTable dtvm2 = Vessel_Matrix_CrewApproval.selectVesselMatrixDetails2forCrewApproval(relieve_id, reliever_id, Convert.ToInt32(ddl_matrix.SelectedValue), vessel_id);
            DataTable dtrem = Vessel_Matrix_CrewApproval.getRemarks(relieve_id, reliever_id, Convert.ToInt32(ddl_matrix.SelectedValue), vessel_id);
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("VesselMatrix_CrewApproval.rpt"));

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub1 = rpt.OpenSubreport("VesselMatrix1_CrewApproval.rpt");
            rptsub1.SetDataSource(dtvm1);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub2 = rpt.OpenSubreport("VesselMatrix2_CrewApproval.rpt");
            rptsub2.SetDataSource(dtvm2);

            rpt.SetParameterValue("@Header", "Vessel Matrix for Matrix : " + matrixname);
            
            if (dtvm1.Rows.Count > 0 && dtvm2.Rows.Count > 0)
            {
                rpt.SetParameterValue("@P1", dtrem.Rows[0][1].ToString());
                rpt.SetParameterValue("@P2", dtrem.Rows[0][2].ToString());
                rpt.SetParameterValue("@P3", dtrem.Rows[0][3].ToString());
                rpt.SetParameterValue("@P4", Convert.ToInt32(dtrem.Rows[1][1].ToString()) - Convert.ToInt32(dtrem.Rows[0][1].ToString()));
                rpt.SetParameterValue("@P5", Convert.ToInt32(dtrem.Rows[1][2].ToString()) - Convert.ToInt32(dtrem.Rows[0][2].ToString()));
                rpt.SetParameterValue("@P6", Convert.ToInt32(dtrem.Rows[1][3].ToString()) - Convert.ToInt32(dtrem.Rows[0][3].ToString()));
            }
            else
            {
                rpt.SetParameterValue("@P1", "0");
                rpt.SetParameterValue("@P2", "0");
                rpt.SetParameterValue("@P3", "0");
                rpt.SetParameterValue("@P4", Convert.ToInt32(dtrem.Rows[1][1].ToString()) - 0);
                rpt.SetParameterValue("@P5", Convert.ToInt32(dtrem.Rows[1][2].ToString()) - 0);
                rpt.SetParameterValue("@P6", Convert.ToInt32(dtrem.Rows[1][3].ToString()) - 0);
            }
            DataTable dt3 = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt3.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            rpt.SetParameterValue("@vname", vesselname);
}
        catch (SystemException se) { }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {

    }
}
