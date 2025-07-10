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
using com.ocimf_sire.ws;
using System.Xml;  

public partial class CrewSireUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";  
        if (!IsPostBack)
        {
            BindVessel();
            ddlIssuingCountry.DataSource = getCountry();
            ddlIssuingCountry.DataTextField = "CountryName";
            ddlIssuingCountry.DataValueField = "CountryId";
            ddlIssuingCountry.DataBind();
                
        }
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddlVessel.DataValueField = "VesselId";
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataSource = ds;
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    public void LoadMatrix()
    {
        GridView1.DataSource = Budget.getTable("Exec dbo.SireCrewList " + ddlVessel.SelectedValue + ",'" + RadioButtonList1.SelectedValue + "'");
        GridView1.SelectedIndex = -1; 
        GridView1.DataBind();  
    }
    public DataTable getCountry()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        return obj.ResultSet.Tables[0];
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Budget.getTable("select LRIMONumber from vessel where vesselid=" + ddlVessel.SelectedValue).Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblImo.Text = dt.Rows[0][0].ToString();
        }
        else
        {
            lblImo.Text = "";
        }
        LoadMatrix();
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
        string rankexp= ((HiddenField)GridView1.SelectedRow.FindControl("hfdrankexp")).Value;
        string tankertypeexp= ((HiddenField)GridView1.SelectedRow.FindControl("hfdtankertypeexp")).Value;
        string alltypeexp = ((HiddenField)GridView1.SelectedRow.FindControl("hfdalltypeexp")).Value;
        string monthtour = ((HiddenField)GridView1.SelectedRow.FindControl("hfdmonthtour")).Value;
        string signondate = ((HiddenField)GridView1.SelectedRow.FindControl("hfdsignondate")).Value;
        string engprof = ((HiddenField)GridView1.SelectedRow.FindControl("hfdengprof")).Value;


        hfdcrewid.Value = crewid;
        hfdrank.Value = rank;
        hfdnationality.Value = nationality;
        ddlCertComp.SelectedIndex = 0;
        ddlIssuingCountry.SelectedIndex = 0;
        ddlAdminAccept.SelectedIndex = 0;
        ddlTankerCert.SelectedIndex = 0;
        ddlSTCW.SelectedIndex = 0;
        ddlRadio.SelectedIndex = 0;

        txtOpertor.Text = operatorexp;
        txtRank.Text = rankexp;
        txtTanker.Text = tankertypeexp;
        txtAllTanker.Text = alltypeexp;
        txtJoinVessel.Text = DateTime.Parse (signondate).ToString("dd MMM yyyy") ;
        lblMonth.Text = monthtour + " month on vessel.";   
        ddlEngProf.SelectedValue =engprof;  
    }
    private float CastAsFloat(object Data)
    {
        float result = 0;
        try
        {
            result = float.Parse(Math.Round(decimal.Parse(Data.ToString())).ToString());
        }
        catch 
        {
            result = 0;
        }
        return result;
    }
    protected void Upload_Click(object sender, EventArgs e)
    {
        // vessel check
        if (ddlVessel.SelectedIndex <= 0)
        {
            lblMsg.Text="Please select vessel.";   
            ddlVessel.Focus();
            return;    
        }
        // IMO number
        if (lblImo.Text.Trim()=="")
        {
            lblMsg.Text="Selected vessel having no IMO Number.";   
            ddlVessel.Focus();
            return;    
        }
        // Select a member in grid
        if (GridView1.SelectedIndex < 0 )
        {
            lblMsg.Text="Please select a crew member to upload.";   
            ddlVessel.Focus();
            return;    
        }
        // Check rank is matched
        if (hfdrank.Value.Trim()=="")
        {
            lblMsg.Text = "Please match the rank of selected crew member from sire rank.";
            ddlVessel.Focus();
            return;
        }   
        // Certificate of competency
        if (ddlCertComp.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select certificate of competency.";   
            ddlVessel.Focus();
            return;    
        }
        // Isssuing counrty
        if (ddlIssuingCountry.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select issuing country.";   
            ddlVessel.Focus();
            return;    
        }
        // Administrative accespetance
        if (ddlAdminAccept.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select administrative acceptance.";   
            ddlVessel.Focus();
            return;    
        }
        // Tanker certification
        if (ddlTankerCert.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select tanker certificate.";   
            ddlVessel.Focus();
            return;    
        }
        // Tanker certification
        if (ddlSTCW.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select STCW V Para.";   
            ddlVessel.Focus();
            return;    
        }
        // Radio qualification
        if (ddlRadio.SelectedIndex <= 0 )
        {
            lblMsg.Text="Please select radion qualification.";   
            ddlVessel.Focus();
            return;    
        }
        // Operator experiecne
        if ( CastAsFloat(txtOpertor.Text)<=0)
        {
            lblMsg.Text="Please enter experience(operator).";   
            ddlVessel.Focus();
            return;    
        }
        // Rank experiecne
        if (CastAsFloat(txtRank.Text)<=0)
        {
            lblMsg.Text="Please enter experience(Rank).";   
            ddlVessel.Focus();
            return;    
        }
        // Tanker experiecne
        if ( CastAsFloat(txtTanker.Text)<=0)
        {
            lblMsg.Text="Please enter experience(On selected type of tanker).";   
            ddlVessel.Focus();
            return;    
        }
        // All experiecne
        if ( CastAsFloat(txtAllTanker.Text)<=0)
        {
            lblMsg.Text="Please enter experience(All tankers).";   
            ddlVessel.Focus();
            return;    
        }


        sireindexrequestinfoParametersVesselInfo vsl = new sireindexrequestinfoParametersVesselInfo();
        SireWebServicesv1011 S = new com.ocimf_sire.ws.SireWebServicesv1011();

        string Request = "<?xml version=\"1.0\"?>" +
                        "<Request>" +
                        "<Vessel imo=\"" + lblImo.Text.Trim() + "\"/>" +
                        "<User name=\"24000562\" password=\"33327202\" />" +
                        "</Request>";
        XmlNode res = S.GetCrewIndex(Request);
        string GUID = "";
        GUID = res.SelectSingleNode("Index/IdxDoc").Attributes["guid"].Value;
        Request = "<?xml version=\"1.0\"?>" +
                        "<Request>" +
                        "<Document guid=\"" + GUID + "\"/>" +
                        "<Rendition mime=\"text/xml\" />" +
                        "<User name=\"24000562\" password=\"33327202\" />" +
                        "</Request>";
        res = S.GetCrewDocument(Request);
        GUID = res.SelectSingleNode("Document/DocMeta").Attributes["guid"].Value;

        Request = "<?xml version=\"1.0\"?>" +
                        "<Request>" +
                        "<User name=\"24000562\" password=\"33327202\" />" +
                        "<Crew Doc=\"" + GUID + "\" Type=\"" + RadioButtonList1.SelectedValue +"Crew\" Action=\"Save\">" +
                        "<Attribute Key=\"rank\">" + hfdrank.Value + "</Attribute>" +
                        "<Attribute Key=\"nationality\">" + hfdnationality.Value + "</Attribute>" +
                        "<Attribute Key=\"certcomp\">" + ddlCertComp.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"issuecountry\">" + ddlIssuingCountry.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"adminaccept\">" + ddlAdminAccept.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"tankercert\">" + ddlTankerCert.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"stcwvpara\">" + ddlSTCW.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"radioqual\">" + ddlRadio.SelectedValue + "</Attribute>" +
                        "<Attribute Key=\"yearsoperator\">" + txtOpertor.Text + "</Attribute>" +
                        "<Attribute Key=\"yearsrank\">" + txtRank.Text + "</Attribute>" +
                        "<Attribute Key=\"yearstankertype\">" + txtTanker.Text + "</Attribute>" +
                        "<Attribute Key=\"yearsalltankertypes\">" + txtAllTanker.Text + "</Attribute>" +
                        "<Attribute Key=\"datejoinedvessel\">" + DateTime.Parse ( txtJoinVessel.Text).ToString("yyyy dd mm") + "</Attribute>" +
                        "<Attribute Key=\"englishprof\">" + ddlEngProf.SelectedValue + "</Attribute>" +
                        "</Crew>" +
                        "</Request>";

        res = S.SaveCrewRecord(Request);  

        if (res.Attributes["action"].Value.ToLower() == "saved")
        {

            lblMsg.Text = "Record uploaded successfully";
        }
        else
        {
            lblMsg.Text = "Unable to upload record";
        }

    }
}
