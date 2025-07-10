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

public partial class AssessmentMail : System.Web.UI.Page
{
    public string GUID
    { 
        get{ return ViewState["GUID"].ToString();}
        set {ViewState["GUID"]=value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            try
            {
                GUID = Request.QueryString["Key"];
                ShowDetails();
            }
            catch 
            {
                dvAll.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.close();", true);
                return;
            }
        }
    }

    public void ShowDetails()
    {
        string SQL = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME,CPD.ReliefDueDate  " +
                     "FROM CREWCONTRACTBONUSMASTER CCB  " +
                     "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID=CPD.CREWID  " +
                     "INNER JOIN RANK R ON CCB.RANKID=R.RANKID " + 
                     "INNER JOIN VESSEL V ON V.VESSELID=CCB.VESSELID " +
                     "WHERE STATUS='A' AND CREWBONUSID = (SELECT CrewBonusId FROM CrewContractBonusDetails WHERE CAST([GUID] AS Varchar(50)) = '" + GUID + "')";
                     //"WHERE STATUS='A' AND CREWBONUSID = (SELECT CrewBonusId FROM CrewContractBonusDetails WHERE CAST([GUID] AS Varchar) = '" + GUID + "')";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            lblCrewNo.Text = dt.Rows[0]["CREWNUMBER"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblCrewName.Text = dt.Rows[0]["CREWNAME"].ToString();
            lblRank.Text = dt.Rows[0]["RANKCODE"].ToString();
            lblContactRefNo.Text = dt.Rows[0]["ContractRefNumber"].ToString();
            lblCurrVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblReliefDueDt.Text = Common.ToDateString(dt.Rows[0]["ReliefDueDate"]);
            Show_Ack_Details();
        }
        else 
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "failure", "alert('you are not authorize to view this .');window.close();", true);
            return;             
        }
    }
    public void Show_Ack_Details()
    {
        DataTable dtGrad = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewContractBonusDetails WHERE [GUID] = '" + GUID + "'");
        rdoGrade.SelectedValue = dtGrad.Rows[0]["Grade"].ToString();
        txtRemarks.Text = dtGrad.Rows[0]["Comments"].ToString();
        lblRequestedOn.Text = Common.ToDateString(dtGrad.Rows[0]["SentOn"]);
        if (dtGrad.Rows[0]["AckRecd"].ToString() == "Y")
        {
            trPreSubmitted.Visible = true;
            btnSave.Visible = false;
           
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "failure", "window.close();", true);
            return;
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string SQL = "UPDATE CrewContractBonusDetails SET Grade='" + rdoGrade.SelectedValue.Trim() + "', Comments='" + txtRemarks.Text.Trim() + "',AckBy='',AckRecd='Y',AckOn=getdate() WHERE [GUID] = '" + GUID + "'";
        Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        btnSave.Visible = false;         
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully');", true);
        Show_Ack_Details();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "refresh", "refreshParent();", true);
    }
}
