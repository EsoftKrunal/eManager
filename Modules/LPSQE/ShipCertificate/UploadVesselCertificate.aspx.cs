using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using System.Web.UI.WebControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
public partial class UploadVesselCertificate : System.Web.UI.Page
{
    Authority Auth;
    public void BindVessel()
    {
        try
        {
            DataSet dsVessel = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName as Name", Convert.ToInt32(Session["loginid"].ToString()));
            ddlVessel.DataSource = dsVessel.Tables[0];
            ddlVessel.DataTextField = "Name";
            ddlVessel.DataValueField= "VesselId";
            ddlVessel.DataBind();  
            ddlVessel.Items.Insert(0,new ListItem("< Select >",""));   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void Bind_grid()
    {
        if (ddlVessel.SelectedValue.Trim()=="")
        {
            BindBlankGrid(); 
            return;
        }
        DataTable dt;
        //dt = Budget.getTable("select * VesselCertId,(Select CertName from dbo.Certmaster where certid=vc.Certid) as CertificateName,certnumber as CertificateNumber,replace(convert(varchar,issuedate,106),' ','-') as IssueDate,replace(convert(varchar,expirydate,106),' ','-') as ExpiryDate,Filename,CertType=case when CertType='P' then 'Provisional' when CertType='I' then 'Interim' when CertType='F' then 'Fullterm' when CertType='C' then 'Conditional' when CertType='P' then 'Permanent' else '' end,NextSurveyInterval as NSI,replace(convert(varchar,NextSurveyDue,106),' ','-') as NSD from dbo.VesselCertificates vc Where vc.vesselid=" + VesselId).Tables[0];
        dt = Budget.getTable("select *,replace(convert(varchar,issuedate,106),' ','-') as IssueDate1,replace(convert(varchar,expirydate,106),' ','-') as ExpiryDate1,replace(convert(varchar,NextSurveyDue,106),' ','-') as NextSurveyDue1,replace(convert(varchar,LastAnnSurvey,106),' ','-') as LastAnnSurvey1 from dbo.VesselCertificates_Temp Where vesselId=" + ddlVessel.SelectedValue).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add(new DataColumn("CertId", typeof(int)));
            foreach (DataRow Dr in dt.Rows)
            {
                DataTable d1 = Budget.getTable("select CertId from dbo.certmaster where lower(ltrim(rtrim(certname)))='" + Dr["CertName"].ToString() + "'").Tables[0];
                if (d1.Rows.Count > 0)
                {
                    Dr["CertId"] = d1.Rows[0][0];
                }
                else
                {
                    Dr["CertId"] = -1;
                }
            }
            gv_VDoc.DataSource = dt;
            gv_VDoc.DataBind();
        }
        else
        {
            BindBlankGrid();
        }
    }
    public void BindBlankGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("VesselCertId");
            dt.Columns.Add("CertId");
            dt.Columns.Add("CertCode");
            dt.Columns.Add("CertName");
            dt.Columns.Add("CertNumber");
            dt.Columns.Add("IssueDate1");
            dt.Columns.Add("ExpiryDate1");
            dt.Columns.Add("Filename");
            dt.Columns.Add("CertType");
            dt.Columns.Add("PlaceIssued");
            dt.Columns.Add("NextSurveyInterval");
            dt.Columns.Add("NextSurveyDue1");
            dt.Columns.Add("LastAnnSurvey1");
            dt.Columns.Add("ME");

            for (int i = 0; i <= 17 ; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dt.Rows[dt.Rows.Count - 1][0] = "-1";
                dt.Rows[dt.Rows.Count - 1][1] = "-1";
                dt.Rows[dt.Rows.Count - 1][2] = "";
                dt.Rows[dt.Rows.Count - 1][3] = "";
                dt.Rows[dt.Rows.Count - 1][4] = "";
                dt.Rows[dt.Rows.Count - 1][5] = "";
                dt.Rows[dt.Rows.Count - 1][6] = "";
                dt.Rows[dt.Rows.Count - 1][7] = "";
                dt.Rows[dt.Rows.Count - 1][8] = "";
                dt.Rows[dt.Rows.Count - 1][9] = "";
            }

            gv_VDoc.DataSource = dt;
            gv_VDoc.DataBind();
            gv_VDoc.SelectedIndex = -1;
        }
        catch (Exception ex) { throw ex; }
    }
    public string GetPath(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../EMANAGERBLOB/LPSQE/Vessel_Certificates/" + _path + ".PDF";
        }
        if (!(File.Exists(Server.MapPath(res)))) { res = ""; }
        return res;
    }
    public string FileExists(string _path)
    {
        string res = "";
        if (_path.StartsWith("U"))
        {
            res = "../" + _path;
        }
        else
        {
            res = "../EMANAGERBLOB/LPSQE/Vessel_Certificates/" + _path + ".PDF";
        }
        if (!(File.Exists(Server.MapPath(res)))) { return "none"; }
        else { return "block"; }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text ="";
        //VesselCertificateMaster vcm = (VesselCertificateMaster)this.Master;
        //PostBackTrigger pt =new PostBackTrigger();
        //pt.ControlID=btnUpload.UniqueID;
        //vcm.upLink.Triggers.Add(pt);   

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 160);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
            rptCerts.DataSource = Budget.getTable("select certid,certname from dbo.certmaster").Tables[0];
            rptCerts.DataBind();
            if (!(IsPostBack))
            {
                BindVessel();
                ddlVessel_SelectedIndexChanged(sender, e);
            }
        }
    }
    public DataSet getDatset(string filename)
    {
        DataSet DS = new DataSet();
        DS.Tables.Add(new DataTable());
        //---------------
        DS.Tables[0].Columns.Add(new DataColumn("ME",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Cert Code",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Certificate Name",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Cert Number",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Cert Type",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Place Issued",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Issue Date",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Last Annual / Interm",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Int",typeof(string)));
        DS.Tables[0].Columns.Add(new DataColumn("Next Survey Due",typeof(string)));  
        DS.Tables[0].Columns.Add(new DataColumn("Expiry Date",typeof(string)));
        DS.Tables[0].TableName = "Data";
        //---------------
        System.Data.OleDb.OleDbDataAdapter MyCommand;
        System.Data.OleDb.OleDbConnection MyConnection;
        MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + Server.MapPath("~\\EMANAGERBLOB\\LPSQE\\CertificateXLS\\") + filename + ";Extended Properties=Excel 8.0;");
        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select 'Main' as ME,* from [MAIN$]", MyConnection);
        MyCommand.Fill(DS, "Data");
        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select 'Extra' as ME,* from [EXTRA$]", MyConnection);
        MyCommand.Fill(DS,"Data");
        MyConnection.Close();
        //---------------
        for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
        {
            DS.Tables[0].Rows[i]["Issue Date"] = FormatDate(DS.Tables[0].Rows[i]["Issue Date"].ToString());
            DS.Tables[0].Rows[i]["Expiry Date"] = FormatDate(DS.Tables[0].Rows[i]["Expiry Date"].ToString());

            DS.Tables[0].Rows[i]["Last Annual / Interm"] = FormatDate(DS.Tables[0].Rows[i]["Last Annual / Interm"].ToString());
            DS.Tables[0].Rows[i]["Next Survey Due"] = FormatDate(DS.Tables[0].Rows[i]["Next Survey Due"].ToString());
        }
        //---------------
        return DS;
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedValue.Trim()=="")
        {
            lblMsg.Text = "Please select a vessel.";
            return;
        }

        //-------------------------------------------
        if (flp1.HasFile && flp1.PostedFile.ContentLength > 0)
        {
            //-------------------------------------------
            string fileName = Path.GetFileName(flp1.PostedFile.FileName);
            if (!(fileName.EndsWith("xls") || fileName.EndsWith("xlsx")))
            {
                lblMsg.Text = "Please check, only excel file are allowed to upload.";
                return;
            }
            try
            {
                flp1.PostedFile.SaveAs(Server.MapPath("~\\EMANAGERBLOB\\LPSQE\\CertificateXLS\\") + fileName);
            }
            catch (DirectoryNotFoundException ex)
            {
                lblMsg.Text = "Please check, if folder \"CertificateXLS\" not exists in the UserUploadDocuments folder. create it now.";
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                lblMsg.Text = "Please check, user \"ASPNET\" have not rights to upload file in folder \"CertificateXLS1\"";
                return;
            }
            //string VesselCode = fileName.Substring(0, 3);
            //DataTable dt = Budget.getTable("SELECT VESSELID FROM dbo.VESSEL WHERE VESSELCODE='" + VesselCode + "'").Tables[0];
            //if (dt.Rows.Count > 0)
            //{
                //ViewState["VesselId"] = dt.Rows[0][0];
                DataSet ds;
                //lblVessel.Text = VesselCode; 
                try
                {
                    ds= getDatset(fileName);
                    SavetoTemp(ds.Tables[0]);
                }
                catch
                {
                    lblMsg.Text = "File format or data is not valid for import.";
                    return; 
                } 
                if(ds.Tables.Count<=0)
                {
                    lblMsg.Text = "File format or data is not valid for import.";
                    return; 
                }
                else if(ds.Tables[0].Rows.Count<=0)
                {
                    lblMsg.Text = "File format or data is not valid for import.";
                    return; 
                }
                Bind_grid();
            //}
            //else
            //{
            //    lblMsg.Text = "Invalid file. (Filename must start with a valid vessel code)";
            //}
            //-------------------------------------------
        }
        else
        {
            lblMsg.Text = "Please select a valid excel file to upload.";
        }
    }
    protected string FormatDate(string dt)
    {
        char[] spliter=new char[1];
        spliter[0]=',';
        string[] vals=ConfigurationManager.AppSettings["DateSettings"].Split(spliter);

        int i, j, k;
        dt=dt.Trim().ToUpper().Replace("00:00:00","").Replace("12:00:00","").Replace("AM","").Replace("PM","").Trim();
        if (dt == "" || dt == "NA" || dt == "-" || dt == "--")
        {
            return "";
        }
        else
        {
            char[] splitter=new char[1];
                if (dt.Contains("-"))
                    splitter[0]= '-';
                else if (dt.Contains("/"))
                    splitter[0]= '/';
            string[] parts=dt.Split(splitter);

            return DateTime.Parse(parts[int.Parse(vals[0])] + "-" + GetMonthName(parts[int.Parse(vals[1])]) + "-" + parts[int.Parse(vals[2])]).ToString("dd-MMM-yyyy");
        }
    }
    public static string GetMonthName(String MonthId)
    {
        int mid = Convert.ToInt16(MonthId);
        switch (mid)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3: ;
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
            default:
                return "";
        }
    }
    protected void SavetoTemp(DataTable dt)
    {
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            string ME,CertCode,CertName, CertNo, CertType, IssueDate, ExpDate, PlaceIssued, LastAnnDate, SurveyInt, NextSurveyDue;
            ME = dt.Rows[i]["ME"].ToString();
            CertCode = dt.Rows[i]["Cert Code"].ToString();
            CertName = dt.Rows[i]["Certificates Name"].ToString();
            CertNo = dt.Rows[i]["Cert Number"].ToString();
            CertType = dt.Rows[i]["Cert Type"].ToString();
            IssueDate = dt.Rows[i]["Issue Date"].ToString();
            ExpDate = dt.Rows[i]["Expiry Date"].ToString();
            PlaceIssued = dt.Rows[i]["Place Issued"].ToString();
            LastAnnDate = dt.Rows[i]["Last Annual / Interm"].ToString();
            SurveyInt = dt.Rows[i]["Int"].ToString();
            NextSurveyDue = dt.Rows[i]["Next Survey Due"].ToString();

            IssueDate = (IssueDate.Trim() == "" || IssueDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + IssueDate.Trim() + "'";
            ExpDate = (ExpDate.Trim() == "" || ExpDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + ExpDate.Trim() + "'";
            NextSurveyDue = (NextSurveyDue.Trim() == "" || NextSurveyDue.Trim().ToUpper() == "NA") ? "NULL" : "'" + NextSurveyDue.Trim() + "'";
            LastAnnDate = (LastAnnDate.Trim() == "" || LastAnnDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + LastAnnDate.Trim() + "'";

            Budget.getTable("INSERT INTO DBO.VESSELCERTIFICATES_TEMP (ME,VESSELID,CERTCODE,CERTNAME,CERTNUMBER,ISSUEDATE,EXPIRYDATE,ISSUEDBY,PLACEISSUED,CERTTYPE,NEXTSURVEYINTERVAL,NEXTSURVEYDUE,ALERTFORSURVEY,ALERTFOREXPIRY,FILENAME,LASTANNSURVEY)" +
            "VALUES('" + ME + "'," + ddlVessel.SelectedValue + ",'" + CertCode + "','" + CertName + "','" + CertNo + "'," + IssueDate + "," + ExpDate + ",'','" + PlaceIssued + "','" + CertType + "','" + SurveyInt + "'," + NextSurveyDue + ",'','',''," + LastAnnDate + ")");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= gv_VDoc.Rows.Count - 1; i++)
        {
            string CertName, CertNo, CertType, IssueDate, ExpDate,PlaceIssued,LastAnnDate,SurveyInt,NextSurveyDue;
            int CertId=0;
            TextBox ib = (TextBox)gv_VDoc.Rows[i].FindControl("txtId");
            CertId = int.Parse(ib.Text);
            if (CertId > 0) // save records only if they mapped
            {
                //               ------------------ THIS CASE IS NOT APPLICABLE NOW ------------------------------

                //if (CertId == -1) // need to add this 
                //{
                //    Label tx = (Label)gv_VDoc.Rows[i].FindControl("lbl_Doc_Type");
                //    CertName = tx.Text;
                //    string Cat = "", SubCat = "", CertCode = "";

                //    HiddenField hfdIn = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdCertCode");
                //    CertCode = hfdIn.Value;

                //    hfdIn = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdME");
                //    if (hfdIn.Value.ToLower().Trim() == "main")
                //    {
                //        Cat = "3"; SubCat = "12";
                //    }
                //    else
                //    {
                //        Cat = "4"; SubCat = "13";
                //    }
                //    DataTable dt = Budget.getTable("INSERT INTO DBO.CERTMASTER(CertCatId,CertSubCatId,CertName,CertCode,CreatedBy,CreatedOn,StatusId)" +
                //        " values(" + Cat + "," + SubCat + ",'" + CertName + "','" + CertCode + "'," + Session["loginid"].ToString() + ",'" + DateTime.Today.ToString("dd-MMM-yyyy") + "','A');Select Max(CertId) From DBO.CERTMASTER").Tables[0];
                //    CertId = int.Parse(dt.Rows[0][0].ToString());
                //}

                //HiddenField hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdCertNo");
                //CertNo = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdCType");
                //CertType = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdIdate");
                //IssueDate = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdEDate");
                //ExpDate = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdPlace");
                //PlaceIssued = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdLastAnn");
                //LastAnnDate = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdInt");
                //SurveyInt = hfd.Value;
                //hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdNSD");
                //NextSurveyDue = hfd.Value;

                //IssueDate = (IssueDate.Trim() == "" || IssueDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + IssueDate.Trim() + "'";
                //ExpDate = (ExpDate.Trim() == "" || ExpDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + ExpDate.Trim() + "'";
                //NextSurveyDue = (NextSurveyDue.Trim() == "" || NextSurveyDue.Trim().ToUpper() == "NA") ? "NULL" : "'" + NextSurveyDue.Trim() + "'";
                //LastAnnDate = (LastAnnDate.Trim() == "" || LastAnnDate.Trim().ToUpper() == "NA") ? "NULL" : "'" + LastAnnDate.Trim() + "'";
                
                HiddenField hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("hfdVesselCertId");
                CertNo = hfd.Value;
                Budget.getTable("EXEC dbo.AddCertificate " + hfd.Value + "," + CertId.ToString());  

                //Budget.getTable("INSERT INTO DBO.VESSELCERTIFICATES (VESSELID,CERTID,CERTNUMBER,ISSUEDATE,EXPIRYDATE,ISSUEDBY,PLACEISSUED,CERTTYPE,NEXTSURVEYINTERVAL,NEXTSURVEYDUE,ALERTFORSURVEY,ALERTFOREXPIRY,FILENAME,LASTANNSURVEY)" +
                //"VALUES(" + ViewState["VesselId"].ToString() + "," + CertId.ToString() + ",'" + CertNo + "'," + IssueDate + "," + ExpDate + ",'','" + PlaceIssued + "','" + CertType + "','" + SurveyInt + "'," + NextSurveyDue + ",'','',''," + LastAnnDate + ")");
            }
        }
        lblMsg.Text = "Certificates imported successfully.";
        Bind_grid(); 
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_grid();
    }
    protected void DeleteRow(object sender, EventArgs e)
    {
        try
        {
            ImageButton im = ((ImageButton)(sender));
            string Id = im.CommandArgument;
            Budget.getTable("DELETE FROM DBO.VESSELCERTIFICATES_TEMP WHERE VESSELCERTID=" + Id);
            Bind_grid();
            lblMsg.Text = "Deleted successfuly.";  
        }
        catch
        {
            lblMsg.Text = "Unable to delete.";
        }
    }
}
