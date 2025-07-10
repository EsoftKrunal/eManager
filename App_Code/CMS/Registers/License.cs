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
/// Summary description for License
/// </summary>
public class License
{
    public static DataTable selectDataOffCrew()
    {
        string procedurename = "SelectOffCrew";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectDataOffGroup()
    {
        string procedurename = "SelectOffGroup";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
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
    public static DataTable selectDataRank(char Offcrew, char Offgroup)
    {
        string procedurename = "SelectRankfromoffcrew_groupid";
        DataTable dt21 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@OffcrewId", DbType.String, Offcrew);
        objDatabase.AddInParameter(objDbCommand, "@OffgroupId", DbType.String, Offgroup);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt21.Load(dr);
        }
        return dt21;
    }
    public static DataTable selectDataLicenseDetails(string s)
    {
        string procedurename = "SelectLicenseDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LicenceName", DbType.String, s);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataLicenceDetailsByLicenceId(int _LicenceId)
    {
        string procedurename = "SelectLicenseDetailsByLicenceId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _LicenceId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdateLicenceDetails(string _strProc, int _Licenseid, string _Licensetype, string _LicenseName, char _offcrew, char _offgroup, char _expires, char _coc, int _rank, int _createdby, int _modifiedby, char _status)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@LicenseId", DbType.Int32, _Licenseid);
        oDatabase.AddInParameter(odbCommand, "@LicenseType", DbType.String, _Licensetype);
        oDatabase.AddInParameter(odbCommand, "@LicenseName", DbType.String, _LicenseName);
        oDatabase.AddInParameter(odbCommand, "@OffCrew", DbType.String, _offcrew);
        oDatabase.AddInParameter(odbCommand, "@OffGroup", DbType.String, _offgroup);
        oDatabase.AddInParameter(odbCommand, "@Expires", DbType.String, _expires);
        oDatabase.AddInParameter(odbCommand, "@COC", DbType.String, _coc);
        oDatabase.AddInParameter(odbCommand, "@Rank", DbType.Int32, _rank);
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
    public static void deleteLicenceDetails(string _strProc, int _Licenseid,int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@LicenseId", DbType.Int32, _Licenseid);
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
