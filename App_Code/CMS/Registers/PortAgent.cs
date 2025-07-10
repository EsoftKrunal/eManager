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
/// Summary description for PortAgent
/// </summary>
public class PortAgent
{
    public static DataTable selectPortName(int _countryid)
    {
        string procedurename = "SelectPortByCountryId";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CountryId", DbType.Int32, _countryid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }
        return dt12;
    }
    public static DataTable selectDataPortName()
    {
        string procedurename = "SelectPort";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }
        return dt12;
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
    public static DataTable selectDataPortAgentDetails(string s)
    {
        string procedurename = "SelectPortAgentDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Name", DbType.String, s);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataPortAgentDetailsByPortAgentId(int _PortAgentId)
    {
        string procedurename = "SelectPortAgentDetailsByPortAgentId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _PortAgentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdatePortAgentDetails(string _strProc, int _PortAgentid, string _PortAgentEmail, string _PortAgentCompany, string _PortAgentContactPerson, string _PortAgentAddress, string _PortAgentPhone, string _PortAgentMobile,string _PortAgentFax,  int _PortId, int _createdby, int _modifiedby, char _status, string _vendorno)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@PortAgentId", DbType.Int32, _PortAgentid);
        oDatabase.AddInParameter(odbCommand, "@PortAgentEmail", DbType.String, _PortAgentEmail);
        oDatabase.AddInParameter(odbCommand, "@Company", DbType.String, _PortAgentCompany);
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _PortAgentContactPerson);
        oDatabase.AddInParameter(odbCommand, "@Address", DbType.String, _PortAgentAddress);
        oDatabase.AddInParameter(odbCommand, "@Phone", DbType.String, _PortAgentPhone);
        oDatabase.AddInParameter(odbCommand, "@Mobile", DbType.String, _PortAgentMobile);
        oDatabase.AddInParameter(odbCommand, "@Fax", DbType.String, _PortAgentFax);
        oDatabase.AddInParameter(odbCommand, "@PortId", DbType.String, _PortId);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@VendorNo", DbType.String, _vendorno);

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
    public static void insertUpdatePortAgentDetails_More(Int32 _PortAgentId, string _PortAgentEmail, string _PortAgentCompany, string _PortAgentContactPerson, string _PortAgentAddress, string _PortAgentPhone, string _PortAgentMobile, string _PortAgentFax, string _vendorno, char _status, int _LoginId , string _PortIdList)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("InsertUpdatePortAgentDetails_More");
        oDatabase.AddInParameter(odbCommand, "@PortAgentId", DbType.Int32, _PortAgentId);
        oDatabase.AddInParameter(odbCommand, "@PortAgentEmail", DbType.String, _PortAgentEmail);
        oDatabase.AddInParameter(odbCommand, "@Company", DbType.String, _PortAgentCompany);
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _PortAgentContactPerson);
        oDatabase.AddInParameter(odbCommand, "@Address", DbType.String, _PortAgentAddress);
        oDatabase.AddInParameter(odbCommand, "@Phone", DbType.String, _PortAgentPhone);
        oDatabase.AddInParameter(odbCommand, "@Mobile", DbType.String, _PortAgentMobile);
        oDatabase.AddInParameter(odbCommand, "@Fax", DbType.String, _PortAgentFax);
        oDatabase.AddInParameter(odbCommand, "@VendorNo", DbType.String, _vendorno);
        oDatabase.AddInParameter(odbCommand, "@Status", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _LoginId);
        oDatabase.AddInParameter(odbCommand, "@PortIds", DbType.String, _PortIdList);
        
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
    public static void deletePortAgentDetails(string _strProc, int _PortAgentid,int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@PortAgentId", DbType.Int32, _PortAgentid);
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
