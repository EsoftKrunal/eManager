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
/// Summary description for Supplier
/// </summary>
public class Supplier
{
    public Supplier()
    {
        
    }

    public static DataTable selectSupplierDetails(string s)
    {
        string procedurename = "SupplierDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@SupplierName", DbType.String, s);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static void insertUpdateSupplierDetails(string _strProc, int _TravelAgentid, string _TravelAgentEmail, string _PortAgentCompany, string _PortAgentContactPerson, string _PortAgentAddress, string _PortAgentPhone, string _PortAgentMobile, string _PortAgentFax, int _PortId, string _vendorno, int _createdby, int _modifiedby, char _status)
            {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@SupplierId", DbType.Int32, _TravelAgentid);
        oDatabase.AddInParameter(odbCommand, "@SupplierEmail", DbType.String, _TravelAgentEmail);
        oDatabase.AddInParameter(odbCommand, "@Company", DbType.String, _PortAgentCompany);
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _PortAgentContactPerson);
        oDatabase.AddInParameter(odbCommand, "@Address", DbType.String, _PortAgentAddress);
        oDatabase.AddInParameter(odbCommand, "@Phone", DbType.String, _PortAgentPhone);
        oDatabase.AddInParameter(odbCommand, "@Mobile", DbType.String, _PortAgentMobile);
        oDatabase.AddInParameter(odbCommand, "@Fax", DbType.String, _PortAgentFax);
        oDatabase.AddInParameter(odbCommand, "@PortId", DbType.Int32, _PortId);
        oDatabase.AddInParameter(odbCommand, "@Vendorno", DbType.String, _vendorno);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);

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


    public static void insertUpdateSupplierDetails_PoP(string _strProc, string Company, string _TravelAgentEmail, int _createdby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Company", DbType.String, Company);
        oDatabase.AddInParameter(odbCommand, "@Email", DbType.String, _TravelAgentEmail);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        
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

    public static DataTable selectDataTravelAgentDetailsByTravelAgentId(int _TravelAgentId)
    {
        string procedurename = "SelectSupplierDetailsbySupplierID";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TravelAgentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }


    public static void deleteSupplierDetails(string _strProc, int _TravelAgentid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@SupplierId", DbType.Int32, _TravelAgentid);
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

    public static DataTable selectDataSupplierCompany(string _company)
    {
        string procedurename = "Supplier_CompanyDuplicateCheck";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@company", DbType.String, _company);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable CheckDuplicateSupplier(string comapny)
    {
        string procedurename = "CheckDuplicateSupplier";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Company", DbType.String, comapny);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

}
