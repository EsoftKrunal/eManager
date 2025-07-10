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
using System.Text.RegularExpressions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using Ionic.Zip;   

public partial class VimsInspections : System.Web.UI.Page
{
    AuthenticationManager Auth;
    bool setScroll = true;
    public string InspNo
    {
        get {return ViewState["InspNo"].ToString();}
        set {ViewState["InspNo"]=value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(274, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("UnAuthorized.aspx");

            }
            else
            {
                //btnPrintCompList.Visible = Auth.IsPrint;
            }
            
        }
        //*******************

        if (!Page.IsPostBack)
        {
            InspNo= "";
            Session["CurrentModule"] = 11;
            BindInspections();
            btn_AddObservations.Visible = false;
            btnXML.Visible = false;
        }
    }
    protected void BindInspections()
    {
        rpt_Inspections.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_INSPECTIONS ORDER BY PLANDATE DESC");
        rpt_Inspections.DataBind();
    }
    protected void BindObservations()
    {
        rpt_Observations.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Ins_Observations WHERE ltrim(rtrim(InspectionNo))='" + InspNo.Trim() + "' order by Qno");
        rpt_Observations.DataBind();
    }
    protected void Select_Inspection(object sender, EventArgs e)
    {
        InspNo = ((LinkButton)sender).Text.Trim();
        lblInsNo1.Text = InspNo;
        BindInspections();
        BindObservations();
        btn_AddObservations.Visible = true;
        btnXML.Visible = true;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (setScroll)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dv_Inspections');", true);
        }
    }
    protected void btn_ImportInspections_Click(object sender, EventArgs e)
    {
        try
        {
            if (flp_Upload.HasFile)
            {
                string FileName = flp_Upload.FileName;
                StreamReader sr = new StreamReader(flp_Upload.FileContent);
                string FileText = sr.ReadToEnd();
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(FileText);
                try
                {
                    foreach (XmlNode xn in xd.DocumentElement.SelectNodes("Inspection"))
                    {
                        string InspectionNo = xn.ChildNodes[0].InnerText;
                        string PlanDate = xn.ChildNodes[1].InnerText;
                        string PortName = xn.ChildNodes[2].InnerText;
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Inspections WHERE LTRIM(RTRIM(INSPECTIONNO))='" + InspectionNo + "'");
                        if (dt.Rows.Count <= 0)
                        {
                            Common.Execute_Procedures_Select_ByQuery("INSERT INTO VIMS_Inspections(INSPECTIONNO,PLANDATE,PORTNAME) VALUES('" + InspectionNo + "','" + PlanDate + "','" + PortName + "')");
                        }

                        ProjectCommon.ShowMessage("Inspection No : " + InspectionNo + " imported successfully.");
                        BindInspections();
                    }
                }
                catch
                {
                    ProjectCommon.ShowMessage("Invalid file to import.");
                }
            }
            else
            {
                ProjectCommon.ShowMessage("Please select file to import.");
            }
        }
        catch( Exception ex)
        {
        ProjectCommon.ShowMessage("Error in importing file. Error : "  + ex.Message);
        }
    }
    protected void btn_OpenImport_Click(object sender, EventArgs e)
    {
        dvImport.Visible = true;
    }
    protected void btnCancelImport_Click(object sender, EventArgs e)
    {
        dvImport.Visible = false;
    }
    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string Qno = ((LinkButton)sender).CommandArgument;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Ins_Observations WHERE [InspectionNo]='" + InspNo + "' AND QNO='" + Qno + "'"); 
        if(dt.Rows.Count >0 )
        {
            txtQId.Text = dt.Rows[0]["QuestionId"].ToString();
            txtQno.Text = dt.Rows[0]["QNO"].ToString();
            txtDeficiency.Text = dt.Rows[0]["Deficiency"].ToString();
            txtMC.Text = dt.Rows[0]["MasterComments"].ToString();
            txtC.Text = dt.Rows[0]["CorrectiveActions"].ToString();
            hfdId.Value = dt.Rows[0]["QNO"].ToString();

            dbAddObservations.Visible = true;
        }
    }
    protected void btn_AddObservation_Click(object sender, EventArgs e)
    {
        if (InspNo.Trim() != "")
        {

            lblInspectionNo.Text = InspNo;
            txtQId.Text = "";
            txtQno.Text = "";
            txtDeficiency.Text = "";
            txtMC.Text = "";
            txtC.Text = "";
            hfdId.Value = "";

            dbAddObservations.Visible = true;
        }
        else
        {
            ProjectCommon.ShowMessage("Please select an inspection first.");
            return; 
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (InspNo.Trim() != "")
        {
            if (txtQno.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter Qno.");
                txtQno.Focus();
                return;
            }
            else if (txtDeficiency.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter Deficiency.");
                txtDeficiency.Focus();
                return;
            }
            else if (txtMC.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter corrective actions.");
                txtQno.Focus();
                txtMC.Focus();
            }
            else
            {
                try
                {
                    string Deficiency = txtDeficiency.Text.Trim().Replace("'", "`");
                    string MC = txtMC.Text.Trim().Replace("'", "`");
                    string C = txtC.Text.Trim().Replace("'", "`");

                    if (hfdId.Value.Trim() == "")
                    {
                        Common.Execute_Procedures_Select_ByQuery("INSERT INTO VIMS_Ins_Observations( [InspectionNo],[Id],[QuestionId],[QNO],[Deficiency],[MasterComments],[CorrectiveActions]) VALUES('" + lblInspectionNo.Text.Trim() + "','" + hfdId.Value.Trim() + "','" + txtQId.Text.Trim() + "','" + txtQno.Text.Trim() + "','" + Deficiency + "','" + MC + "','" + C + "')");
                    }
                    else
                    {
                        Common.Execute_Procedures_Select_ByQuery("UPDATE VIMS_Ins_Observations SET QuestionId='" + txtQId.Text.Trim() + "',QNO='" + txtQno.Text.Trim() + "',Deficiency='" + Deficiency + "',MasterComments='" + MC + "',CorrectiveActions='" + C + "' WHERE ltrim(rtrim(InspectionNo))='" + InspNo + "' AND ltrim(rtrim(QNO))='" + txtQno.Text.Trim() + "'");
                    }

                    dbAddObservations.Visible = false;
                    BindObservations();
                    ProjectCommon.ShowMessage("Observation added successfully.");
                }
                catch( Exception ex)
                {
                    if (ex.Message.Contains("PRIMARY"))
                        ProjectCommon.ShowMessage("Same Qno is already exists.");
                }
                
            }
        }
        else
        {
            ProjectCommon.ShowMessage("Please select an inspection first.");
            return;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dbAddObservations.Visible = false;
    }
    protected void btnXML_Click(object sender, EventArgs e)
    {
        if (InspNo.Trim() != "")
        {
            string DataFile = Server.MapPath("~/Observations.xml");
            string FileName = "Observations_" + InspNo.Trim() + ".xml";

            DataSet ds = new DataSet();
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VIMS_Ins_Observations WHERE LTRIM(RTRIM(InspectionNo))='" + InspNo.Trim() + "'");
            dt.TableName = "VIMS_Ins_Observations";
            dt.DataSet.WriteXml(DataFile);
            //dt.DataSet.WriteXmlSchema(DataFile.Replace("Observations.xml", "Observations_Schema.xml"));
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.WriteFile(DataFile);
            Response.Flush();
            Response.End();
        }
    }
}
