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
/// Summary description for CashToMaster
/// </summary>
public class CashToMaster
{
    public CashToMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectMastersAccordingToVessel(int _VesselId)
    {
        string procedurename = "selectMastersAccordingToVessel";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectPrevBalanceByVesselId(int _VesselId)
    {
        string procedurename = "SelectPrevBalanceByVesselId";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static void insertUpdateCTMDetails(string _strProc, int _Vesselid, double _prevbal,int _crewId,string req_date, double req_amt, double amt_paid, double amt_rec, double handle_charge, string remarks, int _createdby)
    {
     
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _Vesselid);
        oDatabase.AddInParameter(odbCommand, "@PreviousBalance", DbType.Double, _prevbal);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _crewId);
        oDatabase.AddInParameter(odbCommand, "@RequestDate", DbType.String, req_date);
        oDatabase.AddInParameter(odbCommand, "@RequestAmt", DbType.Double, req_amt);
        oDatabase.AddInParameter(odbCommand, "@AmountPaid", DbType.Double, amt_paid);
        oDatabase.AddInParameter(odbCommand, "@AmountReceived", DbType.Double, amt_rec);
        oDatabase.AddInParameter(odbCommand, "@HandlingCharges", DbType.Double, handle_charge);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, remarks);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);

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
