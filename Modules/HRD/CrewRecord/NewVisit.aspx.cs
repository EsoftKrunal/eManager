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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class CrewRecord_NewVisit : System.Web.UI.Page
{    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            lblPageName.Text = "New Visitor Entry";
            BindTime();
            BindVessel();           
        }        

    }

    #region ---------------- UDF -----------------
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlFromHour.Items.Add("0" + i);
                ddlToHour.Items.Add("0" + i);
            }
            else
            {
                ddlFromHour.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
                ddlToHour.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
            }
        }

        for (int j = 0; j < 60; j++)
        {
            if (j < 10)
            {
                ddlFromMin.Items.Add("0" + j);
                ddlToMin.Items.Add("0" + j);
            }
            else
            {
                ddlFromMin.Items.Add(new ListItem(Convert.ToString(j), Convert.ToString(j)));
                ddlToMin.Items.Add(new ListItem(Convert.ToString(j), Convert.ToString(j)));
            }
        }
    }
    public void BindVessel()
    {
        try
        {
            DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataSource = ds;
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0")); 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public Boolean IsValidate()
    {
        string strQuery = "SELECT top 1 * FROM dbo.OV_VisitMaster WHERE CrewNum = '" + txt_CrewNo.Text.Trim() + "' AND ToDate > '" + txt_ToDate.Text.Trim() + "' order by FromDate desc ";
        DataTable dt = Budget.getTable(strQuery).Tables[0];
        if (txt_CrewNo.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter Emp#.";
            txt_CrewNo.Focus();
            return false;
        }
        if (ddl_Category.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select ocassion.";
            ddl_Category.Focus();
            return false;
        }
        if (ddl_Category.SelectedIndex == 3 && txt_Other.Text.Trim() == "")
        {
            lblMessage.Text = "Please specify other.";
            txt_Other.Focus();
            return false;
        }
        if (ddl_Vessel.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select Vessel.";
            ddl_Vessel.Focus();
            return false;
        }
        if (txt_FromDate.Text == "")
        {
            lblMessage.Text = "Please select From Date.";
            txt_FromDate.Focus();
            return false;
        }
        if (Convert.ToDateTime(txt_FromDate.Text.ToString()) < DateTime.Today)
        {
            lblMessage.Text = "From date should not be less than today.";
            txt_FromDate.Focus();
            return false;
        }
        if (txt_ToDate.Text == "")
        {
            lblMessage.Text = "Please select To Date.";
            txt_ToDate.Focus();
            return false;
        }
        if (Convert.ToDateTime(txt_ToDate.Text.ToString()) < Convert.ToDateTime(txt_FromDate.Text.ToString()))
        {
            lblMessage.Text = "To Date can not be less than From Date.";
            txt_ToDate.Focus();
            return false;
        }
        if (dt.Rows.Count > 0)
        {
            lblMessage.Text = "From date should be greater than last visit date.";
            return false;
        } 
        if (ddl_Location.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select location.";
            ddl_Location.Focus();
            return false;
        }
        if (txt_Remark.Text.Trim() != "" && txt_Remark.Text.Trim().Length > 1000)
        {
            lblMessage.Text = "Remark can not be greater than 1000 characters.";
            txt_Remark.Focus();
            return false;
        }
        return true;
    }
    #endregion -----------------------------------

    #region --------------- EVENTS ---------------
    protected void txt_CrewNo_TextChanged(object sender, EventArgs e)
    {
        if (txt_CrewNo.Text != "")
        {
            lblPageName.Text = "New Visitor Entry";
            DataTable dt = Budget.getTable("SELECT '[ ' + (SELECT RANKCODE FROM RANK WHERE RANK.RANKID=CREWPERSONALDETAILS.CURRENTRANKID) + ' ] ' + (FirstName + ' '+ MiddleName + ' ' + LastName) AS CREWNAME  FROM CREWPERSONALDETAILS WHERE CREWNUMBER='" + txt_CrewNo.Text.Trim() + "' ").Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCrewName.Text = dt.Rows[0]["CREWNAME"].ToString();
            }
            else
            {
                lblMessage.Text = "Crew Member Not Found.";
                lblCrewName.Text = "";
            }
        }
    }
    protected void ddl_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Category.SelectedIndex == 3)
        {
            txt_Other.Visible = true;
            txt_Other.Focus();
        }
        else
        {
            txt_Other.Visible = false;
        }

        if (ddl_Category.SelectedIndex == 1)
            lblVessel.Text = "Vessel [Next] ";
        else if (ddl_Category.SelectedIndex == 2)
            lblVessel.Text = "Vessel [Last] ";
        else
            lblVessel.Text = "Vessel ";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidate())
        {
            return;
        }
        try
        {
            DataTable dt = Budget.getTable("EXEC dbo.OV_ADMS_NewVisit 0,'" + txt_CrewNo.Text.ToString() + "'," + ddl_Vessel.SelectedValue + "," + ddl_Category.SelectedIndex.ToString() + ", '" + txt_Other.Text.Trim().ToString() + "', '" + txt_FromDate.Text.ToString() + " " + ddlFromHour.SelectedItem.Text.ToString() + ":" + ddlFromMin.SelectedItem.Text.ToString() + "', '" + txt_ToDate.Text.ToString() + " " + ddlToHour.SelectedItem.Text.ToString() + ":" + ddlToMin.SelectedItem.Text.ToString() + "', '" + ddl_Location.SelectedItem.Text.ToString() + "', '" + txt_Remark.Text.ToString().Replace("'", "''") + "', " + Session["loginid"] + ", '" + DateTime.Today.ToString("dd-MMM-yyyy") + "', " + Session["loginid"] + ", '" + DateTime.Today.ToString("dd-MMM-yyyy") + "' ").Tables[0];
            //DataTable dt = Budget.getTable("EXEC OV_ADMS_NewVisit '" + txt_CrewNo.Text.ToString() + "'," + ddl_Vessel.SelectedValue + "," + ddl_Category.SelectedIndex.ToString() + ", '" + txt_Other.Text.Trim().ToString() + "', '" + txt_FromDate.Text.ToString() + " " + ddlFromHour.SelectedItem.Text.ToString() + ":" + ddlFromMin.SelectedItem.Text.ToString() + "', '" + txt_ToDate.Text.ToString() + " " + ddlToHour.SelectedItem.Text.ToString() + ":" + ddlToMin.SelectedItem.Text.ToString() + "', '" + ddl_Location.SelectedItem.Text.ToString() + "', '" + txt_Remark.Text.ToString().Replace("'", "''") + "', " + Session["loginid"] + " " ).Tables[0];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Visitor saved successfully.');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tre", "reload();", true);
            btnSave.Enabled = false;
            
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to save Visitor." + ex.Message;
        }
    }
   
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        if (txt_CrewNo.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter Emp#.";
            txt_CrewNo.Focus();
            return;
        }
        if (ddl_Vessel.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select Vessel.";
            ddl_Vessel.Focus();
            return;
        }
        try
        {
            string strSuptMail = "";
            DataTable dt811 = Budget.getTable("SELECT GroupEmail,GroupEmail1 FROM dbo.VESSEL WHERE VESSELID =" + ddl_Vessel.SelectedValue + " ").Tables[0];
            if (dt811.Rows.Count > 0)
            {
                DataRow dr = dt811.Rows[0];
                if (Convert.ToString(dr["GroupEmail"]) != "")
                {
                    strSuptMail = Convert.ToString(dr["GroupEmail"]).ToString();
                }
                if (Convert.ToString(dr["GroupEmail1"]) != "")
                {
                    strSuptMail = strSuptMail + ((strSuptMail.Trim() != "") ? "," : "") + Convert.ToString(dr["GroupEmail"]).ToString();
                }
            }
            char[] sep = { ',' };
            string[] mails = strSuptMail.Split(sep);
            string[] Nomails = { };
            if (mails.Length <= 0)
            {
                lblMessage.Text = "Mail cannot be send as no superintendent is assigned.";
                return;
            }
            else
            {
                string Message = "";
                Message = "Following crew member has been scheduled for briefing on " + txt_FromDate.Text + " from " + ddlFromHour.SelectedItem.Text + ":" + ddlFromMin.SelectedItem.Text + " to " + "" + ddlToHour.SelectedItem.Text + ":" + ddlToMin.SelectedItem.Text + ".</br>";
                Message = Message + "--------------------------------------------" + "</br>";
                Message = Message + "Emp # :" + txt_CrewNo.Text + " / " + lblCrewName.Text + "</br>";
                Message = Message + "Occassion :" + ddl_Category.SelectedItem.Text + "</br>";
                Message = Message + "Vessel :" + ddl_Vessel.SelectedItem.Text + "</br>";
                Message = Message + "From Date & Time :" + txt_FromDate.Text + " " + ddlFromHour.SelectedItem.Text + ":" + ddlFromMin.SelectedItem.Text + "</br>";
                Message = Message + "To Date & Time :" + txt_ToDate.Text + " " + ddlToHour.SelectedItem.Text + ":" + ddlToMin.SelectedItem.Text + "</br>";
                Message = Message + "Location :" + ddl_Location.SelectedItem.Text + "</br>";
                Message = Message + "--------------------------------------------" + "</br>";

                SendMail.MailSendNewVisit(strSuptMail, "Office Visit Notification Mail.[ " + txt_CrewNo.Text + " ]", Message, "emanager@energiossolutions.com");
                lblMessage.Text = "Mail Sent Successfully.";                
            }
        }
        catch (Exception ex) { lblMessage.Text = ex.Message.ToString(); }
       
    }
    #endregion
}
