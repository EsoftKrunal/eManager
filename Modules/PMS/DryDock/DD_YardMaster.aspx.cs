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

public partial class DD_YardMaster : System.Web.UI.Page
{
    public int YardId
    {
        set { ViewState["YardId"] = value; }
        get { return Common.CastAsInt32(ViewState["YardId"]); }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCalMag.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            LoadYards();
            BindCountry();
        }
    }
    //------------- Add/ Edit Docking Yardegory Section
    protected void LoadYards()
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT * FROM DD_YardMaster Order By YardName";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptJobYards.DataSource = dtGroups;
        rptJobYards.DataBind();
    }
    protected void BindCountry()
    {
        ddlCountry.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT [CountryId],[CountryCode],[CountryName]  FROM [dbo].[Country] WHERE [StatusId] = 'A' ORDER BY [CountryName]");
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataValueField = "CountryId";
        ddlCountry.DataBind(); 
        ddlCountry.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    protected void btnAddYard_Click(object sender, EventArgs e)
    {
        YardId = 0;
        btnEditYard.Visible = false;
        dv_JobYardegory.Visible = true;
        txtYardName.Focus();
        LoadYards();
    }
    protected void btnEditYard_Click(object sender, EventArgs e)
    {
        // ContactPerson,PortName,CountryId
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_YardMaster WHERE YardId=" + YardId);
        if (dt.Rows.Count > 0)
        {
            txtYardName.Text = dt.Rows[0]["YardName"].ToString();
            txtYardAddress.Text = dt.Rows[0]["Address"].ToString();
            txtYardContact.Text = dt.Rows[0]["ContactNo"].ToString();
            txtYardEmail.Text = dt.Rows[0]["Email"].ToString(); 
            txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
            txtPortName.Text = dt.Rows[0]["PortName"].ToString();
            ddlCountry.SelectedValue = (dt.Rows[0]["CountryId"].ToString() == "" ? "0" : dt.Rows[0]["CountryId"].ToString());
        }
        dv_JobYardegory.Visible = true;
        txtYardName.Focus();
    }
    protected void btnSelectYard_Click(object sender, EventArgs e)
    {
        YardId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        btnEditYard.Visible = true;
        LoadYards();
    }
    protected void btnSaveYard_Click(object sender, EventArgs e)
    {
        if (txtYardName.Text.Trim() == "")
        {
            txtYardName.Focus();
            lblCalMag.Text = "Please enter Yard Name.";
            return;
        }
        if (txtYardContact.Text.Trim() == "")
        {
            txtYardContact.Focus();
            lblCalMag.Text = "Please enter Contact#.";
            return;
        }
        if (txtYardEmail.Text.Trim() == "")
        {
            txtYardEmail.Focus();
            lblCalMag.Text = "Please enter Email.";
            return;
        }

        if (txtContactPerson.Text.Trim() == "")
        {
            txtContactPerson.Focus();
            lblCalMag.Text = "Please enter Contact Person.";
            return;
        }

        if (txtPortName.Text.Trim() == "")
        {
            txtPortName.Focus();
            lblCalMag.Text = "Please enter Port Name.";
            return;
        }

        if (ddlCountry.SelectedIndex == 0)
        {
            ddlCountry.Focus();
            lblCalMag.Text = "Please select country.";
            return;
        }

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT YardName FROM DD_YardMaster WHERE YardName='" + txtYardName.Text.Trim() + "' AND YardId <> " + YardId);

        if (dt1.Rows.Count > 0)
        {
            txtYardName.Focus();
            lblCalMag.Text = "Please check! Yard Name already exists.";
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateYardMaster]");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
               new MyParameter("@YardId", YardId),
               new MyParameter("@YardName", txtYardName.Text.Trim()),
               new MyParameter("@Address", txtYardAddress.Text.Trim()),
               new MyParameter("@ContactNo", txtYardContact.Text.Trim()),
               new MyParameter("@Email", txtYardEmail.Text.Trim()),
               new MyParameter("@ContactPerson", txtContactPerson.Text.Trim()),
               new MyParameter("@PortName", txtPortName.Text.Trim()),
               new MyParameter("@CountryId ", ddlCountry.SelectedValue.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                LoadYards();
                lblCalMag.Text = "Yard added/ edited successfully.";
            }
            else
            {
                lblCalMag.Text = "Unable to add/ edit Yard.Error :" + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lblCalMag.Text = "Unable to add/ edit Yard.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCancelYard_Click(object sender, EventArgs e)
    {
        
        txtYardName.Text = "";
        txtYardAddress.Text = "";
        txtYardContact.Text = "";
        txtYardEmail.Text = "";
        txtContactPerson.Text = "";
        txtPortName.Text = "";
        ddlCountry.SelectedIndex = 0;
        dv_JobYardegory.Visible = false;
    }
}