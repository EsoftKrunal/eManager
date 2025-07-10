using System;
using System.Data;
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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
   
    protected void Button1_Click1(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AddDirectory(@"C:\Users\pankaj.k\Desktop\PMS");
            zip.AddFile(@"C:\Users\pankaj.k\Desktop\PacketConfig.xml","");
            zip.Save(@"C:\Users\pankaj.k\Desktop\PACK-001.zip");
        }
    }
}