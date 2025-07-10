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
/// Summary description for ManningAgent
/// </summary>
public class ManningAgent
{
    public ManningAgent()
    {
        //
        // TODO: Add constructor logic here
        //


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
    public static DataTable selectDataManningAgentDetails()
    {
        string procedurename = "SelectManningAgentDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable GetAllManningAgent()
    {
        string procedurename = "GetAllManningAgent";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    
    public static DataTable selectDataManningAgentDetailsByAgentId(int _ManningAgentId)
    {
        string procedurename = "SelectManningAgentDetailsByAgentId";
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
    public static void insertUpdateManningAgentDetails(string _strProc, int _ManningAgentId, string _ManningAgentName, int _createdby, int _modifiedby, char _status, string  _contactPerson, string _contactno, string _email )
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentId", DbType.Int32, _ManningAgentId);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentName", DbType.String, _ManningAgentName);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _contactPerson.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@ContactNo", DbType.String, _contactno.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@Email", DbType.String, _email.ToString().Trim());

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
    public static void deleteManningAgentDetails(string _strProc, int _ManningAgentId, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentId", DbType.Int32, _ManningAgentId);
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