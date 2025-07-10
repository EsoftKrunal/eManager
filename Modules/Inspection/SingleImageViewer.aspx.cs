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

public partial class Transactions_SingleImageViewer : System.Web.UI.Page
{
    Authority Auth;
    int intInspDueId = 0;// intSrNum = 0;
    string strImgUrl = "", strPicCaption = "", strSrNum = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            try
            {
                intInspDueId = int.Parse(Page.Request.QueryString["InspId"].ToString());
                //intSrNum = int.Parse(Page.Request.QueryString["SrNumb"].ToString());
                strSrNum = Page.Request.QueryString["SrNumb"].ToString();
                strImgUrl = Page.Request.QueryString["ImgUrl"].ToString();
            }
            catch { }
            ImgDel.Visible = Auth.isDelete;
            imgImage.Src = strImgUrl;
            if (Session["Insp_Id"] != null)
            {
                int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                if (useronbehalfauth <= 0)
                {
                    ImgDel.Visible = false;
                }
                else
                {
                    ImgDel.Visible = true;
                }
            }
        }
        catch { }
    }
    protected void ImgDel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //strPicCaption = System.IO.Path.GetFileNameWithoutExtension(strImgUrl);
            strPicCaption = System.IO.Path.GetFileName(strImgUrl);
            DataTable dt1 = Safety_Inspection.InsertSafetyInspChildDetails(0, intInspDueId, strSrNum, "", strPicCaption, 0, "Delete");
            Page.RegisterClientScriptBlock("closewindow", "<script language='JavaScript'>window.close(); window.opener.location=window.opener.location;</script>");
        }
        catch { }
    }
}
