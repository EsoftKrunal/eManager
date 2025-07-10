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
/// Summary description for MiningScale
/// </summary>
public class Budget
{
    public Budget()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getData(string strprocname, int vesselid, int budgetyear,int type)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, vesselid);
        SqlDataAdapter adp;
        
        
  
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int16, budgetyear);
        objDatabase.AddInParameter(objDbCommand, "@BudgetType", DbType.Int16, type);
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataSet getTable(string Query)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand("ExecQuery");
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@Query", DbType.String, Query);
        SqlDataAdapter adp;
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataSet getData1(string strprocname, int vesselid, int budgetyear, int budgettype)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();

        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, vesselid);
        SqlDataAdapter adp;



        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int16, budgetyear);
        objDatabase.AddInParameter(objDbCommand, "@BudgetType", DbType.Int32, budgettype);
        
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static void insertRow(string _strProc,int _budgettype, int _vesselid, int _accountheadid, int _budgetyear, int fromFate,int toDate, int _jan, int _feb, int _mar, int _apr, int _may, int _jun, int _jul, int _aug, int _sep, int _oct, int _nov, int _dec, int _createdby, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@BudgetType", DbType.Int32, _budgettype);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _vesselid);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId", DbType.Int32, _accountheadid);
        oDatabase.AddInParameter(odbCommand, "@BudgetYear", DbType.Int32, _budgetyear);
        oDatabase.AddInParameter(odbCommand, "@FromDate", DbType.Int32, fromFate);
        oDatabase.AddInParameter(odbCommand, "@ToDate", DbType.Int32, toDate);
        oDatabase.AddInParameter(odbCommand, "@Jan", DbType.Int32, _jan);
        oDatabase.AddInParameter(odbCommand, "@Feb", DbType.Int32, _feb);
        oDatabase.AddInParameter(odbCommand, "@Mar", DbType.Int32, _mar);
        oDatabase.AddInParameter(odbCommand, "@Apr", DbType.Int32, _apr);
        oDatabase.AddInParameter(odbCommand, "@May", DbType.Int32, _may);
        oDatabase.AddInParameter(odbCommand, "@Jun", DbType.Int32, _jun);
        oDatabase.AddInParameter(odbCommand, "@Jul", DbType.Int32, _jul);
        oDatabase.AddInParameter(odbCommand, "@Aug", DbType.Int32, _aug);
        oDatabase.AddInParameter(odbCommand, "@Sep", DbType.Int32, _sep);
        oDatabase.AddInParameter(odbCommand, "@Oct", DbType.Int32, _oct);
        oDatabase.AddInParameter(odbCommand, "@Nov", DbType.Int32, _nov);
        oDatabase.AddInParameter(odbCommand, "@Dec", DbType.Int32, _dec);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
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
    public static void UpdateBudget(Int32 VBId, int _jan, int _feb, int _mar, int _apr, int _may, int _jun, int _jul, int _aug, int _sep, int _oct, int _nov, int _dec,int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateVesselBudget");
        oDatabase.AddInParameter(odbCommand, "@VBid", DbType.Int32, VBId);
        oDatabase.AddInParameter(odbCommand, "@Jan", DbType.Int32, _jan);
        oDatabase.AddInParameter(odbCommand, "@Feb", DbType.Int32, _feb);
        oDatabase.AddInParameter(odbCommand, "@Mar", DbType.Int32, _mar);
        oDatabase.AddInParameter(odbCommand, "@Apr", DbType.Int32, _apr);
        oDatabase.AddInParameter(odbCommand, "@May", DbType.Int32, _may);
        oDatabase.AddInParameter(odbCommand, "@Jun", DbType.Int32, _jun);
        oDatabase.AddInParameter(odbCommand, "@Jul", DbType.Int32, _jul);
        oDatabase.AddInParameter(odbCommand, "@Aug", DbType.Int32, _aug);
        oDatabase.AddInParameter(odbCommand, "@Sep", DbType.Int32, _sep);
        oDatabase.AddInParameter(odbCommand, "@Oct", DbType.Int32, _oct);
        oDatabase.AddInParameter(odbCommand, "@Nov", DbType.Int32, _nov);
        oDatabase.AddInParameter(odbCommand, "@Dec", DbType.Int32, _dec);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
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
    public static void DeleteAll(string strprocname, int vesselid, int budgetyear)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
       
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int16, budgetyear);
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

        // Note: connection was closed by ExecuteDataSet method call 
    }
    public static DataSet getAllVessels(string strprocname,int VesselId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, VesselId);
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataSet getVesselBudgetType(string strprocname)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static int IsBudgetAlreadySaved(string strprocname, int vesselid,int year)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = strprocname;
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int16, year);
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

        // Note: connection was closed by ExecuteDataSet method call 

        return Convert.ToInt32(objDataset.Tables[0].Rows[0][0]);
        
    }
}
