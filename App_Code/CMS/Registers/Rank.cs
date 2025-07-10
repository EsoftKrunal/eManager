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
/// Summary description for Rank
/// </summary>
public class Rank
{
    public static DataTable selectDataRankGroupId()
    {
        string procedurename = "SelectRankGroupId";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }
        return dt4;
    }
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
    public static DataTable selectDataRankDetails()
    {
        string procedurename = "SelectRankDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataRankDetailsByRankId(int _RankId)
    {
        string procedurename = "SelectRankDetailsByRankId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _RankId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdateRankDetails(string _strProc, int _Rankid, int _RankGroupId, string _RankCode, string _RankName, char _offcrew, char _offgroup, int _createdby, int _modifiedby, char _status, int _ranklevel, string Rank_Mum, string SMOUCode, string SireRankType, string SireRank)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, _Rankid);
        oDatabase.AddInParameter(odbCommand, "@RankGroupId", DbType.Int32, _RankGroupId);
        oDatabase.AddInParameter(odbCommand, "@RankCode", DbType.String, _RankCode);
        oDatabase.AddInParameter(odbCommand, "@RankName", DbType.String, _RankName);
        oDatabase.AddInParameter(odbCommand, "@OffCrew", DbType.String, _offcrew);
        oDatabase.AddInParameter(odbCommand, "@OffGroup", DbType.String, _offgroup);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@RankLevel", DbType.Int32, _ranklevel);
        oDatabase.AddInParameter(odbCommand, "@Rank_Mum", DbType.String, Rank_Mum);
        oDatabase.AddInParameter(odbCommand, "@Smou_Code", DbType.String, SMOUCode);
        oDatabase.AddInParameter(odbCommand, "@SireRankType", DbType.String, SireRankType);
        oDatabase.AddInParameter(odbCommand, "@SireRank", DbType.String, SireRank);
        
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
    public static void deleteRankDetails(string _strProc, int _rankid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, _rankid);
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
