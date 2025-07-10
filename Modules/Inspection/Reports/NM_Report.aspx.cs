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

public partial class Reports_NM_Report : System.Web.UI.Page
{
    string VesselId = "", strVesselName = "", strCat = "";
    Boolean flag = false;
    Boolean flag1 = false;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            lblmessage.Text = "";
            if (!Page.IsPostBack)
            {
               chklst_AllVsl.Checked = true;
               BindVessel();
            }
            else
                btn_Show_Click(sender, e);
        }
        catch (Exception ex) { throw ex; }
    }
    protected void BindVessel()
    {
        try
        {
            this.chklst_Vsls.DataTextField = "VesselName";
            this.chklst_Vsls.DataValueField = "VesselId";
            this.chklst_Vsls.DataSource = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            this.chklst_Vsls.DataBind();
            for (int a = 0; a < chklst_Vsls.Items.Count; a++)
            {
                chklst_Vsls.Items[a].Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        try
        {
            for (int a = 0; a < chklst_Vsls.Items.Count; a++)
            {
                if (chklst_Vsls.Items[a].Selected == true)
                    flag = true;
            }
            if (flag == false)
            {
                lblmessage.Text = "Please select atleast one Vessel.";
                return;
            }
            if (txt_FromDate.Text.Trim() != "")
            {
                if (txt_ToDate.Text.Trim() == "")
                {
                    lblmessage.Text = "Please enter To Date.";
                    txt_ToDate.Focus();
                    return;
                }
            }
            if (txt_FromDate.Text.Trim() != "" && txt_ToDate.Text.Trim() != "")
            {
                if (DateTime.Parse(txt_ToDate.Text.Trim()) < (DateTime.Parse(txt_FromDate.Text.Trim())))
                {
                    lblmessage.Text = "To Date cannot be less than From Date.";
                    txt_ToDate.Focus();
                    return;
                }
            }
            for (int J = 0; J < chklst_Vsls.Items.Count; J++)
            {
                if (chklst_Vsls.Items[J].Selected == true)
                {
                    if (VesselId == "")
                    {
                        VesselId = chklst_Vsls.Items[J].Value;
                    }
                    else
                    {
                        VesselId = VesselId + "," + chklst_Vsls.Items[J].Value;
                    }
                }
            }
            for (int k = 0; k < chklst_Vsls.Items.Count; k++)
            {
                if (chklst_Vsls.Items[k].Selected == true)
                {
                    if (strVesselName == "")
                    {
                        strVesselName = chklst_Vsls.Items[k].Text;
                    }
                    else
                    {
                        strVesselName = strVesselName + ", " + chklst_Vsls.Items[k].Text;
                    }
                }
            }
            strCat = ddl_Cat.SelectedValue;  
            IFRAME1.Attributes.Add("src", "NM_ReportCrystal.aspx?NMCAT=" + strCat + "&NMFROMDATE=" + txt_FromDate.Text.Trim() + "&NMTODATE=" + txt_ToDate.Text.Trim() + "&NMVSLID=" + VesselId);
        }
        catch { }
    }
}
