using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Vessels
/// </summary>
public class Administration
{ 
    public static string AdminConStr
    {
        get
        {
            try
            {
                return ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
            }
            catch
            {
                return "";
            }
        }
 
}

    public Administration()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static  DataTable CheckLogin(string UserName,string Password)
    {
        if (AdminConStr == "")
        {
            return null;
        }
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(AdminConStr);
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand cmd=new SqlCommand();
        cmd.Connection = myConnection; 
        cmd.CommandText="SELECT * FROM USERMASTER WHERE USERID='" + UserName +"' AND PASSWORD='" + Password +"'";
        Adp.SelectCommand =cmd;
        Adp.Fill(RetValue);
        if (RetValue.Tables.Count > 0)
        {
            if (RetValue.Tables[0].Rows.Count > 0)
            {
                return RetValue.Tables[0];
            }
            else
            {
                return null ;   
            }
        }
        else
        {
            return null;
        }
    }
    public static  DataTable getUserDetails(int LoginId)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(AdminConStr);
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM USERMASTER WHERE LoginID=" + LoginId;
        Adp.SelectCommand = cmd;
        Adp.Fill(RetValue);
        if (RetValue.Tables.Count > 0)
        {
            if (RetValue.Tables[0].Rows.Count > 0)
            {
                return RetValue.Tables[0];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
