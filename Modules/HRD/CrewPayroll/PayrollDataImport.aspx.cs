using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;

public partial class Modules_HRD_CrewPayroll_PayrollDataImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Upload(object sender, EventArgs e)
    {
        //Upload and save the file
        try
        {
            String sql1 = "Truncate table CrewPortageBillHeaderImported";
            DataTable dtruncate = Common.Execute_Procedures_Select_ByQueryCMS(sql1);

            string excelPath = Server.MapPath("~/EMANAGERBLOB/HRD/PayrollSheet/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                // string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                string sheet1 = "Sheet1$";
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtExcelData.Columns.AddRange(new DataColumn[29] { new DataColumn("Crew #", typeof(string)),
                new DataColumn("Crew Name", typeof(string)),
                new DataColumn("Rank", typeof(string)),
                new DataColumn("Contract #", typeof(int)),
                new DataColumn("Vessel", typeof(string)),
                new DataColumn("Pay Month", typeof(int)),
                new DataColumn("Pay Year",typeof(int)),
                new DataColumn("FD",typeof(int)),
                new DataColumn("TD",typeof(int)),
                new DataColumn("Extra OT Hrs",typeof(decimal)),
                new DataColumn("Travel Pay Days",typeof(int)),
                new DataColumn("Joining Exp Reimb",typeof(decimal)),
                new DataColumn("Joining Allow",typeof(decimal)),
                new DataColumn("Hold Cleaning Bonus",typeof(decimal)),
                new DataColumn("HRA Allow",typeof(decimal)),
                new DataColumn("Extra Work Allow",typeof(decimal)),
                new DataColumn("Tank Cleaning Allow",typeof(decimal)),
                new DataColumn("Reefer Bonus",typeof(decimal)),
                new DataColumn("Watchkeeping Allow",typeof(decimal)),
                new DataColumn("Laundry Allow",typeof(decimal)),
                new DataColumn("Others",typeof(decimal)),
                new DataColumn("Regular Allotment",typeof(decimal)),
                new DataColumn("Special Allotment",typeof(decimal)),
                new DataColumn("Cash Advance",typeof(decimal)),
                new DataColumn("Bonded Stores",typeof(decimal)),
                new DataColumn("Phone Internet",typeof(decimal)),
                new DataColumn("Airfare Deduction",typeof(decimal)),
                new DataColumn("Additional Allotment Bank Charges",typeof(decimal)),
                new DataColumn("Other Recoverables",typeof(decimal)),
            });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.CrewPortageBillHeaderImported";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Vessel", "VesselCode");
                        sqlBulkCopy.ColumnMappings.Add("Pay Month", "Month");
                        sqlBulkCopy.ColumnMappings.Add("Pay Year", "Year");
                        sqlBulkCopy.ColumnMappings.Add("Crew #", "EmpNumber");
                        sqlBulkCopy.ColumnMappings.Add("Crew Name", "EmpName");
                        sqlBulkCopy.ColumnMappings.Add("Rank", "Rank");
                        sqlBulkCopy.ColumnMappings.Add("Contract #", "ContractId");
                        sqlBulkCopy.ColumnMappings.Add("FD", "FD");
                        sqlBulkCopy.ColumnMappings.Add("TD", "TD");
                        sqlBulkCopy.ColumnMappings.Add("Extra OT Hrs", "ExtraOtHours");
                        sqlBulkCopy.ColumnMappings.Add("Travel Pay Days", "TravelPayDays");
                        sqlBulkCopy.ColumnMappings.Add("Joining Exp Reimb", "Joining_Exp_Reimb");
                        sqlBulkCopy.ColumnMappings.Add("Joining Allow", "Joining_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Hold Cleaning Bonus", "Hold_Cleaning_Bonus");
                        sqlBulkCopy.ColumnMappings.Add("HRA Allow", "HRA_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Extra Work Allow", "Extra_Work_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Tank Cleaning Allow", "Tank_Cleaning_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Reefer Bonus", "Reefer_Bonus");
                        sqlBulkCopy.ColumnMappings.Add("Watchkeeping Allow", "Watchkeeping_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Laundry Allow", "Laundry_Allow");
                        sqlBulkCopy.ColumnMappings.Add("Others", "Others");
                        sqlBulkCopy.ColumnMappings.Add("Regular Allotment", "Regular_Allotment");
                        sqlBulkCopy.ColumnMappings.Add("Special Allotment", "Special_Allotment");
                        sqlBulkCopy.ColumnMappings.Add("Cash Advance", "Cash_Advance");
                        sqlBulkCopy.ColumnMappings.Add("Bonded Stores", "Bonded_Stores");
                        sqlBulkCopy.ColumnMappings.Add("Phone Internet", "Phone_Internet");
                        sqlBulkCopy.ColumnMappings.Add("Airfare Deduction", "Airfare_Deduction");
                        sqlBulkCopy.ColumnMappings.Add("Additional Allotment Bank Charges", "Addl_Allot_Bank_Charges");
                        sqlBulkCopy.ColumnMappings.Add("Other Recoverables", "Other_Recoverables");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }
                }

                DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.InsertUpdateCrewportagebillHeaderDetails");
                if (dt_Data != null)
                {
                    if (dt_Data.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Import Ship Data successfully.')", true);
                        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("Delete from CrewPortageBillHeaderImported; Select -1");
                    }
                }
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Error : ' "+ ex.Message.ToString() + ")", true);
        }
    }
}