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

public partial class CreateRFQ : System.Web.UI.Page
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        
        AuthenticationManager authRFQList = new AuthenticationManager(0, 0, ObjectType.Page);
        try
        {
            authRFQList = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRFQList.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!IsPostBack)
        {
           StoreRFQ.Visible = false;
           SpareRFQ.Visible = false;
           LGRFQ.Visible = false;

            int PRId = int.Parse("0" + Request.QueryString["PRID"]);
          //  viewpage = bool.Parse(Request.QueryString["ViewPage"].ToString());
            DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("SELECT PRTYPE FROM VW_tblSMDPOMaster where poid=" + PRId);
           if (dtPR.Rows.Count > 0)
           {
                 
               if (int.Parse(dtPR.Rows[0][0].ToString()) == 1)
               {
                   StoreRFQ.PRId = PRId;
                   StoreRFQ.BindRepeater();
                   StoreRFQ.Visible = true;  
               }
               if (int.Parse (dtPR.Rows[0][0].ToString()) == 2)
               {
                   SpareRFQ.PRId = PRId;
                   SpareRFQ.BindRepeater();
                   SpareRFQ.Visible = true;
               }
               if (int.Parse(dtPR.Rows[0][0].ToString()) == 3)
               {
                   LGRFQ.PRId = PRId;
                   LGRFQ.BindRepeater();
                   LGRFQ.Visible = true;
               }
           }
        }
    }
    protected void imgCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/AddRequisition.aspx?PRID="+ Request.QueryString["PRID"].ToString()+ "&ViewPage=true"); 
    }
}
