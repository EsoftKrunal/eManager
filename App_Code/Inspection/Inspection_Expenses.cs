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

/// <summary>
/// Summary description for Inspection_Expenses
/// </summary>
public class Inspection_Expenses
{
	public Inspection_Expenses()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  public static DataTable AddExpanses( int Id,int InspectionDueId ,int SuperIntendentId ,string ExpenseType,string AccountCode,string Description,string ReceiptNo,string ChargeTo,string Currency,double ExchangeRate,double LocalCurAmt,double AmtInDollar,double CashAdvance, string Date,double Amount ,int CreatedBy,string TransType)

  {
      string procedurename = "PR_ADMS_SuptExpanses";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32,Id);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId",DbType.Int32, InspectionDueId);
        objDatabase.AddInParameter(objDbCommand, "@SuperIntendentId", DbType.Int32, SuperIntendentId);
        objDatabase.AddInParameter(objDbCommand, "@ExpenseType", DbType.String, ExpenseType);

        objDatabase.AddInParameter(objDbCommand, "@Description", DbType.String,Description);
        objDatabase.AddInParameter(objDbCommand, "@ReceiptNo", DbType.String,ReceiptNo);
        objDatabase.AddInParameter(objDbCommand, "@ChargeTo", DbType.String,ChargeTo);
        objDatabase.AddInParameter(objDbCommand, "@ExchangeRate", DbType.Double,ExchangeRate);
        objDatabase.AddInParameter(objDbCommand, "@LocalCurAmt", DbType.Double ,LocalCurAmt);
        objDatabase.AddInParameter(objDbCommand, "@AmtInDollar", DbType.Double,AmtInDollar);
        objDatabase.AddInParameter(objDbCommand, "@CashAdvance", DbType.Double,CashAdvance);

        if(Date!="")
            objDatabase.AddInParameter(objDbCommand, "@Date", DbType.DateTime, Date);
        else
            objDatabase.AddInParameter(objDbCommand, "@Date", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@Amount", DbType.Double, Amount);
        //objDatabase.AddInParameter(objDbCommand, "@Department", DbType.String, Department);
        objDatabase.AddInParameter(objDbCommand, "@AccountCode", DbType.String, AccountCode);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@Currency", DbType.String, Currency);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
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
}
