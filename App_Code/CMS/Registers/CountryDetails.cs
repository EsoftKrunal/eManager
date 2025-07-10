using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using System.Data.Common; 
using Microsoft.Practices.EnterpriseLibrary.Data; 

/// <summary>
/// Summary description for CountryDetails
/// </summary>
public class CountryDetails
{
    public CountryDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable  selectcountrydetails()
    {
        string procedurename = "SelectCountryDetails";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertupdatecountry(string _strProc, int _CountryId, string _CountryCode, string _CountryName, string _NationalityName, string _NationalityCode, int _CreatedBy, int _ModifiedBy,char _statusid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CountryId", DbType.Int32, _CountryId);
        oDatabase.AddInParameter(odbCommand, "@CountryCode", DbType.String, _CountryCode);
        oDatabase.AddInParameter(odbCommand, "@CountryName", DbType.String, _CountryName);
        oDatabase.AddInParameter(odbCommand, "@NationalityName", DbType.String, _NationalityName);
        oDatabase.AddInParameter(odbCommand, "@NationalityCode", DbType.String, _NationalityCode);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);
        oDatabase.AddInParameter(odbCommand, "@statusId", DbType.String , _statusid);


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
    public static void deleteCountryById(string _strProc, int _CountryId,int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CountryId", DbType.Int32, _CountryId);
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
