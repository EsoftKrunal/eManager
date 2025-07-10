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
using Microsoft.Practices.EnterpriseLibrary.Data;
using DocumentFormat.OpenXml.Presentation;
using System.Diagnostics.Contracts;

/// <summary>
/// Summary description for Revenue
/// </summary>
public class Revenue
{
    public Revenue()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataSet getMasterDataforRevenue(string TableName, string Field1, string Field2, int loginId = 0)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_GetMaster_ForRevenue";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@MasterName", DbType.String, TableName);
        objDatabase.AddInParameter(objDbCommand, "@Field1", DbType.String, Field1);
        objDatabase.AddInParameter(objDbCommand, "@Field2", DbType.String, Field2);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, loginId);
        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }

    public static DataTable AdVesselContractDetails(int ContractId, int VesselId, DateTime FromDt, int FromHrs, int FromMins, DateTime ToDt, int ToHrs, int ToMins, int contractTypeId, Decimal ContractAmount, Decimal PerdayContractAmount, int Contractduration, int ContractStatus, int CreatedBy, string TransType, int ChartererId, int FromPort, int ToPort, Decimal volume, decimal Rate, decimal totalHireAmtVoyage, int ExpVoyageDays, string CargoDetails,decimal AddComPer = 0, decimal AddComAmt = 0  )
    {

        string procedurename = "SP_RV_VesselContractDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@ContractID", DbType.Int32, ContractId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, FromDt);
        objDatabase.AddInParameter(objDbCommand, "@FromHrs", DbType.Int32, FromHrs);
        objDatabase.AddInParameter(objDbCommand, "@FromMins", DbType.Int32, FromMins);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, ToDt);
        objDatabase.AddInParameter(objDbCommand, "@ToHrs", DbType.Int32, ToHrs);
        objDatabase.AddInParameter(objDbCommand, "@ToMins", DbType.Int32, ToMins);
        objDatabase.AddInParameter(objDbCommand, "@ContractTypeId", DbType.Int32, contractTypeId);
        objDatabase.AddInParameter(objDbCommand, "@ContractAmount", DbType.Decimal, ContractAmount);
        objDatabase.AddInParameter(objDbCommand, "@ContractAmountperDay", DbType.Decimal, PerdayContractAmount);
        objDatabase.AddInParameter(objDbCommand, "@ContractDuration", DbType.Int32, Contractduration);
        objDatabase.AddInParameter(objDbCommand, "@ContractStatus", DbType.Int32, ContractStatus);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);      
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
        objDatabase.AddInParameter(objDbCommand, "@ChartererId", DbType.Int32, ChartererId);
        objDatabase.AddInParameter(objDbCommand, "@FromPort", DbType.Int32, FromPort);
        objDatabase.AddInParameter(objDbCommand, "@ToPort", DbType.Int32, ToPort);
        objDatabase.AddInParameter(objDbCommand, "@volume", DbType.Decimal, volume);
        objDatabase.AddInParameter(objDbCommand, "@Rate", DbType.Decimal, Rate);
        objDatabase.AddInParameter(objDbCommand, "@totalHireAmtVoyage", DbType.Decimal, totalHireAmtVoyage);
        objDatabase.AddInParameter(objDbCommand, "@ExpVoyageDays", DbType.Int32, ExpVoyageDays);
        objDatabase.AddInParameter(objDbCommand, "@CargoDetails", DbType.String, CargoDetails);
        objDatabase.AddInParameter(objDbCommand, "@AddComPer", DbType.Decimal, AddComPer);
        objDatabase.AddInParameter(objDbCommand, "@AddComAmt", DbType.Decimal, AddComAmt);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static DataTable InsertUpdateActExpensesDtls (int ExpensesId, int contractid, int voyageId,int categoryId, Decimal amount, string remarks , int CreatedBy)
    {
        string procedurename = "SP_RV_InsertUpdateContractExpensesDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@ExpensesId", DbType.Int32, ExpensesId);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@RCVDId", DbType.Int32, voyageId);
        objDatabase.AddInParameter(objDbCommand, "@CategoryId", DbType.Int32, categoryId);
        objDatabase.AddInParameter(objDbCommand, "@Amount", DbType.Decimal, amount);
        objDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, remarks);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static DataTable GetActExpensesDtls(int ExpensesId, int contractid, int voyageId, int flag)
    {
        string procedurename = "SP_RV_GETVesselContractExpensesDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@ExpensesId", DbType.Int32, ExpensesId);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@VoyageId", DbType.Int32, voyageId);
        objDatabase.AddInParameter(objDbCommand, "@Flag", DbType.Int32, flag);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static DataTable ContractExpectedExpenses(int contractId, int contractTypeId)
    {
        string procedurename = "SP_Get_VSLContractExpExpensesDetails";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractId);
        objDatabase.AddInParameter(objDbCommand, "@ContractTypeId", DbType.Int32, contractTypeId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static void UpdateExpExpensesDetails(int contractId, string headerstr, string datastr, int createdBy)
    {
        string procedurename = "Update_ContractExpExpensesDetails";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractId);   
        objDatabase.AddInParameter(objDbCommand, "@HeaderStr", DbType.String, headerstr);
        objDatabase.AddInParameter(objDbCommand, "@DataStr", DbType.String, datastr);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, createdBy);
        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

    }

    public static DataTable AddContractVoyageDetails(int voyageId, int ContractId, string voyageno,  int CreatedBy, int fromport, int toport, DateTime formDate, int FromHrs, int FromMins, DateTime todate, int ToHrs, int ToMins, decimal CargoQty) 
    {

        string procedurename = "Sp_InsertUpdateContractVoyageDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@VoyageId", DbType.Int32, voyageId);
        objDatabase.AddInParameter(objDbCommand, "@contractId", DbType.Int32, ContractId);
        objDatabase.AddInParameter(objDbCommand, "@VoyageNo", DbType.String, voyageno);   
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@fromPort", DbType.Int32, fromport);
        objDatabase.AddInParameter(objDbCommand, "@toPort", DbType.Int32, toport);
        objDatabase.AddInParameter(objDbCommand, "@FromDt", DbType.DateTime, formDate);
        objDatabase.AddInParameter(objDbCommand, "@FromHrs", DbType.Int32, FromHrs);
        objDatabase.AddInParameter(objDbCommand, "@FromMins", DbType.Int32, FromMins);
        objDatabase.AddInParameter(objDbCommand, "@ToDt", DbType.DateTime, todate);
        objDatabase.AddInParameter(objDbCommand, "@ToHrs", DbType.Int32, ToHrs);
        objDatabase.AddInParameter(objDbCommand, "@ToMins", DbType.Int32, ToMins);
        objDatabase.AddInParameter(objDbCommand, "@CargoQty", DbType.Decimal, CargoQty);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static DataTable selectDataCharterer()
    {
        string procedurename = "SelectCharterer";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }

    public static DataTable InsertUpdateOffHireDtls(int OffHireId, int contractid, int voyageId, int categoryId, string Location, DateTime FromDt, int FromHrs, int FromMins, DateTime ToDt, int ToHrs, int ToMins, string reason, string remark, Decimal amount, decimal amountperday, int offhireduration, int CreatedBy)
    {
        string procedurename = "SP_RV_InsertUpdateContractOffHireDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@OffHireId", DbType.Int32, OffHireId);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@RCVDId", DbType.Int32, voyageId);
        objDatabase.AddInParameter(objDbCommand, "@CategoryId", DbType.Int32, categoryId);
        objDatabase.AddInParameter(objDbCommand, "@Location", DbType.String, Location);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.DateTime, FromDt);
        objDatabase.AddInParameter(objDbCommand, "@FromHrs", DbType.Int32, FromHrs);
        objDatabase.AddInParameter(objDbCommand, "@FromMins", DbType.Int32, FromMins);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.DateTime, ToDt);
        objDatabase.AddInParameter(objDbCommand, "@ToHrs", DbType.Int32, ToHrs);
        objDatabase.AddInParameter(objDbCommand, "@ToMins", DbType.Int32, ToMins);
        objDatabase.AddInParameter(objDbCommand, "@Reason", DbType.String, reason);
        objDatabase.AddInParameter(objDbCommand, "@Remark", DbType.String, remark);
        objDatabase.AddInParameter(objDbCommand, "@OffHireAmount", DbType.Decimal, amount);
        objDatabase.AddInParameter(objDbCommand, "@OffHireAmountPerDay", DbType.Decimal, amountperday);
        objDatabase.AddInParameter(objDbCommand, "@TotalOffHireDays", DbType.Int32, offhireduration);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static DataTable GetOffhireDtls(int OffhireId, int contractid, int voyageId, int flag)
    {
        string procedurename = "SP_RV_GETVesselContractOffHireDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@OffHireId", DbType.Int32, OffhireId);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, contractid);
        objDatabase.AddInParameter(objDbCommand, "@VoyageId", DbType.Int32, voyageId);
        objDatabase.AddInParameter(objDbCommand, "@Flag", DbType.Int32, flag);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }

    public static string FormatCurrency(object InValue)
    {
        string StrIn = InValue.ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
        return OutValue;
    }


}