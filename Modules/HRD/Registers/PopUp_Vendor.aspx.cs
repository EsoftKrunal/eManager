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

public partial class Registers_PopUp_Vendor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Page.IsPostBack == false)
        {
  
        }
    }
    protected void btn_Save_Port_Click(object sender, EventArgs e)
    {
      
        DataTable dt = Supplier.CheckDuplicateSupplier(txt_CompName.Text);
        if (dt.Rows.Count > 0)
        {
            lbl_Port_Message.Text = "Supplier Name Already Exists.";
        }
       
        else
        {
            try
            {
                Supplier.insertUpdateSupplierDetails_PoP("InsertUpdateSupplierDetails_PopUp", txt_CompName.Text.Trim(), txtPortName.Text, Convert.ToInt32(Session["loginid"].ToString()));   
                lbl_Port_Message.Text = "Record Saved Successfully";
            }
            catch(SystemException es)
            {
                lbl_Port_Message.Text = "Error in saving the data ,Please Try Again";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
