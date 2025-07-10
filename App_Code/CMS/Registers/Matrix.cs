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
/// Summary description for Matrix
/// </summary>
public class Matrix
{
    public Matrix()
    {
       
    }
    public static DataTable selectRank()
    {
        string procedurename = "SelectRank";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectMatrixHeader(int _matrixid)
    {
        string procedurename = "SelectMatrixHeader";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.String, _matrixid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }

    public static DataTable selectMatrixDetail(int _matrixid)
    {
        string procedurename = "SelectMatrixDetail";
        DataTable dt21 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.String, _matrixid);


        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt21.Load(dr);
        }
        return dt21;

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
    public static void InsertMatrixHeader(string _strProc, int _MatrixId, string _MatrixName, char _status, int _CreatedBy, int _ModifiedBy,out int _returnvalue)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        _returnvalue = 0;
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@MatrixId", DbType.Int32, _MatrixId);
        oDatabase.AddInParameter(odbCommand, "@MatrixName", DbType.String, _MatrixName);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, _returnvalue);


        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                _returnvalue = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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

    public static void InsertUpdateMatrixDetails(string _strProc, int _MatrixId, int _Rankid, int _Rank1, int _experience, int _experience1, int _experience2)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@MatrixId", DbType.Int32, _MatrixId);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, _Rankid);
        oDatabase.AddInParameter(odbCommand, "@Rank1", DbType.Int32, _Rank1);
        oDatabase.AddInParameter(odbCommand, "@experience", DbType.Int32, _experience);
        oDatabase.AddInParameter(odbCommand, "@experience1", DbType.Int32, _experience1);
        oDatabase.AddInParameter(odbCommand, "@experience2", DbType.Int32, _experience2);
               

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

    public static void deleteMatrixDetails(string _strProc, int _Licenseid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@MatrixId", DbType.Int32, _Licenseid);
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