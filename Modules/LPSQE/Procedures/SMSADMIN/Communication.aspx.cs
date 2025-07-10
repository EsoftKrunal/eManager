using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Communication : System.Web.UI.Page
{
    AuthenticationManager Auth;


    protected void radCommtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if(radCommtype.SelectedIndex==0)
            frm_Comm.Attributes.Add("src", "FormsComm.aspx");
        else
            frm_Comm.Attributes.Add("src", "MWCommunication1.aspx");

    }
}