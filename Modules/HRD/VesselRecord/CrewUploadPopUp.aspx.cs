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
using System.Xml;
using System.Text; 

public partial class VesselRecord_CrewUploadPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (("" + Request.QueryString["Type"]) == "E")
        {
            lblCrew.Text = ""; 
            dvExp.Visible = true;  
            string CrewNumber = Request.QueryString["CrewNumber"];   
            DataTable dt=Budget.getTable("exec dbo.SiregetCrewExperience " + CrewNumber +"").Tables[0];
            grdExp.DataSource = dt;
            grdExp.DataBind();

            dt = Budget.getTable("SELECT CREWNUMBER,FIRSTNAME +  ' ' + isnull(middlename,'') + '' + LASTNAME FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewNumber).Tables[0]; 
            if (dt.Rows.Count > 0)
            {
                lblCrew.Text = dt.Rows[0][0].ToString();
                lblCrewName.Text = dt.Rows[0][1].ToString();
            }
        }
        else if (("" + Request.QueryString["Type"]) == "C")
        {
            dvLicence.Visible = true;
            string CrewNumber = Request.QueryString["CrewNumber"];   
            DataTable dt = Budget.getTable("exec dbo.SiregetCrewCertificates " + CrewNumber + "").Tables[0];
            grdLicence.DataSource = dt;
            grdLicence.DataBind();

            dt = Budget.getTable("SELECT CREWNUMBER,FIRSTNAME +  ' ' + isnull(middlename,'') + '' + LASTNAME FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewNumber).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCrew.Text = dt.Rows[0][0].ToString();
                lblCrewName.Text = dt.Rows[0][1].ToString();
            }
        }
        else if (("" + Request.QueryString["Type"]) == "D")
        {
            dvDCE.Visible = true;
            string CrewNumber = Request.QueryString["CrewNumber"];   
            DataTable dt = Budget.getTable("exec dbo.SiregetCrewDCE " + CrewNumber + "").Tables[0];
            grdDCE.DataSource = dt;
            grdDCE.DataBind();

            dt = Budget.getTable("SELECT CREWNUMBER,FIRSTNAME +  ' ' + isnull(middlename,'') + '' + LASTNAME FROM CREWPERSONALDETAILS WHERE CREWID=" + CrewNumber).Tables[0];
            if (dt.Rows.Count > 0)
            {
                lblCrew.Text = dt.Rows[0][0].ToString();
                lblCrewName.Text = dt.Rows[0][1].ToString();
            }
        }
        else
        {
            lblHeader.Text = "OCIMF Crew Matrix";
            tbCrew.Visible = false; 
            dvLIT.Visible = true;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Session["CrewXML"].ToString());
            XmlNodeList ndList = doc.SelectNodes("Document/Crew");
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tabcrew' cellspacing='0' cellpadding='2'>");
            sb.Append("<tr id='trhead'>");
            //foreach (XmlNode at in ndList[0])
            //{
            //    sb.Append("<td style='text-transform:capitalize'>" + at.Attributes[0].Value + "</td>");
            //}
            sb.Append("<td style='text-transform:capitalize'>Crew Type</td>");
            sb.Append("<td style='text-transform:capitalize'>Rank</td>");
            sb.Append("<td style='text-transform:capitalize'>Nationality</td>");
            sb.Append("<td style='text-transform:capitalize'>Cert. Comp</td>");
            sb.Append("<td style='text-transform:capitalize'>Issue Country</td>");
            sb.Append("<td style='text-transform:capitalize'>Admin Accept</td>");
            sb.Append("<td style='text-transform:capitalize'>Tanker Cert.</td>");
            sb.Append("<td style='text-transform:capitalize'>STCW V Para</td>");
            sb.Append("<td style='text-transform:capitalize'>Radio Qual.</td>");
            sb.Append("<td style='text-transform:capitalize'>Operator Exp.</td>");
            sb.Append("<td style='text-transform:capitalize'>Rank Exp.</td>");
            sb.Append("<td style='text-transform:capitalize'>Tanker Exp.</td>");
            sb.Append("<td style='text-transform:capitalize'>All Tanker Exp.</td>");
            sb.Append("<td style='text-transform:capitalize'>Date Joined Vessel</td>");
            sb.Append("<td style='text-transform:capitalize'>English Prof.</td>");
            sb.Append("</tr>");

            foreach (XmlNode nd in ndList)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + nd.Attributes["Type"].Value.Replace("Crew","")  + "</td>");
                foreach (XmlNode at in nd.ChildNodes)
                {
                        sb.Append("<td>" + at.InnerText + "</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");  
            litCrew.Text =sb.ToString();  
        }
    }
    
}
