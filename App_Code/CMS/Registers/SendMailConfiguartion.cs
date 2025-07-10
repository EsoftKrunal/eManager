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
using System.Diagnostics;

/// <summary>
/// Summary description for SendMailConfiguartion
/// </summary>
public class SendMailConfiguartion
{
    public SendMailConfiguartion()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable selectDataStatus()
    {
        string procedurename = "Selectstatus";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }

   
    public static DataTable selectDataSendMailDetails()
    {
        string procedurename = "SelectSendMailDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable selectDataSendMailDetailsById(int _SMC_Id)
    {
        string procedurename = "SelectSendMailDetailsById";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _SMC_Id);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdateSendMailDetails(string _strProc, int _SMC_Id, string _SMC_ProcessName, string _SMC_Frommail, string _SMC_BCC, string _SMC_Body, int _createdby, int _modifiedby, char _status, string _subject, int _processId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@SMC_Id", DbType.Int32, _SMC_Id);
        oDatabase.AddInParameter(odbCommand, "@SMC_ProcessName", DbType.String, _SMC_ProcessName);
        oDatabase.AddInParameter(odbCommand, "@SMC_Frommail", DbType.String, _SMC_Frommail);
        oDatabase.AddInParameter(odbCommand, "@SMC_BCC", DbType.String, _SMC_BCC);
        oDatabase.AddInParameter(odbCommand, "@SMC_Body", DbType.String, _SMC_Body);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@subject", DbType.String, _subject.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@processId", DbType.Int32, _processId);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
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
    public static void deleteSendMailDetails(string _strProc, int _SMC_Id, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@SMC_Id", DbType.Int32, _SMC_Id);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
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
}