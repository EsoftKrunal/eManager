using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class eReports_CMSCrewTraining : System.Web.UI.Page
{
    string FormNo = "D110";
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();

        if (!IsPostBack)
        {
            BindList();
        }
    }
    protected void BindList()
    {
        string sqL = " select ROW_NUMBER()over(order by rankid)Sr,*, " +
                     "   (select count(1) from PMS_CREW_TRAININGCOMPLETED T where T.VesselCode = tbl.VesselCode and T.CrewID = tbl.CrewId)Completed, " +
                     "   (select count(*) from PMS_CREW_TRAININGCOMPLETED T where T.VesselCode = tbl.VesselCode and T.CrewID = tbl.CrewId and OfficeRecdOn is null)pendingexport " +                     
                     "   from " +
                     "  (    select H.VesselCode, H.CrewId,H.rankid,RR.RANKCODE, H.CrewNumber, H.CrewName, Count(R.TrainingRequirementId) - Count(C.TrainingRequirementId)Due " +
                     "      from PMS_CREW_TRAININGREQUIREMENT R " +
                     "      left " +
                     "      join PMS_CREW_TRAININGCOMPLETED C on C.VesselCode = R.VesselCode and R.TrainingRequirementId = C.TrainingRequirementId " +
                     "      inner join PMS_CREW_HISTORY H on H.CrewId = R.CrewID and H.VesselCode = R.VesselCode and H.SignOnDate <= Getdate() and(H.SignOffDate >= GETDATE() or H.SignOffDate is null) " +
                     " inner join MP_ALLRANK RR ON RR.RANKID=H.rankid " +
                     "   where H.VesselCode='" +Session["CurrentShip"] +"'" +
                     "      Group By H.VesselCode, H.CrewId,H.rankid,RR.RANKCODE, H.CrewNumber, H.CrewName " +
                     "  ) tbl ORDER BY rankid";

        sqL = " select ROW_NUMBER()over(order by rankid)Sr,*,Planned-Completed as Due,(select count(*) from PMS_CREW_TRAININGCOMPLETED T where T.VesselCode = tbl.VesselCode and T.CrewID = tbl.CrewId and OfficeRecdOn is null)pendingexport  "+
              " from " +
              " ( " +
              "     select H.VesselCode, H.CrewId, H.rankid, RR.RANKCODE, H.CrewNumber, H.CrewName, Count(R.TrainingRequirementId) As Planned, Count(C.TrainingRequirementId)As Completed " +
              "     from PMS_CREW_TRAININGREQUIREMENT R " +
              "     left " +
              "     join PMS_CREW_TRAININGCOMPLETED C on C.VesselCode = R.VesselCode and R.TrainingRequirementId = C.TrainingRequirementId " +
              "     inner join PMS_CREW_HISTORY H on H.CrewId = R.CrewID and H.VesselCode = R.VesselCode " +
              "     and H.SignOnDate <= Getdate() and(H.SignOffDate >= GETDATE() or H.SignOffDate is null) " +
              "     inner join MP_ALLRANK RR ON RR.RANKID = H.rankid where H.VesselCode = '" + Session["CurrentShip"] + "' " +
              "     Group By H.VesselCode, H.CrewId, H.rankid, RR.RANKCODE, H.CrewNumber, H.CrewName " +
              " ) tbl ORDER BY rankid";

        //Response.Write(sqL);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqL);
        if (dt != null)
        {
            rptTraining.DataSource = dt;
            rptTraining.DataBind();
        }
    }
    
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", Session["CurrentShip"].ToString()),
                new MyParameter("@RecordType", "CREW TRAINING UPDATE"),
                new MyParameter("@RecordId", DateTime.Today.ToString("dd-MMM-yyyy")),
                new MyParameter("@RecordNo", DateTime.Today.ToString("dd-MMM-yyyy")),
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
    }
}