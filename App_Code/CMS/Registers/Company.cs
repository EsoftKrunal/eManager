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
/// Summary description for Company
/// </summary>
public class Company
{
    public static DataTable get_defaultcompanylogo()
    {
        string procedurename = "get_defaultcompanylogo";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable selectDataStatus()
    {
        string procedurename = "Selectstatus";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }
        return dt3;
    }

    public static DataTable selectDataCompanyDetails()
    {
        string procedurename = "SelectCompanyDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
       
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataCompanyDetailsByCompanyId(int _CompanyId)
    {
        string procedurename = "SelectCompanyDetailsByCompanyId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _CompanyId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }



    public static void insertUpdateCompanyDetails(string _strProc, int _Companyid, string _companyname, string _companyabbr, string _address1, string _address2, string _address3, string _telno1, string _telno2, string _faxno, string _email1, string _email2, string _website, int _createdby, int _modifiedby, char _status, string _logoImageName)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CompanyId", DbType.Int32, _Companyid);
        oDatabase.AddInParameter(odbCommand, "@CompanyName", DbType.String, _companyname);
        oDatabase.AddInParameter(odbCommand, "@CompanyAbbr", DbType.String, _companyabbr);
        oDatabase.AddInParameter(odbCommand, "@Address1", DbType.String, _address1);
        oDatabase.AddInParameter(odbCommand, "@Address2", DbType.String, _address2);
        oDatabase.AddInParameter(odbCommand, "@Address3", DbType.String, _address3);
        oDatabase.AddInParameter(odbCommand, "@TelephoneNumber1", DbType.String, _telno1);
        oDatabase.AddInParameter(odbCommand, "@TelephoneNumber2", DbType.String, _telno2);
        oDatabase.AddInParameter(odbCommand, "@FaxNumber", DbType.String, _faxno);
        oDatabase.AddInParameter(odbCommand, "@Email1", DbType.String, _email1);
        oDatabase.AddInParameter(odbCommand, "@Email2", DbType.String, _email2);
        oDatabase.AddInParameter(odbCommand, "@Website", DbType.String, _website);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@LogoImageName", DbType.String, _logoImageName);

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
    public static void deleteCompanyDetails(string _strProc, int _companyid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CompanyId", DbType.Int32, _companyid);
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

    public static void insertUpdateContractCompanyMaster(string _strProc, int _Companyid, string _companyname, string _companyabbr, string _address1, string _address2, string _address3, string _telno1, string _telno2, string _faxno, string _email1, string _email2, string _website, int _createdby, int _modifiedby, char _status, string _regno, string _rpsl, int _IsShipManager, int _IsOwnerAgent, int _IsMLCAgent, int _IsManningAgent, string _rpslValidity, string _contactperson)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CompanyId", DbType.Int32, _Companyid);
        oDatabase.AddInParameter(odbCommand, "@CompanyName", DbType.String, _companyname);
        oDatabase.AddInParameter(odbCommand, "@CompanyAbbr", DbType.String, _companyabbr);
        oDatabase.AddInParameter(odbCommand, "@Address1", DbType.String, _address1);
        oDatabase.AddInParameter(odbCommand, "@Address2", DbType.String, _address2);
        oDatabase.AddInParameter(odbCommand, "@Address3", DbType.String, _address3);
        oDatabase.AddInParameter(odbCommand, "@TelephoneNumber1", DbType.String, _telno1);
        oDatabase.AddInParameter(odbCommand, "@TelephoneNumber2", DbType.String, _telno2);
        oDatabase.AddInParameter(odbCommand, "@FaxNumber", DbType.String, _faxno);
        oDatabase.AddInParameter(odbCommand, "@Email1", DbType.String, _email1);
        oDatabase.AddInParameter(odbCommand, "@Email2", DbType.String, _email2);
        oDatabase.AddInParameter(odbCommand, "@Website", DbType.String, _website);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@RegNo", DbType.String, _regno);
        oDatabase.AddInParameter(odbCommand, "@RPSL", DbType.String, _rpsl);
        oDatabase.AddInParameter(odbCommand, "@IsShipManager", DbType.Int32, _IsShipManager);
        oDatabase.AddInParameter(odbCommand, "@IsOwnerAgent", DbType.Int32, _IsOwnerAgent);
        oDatabase.AddInParameter(odbCommand, "@IsMLCAgent", DbType.Int32, _IsMLCAgent);
        oDatabase.AddInParameter(odbCommand, "@IsManningAgent", DbType.Int32, _IsManningAgent);
        oDatabase.AddInParameter(odbCommand, "@RPSLValidityDt", DbType.String, _rpslValidity);
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _contactperson);

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

    public static DataTable selectDataCompanyContractDetails()
    {
        string procedurename = "SelectCompanyContractDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static DataTable selectDataContractCompanyDetailsByCompanyId(int _CompanyId)
    {
        string procedurename = "SelectContractCompanyDetailsByCompanyId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _CompanyId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static void deleteContractCompanyById(string _strProc, int _ContractCompanyId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CompanyId", DbType.Int32, _ContractCompanyId);
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
