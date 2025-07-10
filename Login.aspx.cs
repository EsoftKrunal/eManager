using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;   
using System.Web.UI.WebControls.WebParts;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Login : System.Web.UI.Page
{
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            Session.Clear();
            Session.Abandon();
            if(Request.QueryString["mess"]!=null)
            {
                MessageBox1.ShowMessage("Password changed successfully. Please login again.",false); 
            }
        }
        txt_Login.Focus();  
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        // Validations
        //{
        //    TextReader tr = new StreamReader( Server.MapPath("./")+ "default.aspx");
        //    TextWriter tw = new StreamWriter(Server.MapPath("./") + "default2.aspx");
        //    tw.Write(tr.ReadToEnd().Replace("Default", "_Default2"));
        //    tr.Dispose();
        //    tw.Dispose(); 
        //}
        //{
        //    TextReader tr = new StreamReader(Server.MapPath("./") + "default.aspx.cs");
        //    TextWriter tw = new StreamWriter(Server.MapPath("./") + "default2.aspx.cs");
        //    tw.Write(tr.ReadToEnd().Replace("Default", "_Default2"));
        //    tr.Dispose();
        //    tw.Dispose();
        //}
        if (!(Validator.ValidateUserName(txt_Login.Text)))
        {
            MessageBox1.ShowMessage("Invalid UserName (4-30 letters or digits).", true);
            txt_Login.Focus();
        }
        else if (!(Validator.ValidatePassword(txt_Pwd.Text.Trim())))
        {
            MessageBox1.ShowMessage("Invalid Password length must in between(4-30).", true);
            txt_Pwd.Focus();
        }
        else
        {
            string pwd = ProjectCommon.Encrypt(txt_Pwd.Text.Trim(), "qwerty1235");
           // string pwd = txt_Pwd.Text.Trim();
            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@Query", "SELECT * FROM UserMaster WHERE USERID='" + txt_Login.Text + "' AND PASSWORD='" + pwd + "' And StatusId='A'"));
            //Common.Set_Parameters(new MyParameter("@Query", "SELECT * FROM UserMaster WHERE USERID='" + txt_Login.Text + "' And StatusId='A'"));
            ds = Common.Execute_Procedures_Select();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Label lblPwd = new Label();
                //lblPwd.Text = ProjectCommon.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString().Trim(), "qwerty1235");
                Session.Timeout = 60;
                Session["UserId"] = ds.Tables[0].Rows[0]["LoginId"];
                Session["loginid"] = Session["UserId"];
                Session["Password"] = ProjectCommon.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString().Trim(), "qwerty1235");
                Session["UserName"] = ds.Tables[0].Rows[0]["UserId"];
                Session["FullName"] = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                Session["UserFullName"] = Session["FullName"];
                Session["UN"] = txt_Login.Text;
                Session["Pwd"] = txt_Pwd.Text.Trim();
                Session["IsSuperUser"] = ds.Tables[0].Rows[0]["SuperUser"];
                //Session["UserType"] = ds.Tables[0].Rows[0]["SuperUser"];
                Session["UserType"] = ConfigurationManager.AppSettings["RunningLocation"].ToString();
                DataTable dt33 = UserLogin.getEmailAddress(Convert.ToInt32(Session["loginid"]));
                foreach (DataRow dr in dt33.Rows)
                {
                    Session["EmailAddress"] = dr["Email"].ToString();
                }
                Session["Mode"] = "New";

                ProcessCheckAuthority Auth = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"]), 1);
                Auth.Invoke();
                Session["Authority"] = Auth.Authority;
                CreateSession_Ship();
                //Response.Redirect("Home.aspx")
                // Response.Redirect("DefaultNew.aspx");
                //Response.Redirect("Modules/HRD/CrewRecord/HRDHome.aspx");
                CheckMenuAccessRights();
            }
            else
            {
                MessageBox1.ShowMessage("Invalid UserName / Password.", true);
            }
        }
    }

    protected void CreateSession_Ship()
    {
        string strShip = "SELECT TOP 1 * FROM Settings";
        //string strShip = "SELECT * FROM Settings where shipcode='GEX'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strShip);
        if (dt.Rows.Count > 0)
        {
            Session["CurrentShip"] = dt.Rows[0]["ShipCode"].ToString();
            Session["CurrentShipName"] = dt.Rows[0]["ShipName"].ToString();
        }
    }

    private void CheckMenuAccessRights()
    {
        energiosSecurity.User usr = new energiosSecurity.User();
        DataTable dtActionRights = new DataTable();

        string pageurl = "Modules/Purchase/ApprovalDashboard.aspx";
        int uid = Convert.ToInt32(Session["loginid"]);
        dtActionRights = usr.GetPageRightsByUserID(uid, pageurl);
        if (dtActionRights.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtActionRights.Rows[0]["CanView"]) == 1)
            {
                Response.Redirect("Modules/Purchase/ApprovalDashboard.aspx");
            }
            else
            {
                Response.Redirect("Modules/HRD/Dashboard.aspx");
            }
        }
        else
        {
            Response.Redirect("Modules/HRD/Dashboard.aspx");
        }
    }

}
