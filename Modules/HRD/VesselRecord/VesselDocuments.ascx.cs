using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using System.Web.UI.WebControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class VesselDocuments : System.Web.UI.UserControl
{
    Authority Auth;
    string Mode;
    private int _VesselId;
    public int VesselId
    {
        get { return _VesselId; }
        set { _VesselId = value; }
    }
    private void LoadDDl()
    {
        DataTable dt;
        dt = cls_VesselDocuments.SelectVesselDocumentsType();
        ddl_VDocType.DataSource = dt;
        ddl_VDocType.DataTextField = "VesselDocumentTypeName";
        ddl_VDocType.DataValueField = "VesselDocumentTypeId";
        ddl_VDocType.DataBind();
        ddl_VDocName.Items.Clear(); 
        Load_DDL2(0,0);
    }
    private void Load_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddlFlagStateName.DataValueField = "FlagStateId";
        this.ddlFlagStateName.DataTextField = "FlagStateName";
        this.ddlFlagStateName.DataSource = dt1;
        this.ddlFlagStateName.DataBind();
    }
    private void Load_DDL2(int id, int Rankid)
    {
        DataTable dt;
        dt = cls_VesselDocuments.SelectVesselDocumentsName(id, Rankid, VesselId);
        ddl_VDocName.DataSource = dt;
        ddl_VDocName.DataTextField = "VesselDocumentName";
        ddl_VDocName.DataValueField = "VesselDocumentTypeId";
        ddl_VDocName.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 39);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        
        lbl_VesselDoc.Text = ""; 
        if (!(IsPostBack))
        {
            LoadDDl();
            BindFlagNameDropDown();
            Bind_grid(VesselId);
            Load_Rank();
            pnl_Documents.Visible = false;
            btn_Save.Visible = false;
            btn_Cancel.Visible = false;
            btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
           
            btn_Print.Visible = Auth.isPrint; 
        }
        try
        {
           if (Session["VesselName"] != null)
            {
                txtVesselName.Text = Session["VesselName"].ToString();
            }
            if (Session["FormerName"] != null)
            {
                txtFormerVesselName.Text = Session["FormerName"].ToString();
            }
           if (Session["FlagStateId"] != null)
            {
                ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
            } 
        }
        catch { }
    }
    private void Show_Record(int id)
    {
        string Mess = "";
        lbl_message_documents.Visible = false;
        HiddenField hfd;

        hfd = (HiddenField)gv_VDoc.Rows[id].FindControl("HiddenDocumentTypeId");
        Mess = Mess + Alerts.Set_DDL_Value(ddl_VDocType, hfd.Value, "DocumentType");

        hfd = (HiddenField)gv_VDoc.Rows[id].FindControl("HiddenRankId");
        Mess = Mess + Alerts.Set_DDL_Value(ddl_Rank, hfd.Value, "Rank");
        Load_DDL2(Convert.ToInt32(ddl_VDocType.SelectedValue), Convert.ToInt32(ddl_Rank.SelectedValue));
        hfd = (HiddenField)gv_VDoc.Rows[id].FindControl("HiddenDocumentNameId");
        Mess = Mess + Alerts.Set_DDL_Value(ddl_VDocName, hfd.Value, "DocumentName");
        pnl_Documents.Visible = true;

        if (Mess.Length > 0)
        {
            this.lbl_message_documents.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_message_documents.Visible = true;
        }
    }
    private void Bind_grid(int VesselId)
    {
        DataTable dt;
        dt=cls_VesselDocuments.SelectVesselDocuments(VesselId);
        gv_VDoc.DataSource = dt;
        gv_VDoc.DataBind(); 
    }
    protected void gv_VDoc_SelectIndexChanged(object sender, EventArgs e)
    {
        Show_Record(gv_VDoc.SelectedIndex);  
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
    }
    protected void gv_VDoc_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Show_Record( e.NewEditIndex);
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[e.NewEditIndex].FindControl("HiddenId");
        HiddenDocId.Value = hfd.Value;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
    }
    protected void gv_VDoc_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        lbl_message_documents.Visible = false;
        HiddenField hfd;
        hfd = (HiddenField)gv_VDoc.Rows[e.RowIndex].FindControl("HiddenId");
        cls_VesselDocuments.deleteVesselDocumentsById("DeleteVesselDocuments", Convert.ToInt32(hfd.Value));
        Bind_grid(VesselId);
    }
    protected void gv_VDoc_DataBound(object sender, EventArgs  e)
    {
        try
        {
            gv_VDoc.Columns[1].Visible = false;
            gv_VDoc.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                gv_VDoc.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gv_VDoc.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
      
        }
        catch
        {
            gv_VDoc.Columns[1].Visible = false;
            gv_VDoc.Columns[2].Visible = false;
        }


    }
    protected void gv_VDoc_PreRender(object sender, EventArgs e)
    {
        if (gv_VDoc.Rows.Count <= 0)
        {
            lbl_VesselDoc.Text = "No Records Found..!";
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        pnl_Documents.Visible = false;
        btn_Save.Visible = false;
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Cancel.Visible = false;
        gv_VDoc.SelectedIndex = -1;
        lbl_message_documents.Visible = false;
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenDocId.Value = ""; 
        pnl_Documents.Visible = true;
        ddl_VDocType.SelectedIndex = 0;
        Load_DDL2(0,0);
        btn_Add.Visible = false;
        btn_Save.Visible = true;
        btn_Cancel.Visible = true;
        ddl_Rank.SelectedIndex = 0; 
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int curid,typeid,nameid,rankid;
        if (HiddenDocId.Value =="")
        {curid =-1;}
        else
        {curid = Convert.ToInt32( HiddenDocId.Value);}
        typeid= Convert.ToInt32(ddl_VDocType.SelectedValue );
        nameid = Convert.ToInt32(ddl_VDocName.SelectedValue);
        rankid = Convert.ToInt32(ddl_Rank.SelectedValue);
        cls_VesselDocuments.InsertUpdateVesselDocuments("InsertUpdateVesselDocuments", curid, VesselId, typeid, nameid,rankid, Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["loginid"].ToString()));
        Bind_grid(VesselId);
        btn_Add_Click(sender, e);
        lbl_message_documents.Visible = true;
    }
    protected void ddl_VDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_DDL2(Convert.ToInt32(ddl_VDocType.SelectedValue), Convert.ToInt32(ddl_Rank.SelectedValue));
    }

    protected void ddl_Rank_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_DDL2(Convert.ToInt32(ddl_VDocType.SelectedValue), Convert.ToInt32(ddl_Rank.SelectedValue));
    }
}
