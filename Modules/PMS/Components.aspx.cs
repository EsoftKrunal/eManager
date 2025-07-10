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
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel; 

public partial class Components : System.Web.UI.Page
{
    AuthenticationManager Auth;
    bool setScroll = true;
    public int SelectedJobId
    {
        set { ViewState["SJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SJobId"]); }
    }
    public int SelectedCompJobId
    {
        set { ViewState["SCJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SCJobId"]); }
    }
    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return ""+ViewState["Mode"].ToString(); }
    }

    public bool IsSuperUser
    {
        set { ViewState["IsSuperUser"] = value; }
        get { return Convert.ToBoolean(ViewState["IsSuperUser"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission

        energiosSecurity.User usr = new energiosSecurity.User();
        IsSuperUser = usr.IsSuperUser(Convert.ToInt32(Session["UserID"]));
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1043, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");

            }
            else
            {
                btnPrintCompList.Visible = Auth.IsPrint;
            }
           // Auth = new AuthenticationManager(275, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnCompJobMapping.Visible = false;
            }
            else
            {
                btnPrintJobs.Visible = Auth.IsPrint;
            }
        }
        //*******************

        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 4;
            BindComponents();
            btnAddComponents.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnEditComponents.Visible = false;
            btnDeleteComponents.Visible = false;
            plJobsMapping.Visible = false;
            //tdIntParent.Visible = false;
            btnCompComponentMaster.CssClass = "selbtn";
            Disablefields();
            Mode = "";
            pnlCopyJob.Style.Add("display", "none");
        }
    }
    #region ----------- USER DEFINED FUNCTIONS --------------------
    private void BindComponents()
    {
        DataTable dtGroups = new DataTable();
        DataTable dtSystem;
        string strSQL = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=3 Order By ComponentCode";

        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        tvComponents.Nodes.Clear();

        TreeNode root = new TreeNode();
        root.Text = "COMPONENT STRUCTURE";
        root.Value = "";

        if (dtGroups.Rows.Count > 0)
        {
            for (int i = 0; i < dtGroups.Rows.Count; i++)
            {
                TreeNode gn = new TreeNode();
                gn.Text = dtGroups.Rows[i]["ComponentCode"].ToString() + " : " + dtGroups.Rows[i]["ComponentName"].ToString();
                gn.Value = dtGroups.Rows[i]["ComponentCode"].ToString();
                gn.ToolTip = dtGroups.Rows[i]["ComponentName"].ToString();
                gn.Expanded = false;
                DataTable dtSystems;
                String strQuery = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=6 AND LEFT(ComponentCode,3)='" + gn.Value.Trim() + "' Order By ComponentCode";
                dtSystems = Common.Execute_Procedures_Select_ByQuery(strQuery);
                if (dtSystems != null)
                {
                    for (int j = 0; j < dtSystems.Rows.Count; j++)
                    {
                        TreeNode sn = new TreeNode();
                        sn.Text = dtSystems.Rows[j]["ComponentCode"].ToString() + " : " + dtSystems.Rows[j]["ComponentName"].ToString();
                        sn.Value = dtSystems.Rows[j]["ComponentCode"].ToString();
                        sn.ToolTip = dtSystems.Rows[j]["ComponentName"].ToString();
                        sn.Expanded = false;
                        string SQL = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=9 AND LEFT(ComponentCode,6)='" + sn.Value.Trim() + "' Order By ComponentCode";
                        dtSystem = Common.Execute_Procedures_Select_ByQuery(SQL);
                        if (dtSystem.Rows.Count > 0)
                        {
                            sn.PopulateOnDemand = true;
                        }                                                
                        gn.ChildNodes.Add(sn);
                    }
                }
                root.ChildNodes.Add(gn);
            }
        }
        root.Expanded = true;
        tvComponents.Nodes.Add(root);
    }
    private Boolean IsValidated()
    {
        //if (hfCompCode.Value.Trim()=="")
        //{
        //    MessageBox1.ShowMessage("Please select a Component.",true);
        //    tvComponents.Focus();
        //    return false;
        //}
        //if (txtUnitCode.Visible)
        //{
        //    if (txtUnitCode.Text.Trim() == "")
        //    {
        //        MessageBox1.ShowMessage("Please enter unitcode.", true);
        //        txtUnitCode.Focus();
        //        return false;
        //    }
        //    if (int.Parse(txtUnitCode.Text.Trim()) == 0)
        //    {
        //        MessageBox1.ShowMessage("Invalid unitcode.", true);
        //        txtUnitCode.Focus();
        //        return false;
        //    }
        //    if(int.Parse(txtUnitCode.Text.Trim()) > 99)
        //    {
        //        MessageBox1.ShowMessage("Invalid unitcode,Can't create unit.", true);
        //        txtUnitCode.Focus();
        //        return false;
        //    }
        //}
        
        if (txtComponentCode.Text.Trim() == "")
        {
            MessageBox1.ShowMessage("Please enter Component Code.", true);
            txtComponentCode.Focus();
            return false;
        }
        Regex r = new Regex(@"^\d\d\d(.\d\d)?(.\d\d)?(.\d\d)?(.\d\d)?$");
        if (!(r.IsMatch(txtComponentCode.Text.Trim() + txtUnitCode.Text.Trim())))
        {
            MessageBox1.ShowMessage("Component code should be in proper format.", true);
            txtComponentCode.Focus();
            return false;
        }
        if (tvComponents.SelectedValue.Trim().Length == 0)
        {
            if (txtComponentCode.Text.Trim().Length != 3)
            {
                MessageBox1.ShowMessage("Component Code should be of 3 digits.", true);
                txtComponentCode.Focus();
                return false;
            }
            if (txtComponentCode.Text.Trim().Contains("."))
            {
                MessageBox1.ShowMessage("Component Code should not contain '.'", true);
                txtComponentCode.Focus();
                return false;
            }
        }
        if (txtUnitCode.Visible)
        {
            if (txtUnitCode.Text.Trim() == "")
            {
                MessageBox1.ShowMessage("Please enter Component Code.", true);
                txtUnitCode.Focus();
                return false;

            }
            if (txtUnitCode.Text.Trim().Length != 2)
            {
                MessageBox1.ShowMessage("Component Code should be of 2 digits.", true);
                txtUnitCode.Focus();
                return false;
            }
            if (txtUnitCode.Text.Trim()== "00")
            {
                MessageBox1.ShowMessage("Invalid Component Code.", true);
                txtUnitCode.Focus();
                return false;
            }
            if (txtUnitCode.Text.Trim().Contains("."))
            {
                MessageBox1.ShowMessage("Invalid Component Code.", true);
                txtUnitCode.Focus();
                return false;
            }
        }
        //if (tvComponents.SelectedValue.Trim().Length == 3)
        //{
        //    if (txtComponentCode.Text.Trim().Length != 6)
        //    {
        //        MessageBox1.ShowMessage("Component Code should be of 5 digits.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (!txtComponentCode.Text.Trim().Contains("."))
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOf('.') != 3)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().LastIndexOf('.') == 5)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //}
        //if (tvComponents.SelectedValue.Trim().Length == 6)
        //{
        //    if (txtComponentCode.Text.Trim().Length != 9)
        //    {
        //        MessageBox1.ShowMessage("Component Code should be of 7 digits.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (!txtComponentCode.Text.Trim().Contains("."))
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOfAny(new char[]{'.'}) != 3)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOfAny(new char[] { '.' },4) != 6)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().LastIndexOf('.') == 8)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //}

        //if (tvComponents.SelectedValue.Trim().Length == 9)
        //{
        //    if (txtComponentCode.Text.Trim().Length != 12)
        //    {
        //        MessageBox1.ShowMessage("Component Code should be of 9 digits.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (!txtComponentCode.Text.Trim().Contains("."))
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOfAny(new char[] { '.' }) != 3)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOfAny(new char[] { '.' }, 4) != 6)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().IndexOfAny(new char[] { '.' }, 7) != 9)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (txtComponentCode.Text.Trim().LastIndexOf('.') == 11)
        //    {
        //        MessageBox1.ShowMessage("Please enter valid Component Code.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //    if (Common.CastAsInt32(txtComponentCode.Text.Trim().Split('.').GetValue(3).ToString()) > 99)
        //    {
        //        MessageBox1.ShowMessage("Invalid unitcode,Can't create unit.", true);
        //        txtComponentCode.Focus();
        //        return false;
        //    }
        //}

        if (txtComponentName.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Component Name.",true);
            txtComponentName.Focus();
            return false;
        }
        if (txtComponentDesc.Text.Length > 500)
        {
            MessageBox1.ShowMessage("Description should not be more than 500 characters.",true);
            txtComponentDesc.Focus();
            return false;
        }
        //if (chkClass.Checked && txtClassCode.Text == "")
        //{
        //    MessageBox1.ShowMessage("Please enter class code.",true);
        //    txtClassCode.Focus();
        //    return false;
        //}
        //if (chkSms.Checked && txtSmsCode.Text == "")
        //{
        //    MessageBox1.ShowMessage("Please enter SIRE Code.",true);
        //    txtSmsCode.Focus();
        //    return false;
        //}
        return true;
    }
    private void ClearFields()
    {
        txtComponentCode.Text = "";
        txtComponentName.Text = "";
        txtComponentDesc.Text = "";
        chkClass.Checked = false;
        chkCE.Checked = false;
        chkCE.Visible= false;
        //chkSms.Checked = false;
        //txtClassCode.Text = "";
        //txtSmsCode.Text = "";
        chkCritical.Checked = false;
        //chkCSM.Checked = false;
        chkInactive.Checked = false;
        chkRHComponent.Checked = false;
       //chkInhParent.Checked = false;
        lblLinkedto.Text = "";
        hfCompCode.Value = "";
    }
    private void Disablefields()
    {
        txtComponentName.Enabled = false;
        txtComponentDesc.Enabled = false;
        chkCE.Enabled = false;
        chkClass.Enabled = false;
        //txtClassCode.Enabled = false;
        //chkSms.Enabled = false;
        //txtSmsCode.Enabled = false;
        chkCritical.Enabled = false;
        //chkCSM.Enabled = false;
        chkInactive.Enabled = false;
        //chkInhParent.Enabled = false;
        chkRHComponent.Enabled = false;
    }
    private void Enablefields()
    {
        txtComponentName.Enabled = true;
        txtComponentDesc.Enabled = true;
        chkClass.Enabled = true;
        chkCE.Enabled = true;
        //txtClassCode.Enabled = (chkClass.Checked ? true : false);
        //chkSms.Enabled = true;
        //txtSmsCode.Enabled = (chkSms.Checked ? true : false);
        chkCritical.Enabled = true;
        //chkCSM.Enabled = true;
        chkInactive.Enabled = true;
        //chkInhParent.Enabled = true;
        chkRHComponent.Enabled = true;
    }
    public void BindCopyComponents()
    {
        //ddlComponents.Items.Clear();
        //string strSQL = "SELECT ComponentId,ComponentCode FROM ComponentMaster " +
        //                "WHERE LEN(ComponentCode) = 12 AND LEFT(ComponentCode,9) = LEFT('" + tvComponents.SelectedValue + "',9) AND ComponentCode <> '" + tvComponents.SelectedValue + "' ";
        //DataTable dtcopyComp = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //if (dtcopyComp.Rows.Count > 0)
        //{
        //    ddlComponents.DataSource = dtcopyComp;
        //    ddlComponents.DataTextField = "ComponentCode";
        //    ddlComponents.DataValueField = "ComponentId";
        //    ddlComponents.DataBind();
        //}
        //else
        //{
        //    ddlComponents.Items.Clear();
        //    ddlComponents.DataSource = null;
        //    ddlComponents.DataTextField = "";
        //    ddlComponents.DataValueField = "";
        //    ddlComponents.DataBind();
        //}
        //ddlComponents.Items.Insert(0, new ListItem("< SELECT >", "0"));
        txtCopyCompToCode.Text = "";
    }
    

    // ************* Job Mapping **********************************************
    private void ShowComponentJobs(string componentCode)
    {
        Auth = new AuthenticationManager(1043, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (componentCode.Length != 0 || componentCode.Length != 2)
        {
            DataTable dtComponents;
            string strComponentDetails = "SELECT jm.JobId,cm.ComponentId ,jm.JobName,cjm.CompJobId,cjm.DescrSh,isnull(cm.CriticalEquip,0) as IsCritical,cm.CriticalType, jm.JobCode,JIM.IntervalName,DM.DeptName ,RM.RankCode,cjm.AttachmentForm,cjm.RiskAssessment  " +
                " ,dbo.GetJobAttachmentCount(cjm.CompJobId,'','') as AttachmentCount " +                        
                " FROM ComponentsJobMapping cjm " +
                                         "INNER Join  JobMaster jm ON jm.JobId = cjm.JobId " +                                         
                                         "INNER JOIN  ComponentMaster cm ON cm.ComponentId = cjm.ComponentId " +
                                         "INNER JOIN  JobIntervalMaster JIM ON JIM.IntervalId = cjm.IntervalId " +
                                         "INNER JOIN DeptMaster DM ON cjm.DeptId = DM.DeptId " +
                                         "INNER JOIN Rank RM ON cjm.AssignTo  = RM.RankId " +
                                         " WHERE cm.ComponentCode ='" + componentCode.Trim() + "' ORDER BY JobCode";
            Session.Add("sSqlForPrint", strComponentDetails);
            dtComponents = Common.Execute_Procedures_Select_ByQuery(strComponentDetails);
            if (dtComponents.Rows.Count > 0)
            {
                rptComponentJobs.DataSource = dtComponents;
                rptComponentJobs.DataBind();
                hfCompCode.Value = componentCode;
                imgbtnAddJobs.Visible = Auth.IsAdd;
                imgbtnEditJobs.Visible = Auth.IsUpdate;
               // btnDelete.Visible = Auth.IsDelete; -- by ajay request
                btnDelete.Visible = IsSuperUser; 
                btnPrint.Visible = Auth.IsPrint;
            }
            else
            {
                rptComponentJobs.DataSource = null;
                rptComponentJobs.DataBind();
                hfCompCode.Value = componentCode;
                imgbtnAddJobs.Visible = Auth.IsAdd;
                imgbtnEditJobs.Visible = false;
                btnDelete.Visible = false;
                btnPrint.Visible = false;
            }
        }
        else
        {
            //lblMessage.Text = "Please select a component.";
            MessageBox2.ShowMessage("Please select a component.",true);
        }
    }
    // ************************************************************************
    #endregion ----------------------------------------------------
    #region ------------ EVENTS -----------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (setScroll)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Componenttree');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_JobMaster');", true);
        }
    }
    protected void btnAddComponents_Click(object sender, EventArgs e)
    {
        Enablefields();
        if (tvComponents.SelectedValue.Trim().Length == 9)
        {
            DataTable dtGetData = new DataTable();
            string strSQL = "SELECT * FROM COMPONENTMASTER WHERE ComponentCode= '" + txtComponentCode.Text.Trim() + "'";
            dtGetData = Common.Execute_Procedures_Select_ByQuery(strSQL);
            chkClass.Checked = bool.Parse(dtGetData.Rows[0]["ClassEquip"].ToString());
            //chkSms.Checked = bool.Parse(dtGetData.Rows[0]["SmsEquip"].ToString());
            //txtClassCode.Text = dtGetData.Rows[0]["ClassEquipCode"].ToString();
            //txtSmsCode.Text = dtGetData.Rows[0]["SmsCode"].ToString();
            chkCritical.Checked = bool.Parse(dtGetData.Rows[0]["CriticalEquip"].ToString());
            chkCE.Visible = chkCritical.Checked;
            chkCE.Checked = dtGetData.Rows[0]["CriticalType"].ToString() == "E";
            //chkCSM.Checked = bool.Parse(dtGetData.Rows[0]["CSMItem"].ToString());
            chkInactive.Checked = bool.Parse(dtGetData.Rows[0]["Inactive"].ToString());
            chkRHComponent.Checked = bool.Parse(dtGetData.Rows[0]["RHComponent"].ToString());
            txtUnitCode.Visible = true;
            txtComponentCode.Text = tvComponents.SelectedValue.Trim() + ".";
            txtComponentName.Text = "";
            txtComponentDesc.Text = "";
            chkClass.Checked = false;            
            //chkCritical.Checked = false;            
            //chkInactive.Checked = false;
            //chkInhParent.Checked = false;
            string strNextunitCode = "";
            DataTable dtNextCode;
            strNextunitCode = "SELECT ComponentCode = REPLACE(STR(ISNULL(MAX(RIGHT(LTRIM(RTRIM(ComponentCode)),2)),0)+1,2),' ','0') FROM COMPONENTMASTER WHERE LEN(ComponentCode)=LEN('" + hfCompCode.Value.Trim() + "')+3 AND LEFT(ComponentCode,LEN('" + hfCompCode.Value.Trim() + "'))= '" + hfCompCode.Value.Trim() + "'";
            dtNextCode = Common.Execute_Procedures_Select_ByQuery(strNextunitCode);
            txtUnitCode.Text = dtNextCode.Rows[0]["ComponentCode"].ToString();


        }
        else
        {
            //txtUnitCode.Visible = true;
            DataTable dtNextComponetCode = new DataTable();
            string strComponentCode = "";
            if (tvComponents.SelectedValue.Trim().Length == 0)
            {
                strComponentCode = "SELECT ComponentCode = ltrim(rtrim('" + hfCompCode.Value.Trim() + "')) + REPLACE(STR(ISNULL(MAX(RIGHT(LTRIM(RTRIM(ComponentCode)),3)),0)+1,3),' ','0') FROM COMPONENTMASTER WHERE LEN(ComponentCode)=LEN('" + hfCompCode.Value.Trim() + "')+3 AND LEFT(ComponentCode,LEN('" + hfCompCode.Value.Trim() + "'))= '" + hfCompCode.Value.Trim() + "' ";
            }
            else
            {
                strComponentCode = "SELECT ComponentCode = ltrim(rtrim('" + hfCompCode.Value.Trim() + "')) + '.' + REPLACE(STR(ISNULL(MAX(RIGHT(LTRIM(RTRIM(ComponentCode)),2)),0)+1,2),' ','0'),(SELECT CriticalEquip FROM COMPONENTMASTER WHERE ComponentCode = '" + hfCompCode.Value.Trim() + "') As Critical FROM COMPONENTMASTER WHERE LEN(ComponentCode)=LEN('" + hfCompCode.Value.Trim() + "')+3 AND LEFT(ComponentCode,LEN('" + hfCompCode.Value.Trim() + "'))= '" + hfCompCode.Value.Trim() + "' ";
            }
            dtNextComponetCode = Common.Execute_Procedures_Select_ByQuery(strComponentCode);            
            if (dtNextComponetCode.Rows.Count > 0)
            {
                string nextCode = dtNextComponetCode.Rows[0]["ComponentCode"].ToString().Trim();
                if (nextCode.Contains("*"))
                {
                    nextCode = nextCode.Replace("*","0");
                }
                if (dtNextComponetCode.Rows[0]["ComponentCode"].ToString().Trim().Length == 3)
                {
                    txtComponentCode.Text = nextCode;
                    
                    txtUnitCode.Visible = false;
                    
                }
                else
                {
                    txtComponentCode.Text = nextCode.Substring(0, nextCode.Length - 2);
                    //hfCompCode.Value = dtNextComponetCode.Rows[0]["ComponentCode"].ToString().Trim();
                  
                    txtUnitCode.Visible = true;
                    txtUnitCode.Text = nextCode.Substring(nextCode.Length - 2);
                }
                if (tvComponents.SelectedValue.Trim().Length != 0)
                {
                    chkCritical.Checked = bool.Parse(dtNextComponetCode.Rows[0]["Critical"].ToString());
                    chkCritical.Enabled = false;
                }
                else
                {
                    chkCritical.Checked = false;
                    chkCritical.Enabled = true;
                }
            }
            txtComponentName.Text = "";
            txtComponentDesc.Text = "";
            chkClass.Checked = false;
            
            //chkSms.Checked = false;
            //txtClassCode.Text = "";
            //txtSmsCode.Text = "";
            //chkCritical.Checked = false;
            //chkCSM.Checked = false;
            chkInactive.Checked = false;
            //chkInhParent.Checked = false;
            chkRHComponent.Checked = false;
        }
        //if (tvComponents.SelectedValue.Trim().Length == 9 || tvComponents.SelectedValue.Trim().Length == 12)
        //{
        //    tdIntParent.Visible = false;
        //}
        if (tvComponents.SelectedValue.Trim() != "" || tvComponents.SelectedValue.Trim().Length != 3 )
        {
            if (lblCompCode.Text.Contains(":"))
            {
                string[] strLinkedTo = lblCompCode.Text.Trim().Split(':');
                lblLinkedto.Text = strLinkedTo[1].Trim();
            }
        }
        else
        {
            lblLinkedto.Text = "";
        }
        Mode = "";
        btnAddComponents.Visible = false;
        btnEditComponents.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
    }
    protected void btnEditComponents_Click(object sender, EventArgs e)
    {
        Enablefields();
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnAddComponents.Visible = false;
        Mode = "EDIT";
        txtComponentCode.MaxLength = tvComponents.SelectedValue.Trim().Length;
        
    }
    protected void btnUpdateCompCode_Click(object sender, EventArgs e)
    {
        if (txtNewCode.Text.Trim() == "")
        {
            lblPopupErrorMsg.Text = "Please enter new component code.";
            txtNewCode.Focus();
            return;

        }
        Regex r = new Regex(@"^\d\d\d(.\d\d)?(.\d\d)?(.\d\d)?(.\d\d)?$");
        if (!(r.IsMatch(txtNewCode.Text.Trim())))
        {
            lblPopupErrorMsg.Text = "Component code should be in proper format.";
            txtNewCode.Focus();
            return;
        }
        if (txtNewCode.Text.Trim().StartsWith(tvComponents.SelectedValue.Trim()))
        {
            lblPopupErrorMsg.Text = "Component can not move to self structure.";
            txtNewCode.Focus();
            return;
        }
        if (txtNewCode.Text.Trim().Length != 3)
        {
            int dotPosition = txtNewCode.Text.Trim().LastIndexOf(".");
            string parentCode = txtNewCode.Text.Substring(0, dotPosition);

            string strParent = "SELECT * FROM ComponentMaster WHERE ComponentCode ='" + parentCode + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(strParent);
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                lblPopupErrorMsg.Text = "Parent does not exists for new component code.";
                txtNewCode.Focus();
                return;
            }
        }
        Common.Set_Procedures("sp_MoveComponent");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
           new MyParameter("@MovingComponent", tvComponents.SelectedValue.Trim()),
           new MyParameter("@NewCode", txtNewCode.Text.Trim())
           );
        DataSet dsMoveComp = new DataSet();
        dsMoveComp.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsMoveComp);
        if (res)
        {
            if (dsMoveComp.Tables[0].Rows.Count > 0)
            {
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "1")
                {
                    lblPopupErrorMsg.Text = "Moving component going more than 4 level.";
                }
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "2")
                {
                    lblPopupErrorMsg.Text = "Component with same code already exists.";
                }
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "0")
                {
                    BindComponents();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "reloadComponents('" + txtNewCode.Text.Trim() + "');", true);
                    ModalPopupExtender1.Hide();
                }
            }
        }
        else
        {
            lblPopupErrorMsg.Text = "Unable to move conponent.Error :" + Common.getLastError();
        }
    }
    protected void btnCopyComponent_Click(object sender, EventArgs e)
    {
        if (txtNewCode.Text.Trim() == "")
        {
            lblPopupErrorMsg.Text = "Please enter copy component code.";
            txtNewCode.Focus();
            return;

        }
        Regex r = new Regex(@"^\d\d\d(.\d\d)?(.\d\d)?(.\d\d)?(.\d\d)?$");
        if (!(r.IsMatch(txtNewCode.Text.Trim())))
        {
            lblPopupErrorMsg.Text = "Component code should be in proper format.";
            txtNewCode.Focus();
            return;
        }
        if (txtNewCode.Text.Trim().StartsWith(tvComponents.SelectedValue.Trim()))
        {
            lblPopupErrorMsg.Text = "Component can not copy to self structure.";
            txtNewCode.Focus();
            return;
        }
        if (txtNewCode.Text.Trim().Length != 3)
        {
            int dotPosition = txtNewCode.Text.Trim().LastIndexOf(".");
            string parentCode = txtNewCode.Text.Substring(0, dotPosition);

            string strParent = "SELECT * FROM ComponentMaster WHERE ComponentCode ='" + parentCode + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(strParent);
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                lblPopupErrorMsg.Text = "Parent does not exists for copy component code.";
                txtNewCode.Focus();
                return;
            }
        }
        Common.Set_Procedures("sp_CopyComponent");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
           new MyParameter("@CopyingComponent", tvComponents.SelectedValue.Trim()),
           new MyParameter("@NewCode", txtNewCode.Text.Trim())
           );
        DataSet dsMoveComp = new DataSet();
        dsMoveComp.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsMoveComp);
        if (res)
        {
            if (dsMoveComp.Tables[0].Rows.Count > 0)
            {
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "1")
                {
                    lblPopupErrorMsg.Text = "Copying component going more than 4 level.";
                }
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "2")
                {
                    lblPopupErrorMsg.Text = "Component with same code already exists.";
                }
                if (dsMoveComp.Tables[0].Rows[0][0].ToString() == "0")
                {
                    BindComponents();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "reloadComponents('" + txtNewCode.Text.Trim() + "');", true);
                    ModalPopupExtender1.Hide();
                }
            }
        }
        else
        {
            lblPopupErrorMsg.Text = "Unable to copy conponent.Error :" + Common.getLastError();
        }
    }
    protected void btnDeleteComponents_Click(object sender, ImageClickEventArgs e)
    {
        Common.Set_Procedures("sp_Delete_Office_Components");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
           new MyParameter("@ComponentCode", tvComponents.SelectedValue.Trim())
           );
        DataSet dsComponents = new DataSet();
        dsComponents.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsComponents);
        if (res)
        {
            if(dsComponents.Tables[0].Rows.Count > 0)
            {
                if (dsComponents.Tables[0].Rows[0][0].ToString().Contains("successfully"))
                {
                    string FocusCode = "";
                    if (tvComponents.SelectedValue.Trim().Length != 3)
                    {
                        FocusCode = tvComponents.SelectedValue.Trim().Substring(0, tvComponents.SelectedValue.Trim().Length - 3);
                    }
                    else
                    {
                        string strPreComp = "SELECT TOP 1 ComponentCode FROM ComponentMaster " +
                                     "WHERE LEN(ComponentCode) = 3 AND CONVERT(INT,ComponentCode) < CONVERT(INT, '" + tvComponents.SelectedValue.Trim() + "') ORDER BY CONVERT(INT,ComponentCode) DESC ";
                        DataTable dtPreComp = Common.Execute_Procedures_Select_ByQuery(strPreComp);
                        FocusCode = dtPreComp.Rows[0][0].ToString();
                    }
                    BindComponents();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "reloadComponents('" + FocusCode.Trim() + "');", true);
                    MessageBox1.ShowMessage(dsComponents.Tables[0].Rows[0][0].ToString(), false);
                }
                else
                {
                    MessageBox1.ShowMessage(dsComponents.Tables[0].Rows[0][0].ToString(), true);
                }
            }
        }
        else
        {
            MessageBox1.ShowMessage("Unable to delete Conponent.Error :" + Common.getLastError(), true);
        }

        //string strCheckVessel = "SELECT CV.* FROM ComponentMasterForVessel CV INNER JOIN ComponentMaster CM ON CM.ComponentId = CV.ComponentId WHERE CM.ComponentCode = '" + tvComponents.SelectedValue.Trim() + "' ";
        //DataTable dtCheckVessel = Common.Execute_Procedures_Select_ByQuery(strCheckVessel);
        //string strCheckShip = "SELECT CS.* FROM VSL_ComponentMasterForVessel CS INNER JOIN ComponentMaster CM ON CM.ComponentId = CV.ComponentId WHERE CM.ComponentCode = '" + tvComponents.SelectedValue.Trim() + "' ";
        //DataTable dtCheckShip = Common.Execute_Procedures_Select_ByQuery(strCheckShip);
        //if (dtCheckVessel.Rows.Count > 0 || dtCheckShip.Rows.Count > 0)
        //{
        //    MessageBox1.ShowMessage("Component can not be deleted.it is being used.", true);
        //}
        //else
        //{
        //    string strDeleteComp = "DELETE FROM ComponentMaster WHERE ComponentCode= '" + tvComponents.SelectedValue.Trim() + "'; " +
        //                           "DELETE FROM ComponentMaster WHERE LEFT(ComponentCode,len('" + tvComponents.SelectedValue.Trim() + "'))= RTRIM(LTRIM('" + tvComponents.SelectedValue.Trim() + "' )); SELECT -1 ";
        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(strDeleteComp);
        //    if (dt.Rows.Count > 0)
        //    {
        //        string FocusCode = "";
        //        if (tvComponents.SelectedValue.Trim().Length != 3)
        //        {
        //            FocusCode = tvComponents.SelectedValue.Trim().Substring(0, tvComponents.SelectedValue.Trim().Length - 3);
        //        }
        //        else
        //        {
        //            string strPreComp = "SELECT TOP 1 ComponentCode FROM ComponentMaster " +
        //                         "WHERE LEN(ComponentCode) = 3 AND CONVERT(INT,ComponentCode) < CONVERT(INT, '" + tvComponents.SelectedValue.Trim() + "') ORDER BY CONVERT(INT,ComponentCode) DESC ";
        //            DataTable dtPreComp = Common.Execute_Procedures_Select_ByQuery(strPreComp);
        //            FocusCode = dtPreComp.Rows[0][0].ToString();
        //        }
        //        BindComponents();
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "reloadComponents('" + FocusCode.Trim() + "');", true);
        //        MessageBox1.ShowMessage("Component deleted successfully.", false);
        //    }
        //    else
        //    {
        //        MessageBox1.ShowMessage("Unable to delete component.", true);
        //    }
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {        
        tvComponents_SelectedNodeChanged(sender,e);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String ModeForDb = "";
        string strCode = "";
        DataTable dtComponentName;
        string FocusCompCode = tvComponents.SelectedNode.Value;
        string strCompCode = "";
        if (!IsValidated())
        {
            return;
        }
        if (txtUnitCode.Visible)
        {
            
            strCompCode = txtComponentCode.Text.Trim() + txtUnitCode.Text.Trim();
            // DUPLICASY CHECK -----------------------------------
            
            //strName = "SELECT ComponentName FROM ComponentMaster WHERE ComponentName ='" + txtComponentName.Text.Trim() + "' AND ComponentCode <>'" + txtComponentCode.Text.Trim() + "'";
            
            //strName = "SELECT ComponentName FROM ComponentMaster WHERE ComponentName ='" + txtComponentName.Text.Trim() + "' AND LEN(ComponentCode) = 8 AND ComponentCode = LEFT('" + strCompCode.Trim() + "',6) AND ComponentCode <>'" + strCompCode.Trim() + "'";
            //dtComponentName = Common.Execute_Procedures_Select_ByQuery(strName);
            //if (dtComponentName.Rows.Count > 0)
            //{

            //    MessageBox1.ShowMessage("Component Name already exists.", true);
            //    txtComponentName.Focus();
            //    return;
            //} 
            // DUPLICASY END -----------------------------------
        }
        else
        {
            strCompCode = txtComponentCode.Text.Trim();
            // DUPLICASY CHECK -----------------------------------

            //strName = "SELECT ComponentName FROM ComponentMaster WHERE ComponentName ='" + txtComponentName.Text.Trim() + "' AND ComponentCode <>'" + strCompCode.Trim() + "'";
            //dtComponentName = Common.Execute_Procedures_Select_ByQuery(strName);
            //if (dtComponentName.Rows.Count > 0)
            //{
            //    MessageBox1.ShowMessage("Component Name already exists.", true);
            //    txtComponentName.Focus();
            //    return;
            //}
            // DUPLICASY END -----------------------------------
        }
        if (Mode.Trim() != "EDIT")
        {
            ModeForDb = "A";
            strCode = "SELECT ComponentCode FROM ComponentMaster WHERE ComponentCode ='" + strCompCode.Trim() + "'";
            dtComponentName = Common.Execute_Procedures_Select_ByQuery(strCode);
            if (dtComponentName.Rows.Count > 0)
            {
                MessageBox1.ShowMessage("Component Code already exists.", true);
                txtComponentCode.Focus();
                return;
            } 
        }
        else
        {
            ModeForDb = "E";
            Regex r = new Regex(@"^\d\d\d(.\d\d)?(.\d\d)?(.\d\d)?(.\d\d)?$");
            if (!(r.IsMatch(strCompCode.Trim())))
            {
                MessageBox1.ShowMessage("Component code should be in proper format.", true);
                txtComponentCode.Focus();
                return;
            }
            if (strCompCode.Trim().Length != tvComponents.SelectedValue.Trim().Length)
            {
                MessageBox1.ShowMessage("Use move component button to move component to other level.", true);
                txtComponentCode.Focus();
                return;
            }
            if (strCompCode.Trim() != hfCompCode.Value.Trim())
            {
                strCode = "SELECT ComponentCode FROM ComponentMaster WHERE ComponentCode ='" + strCompCode.Trim() + "'";
                dtComponentName = Common.Execute_Procedures_Select_ByQuery(strCode);
                if (dtComponentName.Rows.Count > 0)
                {
                    MessageBox1.ShowMessage("Component Code already exists.", true);
                    txtComponentCode.Focus();
                    return;
                }
            }
            if (strCompCode.Trim().Length != 3)
            {
                int dotPosition = strCompCode.Trim().LastIndexOf(".");
                string parentCode = strCompCode.Substring(0, dotPosition);

                string strParent = "SELECT * FROM ComponentMaster WHERE ComponentCode ='" + parentCode + "' ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(strParent);
                if (dt.Rows.Count > 0)
                {
                }
                else
                {
                    MessageBox1.ShowMessage("Parent does not exists for selected component.", true);
                    txtComponentCode.Focus();
                    return;
                }
            }
        }
        string CriticalType = "";
        if (chkCritical.Checked)
        {
            if (chkCE.Checked)
                CriticalType = "E";
            else
                CriticalType = "C";
        }
        Common.Set_Procedures("sp_InsertUpdateComponents");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters(
            new MyParameter("@ComponentCode",strCompCode.Trim()),
            new MyParameter("@SelectedCompCode", hfCompCode.Value.Trim()),
            new MyParameter("@ComponentName", txtComponentName.Text.Trim()),
            new MyParameter("@Descr", txtComponentDesc.Text.Trim()),            
            new MyParameter("@ClassEquip", chkClass.Checked ? 1 : 0),
            new MyParameter("@CriticalEquip", chkCritical.Checked ? 1 : 0),
            new MyParameter("@CriticalType", CriticalType),
            new MyParameter("@Inactive", chkInactive.Checked ? 1 : 0),
            new MyParameter("@Mode", ModeForDb), 
            new MyParameter("@RHComponent", chkRHComponent.Checked ? 1 : 0)
            );

        DataSet dsComponents = new DataSet();
        dsComponents.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsComponents);

        if (res)
        {
            MessageBox1.ShowMessage("Component Added/Udpated Successfully.",false) ;
            ClearFields();
            BindComponents();
            btnAddComponents.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            txtUnitCode.Text = "";
            txtUnitCode.Visible = false;
            Disablefields();
            //if (ModeForDb == "A" || (ModeForDb == "E" && (strCompCode.Trim() == hfCompCode.Value.Trim())))
            //{ 
            FocusCompCode = strCompCode.Trim();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "reloadComponents('" + FocusCompCode + "');", true);
            //}
        }
        else
        {
            MessageBox1.ShowMessage("Unable to Add/Update Component.Error :" + Common.getLastError(),true);
        }
    }
    protected void tvComponents_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (plComponent.Visible == true)
        {
            Auth = new AuthenticationManager(1043, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (tvComponents.SelectedNode.Parent == null) // ROOT NODE
            {
                ClearFields();
                hfCompCode.Value = "";
                lblCompCode.Text = tvComponents.SelectedNode.Text;
                //btnAddComponents.Visible = true;
                btnEditComponents.Visible = false;
                btnDeleteComponents.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnMoveComponent.Style.Add("display", "none");
                btnAddComponents.Visible = Auth.IsAdd;
                //btnEditComponents.Visible = Auth.IsUpdate;
                //btnDeleteComponents.Visible = Auth.IsDelete;

            }
            else
            {
                hfCompCode.Value = tvComponents.SelectedNode.Value.Trim();
                lblCompCode.Text = tvComponents.SelectedNode.Text;
                ShowComponent();
                //btnEditComponents.Visible = true;
                //btnDeleteComponents.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                if (Auth.IsAdd && (tvComponents.SelectedNode.Value.Trim().Length != 12))
                {
                    btnAddComponents.Visible = true;  //(tvComponents.SelectedNode.Value.Trim().Length != 12);
                    //btnAddComponents.Visible = true; //Auth.IsAdd;
                }
                else
                {
                    btnAddComponents.Visible = false;
                }
                if (Auth.IsUpdate)
                {
                    btnMoveComponent.Style.Add("display", "");
                }
                btnEditComponents.Visible = Auth.IsUpdate;
                // btnDeleteComponents.Visible = Auth.IsDelete; -- by ajay request
                btnDeleteComponents.Visible =  false;
            }
            txtUnitCode.Visible = false;
            Disablefields();
        }
        else
        {
            Auth = new AuthenticationManager(1043, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (tvComponents.SelectedNode != null)
            {
                if (tvComponents.SelectedNode.Value.Trim().Length != 3)
                {
                    ShowComponentJobs(tvComponents.SelectedNode.Value.Trim());
                    lblComponentCode.Text = tvComponents.SelectedNode.Text;
                    //btnDelete.Visible = true;
                    //btnDelete.Visible = Auth.IsDelete;
                    btnDelete.Visible = IsSuperUser;
                    BindCopyComponents();
                }
                else
                {
                    btnCompJobMapping.Visible = false;
                    btnComponentMaster_Click(sender, e);
                    //btnDelete.Visible = false;
                    //btnDelete.Visible = Auth.IsDelete;
                    btnDelete.Visible = IsSuperUser;
                }
                if (Auth.IsUpdate && tvComponents.SelectedNode.Value.Trim().Length > 3)
                {
                    btnCopyJobs.Style.Add("display", "");
                }
                else
                {
                    btnCopyJobs.Style.Add("display", "none");
                }
            }
        }
        Auth = new AuthenticationManager(1043, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (Auth.IsView && tvComponents.SelectedNode.Value.Trim().Length != 3)
        {
            btnCompJobMapping.Visible = true;
        }
        else
        {
            btnCompJobMapping.Visible = false;
        }
    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {

        DataTable dtSubSystems;
        DataTable dtUnits;
        string strUnits = "";
        string strSubSystems = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=" + (e.Node.Value.Trim().Length + 3) + " AND LEFT(ComponentCode," + e.Node.Value.Trim().Length + ")='" + e.Node.Value.Trim() + "' Order By ComponentCode";
        dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
        if (dtSubSystems != null)
        {
            for (int k = 0; k < dtSubSystems.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtSubSystems.Rows[k]["ComponentCode"].ToString() + " : " + dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Value = dtSubSystems.Rows[k]["ComponentCode"].ToString();
                ssn.ToolTip = dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Expanded = false;
                if (e.Node.Value.Trim().Length == 6)
                {
                    strUnits = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))= 12 AND LEFT(ComponentCode, 9 )='" + ssn.Value.Trim() + "' Order By ComponentCode";
                    dtUnits = Common.Execute_Procedures_Select_ByQuery(strUnits);
                    if (dtUnits.Rows.Count > 0)
                    {
                        ssn.PopulateOnDemand = true;
                    }
                }
                e.Node.ChildNodes.Add(ssn);
            }
        }
    }
    private void ShowComponent()
    {
        string Code = hfCompCode.Value.Trim();
        DataTable dtSubSystemDetails;
        //string strSQL = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster WHERE ComponentCode ='" + Code + "'";
        string strSQL = "SELECT ComponentId,ComponentCode,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,ComponentName,Descr,ClassEquip,CriticalEquip,Inactive,CriticalType,RHComponent FROM ComponentMaster with(nolock) WHERE ComponentCode ='" + Code + "'";
        //,RHComponent
        dtSubSystemDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtSubSystemDetails.Rows.Count > 0)
        {
            hfCompId.Value = dtSubSystemDetails.Rows[0]["ComponentId"].ToString().Trim();
            txtComponentCode.Text = dtSubSystemDetails.Rows[0]["ComponentCode"].ToString().Trim();
            lblLinkedto.Text = dtSubSystemDetails.Rows[0]["LinkedTo"].ToString().Trim();
            txtComponentName.Text = dtSubSystemDetails.Rows[0]["ComponentName"].ToString();
            txtComponentDesc.Text = dtSubSystemDetails.Rows[0]["Descr"].ToString();
            chkClass.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["ClassEquip"].ToString());
            
            //chkSms.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["SmsEquip"].ToString());
            //txtClassCode.Text = dtSubSystemDetails.Rows[0]["ClassEquipCode"].ToString();
            //txtSmsCode.Text = dtSubSystemDetails.Rows[0]["SmsCode"].ToString();
            chkCritical.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["CriticalEquip"].ToString());
            chkCE.Checked = false;
            chkCE.Visible = false;
            if(chkCritical.Checked)
            {
                chkCE.Visible = true ;
                chkCE.Checked = dtSubSystemDetails.Rows[0]["CriticalType"].ToString()=="E";
            }
            //chkCSM.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["CSMItem"].ToString());
            chkInactive.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["Inactive"].ToString());
            chkRHComponent.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["RHComponent"].ToString());
            //if (hfCompCode.Value.Trim().Length == 9)
            //{
            //    if (dtSubSystemDetails.Rows[0]["InheritParentJobs"] != null && dtSubSystemDetails.Rows[0]["InheritParentJobs"].ToString() != "")
            //    {
            //        chkInhParent.Checked = Convert.ToBoolean(dtSubSystemDetails.Rows[0]["InheritParentJobs"].ToString());
            //    }
            //}
        }
        //----------------------
        if (Code.Length == 9)
        {
            btnAddComponents.Visible = false;
            //tdIntParent.Visible = true;
        }
        else
        {
            btnAddComponents.Visible = true;
            //tdIntParent.Visible = false;
        }
        btnSave.Visible = false;
        btnCancel.Visible = false;
        txtUnitCode.Text = "";
        txtUnitCode.Visible = false;
    }
    protected void chkCritical_CheckChanged(object sender, EventArgs e)
    {
        chkCE.Visible = chkCritical.Checked;
    }
    protected void btnPrintCompList_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompreport('Office','All');", true);
    }
    protected void btnPrintJobs_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompjobreport('Office','All');", true);
    }
    //protected void chkClass_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkClass.Checked)
    //    {
    //        txtClassCode.Enabled = true;
    //    }
    //    else
    //    {
    //        txtClassCode.Enabled = false;
    //    }
    //}
    //protected void chkSms_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkSms.Checked)
    //    {
    //        txtSmsCode.Enabled = true;
    //    }
    //    else
    //    {
    //        txtSmsCode.Enabled = false;
    //    }
    //}
    protected void btnSearchedCode_Click(object sender, EventArgs e)
    {
        string Group = "";
        string system = "";
        string subsystem = "";
        string valuepath = "";
        if (tvComponents.SelectedNode != null)
        {
            tvComponents.SelectedNode.Selected = false;
        }
        if (hfSearchCode.Value.Trim().Length == 3)
        {
            valuepath = "/" + hfSearchCode.Value.ToString().Trim() + "         ";
        }
        if (hfSearchCode.Value.Trim().Length == 6)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            valuepath = "/" + Group + "         /" + hfSearchCode.Value.Trim().ToString() + "      ";
        }
        if (hfSearchCode.Value.Trim().Length == 9)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            //TreeNode tng = new TreeNode();
            //tng.Value = system;
            foreach (TreeNode tnc in tvComponents.Nodes)
            {
                if (tnc.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnch in tnc.ChildNodes)
                    {
                        if (tnch.Value.Trim() == Group) // Group 01
                        {
                            if (tnch.ChildNodes.Count > 0)
                            {
                                foreach (TreeNode tnchch in tnch.ChildNodes) // System 0101
                                {
                                    if (tnchch.Value.Trim() == system)
                                    {
                                        tnchch.Expand();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            valuepath = "/" + Group + "         /" + system + "      /" + hfSearchCode.Value.ToString().Trim() + "   ";
        }
        if (hfSearchCode.Value.Trim().Length == 12)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            subsystem = hfSearchCode.Value.Trim().Substring(0,9);
            TreeNode tng = new TreeNode();
            tng.Value = hfSearchCode.Value.Trim();
            foreach (TreeNode tnc in tvComponents.Nodes) // root
            {
                if (tnc.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnGroup in tnc.ChildNodes)
                    {
                        if (tnGroup.ChildNodes.Count > 0) // group 01
                        {
                            if (tnGroup.Value.Trim() == Group)
                            {
                                if (tnGroup.ChildNodes.Count > 0)
                                {
                                    foreach (TreeNode tnSystem in tnGroup.ChildNodes) // System 0101
                                    {
                                        if (tnSystem.Value.Trim() == system)
                                        {
                                            tnSystem.Expand();
                                            if (tnSystem.ChildNodes.Count > 0)
                                            {                                                
                                                foreach (TreeNode tnSubSystem in tnSystem.ChildNodes) // SubSystem 010101
                                                {
                                                    if (tnSubSystem.Value.Trim() == subsystem)
                                                    {
                                                        tnSubSystem.Expand();
                                                        break;                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }                            
                        }
                        
                    }
                }
                
            }
            valuepath = "/" + Group + "         /" + system + "      /" + subsystem + "   /" + hfSearchCode.Value.ToString();
        }

        tvComponents.FindNode(valuepath).Select();
        TreeNode tn = tvComponents.SelectedNode;
        if (hfSearchCode.Value.Trim().Length == 6)
        {
            tn.Parent.Expand();
        }
        if (hfSearchCode.Value.Trim().Length == 9)
        {
            tn.Parent.Expand();
            tn.Parent.Parent.Expand();
        }
        if (hfSearchCode.Value.Trim().Length == 12)
        {
            tn.Parent.Expand();
            tn.Parent.Parent.Expand();
            tn.Parent.Parent.Parent.Expand();
        }
        tvComponents_SelectedNodeChanged(sender, e);
        setScroll = false;
        if (tn.ToolTip != "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "fds", "setFocus('" + tn.ToolTip + "');", true);
        }
    }
    protected void btnSearchComponents_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opensearchwindow();", true);
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=OfficeMasterJobs&ComponentName=" + lblComponentCode .Text.Trim()+ "');", true);
    }
    #region  ************* Job Mapping **********************************************

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode.Parent == null || hfCompCode.Value.Trim().Length == 3 || hfCompCode.Value.Trim().Length == 0)
        {
            MessageBox2.ShowMessage("Please select a component.", true);
            return;
        }
        if (SelectedCompJobId == 0)
        {
            MessageBox2.ShowMessage("Please select a job to delete", true);
            return;
        }
        else
        {
            Common.Set_Procedures("sp_Delete_Office_Jobs");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@CompJobId", SelectedCompJobId),
                new MyParameter("@loginId", Convert.ToInt32(Session["loginid"].ToString()))
                );
            DataSet dsComponentJobs = new DataSet();
            dsComponentJobs.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponentJobs);
            if (res)
            {
                if (dsComponentJobs.Tables[0].Rows.Count > 0)
                {
                    if (dsComponentJobs.Tables[0].Rows[0][0].ToString().Contains("successfully"))
                    {
                        ShowComponentJobs(hfCompCode.Value.Trim());
                        MessageBox2.ShowMessage(dsComponentJobs.Tables[0].Rows[0][0].ToString(), false);
                    }
                    else
                    {
                        MessageBox2.ShowMessage(dsComponentJobs.Tables[0].Rows[0][0].ToString(), true);
                    }
                }
            }
            else
            {
                MessageBox2.ShowMessage("Unable to Delete Job.Error :" + Common.getLastError(), true);
            }
        }
        //if (hfCompCode.Value.Trim().Length == 6)
        //{
        //    foreach (RepeaterItem rpItem in rptComponentJobs.Items)
        //    {
        //        CheckBox chkSelect = (CheckBox)rpItem.FindControl("chkSelect");
        //        HiddenField hfJobId = (HiddenField)rpItem.FindControl("hfJobId");
        //        int jobId = Common.CastAsInt32(hfJobId.Value);
        //        if (chkSelect.Checked)
        //        {
        //            DataTable dt = new DataTable();
        //            string strSQL = "SELECT * FROM ComponentsJobMapping CJM INNER JOIN  ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
        //                            "WHERE LEN(CM.ComponentCode) = 9 AND LEFT(CM.ComponentCode,6) = '" + hfCompCode.Value.Trim() + "' AND  CM.InheritParentJobs = 1 AND CJM.JobId = " + jobId + " ";
        //            dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    int compId = Convert.ToInt32(dr["ComponentId"].ToString());
        //                    Common.Set_Procedures("sp_Delete_Office_Master");
        //                    Common.Set_ParameterLength(3);
        //                    Common.Set_Parameters(
        //                        new MyParameter("@Param_CompId", compId),
        //                        new MyParameter("@Param_JobId", jobId),
        //                        new MyParameter("@ItemType", 3)                                
        //                        );
        //                    DataSet dsComponents = new DataSet();
        //                    dsComponents.Clear();
        //                    Boolean res;
        //                    res = Common.Execute_Procedures_IUD(dsComponents);
        //                    if (res)
        //                    {
        //                        //lblMessage.Text = "Job Deleted Successfully.";
        //                    }
        //                    else
        //                    {
        //                        MessageBox2.ShowMessage("Unable to Delete Job.", true);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //foreach (RepeaterItem rptItem in rptComponentJobs.Items)
        //{
        //    CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
        //    HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
        //    if (chkSelect.Checked)
        //    {
        //        DataTable dtCompId = new DataTable();
        //        string strSQL = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + hfCompCode.Value.Trim() + "' ";
        //        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //        int componentId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
        //        int jobId = Common.CastAsInt32(hfJobId.Value);
        //        Common.Set_Procedures("sp_Delete_Office_Master");
        //        Common.Set_ParameterLength(3);
        //        Common.Set_Parameters(
        //            new MyParameter("@Param_CompId", componentId),
        //            new MyParameter("@Param_JobId", jobId),
        //            new MyParameter("@ItemType", 3)
        //            );
        //        DataSet dsComponents = new DataSet();
        //        dsComponents.Clear();
        //        Boolean res;
        //        res = Common.Execute_Procedures_IUD(dsComponents);
        //        if (res)
        //        {
        //            MessageBox2.ShowMessage("Job Deleted Successfully.", false);
        //        }
        //        else
        //        {
        //            MessageBox2.ShowMessage("Unable to Delete Job.", true);
        //        }
        //    }
        //}
        
    }
    protected void btnAddJobs_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode.Parent == null || hfCompCode.Value.Trim().Length == 3 || hfCompCode.Value.Trim().Length == 0)
        {
            MessageBox2.ShowMessage("Please Select a Component.", true);
            return;
        }
        //string strSQL = "SELECT * FROM JobMaster WHERE JobId NOT IN (SELECT CJM.JobId FROM ComponentsJobMapping CJM INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId WHERE CM.ComponentCode = '" + hfCompCode.Value.Trim() + "' )";
        //DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //if (dtJobs.Rows.Count <= 0)
        //{
        //    MessageBox2.ShowMessage("All jobs assigned to " + tvComponents.SelectedNode.Text.ToString(), true);
        //}
        //else
        //{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddwindow('" + hfCompCode.Value.Trim() + "');", true);
        //}
        
    }
    protected void btnEditJobs_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode.Parent == null || hfCompCode.Value.Trim().Length == 3 || hfCompCode.Value.Trim().Length == 0)
        {
            //lblMessage.Text = "Please select a component.";
            MessageBox2.ShowMessage("Please Select a Component.", true);
            return;
        }
        if (SelectedJobId == 0)
        {
            MessageBox2.ShowMessage("Please select a job to Edit.", true);
            return;
        }
        //int i = 0;
        //string strJobIds = "";
        //foreach (RepeaterItem rptItem in rptComponentJobs.Items)
        //{
        //    CheckBox chkSelectJob = (CheckBox)rptItem.FindControl("chkSelect");
        //    HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
        //    if (chkSelectJob.Checked)
        //    {
        //        i = i + 1;
        //        strJobIds = strJobIds + hfJobId.Value.ToString() + ",";
        //    }
        //}
        //if (i == 0)
        //{
        //    //lblMessage.Text = "Please select a job to Edit.";
        //    MessageBox2.ShowMessage("Please select a job to Edit.", true);
        //    return;
        //}
        //string JobIds = strJobIds.Remove(strJobIds.Length - 1, 1);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openeditwindow('" + hfCompCode.Value.Trim() + "','" + SelectedJobId + "','" + SelectedCompJobId + "');", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddwindow('" + hfCompCode.Value.Trim() + "');", true);

    }
    protected void btnCopyJob_Click(object sender, EventArgs e)
    {
        //if (ddlComponents.SelectedIndex == 0)
        //{
        //    lblCopyJobErrMsg.Text = "Please select a component.";
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please select a component.');", true);
        //    return;
        //}
        if (txtCopyCompToCode.Text.Trim()=="")
        {
            lblCopyJobErrMsg.Text = "Please enter a componentcode.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Please enter componentcode.');", true);
            return;
        }
        if (txtCopyCompToCode.Text.Trim().Length == 3 && (! txtCopyCompToCode.Text.Trim().Contains(".")))
        {
            lblCopyJobErrMsg.Text = "Invalid componentcode.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Invalid componentcode.');", true);
            return;
        }
        DataTable dtComp = Common.Execute_Procedures_Select_ByQuery("select * from componentmaster where componentcode='" + txtCopyCompToCode.Text + "'");
        if (dtComp.Rows.Count <=0)
        {
            lblCopyJobErrMsg.Text = "Invalid componentcode.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Invalid componentcode.');", true);
            return;
        }
        int SCompJobId = 0;
        SCompJobId = Common.CastAsInt32(SelectedCompJobId);
        if (radjobtype.SelectedValue == "S")
        {
            if (SCompJobId <= 0)
            {
                lblCopyJobErrMsg.Text = "No Job Selected. Please select a job to copy.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('No Job Selected. Please select a job to copy.');", true);
                return;
            }
        }
        
        //-----------------------------------------
        Common.Set_Procedures("sp_CopyComponentJob");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
           new MyParameter("@TreeSelectedCompCode", tvComponents.SelectedValue.Trim()),
           new MyParameter("@ToCompCode", txtCopyCompToCode.Text.Trim()),
           new MyParameter("@Copy_CompJobId", SCompJobId)
           );

        DataSet dsCopy = new DataSet();
        dsCopy.Clear(); 
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsCopy);
        if (res)
        {
            lblCopyJobErrMsg.Text = "Jobs copied successfully.";
            string Folder = Server.MapPath("UploadFiles/AttachmentForm/");
            foreach (DataRow dr in dsCopy.Tables[0].Rows)
            {
                try
                {
                    System.IO.File.Copy(Folder + dr["SORUCEFILE"].ToString(), Folder + dr["DESTFILE"].ToString(), true);
                }
                catch { }
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Jobs copied successfully.');", true);
            ShowComponentJobs(tvComponents.SelectedNode.Value.Trim());
        }
        else
        {
            lblCopyJobErrMsg.Text = "Unable to copy conponent.Error :" + Common.getLastError();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Unable to copy conponent.');", true);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowComponentJobs(hfCompCode.Value.Trim());
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_JobMaster');", true);
    }
    protected void btnComponentMaster_Click(object sender, EventArgs e)
    {
        plComponent.Visible = true;
        plJobsMapping.Visible = false;
        btnCompComponentMaster.CssClass = "selbtn";
        btnCompJobMapping.CssClass = "btn1";
        btnJobComponentmaster.CssClass = "selbtn";
        btnJobJobMapping.CssClass = "btn1";
        pnlUpdate.Visible = true;
        pnlCopyJob.Visible = false;
        if (tvComponents.SelectedNode != null)
        {
            tvComponents_SelectedNodeChanged(sender, e);
        }
    }
    protected void btnJobMapping_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode != null)
        {            
            plComponent.Visible = false;
            plJobsMapping.Visible = true;
            btnCompComponentMaster.CssClass = " btn1";
            btnCompJobMapping.CssClass = "selbtn";
            btnJobComponentmaster.CssClass = "btn1";
            btnJobJobMapping.CssClass = "selbtn";
            tvComponents_SelectedNodeChanged(sender, e);
            pnlUpdate.Visible = false;
            pnlCopyJob.Visible = true;
        }
        else
        {
            MessageBox1.ShowMessage("Please select a component.", true);
        }
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedCompJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        HiddenField hfJobId = (HiddenField)((ImageButton)sender).Parent.FindControl("hfJobId");
        SelectedJobId = Common.CastAsInt32(hfJobId.Value);
        ShowComponentJobs(tvComponents.SelectedNode.Value.Trim());
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_JobMaster');", true);
    }

#endregion ************************************************************************      
   
    #endregion ----------------------------------------------------

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportComponent();
        ExportComonentJobs();
    }
    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            MessageBox1.ShowMessage("Unable to release the Object " + ex.ToString(), true);
        }
        finally
        {
            GC.Collect();
        }
    }
    public void ExportComponent()
    {
        string strSQL = "SELECT ComponentCode, ComponentName FROM  ComponentMaster ORDER BY ComponentCode ";
        DataTable dtExportComp = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtExportComp.Rows.Count > 0)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("SetUp/ComponentMaster.xls"), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Unprotect("template");
            for (int i = 0; i < dtExportComp.Rows.Count; i++)
            {
                xlWorkSheet.Cells[9 + i, 1] = dtExportComp.Rows[i]["ComponentCode"];
                xlWorkSheet.Cells[9 + i, 2] = dtExportComp.Rows[i]["ComponentName"];
            }
            xlWorkSheet.Protect("template", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

    }
    public void ExportComonentJobs()
    {
        string strSQL = "SELECT dbo.ComponentMaster.ComponentCode, dbo.ComponentMaster.ComponentName,dbo.ComponentsJobMapping.CompJobId, dbo.JobMaster.JobCode,dbo.ComponentsJobMapping.DescrSh, dbo.Rank.RankCode, dbo.DeptMaster.DeptName, dbo.JobIntervalMaster.IntervalName,dbo.ComponentMaster.CriticalEquip, '' AS LastdoneDate " +
                       "FROM  dbo.ComponentMaster  " +
                       "INNER JOIN dbo.ComponentsJobMapping ON dbo.ComponentMaster.ComponentId = dbo.ComponentsJobMapping.ComponentId  " +
                       "INNER JOIN dbo.JobMaster ON dbo.ComponentsJobMapping.JobId = dbo.JobMaster.JobId " +
                       "INNER JOIN dbo.DeptMaster ON dbo.ComponentsJobMapping.DeptId = dbo.DeptMaster.DeptId " +
                       "INNER JOIN dbo.Rank ON dbo.ComponentsJobMapping.AssignTo = dbo.Rank.RankId " +
                       "INNER JOIN dbo.JobIntervalMaster ON dbo.JobIntervalMaster.IntervalId = dbo.ComponentsJobMapping.IntervalId " +
                       "ORDER BY dbo.ComponentMaster.ComponentCode ";
        DataTable dtExportCompJob = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtExportCompJob.Rows.Count > 0)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("SetUp/JobMaster.xls"), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Unprotect("template");
            for (int i = 0; i < dtExportCompJob.Rows.Count; i++)
            {
                xlWorkSheet.Cells[9 + i, 1] = dtExportCompJob.Rows[i]["ComponentCode"];
                xlWorkSheet.Cells[9 + i, 2] = dtExportCompJob.Rows[i]["ComponentName"];
                xlWorkSheet.Cells[9 + i, 3] = dtExportCompJob.Rows[i]["CompJobId"];
                xlWorkSheet.Cells[9 + i, 4] = dtExportCompJob.Rows[i]["JobCode"];
                xlWorkSheet.Cells[9 + i, 5] = dtExportCompJob.Rows[i]["DescrSh"];
                xlWorkSheet.Cells[9 + i, 6] = dtExportCompJob.Rows[i]["DeptName"];
                xlWorkSheet.Cells[9 + i, 7] = dtExportCompJob.Rows[i]["RankCode"];
            }

            xlWorkSheet.Protect("template", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            MessageBox1.ShowMessage("Component and Jobs Exported successfully.", false);
        }
    }

    protected void chkRHComponent_CheckedChanged(object sender, EventArgs e)
    {

    }
}
