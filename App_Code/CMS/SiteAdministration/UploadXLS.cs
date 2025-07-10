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


public class UploadXLS
{
    public static void DeleteDeductions(string Vcode,int month,int year)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("DeleteDeductions");
        oDatabase.AddInParameter(odbCommand, "@VesselCode", DbType.String, Vcode);
        oDatabase.AddInParameter(odbCommand, "@Month", DbType.Int32, month);
        oDatabase.AddInParameter(odbCommand, "@Year", DbType.Int32, year);

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
