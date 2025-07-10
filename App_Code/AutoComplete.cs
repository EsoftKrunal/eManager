using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
/// 
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{ 
    public AutoComplete()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [WebMethod]
    //[System.Web.Script.Services.ScriptMethod]
    public string[] GetMenuList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        DataTable dt = GetMenuRecords(prefixText);
        List<string> items = new List<string>(count);

        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            string strName = dt.Rows[i][0].ToString();
            items.Add(strName);
        }
        return items.ToArray();
    }

    public DataTable GetMenuRecords(string strName)
    {
        string strConn = ECommon.ConString;
        SqlConnection con = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Parameters.AddWithValue("@Name", strName);
        cmd.CommandText = "Select (CASE WHEN COALESCE(MenuID,0)=0 then MenuName else cast(COALESCE(menuID,0) as varchar)  + ' - ' +  MenuName end ) menuname from appmstr_menu where MenuName like '%'+@Name+'%'";
        DataSet objDs = new DataSet();
        SqlDataAdapter dAdapter = new SqlDataAdapter();
        dAdapter.SelectCommand = cmd;
        con.Open();
        dAdapter.Fill(objDs);
        con.Close();
        return objDs.Tables[0];
    }
}