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

public class CostCentre
{
    public static DataTable selectDataCostCentreDetails()
    {
        string procedurename = "SelectCostCentreDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
      
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
  
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }

   public static DataTable selectDataCostCentreDetailsByCostCentreId(int _CostCentreSystemId)
    {
        string procedurename = "SelectCostCentreDetailsByCostCentreId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CostCentreSystemId", DbType.Int32, _CostCentreSystemId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void insertUpdateCostCentreDetails(string _strProc, int _CostCentreSystemId, int _CostCentreId, string _CostCentreName, int _createdBy, int _modifiedBy, char _statusId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CostCentreSystemid", DbType.Int32, _CostCentreSystemId);
        oDatabase.AddInParameter(odbCommand, "@CostCentreid", DbType.Int32, _CostCentreId);
        oDatabase.AddInParameter(odbCommand, "@CostCentrename", DbType.String, _CostCentreName);
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

    public static void deleteCostCentreDetailsById(string _strProc, int _CostCentreSystemId,int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CostCentreSystemid", DbType.Int32, _CostCentreSystemId);
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
