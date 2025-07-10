using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_Emtm_Performance : System.Web.UI.Page
{
    protected void imgDownloadManual_OnClick(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "Office_Policies_Procedures.pdf"));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(getContentFile());
        }
        catch { }

    }
    protected void imgDownloadManual2_OnClick(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "SMD_Procedures.pdf"));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(getContentFile_SP());
        }
        catch { }

    }
    
    public byte[] getContentFile()
    {
        byte[] ret = null;
        DataSet RetValue = new DataSet();

       
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlCommand Cmd = new SqlCommand("select FileName,FileContent from SMS_ManualDetails WHERE ManualId=12 and rtrim(ltrim(SectionId))='7.2'", myConnection);
        SqlDataAdapter Adp = new SqlDataAdapter(Cmd);
        Adp.Fill(RetValue, "File");
        if (RetValue.Tables["File"].Rows.Count > 0)
        {
            ret = (byte[])RetValue.Tables["File"].Rows[0]["FileContent"];
        }
        return ret;
    }
    public byte[] getContentFile_SP()
    {
        byte[] ret = null;
        DataSet RetValue = new DataSet();

       
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlCommand Cmd = new SqlCommand("select FileName,FileContent from SMS_ManualDetails WHERE ManualId=30 and rtrim(ltrim(SectionId))='1'", myConnection);
        SqlDataAdapter Adp = new SqlDataAdapter(Cmd);
        Adp.Fill(RetValue, "File");
        if (RetValue.Tables["File"].Rows.Count > 0)
        {
            ret = (byte[])RetValue.Tables["File"].Rows[0]["FileContent"];
        }
        return ret;
    }
  
}