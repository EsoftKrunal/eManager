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
using System.Data.SqlClient;
using System.Xml;
using System.IO; 
using System.Text;
using Ionic.Zip;

public partial class OpenVNA : System.Web.UI.Page
{
    public int VNAID
    {
        get { return Common.CastAsInt32(ViewState["VNAID"]); }
        set{ViewState["VNAID"]=value;} 
    }
    public string VESSELCODE
    {
        get { return ViewState["VESSELCODE"].ToString(); }
        set { ViewState["VESSELCODE"] = value; }
    }
    public int QID
    {
        get { return Common.CastAsInt32(ViewState["QID"]); }
        set { ViewState["QID"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public bool Closed
    {
        get { return Convert.ToBoolean(ViewState["ClosedOn"]); }
        set { ViewState["ClosedOn"] = value; }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblMessage.Text = "";
        //lblMsgPOP.Text = "";
        if (!Page.IsPostBack)
        {
            UserName = Session["UserName"].ToString();
            VESSELCODE = Session["CurrentShip"].ToString();
            if (Request.Form["comply"] != null && Request.Form["text"] != null && Request.Form["vnaid"] != null)
            {
                int _vnaid = Common.CastAsInt32(Request.Form["vnaid"]);
                int _qid = Common.CastAsInt32(Request.Form["qid"]);
                string _vsl = Request.Form["vsl"];
                string _Comply = Request.Form["comply"];
                string _Text = Request.Form["text"];
                Response.Clear();
                try
                {
                    string sql = "UPDATE DBO.vna_details SET COMPLY='" + _Comply + "', SHIPCOMMENTS='" + _Text.Replace("'", "`") + "',SHIPCOMMENTBY='" + UserName + "',SHIPCOMMENTDATE=GETDATE() WHERE VESSELCODE='" + _vsl + "' AND VNAID=" + _vnaid + " AND QID=" + _qid;
                    Common.Execute_Procedures_Select_ByQuery(sql);
                    Response.Write("Updated successfully.");
                }
                catch { Response.Write("Unable to save."); }
                Response.End();
                return;
                // savwe
            }

            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.VNA_MASTER where vnano='" + Request.QueryString["Key"] + "'");
            if (DT.Rows.Count > 0)
            {
                
                VNAID = Common.CastAsInt32(DT.Rows[0]["VNAID"]);
                lblNANo.Text = Page.Request.QueryString["Key"];
                VESSELCODE = lblNANo.Text.Trim().Substring(0, 3);
                ShowMasterData();
                BindRepeater();

               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "cls", "alert('Invalid VNA#.');window.close();", true);
            }
        }
    }
    public void ShowMasterData()
    {
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.VNA_MASTER where VNAID=" + VNAID + " AND VESSELCODE='" + VESSELCODE + "'");
        if (DT.Rows.Count > 0)
        {
            txtGenComments.Text = DT.Rows[0]["OfficeRemark"].ToString();
            Closed = !Convert.IsDBNull(DT.Rows[0]["ClosedOn"]);
            btnExport.Visible = !Closed;
            btnSave.Visible = !Closed;

            txtFromDate.Text=Common.ToDateString(DT.Rows[0]["AuditFrom"]);
            txtToDate.Text=Common.ToDateString(DT.Rows[0]["AuditTo"]);
            txtPortFrom.Text=DT.Rows[0]["PortFrom"].ToString();
            txtPortTo.Text=DT.Rows[0]["PortTo"].ToString();
            txtMasterCrewNo.Text=DT.Rows[0]["MasterCrewNo"].ToString();
            txtMasterName.Text=DT.Rows[0]["MasterName"].ToString();
            if(Closed)
                lblClosedByOn.Text = " ( " + DT.Rows[0]["ClosedBy"].ToString() + Common.ToDateString(DT.Rows[0]["ClosedOn"]) + " ) ";
            
        }
    }
    // ------------ Function
    protected void btnSaveMaster_Click(object sender, EventArgs e)
    {
        try
        {
            string sql="UPDATE DBO.vna_MASTER " +
                    "SET " +
                    "AuditFrom='" + txtFromDate.Text.Trim() + "'," +
                    "AuditTo='" +txtToDate.Text.Trim()+ "'," +
                    "PortFrom='" +txtPortFrom.Text.Trim()+ "'," +
                    "PortTo='" +txtPortTo.Text.Trim()+ "'," +
                    "MasterCrewNo='" +txtMasterCrewNo.Text.Trim()+ "'," +
                    "MasterName='" +txtMasterName.Text.Trim()+ "' " +
                    "WHERE VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID;
            Common.Execute_Procedures_Select_ByQuery(sql);
            ShowMessage("Record Saved Successfully",false);
        }
        catch 
        {
            ShowMessage("Unable to save record.",false);
        }
    }
    protected void ddlShowMode_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void Comply_Changed(object sender, EventArgs e)
    {
        BindRepeater();
    }
  
    // ------------ Function
    protected void BindRepeater()
    {
        string Filter = "";
        switch (Common.CastAsInt32(ddlShowMode.SelectedValue))
        {
            case 1:
                Filter = " AND LTRIM(RTRIM(ShipComments))<>''";
            break;
            case 2:
                Filter = " AND LTRIM(RTRIM(oFFICEComments))<>''";
            break;
            case 3:
                Filter = " AND LTRIM(RTRIM(ISNULL(ShipComments,'')))=''";
            break;
            case 4:
            Filter = " AND ( (ISNULL(Comply,'')='') OR ( Comply<>'YES' AND LTRIM(RTRIM(ISNULL(ShipComments,'')))='' ) )";
            break;
            default:
                break;
        }
        string SQL = "select *,(select top 1 d1.shipcommentdate from DBO.vna_details d1 where d1.vesselcode='" + VESSELCODE + "' and d1.vnaid<>" + VNAID + " and d1.qid=DBO.vna_details.qid order by d1.shipcommentdate desc)  as lastDate from DBO.vna_details where VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID + Filter + " ORDER BY QNO";
        //rptData.DataSource = Common.Execute_Procedures_Select_ByQuery("select * from DBO.vna_details where VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID + Filter + " ORDER BY QNO");
        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptData.DataBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindRepeater();
        
        string cHK1 = "select * from DBO.VNA_MASTER where VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID + " AND (AuditFrom Is NULL OR AuditTo IS NULL) ";
        DataTable dt12 = Common.Execute_Procedures_Select_ByQuery(cHK1);
        if (dt12.Rows.Count > 0)
        {
            ShowMessage("Please enter audit duration and save before export.", true);
            return;
        }

        string cHK= "select * from DBO.vna_details where VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID  + " AND ISNULL(COMPLY,'')=''";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(cHK);
        if(dt1.Rows.Count>0)
        {

            ShowMessage("Please save all records before export.",true);
            return;
        }
        //----------------------------------

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VESSELCODE),
                new MyParameter("@RecordType", "Vessel Navigation Audit"),
                new MyParameter("@RecordId", VNAID),
                new MyParameter("@RecordNo", lblNANo.Text),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }


        ////string SQLEvt = "select * from DBO.VNA_MASTER where VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID;
        ////DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLEvt);
        ////dt.TableName = "VNA_MASTER";
        ////ds.Tables.Add(dt.Copy());

        ////SQLEvt = "select * from DBO.vna_details WHERE VESSELCODE='" + VESSELCODE + "' AND VNAID=" + VNAID.ToString();
        ////DataTable dt11 = Common.Execute_Procedures_Select_ByQuery(SQLEvt);
        ////dt11.TableName = "vna_details";
        ////ds.Tables.Add(dt11.Copy());

        ////string SchemaFile = Server.MapPath("TEMP/VNASchema.xml");
        ////string DataFile = Server.MapPath("TEMP/VNAData.xml");
        ////string ZipFile = Server.MapPath("TEMP/VNA_S_" + lblNANo.Text.Trim() + ".zip");
        ////ds.WriteXmlSchema(SchemaFile);
        ////ds.WriteXml(DataFile);

        ////using (ZipFile zip = new ZipFile())
        ////{
        ////    zip.AddFile(SchemaFile);
        ////    zip.AddFile(DataFile);
        ////    zip.Save(ZipFile);
        ////}

        ////byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
        ////Response.BinaryWrite(buff);
        ////Response.Flush();
        ////Response.End();
    }
    protected void ShowMessage(string Msg,bool error)
    {
        lblMessage.Text = Msg;
        lblMessage.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
}

