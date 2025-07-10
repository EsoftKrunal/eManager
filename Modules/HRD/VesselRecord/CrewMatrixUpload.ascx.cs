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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using com.ocimf_sire.ws;
using System.Xml;
using com.ocimf_sire.wsv2;  
public partial class VesselRecord_CrewMatrixUpload : System.Web.UI.UserControl
{
    Authority Auth;
    public int VesselId
    {
        get {
            int vsl = 0;
           try
            {
                 vsl=int.Parse(Session["VesselId"].ToString()); ;
            }
            catch
            {
            }
            return vsl;
        }
        set { Session["VesselId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 226);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 226);
        Obj.InvokeByPage(); 
        Auth=Obj.Authority;
        //------------
        if (!IsPostBack)
        {
            DataTable dt = Budget.getTable("select LRIMONumber from vessel where vesselid=" + VesselId.ToString()).Tables[0] ;
            if (dt.Rows.Count > 0)
            {
                lblImo.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                lblImo.Text = "";
            }
            LoadMatrix();
            btnUpload.Enabled = Auth.isVerify2;   
            //------------
            ddlIssuingCountry.DataSource = Budget.getTable("select CountryName,CountryId from Country order by CountryName");
            ddlIssuingCountry.DataTextField = "CountryName";
            ddlIssuingCountry.DataValueField = "CountryId";
            ddlIssuingCountry.DataBind();
            ddlIssuingCountry.Items.Insert(0,new ListItem("< Select >","0"));
            ddlIssuingCountry.SelectedIndex = 0;  
        }
    }
    public string ConvertYear(object input)
    {
        double res = double.Parse(input.ToString());
        res = res / 12.0;
        res = Math.Round(res, 1);
        string resstr = res.ToString();
        if (!(resstr.Contains(".")))
            resstr = resstr + ".0";
        return resstr; 
    }
    public void LoadMatrix()
    {
        DataTable dt= Budget.getTable("Exec dbo.SireCrewList " + VesselId.ToString() + ",'" + RadioButtonList1.SelectedItem.Text + "'").Tables[0];
        GridView1.DataSource = dt;
        GridView1.SelectedIndex = -1;
        GridView1.DataBind();

        DataTable dtOfficer = Budget.getTable("Exec dbo.SireCrewList " + VesselId.ToString() + ",'Officer'").Tables[0];
        DataTable dtCE= Budget.getTable("Exec dbo.SireCrewList " + VesselId.ToString() + ",'Engineer'").Tables[0];

        Label10.Text = GetSum(dtOfficer, "AllTypeExp", "Rank='Master' OR Rank='Chief Officer'");
        Label20.Text = GetSum(dtOfficer, "RankExp", "Rank='Master' OR Rank='Chief Officer'");
        Label30.Text = GetSum(dtOfficer, "OperatorExp", "Rank='Master' OR Rank='Chief Officer'");

        Label11.Text = GetSum(dtCE, "AllTypeExp", "Rank='Chief Engineer' OR Rank='1st Engineer'");
        Label21.Text = GetSum(dtCE, "RankExp", "Rank='Chief Engineer' OR Rank='1st Engineer'");
        Label31.Text = GetSum(dtCE, "OperatorExp", "Rank='Chief Engineer' OR Rank='1st Engineer'");


        ddlCertComp.SelectedIndex = 0;
        ddlIssuingCountry.SelectedIndex= 0;
        ddlAdminAccept.SelectedIndex = 0;
        ddlTankerCert.SelectedIndex = 0;
        ddlSTCW.SelectedIndex = 0;
        ddlRadio.SelectedIndex = 0;

        txtOpertor.Text = "";
        txtRank.Text = "";
        txtTanker.Text = "";
        txtAllTanker.Text = "";
        txtJoinVessel.Text = "";
        lblMonth.Text ="" ;
        ddlEngProf.SelectedIndex = 0;
        lblLastUpload.Text = "";


        ancCertificate.HRef = "";
        ancDCE.HRef = "";
        ancExperience.HRef = "";

        ancCertificate.Attributes["class"] = "";
        ancDCE.Attributes["class"] = "";
        ancExperience.Attributes["class"] = "";

    }
    public DataTable getCountry()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        return obj.ResultSet.Tables[0];
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMatrix();
    }
    protected void SelectIndexChanged(object sender, EventArgs e)
    {
        string crewid = ((HiddenField)GridView1.SelectedRow.FindControl("hfdcrewid")).Value;
        string crewnumber = ((HiddenField)GridView1.SelectedRow.FindControl("hfdcrewnumber")).Value;
        string crewname = ((HiddenField)GridView1.SelectedRow.FindControl("hfdcrewname")).Value;
        string rank = ((HiddenField)GridView1.SelectedRow.FindControl("hfdrank")).Value;
        string nationality = ((HiddenField)GridView1.SelectedRow.FindControl("hfdnationality")).Value;
        string certcomp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdcertcomp")).Value;
        string issuingcountry = ((HiddenField)GridView1.SelectedRow.FindControl("hfdissuingcountry")).Value;
        string adminaccept = ((HiddenField)GridView1.SelectedRow.FindControl("hfdadminaccept")).Value;
        string tankercert = ((HiddenField)GridView1.SelectedRow.FindControl("hfdtankercert")).Value;
        string stcw = ((HiddenField)GridView1.SelectedRow.FindControl("hfdstcw")).Value;
        string radioqual = ((HiddenField)GridView1.SelectedRow.FindControl("hfdradioqual")).Value;
        string operatorexp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdoperatorexp")).Value;
        string rankexp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdrankexp")).Value;
        string tankertypeexp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdtankertypeexp")).Value;
        string alltypeexp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdalltypeexp")).Value;
        string monthtour = ((HiddenField)GridView1.SelectedRow.FindControl("hfdmonthtour")).Value;
        string signondate = ((HiddenField)GridView1.SelectedRow.FindControl("hfdsignondate")).Value;
        string engprof = ((HiddenField)GridView1.SelectedRow.FindControl("hfdengprof")).Value;
        string uploadon = ((HiddenField)GridView1.SelectedRow.FindControl("hfduploadon")).Value;
        string uploadby = ((HiddenField)GridView1.SelectedRow.FindControl("hfduploadby")).Value;

        hfdcrewid.Value = crewid;
        hfdrank.Value = rank;
        hfdnationality.Value = nationality;
        ddlCertComp.SelectedValue = certcomp;
        try
        {
            ddlIssuingCountry.SelectedValue = issuingcountry;
        }
        catch { }

        ddlAdminAccept.SelectedValue = adminaccept ;
        ddlTankerCert.SelectedValue = tankercert;
        ddlSTCW.SelectedValue = stcw;
        ddlRadio.SelectedValue = radioqual;

        ancCertificate.HRef = "CrewUploadPopUp.aspx?Type=C&CrewNumber=" + crewid + "&width=900&height=400&postback=" + ddlCertComp.ClientID;
        ancDCE.HRef = "CrewUploadPopUp.aspx?Type=D&CrewNumber=" + crewid + "&width=900&height=400&postback=" + ddlTankerCert.ClientID;
        ancExperience.HRef = "CrewUploadPopUp.aspx?Type=E&CrewNumber=" + crewid + "&width=900&height=400";

        ancCertificate.Attributes["class"] = "thickbox";
        ancDCE.Attributes["class"] = "thickbox";
        ancExperience.Attributes["class"] = "thickbox";

        txtOpertor.Text = operatorexp;
        txtRank.Text = rankexp;
        txtTanker.Text = tankertypeexp;
        txtAllTanker.Text = alltypeexp;
        txtJoinVessel.Text = DateTime.Parse(signondate).ToString("dd MMM yyyy");
        lblMonth.Text = monthtour + " month on vessel.";
        ddlEngProf.SelectedValue = engprof;
        lblLastUpload.Text = uploadby + " / " + uploadon;
    }
    private float CastAsFloat(object Data)
    {
        float result = 0;
        try
        {
            result = float.Parse(Math.Round(decimal.Parse(Data.ToString()),1).ToString());
        }
        catch
        {
            result = 0;
        }
        return result;
    }
    protected void Upload_Click(object sender, EventArgs e)
    {
        string AccountId = txtAccountId.Text.Trim();
        string UserId = txtUserId.Text.Trim();
        string Password = txtPassword.Text.Trim();

        // IMO number
        if (lblImo.Text.Trim() == "")
        {
            lblMsg.Text = "Selected vessel having no IMO Number.";
            return;
        }
        // AccountId
        if (txtAccountId.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid AccountId.";
            txtAccountId.Focus();
            return;
        }
        // UserId
        if (txtUserId.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid OCIMF User Id.";
            txtUserId.Focus();
            return;
        }
        // Password
        if (txtPassword.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid password.";
            txtPassword.Focus();
            return;
        }
        // Select a member in grid
        if (GridView1.SelectedIndex < 0)
        {
            lblMsg.Text = "Please select a crew member to upload.";
            return;
        }
        // Check rank is matched
        if (hfdrank.Value.Trim() == "")
        {
            lblMsg.Text = "Please match the rank of selected crew member from sire rank.";
            return;
        }
        // Certificate of competency
        if (ddlCertComp.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select certificate of competency.";
            ddlCertComp.Focus();
            return;
        }
        // Isssuing counrty
        if (ddlIssuingCountry.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select issuing country.";
            ddlIssuingCountry.Focus();
            return;
        }
        // Administrative accespetance
        if (ddlAdminAccept.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select administrative acceptance.";
            ddlIssuingCountry.Focus();
            return;
        }
        // Tanker certification
        if (ddlTankerCert.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select tanker certificate.";
            ddlTankerCert.Focus();
            return;
        }
        // Tanker certification
        if (ddlSTCW.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select STCW V Para.";
            ddlSTCW.Focus();
            return;
        }
        // Radio qualification
        if (ddlRadio.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select radion qualification.";
            ddlRadio.Focus();
            return;
        }
        // Operator experiecne
        if (CastAsFloat(txtOpertor.Text) <= 0)
        {
            lblMsg.Text = "Please enter experience(operator).";
            txtOpertor.Focus();
            return;
        }
        // Rank experiecne
        if (CastAsFloat(txtRank.Text) <= 0)
        {
            lblMsg.Text = "Please enter experience(Rank).";
            txtRank.Focus();
            return;
        }
        // Tanker experiecne
        if (CastAsFloat(txtTanker.Text) <= 0)
        {
            lblMsg.Text = "Please enter experience(On selected type of tanker).";
            txtTanker.Focus();
            return;
        }
        // All experiecne
        if (CastAsFloat(txtAllTanker.Text) <= 0)
        {
            lblMsg.Text = "Please enter experience(All tankers).";
            txtAllTanker.Focus();
            return;
        }

        #region -------- IMPLEMENTATION OF WEB SERVICE 1.0 CREW DOCUMENTS UPLOAD ( OBSOLETED NOW ) ------------------


        com.ocimf_sire.wsv2.OcimfServices ser = new com.ocimf_sire.wsv2.OcimfServices();
        com.ocimf_sire.wsv2.OcimfWsResponse resp = new com.ocimf_sire.wsv2.OcimfWsResponse();
        resp = ser.StartWebServiceSession(AccountId, UserId, Password);
        
        if (resp.ResultCode == 1)
        {
            string AuthKey = resp.DataXml.Replace("<Token>", "").Replace("</Token>", "").Trim();
            string Request = "<Request>" +
                             "<Dates rangeStart=\"2000-03-01T16:00:00\" rangeEnd=\"" + (DateTime.Today.Year +1).ToString() + "-03-01T17:00:00\" />" +
                             "<Vessel imo=\"" + lblImo.Text.Trim() + "\" />" +
                             "</Request>";
            resp = ser.InvokeMethod(AuthKey, "SIRE.GetCrewIndex", "2.0", Request);
            if (resp.ResultCode == 1)
            {
                int st = resp.DataXml.IndexOf("IdxDoc guid") + 13;
                int end = resp.DataXml.IndexOf("\"", st) - st;
                string guid = resp.DataXml.Substring(st, end);
                //---------
                Request="<Request>" +
                        "<Crew Doc=\"" + guid + "\" Type=\"" + RadioButtonList1.SelectedValue + "Crew\" Action=\"Save\">" +
                        "<Attribute Key=\"rank\">" + hfdrank.Value + "</Attribute>" +
                        "<Attribute Key=\"nationality\">" + hfdnationality.Value + "</Attribute>" +
                        "<Attribute Key=\"certcomp\">" + ddlCertComp.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"issuecountry\">" + ddlIssuingCountry.SelectedItem.Text  + "</Attribute>" +
                        "<Attribute Key=\"adminaccept\">" + ddlAdminAccept.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"tankercert\">" + ddlTankerCert.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"stcwvpara\">" + ddlSTCW.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"radioqual\">" + ddlRadio.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"yearsoperator\">" + txtOpertor.Text + "</Attribute>" +
                        "<Attribute Key=\"yearsrank\">" + txtRank.Text + "</Attribute>" +
                        "<Attribute Key=\"yearstankertype\">" + txtTanker.Text + "</Attribute>" +
                        "<Attribute Key=\"yearsalltankertypes\">" + txtAllTanker.Text + "</Attribute>" +
                        "<Attribute Key=\"datejoinedvessel\">" + DateTime.Parse(txtJoinVessel.Text).ToString("yyyy-MM-dd") + "</Attribute>" +
                        "<Attribute Key=\"englishprof\">" + ddlEngProf.SelectedValue + "</Attribute>" +
                        "</Crew>" +
                        "</Request>";
                resp=ser.InvokeMethod(AuthKey, "SIRE.SaveCrewRecord","2.0",Request);
                if (resp.DataXml.Contains("action=\"error\""))
                {
                    st = resp.DataXml.IndexOf("message=\"") + 9;
                    end = resp.DataXml.IndexOf("\"", st) - st;
                    string mess = resp.DataXml.Substring(st, end);
                    lblMsg.Text = "Unable to upload record. Error : " + mess;
                }
                else
                {
                    Budget.getTable("exec dbo.SireCrewUploadProc " + hfdcrewid.Value + "," +
                             Session["loginid"].ToString() + ",'" +
                             ddlCertComp.SelectedValue + "','" +
                             ddlIssuingCountry.SelectedValue + "','" +
                             ddlAdminAccept.SelectedValue + "','" +
                             ddlTankerCert.SelectedValue + "','" +
                             ddlSTCW.SelectedValue + "','" +
                             ddlRadio.SelectedValue + "','" +
                             ddlEngProf.SelectedValue + "'," + VesselId.ToString());
                    LoadMatrix();
                    lblMsg.Text = "Upload successfully.";
                }
                //---------
            }
        }

        //sireindexrequestinfoParametersVesselInfo vsl = new sireindexrequestinfoParametersVesselInfo();
        //SireWebServicesv1011 S = new com.ocimf_sire.ws.SireWebServicesv1011();
        ////---------------
        //string Request = "<?xml version=\"1.0\"?>" +
        //                "<Request>" +
        //                "<Vessel imo=\"" + lblImo.Text.Trim() + "\"/>" +
        //                "<User name=\"" + UserId + "\" password=\"" + Password + "\" />" +
        //                "</Request>";
        //XmlNode res = S.GetCrewIndex(Request);
        ////---------------
        //if (res.Attributes["status"].Value.ToLower() == "error")
        //{
        //    lblMsg.Text = "Username OR passsword is incorrect.";
        //    txtUserId.Focus();
        //    return;
        //}
        ////---------------
        //if (res.Attributes["status"].Value.ToLower() == "NoMatchForImo")
        //{
        //    lblMsg.Text = "IMO# is not mathching.";
        //    return;
        //}
        ////---------------
        //string GUID = "";
        //try
        //{
        //    GUID = res.SelectSingleNode("Index/IdxDoc").Attributes["guid"].Value;
        //}
        //catch { GUID = ""; } 

        //if (GUID=="")
        //{
        //    lblMsg.Text = "Please visit \"Vessel Crew Management\" page for selected page on OCIMF-SIRE website.";
        //    return;
        //}
        ////---------------
        //Request = "<?xml version=\"1.0\"?>" +
        //                "<Request>" +
        //                "<Document guid=\"" + GUID + "\"/>" +
        //                "<Rendition mime=\"text/xml\" />" +
        //                "<User name=\"" + UserId + "\" password=\"" + Password + "\" />" +
        //                "</Request>";
        //res = S.GetCrewDocument(Request);
        //GUID = res.SelectSingleNode("Document/DocMeta").Attributes["guid"].Value;

        //Request = "<?xml version=\"1.0\"?>" +
        //                "<Request>" +
        //                "<User name=\"" + UserId + "\" password=\"" + Password + "\" />" +
        //                "<Crew Doc=\"" + GUID + "\" Type=\"" + RadioButtonList1.SelectedValue + "Crew\" Action=\"Save\">" +
        //                "<Attribute Key=\"rank\">" + hfdrank.Value + "</Attribute>" +
        //                "<Attribute Key=\"nationality\">" + hfdnationality.Value + "</Attribute>" +
        //                "<Attribute Key=\"certcomp\">" + ddlCertComp.SelectedValue + "</Attribute>" +
        //                "<Attribute Key=\"issuecountry\">" + ddlIssuingCountry.SelectedItem.Text  + "</Attribute>" +
        //                "<Attribute Key=\"adminaccept\">" + ddlAdminAccept.SelectedValue + "</Attribute>" +
        //                "<Attribute Key=\"tankercert\">" + ddlTankerCert.SelectedValue + "</Attribute>" +
        //                "<Attribute Key=\"stcwvpara\">" + ddlSTCW.SelectedValue + "</Attribute>" +
        //                "<Attribute Key=\"radioqual\">" + ddlRadio.SelectedValue + "</Attribute>" +
        //                "<Attribute Key=\"yearsoperator\">" + txtOpertor.Text + "</Attribute>" +
        //                "<Attribute Key=\"yearsrank\">" + txtRank.Text + "</Attribute>" +
        //                "<Attribute Key=\"yearstankertype\">" + txtTanker.Text + "</Attribute>" +
        //                "<Attribute Key=\"yearsalltankertypes\">" + txtAllTanker.Text + "</Attribute>" +
        //                "<Attribute Key=\"datejoinedvessel\">" + DateTime.Parse(txtJoinVessel.Text).ToString("yyyy-MM-dd") + "</Attribute>" +
        //                "<Attribute Key=\"englishprof\">" + ddlEngProf.SelectedValue + "</Attribute>" +
        //                "</Crew>" +
        //                "</Request>";

        //res = S.SaveCrewRecord(Request);

        //if (res.Attributes["action"].Value.ToLower() == "saved")
        //{
        //    Budget.getTable("exec dbo.SireCrewUploadProc " + hfdcrewid.Value + "," +
        //             Session["loginid"].ToString() + ",'" +
        //             ddlCertComp.SelectedValue + "','" +
        //             ddlIssuingCountry.SelectedValue + "','" +
        //             ddlAdminAccept.SelectedValue + "','" +
        //             ddlTankerCert.SelectedValue + "','" +
        //             ddlSTCW.SelectedValue + "','" +
        //             ddlRadio.SelectedValue + "','" +
        //             ddlEngProf.SelectedValue + "'," + VesselId.ToString());
        //    LoadMatrix();
        //    lblMsg.Text = "Record uploaded successfully";
        //}
        //else
        //{
        //    lblMsg.Text = "Unable to upload record";
        //}

#endregion

    }
    protected void CrewList_Click(object sender, EventArgs e)
    {
        string AccountId = txtAccountId.Text.Trim();
        string UserId = txtUserId.Text.Trim();
        string Password = txtPassword.Text.Trim();

        // IMO number
        if (lblImo.Text.Trim() == "")
        {
            lblMsg.Text = "Selected vessel having no IMO Number.";
            return;
        }
        // AccountId
        if (txtAccountId.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid AccountId.";
            txtAccountId.Focus();
            return;
        }
        // UserId
        if (txtUserId.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid OCIMF User Id.";
            txtUserId.Focus();
            return;
        }
        // Password
        if (txtPassword.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter valid password.";
            txtPassword.Focus();
            return;
        }

        //  ---------- START IMPLEMETIOAN FOR SIRE 2.0 WEB SERVICE

        com.ocimf_sire.wsv2.OcimfServices ser = new com.ocimf_sire.wsv2.OcimfServices();
        com.ocimf_sire.wsv2.OcimfWsResponse resp = new com.ocimf_sire.wsv2.OcimfWsResponse();
        resp = ser.StartWebServiceSession(AccountId, UserId, Password);
        if (resp.ResultCode == 1)
        {
            string AuthKey = resp.DataXml.Replace("<Token>", "").Replace("</Token>", "").Trim();
            string Request = "<Request>" +
                             "<Dates rangeStart=\"2000-03-01T16:00:00\" rangeEnd=\"" + (DateTime.Today.Year +1).ToString() + "-03-01T17:00:00\" />" +
                             "<Vessel imo=\"" + lblImo.Text.Trim() + "\" />" +
                             "</Request>";
            resp = ser.InvokeMethod(AuthKey, "SIRE.GetCrewIndex", "2.0", Request);
            if (resp.ResultCode == 1)
            {
                int st = resp.DataXml.IndexOf("IdxDoc guid") + 13;
                int end = resp.DataXml.IndexOf("\"", st) - st;
                string guid = resp.DataXml.Substring(st, end);
                Request = "<Request>" +
                          "<Document guid=\"" + guid + "\" />" +
                          "<Rendition mime=\"text/xml\" />" +
                          "</Request>";

                resp = ser.InvokeMethod(AuthKey, "SIRE.GetCrewDocument", "2.0", Request);

                if (resp.ResultCode == 1)
                {
                    st = resp.DataXml.IndexOf("<Document ");
                    end = resp.DataXml.IndexOf("</Document>") + 11 - st;
                    string doc = resp.DataXml.Substring(st, end);
                    Session["CrewXML"] = doc;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('CrewUploadPopUp.aspx?Type=U&width=900&height=400');", true);  
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "na", "alert('Unable to Login. Error : " + resp.ErrorMessage + "');", true);
        }

        //-------------------------------------------------------------------
 
        #region ---------- START IMPLEMETIOAN FOR SIRE 1.0 WEB SERVICE [ OBSOLETED NOW ]  ------------------

        //sireindexrequestinfoParametersVesselInfo vsl = new sireindexrequestinfoParametersVesselInfo();
        //SireWebServicesv1011 S = new com.ocimf_sire.ws.SireWebServicesv1011();
        ////---------------
        //string Request = "<?xml version=\"1.0\"?>" +
        //                "<Request>" +
        //                "<Vessel imo=\"" + lblImo.Text.Trim() + "\"/>" +
        //                "<User name=\"" + UserId + "\" password=\"" + Password + "\" />" +
        //                "</Request>";
        //XmlNode res = S.GetCrewIndex(Request);
        ////---------------
        //if (res.Attributes["status"].Value.ToLower() == "error")
        //{
        //    lblMsg.Text = "Username OR passsword is incorrect.";
        //    txtUserId.Focus();
        //    return;
        //}
        ////---------------
        //if (res.Attributes["status"].Value.ToLower() == "NoMatchForImo")
        //{
        //    lblMsg.Text = "IMO# is not mathching.";
        //    return;
        //}
        ////---------------
        //string GUID = "";
        //string CrewList = "";
        //try
        //{
        //    GUID = res.SelectSingleNode("Index/IdxDoc").Attributes["guid"].Value;
        //}
        //catch { GUID = ""; }

        //if (GUID == "")
        //{
        //    lblMsg.Text = "Crew list not available for selected vessel on server.";
        //    return;
        //}
        ////---------------
        //Request = "<?xml version=\"1.0\"?>" +
        //                "<Request>" +
        //                "<Document guid=\"" + GUID + "\"/>" +
        //                "<Rendition mime=\"text/xml\" />" +
        //                "<User name=\"" + UserId + "\" password=\"" + Password + "\" />" +
        //                "</Request>";
        //res = S.GetCrewDocument(Request);
        //Session["CrewXML"] = res.InnerText;

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('CrewUploadPopUp.aspx?Type=U&width=900&height=400');", true);  

#endregion
    }
    private string GetSum(DataTable dt,String Column,string RowFilter)
    {
        double res=0;
        DataView dv=dt.DefaultView;
        dv.RowFilter=RowFilter;
        DataTable dtTo=dv.ToTable();
        foreach (DataRow dr in dtTo.Rows)
        {
            res = res + double.Parse("0" + dr[Column].ToString()); 
        }
        return res.ToString(); 
    }
}

