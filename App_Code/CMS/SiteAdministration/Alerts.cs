using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ShipSoft.CrewManager.BusinessObjects;
public class Alerts
{
    public static void SetHelp(HtmlImage imgCtl)
    {
        imgCtl.Attributes.Add("onclick", "javascript:window.open('" + ConfigurationManager.AppSettings["ManualLink"] + "Module_" + imgCtl.Attributes["moduleid"].ToString() + ".pdf')");
        imgCtl.Visible = true;  
    }
    public static DataTable getCRMAlert()
    {
        string procedurename = "get_CRMAlert";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable getArchivedDocuments( int crewid)
    {
        //DataTable dt = new DataTable(); 
        //return dt;
        return Common.Execute_Procedures_Select_ByQueryCMS("select * from dbo.CrewArchivedDocuments where crewid=" + crewid.ToString());
    }
    public static bool ArchiveDocument(int CrewId, string MainCat, string SubCat, string DocName ,string DocNo, string IssueDate, string Expdate, string FileName, Byte[] Data, int ArchivedBy)
    {
        object d1, d2;

        if (IssueDate.Trim() == "")
            d1 = DBNull.Value;
        else
            d1 = Convert.ToDateTime(IssueDate);

        if (Expdate.Trim() == "")
            d2 = DBNull.Value;
        else
            d2 = Convert.ToDateTime(Expdate);


        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("CrewDocuments_Archive");

        oDatabase.AddInParameter(odbCommand, "@CREWID", DbType.Int32, CrewId);
        oDatabase.AddInParameter(odbCommand, "@MAINCATEGORY", DbType.String, MainCat);
        oDatabase.AddInParameter(odbCommand, "@SUBCATEGORY", DbType.String, SubCat);
        oDatabase.AddInParameter(odbCommand, "@DocumentName", DbType.String, DocName);
        oDatabase.AddInParameter(odbCommand, "@DOCUMENTNUMBER", DbType.String, DocNo);

        oDatabase.AddInParameter(odbCommand, "@ISSUEDATE", DbType.DateTime, d1);
        oDatabase.AddInParameter(odbCommand, "@EXPIRYDATE", DbType.DateTime, d2);
        oDatabase.AddInParameter(odbCommand, "@FILENAME", DbType.String, FileName);
        oDatabase.AddInParameter(odbCommand, "@ATTACHMENT", DbType.Binary, Data);

        oDatabase.AddInParameter(odbCommand, "@ArchivedBy", DbType.Int32, ArchivedBy);

        bool result = false; 
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
                result = true;
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                result = false; 
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
        return result;


        //Common.Set_Procedures("CrewDocuments_Archive");
        //Common.Set_ParameterLength(9);
        //Common.Set_Parameters(
        //    new MyParameter("@CREWID", CrewId),
        //    new MyParameter("@MAINCATEGORY",MainCat),
        //    new MyParameter("@SUBCATEGORY",SubCat),
        //    new MyParameter("@DOCUMENTNUMBER", DocNo),
        //    new MyParameter("@ISSUEDATE", d1) , 
        //    new MyParameter("@EXPIRYDATE", d2),
        //    new MyParameter("@FILENAME", FileName),
        //    new MyParameter("@ATTACHMENT", Data), 
        //    new MyParameter("@ArchivedBy",ArchivedBy) 
        //    );

        //DataSet ds=new DataSet(); 

        //return Common.Execute_Procedures_IUD(ds);
    }
    
    public static int isAutorized(int LoginId, int ModuleId)
    {
        string procedurename = "isAutorized";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
        objDatabase.AddInParameter(objDbCommand, "@ApplicationModuleId", DbType.Int32, ModuleId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }
        return Convert.ToInt32(dt4.Rows[0][0].ToString());
    }
    public static int getRoleId(int LoginId)
    {
        string procedurename = "isAdmin";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }
        return Convert.ToInt32(dt4.Rows[0][0].ToString());
    }
    public static DataTable get_DocumentAlert()
    {
        string procedurename = "get_DocumentAlert";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable getSignOffCrewAlert()
    {
        string procedurename = "get_SignOffCrewAlert";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable getVesselManningAlert()
    {
        string procedurename = "get_VesselManningAlert";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
    public static DataTable get_Invoice_Status(int InvoiceId)
    {
        string procedurename = "getInvoiceStatus_UserName";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InvoiceId", DbType.Int32, InvoiceId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
    public static string Set_DDL_Value(DropDownList ddl, string Value, string Mess)
    {
        if (Value == null)
        {
            ddl.SelectedIndex = 0;
            return "";
        }
        if (Value.Trim() != "")
        {
            if (ddl.Items.IndexOf(ddl.Items.FindByValue(Value)) < 0)
            {
                ddl.SelectedIndex = 0;
                return ", " + Mess;
            }
            else
            {
                ddl.SelectedValue = Value;
                return "";
            }
        }
        else
        {
            ddl.SelectedIndex = 0;
            return "";
        }
    }
    public static void InsertCrewPassportNo(int _CrewId, string PPno, int LoginId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertPassportDetails");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _CrewId);
        oDatabase.AddInParameter(odbCommand, "@PPNo", DbType.String, PPno);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.String, LoginId);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }
    public static void Update_AvailableDate(int _CrewId, DateTime dt,string Rem, int LoginId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Update_AvailableDate");
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _CrewId);
        oDatabase.AddInParameter(odbCommand, "@AvailableDate", DbType.DateTime, dt);
        oDatabase.AddInParameter(odbCommand, "@Remark", DbType.String, Rem);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, LoginId);
        
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }
    public static int Update_ContractDetails(int ContractId, string ID, string SD, int WageScale, string OtherAmt, int sta, string Rem)
    {
        DataTable dt4= new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Update_ContractDetails");
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, ContractId);
        oDatabase.AddInParameter(odbCommand, "@IssueDate", DbType.DateTime, ID);
        oDatabase.AddInParameter(odbCommand, "@StartDate", DbType.DateTime, SD);
        oDatabase.AddInParameter(odbCommand, "@WageScale", DbType.Int32, WageScale);
        oDatabase.AddInParameter(odbCommand, "@OtherAmount", DbType.Double, OtherAmt);
        oDatabase.AddInParameter(odbCommand, "@STA", DbType.Int32, sta);
        oDatabase.AddInParameter(odbCommand, "@Remark", DbType.String, Rem);
        
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt4.Load(dr);
        }
        return Convert.ToInt32(dt4.Rows[0][0].ToString());
        
    }
    //=============== REGISTER PAGES
    public static void HANDLE_AUTHORITY(int Action, Button Add, Button Save, Button Cancel, Button Print,Authority Auth)
    {
        switch (Action)
        {
            case 1:  // case when Page Load 
                Add.Visible = Auth.isAdd;
                Save.Visible = false;
                Cancel.Visible = false;
                Print.Visible = false;
                break;
            case 2:  // case when Add Click
                Add.Visible = false;
                Save.Visible = true;
                Cancel.Visible = true;
                Print.Visible = false;
                break;
            case 3:  // case when Save Click
                Add.Visible = Auth.isAdd;
                Save.Visible = false;
                Cancel.Visible = false;
                Print.Visible = false;
                break;
            case 4:  // case when View Click
                Add.Visible = Auth.isAdd;
                Save.Visible = false;
                Cancel.Visible = true;
                Print.Visible = Auth.isPrint;
                break;
            case 5:  // case when Edit Click
                Add.Visible = Auth.isAdd;
                Save.Visible = true;
                Cancel.Visible = true;
                Print.Visible = Auth.isPrint;
                break;
            // Transactions
            case 6: // Search Screen
                Add.Enabled = Auth.isAdd;
                Print.Enabled = Auth.isPrint;
                break;
            case 7: // Planning Screen (Page Load)
                Add.Enabled = Auth.isEdit;
                Save.Enabled = Auth.isEdit;
                Print.Enabled = Auth.isPrint;
                break;
            case 8: // Travel Schedule Screen
                Save.Enabled = Auth.isEdit;
                Print.Enabled = Auth.isPrint;
                break;
            case 9: // Observation Screen
                Add.Enabled = Auth.isEdit;
                Save.Enabled = Auth.isEdit;
                Cancel.Enabled = Auth.isEdit;
                Print.Enabled = Auth.isEdit;
                break;
            case 10: // Response Screen
                Add.Enabled = Auth.isAdd;
                Save.Enabled = Auth.isEdit;
                Cancel.Enabled = Auth.isDelete;
                Print.Enabled = Auth.isPrint;
                break;
            case 11: // Inspection Closer Screen
                Add.Enabled = Auth.isEdit;
                break;
            case 12: // Create Reports
                Add.Enabled = Auth.isAdd;
                Save.Enabled = Auth.isEdit;
                Cancel.Enabled = Auth.isEdit;
                Print.Enabled = Auth.isEdit;
                break;
            case 13: // Create Reports
                Cancel.Enabled = Auth.isDelete;
                break;
            case 14: // Vessel Certificates
                Add.Enabled = Auth.isAdd;
                Save.Enabled = Auth.isEdit;
                Cancel.Enabled = Auth.isEdit;
                Print.Enabled = Auth.isEdit;
                break;
            case 15: // For Revenue Contract save    
                Save.Enabled = Auth.isEdit;
                Cancel.Visible = true;
                break;
            default: // cancel
                Add.Visible = Auth.isAdd;
                Save.Visible = false;
                Cancel.Visible = false;
                Print.Visible = false;
                break;
        }

    }
    public static void ShowPanel(Panel p1)
    {
        p1.Visible = true;
    }
    public static void HidePanel(Panel p1)
    {
        p1.Visible = false;
    }
    public static void HANDLE_GRID(GridView gv, Authority ob)
    {
        try
        {
            gv.Columns[1].Visible = ob.isEdit;
            gv.Columns[2].Visible = ob.isDelete;
        }
        catch
        {
            gv.Columns[1].Visible = false;
            gv.Columns[1].Visible = ob.isDelete;
        }
    }
    //=============== REPORT PRINTINGAUTHORITY
    public static bool Check_ReportPrintingAuthority(int _loginid,int _pageid)
    {
        DataTable dt11 = new DataTable();
        dt11.Columns.Add("IsPrint");
        dt11.Rows.Add(dt11.NewRow());
        AuthenticationManager am = new AuthenticationManager(_pageid, _loginid, ObjectType.Page);
        return am.IsPrint; 
    }
    //=============== Critical Remark
    public static DataTable getCriticalRemarkofCrewMember(string CrewId)
    {
        string procedurename = "get_CriticalRemarkforCrewMember";
        DataTable dt34 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.String, CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt34.Load(dr);
        }
        return dt34;

    }
    //=============== CRM Alert
    public static DataTable getCRMAlertofCrewMember(string CrewId)
    {
        string procedurename = "get_CRMAlertforCrewMember";
        DataTable dt66 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.String, CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt66.Load(dr);
        }
        return dt66;

    }
    //=============== Document Alert
    public static DataTable getDocumentAlertofCrewMember(string CrewId)
    {
        string procedurename = "get_DocumentAlertforCrewMember";
        DataTable dt77 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.String, CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt77.Load(dr);
        }
        return dt77;

    }
    //=============== INVALID CHARACTERS 
    public static Boolean Check_String_OK(string s)
    {
        bool res;
        res = true;
        int a;
        char[] c={'<','>','?','^'};
        for (int i = 0; i < c.Length - 1; i++)
        {
            if (s.IndexOf(c[i])  >= 0)
            {
                res = false;
                break;
            }
        }
        return res;
    }

    public static DataTable GetCrewIdFromPortCallId(int _portcallid)
    {
        string procedurename = "sp_TransferStatus_Get_CrewId_FromPortCallId";
        DataTable dt68 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _portcallid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt68.Load(dr);
        }
        return dt68;

    }

    public static string Check_BranchId(int _CrewId)
    {
        int recruitingoffice;
        string crewnumber;
        int Branch_Id = Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"].ToString());
        if (Branch_Id == 1)
        {
            DataTable dt99 = new DataTable();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand odbCommand = oDatabase.GetStoredProcCommand("sp_TransferStatus_Check_Branch_Id");
            oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _CrewId);
            using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
            {
                dt99.Load(dr);
                recruitingoffice = Convert.ToInt32(dt99.Rows[0]["RecruitmentOfficeId"].ToString());
                crewnumber = dt99.Rows[0]["CrewNumber"].ToString();
                if (recruitingoffice == 3)
                {
                    return "Crew Member [" + crewnumber + "] belongs to Yangon Office.";
                }
                else
                {
                    return "";
                }
            }
        }
        else
        {
            DataTable dt99 = new DataTable();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand odbCommand = oDatabase.GetStoredProcCommand("sp_TransferStatus_Check_Branch_Id");
            oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _CrewId);
            using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
            {
                dt99.Load(dr);
                recruitingoffice = Convert.ToInt32(dt99.Rows[0]["RecruitmentOfficeId"].ToString());
                crewnumber = dt99.Rows[0]["CrewNumber"].ToString();
                if (recruitingoffice != 3)
                {
                    return "Crew Member [" + crewnumber + "] doesn't belong to Yangon Office.";
                }
                else
                {
                    return "";
                }
            }
        }    
    }
    public static string Check_BranchIdforPlanning(int _TrainingPlanningId)
    {
        int recruitingoffice;
        int Branch_Id = Convert.ToInt32(ConfigurationManager.AppSettings["BranchId"].ToString());
        if (Branch_Id == 1)
        {
            DataTable dt99 = new DataTable();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand odbCommand = oDatabase.GetStoredProcCommand("sp_TransferStatus_Check_RecruitingOfficeforPlanning");
            oDatabase.AddInParameter(odbCommand, "@TrainingPlanningId", DbType.Int32, _TrainingPlanningId);
            using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
            {
                dt99.Load(dr);
                recruitingoffice = Convert.ToInt32(dt99.Rows[0]["RecruitingOfficeId"].ToString());
                if (recruitingoffice == 3)
                {
                    return "Training belongs to Yangon Office.";
                }
                else
                {
                    return "";
                }
            }
        }
        else
        {
            DataTable dt99 = new DataTable();
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand odbCommand = oDatabase.GetStoredProcCommand("sp_TransferStatus_Check_RecruitingOfficeforPlanning");
            oDatabase.AddInParameter(odbCommand, "@TrainingPlanningId", DbType.Int32, _TrainingPlanningId);
            using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
            {
                dt99.Load(dr);
                recruitingoffice = Convert.ToInt32(dt99.Rows[0]["RecruitingOfficeId"].ToString());
                if (recruitingoffice != 3)
                {
                    return "Training doesn't belong to Yangon Office.";
                }
                else
                {
                    return "";
                }
            }
        }
    }
    public static string getLocation()
    {
        DataSet ds=Budget.getTable("Select BranchName from Branch Where Self=1");
        return ds.Tables[0].Rows[0][0].ToString().Substring(0, 1);    
    }
    public static string GetMonthName(String MonthId)
    {
        int mid = Convert.ToInt16(MonthId);
        switch (mid)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3: ;
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
            default:
                return "";
        }
    }
    public static string FormatDate(object input)
    {
        if (input == null)
        {
            return "";
        }
        else
        {
            if (input.GetType() == typeof(TableCell) || input.GetType() == typeof(DataControlFieldCell))
            {
                try
                {
                    return ((DataBoundLiteralControl)((TableCell)input).Controls[0]).Text.Trim();
                }
                catch
                {
                    return ""; 
                }
            }
            else
            {
                try
                {
                    string[] res;
                    char[] arr = { '/','-' };
                    res =input.ToString().Split(arr);
                    return res[1] + "-" + GetMonthName(res[0]) + "-" + res[2];
                }
               
                catch (Exception ex)
                {
                    return "";
                }
            }
        }
    }
    //*****UserOnBehalf/Subordinate Rights
    public static int UserOnBehalfRight(int _LoginId, int _InspectionDueId)
    {
        return 1;
        //DataTable dtub = Common.Execute_Procedures_Select_ByQuery("SELECT SUPERINTENDENTID FROM dbo.T_INSPSUPT WHERE INSPECTIONDUEID=" + _InspectionDueId.ToString() + " AND SUPERINTENDENTID IN (SELECT ASSIGNEDUSERID FROM UserOnbehalfRight WHERE ONBEHALFUSERID=" + _LoginId.ToString() + ")");
        //if (dtub != null)
        //{
        //    if (dtub.Rows.Count > 0)
        //        return 1;
        //    else
        //        return 0;
        //}
        //else
        //{
        //    return 0;
        //}
    }
    //*************************************

    public static void HANDLE_PLANNING_GRID(ImageButton imgbtn, Authority ob)
    {
        AuthenticationManager au = new AuthenticationManager(ob.ApplicationModuleId, ob.UserId, ObjectType.Page);
        try
        {
            imgbtn.Enabled = au.IsDelete;
            //gv.Columns[gv.Columns.Count - 1].Visible = ob.isDelete;
        }
        catch
        {
            imgbtn.Enabled = false;
            imgbtn.Enabled = au.IsDelete;
            //gv.Columns[gv.Columns.Count - 1].Visible = false;
            //gv.Columns[gv.Columns.Count - 1].Visible = ob.isDelete;
        }

        //try
        //{
        //    imgbtn.Enabled = ob.isDelete;
        //    //gv.Columns[gv.Columns.Count - 1].Visible = ob.isDelete;
        //}
        //catch
        //{
        //    imgbtn.Enabled = false;
        //    imgbtn.Enabled = ob.isDelete;
        //    //gv.Columns[gv.Columns.Count - 1].Visible = false;
        //    //gv.Columns[gv.Columns.Count - 1].Visible = ob.isDelete;
        //}
    }

    public static void HANDLE_SEARCH_GRID(GridView gv, Authority ob)
    {
        AuthenticationManager au = new AuthenticationManager(ob.ApplicationModuleId, ob.UserId, ObjectType.Module);
        try
        {
            gv.Columns[0].Visible = au.IsView;
        }
        catch
        {
            gv.Columns[0].Visible = false;
            gv.Columns[0].Visible = au.IsView;
        }

        //try
        //{
        //    gv.Columns[0].Visible = ob;
        //}
        //catch
        //{
        //    gv.Columns[0].Visible = false;
        //    gv.Columns[0].Visible = ob.isPrint;
        //}
    }
}