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
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for VesselDocuments
/// </summary>
public class cls_VesselDocuments
{
    public cls_VesselDocuments()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectFSDocuments(int id)
    {
        string procedurename = "SelectFSDocuments";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@FSId", DbType.Int32, id);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable SelectVesselDocuments(int id)
    {
        string procedurename = "SelectVesselDocuments";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32,id);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable SelectRankDocuments(int id)
    {
        string procedurename = "SelectRankDocuments";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, id);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static DataTable SelectVesselDocumentsType()
    {
        string procedurename = "SelectVesselDocumentsType";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable SelectVesselDocumentsName(int id,int Rankid,int Vesselid)
    {

        string procedurename;
        procedurename = "SelectVesselDocumentsName";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Type", DbType.Int32, id);
        objDatabase.AddInParameter(objDbCommand, "@Rankid", DbType.Int32, Rankid);
        objDatabase.AddInParameter(objDbCommand, "@Vesselid", DbType.Int32, Vesselid);     
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable SelectFSDocumentsName(int id, int Rankid, int FSid)
    {

        string procedurename;
        procedurename = "SelectFSDocumentsName";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Type", DbType.Int32, id);
        objDatabase.AddInParameter(objDbCommand, "@Rankid", DbType.Int32, Rankid);
        objDatabase.AddInParameter(objDbCommand, "@FSid", DbType.Int32, FSid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static void InsertUpdateVesselDocuments(string _strProc,int id, int _vesselId, int TypeId, int NameId,int rankid, int CreatedBy, int ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.Int32, id);
        oDatabase.AddInParameter(odbCommand, "@vesselid", DbType.Int32, _vesselId);
        oDatabase.AddInParameter(odbCommand, "@TypeId", DbType.String, TypeId);
        oDatabase.AddInParameter(odbCommand, "@NameId", DbType.Int32, NameId);
        oDatabase.AddInParameter(odbCommand, "@rankid", DbType.Int32, rankid);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);
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
    public static void InsertUpdateRankDocuments(string _strProc, int id, int _rankId, int TypeId, int NameId, int AlertDays, int CreatedBy, int ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.Int32, id);
        oDatabase.AddInParameter(odbCommand, "@Rankid", DbType.Int32, _rankId);
        oDatabase.AddInParameter(odbCommand, "@TypeId", DbType.String, TypeId);
        oDatabase.AddInParameter(odbCommand, "@NameId", DbType.Int32, NameId);
        oDatabase.AddInParameter(odbCommand, "@AlertDays", DbType.Int32, AlertDays);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);
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
    public static void InsertUpdateFSDocuments(string _strProc, int id, int _FSId, int TypeId, int NameId, int rankid, int CreatedBy, int ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.Int32, id);
        oDatabase.AddInParameter(odbCommand, "@FSid", DbType.Int32, _FSId);
        oDatabase.AddInParameter(odbCommand, "@TypeId", DbType.String, TypeId);
        oDatabase.AddInParameter(odbCommand, "@NameId", DbType.Int32, NameId);
        oDatabase.AddInParameter(odbCommand, "@rankid", DbType.Int32, rankid);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);
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
    
    public static void deleteVesselDocumentsById(string _strProc, int _Id)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.Int32, _Id);
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
