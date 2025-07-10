using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.SqlClient; 
/// <summary>
/// Summary description for MTMWeb
/// </summary>
public class MTMWeb
{
    public MTMWeb()
    {
    }
    public static DataSet getTable(string Query)
    {
        DataSet ds=new DataSet();
        new SqlDataAdapter(Query,new SqlConnection(ConfigurationManager.ConnectionStrings["MW_DBConnection"].ToString())).Fill(ds);
        return ds;
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
    public static bool  InsertUpdateUser(int UserId, string UserName, int Owner, string Password, int LoginId, string Status,string PoUser,string PoPassword, string Veselid1, string Veselid2,string Veselid3,string HTMLFILE)
    {
        bool isSuccess=true;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MW_DBConnection"].ToString());
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SqlCommand cmd = new SqlCommand("MW_InsertUpdateUser",con);
        cmd.Transaction = trans; 
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@UID", UserId));
        cmd.Parameters.Add(new SqlParameter("@UserName",UserName));
        cmd.Parameters.Add(new SqlParameter("@Owner", Owner));
        cmd.Parameters.Add(new SqlParameter("@Paswd", Password));
        cmd.Parameters.Add(new SqlParameter("@Loginid", LoginId));
        cmd.Parameters.Add(new SqlParameter("@Status", Status));
        cmd.Parameters.Add(new SqlParameter("@PoUser", PoUser));
        cmd.Parameters.Add(new SqlParameter("@PoPassword", PoPassword));
        cmd.Parameters.Add(new SqlParameter("@Vessel1", Veselid1));
        cmd.Parameters.Add(new SqlParameter("@Vessel2", Veselid2));
        cmd.Parameters.Add(new SqlParameter("@Vessel3", Veselid3));
        cmd.Parameters.Add(new SqlParameter("@HTMLFile", HTMLFILE));

        try
        {
            cmd.ExecuteNonQuery();
            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            isSuccess = false; 
        }
        finally
        {
            con.Close();
        }
        return isSuccess; 
    }
    public static void deleteUser(int UserId)
    {
        bool isSuccess = true;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MW_DBConnection"].ToString());
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SqlCommand cmd = new SqlCommand("DELETE FROM MW_UserLogin Where Userid=" + UserId.ToString(), con);
        cmd.Transaction = trans;
        cmd.CommandType = CommandType.Text;
        try
        {
            cmd.ExecuteNonQuery();
            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            isSuccess = false;
        }
        finally
        {
            con.Close();
        }
    }
}
