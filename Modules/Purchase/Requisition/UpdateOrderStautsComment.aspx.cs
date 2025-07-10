using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UpdateOrderStautsComment : System.Web.UI.Page
{
    #region Properties ***************************************************
    public int BidId
    {
        set { ViewState["BidId"] = value; }
        get { return int.Parse("0" + ViewState["BidId"]); }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (Page.Request.QueryString["BidID"] != null)
        {
            BidId = Common.CastAsInt32(Page.Request.QueryString["BidID"]);
        }
        if (!Page.IsPostBack)
        {
            GetComments();
        }
        
    }
    protected void imgSave_OnClick(object sender, EventArgs e)
    {
        //string sql = "update [dbo].tblBudgetVActualComments set comment='"+txtOrderStatus.Text+"' where CommBidID="+BidId+"";

        Common.Set_Procedures("sp_NewPR_InsertUpdateOrderStatusCommets");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@BidID", BidId),
            new MyParameter("@OrderStatusComment", txtOrderStatus.Text.Trim()),
            new MyParameter("@UserName", Session["UserFullName"].ToString())
            );
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsPrType);
        if (res == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Comments udpated successfully.');window.opener.ReloadPage();window.close();", true);
            //lblmsg.Text = "Commemts udpated successfully.";
        }
        else
        {
            lblmsg.Text="Commemts could not be udpated.";
        }
    }
    public void GetComments()
    {
        string sql = "select * from Add_tblsmdpomasterBid where BidID=" + BidId + "";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();
        if (dsPrType != null)
        {
            if (dsPrType.Tables[0].Rows.Count != 0)
            {
                txtOrderStatus.Text = dsPrType.Tables[0].Rows[0]["OrderStatusComment"].ToString();
                lblupdatedby.Text =dsPrType.Tables[0].Rows[0]["Commentsby"].ToString();
                lblupdatedon.Text = Common.ToDateString(dsPrType.Tables[0].Rows[0]["CommentsOn"]);
            }
        }
    }
}
