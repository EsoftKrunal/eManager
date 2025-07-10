using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ForeCastComments : System.Web.UI.Page
{
    AuthenticationManager authRecInv;
    decimal ActComm=0, Projected=0, Budget=0, ForeCast=0;
    string Company="";
    string Ship = "";
    string MidCat = "";
    public string Fyear = "";
    public string Byear = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1091, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('You have not permissions to access this page.');window.close();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Your session is expired.');window.close();", true);    
        }

        #endregion ----------------------------------------
        imgSave.Visible= authRecInv.IsUpdate; 

        Company = Request.QueryString["Comp"].Trim();
        Ship = Request.QueryString["Ship"].Trim();
        MidCat = Request.QueryString["MidCat"].Trim();
        Fyear = Request.QueryString["Fyear"].Trim();
        Byear = (Common.CastAsInt32(Fyear) - 1).ToString();
        if(!IsPostBack)
        {
            //-----------------------
            DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE] '" + Company + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + ",'" + Ship + "'");
            if (MidCat != "0")
            {
                DataView dv = dtActCom_Proj.DefaultView;
                dv.RowFilter = "MidCatId=" + MidCat;
                DataTable dt1 = dv.ToTable();

                if (dt1.Rows.Count > 0)
                {
                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                }
                DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + Ship + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + MidCat);
                if (dtShip.Rows.Count > 0)
                {
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + Ship + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + MidCat);
                if (dtShipLast.Rows.Count > 0)
                {
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
            }
            else
            {
                ActComm = Common.CastAsDecimal(dtActCom_Proj.Compute("Sum(ACT_CONS)", ""));
                Projected = Common.CastAsDecimal(dtActCom_Proj.Compute("Sum(PROJECTED)", "")); 

                DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select sum(budget) from dbo.v_New_CurrYearBudgetHome where shipid='" + Ship + "' AND year=" + (DateTime.Today.Year - 1).ToString());
                if (dtShip.Rows.Count > 0)
                {
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select sum(nextyearforecastamount) from dbo.v_New_CurrYearBudgetHome where shipid='" + Ship + "' AND year=" + (DateTime.Today.Year - 1).ToString());
                if (dtShipLast.Rows.Count > 0)
                {
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
            }
            //-----------------
            lblHeadName.Text = Request.QueryString["HeadName"].Replace("_","&");
            string sql = "SELECT COMMENTS FROM tblBudgetForecastComments WHERE COCODE='" + Company + "' AND SHIPID='" + Ship + "' AND MIDCATID=" + MidCat + " AND FORECASTYEAR=" + Fyear;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
            {
                txtComments.Text = dt.Rows[0]["COMMENTS"].ToString().Replace("`", "'");     
            }
            //-----------------
            lblActComm.Text=ActComm.ToString();   
            lblProj.Text =Projected.ToString();
            lblBudg.Text=Budget.ToString();    
            lblFore.Text=ForeCast.ToString();    
            //-----------------
            Decimal Var1, Var2, Var3;
                
                if (Budget > 0)
                    Var1 = ((Projected - Budget) / Budget) * 100;
                else
                    Var1 = 0;

                if (Projected > 0)
                    Var2 = ((ForeCast - Projected) / Projected) * 100;
                else
                    Var2 = 0;

                if (Budget > 0)
                    Var3 = ((ForeCast - Budget) / Budget) * 100;
                else
                    Var3 = 0;

            lblVar1.Text=Math.Round(Var1,0).ToString();    
            lblVar2.Text=Math.Round(Var2,0).ToString();
            lblVar3.Text = Math.Round(Var3, 0).ToString();    
         }
    }
    protected void imgSave_OnClick(object sender, ImageClickEventArgs e)
    {
        string sql = "SELECT * FROM tblBudgetForecastComments WHERE COCODE='" + Company + "' AND SHIPID='" + Ship + "' AND MIDCATID=" + MidCat  + " AND FORECASTYEAR=" + Fyear;
        DataTable dt=Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
            sql = "UPDATE tblBudgetForecastComments SET COMMENTS='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE COCODE='" + Company + "' AND SHIPID='" + Ship + "' AND MIDCATID=" + MidCat + " AND FORECASTYEAR=" + Fyear ;
        else
            sql = "INSERT INTO tblBudgetForecastComments(COCODE,SHIPID,FORECASTYEAR,MIDCATID,COMMENTS) VALUES('" + Company + "','" + Ship + "'," + Fyear + "," + MidCat + ",'" + txtComments.Text.Trim().Replace("'", "`") + "')";
        try
        {
            Common.Execute_Procedures_Select_ByQuery(sql);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "p", "alert('Comment saved successfully.');", true);
        }
        catch { } 
    }
}
