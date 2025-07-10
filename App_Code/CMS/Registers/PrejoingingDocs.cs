using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Web;
using System.Transactions;


/// <summary>
/// Summary description for PrejoingingDocs
/// </summary>
public class PrejoingingDocs
{
    public PrejoingingDocs()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static Boolean chkDocs_ManningAgent(int ManningAgentId, int DocId)
    {
        string procedurename = "Check_PreJoinDocs_ManningAgent";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ManningAgentId", DbType.Int32, ManningAgentId);
        objDatabase.AddInParameter(objDbCommand, "@DocId", DbType.Int32, DocId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt10.Load(dr);
        }
        if (Convert.ToInt32(dt10.Rows[0][0].ToString()) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void InsertPreJoiningDocs(string DocId, int ManningAgentId, int createdBy)
    {
        string procedurename = "Insert_PrejoingingDocMapppingWithManningAgent";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@DocId", DbType.String, DocId);
        objDatabase.AddInParameter(objDbCommand, "@ManningAgentId", DbType.Int32, ManningAgentId);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, createdBy);
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

    public static DataTable selectDataPrejoingDocsDetails()
    {
        string procedurename = "SelectPreJoingDocsDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable SelectPreJoingDocsDetailsByDocId(int _ManningAgentId)
    {
        string procedurename = "SelectPreJoingDocsDetailsByDocId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _ManningAgentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdatePrejoingDocDetails(string _strProc, int _DocId, string _DocName, int _createdby, char _status)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@DocId", DbType.Int32, _DocId);
        oDatabase.AddInParameter(odbCommand, "@DocName", DbType.String, _DocName);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status.ToString().Trim());
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
    public static void deletePrejoingDocDetails(string _strProc, int _ManningAgentId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentId", DbType.Int32, _ManningAgentId);
       // oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);

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