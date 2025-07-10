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
/// Summary description for LeaveRule
/// </summary>
public class LeaveRule
{
    public static DataTable selectRankLeaveDetails()
    {
        string procedurename = "Select_LeaveRule";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static void UpdaterankLeaveDetails(string _strProc, int _rankid, double _leave)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, _rankid);
        oDatabase.AddInParameter(odbCommand, "@Leaves", DbType.Double, _leave);
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
}
