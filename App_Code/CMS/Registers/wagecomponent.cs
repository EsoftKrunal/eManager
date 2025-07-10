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
/// Summary description for wagecomponent
/// </summary>
public class wagecomponent
{
    public wagecomponent()
    {
        
    }
    public static DataTable selectDataWageScaleComponentMasterDetailsByWageScaleId(int _WageScaleId)
    {
        string procedurename = "SelectWageScaleMasterDetailsById";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, _WageScaleId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static DataTable selectWageScaleComponentDetails()
    {
        string procedurename = "SelectWageScaleComponentDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectWageScaleDetails(int wageid)
    {
        string procedurename = "SelectWageScaleDetails";
        DataTable dt15 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageId", DbType.Int32, wageid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt15.Load(dr);
        }

        return dt15;
    }
    public static DataTable w_component()
    {
        string procedurename = "M_WageScaleComponent";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static void insertdata(string wgname, string component, int loginid)
    {
       
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Insert_WageComponentMaster1");
        oDatabase.AddInParameter(odbCommand, "@wagename", DbType.String, wgname);
        oDatabase.AddInParameter(odbCommand, "@componentid", DbType.String, component);
        oDatabase.AddInParameter(odbCommand, "@LoginID", DbType.Int32, loginid);

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

    public static void insertdata1(string wgname, string component, int loginid,string _status,int _wageid)
    {

        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Insert_WageComponentMaster");
        oDatabase.AddInParameter(odbCommand, "@wagename", DbType.String, wgname);
        oDatabase.AddInParameter(odbCommand, "@componentid", DbType.String, component);
        oDatabase.AddInParameter(odbCommand, "@wageIdd", DbType.Int32, _wageid);
        oDatabase.AddInParameter(odbCommand, "@LoginID", DbType.Int32, loginid);

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
    public static DataTable w_componentgdsearch()
    {
        string procedurename = "WageScaleComponentMaster";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static void updateWageName(int wgid, string wgname, string currency,int modifiedby, char statusid, decimal workinghrs, string workinghrsCategory)
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdateWageName");
        oDatabase.AddInParameter(odbCommand, "@wagescaleid", DbType.Int32, wgid);
        oDatabase.AddInParameter(odbCommand, "@Wagescalename", DbType.String, wgname);
        oDatabase.AddInParameter(odbCommand, "@Currency", DbType.String, currency);
        oDatabase.AddInParameter(odbCommand, "@modifiedby", DbType.Int32, modifiedby);
        oDatabase.AddInParameter(odbCommand, "@statusid", DbType.String, statusid);
        oDatabase.AddInParameter(odbCommand, "@WorkingHours", DbType.Decimal, workinghrs);
        oDatabase.AddInParameter(odbCommand, "@WorkinHoursCategory", DbType.String, workinghrsCategory);

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
    public static void insertwagedetail(int wgname, int component)
    {
        int RValue;
        RValue = 0;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("insert_wageDetails");
        oDatabase.AddInParameter(odbCommand, "@wagescaleid", DbType.Int32, wgname);
        oDatabase.AddInParameter(odbCommand, "@wagecompid", DbType.Int32, component);

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
    public static void deletewagedetail(int wgname)
    {
       
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand("Delete_wageDetails");
        oDatabase.AddInParameter(odbCommand, "@wagescaleid", DbType.Int32, wgname);

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

    public static DataTable selectWageScaleComponentDetailsById(int _WageScaleComponentId)
    {
        string procedurename = "SelectWageScaleComponentDetailsById";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleComponentId", DbType.Int32, _WageScaleComponentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static DataTable selectWageScaleDetailsById(int _WageScaleComponentId,int _wagescaleid,int _nationalityid,int _seniority)
    {
        string procedurename = "SelectWageScaleDetailsById";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleComponentId", DbType.Int32, _WageScaleComponentId);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, _wagescaleid);
        objDatabase.AddInParameter(objDbCommand, "@Seniorty", DbType.Int32, _seniority);
        objDatabase.AddInParameter(objDbCommand, "@Nationality", DbType.Int32, _nationalityid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectWageScaleDetailsById_History(int _WageScaleComponentId, int _wagescalerankid)
    {
        string procedurename = "SelectWageScaleDetailsById_History";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleComponentId", DbType.Int32, _WageScaleComponentId);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleRankId", DbType.Int32, _wagescalerankid);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
