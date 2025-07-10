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

public partial class Reports_LTI_Report : System.Web.UI.Page
{
    string VesselId = "", strVesselName = "";
    int intVesselCount = 0;
    Boolean flag = false;
    Boolean vslcnt = false;
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
                ddl_Year.SelectedValue = DateTime.Now.Year.ToString();
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
            chklst_Vsls.Items.Clear();
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
            if (ddl_Year.SelectedIndex == 0)
            {
                lblmessage.Text = "Please select a Year.";
                ddl_Year.Focus();
                return;
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
            if (vslcnt == false)
            {
                for (int c = 0; c < chklst_Vsls.Items.Count; c++)
                {
                    if (chklst_Vsls.Items[c].Selected == true)
                        intVesselCount++;
                    vslcnt = true;
                }
            }
            IFRAME1.Attributes.Add("src", "LTI_ReportCrystal.aspx?LTIVSLID=" + VesselId + "&LTIYEAR=" + int.Parse(ddl_Year.SelectedValue) + "&LTIYRNAME=" + ddl_Year.SelectedItem.Text.ToString() + "&LTIVSLCNT=" + intVesselCount);
        }
        catch { }
    }
}
