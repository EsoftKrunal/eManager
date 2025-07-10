using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class ReqFromVessels : System.Web.UI.Page
{
    //public AuthenticationManager auth = new AuthenticationManager(0,0,ObjectType.Page);
            
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    //------------------------------
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1061, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }

            AuthenticationManager authPR = new AuthenticationManager(1061, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            ancAddReq.Visible = authPR.IsAdd;

            AuthenticationManager authRFQList = new AuthenticationManager(1061, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            btnCreateRFQ.Visible = authRFQList.IsView;

        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        lblMsg.Text = "";
        //this.Form.DefaultButton = btnSearch.UniqueID;
        if (!Page.IsPostBack)
        {
            BindVesselDropdown();
            //  BindStatus();
            BindPOStatus();
            BindPrType();
            //txtFromDate.Text = new DateTime(DateTime.Today.Year, 1, 1).ToString("dd-MMM-yyyy");  //DateTime.Today.AddDays(-20).ToString("dd-MMM-yyyy");  //
            //txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            LoadSession(); 
            BindRPRepeater();
        }
        if (Session["NWC"] != null)
        {
            chkNWC.Visible = (Session["NWC"].ToString() == "Y");
        }
        else
        {
            chkNWC.Visible = false;
        }
        
    }

    private void LoadSession()
    {
        string[] Delemeters = { SessionDeleimeter}; 
        string values = "" + Session["ReqFromVessel"];
        string[] ValueList = values.Split(Delemeters,StringSplitOptions.None);
        try
        {
            ChkAllVess.Checked = ValueList[0] == "Y";
            ddlVessel.SelectedValue = ValueList[1];
            ddlPRType.SelectedValue = ValueList[2];
            txtPRNumber.Text = ValueList[3];
            ddlStatus.SelectedValue = ValueList[4];
            txtFromDate.Text = ValueList[5];
            txtToDate.Text = ValueList[6];
            SelectedPoId = int.Parse("0" + ValueList[7]);
        }catch { } 
    }
    private void WriteSession()
    {
        string values = ((ChkAllVess.Checked) ? "Y" : "N") + SessionDeleimeter + 
                        ddlVessel.SelectedValue + SessionDeleimeter +
                        ddlPRType.SelectedValue + SessionDeleimeter +
                        txtPRNumber.Text + SessionDeleimeter +
                        ddlStatus.SelectedValue  + SessionDeleimeter +
                        txtFromDate.Text+ SessionDeleimeter +
                        txtToDate.Text + SessionDeleimeter + 
                        SelectedPoId;
        Session["ReqFromVessel"] = values; 
    }
    private void ClearSession()
    {
        Session["ReqFromVessel"] = null;  
    }
    #endregion
    //------------------------------
    #region -------- Page Control Loader ------------
    public void BindStatus()
    {
         try
        {
            string sql = "Select -1 As StatusID, 'Select' As StatusName UNION select StatusID,StatusName from sql_tblSMDPRStatusCodes where 1=1 and StatusID in (0,15,20,25,30,60,75,80,85,200) ";
            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@Query", sql));
            DataSet dsPrType = new DataSet();
            dsPrType.Clear();
            dsPrType = Common.Execute_Procedures_Select();

            ddlStatus.DataSource = dsPrType;
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();
            //ddlStatus.Items.Insert(-1, new ListItem("Select","999"));
//            ddlStatus.Items.Add(new ListItem("Ready For Place Order", "998"));          
           // ddlStatus.Items.Add(new ListItem("Order Placed", "60"));           
           //// ddlStatus.Items.Add(new ListItem("Receipt In Progress", "75"));
           // ddlStatus.Items.Add(new ListItem("Order Received", "80"));
           // ddlStatus.Items.Add(new ListItem("Invoice Received", "85"));
           // ddlStatus.Items.Add(new ListItem("Cancelled PR", "200"));
            ddlStatus.SelectedValue = "0";
        }
        catch { }
    }

    public void BindPOStatus()
    {
        try
        {
            string sql = "select StatusID,StatusName from sql_tblSMDPRStatusCodes with(nolock) where 1=1  ";
            if (ddlReqSupplyStatus.SelectedValue == "1" )
            {
                sql += " AND StatusID in (0,15,20,25,30,60,75)";
            }
            else if (ddlReqSupplyStatus.SelectedValue == "2")
            {
                sql += " AND StatusID in (80,85)";
            }
            else if (ddlReqSupplyStatus.SelectedValue == "3")
            {
                sql += " AND StatusID in (200)";
            }
            else
            {
                sql += " AND StatusID in (0,15,20,25,30,60,75,80,85,200)";
            }
            Common.Set_Procedures("ExecQuery");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@Query", sql));
            DataSet dsPrType = new DataSet();
            dsPrType.Clear();
            dsPrType = Common.Execute_Procedures_Select();

            ddlStatus.DataSource = dsPrType;
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select", "999"));
            //            ddlStatus.Items.Add(new ListItem("Ready For Place Order", "998"));          
            //ddlStatus.Items.Add(new ListItem("Order Placed", "60"));
            //// ddlStatus.Items.Add(new ListItem("Receipt In Progress", "75"));
            //ddlStatus.Items.Add(new ListItem("Order Received", "80"));
            //ddlStatus.Items.Add(new ListItem("Invoice Received", "85"));
            //ddlStatus.Items.Add(new ListItem("Cancelled PR", "200"));
            if (ddlReqSupplyStatus.SelectedValue != "2" || ddlReqSupplyStatus.SelectedValue != "3")
            {
                ddlStatus.SelectedValue = "0";
            }

        }
        catch { }
    }

    protected void BindVesselDropdown()
    {
        string sql;
        if (ChkAllVess.Checked)
        {
            sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw where vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID";
        }
        else
        {
            sql = "SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE vw.Active='A' and vw.VesselStatusId <> 2 and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY active desc,vw.ShipID";
        }
      
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsVessel = new DataSet();
        dsVessel.Clear();
        dsVessel = Common.Execute_Procedures_Select();
        ddlVessel.DataSource = dsVessel;
        ddlVessel.DataTextField = "ShipName";
        ddlVessel.DataValueField = "ShipID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("--ALL--", "0"));
    }
    public void BindPrType()
    {
        string sql = "select * from vw_sql_tblSMDPRTypes";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlPRType.DataSource = dsPrType;
        ddlPRType.DataTextField = "PrtypeDesc";
        ddlPRType.DataValueField = "prtype";
        ddlPRType.DataBind();
        ddlPRType.Items.Insert(0, new ListItem("Select", "0")); ;
        ddlPRType.SelectedIndex = 0;
    }
    #endregion    
    //------------------------------
    protected void BindRPRepeaterData(bool Excel)
    {
        WriteSession();
        //string sql = "select *,(case when statusid=200 then (select cancelreason from dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.poid=VM.poid) else convert(varchar,pocomments) + 'Comments :' + convert(varchar,remarkssmd) end) as PoComments2,Replace(Convert(varchar,ADT.RECEIVEDDATE,106),' ','-') as CreatedDate,isnull(bidsSent,0)as TotbidsSent,isnull(bidsRecd,0)as totbidsRecd,(select AccountNumber from VW_sql_tblSMDPRAccounts AC where AC.accountID=VM.accountID)AccountNumber,(select AccountName from VW_sql_tblSMDPRAccounts AC where AC.accountID=VM.accountID)AccountName,(select deptName from VW_sql_tblSMDPRDept dpt where dpt.dept=VM.dept )deptName  from VW_tblSMDPOMasterData VM left join add_tblsmdpomaster adt on adt.poid=VM.POID ";

        string sql = "select *,adt.officecomments,(case when statusid=200 then (select cancelreason from dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.poid=VM.poid) when prtype=2 then ISNULL(EQUIPNAME,'') + ' | ' + ISNULL(EquipMfg,'') + ' | ' + ISNULL(EquipModel,'')+ ' | ' + ISNULL(EquipSerialNo,'') else '' + convert(varchar,VM.remarkssmd) end) as PoComments2, Case when LEN((case when statusid=200 then (select cancelreason from dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.poid=VM.poid) when prtype=2 then ISNULL(EQUIPNAME,'') + ' | ' + ISNULL(EquipMfg,'') + ' | ' + ISNULL(EquipModel,'')+ ' | ' + ISNULL(EquipSerialNo,'') else '' + convert(varchar,VM.remarkssmd) end))> 50 then LEFT((case when statusid=200 then (select cancelreason from dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.poid=VM.poid) when prtype=2 then ISNULL(EQUIPNAME,'') + ' | ' + ISNULL(EquipMfg,'') + ' | ' + ISNULL(EquipModel,'')+ ' | ' + ISNULL(EquipSerialNo,'') else '' + convert(varchar,VM.remarkssmd) end),50)+'...'  ELSE (case when statusid=200 then (select cancelreason from dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.poid=VM.poid) when prtype=2 then ISNULL(EQUIPNAME,'') + ' | ' + ISNULL(EquipMfg,'') + ' | ' + ISNULL(EquipModel,'')+ ' | ' + ISNULL(EquipSerialNo,'') else '' + convert(varchar,VM.remarkssmd) end) END as PoCommentShort,Replace(Convert(varchar,ADT.RECEIVEDDATE,106),' ','-') as CreatedDate,isnull(bidsSent,0)as TotbidsSent,isnull(bidsRecd,0)as totbidsRecd,(select AccountNumber from VW_sql_tblSMDPRAccounts AC where AC.accountID=VM.accountID)AccountNumber,(select cast(AccountNumber As Varchar(5)) +' - '+ AccountName As AccountName from VW_sql_tblSMDPRAccounts AC where AC.accountID=VM.accountID)AccountName,(select deptName from VW_sql_tblSMDPRDept dpt where dpt.dept=VM.dept )deptName, case when VM.StatusID in (0,15,20,25,30,60,75) then 'Supply In-Progress' when VM.StatusID in (80,85) then 'Supply Completed' when VM.StatusID in (200) then 'Supply Cancelled' ELSE '' END As SupplyStatus , dbo.GetRequisitionFlags (VM.IsUrgent,VM.IsFastTrack,VM.IsSafetyUrgent) As Criticality from VW_tblSMDPOMasterData VM left join add_tblsmdpomaster adt on adt.poid=VM.POID ";
        string WhereClause = "";
        if (ddlVessel.SelectedValue != "0")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and ShipID='" + ddlVessel.SelectedValue + "'";
            }
            else
            {
                WhereClause = " where ShipID='" + ddlVessel.SelectedValue + "'";
            }
        }
        else /// code for nwc vessels
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and ShipID IN (Select v.VesselCode from UserVesselRelation uv with(nolock) inner join Vessel v with(nolock) on uv.VesselId = v.VesselId where uv.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ";
            }
            else
            {
                WhereClause = " where ShipID IN (Select v.VesselCode from UserVesselRelation uv with(nolock) inner join Vessel v with(nolock) on uv.VesselId = v.VesselId where uv.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ";
            }
            
           
        }

        if (ddlPRType.SelectedIndex != 0)
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and prtype=" + ddlPRType.SelectedValue;
            }
            else
            {
                WhereClause = " where prtype=" + ddlPRType.SelectedValue;
            }
        }
        if (txtPRNumber.Text.Trim() != "")
        {
            Int16 test = 0;
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and ( VM.prnum like '%" + txtPRNumber.Text.Trim() + "%' or isnull(VM.ReqNo,'') like '%" + txtPRNumber.Text.Trim() + "%') ";
                //if (txtPRNumber.Text.Trim().Length == 4 && Int16.TryParse(txtPRNumber.Text.Trim(), out test))
                //{
                //    WhereClause = WhereClause + " and prnum=" + txtPRNumber.Text.Trim();
                //}
                //else
                //{
                //    WhereClause = WhereClause + " and isnull(ReqNo,'')='" + txtPRNumber.Text.Trim() + "'";
                //}
            }
            else
            {
                WhereClause = WhereClause + " where ( VM.prnum like '%" + txtPRNumber.Text.Trim() + "%' or isnull(VM.ReqNo,'') like '%" + txtPRNumber.Text.Trim() + "%') ";
                //if (txtPRNumber.Text.Trim().Length == 4 && Int16.TryParse(txtPRNumber.Text.Trim(),out test))
                //{
                //    WhereClause = " where prnum=" + txtPRNumber.Text.Trim();
                //}
                //else
                //{
                //    WhereClause = " where isnull(ReqNo,'')='" + txtPRNumber.Text.Trim() + "'";
                //}
            }
        }

        //- STATUS ------------
        if (WhereClause != "")
        {
            if (ddlStatus.SelectedValue == "999") // all
            {
                //WhereClause = WhereClause + " and StatusID in (0,1,15) ";
            }
            else
            {
                WhereClause = WhereClause + " and VM.StatusID=" + ddlStatus.SelectedValue;
            }
        }
        else
        {
            if (ddlStatus.SelectedValue == "999") // all
            {
                //WhereClause = WhereClause + " where StatusID in (0,1,15) ";
            }
            else
            {
                WhereClause = " where VM.StatusID=" + ddlStatus.SelectedValue;
            }
        }

        if (ddlReqSupplyStatus.SelectedIndex > 0)
        {
           if (Convert.ToInt32(ddlReqSupplyStatus.SelectedValue) == 1)
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VM.StatusID in (0,15,20,25,30,60,75) ";
                }
                else
                {

                    WhereClause = " where VM.StatusID in (0,15,20,25,30,60,75) ";

                }
            }
           else if (Convert.ToInt32(ddlReqSupplyStatus.SelectedValue) == 2)
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VM.StatusID in (80,85) ";
                }
                else
                {

                    WhereClause = " where VM.StatusID in (80,85) ";

                }
            }
           else if (Convert.ToInt32(ddlReqSupplyStatus.SelectedValue) == 3)
            {
                if (WhereClause != "")
                {
                    WhereClause = WhereClause + " and VM.StatusID = 200 ";
                }
                else
                {

                    WhereClause = " where VM.StatusID = 200 ";

                }
            }
        }
        //--------------        
        if (txtFromDate.Text.Trim() != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and convert(datetime,ADT.RECEIVEDDATE,103) >= convert(datetime,'" + txtFromDate.Text.Trim() + "',101)";
            }
            else
            {
                WhereClause = " where convert(datetime,ADT.RECEIVEDDATE,103) >= convert(datetime,'" + txtFromDate.Text.Trim() + "',101)";
            }
        }
        if (txtToDate.Text.Trim() != "")
        {
            if (WhereClause != "")
            {
                WhereClause = WhereClause + " and convert(datetime,ADT.RECEIVEDDATE,103) <convert(datetime,dateadd(dd, 1,'" + txtToDate.Text.Trim() + "'),101)";
            }
            else
            {
                WhereClause = " where convert(datetime,ADT.RECEIVEDDATE,103) < convert(datetime,dateadd(dd, 1,'" + txtToDate.Text.Trim() + "'),101)";
            }
        }

        if (ddlCriticality.SelectedValue != "0")
        {
            if (WhereClause != "")
            {
                if (ddlCriticality.SelectedValue == "U")
                {
                    WhereClause = WhereClause + " AND VM.IsUrgent = 1 ";
                }
                else if (ddlCriticality.SelectedValue == "FT")
                {
                    WhereClause = WhereClause + " AND VM.IsFastTrack = 1 ";
                }
                else if (ddlCriticality.SelectedValue == "SU")
                {
                    WhereClause = WhereClause + " AND VM.IsSafetyUrgent = 1 ";
                }
            }
            else
            {
                if (ddlCriticality.SelectedValue == "U")
                {
                    WhereClause = " WHERE VM.IsUrgent = 1 ";
                }
                else if (ddlCriticality.SelectedValue == "FT")
                {
                    WhereClause = " WHERE VM.IsFastTrack = 1 ";
                }
                else if (ddlCriticality.SelectedValue == "SU")
                {
                    WhereClause = " WHERE VM.IsSafetyUrgent = 1 ";
                }
            }
        }

        if (WhereClause != "")
        {
            WhereClause = WhereClause + " and VM.prtype is not null";
        }
        else
        {
            WhereClause = " where VM.prtype is not null";
        }
        sql = sql + WhereClause;
        sql = sql + " order by VM.Poid desc";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
//Response.Write(sql);
//Response.End();

        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        if (dsPrType != null)
        {
            if (dsPrType.Tables[0].Rows.Count != 0)
            {
                RptPRMaster.DataSource = dsPrType;
                RptPRMaster.DataBind();
                if (Excel)
                {

                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("Vessel");
                   // dtNew.Columns.Add("Type");
                    dtNew.Columns.Add("Vsl Req No");
                    dtNew.Columns.Add("Off Req No");
                    dtNew.Columns.Add("Requisition Title");
                    dtNew.Columns.Add("Department");
                    dtNew.Columns.Add("Recieved Date");
                    dtNew.Columns.Add("Status");
                    dtNew.Columns.Add("AccountNo");
                    dtNew.Columns.Add("Ship Comments");
                    dtNew.Columns.Add("Office Comment");
                    foreach (DataRow dr in dsPrType.Tables[0].Rows)
                    {
                        dtNew.Rows.Add(dtNew.NewRow());
                        dtNew.Rows[dtNew.Rows.Count - 1]["Vessel"] = dr["ShipID"].ToString();
                       // dtNew.Rows[dtNew.Rows.Count - 1]["Type"] = dr["PRTypeCode"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Vsl Req No"] = dr["reqNo"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Off Req No"] = dr["prnum"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Requisition Title"] = dr["RequisitionTitle"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Department"] = dr["deptName"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Recieved Date"] = dr["CreatedDate"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Status"] = dr["StatusName"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["AccountNo"] = dr["AccountNumber"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Ship Comments"] = dr["PoComments2"].ToString();
                        dtNew.Rows[dtNew.Rows.Count - 1]["Office Comment"] = dr["officecomments"].ToString();
                    }
                    ECommon.ExportDatatable(Response, dtNew,"RFQ");             
                }
            }
            else
            {
                //show msg
                RptPRMaster.DataSource = dsPrType;
                RptPRMaster.DataBind();
                lblMsg.Text = "No Data Found with this selection";
            }
            lblRowCount.Text = "( " + RptPRMaster.Items.Count.ToString() + " ) records found.";

        }
    }
    protected void BindRPRepeater()
    {
        BindRPRepeaterData(false);
    }
    public string getStatusColor(string StatusText)
    {
        switch (StatusText)
        {
            case "RFQ Activity" :
                return "Blue"; 
                break ;
            case "Ready for Approval":
                return "Green";
                break;
            case "Order Placed" :
                return "Black";
                break;
            default:
                return "Red";
                break;
        }
    }
    //------------------------------
    protected void ChkAllVess_CheckedChanged(object sender, EventArgs e)
    {
        //ddlVessel.IncludeInActive = ChkAllVess.Checked;
        BindVesselDropdown();
    }
    protected void ChkNWC_CheckedChanged(object sender, EventArgs e)
    {
        //ddlVessel.IncludeNWC = chkNWC.Checked;   
    }
    
    protected void btnPost_OnClick(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
    //------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRPRepeater();
    }
    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        BindRPRepeaterData(true);
    }
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");   
    }
    protected void btnCancelPRCancel_Click(object sender, EventArgs e)
    {
        dvConfirmCancel.Visible = false ;  
    }
    protected void btnCancelPRSubmit_Click(object sender, EventArgs e)
    {
        Button imgBtn = (Button)sender;
        int PRId = int.Parse(imgBtn.CommandArgument);
        Common.Set_Procedures("sp_NewPR_CancelPR");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@PRId", PRId), new MyParameter("@UserName", Session["FullName"]), new MyParameter("@Reason", txtReason.Text));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            if (Common.CastAsInt32(ds.Tables[0].Rows[0][0]) == 1)
            {
                lblPopError.Text = "Unable to cancel Purchase Request(PO Approved).";
            }
            else
            {
                dvConfirmCancel.Visible = false;  
                BindRPRepeater();
                txtReason.Text = "";  
                lblMsg.Text = "PR cancelled successfully.";
            }
        }
        else
        {
            lblPopError.Text = "Unable to cancel Purchase Request. Error : " + Common.ErrMsg;
        }
    }
    protected void btnCancelPR_Click(object sender, EventArgs e)
    {
        //Button imgBtn = (Button)sender;
        int PRId = int.Parse(SelectedPoId.ToString());
        dvConfirmCancel.Visible = true;
        btnSet.CommandArgument = PRId.ToString();      
    }
    //------------------------------
    protected void btnViewPR_Click(object sender, ImageClickEventArgs e)
    {
        if (SelectedPoId > 0)
        {
            WriteSession();
            Response.Redirect("AddRequisition.aspx?PRID=" + SelectedPoId.ToString()+"&ViewPage=true");
        }
    }
    protected void btnCreateRFQ_Click(object sender, EventArgs e)
    {
        if (SelectedPoId > 0)
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select isnull(dept,'') as dept,accountid,MidCatId from dbo.vw_tblsmdpomaster where poid=" + SelectedPoId);

            if (dt.Rows[0]["MidCatId"].ToString().Trim() != "" && Common.CastAsInt32(dt.Rows[0]["accountid"]) > 0)
            {
                WriteSession();
                Response.Redirect("CreateRFQ.aspx?PRID=" + SelectedPoId.ToString());
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Please update department and accountid for this Requisition.');", true);
            }
        }
        else
        {
            BindRPRepeater();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ChkAllVess.Checked = false;
        ddlVessel.SelectedIndex = 0; 
        ddlPRType.SelectedIndex = 0;
        txtPRNumber.Text = "";
        ddlStatus.SelectedIndex = 0;
        ddlReqSupplyStatus.SelectedIndex = 0;
        txtFromDate.Text = DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
        txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        ClearSession(); 
        BindRPRepeater(); 
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RFQ');", true);
    }
    
    
    //------------------------------


    protected void ancAddReq_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/AddRequisition.aspx");
    }

    protected void ddlReqSupplyStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPOStatus();
        }
        catch(Exception ex)
        {

        }
    }
}

   