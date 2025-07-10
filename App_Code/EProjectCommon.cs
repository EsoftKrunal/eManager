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
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Net.Mail;

/// <summary>
/// Summary description for ProjectCommon
/// </summary>
public class EProjectCommon
{
    public EProjectCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public static int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public static MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public static string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public static string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];
    // edited by manita Singhal
    public static void LoadDDL(DropDownList Ob, string MasterName, string DisplayField, string ValueField, String AdditionalItem, string WhereCondition)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetMasterDataForDropdown");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@DisplayField", DisplayField), new MyParameter("@ValueField", ValueField), new MyParameter("@WhereCondition", WhereCondition));
        ds = Common.Execute_Procedures_Select();
        Ob.DataTextField = DisplayField;
        Ob.DataValueField = ValueField;
        Ob.DataSource = ds;
        Ob.DataBind();
        if (AdditionalItem.Trim() != "NONE")
        {
            Ob.Items.Insert(0, new ListItem(AdditionalItem, "0"));
        }
    }
    public static DataTable getTable(string MasterName, string DisplayField, string ValueField, String AdditionalItem, string WhereCondition)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("GetMasterDataForDropdown");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@DisplayField", DisplayField), new MyParameter("@ValueField", ValueField), new MyParameter("@WhereCondition", WhereCondition));
        ds = Common.Execute_Procedures_Select();
        if (AdditionalItem.Trim().ToUpper() != "NONE")
        {
            ds.Tables[0].Rows.InsertAt(ds.Tables[0].NewRow(), 0);
            ds.Tables[0].Rows[0][0] = "";
            ds.Tables[0].Rows[0][1] = "0";
        }
        return ds.Tables[0]; 
    }
    public static string getName(object ID, string TableName, string TextColumnName, string ValueColumnName)
    {
        DataSet ds = new DataSet();
        Int32 intID = 0;
        try
        {
            intID = Int32.Parse(ID.ToString());
        }
        catch{}
        Common.Set_Procedures("Execquery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", "Select " + TextColumnName + " From " + TableName + " Where " + ValueColumnName + "=" + intID.ToString()));
        ds = Common.Execute_Procedures_Select();

        if (ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0].Rows[0][0].ToString();
        else
            return "";
    }

    public static string getNameByCompany(object ID,int CompanyId, string TableName, string TextColumnName, string ValueColumnName)
    {
        DataSet ds = new DataSet();
        Int32 intID = 0;
        try
        {
            intID = Int32.Parse(ID.ToString());
        }
        catch { }
        Common.Set_Procedures("Execquery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", "Select " + TextColumnName + " From " + TableName + " Where " + ValueColumnName + "=" + intID.ToString() + " and CompanyId="+ CompanyId.ToString()));
        ds = Common.Execute_Procedures_Select();

        if (ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0].Rows[0][0].ToString();
        else
            return "";
    }

    public static void LoadYear(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year-1),Convert.ToString(DateTime.Today.Year-1)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year), Convert.ToString(DateTime.Today.Year)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year+1), Convert.ToString(DateTime.Today.Year+1)));
        Ob.SelectedValue = DateTime.Today.Year.ToString() ;
    }
    // class edited by manita Singhal (Function for Get all fields from masters)
    public static DataSet SelectAllDataFromMasters(string MasterName, string FieldsName, string WhereCondition)
    {
        Common.Set_Procedures("SelectMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@FieldsName", FieldsName), new MyParameter("@WhereCondition", WhereCondition));
        DataSet result = new DataSet();
        result = Common.Execute_Procedures_Select();
        return result;
    }
    // class edited by manita Singhal (Function for deletion from masters)
    public static bool DeleteDataFromMasters(string MasterName, string WhereCondition)
    {
        DataSet ds = new DataSet();
        Common.Set_Procedures("DeleteMastersData");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@MasterName", MasterName), new MyParameter("@WhereCondition", WhereCondition));
        bool res1;
        res1 = Common.Execute_Procedures_IUD(ds);
        return res1;
    }
    // class edited by manita Singhal
    public static void LoadFiveYearsPreviousNNextFromCurrent(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 5), Convert.ToString(DateTime.Today.Year - 5)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 4), Convert.ToString(DateTime.Today.Year - 4)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 3), Convert.ToString(DateTime.Today.Year - 3)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 2), Convert.ToString(DateTime.Today.Year - 2)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 1), Convert.ToString(DateTime.Today.Year - 1)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year), Convert.ToString(DateTime.Today.Year)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 1), Convert.ToString(DateTime.Today.Year + 1)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 2), Convert.ToString(DateTime.Today.Year + 2)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 3), Convert.ToString(DateTime.Today.Year + 3)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 4), Convert.ToString(DateTime.Today.Year + 4)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 5), Convert.ToString(DateTime.Today.Year + 5)));
        Ob.SelectedValue = DateTime.Today.Year.ToString();
    }
    // class edited by manita Singhal
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
    public static void LoadPhoneDropDown(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Us/Canada", "Us/Canada"));
        Ob.Items.Add(new ListItem("Other", "Other"));
    }
    public static void LoadInvoiceStatus(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Draft", "Draft"));
        Ob.Items.Add(new ListItem("Active", "Active"));
        Ob.Items.Add(new ListItem("InActive", "InActive"));
    }
    public static void LoadTransmisionMethod(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Prompt", "Prompt"));
        Ob.Items.Add(new ListItem("Email", "Email"));
        Ob.Items.Add(new ListItem("cXML", "cXML"));
        Ob.Items.Add(new ListItem("Buy Online", "Buy Online"));
    }
    public static void LoadPaymentMethod(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Invoice", "Invoice"));
        Ob.Items.Add(new ListItem("P-Card", "P-Card"));
    }
    public static void LoadInvoiceMatching(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("2-Way", "2-Way"));
        Ob.Items.Add(new ListItem("3-Way", "3-Way"));
        Ob.Items.Add(new ListItem("None", "None"));
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
        for (int i = 0; i < extenstions.Length - 1; i++)
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
    public static string Encrypt(string strText, string strEncrKey)
    {
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        try
        {
            byte[] bykey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0,8));
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
    public static string ChangeDateFormat(object _InDate)
    {
        try
        {
            string InDate = _InDate.ToString();
            int date, month, year;
            char[] ch = {'/'};
            string[] arr = InDate.Split(ch);
            date = int.Parse(arr[1].ToString());
            month = int.Parse(arr[0].ToString());
            year = int.Parse(arr[2].ToString());
            DateTime dt = new DateTime(year, month, date);
            return dt.ToString("dd-MMM-yyyy");
        }
        catch
        {
            return "";
        }
    }
    //-------------------------------
    # region GET NAME/TEXT FOR PASSSING THE ID
    // GET NAME/TEXT FOR PASSSING THE ID
    public static string getUOMNameById(object UOMId)
    {
        return getName(UOMId, "UOMMaster", "UOMName", "UOMId");
    }
    public static string getCommodityNameById(object CommodityId)
    {
        return getName(CommodityId, "CommodityMaster", "Name", "CommodityId");
    }
    public static string getCurrencyNameById(object CurrencyId)
    {
        return getName(CurrencyId, "CurrencyMaster", "Code", "CurrencyId");
    }
    public static string getSupplierNameById(object SupplierId)
    {
        return getName(SupplierId, "SupplierMaster", "Name", "SupplierId");
    }
    public static string getUserNameById(object SupplierId)
    {
        return getName(SupplierId, "UserMaster", "FName", "UserId");
    }
    public static string getWareHouseNameById(object SupplierId)
    {
        return getName(SupplierId, "WareHouseMaster", "WareHouseName", "WareHouseId");
    }
    # endregion
    //-------------------------------
    # region GETTING TABLES FOR LOADING THE DDLS
    // GET TABLE FOR LOADING DDL'S
    public static DataTable getUOM()
    {
        return getTable("UOMMaster", "UOMName", "UOMId", "","");
    }
    public static DataTable getCommodity()
    {
        return getTable("CommodityMaster", "Name", "CommodityId", "","");
    }
    public static DataTable getCurrency()
    {
        return getTable("CurrencyMaster", "Code", "CurrencyId", "","");
    }
    public static DataTable getCurrencyNames()
    {
        return getTable("CurrencyMaster", "Name", "CurrencyId", "", "");
    }
    public static DataTable getContracts()
    {
       return getTable("Contracts", "Name", "ContractId", "","");
    }
    public static DataTable getUsers()
    {
        return getTable("UsersMaster", "FName", "UserId", "", "");
    }
    public static DataTable getWareHouse()
    {
        return getTable("WareHouse", "Name", "WareHouseId", "", "");
    }
    public static DataTable getViews(string MasterName)
    {
        DataTable dt = getTable("Views", "ViewName", "ViewId", "NONE", "MasterName='" + MasterName + "'");
        if(dt!=null )
        {
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1]["ViewId"] = -1;
            dt.Rows[dt.Rows.Count - 1]["ViewName"] = "Create View..";

            dt.Rows.InsertAt(dt.NewRow(),0);
            dt.Rows[0]["ViewId"] = 0;
            dt.Rows[0]["ViewName"] = "ALL";
        }
        return dt;
    }
    public static DataTable getMainMenus()
    {
        return SelectAllDataFromMasters("MainMenu", " * ", "").Tables[0];
    }
    public static DataTable getChildMenus(int _MainMenuId)
    {
        return SelectAllDataFromMasters("ChildMenu", " * ", " MainMenuId=" + _MainMenuId.ToString()).Tables[0];
    }
    public static int getMainMenuIdByPageName(string _Url)
    {
        DataTable dt = SelectAllDataFromMasters("MainMenu", "MainMenuId", "lower(ltrim(rtrim(PageUrl))) like '%" + _Url.ToLower().Trim() + "'").Tables[0];
        if (dt == null)
            return 0;
        else if (dt.Rows.Count > 0)
            return Int32.Parse(dt.Rows[0][0].ToString());
        else
            return 0;
    }
    public static string gerUserEmail(string LoginId)
    {
        DataTable dtuser = ECommon.Execute_Procedures_Select_ByQueryAdmin("select email from usermaster where loginid=" + LoginId);
        string UserEmail = "";
        if (dtuser != null)
            if (dtuser.Rows.Count > 0)
            { UserEmail = dtuser.Rows[0][0].ToString(); }

        return UserEmail;
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

    public static bool SendeMail_MTM_ATT(int LoginId, string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string[] AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            string Mailfrom;
            Mailfrom = "";
            //using (MailMessage objMessage = new MailMessage())
            //{
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = _ServerName;
            objSmtpClient.Port = _Port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                 | SecurityProtocolType.Tls11
                                 | SecurityProtocolType.Tls12;
            objSmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
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
                    objMessage.CC.Add(ma);
                }
            }

            SetMails(objSmtpClient, objMessage, FromAddress);

            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            Attachment Att = null;

            if (AttachmentFilePath.Length > 0)
            {
                foreach (string tmp in AttachmentFilePath)
                {
                    if (tmp.Trim() != "")
                    {
                        Att = new Attachment(tmp);
                        objMessage.Attachments.Add(Att);
                    }
                }
            }

            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";

            Att.Dispose();
            //}
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }

    public static void SetMails(SmtpClient _SmtpClient, MailMessage _MailMessage, string _ReplyMailAddress)
    {
        _SmtpClient.Host = _ServerName;
        _SmtpClient.Port = _Port;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _MailMessage.From = _FromAdd;
        _SmtpClient.EnableSsl = true;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
        _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }

    public static bool SendeMail_MTM(int LoginId, string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            string Mailfrom;
            Mailfrom = "";
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = _ServerName;
            objSmtpClient.Port = _Port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                 | SecurityProtocolType.Tls11
                                 | SecurityProtocolType.Tls12;
            objSmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
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
                    objMessage.CC.Add(ma);
                }
            }

            SetMails(objSmtpClient, objMessage, FromAddress);

            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            Attachment Att = null;
            if (AttachmentFilePath.Trim() != "")
            {
                Att = new Attachment(AttachmentFilePath);
                objMessage.Attachments.Add(Att);
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
            Att.Dispose();
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    #endregion
}
