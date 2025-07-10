using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_PopupPositionMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                string PositionIdName,PageMode,PageName,OfficeName;
                PositionIdName = Request.QueryString.GetKey(0);
                int PositionId = Common.CastAsInt32(Request.QueryString[PositionIdName].Trim());
                Session.Add("PopUpPositionId", PositionId);

                PageMode = Request.QueryString.GetKey(1);
                PageName = Request.QueryString[PageMode].Trim();
                Session["PopUpPageView"] = PageName.ToString().Trim();

                ddlVesselPosition.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VesselPositions ORDER BY PositionName");
                ddlVesselPosition.DataTextField="PositionName";
                ddlVesselPosition.DataValueField="VPId";
                ddlVesselPosition.DataBind();
                ddlVesselPosition.Items.Insert(0, new ListItem(" < Select > ","0"));

                if (Session["PopUpPageView"].ToString() == "Add")
                {
                    OfficeName = Request.QueryString.GetKey(2);
                    int OfficeId = Common.CastAsInt32(Request.QueryString[OfficeName].Trim());
                    hdnOfficeId.Value = OfficeId.ToString();
                }

                ShowRecord(PositionId);
                setButtons(Session["PopUpPageView"].ToString());
            }
        }
    }

    # region --- User Defined Functions ---
    public void ShowRecord(int Id)
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from Position where PositionId =" + Id);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                txtPosCode.Text = dr["PositionCode"].ToString();
                txtPosName.Text = dr["PositionName"].ToString();
                hdnOfficeId.Value = dr["OfficeId"].ToString().Trim();
                ddlVesselPosition.SelectedValue = Common.CastAsInt32(dr["VesselPositions"]).ToString();
                if (Convert.IsDBNull(dr["IsManager"]))
                {
                    radmangaer.SelectedIndex = 1; 
                }
                else
                {
                    if (Convert.ToBoolean(dr["IsManager"].ToString()))
                    {
                        radmangaer.SelectedIndex = 0;
                    }
                    else
                    {
                        radmangaer.SelectedIndex = 1;
                    }
                }


                if (Convert.IsDBNull(dr["IsInspector"]))
                {
                    radInspector.SelectedIndex = 1;
                }
                else
                {
                    if (Convert.ToBoolean(dr["IsInspector"].ToString()))
                    {
                        radInspector.SelectedIndex = 0;
                    }
                    else
                    {
                        radInspector.SelectedIndex = 1;
                    }
                }
            }
    }
    private void ClearControls()
    {
        txtPosCode.Text = "";
        txtPosName.Text = "";
    }
    protected void setButtons(string Action)
    {
        switch (Action)
        {
            case "View":
                //tblview.Visible = true;
                
                btnsave.Visible = false;
                btnCancel.Visible = true;
                break;
            case "Add":
                //tblview.Visible = true;
                
                btnsave.Visible = true;
                btnCancel.Visible = true;
                break;
            case "Edit":
                //tblview.Visible = true;
                
                btnsave.Visible = true;
                btnCancel.Visible = true;
                break;
            default:
                //tblview.Visible = false;
                
                btnsave.Visible = false;
                btnCancel.Visible = false;
                break;
        }
    }
    #endregion

    # region --- Controls Events ---
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtPosName.Text.Trim()=="" )
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please enter Position Name.');", true);
            return;
        }

        Common.Set_Procedures("HR_InsertUpdatePositionMaster");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(new MyParameter("@PositionId", Session["PopUpPositionId"]),
            new MyParameter("@OfficeId", hdnOfficeId.Value.Trim()),
            new MyParameter("@PositionCode", txtPosCode.Text.Trim()),
            new MyParameter("@PositionName", txtPosName.Text.Trim()),
            new MyParameter("@IsManager", radmangaer.SelectedValue),
            new MyParameter("@IsInspector", radInspector.SelectedValue),
            new MyParameter("@VesselPositions", ddlVesselPosition.SelectedValue)
            );

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Position Already Exist.');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Position Already Exist');", true);
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
    }
    #endregion
}
