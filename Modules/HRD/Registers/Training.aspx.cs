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

public partial class Registers_Training : System.Web.UI.Page
{
    public Authority Auth;
    AuthenticationManager Auth1;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Training_Message.Text = "";
        lblTraining.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 65);
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");

        }
        //******************* 
        Auth = new Authority();
        Auth1 = new AuthenticationManager(65, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        Auth.isAdd = Auth1.IsAdd;
        Auth.isDelete = Auth1.IsDelete;
        Auth.isEdit = Auth1.IsUpdate;
        Auth.isPrint = Auth1.IsPrint;
        Auth.isVerify = Auth1.IsVerify;
        Auth.isVerify2 = Auth1.IsVerify2;

        if (!Page.IsPostBack)
        {
            BindTrainingTypeDropDown();
            BindStatusDropDown();
            //BindSireChap();
            BindGridTraining();
            
            Alerts.HidePanel(Trainingpanel);
            divAddTraining.Visible = false;
            Alerts.HANDLE_AUTHORITY(1, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);
            
        }
    }
    // -------------- Function 
    private void BindTrainingTypeDropDown()
    {
        DataTable dt114 = Training.selectDataTrainingTypeId();

        this.dd_trainingType.DataValueField = "TrainingTypeId";
        this.dd_trainingType.DataTextField = "TrainingTypeName";
        this.dd_trainingType.DataSource = dt114;
        this.dd_trainingType.DataBind();

        ddlTrainingTypeSrch.DataValueField = "TrainingTypeId";
        ddlTrainingTypeSrch.DataTextField = "TrainingTypeName";
        ddlTrainingTypeSrch.DataSource = dt114;
        ddlTrainingTypeSrch.DataBind();
        ddlTrainingTypeSrch.Items.RemoveAt(0);
        ddlTrainingTypeSrch.Items.Insert(0, new ListItem(" All ", ""));


        ddlTrainingTypeAddTrng.DataValueField = "TrainingTypeId";
        ddlTrainingTypeAddTrng.DataTextField = "TrainingTypeName";
        ddlTrainingTypeAddTrng.DataSource = dt114;
        ddlTrainingTypeAddTrng.DataBind();
        ddlTrainingTypeAddTrng.Items.RemoveAt(0);
        ddlTrainingTypeAddTrng.Items.Insert(0, new ListItem(" Select ", ""));
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = Training.selectDataStatus();
        this.ddstatus_Training.DataValueField = "StatusId";
        this.ddstatus_Training.DataTextField = "StatusName";
        this.ddstatus_Training.DataSource = dt2;
        this.ddstatus_Training.DataBind();
    }
    protected void Show_Record_Training(int Trainingid)
    {
        HiddenTrainingpk.Value = Trainingid.ToString();
        DataTable dt3 = Training.selectDataTrainingDetailsByTrainingId(Trainingid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtTrainingname.Text = dr["TrainingName"].ToString();
            dd_trainingType.SelectedValue = dr["TypeOfTraining"].ToString();
            txtcreatedby_Training.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Training.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Training.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Training.Text = dr["ModifiedOn"].ToString();
            ddstatus_Training.SelectedValue = dr["StatusId"].ToString();
            //try
            //{
            //    dd_MTMTraining.SelectedValue = dr["MTMTrainingId"].ToString();
            //}
            //catch (Exception ex){dd_MTMTraining.SelectedIndex = 0; }

            txtCBTNo.Text = dr["CBTNo"].ToString();
            //txtSireChap.Text = dr["SireChap"].ToString();
            //try
            //{ ddlSireChap.SelectedValue = dr["SireChap"].ToString(); }
            //catch (Exception ex) { ddlSireChap.SelectedIndex = 0; }
        }
    }
    private void BindGridTraining()
    {
        string WhereClause = " where 1=1 ";
        if (txtTrianingNameSrch.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and TrainingName like '%" + txtTrianingNameSrch.Text.Replace("'","''") + "%'";
        }
        if (ddlTrainingTypeSrch.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and TypeOfTraining =" + ddlTrainingTypeSrch.SelectedValue + "";
        }
        //if (ddlSireChp.SelectedIndex != 0)
        //{
        //    WhereClause = WhereClause + " and SireChap=" + ddlSireChp.SelectedValue + "";
        //}
        if (txtCBTNoSrch.Text.Trim() != "")
        {
            WhereClause = WhereClause + " and CBTNo ='" + txtCBTNoSrch.Text.Replace("'","''") + "'";
        }

        string sql = "select TrainingId,TrainingName, "+
		            " (select TrainingTypeName from DBO.TrainingType where TrainingTypeId=Training.TypeOfTraining) as TypeOfTraining, "+
                    " (select TrainingName from DBO.CompanyTraining where TrainingId=Training.MTMTrainingId) as MtmTraining, " +
                   " (SELECT FirstName+' '+LastName from DBO.UserLogin where LoginId=Training.CreatedBy) as CreatedBy, " +
	               " Convert(char(10),CreatedOn,101) as 'CreatedOn', "+
                   " (SELECT FirstName+' '+LastName from DBO.UserLogin where LoginId=Training.ModifiedBy) as ModifiedBy, " +
	               " Convert(char(10),ModifiedOn,101) as 'ModifiedOn', "+
                   " (select StatusName from DBO.Status where StatusId=Training.StatusId) as StatusId " +
                    " ,CBTNo " +
                    //" ,(select top 1 ChapterName from dbo.m_Chapters where ID=Training.SireChap order by versionid desc)SireChap " +
                    " , '' As SireChap " +
                   " from DBO.Training ";
        sql = sql + WhereClause;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);


        this.GvTraining.DataSource = dt;
        this.GvTraining.DataBind();
    }
    //public void BindSireChap()
    //{
    //    string sql = "select ChapterNo,Convert(varchar,ChapterNo)+' '+ ChapterName as ChapterName from dbo.m_Chapters where InspectionGroup=1  and versionid in (select max(versionid) from dbo.m_Chapters where InspectionGroup=1 )order by ChapterNo ";
    //    DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

    //    ddlSireChap.DataSource = Dt;
    //    ddlSireChap.DataTextField = "ChapterName";
    //    ddlSireChap.DataValueField = "ChapterNo";
    //    ddlSireChap.DataBind();
    //    ddlSireChap.Items.Add(new ListItem("OTHERS", "-1"));
    //    ddlSireChap.Items.Insert(0, new ListItem(" Select ", ""));


    //    ddlSireChp.DataSource = Dt;
    //    ddlSireChp.DataTextField = "ChapterName";
    //    ddlSireChp.DataValueField = "ChapterNo";
    //    ddlSireChp.DataBind();
    //    ddlSireChp.Items.Add(new ListItem("OTHERS", "-1"));
    //    ddlSireChp.Items.Insert(0, new ListItem(" All ", ""));
        
    //}
    public void BindTraining()
    {
        string sql = "Select TrainingID,TrainingName from DBO.Training where TypeofTraining=" + Common.CastAsInt32(ddlTrainingTypeAddTrng.SelectedValue) + " order by TrainingName";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlTrainingAddTrng.DataSource = Dt;
        ddlTrainingAddTrng.DataTextField = "TrainingName";
        ddlTrainingAddTrng.DataValueField = "TrainingID";
        ddlTrainingAddTrng.DataBind();
        ddlTrainingAddTrng.Items.Insert(0, new ListItem(" Select ", ""));

        
    }

    public void BindSimilerrepeater()
    {
        string SQL = "select TrainingID,TrainingName,CBTNo,(select TrainingTypeName from DBO.trainingtype where TrainingTypeId in (select TypeOfTraining  from DBO.training where TrainingID=T.TrainingID))TrainingType from DBO.Training T Where T.TrainingID in (select SimilerTrainingID from DBO.TrainingSimiler where TrainingID=" + HiddenTrainingpk.Value + " Union" +
                    " select TrainingID from DBO.TrainingSimiler where SimilerTrainingID =" + HiddenTrainingpk.Value + ")";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptSimilarTraining.DataSource = dt;
        rptSimilarTraining.DataBind();
    }
    protected void DeleteSimiler(object sender, EventArgs e)
    {
        ImageButton imgb=(ImageButton)sender; 
        string SQL = "delete from TrainingSimiler where TrainingID=" + HiddenTrainingpk.Value + " and SimilerTrainingId=" + imgb.CommandArgument +
                     "delete from TrainingSimiler where SimilerTrainingId=" + HiddenTrainingpk.Value + " and TrainingID=" + imgb.CommandArgument;
        Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        BindSimilerrepeater();
    }
    
    
    // -------------- Event 
    protected void GvTraining_SelectIndexChanged(object sender, EventArgs e)
    {
        ImageButton btnView = (ImageButton)sender;
        HiddenField hfdTraining;
        hfdTraining = (HiddenField)btnView.Parent.FindControl("HiddenTrainingId");
        Show_Record_Training(Common.CastAsInt32(hfdTraining.Value));
        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        Alerts.HANDLE_AUTHORITY(4, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);     
  
    }
    protected void GvTraining_Row_Editing(object sender, EventArgs e)
    {
        ImageButton btnEdit = (ImageButton)sender;
        Mode = "Edit";
        HiddenField hfdTraining;
        hfdTraining = (HiddenField)btnEdit.Parent.FindControl("HiddenTrainingId");
        int id = Convert.ToInt32(hfdTraining.Value.ToString());
        Show_Record_Training(id);
        BindSimilerrepeater();
        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        fldSimilarTraining.Visible = true;
        Alerts.HANDLE_AUTHORITY(5, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);

    }
    protected void GvTraining_Row_Deleting(object sender,EventArgs e)
    {
        ImageButton btnDelete = (ImageButton)sender;
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)btnDelete.Parent.FindControl("HiddenTrainingId");
        int id = Convert.ToInt32(hfddel.Value.ToString());
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM TRAINING WHERE TRAININGID=" + id.ToString());
        Common.Execute_Procedures_Select_ByQueryCMS("delete from TrainingSimiler where TrainingID=" + id.ToString() + " OR SimilerTrainingId=" + id.ToString());
        BindGridTraining();
    }
    protected void btn_Print_Training_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Training_add_Click(object sender, EventArgs e)
    {
        HiddenTrainingpk.Value = "";
        txtTrainingname.Text = "";
        dd_trainingType.SelectedIndex = 0;
        txtcreatedby_Training.Text = "";
        txtcreatedon_Training.Text = "";
        txtmodifiedby_Training.Text = "";
        txtmodifiedon_Training.Text = "";
        //txtSireChap.Text = "";
        //ddlSireChap.SelectedIndex = 0;
        txtCBTNo.Text = "";
        ddstatus_Training.SelectedIndex = 0;
        //dd_MTMTraining.SelectedIndex = 0;
        Alerts.ShowPanel(Trainingpanel);
        divAddTraining.Visible = true;
        fldSimilarTraining.Visible = false;
        Alerts.HANDLE_AUTHORITY(2, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);    
 
    }
    protected void btn_Training_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (RepeaterItem dg in GvTraining.Items)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenTrainingName");
                //hfd1 = (HiddenField)dg.FindControl("HiddenTrainingId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtTrainingname.Text.ToUpper().Trim())
                {
                    if (HiddenTrainingpk.Value.Trim() == "")
                    {
                        lbl_Training_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    //else if (HiddenTrainingpk.Value.Trim() != hfd1.Value.ToString())
                    //{
                    //    lbl_Training_Message.Text = "Already Entered.";
                    //    Duplicate = 1;
                    //    break;
                    //}
                }
                else
                {
                    lbl_Training_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int TrainingId = -1;
                int createdby = 0, modifiedby = 0;
               
                string strTrainingName = txtTrainingname.Text;
                int typetraining = Convert.ToInt32(dd_trainingType.SelectedValue);
                int MTMTrainingId = Convert.ToInt32(0);
                char status = Convert.ToChar(ddstatus_Training.SelectedValue);
                if (HiddenTrainingpk.Value.Trim() == "")
                {
                    //createdby = 1;
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    TrainingId = Convert.ToInt32(HiddenTrainingpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                Training.insertUpdateTrainingDetails("InsertUpdateTrainingDetails",
                                              TrainingId,
                                              strTrainingName,
                                              typetraining,
                                              createdby,
                                              modifiedby,
                                              status, MTMTrainingId, txtCBTNo.Text.Trim(), "");
                string sql = "select max(TrainingID) from DBO.Training";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (HiddenTrainingpk.Value.Trim() == "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        HiddenTrainingpk.Value = dt.Rows[0][0].ToString();
                    }
                }

                BindGridTraining();
                BindSimilerrepeater();
                fldSimilarTraining.Visible = true;
            }
    }
    protected void btn_Training_Cancel_Click(object sender, EventArgs e)
    {
        Alerts.HidePanel(Trainingpanel);
        divAddTraining.Visible = false;
        Alerts.HANDLE_AUTHORITY(6, btn_Training_add, btn_Training_save, btn_Training_Cancel, btn_Print_Training, Auth);     
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindGridTraining();
    }
    protected void btnSaveSimilarTraining_OnClick(object sender, EventArgs e)
    {
        if (ddlTrainingAddTrng.SelectedIndex==0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select a training to add in list.')", true);
            return;
        }
        if (Common.CastAsInt32(HiddenTrainingpk.Value) == Common.CastAsInt32(ddlTrainingAddTrng.SelectedValue))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Same training can not added again in the list.')", true);
            return;
        }

        string sChk = "select * from DBO.TrainingSimiler  where TrainingID=" + HiddenTrainingpk.Value + " and SimilerTrainingID =" + ddlTrainingAddTrng.SelectedValue + " Union" +
                        " select * from DBO.TrainingSimiler  where TrainingID=" + ddlTrainingAddTrng.SelectedValue + " and SimilerTrainingID =" + HiddenTrainingpk.Value;

        DataTable dtChk = Common.Execute_Procedures_Select_ByQuery(sChk);
        if (dtChk.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Selected training is already added to similar training.')", true);
            return;
        }

        string sql = "INSERT INTO DBO.TrainingSimiler (TrainingID,SimilerTrainingID ) VALUES(" + HiddenTrainingpk.Value + "," + ddlTrainingAddTrng.SelectedValue + ")";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        BindSimilerrepeater();
    }

    protected void ddlTrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTraining();
    }

    //protected void CBTNumber_OnTextChanged(object sender, EventArgs e)
    //{
    //    string sql = "select TrainingID from training where CBTNo='"+txtCBTNo.Text+"'";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlTrainingAddTrng.SelectedValue = dt.Rows[0][0].ToString();
    //    }
    //}
    protected void CBTNumber_OnTextChanged(object sender, EventArgs e)
    {
        string sql = "select TrainingID from training where CBTNo='" + CBTNumber.Text + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            try
            {
                ddlTrainingAddTrng.SelectedValue = dt.Rows[0][0].ToString();
            }
            catch
            {
            }
        }
    }

    protected void ddlTrainingAddTrng_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "select CBTNo from training where TrainingID=" + ddlTrainingAddTrng.SelectedValue + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            CBTNumber.Text = dt.Rows[0][0].ToString();
        }
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        dd_trainingType.SelectedIndex = 0;
        txtCBTNo.Text = "";
        txtTrianingNameSrch.Text = "";
        //ddlSireChp.SelectedIndex = 0;
    }
    

}
