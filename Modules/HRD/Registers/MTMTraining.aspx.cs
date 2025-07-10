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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class MTMTraining : System.Web.UI.Page
{
    Authority Auth;
    public int TID
    {
        set { ViewState["TID"] = value; }
        get { return Common.CastAsInt32(ViewState["TID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Training_Message.Text = "";
        lblTraining.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindTraining();
            BindStatusDropDown();
            Alerts.HidePanel(Trainingpanel);
            divAddTraining.Visible = false;
            Alerts.HANDLE_AUTHORITY(1, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);
        }
    }


    //----------------- EVENT 

    protected void btn_Training_add_Click(object sender, EventArgs e)
    {
        txtTrainingname.Text = "";
        txtcreatedby_Training.Text = "";
        txtcreatedon_Training.Text = "";
        txtmodifiedby_Training.Text = "";
        txtmodifiedon_Training.Text = "";
        ddstatus_Training.SelectedIndex = 0;
        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        Alerts.HANDLE_AUTHORITY(2, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);  
    }
    protected void btn_Training_save_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.sp_InsertUpdateCompanyTraining");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@TRAININGID", TID),
            new MyParameter("@TRAININGNAME", txtTrainingname.Text.Trim()),
            new MyParameter("@CREATEDBY", Convert.ToInt32(Session["loginid"].ToString())),
            new MyParameter("@MODIFIEDBY", Convert.ToInt32(Session["loginid"].ToString())),
            new MyParameter("@STATUSID", ddstatus_Training.SelectedValue)
            );
        DataSet ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            lbl_Training_Message.Text = "Training saved successfully.";
            BindTraining();
            TID = 0;
            Alerts.HidePanel(Trainingpanel);
            divAddTraining.Visible = false;
            btn_Training_Cancel.Visible = false;
            btn_Training_save.Visible = false;
            btn_Training_add.Visible = true;
        }

    }
    protected void btn_Training_Cancel_Click(object sender, EventArgs e)
    {
        Alerts.HidePanel(Trainingpanel);
        divAddTraining.Visible = false;
        btn_Training_add.Visible = true;
        btn_Training_save.Visible = false;
        btn_Training_Cancel.Visible = false;
    }
    protected void btn_Print_Training_Click(object sender, EventArgs e)
    {
        Alerts.HidePanel(Trainingpanel);
        divAddTraining.Visible = false;
        Alerts.HANDLE_AUTHORITY(2, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);  
    }

    protected void imgView_OnClick(object sender, EventArgs e)
    {
        ImageButton btn=(ImageButton )sender;
        HiddenField hfTid = (HiddenField)btn.Parent.FindControl("hfTid");
        HiddenField hfStatusID = (HiddenField)btn.Parent.FindControl("hfStatusID");

        HiddenField hfUpdatedBy = (HiddenField)btn.Parent.FindControl("hfUpdatedBy");
        HiddenField hfUpdatedOn = (HiddenField)btn.Parent.FindControl("hfUpdatedOn");
        HiddenField hfModifyBy = (HiddenField)btn.Parent.FindControl("hfModifyBy");
        HiddenField hfModifyOn = (HiddenField)btn.Parent.FindControl("hfModifyOn");
        
        Label lblTName = (Label)btn.Parent.FindControl("lblTName");


        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        Alerts.HANDLE_AUTHORITY(2, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);  

        txtTrainingname.Text = lblTName.Text;

        txtcreatedby_Training.Text = hfUpdatedBy.Value;
        txtcreatedon_Training.Text = hfUpdatedOn.Value;
        txtmodifiedby_Training.Text = hfModifyBy.Value;
        txtmodifiedon_Training.Text = hfModifyOn.Value;

        ddstatus_Training.SelectedValue = hfStatusID.Value;
        btn_Training_save.Visible = false;
        
    }
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfTid = (HiddenField)btn.Parent.FindControl("hfTid");
        TID = Common.CastAsInt32(hfTid.Value);
        HiddenField hfStatusID = (HiddenField)btn.Parent.FindControl("hfStatusID");

        HiddenField hfUpdatedBy = (HiddenField)btn.Parent.FindControl("hfUpdatedBy");
        HiddenField hfUpdatedOn = (HiddenField)btn.Parent.FindControl("hfUpdatedOn");
        HiddenField hfModifyBy = (HiddenField)btn.Parent.FindControl("hfModifyBy");
        HiddenField hfModifyOn = (HiddenField)btn.Parent.FindControl("hfModifyOn");

        Label lblTName = (Label)btn.Parent.FindControl("lblTName");


        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        Alerts.HANDLE_AUTHORITY(2, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);

        txtTrainingname.Text = lblTName.Text;

        txtcreatedby_Training.Text = hfUpdatedBy.Value;
        txtcreatedon_Training.Text = hfUpdatedOn.Value;
        txtmodifiedby_Training.Text = hfModifyBy.Value;
        txtmodifiedon_Training.Text = hfModifyOn.Value;

        ddstatus_Training.SelectedValue = hfStatusID.Value;
        btn_Training_save.Visible = true;
    }
    protected void imgDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfTid = (HiddenField)btn.Parent.FindControl("hfTid");

        string sql = "delete from DBO.MTMTraining where TrainingId=" + Common.CastAsInt32(hfTid.Value) + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        BindTraining();
        lbl_Training_Message.Text = "Record deleted succesfully.";
    }
    
    //----------------- FUNCTION 
    private void BindStatusDropDown()
    {
        DataTable dt2 = Training.selectDataStatus();
        this.ddstatus_Training.DataValueField = "StatusId";
        this.ddstatus_Training.DataTextField = "StatusName";
        this.ddstatus_Training.DataSource = dt2;
        this.ddstatus_Training.DataBind();
    }
    private void BindTraining()
    {
        string sql = " SELECT TrainingId "+
                       " ,TrainingName ,STATUSID " +
                       " ,(SELECT FirstName+' '+LastName from DBO.UserLogin where LoginId=MT.CreatedBy) as CreatedBy "+
                       " ,replace(Convert(varchar,CreatedOn,106),' ','-')CreatedOn "+
                       " ,(SELECT FirstName+' '+LastName from DBO.UserLogin where LoginId=MT.ModifiedBy) as ModifiedBy " +
                       " , replace(Convert(varchar,ModifiedOn,106),' ','-')ModifiedOn  "+
                       " ,(Case When STATUSID='A' then 'Active' else 'In-Active' end)Status  " +
                   " FROM DBO.CompanyTraining MT";
        DataTable dt  = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            rptTraining.DataSource = dt;
            rptTraining.DataBind();
        }
        
    }
    
}
