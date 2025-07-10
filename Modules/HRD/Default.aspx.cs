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

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int LoginId;
        LoginId = 0;
        try
        {
            LoginId = Convert.ToInt32(Request.Form["hfd_loginid"]);
        }
        catch { }
        
        Session["NoMenu"] = "N";

        if (Request.Form["hfd_loginid"] != null)
        {
            if (LoginId > 0)
            {
                DataTable dt3 = UserLogin.selectDataUserLoginDetailsByUserLoginId(LoginId);
                if (dt3.Rows.Count > 0)
                {
                    DoSubmit(dt3.Rows[0]["UserId"].ToString(), dt3.Rows[0]["Password"].ToString());
                }
            }
        }
        Message.Text = "";
        if (("" + Request.QueryString["Exp"]) == "true" && !(IsPostBack))
        {
            Message.Text = "Your session has been expired. Please Login !";
        }   
        this.LoginId.Focus();
        string username="" + Request.QueryString["u"]; 
        string pwd="" + Request.QueryString["p"];
        DoSubmit(username, pwd);
    }
    protected void DoSubmit( string Uname,String Pword)
    {
        ProcessCheckLogin Obj = new ProcessCheckLogin();
        Obj.UserId = Uname;
        Obj.Password = Pword;
        Obj.Invoke();
        if (Obj.LoginId <= 0)
        {
            Message.Text = "User Id/Password is Invalid";
        }
        else
        {
            Session["loginid"] = Obj.LoginId.ToString();
            ProcessCheckAuthority Auth = new ProcessCheckAuthority(Obj.LoginId, 1);
            Auth.Invoke();

            Session["UserName"] = UserLogin.getUserName(Obj.LoginId);
            Session["UN"] = Uname;
            Session["Pwd"] = Pword; 
            DataTable dt33 = UserLogin.getEmailAddress(Obj.LoginId);
            foreach (DataRow dr in dt33.Rows)
            {
                Session["EmailAddress"] = dr["Email"].ToString();
            }
            Session["Mode"] = "New";
            Session["Authority"] = Auth.Authority;

            // Temp Setting 
		    Session["ProfileId"]=null;
            if (("" + Request.QueryString["emtm"]) != "")
            {
                int Lid = 0;
                Lid=Common.CastAsInt32(Session["loginid"].ToString()); 
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select EmpId from DBO.Hr_PersonalDetails where UserId=" + Lid.ToString());
                if (dt1.Rows.Count > 0)
                {
                    DataRow dr = dt1.Rows[0];
                    Session["ProfileId"] = dr[0].ToString();
                }
                else
                {
                    Session["ProfileId"] = null;
                    Message.Text = "User has no linking in EMTM.";  
                    //return; 
                }
                //---------------------------
                Response.Redirect("EMTM/Emtm_Home.aspx");
            }
            if (("" + Request.QueryString["emtm1"]) != "")
            {
                int Lid = 0;
                Lid = Common.CastAsInt32(Session["loginid"].ToString());
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select EmpId from DBO.Hr_PersonalDetails where UserId=" + Lid.ToString());
                if (dt1.Rows.Count > 0)
                {
                    DataRow dr = dt1.Rows[0];
                    Session["ProfileId"] = dr[0].ToString();
                }
                else
                {
                    Session["ProfileId"] = null;
                    Message.Text = "User has no linking in EMTM.";
                    //return; 
                }
                //---------------------------
                Response.Redirect("EMTM/Emtm_Home1.aspx");
            }
            else
            {
                //if(Obj.LoginId==1)
                    Response.Redirect("CrewRecord/HRDHome.aspx");
                //else
                //    Response.Redirect("CrewRecord/CrewSearch.aspx");
            }
         
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DoSubmit(LoginId.Text.Trim(), Password.Text.Trim());
        Session["NoMenu"] = "N";

        //----------------------------------------------------------------------------

        //ProcessCheckLogin Obj = new ProcessCheckLogin();
        //Obj.UserId = LoginId.Text.Trim();
        //Obj.Password = Password.Text.Trim();   
        //Obj.Invoke();
        //if (Obj.LoginId <= 0)
        //{
        //    Message.Text = "User Id/Password is Invalid";
        //}
        //else
        //{
        //    Session["loginid"] = Obj.LoginId.ToString();
        //    ProcessCheckAuthority Auth = new ProcessCheckAuthority(Obj.LoginId, 1);
        //    Auth.Invoke();
        //    Session["UN"] = Obj.UserId;
        //    Session["Pwd"] = Obj.Password; 
        //    Session["UserName"] = UserLogin.getUserName(Obj.LoginId);
        //    DataTable dt33 = UserLogin.getEmailAddress(Obj.LoginId);
        //    foreach(DataRow dr in dt33.Rows)
        //    {
        //        Session["EmailAddress"] = dr["Email"].ToString();
        //    }
        //    Session["Mode"] = "New";
        //    Session["Authority"] = Auth.Authority;
            
        //    // Temp Setting 
        //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select EmpId from DBO.Hr_PersonalDetails where UserId=" + Session["loginid"]);
        //    if (dt1 != null)
        //    {
        //        if (dt1.Rows.Count > 0)
        //        {
        //            DataRow dr = dt1.Rows[0];
        //            Session["ProfileId"] = dr[0].ToString();
        //        }
        //        else
        //        {
        //        }
        //    }
          
        //    Response.Redirect("SiteFrame.aspx");
        //}

        //----------------------------------------------------------------------------
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.LoginId.Text = "";
        this.Password.Text = "";
    }
}
