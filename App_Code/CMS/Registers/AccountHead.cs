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

public class AccountHead
{
    public static DataTable selectDataAccountHeadDetails(string _accountheadname)
    {
        string procedurename = "SelectAccountHeadDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@HeadName", DbType.String, _accountheadname);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static DataTable selectDataStatusDetails()
    {
        string procedurename = "Selectstatus";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }

    public static DataTable selectDataAccountHeadDetailsByAccountHeadId(int _AccountHeadId)
    {
        string procedurename = "SelectAccountHeadDetailsByAccountHeadId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccountHeadId", DbType.Int32, _AccountHeadId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void insertUpdateAccountHeadDetails(string _strProc, int _AccountHeadId, string _AccountHeadNumberCLS, string _AccountHeadNumber, string _AccountHeadName, char _AccountHeadType, char _IncludeInBudgets,char _recovearblecost, int _createdBy, int _modifiedBy, char _statusId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadid", DbType.Int32, _AccountHeadId);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadnumberCLS", DbType.String, _AccountHeadNumberCLS);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadnumberCostPlus", DbType.String, _AccountHeadNumber);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadname", DbType.String, _AccountHeadName);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadtype", DbType.String, _AccountHeadType);
        oDatabase.AddInParameter(odbCommand, "@IncludeInBudgets", DbType.String, _IncludeInBudgets);
        oDatabase.AddInParameter(odbCommand, "@RecoverableCost", DbType.String, _recovearblecost);
        oDatabase.AddInParameter(odbCommand, "@createdby", DbType.Int32, _createdBy);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, _modifiedBy);
        oDatabase.AddInParameter(odbCommand, "@statusid", DbType.String, _statusId);

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

    public static void deleteAccountHeadDetailsById(string _strProc, int _AccountHeadId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadid", DbType.Int32, _AccountHeadId);
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
