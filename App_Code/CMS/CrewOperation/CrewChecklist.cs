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
/// Summary description for CrewChecklist
/// </summary>
public class CrewChecklist
{
    public CrewChecklist()
    {
        
    }
    public static DataTable selectDataCrewChecklistDetails()
    {
        string procedurename = "SelectCrewChecklist";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataContractid(int _crewid)
    {
        string procedurename = "SelectDataContractid";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectDataContractidNew(int _crewid)
    {
        string procedurename = "SelectDataContractidNew";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static void insertUpdatechecklistDetails(string _strProc,int _insertid, int _crewId, int _contractId, int _checklistId, char _checklistflag,int _createdby,int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
         oDatabase.AddInParameter(odbCommand, "@InsertId", DbType.Int32, _insertid);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _crewId);
        oDatabase.AddInParameter(odbCommand, "@ContractId", DbType.Int32, _contractId);
        oDatabase.AddInParameter(odbCommand, "@CheckListId", DbType.Int32, _checklistId);
        oDatabase.AddInParameter(odbCommand, "@CheckListFlag", DbType.String, _checklistflag);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
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
