using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class CrewAccounting_UploadXLSFiles : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Import Deduction";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 125);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        int sss=Convert.ToInt32(Session["loginid"].ToString());
        Label2.Text = "";
        this.btn_Upload_save.Visible = Auth.isEdit;
    }
    public DataSet getDatset(string VesselCode,string Month,string Year,string filename)
    {
        DataSet DS = new DataSet();
        System.Data.OleDb.OleDbDataAdapter MyCommand;
        System.Data.OleDb.OleDbConnection MyConnection;
        MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + Server.MapPath("..\\EMANAGERBLOB\\HRD\\WagesSheet\\") + filename + ";Extended Properties=Excel 8.0;");
        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select '" + VesselCode + "' as VesselCode," + Month + " as PMonth," + Year + " as PYear,EMPNo,UnfixedOT,Allotments,CashAdvance,BondedStores,RadioTeleCall,OtherDeductions,PaidDays from [Sheet1$]", MyConnection);

        MyCommand.Fill(DS);
        MyConnection.Close();
        return DS;
    }
    protected void btn_Authority_save_Click(object sender, EventArgs e)
    {
        string File;
        String VesselCode;
        DataSet ds;
        int Month, Year;
        VesselCode = "BBA";
        Month = 1;
        Year = 2008;

        string str1 = FileUpload1.PostedFile.FileName;
        Boolean str_correct = str1.EndsWith(".xls");
        if (str_correct == true)
        {
            string str2 = System.IO.Path.GetFileName(str1);
        }
        else
        {
            Label2.Text = "Upload only XLS files.";
        }

        try
        {
            File = FileUpload1.PostedFile.FileName;
            File = File.Substring(File.LastIndexOf("\\") + 1, File.Length - File.LastIndexOf("\\") - 1);
            VesselCode = File.Substring(0, 3);

            Month = Convert.ToInt32(File.Substring(4, 2));
            Year = Convert.ToInt32(File.Substring(7, 4));

            FileUpload1.SaveAs(Server.MapPath("~/EMANAGERBLOB/HRD/WagesSheet") + "/" + File);

            ds = getDatset(VesselCode, Month.ToString(), Year.ToString(), File);
            UploadXLS.DeleteDeductions(VesselCode, Month, Year);
            SqlConnection MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
            SqlDataAdapter MyDataAdapter = new SqlDataAdapter("SELECT * from PayrollDeductions", MyConnection);
            SqlCommandBuilder MyCmd = new SqlCommandBuilder(MyDataAdapter);
            DataSet MyDataSet = new DataSet();
            MyDataAdapter.Fill(MyDataSet);
            MyDataSet.Tables[0].Rows.Clear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow MyRow = MyDataSet.Tables[0].NewRow();

                MyRow[0] = ds.Tables[0].Rows[i][0];
                MyRow[1] = ds.Tables[0].Rows[i][1];
                MyRow[2] = ds.Tables[0].Rows[i][2];
                MyRow[3] = ds.Tables[0].Rows[i][3];
                MyRow[4] = ds.Tables[0].Rows[i][4];
                MyRow[5] = ds.Tables[0].Rows[i][5];
                MyRow[6] = ds.Tables[0].Rows[i][6];
                MyRow[7] = ds.Tables[0].Rows[i][7];
                MyRow[8] = ds.Tables[0].Rows[i][8];
                MyRow[9] = ds.Tables[0].Rows[i][9];
                MyRow[10] = ds.Tables[0].Rows[i][10];

                MyDataSet.Tables[0].Rows.Add(MyRow);
            }
            MyDataAdapter.Update(MyDataSet);
            Label2.Text = "Deductions Imported Successfully.";
        }
        catch
        {
            Label2.Text = "Deductions Can't Imported..";
        }
         
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AdminDashBoard.aspx");
    }
}
