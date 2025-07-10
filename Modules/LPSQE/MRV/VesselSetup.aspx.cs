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
using System.Text;
using System.Data.SqlClient;
using Ionic.Zip;

public partial class MRV_VesselSetup : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return hfdVesselCode.Value; }
    }
    public int EmissionSourceId
    {
        get { return Common.CastAsInt32(ViewState["EmissionSourceId"]); }
        set { ViewState["EmissionSourceId"] = value.ToString(); }
    }
    public int FuelTankId
    {
        get { return Common.CastAsInt32(ViewState["FuelTankId"]); }
        set { ViewState["FuelTankId"] = value.ToString(); }
    }
    public int FlowMeterId
    {
        get { return Common.CastAsInt32(ViewState["FlowMeterId"]); }
        set { ViewState["FlowMeterId"] = value.ToString(); }
    }    

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        lblMsg.Text = "";
        ProjectCommon.SessionCheck_New();
        Session["MM"] = 1;
        //------------------------------------
        if (!IsPostBack)
        {
            BindVessel();
            BindFuelType();
            BindInletOutletFuelMeters();
            ShowVesselParticulars();
        }
    }
    
    protected void BindInletOutletFuelMeters()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FlowMeters where VesselCode='" + VesselCode + "' Order BY FlowMeterName");
        DataView dv = dt.DefaultView;
        dv.RowFilter = "FlowMeterType='I'";
        chhklst_InletFM.DataSource = dv.ToTable();
        chhklst_InletFM.DataTextField = "FlowMeterName";
        chhklst_InletFM.DataValueField = "FlowMeterId";
        chhklst_InletFM.DataBind();

        dv.RowFilter = "FlowMeterType='O'";
        chhklst_OutletFM.DataSource = dv.ToTable();
        chhklst_OutletFM.DataTextField = "FlowMeterName";
        chhklst_OutletFM.DataValueField = "FlowMeterId";
        chhklst_OutletFM.DataBind();
    }
    protected void BindVessel()
    {
        DataTable dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELCODE,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString()) + ")  ORDER BY VESSELNAME");
        ddlVessel.DataTextField = "VESSELNAME";
        ddlVessel.DataValueField = "VESSELID";
        ddlVessel.DataSource = dtv;
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void BindFuelType()
    {
        DataTable dtv = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FuelTypes ORDER BY FuelTypeName");
        ddlFuelType.DataTextField = "ShortName";
        ddlFuelType.DataValueField= "FuelTypeId";
        ddlFuelType.DataSource = dtv;
        ddlFuelType.DataBind();
        ddlFuelType.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID,VESSELCODE,VESSELNAME FROM DBO.VESSEL WHERE VesselCode='" + VesselCode + "' and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString()) + ")");
        if (dtv.Rows.Count > 0)
        {
            DataRow dr = dtv.Rows[0];
            //lblVesselName.Text = dr["VesselName"].ToString();
            dvVesselDetails.Visible = true;
            BindEmissionSource();
            BindFuelTank();
            BindFlowMeters();
            ShowVesselParticulars();
            BindFuelConsMethod();
        }
    }
    protected void BindFuelConsMethod()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_EmissionSource A WHERE VESSELCODE='" + VesselCode + "' Order BY EmissionSourceName");
        rptFuelConsMethod.DataSource = dt;
        rptFuelConsMethod.DataBind();
    }
    public string GetFuelConsumptionString(object _key)
    {
        StringBuilder res = new StringBuilder();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FlowMeterName,* FROM DBO.MRV_EmissionSource_FlowMeters_Calc  C inner join DBO.MRV_FlowMeters FM ON C.FlowMeterId=FM.FlowMeterId WHERE EmissionSourceId=" + _key + " order by multiple desc");
        if(dt.Rows.Count<=0)
        {
            dt = Common.Execute_Procedures_Select_ByQuery("SELECT calccomments FROM DBO.MRV_EmissionSource WHERE EmissionSourceId=" + _key);
            if(Convert.IsDBNull(dt.Rows[0][0]))
                res.Append("<b style='color:Red'>Sorry ! No calculation method exists for this emission source.</b>");
            else
                res.Append("<b style='color:Red'>" + dt.Rows[0][0].ToString() + "</b>");
        }
        else
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Multiple=1";
            DataTable dtplus = dv.ToTable();
            
            if (dtplus.Rows.Count > 0)
                res.Append("( ");            
            for(int i = 0; i <= dtplus.Rows.Count-1;i++)
            {
                if (i != 0) { res.Append(" + "); }
                res.Append(dtplus.Rows[i]["FlowMeterName"]);
            }
            if (dtplus.Rows.Count > 0)
                res.Append(" )");


            dv.RowFilter = "Multiple=-1";
            DataTable dtminus = dv.ToTable();

            if (dtminus.Rows.Count > 0)
                res.Append(" - ( ");
            for (int i = 0; i <= dtminus.Rows.Count - 1; i++)
            {
                if (i != 0) { res.Append(" + "); }
                res.Append(dtminus.Rows[i]["FlowMeterName"]);
            }
            if (dtminus.Rows.Count > 0)
                res.Append(" )");
        }
        
        return res.ToString();
    }
    protected void btnTab_Click(object sender, EventArgs e)
    {
        int Arg =Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //li1.CssClass = "btn1";
        //li2.CssClass = "btn1";
        //li3.CssClass = "btn1";
        //li4.CssClass = "btn1";
        //li5.CssClass = "btn1";
        //((HtmlControl)li1.Parent).Attributes.Add("class", "btn1");
        //((HtmlControl)li2.Parent).Attributes.Add("class", "btn1");
        //((HtmlControl)li3.Parent).Attributes.Add("class", "btn1");
        //((HtmlControl)li4.Parent).Attributes.Add("class", "btn1");
        //((HtmlControl)li5.Parent).Attributes.Add("class", "btn1");

        //((HtmlControl)((Button)sender).Parent).Attributes.Add("class", "selbtn");
        mv1.ActiveViewIndex = Arg;
        if (mv1.ActiveViewIndex == 0)
        {
            BindFuelConsMethod();
        }
        //switch (Arg)
        //{
        //    case 0:
        //        li1.CssClass = "selbtn";
        //        break;
        //    case 1:
        //        li2.CssClass = "selbtn";
        //        break;
        //    case 2:
        //        li3.CssClass = "selbtn";
        //        break;
        //    case 3:
        //        li4.CssClass = "selbtn";
        //        break;
        //    case 4:
        //        li5.CssClass = "selbtn";
        //        break;
        //    default:
        //        break;
        //}

    }
   protected void btnExport_Click(object sender, EventArgs e)
    {
        if(VesselCode!="")
        {
            try
            {
                GC.Collect();
                DataSet ds_Ret = new DataSet();
                SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
                SqlCommand MyCommand = new SqlCommand();
                MyCommand.Connection = MyConnection;
                MyCommand.CommandType = CommandType.StoredProcedure;
                MyCommand.CommandText = "MRV_Get_Office_Export_Data";
                MyCommand.Parameters.Add(new SqlParameter("@VesselCode", VesselCode));
                SqlDataAdapter adp = new SqlDataAdapter(MyCommand);
                adp.Fill(ds_Ret, "Data");
                
                if (ds_Ret.Tables.Count ==7  )
                {
                    ds_Ret.Tables[0].TableName = "MRV_FuelTank";
                    ds_Ret.Tables[1].TableName = "MRV_FuelTypes";
                    ds_Ret.Tables[2].TableName = "MRV_EmissionSource";
                    ds_Ret.Tables[3].TableName = "MRV_EmissionSource_FlowMeters_Calc";
                    ds_Ret.Tables[4].TableName = "MRV_EmissionSource_FlowMeters";
                    ds_Ret.Tables[5].TableName = "MRV_FlowMeters";
                    ds_Ret.Tables[6].TableName = "MRV_Activity";

                    int FuelTypes = ds_Ret.Tables["MRV_FuelTypes"].Rows.Count;
                    int Sources = ds_Ret.Tables["MRV_EmissionSource"].Rows.Count;
                    int FlowMeters = ds_Ret.Tables["MRV_FlowMeters"].Rows.Count;
                    bool SourceCalc = (ds_Ret.Tables["MRV_EmissionSource"].Select("CalcComments<>''").Length <= 0);

                    if(FuelTypes<=0 || Sources <= 0 ||FlowMeters <= 0 || SourceCalc == false)
                    {
                        ShowMessage(true, "Error : Selected vessel has not configured properly for MRV.");
                        return;
                    }

                    // TRUNCATING TEMP FOLDER
                    string TempFolder = Server.MapPath("~/Modules/LPSQE/Temp/");
                    foreach (string fl in System.IO.Directory.GetFiles(TempFolder))
                    {
                        try
                        {
                            System.IO.File.Delete(fl);
                        }
                        catch { }
                    }

                    string SchemaFile = TempFolder + "MRV_Schema.xml";
                    string DataFile = TempFolder + "MRV_Data.xml";

                    ds_Ret.WriteXmlSchema(SchemaFile);
                    ds_Ret.WriteXml(DataFile);


                    string zipFileName = "MRV_O_" + VesselCode + "_" + DateTime.Today.ToString("dd_MMM") + ".zip";
                    string RetFile = "";
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(SchemaFile);
                        zip.AddFile(DataFile);
                        RetFile = TempFolder + zipFileName;
                        zip.Save(RetFile);
                        //Response.WriteFile(RetFile);
    		        Byte[] data = System.IO.File.ReadAllBytes(RetFile);
        		Response.Clear();
		        Response.AppendHeader("content-disposition", "attachment; filename=" + zipFileName);
		        Response.ContentType = "application/octet-stream";
		        Response.BinaryWrite(data);
		        Response.Flush();
		        Response.End();

                        ShowMessage(false, "MRV Packet created successfuly. File Name : " + zipFileName);
                    }
                    ds_Ret.Dispose();                    
                }
                else
                {
                    ds_Ret.Dispose();
                    ShowMessage(true, "Error : Required data not found.");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(true, "Error : " + ex.Message);
            }
        }
    }
    
    //----------------------------------------

    protected void btn_AddSource_Click(object sender, EventArgs e)
    {
        ClearEmissionSource();
        dvModal.Visible = true;
        dvAddEditSource.Visible = true;
    }
    protected void btn_EditSource_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowSourceRecord(_key);        
    }
    protected void lnk_ShowLinkedFlowMeters_Click(object sender, EventArgs e)
    {
        BindInletOutletFuelMeters();
        EmissionSourceId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        BindLinkedFlowMeters(EmissionSourceId);
        lblEmissionsourceName.Text = ((LinkButton)sender).Attributes["esname"];
        dvModal.Visible = true;
        dvLinkFlowMeters.Visible = true;

        chhklst_InletFM.ClearSelection();
        chhklst_OutletFM.ClearSelection();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_EmissionSource_FlowMeters WHERE EmissionSourceId=" + EmissionSourceId);
        if(dt.Rows.Count>0)
        {
            foreach(ListItem ch in chhklst_InletFM.Items)
            {
                ch.Selected = Common.CastAsInt32(dt.Compute("COUNT(EmissionSourceId)", "FlowMeterId=" + ch.Value))>0;
            }
            foreach (ListItem ch in chhklst_OutletFM.Items)
            {
                ch.Selected = Common.CastAsInt32(dt.Compute("COUNT(EmissionSourceId)", "FlowMeterId=" + ch.Value)) > 0;  
            }
        }
    }
    protected void btnCancelLink_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
        dvLinkFlowMeters.Visible = false;
    }
    protected void btnSaveLink_Click(object sender, EventArgs e)
    {
        string MetersString="";
        foreach (ListItem ch in chhklst_InletFM.Items)
        {
            MetersString += (ch.Selected) ? "," + ch.Value : ""; ;
        }
        foreach (ListItem ch in chhklst_OutletFM.Items)
        {
            MetersString += (ch.Selected) ? "," + ch.Value : ""; ;
        }
        if (MetersString.StartsWith(","))
            MetersString = MetersString.Substring(1);

        Common.Set_Procedures("DBO.MRV_InsertUpdateLinkedFlowMeters");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@EmissionSourceId", EmissionSourceId),
            new MyParameter("@FlowMetrers", MetersString));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
            BindEmissionSource();
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }
    public void BindEmissionSource()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT COUNT(*) FROM DBO.MRV_EmissionSource_FlowMeters FM WHERE FM.EmissionSourceId=A.EmissionSourceId) AS FlowMetersCount FROM DBO.MRV_EmissionSource A WHERE VESSELCODE='" + VesselCode + "' Order BY EmissionSourceName");
        rptEmissionSource.DataSource = dt;
        rptEmissionSource.DataBind();
    }
    public void BindLinkedFlowMeters(object _EmissionSourceId)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FlowMeters WHERE VESSELCODE='" + VesselCode + "' and FlowMeterId In (SELECT FlowMeterId FROM dbo.MRV_EmissionSource_FlowMeters FM where FM.EmissionSourceId=" + _EmissionSourceId + ") Order BY FlowMeterName");
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FlowMeters WHERE VESSELCODE='" + VesselCode + "' Order BY FlowMeterName");
        //rptEmissionSource.DataSource = dt;
        //rptEmissionSource.DataBind();
    }
    protected void btnSaveEmissionSource_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.MRV_InsertUpdate_EmissionSource");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@EmissionSourceId", EmissionSourceId),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@EmissionSourceName", txtEmissionSourceName.Text));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
            ClearEmissionSource();
            BindEmissionSource();
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }
    protected void btnCancelEmissionSource_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
        dvAddEditSource.Visible = false;
    }
    
    private void ClearEmissionSource()
    {
        EmissionSourceId = 0;
        txtEmissionSourceName.Text = "";
    }
    private void ShowSourceRecord(int _EmissionSourceId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_EmissionSource where EmissionSourceId=" + _EmissionSourceId);
        if (dt.Rows.Count > 0)
        {
            EmissionSourceId = _EmissionSourceId;
            DataRow dr = dt.Rows[0];
            txtEmissionSourceName.Text = dr["EmissionSourceName"].ToString();

            dvModal.Visible = true;
            dvAddEditSource.Visible = true;
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
            BindEmissionSource();
        }
    }

    //----------------------------------------

    protected void btn_AddFuelTank_Click(object sender, EventArgs e)
    {
        ClearFuelTank();
        dvModal.Visible = true;
        dvAddEditFuelTank.Visible = true;
    }
    protected void btn_EditFuelTank_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowFuelTankRecord(_key);
    }
    public void BindFuelTank()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FuelTank FT LEFT JOIN DBO.MRV_FuelTypes FL on FT.FuelTypeId=FL.FuelTypeId  WHERE VESSELCODE='" + VesselCode + "' Order BY FuelTankName");
        rptFuelTanks.DataSource = dt;
        rptFuelTanks.DataBind();
    }
    protected void btnSaveFuelTank_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.MRV_InsertUpdate_FuelTank");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(new MyParameter("@FuelTankId", FuelTankId),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@FuelTankName", txtFuelTankName.Text),
            new MyParameter("@FuelTypeId", ddlFuelType.SelectedValue),
            new MyParameter("@Capacity", Common.CastAsDecimal(txtCapacity.Text)),
            new MyParameter("@MeasuringDevice", txtMeasuringDevice.Text));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
            ClearFuelTank();
            BindFuelTank();
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }
    protected void btnCancelFuelTank_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
        dvAddEditFuelTank.Visible = false;
    }
    private void ClearFuelTank()
    {
        FuelTankId = 0;
        txtFuelTankName.Text = "";
        ddlFuelType.SelectedIndex = 0;
        txtCapacity.Text = "";
        txtMeasuringDevice.Text = "";
    }
    private void ShowFuelTankRecord(int _FuelTankId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FuelTank where FuelTankId=" + _FuelTankId);
        if (dt.Rows.Count > 0)
        {
            FuelTankId = _FuelTankId;
            DataRow dr = dt.Rows[0];
            txtFuelTankName.Text  = dr["FuelTankName"].ToString();
            txtCapacity.Text = dr["Capacity"].ToString();
            txtMeasuringDevice.Text = dr["MeasuringDevice"].ToString();
            try {
                ddlFuelType.SelectedValue = dr["FuelTypeId"].ToString();
            }
            catch { }
            dvModal.Visible = true;
            dvAddEditFuelTank.Visible = true;
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
            BindFuelTank();
        }
    }

    //----------------------------------------

    protected void btn_AddFlowMeter_Click(object sender, EventArgs e)
    {
        ClearFlowMeters();
        dvModal.Visible = true;
        dvAddEditFlowMetres.Visible = true;

    }
    protected void btn_EditFlowMeter_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowFlowMeterRecord(_key);
    }
    public void BindFlowMeters()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FlowMeters WHERE VESSELCODE='" + VesselCode + "' Order BY FlowMeterName");
        rptFlowMeters.DataSource = dt;
        rptFlowMeters.DataBind();
    }
    public void ShowVesselParticulars()
    {
        lblVP_VesselCode.Text = VesselCode;
        lblVP_VesselName.Text = ddlVessel.SelectedItem.Text;

        string sql = " Select * from DBO.MRV_VesselSettings WHERE VESSELCODE='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            

            lblVP_IMOID.Text = Dr["IMOID"].ToString();
            lblVP_PortOfRegistry.Text = Dr["PortOfRegistry"].ToString();
            lblVP_HomePort.Text = Dr["HomePort"].ToString();
            try
            {
                ddlVP_IceClass.SelectedValue = Dr["IceClass"].ToString();
            }
            catch { }
            lblVP_EEDI.Text = Dr["EEDI"].ToString();
            lblVP_EIV.Text = Dr["EIV"].ToString();
            lblVP_ShipOwner.Text = Dr["Shipowner"].ToString();
            lblVP_AddressOfShipowner.Text = Dr["AddressOfShipowner"].ToString();
            lblVP_NameOfCompany.Text = Dr["NameOfCompany"].ToString();
            lblVP_AddressOfCompany.Text = Dr["AddressOfCompany"].ToString();
            lblVP_ContactPerson.Text = Dr["ContactPerson"].ToString();
            lblCP_VP_AddressOfCompany.Text = Dr["CP_AddressOfCompany"].ToString();
            lblCP_VP_Telephone.Text = Dr["CP_Telephone"].ToString();
            lblCP_VP_Email.Text = Dr["CP_Email"].ToString();

            lblVP_NameofVerifier.Text = Dr["NameofVerifier"].ToString();
            lblVP_AddressofVerifier.Text = Dr["AddressofVerifier"].ToString();
            lblVP_AccreditationNo.Text = Dr["AccreditationNo"].ToString();
            lblVP_VerifiersStatement.Text = Dr["VerifiersStatement"].ToString();
        }
        else
        {
            
            lblVP_IMOID.Text = "";
            lblVP_PortOfRegistry.Text = "";
            lblVP_HomePort.Text = "";
            ddlVP_IceClass.SelectedIndex = 0;
            lblVP_EEDI.Text = "";
            lblVP_EIV.Text = "";
            lblVP_ShipOwner.Text = "";
            lblVP_AddressOfShipowner.Text = "";
            lblVP_NameOfCompany.Text = "";
            lblVP_AddressOfCompany.Text = "";
            lblVP_ContactPerson.Text = "";
            lblCP_VP_AddressOfCompany.Text = "";
            lblCP_VP_Telephone.Text = "";
            lblCP_VP_Email.Text = "";

            lblVP_NameofVerifier.Text = "";
            lblVP_AddressofVerifier.Text = "";
            lblVP_AccreditationNo.Text = "";
            lblVP_VerifiersStatement.Text = "";
        }
    }
    protected void btnSaveFlowMeter_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.MRV_InsertUpdate_MRV_FlowMeters");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters(new MyParameter("@FlowMeterId", FlowMeterId),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@FlowMeterName", txtFlowMeterName.Text),
            new MyParameter("@FlowMeterType", ddlFlowMeterType.SelectedValue),
            new MyParameter("@ReadingMode", ddlFlowUnit.SelectedValue),
            new MyParameter("@PMSCompCode", txtPMSCompCode.Text),
            new MyParameter("@Maker", txtMaker.Text),
            new MyParameter("@Model", txtModel.Text),
            new MyParameter("@PartNo", txtPartNo.Text),
            new MyParameter("@OtherDetails", txtOtherDetails.Text));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
            ClearFlowMeters();
            BindFlowMeters();
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }
    protected void btnCancelFlowMeter_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
        dvAddEditFlowMetres.Visible = false;
    }
    private void ClearFlowMeters()
    {
        FlowMeterId = 0;
        txtFlowMeterName.Text = "";
        ddlFlowMeterType.SelectedIndex = 0;
        txtPMSCompCode.Text = "";
        txtMaker.Text = "";
        txtModel.Text = "";
        txtPartNo.Text = "";
        txtOtherDetails.Text = "";
        ddlFlowUnit.SelectedIndex=0;
    }
    private void ShowFlowMeterRecord(int _FlowMeterId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FlowMeters where FlowMeterId=" + _FlowMeterId);
        if (dt.Rows.Count > 0)
        {
            FlowMeterId = _FlowMeterId;
            DataRow dr = dt.Rows[0];
            txtFlowMeterName.Text = dr["FlowMeterName"].ToString();
            try
            {
                ddlFlowMeterType.SelectedValue = dr["FlowMeterType"].ToString();
            }
            catch { }
            try
            {
                ddlFlowUnit.SelectedValue = dr["ReadingMode"].ToString();
            }
            catch { }
            txtPMSCompCode.Text = dr["PMSCompCode"].ToString();
            txtMaker.Text = dr["Maker"].ToString();
            txtModel.Text = dr["Model"].ToString();
            txtPartNo.Text = dr["PartNo"].ToString();
            txtOtherDetails.Text = dr["OtherDetails"].ToString();
            
            dvModal.Visible = true;
            dvAddEditFlowMetres.Visible = true;
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
            BindFlowMeters();
        }
    }

    //----------------------------------------

    public void ShowMessage(bool error, string Message)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass = (error) ? "modal_error" : "modal_success";
    }

    protected void btnSaveVesselParticulars_OnClick(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.MRV_InsertUpdate_VesselSettings");
        Common.Set_ParameterLength(20);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@VESSELNAME", ddlVessel.SelectedItem.Text),
            new MyParameter("@IMOID", lblVP_IMOID.Text),
            new MyParameter("@PORTOFREGISTRY", lblVP_PortOfRegistry.Text.Trim()),
            new MyParameter("@HOMEPORT", lblVP_HomePort.Text.Trim()),
            new MyParameter("@ICECLASS", ddlVP_IceClass.SelectedValue),
            new MyParameter("@EEDI", lblVP_EEDI.Text.Trim()),
            new MyParameter("@EIV", lblVP_EIV.Text.Trim()),
            new MyParameter("@SHIPOWNER", lblVP_ShipOwner.Text.Trim()),
            new MyParameter("@ADDRESSOFSHIPOWNER", lblVP_AddressOfShipowner.Text.Trim()),
            new MyParameter("@NAMEOFCOMPANY", lblVP_NameOfCompany.Text.Trim()),
            new MyParameter("@ADDRESSOFCOMPANY", lblVP_AddressOfCompany.Text.Trim()),
            new MyParameter("@CONTACTPERSON", lblVP_ContactPerson.Text.Trim()),
            new MyParameter("@CP_ADDRESSOFCOMPANY", lblCP_VP_AddressOfCompany.Text.Trim()),
            new MyParameter("@CP_TELEPHONE", lblCP_VP_Telephone.Text.Trim()),
            new MyParameter("@CP_EMAIL", lblCP_VP_Email.Text.Trim()),
            
            new MyParameter("@NameofVerifier", lblVP_NameofVerifier.Text.Trim()),
            new MyParameter("@AddressofVerifier", lblVP_AddressofVerifier.Text.Trim()),
            new MyParameter("@AccreditationNo", lblVP_AccreditationNo.Text.Trim()),
            new MyParameter("@VerifiersStatement", lblVP_VerifiersStatement.Text.Trim())

            );
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0)
        {
            DataTable dtv = Common.Execute_Procedures_Select_ByQuery("SELECT top 1 VESSELID,VESSELCODE,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  And VESSELID = '" + ddlVessel.SelectedValue + " ' ORDER BY VESSELNAME");

            if (dtv.Rows.Count > 0)
            {
                hfdVesselCode.Value = dtv.Rows[0]["VESSELCODE"].ToString();
                //li1.CssClass = "selbtn";
                //li2.CssClass = "btn1";
                //li3.CssClass = "btn1";
                //li4.CssClass = "btn1";
                //li5.CssClass = "btn1";
                btnExport.Visible = true;
                btnRefresh_Click(sender, e);
            }
        }
    }
}
