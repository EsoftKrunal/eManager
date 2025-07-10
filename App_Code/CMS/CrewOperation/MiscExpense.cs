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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Transactions; 

/// <summary>
/// Summary description for MiscExpense
/// </summary>
public class MiscExpense
{
    public static DataTable selectDataMiscCostDetails()
    {
        string procedurename = "SelectMiscCostinGrid";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataSet selectaccounthead(int _vesselid,int _budgetyear)
    {
                
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "selectaccounthead_byvesselid";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@BudgetYear", DbType.Int32, _budgetyear);
        
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

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }

    public static void insertUpdateMiscCostDetails(string _strProc, out int _refno, int MiscCostId, int vesselId, string empno, int ac_headid1, string desc1, double tot_amt1, int ac_headid2, string desc2, double tot_amt2, int ac_headid3, string desc3, double tot_amt3, int ac_headid4, string desc4, double tot_amt4, int ac_headid5, string desc5, double tot_amt5, double d, int createdby, int modifiedby, int vendorid)
    {
        _refno = -1;
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddOutParameter(odbCommand, "@refnumber", DbType.Int32, _refno);
        oDatabase.AddInParameter(odbCommand, "@MiscCostRefNo", DbType.Int32, MiscCostId);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, vesselId);
        oDatabase.AddInParameter(odbCommand, "@CrewNumber", DbType.String, empno);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId1", DbType.Int32, ac_headid1);
        oDatabase.AddInParameter(odbCommand, "@Desc1", DbType.String, desc1);
        oDatabase.AddInParameter(odbCommand, "@Amount1", DbType.Double, tot_amt1);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId2", DbType.Int32, ac_headid2);
        oDatabase.AddInParameter(odbCommand, "@Desc2", DbType.String, desc2);
        oDatabase.AddInParameter(odbCommand, "@Amount2", DbType.Double, tot_amt2);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId3", DbType.Int32, ac_headid3);
        oDatabase.AddInParameter(odbCommand, "@Desc3", DbType.String, desc3);
        oDatabase.AddInParameter(odbCommand, "@Amount3", DbType.Double, tot_amt3);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId4", DbType.Int32, ac_headid4);
        oDatabase.AddInParameter(odbCommand, "@Desc4", DbType.String, desc4);
        oDatabase.AddInParameter(odbCommand, "@Amount4", DbType.Double, tot_amt4);
        oDatabase.AddInParameter(odbCommand, "@AccountHeadId5", DbType.Int32, ac_headid5);
        oDatabase.AddInParameter(odbCommand, "@Desc5", DbType.String, desc5);
        oDatabase.AddInParameter(odbCommand, "@Amount5", DbType.Double, tot_amt5);
        oDatabase.AddInParameter(odbCommand, "@Total", DbType.Double, d);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, modifiedby);
        oDatabase.AddInParameter(odbCommand, "@VendorId", DbType.Int32, vendorid);
       
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                _refno = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@refnumber"));
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
    public static DataTable selectCrewIdCrewNumberInMiscCost(string _crewNumber)
    {
        string procedurename = "SelectCrewIdCrewNumberInMiscCost";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _crewNumber);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable selectDataMiscCostDetailsByMiscCostId(int _MiscCostId)
    {
        string procedurename = "SelectMiscCostDetailsByMiscCostId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _MiscCostId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void deleteMiscCostDetails(string _strProc, int _MiscCostid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@MiscCostId", DbType.Int32, _MiscCostid);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                // _rowId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
