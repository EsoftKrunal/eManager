using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.Globalization;



public partial class CRM_CrewCommunication : System.Web.UI.Page
{
    public string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];


    protected void Page_Load(object sender, EventArgs e)
    {
        txtMessage.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();

        if (!Page.IsPostBack)
        {
            BindReport();
            BindRO();
            BindRankDropDown();
            BindNationalityDropDown();
            for (int month = 1; month <= 12; month++)
            {
                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                ddlMonths.Items.Add(new ListItem(monthName, month.ToString()));      
            }
            ddlMonths.Items.Insert(0, new ListItem("All", "0"));

            //for (int month = 1; month <= 12; month++)
            //{
            //    ddlMonths.Items.Add(new ListItem(month.ToString().PadLeft(2, '0'), month.ToString().PadLeft(2, '0')));
            //}

            txtToAddress.Text = MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString()));
        }
        
            
    }
   
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void btn_Send_Click(object sender, EventArgs e)
    {
        dv_Pop.Visible = true;
        
    }
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    protected void btnFinalSend_Click(object sender, EventArgs e)
    {
       
        if (txtToAddress.Text.Trim() == "")
        {
            txtMessage.Text = "To address does not exists.";
            return;
        }
        else if (txtSubject.Text.Trim() == "")
        {
            txtMessage.Text = "Please enter mail subject.";
            return;
        }
        else if (StripHTML(txtMesssage.Text).Trim() == "")
        {
            txtMessage.Text = "Please enter mail content.";
            return;
        }
        else
        {
            
            SmtpClient _SmtpClient = new SmtpClient();
            _SmtpClient.Host = _ServerName;
            _SmtpClient.Port = _Port;
            _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
            _SmtpClient.EnableSsl = true;

            MailMessage _MailMessage = new MailMessage();
            _MailMessage.From = _FromAdd;
            _MailMessage.To.Add(new MailAddress(txtToAddress.Text));

            foreach (RepeaterItem ri in rpt51.Items)
            {
                CheckBox ch = (CheckBox)ri.FindControl("chkSel");
                if(ch.Checked)
                {
                    HiddenField hfd = (HiddenField)ri.FindControl("hfdEmail");
                    string Email = hfd.Value;
                    try
                    {
                        _MailMessage.Bcc.Add(Email);
                        //_MailMessage.Bcc.Add("emanager@energiossolutions.com");
                    }
                    catch { }
                }
            }


            //------------------------------------------------------------------------------------------------------------------
            if (flpFile.HasFile)
            {
                //using (System.IO.MemoryStream ms = new System.IO.MemoryStream(flpFile.FileBytes))
                //{
                //    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, flpFile.PostedFile.FileName, System.Net.Mime.MediaTypeNames.Application.Octet);
                //    _MailMessage.Attachments.Add(attach);
                //}
                string tmpfile = Server.MapPath("~/Temp/" + flpFile.PostedFile.FileName);
                if (System.IO.File.Exists(tmpfile))
                    System.IO.File.Delete(tmpfile);

                flpFile.PostedFile.SaveAs(tmpfile);
                _MailMessage.Attachments.Add(new Attachment(tmpfile));
            }
           //------------------------------------------------------------------------------------------------------------------            

            _MailMessage.Subject = txtSubject.Text.ToString();
            _MailMessage.Body = txtMesssage.Text;
            _MailMessage.IsBodyHtml = true;
            _MailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                _SmtpClient.Send(_MailMessage);
                txtMessage.Text = "Mail send successfully.";
            }
            catch(Exception ex) {
                txtMessage.Text = "Unable to sent mail. Error : " +ex.Message;
            }

            // send 
            //dv_Pop.Visible = false;
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dv_Pop.Visible = false;

    }
    

    //-- Function ----------------------------------------------------------------------------------------
    public void BindReport()
    {
        {
            string crewwhereclause = " Where c.CrewStatusId=2 AND LEFT(CREWNUMBER,1) IN ('S','Y') AND CURRENTRANKID<>49 ";
            if (ddlRO.SelectedIndex > 0)
            {
                crewwhereclause += " AND c.RecruitmentOfficeId=" + ddlRO.SelectedValue + "";
            }
            if (ddlRank.SelectedIndex > 0)
            {
                crewwhereclause += " AND c.CURRENTRANKID=" + ddlRank.SelectedValue + "";
            }
            if (ddlNat.SelectedIndex > 0)
            {
                crewwhereclause += " AND c.NationalityId=" + ddlNat.SelectedValue + "";
            }
            if (ddlOR.SelectedIndex>0)
            {
               crewwhereclause += " AND r.OffCrew='" + ddlOR.SelectedValue +"'";
            }

            if (ddlMonths.SelectedIndex > 0)
            {
                crewwhereclause += " AND Month(AvailableFrom) = '" + ddlMonths.SelectedValue + "'"; 
            }

            string sql = "select row_number() over(order by ranklevel) as sno,crewid,crewnumber,firstname + ' ' + middlename + ' ' + lastname as CrewName,CountryName,RankCode, RecruitingOfficeName,VesselName as LastVesselName,SignOffDate,(select top 1 email1 from CrewContactDetails cc where cc.crewid=c.crewid and cc.AddressType='C') as Email,(select top 1 MobileNumber from CrewContactDetails cc where cc.crewid=c.crewid and cc.AddressType='C') as MobileNumber,AvailableFrom,AvalRemark,Isnull(DATEDIFF(day,GETDATE(),  AvailableFrom),0) AS days " +
                        "from " +
                        "CrewPersonalDetails c " +
                        "inner " +
                        "join rank r on c.CurrentRankId = r.rankid " +
                        "inner " +
                        "join Country c1 on c.NationalityId = c1.Countryid " +
                        "inner " +
                        "join RecruitingOffice r1 on c.RecruitmentOfficeId = r1.RecruitingOfficeId " +
                        "left " +
                        "join Vessel v on c.LastVesselId = v.VesselId " + crewwhereclause + " order by sno";

            rpt51.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rpt51.DataBind();
        }
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddlRank.DataSource = obj.ResultSet.Tables[0];
        ddlRank.DataTextField = "RankName";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();

    }
    private void BindRO()
    {
        ddlRO.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM RECRUITINGOFFICE ");
        ddlRO.DataTextField = "RECRUITINGOFFICENAME";
        ddlRO.DataValueField = "RECRUITINGOFFICEID";
        ddlRO.DataBind();
        ddlRO.Items.Insert(0, new ListItem("All", "0"));
    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddlNat.DataSource = obj.ResultSet.Tables[0];
        ddlNat.DataTextField = "CountryName";
        ddlNat.DataValueField = "CountryId";
        ddlNat.DataBind();
    }

    protected void btn_Update_AvailabelDate_Click(object sender, EventArgs e)
    {
        if (txt_AvailableDt.Text.Trim() == "")
        {
            lblMessage.Text = "Available date is required.";
            txt_AvailableDt.Focus();
            return;
        }
        if (txt_AvailableDt.Text.Trim() != "")
        {
            if (DateTime.Today >= DateTime.Parse(txt_AvailableDt.Text))
            {
                lblMessage.Text = "Available date must be more than today.";
                return;
            }
        }
        if (txtAvlRem.Text.Trim() == "")
        {
            lblMessage.Text = "Remark field is required.";
            txtAvlRem.Focus();
            return;
        }
        try
        {
            Alerts.Update_AvailableDate(Convert.ToInt32(HiddenPK.Value), DateTime.Parse(txt_AvailableDt.Text), txtAvlRem.Text, Convert.ToInt32(Session["loginid"].ToString()));
            lblMessage.Text = "Updated Successfully.";
        }
        catch
        {
            lblMessage.Text = "Cant Updated.";
        }
    }

    protected void btnClose_AvailabelDate_OnClick(object sender, EventArgs e)
    {
        dv_Pop51.Visible = false;
        BindReport();
    }

    protected void lnlEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton nlk = (LinkButton)sender;
        HiddenField hfCrewId = (HiddenField)nlk.FindControl("hfCrewId");
        HiddenField hfAvailablefrom = (HiddenField)nlk.FindControl("hfAvailablefrom");
        HiddenField hfAvalRemark = (HiddenField)nlk.FindControl("hfAvalRemark");

        txt_AvailableDt.Text = hfAvailablefrom.Value;
        txtAvlRem.Text = hfAvalRemark.Value;

        HiddenPK.Value = hfCrewId.Value;
        dv_Pop51.Visible = true;
    }

    protected string FormatColorRow(string day)
    {
        int days = Convert.ToInt32(day);
        if (days >= 0 && days <= 15)
        {
            return "style='background-color:yellow;'";
        }
        else if (days < 0)
        {
            return "style='background-color::red;'";
        }
        else
        {
            return "style=''";
        }
    }
}
