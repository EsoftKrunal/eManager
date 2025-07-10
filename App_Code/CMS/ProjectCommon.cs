using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessLogicLayer;
using System.IO;
using System.Security.Cryptography;


/// <summary>
/// Summary description for ProjectCommon
/// </summary>
public class ProjectCommon
{
    public static string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public static int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public static MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public static string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public static string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];
    public ProjectCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void SessionCheck()
    {
        if (HttpContext.Current.Session["loginid"] == null)
        {
            System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), "axdxasda", "alert('Application session expired. Please login again.');", true);
            string sessionExpiredURL = ConfigurationManager.AppSettings["SessionExpired"];
            HttpContext.Current.Response.Redirect(sessionExpiredURL);
        }

        //if (HttpContext.Current.Session["UserType"] == null)
        //{

        //    //if (ConfigurationManager.AppSettings["RunningLocation"] == "S")
        //    //{
        //    //    int index = HttpContext.Current.Request.Url.AbsolutePath.IndexOf("/", 1);
        //    //    string ForwardUrl = HttpContext.Current.Request.Url.AbsolutePath.Substring(index);
        //    //    ForwardUrl = HttpContext.Current.Request.Url.ToString().Replace(ForwardUrl, "").Replace("?" + HttpContext.Current.Request.QueryString,"");
        //    //    ForwardUrl += "/default.aspx?SessionExpired=1";
        //    //    HttpContext.Current.Response.Redirect(ForwardUrl);
        //    //}
        //    //else
        //    //{
        //    //    HttpContext.Current.Response.Write("<script src='JS/common.js'></script><script>OfficeReLogin('" + ConfigurationManager.AppSettings["RootURL"] + "');</script>");
        //    //    HttpContext.Current.Response.End();
        //    //}
        //    //return;

           
        //}
    }
    public static void SessionCheck_New()
    {
        if (HttpContext.Current.Session["loginid"] == null)
        {
            System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), "axdxasda", "alert('Application session expired. Please login again.');", true);
            string sessionExpiredURL = ConfigurationManager.AppSettings["SessionExpired"];
            HttpContext.Current.Response.Redirect(sessionExpiredURL);
        }
    }

    public static bool DeleteRecord(string mastername,string fieldvalue,int IdValue)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("DeleteMaster");
        ds.Tables.Clear();
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", mastername),
        new MyParameter("@PrimaryFieldName", fieldvalue), new MyParameter("@PrimaryValue", IdValue));
        bool res1;
        res1 = Common.Execute_Procedures_IUD(ds);
        return res1;
    }
    // class edited by Manita (Function for Get all fields from masters)
    public static DataSet SelectAllDataFromMasters(string MasterName, string FieldsName, string WhereCondition)
    {
        Common.Set_Procedures("SelectMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@FieldsName", FieldsName), new MyParameter("@WhereCondition", WhereCondition));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    public static DataSet SelectDataById(string MasterName, string FieldsName, string FieldValue)
    {
        Common.Set_Procedures("GetRowsByID");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@FieldId", FieldsName), new MyParameter("@FieldValue", FieldValue));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    public static void LoadDDL(DropDownList Ob, string MasterName, string IdField, string NameField, String AdditionalItem, string whereCondition)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetMasterByWhereCond");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@MasterName", "POS_" + MasterName), new MyParameter("@Field1", IdField), new MyParameter("@Field2", NameField), new MyParameter("@WhrCond", whereCondition));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = NameField;
        Ob.DataValueField = IdField;
        Ob.DataSource = ds;
        Ob.DataBind();
        if(AdditionalItem.Trim()!= "") 
        {
            Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        }
    }
    public static void LoadDDLBYID(DropDownList Ob, string MasterName, string IdFieldfordd,string IdField, string NameField, String AdditionalItem,string ID)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetRowsByID");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName",MasterName), new MyParameter("@FieldId", IdField), new MyParameter("@FieldValue", ID));
            
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = NameField;
        Ob.DataValueField = IdFieldfordd;
        Ob.DataSource = ds;
        Ob.DataBind();
        if (AdditionalItem.Trim() != "")
        {
            Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        }
    }
    public static void LoadDDLProfession(DropDownList Ob, string MODE, string IdField, string NameField, String AdditionalItem)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("BingProfession");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@MODE", MODE));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = NameField;
        Ob.DataValueField = IdField;
        Ob.DataSource = ds;
        Ob.DataBind();
        if (AdditionalItem.Trim() != "")
        {
            Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        }
    }
    public static void LoadCHKL(CheckBoxList Ob, string MasterName, string IdField, string NameField)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", "POS_" + MasterName), new MyParameter("@Field1", IdField), new MyParameter("@Field2", NameField));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = NameField;
        Ob.DataValueField = IdField;
        Ob.DataSource = ds;
        Ob.DataBind();
        //if (AdditionalItem.Trim() != "")
        //{
        //    Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        //}
    }
    public static void LoadCHKLSubpurpose(CheckBoxList Ob,string id)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetRowsByID");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", "SubpurposeMaster"), new MyParameter("@FieldId", "AIDID"), new MyParameter("@FieldValue", id));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = "Subpurpurpose";
        Ob.DataValueField = "SUBID";
        Ob.DataSource = ds;
        Ob.DataBind();
        //if (AdditionalItem.Trim() != "")
        //{
        //    Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        //}
    }
    public static void LoadCHKLBeniSubpurpose(CheckBoxList Ob, string id)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetRowsByID");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", "BeneficarySubPurpose"), new MyParameter("@FieldId", "AIDID"), new MyParameter("@FieldValue", id));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = "SubPurpose";
        Ob.DataValueField = "SubPurposeId";
        Ob.DataSource = ds;
        Ob.DataBind();
        //if (AdditionalItem.Trim() != "")
        //{
        //    Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        //}
    }
    public static DataSet getAllRowsFromTable(string TableName,string orderby)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("FetchAllMaster");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@MasterName", "POS_" + TableName), new MyParameter("@ORDERBY", orderby));
        ds = Common.Execute_Procedures_Select();
        return ds;
    }
    public static void LoadStatus(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Active", "1"));
        Ob.Items.Add(new ListItem("In-Active", "0"));
    }
    public static void LoadYear(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year-1),Convert.ToString(DateTime.Today.Year-1)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year), Convert.ToString(DateTime.Today.Year)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year+1), Convert.ToString(DateTime.Today.Year+1)));
        Ob.SelectedValue = DateTime.Today.Year.ToString() ;
    }
    public static void LoadMonth(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Jan",Convert.ToString(1)));
        Ob.Items.Add(new ListItem("Feb", Convert.ToString(2)));
        Ob.Items.Add(new ListItem("Mar", Convert.ToString(3)));
        Ob.Items.Add(new ListItem("Apr", Convert.ToString(4)));
        Ob.Items.Add(new ListItem("May", Convert.ToString(5)));
        Ob.Items.Add(new ListItem("Jun", Convert.ToString(6)));
        Ob.Items.Add(new ListItem("Jul", Convert.ToString(7)));
        Ob.Items.Add(new ListItem("Aug", Convert.ToString(8)));
        Ob.Items.Add(new ListItem("Sep", Convert.ToString(9)));
        Ob.Items.Add(new ListItem("Oct", Convert.ToString(10)));
        Ob.Items.Add(new ListItem("Nov", Convert.ToString(11)));
        Ob.Items.Add(new ListItem("Dec", Convert.ToString(12)));
        Ob.SelectedValue = DateTime.Today.Month.ToString() ;
    }
    public static string MakeId(Object MemberId)
    {
        return "SNM" + MemberId.ToString().PadLeft(5,'0');    
    }
    public static int RevertId(Object MemberId)
    {
        return int.Parse(MemberId.ToString().Substring(3));   
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
            case 3:;
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
            default :
                return "";
          }
        }

    public static string GetMonth(String MonthName)
    {
        string mName = MonthName.ToUpper();
        switch (mName)
        {
            case "JAN":
                return "01";
            case "FEB":
                return "02";
            case "MAR":
                return "03";
            case "APR":
                return "04";
            case "MAY":
                return "05";
            case "JUN":
                return "06";
            case "JUL":
                return "07";
            case "AUG":
                return "08";
            case "SEP":
                return "09";
            case "OCT":
                return "10";
            case "NOV":
                return "11";
            case "DEC":
                return "12";
            default:
                return "";
        }
    }
    public static string ConvertDateFormat(Object dt)
    {
        if (dt == null)
        {
            return "";
        }
        else if (dt.ToString().Trim() == "")
        {
            return "";
        }
        else
        {
            string[] res;
            char[] arr ={ '/' };

            res = dt.ToString().Split(arr);
            if (dt.ToString().Trim() == "")
                return "";
            else
                return res[1].ToString().PadLeft(2, '0') + "/" + res[0].ToString().PadLeft(2, '0') + "/" + res[2];
        }
    }
    public static string ConvertDateFormatDDToMM(Object dt)
    {
        if (dt == null)
        {
            return "";
        }
        else if (dt.ToString().Trim() == "")
        {
            return "";
        }
        else
        {
            string[] res;
            char[] arr ={ '/' };

            res = dt.ToString().Split(arr);
            if (dt.ToString().Trim() == "")
                return "";
            else
                return res[1].ToString().PadLeft(2, '0') + "/" + res[0].ToString().PadLeft(2, '0') + "/" + res[2];
        }
    }
    public static bool LoginStatus(object ob)
    {
        int a;
        if (ob == null)
        {
            return false;
        }
        else
        {
            try
            {
                a = int.Parse(ob.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public static string getFileName(int id,string Path)
    {
        string str="";
        string[] extenstions = { "jpg", "gif", "png" };
        int flag = 0;
        for (int i = 0; i <=extenstions.Length - 1; i++)
        {
            if (System.IO.File.Exists( Path + "\\" + id.ToString() + "." + extenstions[i]))
            {
                flag = 1;
                return id.ToString() + "." + extenstions[i];
            }
        }
        if (flag == 0)
        {
 
        }
        return "def.jpg";
    }
    public static void getFileNameanddel(int id, string Path)
    {
        string str = "";
        string[] extenstions = { "jpg", "gif", "png" };
        int flag = 0;
        for (int i = 0; i <= extenstions.Length-1 ; i++)
        {
            if (System.IO.File.Exists(Path + "\\" + id.ToString() + "." + extenstions[i]))
            {
                flag = 1;
                System.IO.File.Delete(Path + "\\" + id.ToString() + "." + extenstions[i]);
                //return id.ToString() + "." + extenstions[i];

            }
        }
        if (flag == 0)
        {

        }
       
    }
    public static string FormatCurrency(object Amount)
    {
        return "$ " + string.Format("{0:C}", Amount).Replace("$", "");
    }
    public static string FormatCurrencyWithoutSign(object Amount)
    {
        return string.Format("{0:C}", Amount).Replace("$", "");
    }
    //----------------------------------------------------
    public static void SetMails(SmtpClient _SmtpClient, MailMessage _MailMessage, string _ReplyMailAddress)
    {
        _SmtpClient.Host = _ServerName;
        _SmtpClient.Port = _Port;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _SmtpClient.EnableSsl = true;
        _MailMessage.From = _FromAdd;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
        // _ReplyMailAddress = _ReplyMailAddress.Replace("@abc.com", "@abcd.com");
        // _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }
    public static bool SendeMail(string FromAddress,string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message,out string Error,string AttachmentFilePath)
    {   
        bool MailSend = false;
        try
        {
           
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();       
            objMessage.From = new MailAddress(FromAddress);
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);
            
            foreach (string Address in ToAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.To.Add(ma);
                }
            }
            foreach (string Address in CCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            foreach (string Address in BCCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.Bcc.Add(ma);
                }
            }
            SetMails(objSmtpClient, objMessage, FromAddress);
            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }

    public static bool SendeMailforPV(string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath, Byte[] doc, string docName)
    {
        bool MailSend = false;
        try
        {

            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            objMessage.From = new MailAddress(FromAddress);
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            foreach (string Address in ToAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.To.Add(ma);
                }
            }
            foreach (string Address in CCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            foreach (string Address in BCCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.Bcc.Add(ma);
                }
            }
            SetMails(objSmtpClient, objMessage, FromAddress);
            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }

            if (docName.Trim() != "")
            {
                //objMessage.Attachments.Add(new Attachment(new MemoryStream(doc, docName)));

                MemoryStream pdf = new MemoryStream(doc);
                Attachment data = new Attachment(pdf, docName);
                objMessage.Attachments.Add(data);
            }
            //for (int i = 0; i <= hfc.Count - 1; i++)
            //{
            //    HttpPostedFile pf = hfc[i];
            //    Attachment attach = new Attachment(pf.InputStream, pf.FileName);
            //    objMessage.Attachments.Add(attach);
            //}
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }

    public static DataTable getBudgetDetails(string ShipID,string AccountCode,DateTime dtOn)
    {
        DataTable dt = new DataTable();
        DataTable dtRes = new DataTable();
        string CoCode = "", VslNo = "";
        //-------------
        dt = Common.Execute_Procedures_Select_ByQuery("SELECT company,VesselNo FROM VW_sql_tblSMDPRVessels WHERE SHIPID='" + ShipID + "'");
        if (dt.Rows.Count > 0)
        {
            CoCode = dt.Rows[0][0].ToString();    
            VslNo = dt.Rows[0][1].ToString();
        }

        dt=Common.Execute_Procedures_Select_ByQuery("EXEC DBO.getBudgetComittedReport '" + CoCode + "'," + VslNo + "," + AccountCode + ",'" + DateTime.Today.ToString("dd/MMM/yyyy") + "'");
        dtRes.Columns.Add("CYBudget");
        dtRes.Columns.Add("CYActandComm");
        dtRes.Columns.Add("Remaining");
        dtRes.Columns.Add("Utilization");

        dtRes.Rows.Add(dtRes.NewRow());
        if(dt.Rows.Count >0)
        {
            decimal CYBudget=Common.CastAsDecimal( dt.Rows[0]["FYBudg"].ToString());
            decimal CYActandComm = (Common.CastAsDecimal(dt.Rows[0]["A"].ToString()) - Common.CastAsDecimal(dt.Rows[0]["B"].ToString())) + Common.CastAsDecimal(dt.Rows[0]["C"].ToString());
            decimal Utilization=100;
            if (CYBudget != 0)
            {
                Utilization = Math.Round( (CYActandComm *100 )/ CYBudget,2);
            }
            dtRes.Rows[0]["CYBudget"] = CYBudget.ToString();
            dtRes.Rows[0]["CYActandComm"] = CYActandComm.ToString();
            dtRes.Rows[0]["Remaining"] = Convert.ToString(CYBudget - CYActandComm);
            dtRes.Rows[0]["Utilization"] = Utilization.ToString()+" %";
        }
        //Me.CYBudget = Nz(rs("FYBudg"), 0)
        //Me.CYActandComm = Nz(rs("A"), 0) - Nz(rs("B"), 0) + Nz(rs("C"), 0)
        //Me.CYBudget-Me.CYActandComm 
        //IIF(Me.CYBudget <>0,CYActandComm/CYBudget,1)
        return dtRes;
    }
    //----------------------------------------------------
    public static string getVesselNameById_Code(string VesselCode,int VesselId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELCODE='" + VesselCode + "' OR V.VESSELID=" + VesselId);
        return dt.Rows[0][0].ToString();
    }
    public static string getUserNameByID(string UID)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand("select (Firstname+' '+LastName) as UserName from Usermaster where LoginID="+UID+"", myConnection);
        Adp.SelectCommand = Command;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0].Rows[0][0].ToString();
        }
        catch { return null; }
    }
    public static string getUserPositionByID(string UID)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand("SELECT POSITIONNAME FROM DBO.POSITION P WHERE P.POSITIONID IN (SELECT POSITION FROM DBO.Hr_PersonalDetails WHERE USERID=" + UID + ")", myConnection);
        Adp.SelectCommand = Command;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0].Rows[0][0].ToString();
        }
        catch { return null; }
    }
    public static string getUserEmailByID(string UID)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand("select Email from Usermaster where LoginID=" + UID + "", myConnection);
        Adp.SelectCommand = Command;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0].Rows[0][0].ToString();
        }
        catch { return null; }
    }
    public static string getUploadFolder(DateTime dt)
    {
        return "UploadFiles/" + dt.ToString("MMM-yyyy") + "/"; 
    }
    public static string getLinkFolder(DateTime dt)
    {
        return "UploadFiles\\" + dt.ToString("MMM-yyyy") + "\\"; 
    }
    public static int getParentComponentId(int CompId)
    {
        int ret = 0;
        DataTable dt=Common.Execute_Procedures_Select_ByQuery("select componentid from dbo.ComponentMaster where componentcode=(select left(componentcode,len(componentcode)-3) from dbo.ComponentMaster where componentid=" + CompId.ToString() + ")");
        if(dt!=null)
            if (dt.Rows.Count > 0)
            {
                ret=Common.CastAsInt32(dt.Rows[0][0]); 
            }

        return ret;
    }
    public static string getParentComponents_Chain(int CompId)
    {
        string CompCode = "";
        string ret = "0";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select componentcode from dbo.ComponentMaster where componentid=" + CompId.ToString());
        if (dt.Rows.Count > 0)
        {
            CompCode = dt.Rows[0][0].ToString();
            dt = Common.Execute_Procedures_Select_ByQuery("select m.componentid from dbo.ComponentMaster m where m.Componentcode= + left((select componentcode from dbo.ComponentMaster where componentcode='" + CompCode.Trim() + "'),len(m.Componentcode))");
            if (dt != null)
                for (int i = 0; i <= dt.Rows.Count - 1;i++ )
                {
                    ret += "," + Common.CastAsInt32(dt.Rows[i][0]);
                }
        }
        return ret;
    }
    public static void ShowMessage(string Mess)
    {
        System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), "pt", "alert('" + Mess.Replace("\"", "").Replace("'", "`") + "');", true);
    }
    public static void CallScript(string Script)
    {
        System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), "pt1", Script, true);
    }
    
    public static void setSpareKeys(string PkId, ref string VesselCode, ref int ComponentId, ref string OfficeShip, ref int SpareId)
    {
        char[] splitter = { '#' };
        string[] res = PkId.Split(splitter);
        VesselCode = res[0];
        ComponentId = Common.CastAsInt32(res[1]);
        OfficeShip = res[2];
        SpareId = Common.CastAsInt32(res[3]);
    }
    public static float getROB(string PkId,DateTime dt)
    {
        string VesselCode="";
        int ComponentId=0;
        string OfficeShip="";
        int SpareId=0;
        setSpareKeys(PkId, ref VesselCode, ref ComponentId, ref OfficeShip, ref SpareId);
        return getROB(VesselCode, ComponentId, OfficeShip, SpareId,dt); 
    }
    public static float getROB(string VesselCode,int ComponentId,string OfficeShip,int SpareId,DateTime dtOnDate)
    {
        float rob=0;
        string qry = "select dbo.getROB('" + VesselCode + "'," + ComponentId.ToString() + ",'" + OfficeShip + "'," + SpareId.ToString() + ",'" + dtOnDate.ToString("dd-MMM-yyyy") + "')";
        DataTable dt=Common.Execute_Procedures_Select_ByQuery(qry); 
        if(dt!=null)
            if (dt.Rows.Count > 0)
            {
                rob = float.Parse(dt.Rows[0][0].ToString()); 
            }
        return rob;
    }
    public static string getValueFromDB(string tablename, string whereclause, string Columnname)
    {
        DataTable dtuser = Common.Execute_Procedures_Select_ByQuery("select " + Columnname + " from " + tablename + whereclause);
        string UserEmail = "";
        if (dtuser != null)
            if (dtuser.Rows.Count > 0)
            { UserEmail = dtuser.Rows[0][0].ToString(); }

        return UserEmail;
    }

    //---------------- eReport Section -----------------------------

    public static void Bind_Registers(Repeater rpt ,string FormNo, string RegistersList)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [OptionId],[OptionText] FROM [DBO].[ER_RegisterOptions] RO INNER JOIN [DBO].[ER_Registers] R ON R.[RegisterId] = RO.[RegisterId] WHERE R.[FormNo]='" + FormNo + "' AND RO.[OptionId] IN ( " + RegistersList + " )");
        if(RegistersList.Trim()!="")
            rpt.DataSource = dt;
        else
            rpt.DataSource = null;
        rpt.DataBind();
    }

    public static void LoadRegisters(CheckBoxList chk, string FormNo, int RegisterId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [OptionId],[OptionText] FROM [DBO].[ER_RegisterOptions] RO INNER JOIN [DBO].[ER_Registers] R ON R.[RegisterId] = RO.[RegisterId] WHERE R.[FormNo]='" + FormNo + "' AND R.[RegisterId] = " + RegisterId.ToString());
        chk.DataTextField = "OptionText";
        chk.DataValueField = "OptionId";
        chk.DataSource = dt;
        chk.DataBind();
    }

    public static void LoadRegisters(RadioButtonList rdo, string FormNo, int RegisterId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [OptionId],[OptionText] FROM [DBO].[ER_RegisterOptions] RO INNER JOIN [DBO].[ER_Registers] R ON R.[RegisterId] = RO.[RegisterId] WHERE R.[FormNo]='" + FormNo + "' AND R.[RegisterId] = " + RegisterId.ToString());
        rdo.DataTextField = "OptionText";
        rdo.DataValueField = "OptionId";
        rdo.DataSource = dt;
        rdo.DataBind();
    }
    public static void LoadRegisters(DropDownList ddl, string FormNo, int RegisterId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [OptionId],[OptionText] FROM [DBO].[ER_RegisterOptions] RO INNER JOIN [DBO].[ER_Registers] R ON R.[RegisterId] = RO.[RegisterId] WHERE R.[FormNo]='" + FormNo + "' AND R.[RegisterId] = " + RegisterId.ToString());
        ddl.DataTextField = "OptionText";
        ddl.DataValueField = "OptionId";
        ddl.DataSource = dt;
        ddl.DataBind();
    }

    public static void SetCheckboxListData(string value, CheckBoxList chk)
    {
        value = "," + value + ",";

        foreach (ListItem li in chk.Items)
        {
           li.Selected = value.Contains("," + li.Value + ",");
        }

    }

    public static DataTable ExecuteQuery(string Query)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString);
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand(Query, myConnection);
        Adp.SelectCommand = Command;
        Command.CommandTimeout = 600;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0];
        }
        catch { return null; }
    }
    //public static bool ClearAlert(string AlertKey)
    //{
    //    DataSet RetValue = new DataSet();
    //    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString);
    //    SqlCommand Command = new SqlCommand("EXEC [Shipsoft_Admin].[dbo].[CloseAlert] '" + AlertKey + "'", myConnection);
    //    Command.CommandTimeout = 600;
    //    try
    //    {
    //        Command.ExecuteNonQuery();
    //        return true;
    //    }
    //    catch { return false; }
    //}
    public static void ExportDatatable(HttpResponse Response, DataTable dt, String FileName)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    public static string Encrypt(string strText, string strEncrKey)
    {
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        try
        {
            byte[] bykey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
            cs.Write(InputByteArray, 0, InputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static string Decrypt(string strText, string sDecrKey)
    {
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        byte[] inputByteArray = new byte[strText.Length + 1];
        try
        {
            byte[] byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static string FormatCurrencyWithoutSignNoDecimal(object Amount)
    {
        return string.Format("{0:C2}", Amount).Replace("$", "").Replace("(", "-").Replace(")", "").Replace(".00", "");
    }
    public static string gerUserEmail(string LoginId)
    {
        DataTable dtuser = Common.Execute_Procedures_Select_ByQuery("select email from usermaster where loginid=" + LoginId);
        string UserEmail = "";
        if (dtuser != null)
            if (dtuser.Rows.Count > 0)
            { UserEmail = dtuser.Rows[0][0].ToString(); }

        return UserEmail;
    }

    public static bool View_Quote_Permission(int poid, int bidid, int userid)
    {
        int loginid = Common.CastAsInt32(HttpContext.Current.Session["loginid"]);
        if (loginid == 1)
            return true;

        DataSet ds = new DataSet();
        Common.Set_Procedures("View_Quote_Permission");
        ds.Tables.Clear();
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@POId", poid),
                              new MyParameter("@BidId", bidid),
                             new MyParameter("@UserID", userid));
        bool res1;
        res1 = Common.Execute_Procedures_IUD(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return Common.CastAsInt32(ds.Tables[0].Rows[0][0]) == 1;
        }
        else
            return false;

    }
    public static int Get_ManningAmount(string Company, string VesselCode, int Year)
    {
        int Amount = 0;
        string SQl = "select isnull(sum(amount),0) from dbo.Add_v_BudgetForecastYear where cocode='" + Company + "' " +
                    "and left(acctid,4) in (select accountnumber from dbo.sql_tblSMDPRAccounts where midcatid=12) and byear=" + Year.ToString() +
                    "and cast(right(acctid,4) as int) in (Select VesselId As VesselNo from Vessel with(nolock)  where VesselCode='" + VesselCode + "' AND AccontCompany='" + Company + "')";
        DataTable DtAmt = Common.Execute_Procedures_Select_ByQuery(SQl);
        Amount = Common.CastAsInt32(DtAmt.Rows[0][0]);
        return Amount;
    }

    public static bool SendMail(string[] ToAddresses, string[] CCAddresses, string[] BccAddresses, string Subject, string BodyContent, string[] AttachMentsPath)
    {
        string SenderAddress = _FromAdd.ToString() ;
        string SenderUserName = _UserName.ToString();
        string SenderPass = _Password;
        string MailClient = _ServerName;
        int Port = 587;
        //=========================================
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();
        string strToAddresses = string.Join(",", ToAddresses);
        string strCCAddresses = string.Join(",", CCAddresses);
        string strBCCAddresses = string.Join(",", BccAddresses);
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
            objMessage.From = new MailAddress(SenderAddress);     
            objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            objSmtpClient.Host = MailClient;
            objSmtpClient.Port = Port;
           
            foreach (string add in ToAddresses)
            {
                objMessage.To.Add(add);
            }
            if (CCAddresses != null)
            {
                foreach (string add in CCAddresses)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(add))
                        {
                            objMessage.CC.Add(add);
                        } 
                    }
                    catch { }
                }
            }
            if (BccAddresses != null)
            {
                foreach (string add in BccAddresses)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(add))
                        {
                            objMessage.Bcc.Add(add);
                        }
                    }
                    catch { }
                }
            }
            objSmtpClient.EnableSsl = true;
            objMessage.Body = BodyContent;

            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            foreach (string AttachMentPath in AttachMentsPath)
            {
                if (System.IO.File.Exists(AttachMentPath))
                    objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            }
            objSmtpClient.Send(objMessage);
            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
    }

    public static bool SendMailAsync(string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string[] AttachMentsPath)
    {
        string SenderAddress = _FromAdd.ToString();
        string SenderUserName = _UserName.ToString();
        string SenderPass = _Password;
        string MailClient = _ServerName;
        int Port = 587;
        //=========================================
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();
        string strToAddresses = string.Join(";", ToAddresses);
        string strCCAddresses = string.Join(";", CCAddresses);
        try
        {
            objMessage.From = new MailAddress(SenderAddress);
            objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            objSmtpClient.Host = MailClient;
            objSmtpClient.Port = Port;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
            foreach (string add in ToAddresses)
            {
                objMessage.To.Add(add);
            }
            if (CCAddresses != null)
            {
                foreach (string add in CCAddresses)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(add))
                        {
                            objMessage.CC.Add(add);
                        }
                    }
                    catch { }
                }
            }
            objSmtpClient.EnableSsl = true;
            objMessage.Body = BodyContent;

            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            //objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            foreach (string AttachMentPath in AttachMentsPath)
            {
                if (System.IO.File.Exists(AttachMentPath))
                    objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            }
            objSmtpClient.SendAsync(objMessage, null);
            return true;
        }
        catch (System.Exception ex)
        {
            return false;
        }
    }
 
    public static string GetFullMonthName(String MonthId)
    {
        int mid = Convert.ToInt16(MonthId);
        switch (mid)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                ;
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            default:
                return "";
        }
    }

    public static string getUserEmailByUserName(string UID)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand("select (FirstName+' '+LastName)as UserName from Usermaster where userID='" + UID + "'", myConnection);
        Adp.SelectCommand = Command;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0].Rows[0][0].ToString();
        }
        catch { return null; }
    }
    public static string FormatCurrency2(object Amount)
    {
        decimal amt = Common.CastAsInt32(Amount);
        if (amt < 0)
            return "-" + amt.ToString("C3", System.Globalization.CultureInfo.CurrentCulture).Replace(".000", "").Replace("$", "").Replace("(", "").Replace(")", "").Replace("Rs.", "");
        else
            return amt.ToString("C3", System.Globalization.CultureInfo.CurrentCulture).Replace(".000", "").Replace("$", "").Replace("(", "").Replace(")", "").Replace("Rs.", "");
    }

    public static bool VERIFIYPOSTINGS(string Company, ref string Error)
    {
        using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString()))
        {
            string UpdateSQL = "Update " +
                               "dbo.qApEntries set  " +
                               "postrun=(select postrun from tblApHistHeader ap where ap.transid=dbo.qApEntries.transid), " +
                               "OrigInvoiceNum=(select InvoiceNum from tblApHistHeader ap where ap.transid=dbo.qApEntries.transid), " +
                               "GLYear=(select FiscalYear from tblApHistHeader ap where ap.transid=dbo.qApEntries.transid), " +
                               "GLPeriod=(select GLPeriod from tblApHistHeader ap where ap.transid=dbo.qApEntries.transid) " +
                               "where  " +
                               "transid is not null and postrun is null and company='" + Company.ToUpper().Trim() + "'  " +
                               "and exists(select postrun from tblApHistHeader ap where ap.transid=dbo.qApEntries.transid)";

            DataSet RetValue = new DataSet();
            SqlCommand Command = new SqlCommand(UpdateSQL, myConnection);
            Command.CommandType = CommandType.Text;
            myConnection.Open();
            try
            {
                Command.ExecuteNonQuery();
                Error = "";
                return true;
            }
            catch (Exception ex) { Error = ex.Message; return false; }
        }
    }

  
}
