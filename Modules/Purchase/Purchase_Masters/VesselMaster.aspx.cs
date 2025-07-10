using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class Registers : System.Web.UI.Page
{
    public int SelectedVessId
    {
        get { return Common.CastAsInt32(hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public string VessId
    {
        get { return Convert.ToString(ViewState["SupplierID"]); }
        set { ViewState["SupplierID"] = value.ToString(); }        
    }
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblmsg.Text = "";
        #region --------- USER RIGHTS MANAGEMENT -----------
        AuthenticationManager authPR = new AuthenticationManager(1062, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        //AuthenticationManager authRFQList = new AuthenticationManager(0, 0, ObjectType.Page);
        try
        {
            
            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            
            

            //authRFQList = new AuthenticationManager(227, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            BindVessel();
            BindOwner();
        }
    }
    #endregion
    protected void radNWC_OnSelectIndexChanged(object sender,EventArgs e)
    {
        BindVessel();
    }
    private void BindVessel()
    {
        string str = "SELECT VW_sql_tblSMDPRVessels.*, VW_tblSMDVessels.*, AccountCompany.[Company Name] " +
                        " FROM VW_sql_tblSMDPRVessels  INNER JOIN VW_tblSMDVessels ON VW_sql_tblSMDPRVessels.ShipID = VW_tblSMDVessels.ShipID "+
                        " INNER JOIN [dbo].AccountCompany ON VW_sql_tblSMDPRVessels.Company = AccountCompany.Company";
        if (radNWC.SelectedIndex == 1)
        {
            str += " WHERE AccountCompany.COMPANY='NWC' order by VW_sql_tblSMDPRVessels.SHIPID";
        }
        else
        {
            str += " WHERE AccountCompany.COMPANY<>'NWC' order by VW_sql_tblSMDPRVessels.SHIPID";
        }
        DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtVessel != null)
        {
            RptVessel.DataSource = dtVessel;
            RptVessel.DataBind();
            if (dtVessel.Rows.Count <= 0)
            {
                lblmsg.Text = "No Data Found.";
            }
        }
        else
        {
            lblmsg.Text = "No Data Found.";
        }

    }
    public void BindOwner()
    {
        string sql = "select * from AccountCompany with(nolock) ";
        DataTable Dt= Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            ddlOwner.DataSource = Dt;
            ddlOwner.DataTextField = "Company Name";
            ddlOwner.DataValueField = "Company";
            ddlOwner.DataBind();
            ddlOwner.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    public void ClearFields()
    {
        txtShipID.Text = "";
        txtShipName.Text = "";
        ddlOwner.SelectedIndex = 0;
        txtShipNo.Text = "";
        txtEmail.Text = "";
        txtShipFax.Text = "";
        txtInmarsat.Text = "";
        chkActive.Checked = false;

        txtBuilt.Text = "";
        txtDWT.Text = ""; txtShipID.Text = "";
        txtGRT.Text = "";
        txtNRT.Text = "";
        txtRegistery.Text = ""; txtShipID.Text = "";
        txtYearBuilt.Text = "";
        txtHullNo.Text = "";
        txtHAndM.Text = "";
        txtNWC.Text = "";

        txtPAndI.Text = "";
        txtRegistery2.Text = "";
        txtOfficialNo.Text = "";
        txtCallSign.Text = "";
        txtMainEngine.Text = "";
        txtMainEngineNo.Text = "";
        txtAUXEngine.Text = "";        
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        ClearFields();
        dvAddVessel.Visible = true;
    }
    protected void imgSave_OnClick(object sender, EventArgs e)
    {
        if (txtShipID.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter Ship ID.";
            txtShipID.Focus(); return;
        }
        if (txtShipName.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter Ship Name.";
            txtShipName.Focus(); return;
        }
        if (ddlOwner.SelectedIndex == 0)
        {
            lblmsg.Text = "Please select owner.";
            txtShipNo.Focus(); return;
        }
        if (txtShipNo.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter Ship Number.";
            txtShipNo.Focus(); return;
        }
        if (txtShipNo.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter Ship Number.";
            txtShipNo.Focus(); return;
        }
        //if (ddlOwner.SelectedItem.Text.Trim().Contains("NWC"))
        //{
        //    txtNWC.Text = "";
        //}
        //else
        //{
        //    if (txtNWC.Text.Trim() == "")
        //    {
        //        lblmsg.Text = "Please enter NWC Code.";
        //        txtNWC.Focus(); return;
        //    }
        //    else
        //    {
        //        string sqlChk = "select * from dbo.sql_tblSMDPRVessels  where ShipId='" + txtNWC.Text.Trim() + "'  AND Company='NWC'";
        //        DataTable dtChk1 = Common.Execute_Procedures_Select_ByQuery(sqlChk);
        //        if (dtChk1.Rows.Count != 1)
        //        {
        //            lblmsg.Text = "NWC Vessel not exists.";
        //            txtNWC.Focus(); return;
        //        }
        //    }
        //}
        

        if (VessId == "")
        {
            string sqlChk = "select * from dbo.Vessel with(nolock)  where VesselCode='" + txtShipID.Text + "'";
            DataTable dtChk = Common.Execute_Procedures_Select_ByQuery(sqlChk);

            if (dtChk != null)
            {
                if (dtChk.Rows.Count > 0)
                {
                    lblmsg.Text = "Ship ID already exist in the database.";
                    txtShipID.Focus(); return;
                }
            }
        }


        string Mode = "";
        if (VessId == "")
            Mode = "Add";
        else
            Mode = "Edit";
        Common.Set_Procedures("sp_InsertUpdateVesselDetails");
        Common.Set_ParameterLength(26);
        Common.Set_Parameters(
                            new MyParameter("@Mode", Mode),
                            new MyParameter("@ShipID", txtShipID.Text.Trim()),
                            new MyParameter("@SHipName", txtShipName.Text),
                            new MyParameter("@Company", ddlOwner.SelectedValue),
                            new MyParameter("@VesselNo", Common.CastAsInt32( txtShipNo.Text)),
                            new MyParameter("@email", txtEmail.Text),
                            new MyParameter("@Active", ((chkActive.Checked)?'Y':'N')),
                            new MyParameter("@NWC", txtNWC.Text),
                            new MyParameter("@ShipFax", txtShipFax.Text),
                            new MyParameter("@Inmarsat", txtInmarsat.Text),
                            new MyParameter("@Built", txtBuilt.Text),
                            new MyParameter("@DWTMetricTons", Common.CastAsDecimal( txtDWT.Text)),
                            new MyParameter("@GRT", txtGRT.Text),
                            new MyParameter("@NRT", txtNRT.Text),
                            new MyParameter("@ClassNo", txtRegistery.Text) ,                           
                            new MyParameter("@YardBuilt", txtYearBuilt.Text),
                            new MyParameter("@HullNo", txtHullNo.Text),
                            new MyParameter("@HandM", txtHAndM.Text),
                            new MyParameter("@PandI", txtPAndI.Text),
                            new MyParameter("@Registry", txtRegistery2.Text),
                            new MyParameter("@OfficialNo", txtOfficialNo.Text),
                            new MyParameter("@CallSign", txtCallSign.Text),
                            new MyParameter("@MainEngine", txtMainEngine.Text),
                            new MyParameter("@mainEngineNo", txtMainEngineNo.Text),
                            new MyParameter("@AuxiliaryEngine", txtAUXEngine.Text),
                            new MyParameter("@ExName", txtEXNo.Text)

            );
        DataSet Ds = new DataSet();
        Boolean res = Common.Execute_Procedures_IUD(Ds);

        //string sql = "INSERT INTO dbo.sql_tblSMDPRVessels " +
        //             "  (ShipID,ShipName,Company,VesselNo,email,Active) " +
        //             "  VALUES " + 
        //             "  ('"+txtShipID.Text.Trim()+"','"+txtShipName.Text.Trim()+"','"+ddlOwner.SelectedValue+"',"+txtShipNo.Text.Trim()+",'"+txtEmail.Text.Trim()+"','"+((chkActive.Checked)?'Y':'N')+"')";
        //    //Common.Execute_Procedures_Select_ByQuery(sql);

        //   sql = "INSERT INTO dbo.tblSMDVessels (Inmarsat,ShipFax,ShipID,SHipName) "+
        //       "VALUES( '"+txtInmarsat.Text.Trim()+"','"+txtShipFax.Text.Trim()+"','"+txtShipID.Text.Trim()+"','"+txtShipName.Text.Trim()+"')";
            //Common.Execute_Procedures_Select_ByQuery(sql);
        if (res)
        {
            lblmsg.Text = "Record saved successfully.";
            BindVessel();
            ClearFields();
            txtShipID.Enabled = true;
            VessId = "";
            dvAddVessel.Visible = false;
        }
        else
        {
            lblmsg.Text = "Record could not be saved." + Common.ErrMsg;
        }
                          
    }
    protected void imgUpdate_OnClick(object sender, EventArgs e)
    {
        ImageButton Update = (ImageButton)sender;
        Label lblShip = (Label)Update.Parent.FindControl("lblShipID");        
        dvAddVessel.Visible = true;
        VessId = lblShip.Text;
        txtShipID.Enabled = false;
        ShowData();
    }
    public void ShowData()
    {
        string str = "select ShipID, Shipname , Company, VesselNo, Active,Email, '' As NWCShipId   from VW_sql_tblSMDPRVessels with(nolock)  where ShipID='" + VessId + "'";
        DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtVessel != null)
        {
            if (dtVessel.Rows.Count > 0)
            {
                DataRow Dr = dtVessel.Rows[0];
                txtShipID.Text = Dr["ShipID"].ToString();
                txtShipName.Text = Dr["SHipName"].ToString();
                ddlOwner.SelectedValue = Dr["Company"].ToString();
                txtShipNo.Text = Dr["VesselNo"].ToString();
                txtEmail.Text = Dr["email"].ToString();
                txtNWC.Text = Dr["NWCShipId"].ToString();

                if (Dr["Active"].ToString() == "A")
                    chkActive.Checked = true;
                else
                    chkActive.Checked = false;
            }
        }


        str = "select ShipID,SHipName,ShipFax,Inmarsat,Built,DWTMetricTons,GRT,NRT,ClassNo,YardBuilt,HullNo,HandM "+
            " ,PandI,Registry,OfficialNo,CallSign,MainEngine,AuxiliaryEngine,ExName,mainEngineNo from dbo.tblSMDVessels  where ShipId='" + VessId + "'";
        DataTable dtVessdetails = Common.Execute_Procedures_Select_ByQuery(str);
        if (dtVessdetails != null)
        {
            DataRow Dr = dtVessdetails.Rows[0];
            txtShipFax.Text = Dr["ShipFax"].ToString();
            txtInmarsat.Text = Dr["Inmarsat"].ToString();
            txtBuilt.Text = Dr["Built"].ToString();
            txtDWT.Text = Dr["DWTMetricTons"].ToString();
            txtGRT.Text = Dr["GRT"].ToString();
            txtNRT.Text = Dr["NRT"].ToString();
            txtRegistery.Text = Dr["ClassNo"].ToString();
            txtYearBuilt.Text = Dr["YardBuilt"].ToString();
            txtHullNo.Text = Dr["HullNo"].ToString();
            txtHAndM.Text = Dr["HandM"].ToString();

            txtPAndI.Text = Dr["PandI"].ToString();
            txtRegistery2.Text = Dr["Registry"].ToString();
            txtOfficialNo.Text = Dr["OfficialNo"].ToString();
            txtCallSign.Text = Dr["CallSign"].ToString();
            txtMainEngine.Text = Dr["MainEngine"].ToString();
            txtMainEngineNo.Text = Dr["mainEngineNo"].ToString();            
            txtMainEngineNo.Text = Dr["AuxiliaryEngine"].ToString();
            txtAUXEngine.Text = Dr["ExName"].ToString();
        }
    }
    protected void imgCalcel_OnClick(object sender, EventArgs e)
    {
        dvAddVessel.Visible = false;
        txtShipID.Enabled = true;
    }
    
}
