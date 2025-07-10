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

public partial class VesselSetup : System.Web.UI.Page
{
    public static string mode = "";
    public int CompId
    {
        set { ViewState["CompCode"] = value; }
        get { return Common.CastAsInt32(ViewState["CompCode"].ToString()); }
    }
    public int SelectedNodeParentId
    {
        set { ViewState["SelectedNodeParentId"] = value; }
        get { return Common.CastAsInt32(ViewState["SelectedNodeParentId"].ToString()); }
    }
    public static string SelectedCompCode = "";    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1044, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------
        if (!Page.IsPostBack)
        {
            SelectedNodeParentId = 0;
            BindVessels();
            plOfficeMaster.Visible = false;
            plExcelTemp.Visible = false;
            plCopyVessel.Visible = false;
            if (Request.QueryString["VC"] != null && Request.QueryString["Mode"] != null)
            {
                ddlVessels.SelectedValue = Request.QueryString["VC"].ToString();
                ddlVessels_SelectedIndexChanged(sender, e);
                lblVessel.Text = "Vessel Code & Name : [ " + Request.QueryString["VC"].ToString() + " ] " + ddlVessels.SelectedItem.Text.ToString() ;
                lblToVesselName.Text = ddlVessels.SelectedItem.Text.ToString();
                mode = Request.QueryString["Mode"].ToString();
                //if(Request.QueryString["Mode"].ToString() == "ADD")
                //{
                //    lblPage.Text = " Create Vessel Component Structure";                    
                //    dvMode.Visible = true;
                //}
                //if (Request.QueryString["Mode"].ToString() == "EDIT")
                //{
                //    lblPage.Text = " Modify Vessel Component Structure";
                //    dvMode.Visible = false;
                //    plOfficeMaster.Visible = true;
                //}
                
                lblPage.Text = " Modify Vessel Component Structure";

                
                BindComponentFirstLevel();
                btnRemoveselectedComp.Visible = false;
            }
        }
    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel ORDER BY VesselName";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, "< SELECT >");
    }
    private void BindCopyVessels()
    {
        DataTable dtCopyVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel WHERE VesselCode IN (SELECT DISTINCT VesselCode FROM dbo.ComponentMasterForVessel ) ORDER BY VesselName";
        dtCopyVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtCopyVessels.Rows.Count > 0)
        {
            ddlCopyVessel.DataSource = dtCopyVessels;
            ddlCopyVessel.DataTextField = "VesselName";
            ddlCopyVessel.DataValueField = "VesselCode";
            ddlCopyVessel.DataBind();
        }
        else
        {
            ddlCopyVessel.DataSource = null;
            ddlCopyVessel.DataBind();
        }
        ddlCopyVessel.Items.Insert(0, "< SELECT >");
    }
    private void BindComponentFirstLevel()
    {
        string strSQL = "SELECT ComponentCode, ComponentId, ComponentCode + ' : ' + ComponentName AS ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=3 Order By ComponentCode";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            ddlComponentFirstLevel.DataSource = dt;
            ddlComponentFirstLevel.DataTextField = "ComponentName";
            ddlComponentFirstLevel.DataValueField = "ComponentCode";
            ddlComponentFirstLevel.DataBind();
            ddlComponentFirstLevel.Items.Insert(0,"< SELECT Component >");
        }
    }
    private void BindGroupsAndSystems()
    {
        //DataTable dtGroups = new DataTable();        
        //string strSQL = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=2 Order By ComponentCode";
        //dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //tvComponents.Nodes.Clear();
        //if (dtGroups.Rows.Count > 0)
        //{
            //for (int i = 0; i < dtGroups.Rows.Count; i++)
            //{
            //    TreeNode gn = new TreeNode();
            //    gn.Text = dtGroups.Rows[i]["ComponentCode"].ToString() + " : " + dtGroups.Rows[i]["ComponentName"].ToString();
            //    gn.Value = dtGroups.Rows[i]["ComponentId"].ToString();
            //    gn.ToolTip = dtGroups.Rows[i]["ComponentName"].ToString();
            //    gn.Expanded = false;
            //    string gncode = dtGroups.Rows[i]["ComponentCode"].ToString();
        
            DataTable dtSystems;
            String strQuery = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=6 AND LEFT(ComponentCode,3)='" + ddlComponentFirstLevel.SelectedValue.Trim() + "' Order By ComponentCode";
            dtSystems = Common.Execute_Procedures_Select_ByQuery(strQuery);
            if (dtSystems != null)
            {
                for (int j = 0; j < dtSystems.Rows.Count; j++)
                {
                    TreeNode sn = new TreeNode();
                    sn.Text = dtSystems.Rows[j]["ComponentCode"].ToString() + " : " + dtSystems.Rows[j]["ComponentName"].ToString();
                    sn.Value = dtSystems.Rows[j]["ComponentId"].ToString();
                    sn.ToolTip = dtSystems.Rows[j]["ComponentName"].ToString();
                    sn.Expanded = false;
                    DataTable dtSubSystems;
                    DataTable dtUnits;
                    string strUnits = "";
                    string sncode = sn.Text.ToString().Split(':').GetValue(0).ToString().Trim();
                    string strSubSystems = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=" + (sncode.Trim().Length + 3) + " AND LEFT(ComponentCode," + sncode.Trim().Length + ")='" + sncode + "' Order By ComponentCode";
                    dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
                    if (dtSubSystems != null)
                    {
                        for (int k = 0; k < dtSubSystems.Rows.Count; k++)
                        {
                            TreeNode ssn = new TreeNode();
                            ssn.Text = dtSubSystems.Rows[k]["ComponentCode"].ToString() + " : " + dtSubSystems.Rows[k]["ComponentName"].ToString();
                            ssn.Value = dtSubSystems.Rows[k]["ComponentId"].ToString();
                            ssn.ToolTip = dtSubSystems.Rows[k]["ComponentName"].ToString();
                            ssn.Expanded = false;
                            string ssncode = ssn.Text.Split(':').GetValue(0).ToString().Trim();
                            strUnits = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))= 12 AND LEFT(ComponentCode, 9 )='" + ssncode.Trim() + "' Order By ComponentCode";
                            dtUnits = Common.Execute_Procedures_Select_ByQuery(strUnits);
                            if (dtUnits != null)
                            {
                                for (int i = 0; i < dtUnits.Rows.Count; i++)
                                {
                                    TreeNode ssnu = new TreeNode();
                                    ssnu.Text = dtUnits.Rows[i]["ComponentCode"].ToString() + " : " + dtUnits.Rows[i]["ComponentName"].ToString();
                                    ssnu.Value = dtUnits.Rows[i]["ComponentId"].ToString();
                                    ssnu.ToolTip = dtUnits.Rows[i]["ComponentName"].ToString();
                                    ssnu.Expanded = false;
                                    ssn.ChildNodes.Add(ssnu);
                                }
                            }
                            sn.ChildNodes.Add(ssn);
                        }
                    }
                    tvComponents.Nodes.Add(sn);
                }
            }        
        if (Request.QueryString["Mode"].ToString() == "EDIT")
        {
            DataTable dtComponents = new DataTable();
            string strEditSQL = "SELECT ComponentId FROM ComponentMasterForVessel WHERE VesselCode='" + ddlVessels.SelectedValue.Trim().ToString() + "'";
            dtComponents = Common.Execute_Procedures_Select_ByQuery(strEditSQL);
            if (dtComponents.Rows.Count > 0)
            {
                foreach(DataRow dr in dtComponents.Rows)
                {
                    foreach (TreeNode tn in tvComponents.Nodes)
                    {
                        if (tn.Value == dr["ComponentId"].ToString())
                        {
                            tn.Checked = true;
                        }
                        else 
                        {
                            if (tn.ChildNodes.Count > 0)
                            {
                                foreach(TreeNode tnc in tn.ChildNodes )
                                {
                                    if (tnc.Value == dr["ComponentId"].ToString())
                                    {
                                        tnc.Checked = true;

                                    }
                                    else
                                    {
                                        if (tnc.ChildNodes.Count > 0)
                                        {
                                            foreach(TreeNode tncu in tnc.ChildNodes)
                                            {
                                                if (tncu.Value == dr["ComponentId"].ToString())
                                                {
                                                    tncu.Checked = true;
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
    }
    private void SaveStructure()
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a Vessel.", true);
        }
        if (ddlComponentFirstLevel.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a Component.", true);
        }
        else
        {
            if (tvComponents.CheckedNodes.Count > 0)
            {
                string ComponentIds = Convert.ToString(CompId) + ",";

                //if (mode == "EDIT")
                //{
                //    foreach (TreeNode tn in tvComponents.CheckedNodes)
                //    {
                //        ComponentIds = ComponentIds + tn.Value + ",";
                //        if (tn.ChildNodes.Count > 0)
                //        {
                //            foreach (TreeNode tnc in tn.ChildNodes)
                //            {
                //                ComponentIds = ComponentIds + tnc.Value + ",";
                //                if (tnc.ChildNodes.Count > 0)
                //                {
                //                    foreach (TreeNode tncu in tnc.ChildNodes)
                //                    {
                //                        ComponentIds = ComponentIds + tncu.Value + ",";
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    ComponentIds = ComponentIds.Remove(ComponentIds.Length - 1);
                //    string strDelete = " DELETE FROM ComponentMasterForVessel WHERE VesselCode ='" + ddlVessels.SelectedValue.ToString() + "' AND ComponentId IN (" + ComponentIds + ") ";
                //    Common.Execute_Procedures_Select_ByQuery(strDelete);
                //}
                Common.Set_Procedures("sp_InsertComponentsForVessel");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
                    new MyParameter("@ComponentId", CompId)
                    );
                DataSet dsVesselComp = new DataSet();
                dsVesselComp.Clear();
                Boolean resu;
                resu = Common.Execute_Procedures_IUD(dsVesselComp);
                if (resu)
                {
                    MessageBox1.ShowMessage("Component added to vessel.", false);
                }
                else
                {
                    MessageBox1.ShowMessage("Unable to add Componenet to vessel.Error :" + Common.getLastError(), true);
                }
                foreach (TreeNode tn in tvComponents.CheckedNodes)
                {
                    if (tn.Checked)
                    {
                        Common.Set_Procedures("sp_InsertComponentsForVessel_AutoSpecFromParent");
                        Common.Set_ParameterLength(3);
                        Common.Set_Parameters(
                            new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
                            new MyParameter("@ComponentId", tn.Value),
                            new MyParameter("@ParentComponentId", SelectedNodeParentId == 0 ? CompId : SelectedNodeParentId)
                                                        );
                        DataSet dsVesselComponents = new DataSet();
                        dsVesselComponents.Clear();
                        Boolean res;
                        res = Common.Execute_Procedures_IUD(dsVesselComponents);
                        if (res)
                        {
                            MessageBox1.ShowMessage("Component added to vessel.", false);
                        }
                        else
                        {
                            MessageBox1.ShowMessage("Unable to add Component to vessel.Error :" + Common.getLastError(), true);
                        }
                    }
                }

                SelectedNodeParentId = 0;
 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fr", "refresh();", true);
            }
            else
            {
                MessageBox1.ShowMessage("Please select components.", true);
            }
        }
    }
    private void SaveStructureOnUncheck(string deletecompcode)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a Vessel.", true);
        }
        if (ddlComponentFirstLevel.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a Component.", true);
        }
        else
        {
                    try
                    {
                        Common.Set_Procedures("sp_Delete_Vessel_Components");
                        Common.Set_ParameterLength(2);
                        Common.Set_Parameters(
                            new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
                            new MyParameter("@ComponentCode", deletecompcode)
                            );
                        DataSet dsVesselComp = new DataSet();
                        dsVesselComp.Clear();
                        Boolean resu;
                        resu = Common.Execute_Procedures_IUD(dsVesselComp);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox1.ShowMessage("Unable to remove.Error :" + ex.Message + Common.getLastError(), true);
                    }
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fr", "refresh();", true);
        }
    }
    #endregion

    #region ---------------- Events -------------------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "td", "SetLastFocus('div_Search');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Componenttree');", true);
    }
    protected void rdoMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoMode.SelectedIndex == 0)
        {
            plOfficeMaster.Visible = true;
            plExcelTemp.Visible = false;
            plCopyVessel.Visible = false;            
        }
        if (rdoMode.SelectedIndex == 1)
        {
            plCopyVessel.Visible = true;
            plOfficeMaster.Visible = false;
            plExcelTemp.Visible = false;
            BindCopyVessels();
        }
    }
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            BindGroupsAndSystems();
        }
        else
        {
            tvComponents.Nodes.Clear();
        }
    }
    protected void ddlComponentFirstLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlComponentFirstLevel.SelectedIndex != 0)
        {
            string SQL = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + ddlComponentFirstLevel.SelectedValue + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            CompId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());
            tvComponents.Nodes.Clear();
            BindGroupsAndSystems();
            btnRemoveselectedComp.Visible = true;
        }
        else
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            btnRemoveselectedComp.Visible = false;
        }
    }
    protected void tvComponents_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Parent != null)
        {
            SelectedNodeParentId = Common.CastAsInt32(e.Node.Parent.Value.Trim());
        }

        if (e.Node.ChildNodes.Count > 0)
        {
            foreach (TreeNode cn in e.Node.ChildNodes)
            {
                cn.Checked = e.Node.Checked;
                if (cn.ChildNodes.Count > 0)
                {
                    foreach (TreeNode ccn in cn.ChildNodes)
                    {
                        ccn.Checked = cn.Checked;
                    }
                }
            }
        }
        string Code = getCode(e.Node.Text);
        //if (Code.Length == 4)
        //{
        //    foreach(TreeNode tn in e.Node.Parent.ChildNodes)
        //        if (tn.Checked) 
        //            e.Node.Parent.Checked=true;    
        //}
        if (Code.Length == 9)
        {
                foreach (TreeNode tn in e.Node.Parent.ChildNodes)
                if (tn.Checked)
                {
                    e.Node.Parent.Checked = true;
                    e.Node.Parent.Checked = true;
                }
        }
        if (Code.Length == 12)
        {
            foreach (TreeNode tn in e.Node.Parent.ChildNodes)
                if (tn.Checked)
                {
                    e.Node.Parent.Checked = true;
                    e.Node.Parent.Parent.Checked = true;
                }
        }
        if (!e.Node.Checked)
        {
            SaveStructureOnUncheck(Code);
        }
        else
        {
            SaveStructure();
        }
    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        DataTable dtSubSystems;
        DataTable dtUnits;
        string strUnits = "";
        string sncode = e.Node.Text.ToString().Split(':').GetValue(0).ToString().Trim();
        string strSubSystems = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=" + (sncode.Trim().Length + 3) + " AND LEFT(ComponentCode," + sncode.Trim().Length + ")='" + sncode + "' Order By ComponentCode";        
        dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
        if (dtSubSystems != null)
        {
            for (int k = 0; k < dtSubSystems.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtSubSystems.Rows[k]["ComponentCode"].ToString() + " : " + dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Value = dtSubSystems.Rows[k]["ComponentId"].ToString();
                ssn.ToolTip = dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Expanded = false;
                if (sncode.Trim().Length == 6)
                {
                    string ssncode = ssn.Text.Split(':').GetValue(0).ToString().Trim();
                    strUnits = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))= 12 AND LEFT(ComponentCode, 9 )='" + ssncode.Trim() + "' Order By ComponentCode";
                    dtUnits = Common.Execute_Procedures_Select_ByQuery(strUnits);
                    if (dtUnits.Rows.Count > 0)
                    {
                        ssn.PopulateOnDemand = true;
                    }
                }
                e.Node.ChildNodes.Add(ssn);
            }
        }
        if (Request.QueryString["Mode"].ToString() == "EDIT")
        {
            if (e.Node.ChildNodes.Count > 0)
            {
                DataTable dtComponents = new DataTable();
                string strEditSQL = "SELECT ComponentId FROM ComponentMasterForVessel WHERE VesselCode='" + Request.QueryString["VC"].ToString() + "'";
                dtComponents = Common.Execute_Procedures_Select_ByQuery(strEditSQL);
                if (dtComponents.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtComponents.Rows)
                    {
                        foreach (TreeNode tn in e.Node.ChildNodes)
                        {
                            if (tn.Value == dr["ComponentId"].ToString())
                            {
                                tn.Checked = true;
                            }
                            
                        }
                    }
                }
            }
        }
    }
    private string getCode(string Code_Name)
    {
        string[] strComponent = Code_Name.Trim().Split(':');
        return strComponent[0].Trim();
    }
    private string getName(string Code_Name)
    {
        string[] strComponent = Code_Name.Trim().Split(':');
        return strComponent[1].Trim();
    }
    protected void btnPlOMSave_Click(object sender, EventArgs e)
    {
        SaveStructure();
    }
    protected void btnOpenStructure_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            Session["Vessel"] = ddlVessels.SelectedItem.Text;
            Session["VC"] = ddlVessels.SelectedValue.Trim();
          //Response.Redirect("VesselSetupMaster.aspx");
        }
        else
        {
            MessageBox1.ShowMessage("Please select a vessel.",true);
        }
    }
    protected void btnPlETSave_Click(object sender, EventArgs e)
    {
        if (fulExcelTemp.HasFile)
        {
            string strPath = "VesselExcelTemplate/";
            string sFile = ddlVessels.SelectedValue.ToString() + "_" + fulExcelTemp.FileName.ToString(); ;
            fulExcelTemp.SaveAs(Server.MapPath(strPath) + sFile);
        }
    }
    protected void btnRemoveselectedComp_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("sp_Delete_Vessel_Components");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", ddlVessels.SelectedValue.ToString().Trim()),
                new MyParameter("@ComponentCode", ddlComponentFirstLevel.SelectedValue.Trim())                
                );

            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to remove component.Error :" + Common.getLastError(), true);
        }

        //string strDelete = "DELETE FROM ComponentMasterForVessel WHERE ComponentId IN ( SELECT CMV.ComponentId FROM ComponentMasterForVessel CMV INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
        //                   "WHERE CMV.VesselCode ='" + ddlVessels.SelectedValue.ToString() + "' AND CM.ComponentCode LIKE '%" + ddlComponentFirstLevel.SelectedValue.Trim() + "%') AND VesselCode ='" + ddlVessels.SelectedValue.ToString() + "' ";

        //Common.Execute_Procedures_Select_ByQuery(strDelete);
        ddlComponentFirstLevel_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fr", "refresh();", true);

    }
    #region ------------------- Copy Vessel ------------------------------

    protected void btnPlCopyVessel_Click(object sender, EventArgs e)
    {
        if (ddlCopyVessel.SelectedIndex == 0)
        {
            MessageBox2.ShowMessage("Please select a vessel.", true);
            return;
        }

        Common.Set_Procedures("sp_CopyVessel");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@VesselCodeTo", ddlVessels.SelectedValue.Trim()),
            new MyParameter("@VesselCodeFrom", ddlCopyVessel.SelectedValue.Trim())
                                        );
        DataSet dsCopyVesselComponents = new DataSet();
        dsCopyVesselComponents.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsCopyVesselComponents);
        if (res)
        {
            //---------------------------------
            string sourceFolder = Server.MapPath("~/UploadFiles/" + ddlCopyVessel.SelectedValue);
            string DestFolder = Server.MapPath("~/UploadFiles/" + ddlVessels.SelectedValue);
            
            string[] FilesList;
            if (System.IO.Directory.Exists(DestFolder))
            {
                FilesList = System.IO.Directory.GetFiles(DestFolder);
                foreach (string Fl in FilesList)
                {
                    try
                    {
                        System.IO.File.Delete(Fl);
                    }
                    catch { }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(DestFolder);
            }
            //---------------------------------
            if (System.IO.Directory.Exists(sourceFolder))
            {
                FilesList = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string Fl in FilesList)
                {
                    string FileName = System.IO.Path.GetFileName(Fl);
                    System.IO.File.Copy(Fl, DestFolder + "/" + FileName, true);
                }
            }
            MessageBox2.ShowMessage("Vessel copied successfully.", false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fr", "refresh();", true);
            //---------------------------------
        }
        else
        {
            MessageBox2.ShowMessage("Unable to copy vessel.Error :" + Common.getLastError(), true);
        }
    }

    #endregion

    #region --------------- Search Components ---------------------------------
    protected void SearchComponent_Click(object sender, EventArgs e)
    {
        //string strSQL = "SELECT CM.* FROM ComponentMasterForVessel CMV INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId ";
        string strSQL = "SELECT * FROM ComponentMaster ";

        string WhereClause = "WHERE 1=1 ";
        if (txtCompCode.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND componentcode like '%" + txtCompCode.Text + "%'";

        }
        if (txtCopmpname.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND componentname like '%" + txtCopmpname.Text + "%'";
        }
        string strSearch = strSQL + WhereClause;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSearch);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                rptCompoenents.DataSource = dt;
                rptCompoenents.DataBind();
                if (dt.Rows.Count > 1)
                {
                    lblSearchRecords.Text = dt.Rows.Count + " Components Found.";
                }
                else
                {
                    lblSearchRecords.Text = dt.Rows.Count + " Component Found.";
                }
            }
            else
            {
                rptCompoenents.DataSource = null;
                rptCompoenents.DataBind();
                lblSearchRecords.Text = "No Component Found.";
            }
        tdSearchResult.Visible = true;
    }
    protected void btnComponent_Click(object sender, EventArgs e)
    {
        SelectedCompCode = ((LinkButton)sender).CommandArgument;
        string ParentCode = SelectedCompCode.Substring(0, 3);        
        ddlComponentFirstLevel.SelectedValue = ParentCode + "         ";
        ddlComponentFirstLevel_SelectedIndexChanged(sender, e);
        SelectSearchedComponent();
        SearchComponent_Click(sender, e);
        SaveStructure();
    }
    public void SelectSearchedComponent()
    {
        DataTable dtComponents = new DataTable();
        string strSQL = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode='" + SelectedCompCode + "'";
        dtComponents = Common.Execute_Procedures_Select_ByQuery(strSQL);
        string CompId = dtComponents.Rows[0][0].ToString();
        if (SelectedCompCode.Trim().Length == 6)
        {
            foreach (TreeNode tn in tvComponents.Nodes)
            {
                if (tn.Value.Trim() == CompId)
                {
                    tn.Checked = true;
                    if (tn.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tnc in tn.ChildNodes)
                        {
                            tnc.Checked = true;
                            tn.Expand();
                            if (tnc.ChildNodes.Count > 0)
                            {
                                foreach (TreeNode tncc in tnc.ChildNodes)
                                {
                                    tncc.Checked = true;
                                }
                                tnc.Expand();
                            }
                        }
                    }
                    return;
                }
            }
        }
        else if (SelectedCompCode.Trim().Length == 9)
        {
            foreach (TreeNode tn in tvComponents.Nodes)
            {
                if (tn.Text.Split(':').GetValue(0).ToString().Substring(0, 6) == SelectedCompCode.Substring(0, 6))
                {
                    if (tn.ChildNodes.Count > 0)
                    {                        
                        foreach (TreeNode tnc in tn.ChildNodes)
                        {
                            if (tnc.Value.Trim() == CompId)
                            {
                                tn.Checked = true;
                                if (tnc.ChildNodes.Count > 0)
                                {
                                    foreach (TreeNode tncc in tnc.ChildNodes)
                                    {
                                        tncc.Checked = true;
                                    }
                                    tnc.Expand();
                                }
                                tnc.Checked = true;
                                tn.Expand();
                                return;
                            }
                        }
                        
                    }
                }
            }
        }
        else if (SelectedCompCode.Trim().Length == 12)
        {
            foreach (TreeNode tn in tvComponents.Nodes)
            {
                if (tn.Text.Split(':').GetValue(0).ToString().Substring(0, 6) == SelectedCompCode.Substring(0, 6))
                {
                    if (tn.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tnc in tn.ChildNodes)
                        {
                            if (tnc.Text.Split(':').GetValue(0).ToString().Substring(0, 9) == SelectedCompCode.Substring(0, 9))
                            {
                                if (tnc.ChildNodes.Count > 0)
                                {
                                    foreach (TreeNode tncc in tnc.ChildNodes)
                                    {
                                        if (tncc.Value.Trim() == CompId)
                                        {
                                            tn.Checked = true;
                                            tnc.Checked = true;
                                            tncc.Checked = true;
                                            tn.Expand();
                                            tnc.Expand();
                                            return;
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
    #endregion ----------------------------------------------------------------
    #endregion
    
}
