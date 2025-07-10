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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions; 

/// <summary>
/// Summary description for InvoiceEntry
/// </summary>
public class InvoiceVerification
{
    public static DataTable SelectAuthorityOfDepartment(out int _retno, int _LoginId)
    {
        _retno = 0;
        string procedurename = "SelectAuthorityOfDepartmentId";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddOutParameter(objDbCommand, "@returnno", DbType.Int32, _retno);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _LoginId);
        objDatabase.AddInParameter(objDbCommand, "@PageId", DbType.String, 4);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
            _retno = Convert.ToInt32(objDatabase.GetParameterValue(objDbCommand, "@returnno"));
        }

        return dt33;
    }

    public static void insertUpdateInvoiceDetails(int Mode,int UserId, string InvoiceList,int Loginid)
    {
        
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateInvoiceApprovalDetails");
        oDatabase.AddInParameter(odbCommand, "@Mode", DbType.Int32, Mode);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.Int32, UserId);
        oDatabase.AddInParameter(odbCommand, "@InvoiceList", DbType.String, InvoiceList);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, Loginid);
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
