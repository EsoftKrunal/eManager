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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class CrewOperatin_KST : System.Web.UI.Page
{

    public int KSTId
    {
        get { return Common.CastAsInt32(ViewState["KSTId"]); }
        set { ViewState["KSTId"] = value; }
    }

    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }  
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        //------------------------------------
        SessionManager.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            UserName = Session["UserName"].ToString();
            Load_Vessel();
            Bindgrid();
        }
    }
    protected void chkLinked_OnCheckedChanged(object sender, EventArgs e)
    {
        if (txtFIncidentNo.Text.Trim() != "")
        {
            if (chkLinked.Checked)
            {
                string sql = "select vesselcode,reportid,reportno as IncidentNo,incidentdate,AccidentSeverity as Severity,AccidentClassification as Classification,(select vesselname from DBO.vessel v where v.vesselcode=r.vesselcode) as vesselname from dbo.ER_S115_report r where reportno='" + txtFIncidentNo.Text + "'";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    string vesselcode = dt.Rows[0]["vesselcode"].ToString();
                    int reportid = Common.CastAsInt32(dt.Rows[0]["reportid"]);

                    sql = "select 1 from DBO.KST_Master where incidentid=" + reportid + " and incidentvesselcode='" + vesselcode + "'";
                    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        lblmsg.Text = "KST for this incident already created.";
                        txtFIncidentNo.Text = "";
                        chkLinked.Checked = false;
                    }
                    else
                    {

                        hfdIncidentId.Value = reportid.ToString();
                        ddlSeverity.SelectedValue = dt.Rows[0]["Severity"].ToString();
                        ddlVesselName.SelectedValue = dt.Rows[0]["vesselcode"].ToString();
                        txtFIncidentNo.Text = dt.Rows[0]["IncidentNo"].ToString();
                        txtIncidentDate.Text = Common.ToDateString(dt.Rows[0]["IncidentDate"]);
                        string classification = dt.Rows[0]["Classification"].ToString() + ",";
                        string[] names = { "", "Injury to People", "Mooring", "Security", "Cargo Contamination", "", "Equipment Failure", "", "Navigation", "Damage to Property", "Pollution", "Fire" };
                        //string[] AccidentClassifications = dt.Rows[0]["Classification"].ToString().Split(',');
                        chkClassification.ClearSelection();
                        foreach (ListItem li in chkClassification.Items)
                        {
                            if (classification.Contains(li.Value + ","))
                                li.Selected = true;
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Incident does'nt exists.";
                    chkLinked.Checked = false;
                }
            }
            else
            {
                hfdIncidentId.Value = "";
                ddlSeverity.SelectedValue = "";
                ddlVesselName.SelectedValue = "";
                txtIncidentDate.Text = "";
                chkClassification.ClearSelection();
            }
        }
    }
    protected void Filter_Visits(object sender, EventArgs e)
    {
        Bindgrid();
    }
    
    protected void Bindgrid()
    {
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY k.CREATEDON DESC) AS SNO,K.*," +
            "replace(replace(replace(replace(replace(replace(replace(replace(replace(Classification,'1','Injury to People'),'2','Mooring'),'3','Security'),'4','Cargo Contamination'),'6','Equipment Failure'),'8','Navigation'),'9','Damage to Property'),'10','Pollution'),'11','Fire')" +
            " as CategoryName,case when Severity=1 then 'Minor' when Severity=2 then 'Major' when Severity=3 then 'Sever' else 'NA' end accidentseverity, (select vesselname from DBO.vessel v where v.vesselcode=incidentvesselcode) as vesselname,IncidentNo,IncidentDate,Severity,Classification from DBO.KST_Master K WHERE topic like '%" + txtFilterTopic.Text + "%' ";
        if (ddlcat.SelectedIndex > 0)
            sql += "and Classification + ',' like '%" + ddlcat.SelectedValue+ ",%'";
        sql += " ORDER BY k.CREATEDON DESC";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSeminars.DataSource = dt;
        rptSeminars.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
        tbl_mst.Visible = true;
        tbl_det.Visible = true;
        txtFIncidentNo.Text = "";
        KSTId = 0;
        chkLinked.Checked = false;
    }
    private void Load_Vessel()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT VESSELCODE,VESSELNAME FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME");
        ddlVesselName.DataSource = dt;
        ddlVesselName.DataTextField = "VesselName";
        ddlVesselName.DataValueField = "VESSELCODE";
        ddlVesselName.DataBind();
        ddlVesselName.Items.Insert(0, new ListItem("< Select >", ""));
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        KSTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dvFrame.Visible = true;
        tbl_det.Visible = true;
        string sql = "select * from DBO.KST_Master where kst_id=" + KSTId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            string vesselcode = dt.Rows[0]["incidentvesselcode"].ToString();
            int reportid = Common.CastAsInt32(dt.Rows[0]["incidentid"]);
            txtTopic.Text= dt.Rows[0]["topic"].ToString();

            sql = "select * from DBO.KST_Master K where KST_Id=" + KSTId;
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt1.Rows.Count > 0)
            {
                hfdIncidentId.Value = reportid.ToString();
                ddlVesselName.SelectedValue = vesselcode;
                ddlSeverity.SelectedValue = dt1.Rows[0]["Severity"].ToString();
                tbl_det.Visible = true;
                txtFIncidentNo.Text = dt1.Rows[0]["incidentno"].ToString();
                txtIncidentDate.Text = Common.ToDateString(dt1.Rows[0]["incidentdate"]);
                string classification = dt.Rows[0]["Classification"].ToString() + ",";
                chkClassification.ClearSelection();
                foreach (ListItem li in chkClassification.Items)
                {
                    if (classification.Contains(li.Value + ","))
                        li.Selected = true;
                }
            }
        }

    
    }
    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtTopic.Text.Trim() == "")
        {
            lblmsg.Text = "Please enter incident topic.";
            return;
        }
        else
        {
            List<string> arg = new List<string>();
            foreach (ListItem li in chkClassification.Items)
            {
                if(li.Selected)
                    arg.Add(li.Value);
            }
            Common.Set_Procedures("IU_KST_Master");
            Common.Set_ParameterLength(11);
            Common.Set_Parameters(
                new MyParameter("@KST_ID", KSTId),
                new MyParameter("@INCIDENTID", Common.CastAsInt32(hfdIncidentId.Value)),
                new MyParameter("@INCIDENTVESSELCODE", ddlVesselName.SelectedValue),
                new MyParameter("@TOPIC", txtTopic.Text),
                new MyParameter("@ATTACHMENTNAME", Path.GetFileName(flpupload.FileName)),
                new MyParameter("@ATTACHMENT", ReadFully(flpupload.PostedFile.InputStream)),
                new MyParameter("@UserName", UserName),
                new MyParameter("@IncidentNo", txtFIncidentNo.Text),
                new MyParameter("@IncidentDate", txtIncidentDate.Text),
                new MyParameter("@Severity", ddlSeverity.SelectedValue),
                new MyParameter("@Classification", String.Join(",",arg.ToArray())));

            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                KSTId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                lblmsg.Text = "Record saved successfully.";
            }
            else
            {
                lblmsg.Text = "Please enter fields properly.";
            }
        }
    }
    
    protected void btndownload_click(object sender, EventArgs e)
    {
        int _KSTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string sql = "select * from DBO.KST_Master where kst_id=" + _KSTId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            Response.Clear();
            byte[] b = (byte[])dt.Rows[0]["Attachment"];
            Response.AddHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["AttachmentName"].ToString());
            Response.AddHeader("Content-Length", b.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(b);            
            Response.End();
            Response.Flush();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int _KSTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string sql = "delete from DBO.KST_Master where kst_id=" + _KSTId;
        Common.Execute_Procedures_Select_ByQuery(sql);
        Bindgrid();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
        chkClassification.ClearSelection();
        txtTopic.Text = "";
        tbl_mst.Visible = true;
        tbl_det.Visible = false;
        txtFIncidentNo.Text = "";
        KSTId = 0;
        ddlVesselName.SelectedValue = "";
        txtIncidentDate.Text = "";
        ddlSeverity.SelectedValue = "";
        Bindgrid();
    }
    
}
