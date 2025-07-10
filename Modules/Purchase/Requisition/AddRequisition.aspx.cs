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
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Page Name            : AddSpareRequisition.aspx
/// Purpose              : For Create Requisitions
/// Author               : Manita Singhal
/// Developed on         : 10 june 2010
/// Last Modified by/on  : Arijit/10 sept 2010
/// Modifier Comments    : 
/// </summary>

public partial class AddSpareRequisition : System.Web.UI.Page
{

    #region Properties
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public int PRId
    {
        get { return Convert.ToInt32(ViewState["_PRId"]); }
        set { ViewState["_PRId"] = value; }
    }
    public int PRType
    {
        get { return Convert.ToInt32(ViewState["_PRType"]); }
        set { ViewState["_PRType"] = value; }
    }
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        // AuthenticationManager authPR = new AuthenticationManager();
        //  AuthenticationManager authRFQList;
        AuthenticationManager authPR = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        AuthenticationManager authRFQList = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        try
        {
            //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 208);

            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            BindVessel();
            if (Page.Request.QueryString["PRID"] != null)
            {
                PRId = Convert.ToInt32(Page.Request.QueryString["PRID"]);
                PRType = GetPRType(PRId);
                rdPRTYpe.Visible = false;
                ImgCreateRFQ.Visible = true && authRFQList.IsView;
                imgPrint.Attributes.Add("onclick", "window.open('Print.aspx?PRID=" + PRId + "&PRType=" + PRType + "'); return false;");
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("select count(*) from vw_tblsmdpomasterbid where poid=" + PRId);
                if (Common.CastAsInt32(dt.Rows[0][0]) <= 0)
                {
                    imgEdit.Visible = true && authPR.IsUpdate;
                    btnStatusNote.Visible = true && authPR.IsUpdate;
                }
                DataTable dtPOSStatus = Common.Execute_Procedures_Select_ByQuery("Select StatusID  from tblSMDPOMaster with(nolock) where POid = " + PRId);
                if (Common.CastAsInt32(dt.Rows[0][0]) >= 0 && Common.CastAsInt32(dtPOSStatus.Rows[0][0]) <= 15) 
                {
                    imgEdit.Visible = true && authPR.IsUpdate;
                }

                    if (PRType == 1)
                {
                    ucStore.PRId = PRId;
                    ucStore.Visible = true;
                    rdPRTYpe.SelectedIndex = 0;
                }
                else if (PRType == 2)
                {
                    ucSpare.PRId = PRId;
                    ucSpare.Visible = true;
                    rdPRTYpe.SelectedIndex = 1;
                }
                else if (PRType == 3)
                {
                    UcLND.PRId = PRId;
                    UcLND.Visible = true;
                    rdPRTYpe.SelectedIndex = 2;
                }
                else
                {
                    lblMsg.Text = "No PR Type found .";
                }
            }
            else
            {
                ucStore.Visible = true;
                rdPRTYpe.Visible = true;
                imgEdit.Visible = false;
                btnStatusNote.Visible = false;
                ImgCreateRFQ.Visible = false;
                imgPrint.Visible = false;
                rdPRTYpe.SelectedIndex = 0;
            }

        }
    }
    #endregion
    protected void btnOfficeRemSave_Click(object sender, EventArgs e)
    {
        int PoId = Common.CastAsInt32(ViewState["spoid"]);
        Common.Execute_Procedures_Select_ByQuery("update add_tblsmdpomaster set officecomments='" + tctOfficeRemarks.Text.Trim().Replace("'", "`") + "',commentsby='" + Session["FullName"].ToString() + "',commentson=getdate() where poid=" + PoId.ToString());
        dvOfficeRemarks.Visible = false;
        dvOfficeRemarks.Visible = false;
    }
    protected void btnOfficeRemCancel_Click(object sender, EventArgs e)
    {
        ViewState["spoid"] = null;
        dvOfficeRemarks.Visible = false;
    }
    #region My Function  ************************************************************************************************************

    public void BindVessel()
    {
        string sql = "SELECT vw.ShipID, vw.ShipID+' - '+vw.ShipName As ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY vw.ShipID";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlCopyVessel.DataSource = dsPrType;

        ddlCopyVessel.DataTextField = "ShipName";
        ddlCopyVessel.DataValueField = "ShipID";
        ddlCopyVessel.DataBind();

        ddlCopyVessel.Items.Insert(0, new ListItem("< Select Vessel >", "0"));
        ddlCopyVessel.SelectedIndex = 0;
    }
    public void tmp(object sed, EventArgs e)
    {

    }

    public int GetPRType(int PrID)
    {
        string sql = " select   isnull(prtype ,0),shipid +'-' + convert(varchar, prnum) as officeprnum from VW_tblSmdpoMaster where poid=" + PrID + "";
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
                lblORNo.Text = dsPrType.Tables[0].Rows[0]["officeprnum"].ToString();
                return Convert.ToInt32(dsPrType.Tables[0].Rows[0][0]);
            }
        }
        return 0;
    }
    #endregion

    #region Event  ************************************************************************************************************

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
    }
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        if (ucSpare.Visible == true)
            ucSpare.ShowSaveButton();
        else if (ucStore.Visible == true)
            ucStore.ShowSaveButton();
        else if (UcLND.Visible == true)
            UcLND.ShowSaveButton();

    }
    protected void ImgCreateRFQ_OnClick(object sender, EventArgs e)
    {
        if (PRId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select isnull(dept,'') as dept,accountid,MidCatId from dbo.vw_tblsmdpomaster where poid=" + PRId);
            if (dt.Rows[0]["MidCatId"].ToString().Trim() != "" && Common.CastAsInt32(dt.Rows[0]["accountid"]) > 0)
            {
                Response.Redirect("CreateRFQ.aspx?PRID=" + PRId.ToString());
            }
            else
            {
                lblMsg.Text = "Please update department and accountid for this Requisition.";

            }
        }
    }
    protected void rdPRTYpe_onselectedindexchanged(object sender, EventArgs e)
    {
        if (rdPRTYpe.SelectedIndex == 0)
        {
            ucStore.Visible = true;
            ucSpare.Visible = false;
            UcLND.Visible = false;
        }
        else if (rdPRTYpe.SelectedIndex == 1)
        {
            ucSpare.Visible = true;
            ucStore.Visible = false;
            UcLND.Visible = false;
        }
        else if (rdPRTYpe.SelectedIndex == 2)
        {
            UcLND.Visible = true;
            ucSpare.Visible = false;
            ucStore.Visible = false;
        }
    }
    protected void imgCopyVessel_OnClick(object sender, EventArgs e)
    {
        if (ddlCopyVessel.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select Vessel.";
            lblMsg.Style.Add("float", "right");
            return;
        }
        DataSet dsPrType = null;

        Common.Set_Procedures("sp_New_CopyReqn");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@PrId", PRId), new MyParameter("@ShipID", ddlCopyVessel.SelectedValue));
        dsPrType = Common.Execute_Procedures_Select();
        if (dsPrType != null)
        {
            lblMsg.Text = "Requisition successfully coppied.";
            Response.Redirect("AddRequisition.aspx?PRID=" + dsPrType.Tables[0].Rows[0][0].ToString() + "&ViewPage=true");
        }
        else
        {
            lblMsg.Text = "Requisition could not be coppied.";
            lblMsg.Style.Add("float", "right");
        }
    }
    #endregion

    protected void btnStatusNote_OnClick(object sender, EventArgs e)
    {
        dvOfficeRemarks.Visible = true;
        int PoId = Common.CastAsInt32(((Button)sender).CssClass);
        ViewState["spoid"] = PoId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from add_tblsmdpomaster where poid=" + PoId.ToString());
        if (dt.Rows.Count > 0)
        {
            tctOfficeRemarks.Text = Convert.ToString(dt.Rows[0]["officecomments"]);
            lblupdatedby.Text = dt.Rows[0]["Commentsby"].ToString();
            lblupdatedon.Text = Common.ToDateString(dt.Rows[0]["CommentsOn"]);
        }
        else
        {
            tctOfficeRemarks.Text = "";
            lblupdatedby.Text = "";
            tctOfficeRemarks.Text = "";
        }
    }
}
    

