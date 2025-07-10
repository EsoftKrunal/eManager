using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class MWUC_EntryMaste : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    public int TABLEID
    {
        get { return Common.CastAsInt32(ViewState["TABLEID"]); }
        set { ViewState["TABLEID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {            
            CurrentVessel = Session["CurrentShip"].ToString();
            if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString() != "")
            {
                TABLEID = Common.CastAsInt32(Request.QueryString["Id"]);
            }
            ShowMasterDetails();                
            BindGrid();           
        }
    }
    public void ShowMasterDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FORDATE,MASTERCREWNO,MASTERNAME,CREATEDON,OFFICERECDON,OFFICECLOSEDON,MGMTVISIT FROM DBO.MWUC_MASTER WHERE TABLEID=" + TABLEID + " And VESSELCODE='" + CurrentVessel + "'");
        if (dt.Rows.Count > 0)
        {
            txtRDate.Text = Common.ToDateString(dt.Rows[0]["FORDATE"]);
            txtCrewNo.Text = dt.Rows[0]["MASTERCREWNO"].ToString();
            txtMasterName.Text = dt.Rows[0]["MASTERNAME"].ToString();
            ddlVisit.SelectedValue = dt.Rows[0]["MGMTVISIT"].ToString();
            btnSave.Visible = Convert.IsDBNull(dt.Rows[0]["OFFICECLOSEDON"]);
        }
    }
    public void BindGrid()
    {
        string SQL = "SELECT CM.[CATID],CM.[CATNAME], " +
                     "(SELECT SHIPCOMMENT FROM MWUC_DETAILS S WHERE S.VESSELCODE = CM.VESSELCODE AND S.CATID = CM.CATID AND S.TABLEID=" + TABLEID + " )  AS [SHIPCOMMENT], " +
                     "(SELECT OFFICECOMMENT FROM MWUC_DETAILS O WHERE O.VESSELCODE = CM.VESSELCODE AND O.CATID = CM.CATID AND O.TABLEID=" + TABLEID + " )  AS [OFFICECOMMENT], " +
                     "(SELECT ('[ ' + OFFICECOMMENTPOSITION + ' ] '  + OFFICECOMMENTBY + '/ ' + REPLACE(CONVERT(Varchar(11), OFFICECOMMENTON, 106), ' ', '-')) FROM MWUC_DETAILS O WHERE O.VESSELCODE = CM.VESSELCODE AND O.CATID = CM.CATID AND O.TABLEID=" + TABLEID + " )  AS OFFICECOMMENTBYON " +
                     //"[SHIPCOMMENTON],[OFFICECOMMENTON] " +
                     "FROM MWUC_CATMASTER CM  " +                      
                     "WHERE CM.VESSELCODE = '" + CurrentVessel + "' " ;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptMWUC.DataSource = dt;
        rptMWUC.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string ForDate = txtRDate.Text.Trim();
        foreach (RepeaterItem item in rptMWUC.Items)
        {
            
            Label lblCatName = (Label)item.FindControl("lblCatName");
            TextBox txt = (TextBox)item.FindControl("txtShipComments");
            if (txt.Text.Trim() == "")
            {
                ShowMessage("Please enter ship comments for "+ lblCatName.Text, true);
                txt.Focus();
                return;
            }

            
        }

        try
        {
            Common.Set_Procedures("[dbo].[MWUC_InsertUpdateMaster]");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@VESSELCODE", CurrentVessel),
               new MyParameter("@TABLEID", TABLEID),
               new MyParameter("@FORDATE", txtRDate.Text.Trim()),
               new MyParameter("@MASTERCREWNO", txtCrewNo.Text.Trim()),
               new MyParameter("@MASTERNAME", txtMasterName.Text.Trim()),
               new MyParameter("@MGMTVISIT", ddlVisit.SelectedValue)               
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                TABLEID = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);

                foreach (RepeaterItem item in rptMWUC.Items)
                {
                    Label lblCatName = (Label)item.FindControl("lblCatName");
                    TextBox txtShipComments = (TextBox)item.FindControl("txtShipComments");
                    //TextBox txtOfficeComments = (TextBox)item.FindControl("txtOfficeComments");
                    
                    int CatId = Common.CastAsInt32(lblCatName.Attributes["CatId"]);

                    //if (txtShipComments.Text.Trim() != "")
                    //{ 
                        Common.Set_Procedures("[dbo].[MWUC_InsertUpdateDetails]");
                        Common.Set_ParameterLength(4);
                        Common.Set_Parameters(
                           new MyParameter("@VESSELCODE", CurrentVessel),
                           new MyParameter("@TABLEID", TABLEID),
                           new MyParameter("@CATID", CatId),
                           new MyParameter("@SHIPCOMMENT", txtShipComments.Text.Trim())
                           );
                        DataSet ds1 = new DataSet();
                        ds1.Clear();
                        Boolean res1;
                        res1 = Common.Execute_Procedures_IUD(ds1);
                    //}
                }

                ShowMessage("Saved successfully.", false);

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "RP", "RefreshParent();", true);
            }
            else
            {
                ShowMessage("Unable to save. Error : " + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Unable to save. Error : " + ex.Message.ToString(), true);
        }
    }
    public void ShowMessage(string msg, bool error)
    {
        lblMessage.Text = msg;
        lblMessage.ForeColor = (error ? System.Drawing.Color.Red : System.Drawing.Color.Green);
    }
    
  }
