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
using System.IO;
using ShipSoft.CrewManager.Operational;

public partial class CrewRecord_CrewDocumentsDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {
        int crewid;
        int passport=0;
        int visa=0;
        int seamanbook =0;
        int license, course, cargo, other;
       crewid=Convert.ToInt32(Session["CrewId"].ToString());
       if (chkpassport.Checked==true)
       {
           passport = 1;
       }
       else
       {
           passport = 0;
       }


       if (chkvisa.Checked == true)
{
    visa=1;
}
else
{
    visa = 0;
}
if (chkseamanbook.Checked == true)
{
    seamanbook=1;
}
else 
{
    seamanbook = 0;
}
if (this.chklicense.Checked == true)
{
    license  = 1;
}
else
{
    license = 0;
}
if (this.chkcourse.Checked == true)
{
    course = 1;
}
else
{
    course = 0;
}
if (this.chkcargo.Checked == true)
{
    cargo = 1;
}
else
{
    cargo = 0;
}
if (chkother.Checked == true)
{
    other = 1;
}
else
{
    other = 0;
}
 DataTable dt=(CrewDocument.selectCrewDocument(crewid,passport,visa,seamanbook,license,course,cargo,other));
 //for(int i=0; i<dt.Rows.Count ;i++)  
 //{ 
 //     UtilityManager um=new UtilityManager() ;
    
 //    DataRow dr;
 //    dr = dt.Rows[i];
 //    string str = um.DownloadFileFromServer(dr["imagepath"].ToString(), dr["mode"].ToString());
 //    dr["imagepath"] = str;
 //    dt.AcceptChanges(); 
     
 //    //ds.Rows[0][0] = "1";
      
 //     //dr["imagepath"] = 
 //}
 Session.Add("mailattachments", dt);
 Response.Redirect("SendCrewDocuments.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDetails.aspx");
    }
}
