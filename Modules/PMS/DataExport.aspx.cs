using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;

public partial class DataExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        int userid = 0;
        try {
            userid = int.Parse(Session["loginid"].ToString());
        }catch(Exception ex)
        { }

        if (!IsPostBack)
        {
            if (userid == 1)
            {
                ddlVessel.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Vessel ORDER BY VESSELNAME");
                ddlVessel.DataTextField = "VesselName";
                ddlVessel.DataTextField = "VesselCode";
                ddlVessel.DataBind();
            }
            else
            {
                ddlVessel.Visible = false;
                Button1.Visible = false;
            }
        }
    }
   
    protected void btnexport_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();        
        sb.AppendLine("USE eMANAGER");
        sb.AppendLine("GO");
        sb.AppendLine("---------------------------------");
        string[] tables = { "VSL_BreakDownMaster", "VSL_BreakDownSpareDetails", "VSL_BreakDown_Attachments" };
        foreach(string table in tables)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO." + table + " where vesselcode='" + ddlVessel.SelectedValue + "'");
            sb.AppendLine("--- EXPORTING TABLE " + table);
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("INSERT INTO " + table);
                sb.Append(" VALUES(");
                List<string> cols = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (Convert.IsDBNull(dr[dc]))
                    {
                        cols.Add("NULL");
                    }
                    else
                    {
                        if (dc.DataType == typeof(System.Byte) || dc.DataType == typeof(System.Decimal) || dc.DataType == typeof(System.Double) || dc.DataType == typeof(System.Int16) || dc.DataType == typeof(System.Int32) || dc.DataType == typeof(System.Int64) || dc.DataType == typeof(System.SByte) || dc.DataType == typeof(System.Single) || dc.DataType == typeof(System.UInt16) || dc.DataType == typeof(System.UInt32) || dc.DataType == typeof(System.UInt64))
                            cols.Add(dr[dc].ToString());
                        else
                            cols.Add("'" + dr[dc].ToString().Replace("'","''") + "'");                        
                    }
                }
                sb.Append(String.Join(",", cols.ToArray()));
                sb.AppendLine(")");
            }          
            sb.AppendLine("--- EXPORTING TABLE END ");        }
        sb.AppendLine("---------------------------------");
        littext.Text = sb.ToString();
    }
}