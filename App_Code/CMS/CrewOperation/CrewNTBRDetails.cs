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
/// <summary>
/// Summary description for CrewNTBRDetails
/// </summary>
public class CrewNTBRDetails
{
    public static DataTable selectNTBRDetailsByCrewId(int _crewId)
    {
        string procedurename = "SelectNTBRDeNTBRDetailsByCrewId";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _crewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable selectNTBRDetailsByEmpNo(string _crewId)
    {
        string procedurename = "SelectNTBRDeNTBRDetailsByEmpNo";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@empno", DbType.String, _crewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }





    public static void insertUpdateCrewNTBRDetails(string _strProc, int _crewid, string _ntbrdate, int _NtbrReasonId, string _Remarks, char _NorD,int _loginid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _crewid);
        oDatabase.AddInParameter(odbCommand, "@NTBRDate", DbType.String, _ntbrdate);
        oDatabase.AddInParameter(odbCommand, "@NTBRReasonId", DbType.Int32, _NtbrReasonId);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, _Remarks);
        oDatabase.AddInParameter(odbCommand, "@NTBRFlag", DbType.String, _NorD);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _loginid);
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
