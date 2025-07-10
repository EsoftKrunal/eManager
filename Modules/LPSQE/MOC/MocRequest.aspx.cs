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

public partial class HSSQE_MOC_MocRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {
		ddlFYear.Items.Add(new ListItem(" Select ","0"));
	    for(int i=DateTime.Today.Year;i >=2015;i--)
		{
			ddlFYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
		}
		ddlFYear.SelectedIndex=1;
            BindMocRequest();
        }
    }
    public void BindMocRequest()
    {
        string SQL = "SELECT MR.*, " +
                     "CASE WHEN [VesselCode] <> '' THEN (SELECT VesselName From Vessel WHERE VesselCode = MR.VesselCode) " +
                     "     WHEN [OfficeId] <> 0 THEN (SELECT OfficeName From Office WHERE OfficeId = MR.OfficeId)  " +
                     "ELSE '' END AS VesselOffice , U.FirstName  + ' ' + U.LastName as RequestedByName ,(case when Status='O' THEN 'Open' ELSE 'Closed' END) as StatusName " +
                     "FROM  Moc_Request MR left join USERLOGIN U ON MR.REQUESTEDBY=U.LOGINID ";
	if(ddlFYear.SelectedIndex > 0)
	{
		SQL +=" where year(MOCDate)=" + ddlFYear.SelectedValue;
	}
        SQL +=" order by MOCNumber";
	DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptMOC.DataSource = dt;
        rptMOC.DataBind();

    }

    protected void ddlFYear_onSelectIndexChanged(object sender, EventArgs e)
    {
	BindMocRequest();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int MocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "window.open('EditMOC.aspx?MOCId='+ " + MocId + ", '_blank', '');", true);
    }
    protected void btnCreateNewMOC_Click(object sender, EventArgs e)
    {
        dv_AddNewMOC.Visible = true;
        btnSaveNew.Visible = true;
        btnNext.Visible = false;
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        string Impact = "";
        foreach( ListItem lst in cbImpact.Items )
        {
            if(lst.Selected)
            {
               Impact += lst.Value + "," ;
            }
        }

        if(Impact.Trim().Length > 0)
        {
            Impact = Impact.Remove(Impact.LastIndexOf(','));
        }

        if(Impact.Trim() == "")
        {
            lblMsg.Text = "Please select Impact.";
            cbImpact.Focus();
            return;
        }


        try
        {
            
            Common.Set_Procedures("SNQ.dbo.MOC_CreateMocRequest");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
                new MyParameter("@Topic", txtTopic.Text.Trim()),                        
                new MyParameter("@Source",  ddlSource.SelectedValue.Trim()),
                new MyParameter("@MOCDate", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
                new MyParameter("@VesselCode", ((ddlSource.SelectedValue.Trim() == "Vessel") ? ddlVessel_Office.SelectedValue.Trim() : "")),
                new MyParameter("@OfficeId", ((ddlSource.SelectedValue.Trim() == "Office") ? ddlVessel_Office.SelectedValue.Trim() : "")),
                new MyParameter("@RequestBy", Common.CastAsInt32(Session["loginid"])),
                new MyParameter("@RequestOn", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
                new MyParameter("@Impact", Impact),
                new MyParameter("@ReasonForChange", txtReasonforChange.Text.Trim()),
                new MyParameter("@ProposedTimeline", txtPropTL.Text.Trim()),
                new MyParameter("@DescrOfChange", txtDescr.Text.Trim()),
                new MyParameter("@ReferenceKey", "")
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds); 

            if (res)
            {
                lblMsg.Text = "MOC Created successfully.";
                btnSaveNew.Visible = false;
                btnNext.Visible = true;
                ViewState["NEWMOCID"] =Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
               lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }

    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "Vessel")
        {
            Load_vessel(); 
        }
        else if (ddlSource.SelectedValue == "Office")
        {
            Load_office();
        }
        else
        {
            ddlVessel_Office.Items.Clear();
            ddlVessel_Office.Items.Insert(0, new ListItem(" < Select >", "0"));
        }
    }
    protected void btnCloseNew_Click(object sender, EventArgs e)
    {
        ddlSource.SelectedIndex = 0;
        ddlVessel_Office.Items.Clear();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select >", "0"));
        //txtMOCDate.Text = "";
        cbImpact.SelectedIndex = -1;
        txtReasonforChange.Text = "";
        txtDescr.Text = "";
        txtPropTL.Text = "";
        BindMocRequest();
        dv_AddNewMOC.Visible = false;

    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from shipsoft.dbo.vessel where vesselstatusid<>2 and vesselid not in (41,43,44) order by vesselname");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "VesselName";
        ddlVessel_Office.DataValueField = "VesselCode";
        ddlVessel_Office.DataBind();

        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select >", "0"));
    }
    private void Load_office()
    {
        DataSet dt = Budget.getTable("SELECT OfficeId, OfficeName, OfficeCode FROM ShipSoft.[dbo].[Office] Order By OfficeName");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "OfficeName";
        ddlVessel_Office.DataValueField = "OfficeId";
        ddlVessel_Office.DataBind();

        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select >", "0"));
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnCloseNew_Click(sender,e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('EditMoc.aspx?MOCId=" + ViewState["NEWMOCID"].ToString() + "','');", true);
    }
}