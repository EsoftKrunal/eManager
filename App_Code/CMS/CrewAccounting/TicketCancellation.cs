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
/// Summary description for TicketCancellation
/// </summary>
public class TicketCancellation
{
    public TicketCancellation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getVessel(int _userid)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "SelectVesselAccordingToUser";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _userid);

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
    public static DataTable selectPONo(int _vesselid)
    {
        string procedurename = "Get_Po_No_ByVessel";
        DataTable dt2 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable get_POAmt(int _PoId)
    {
        string procedurename = "Get_Po_Amount_ByPoNo";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoId", DbType.Int32, _PoId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void InsertUpdateTicketCancellationDetails(string _strProc, int _PoId, double _CancellationAmount, int _loginid, char _status, string _remarks)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@Poid", DbType.Int32, _PoId);
        oDatabase.AddInParameter(odbCommand, "@Amount", DbType.Double, _CancellationAmount);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _loginid);
        oDatabase.AddInParameter(odbCommand, "@Status", DbType.String, _status);
        oDatabase.AddInParameter(odbCommand, "@Remarks", DbType.String, _remarks);

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
    //public static void DeleteTicketCancellationDetails(string _strProc, int _PoId)
    //{
    //    Database oDatabase = DatabaseFactory.CreateDatabase();

    //    DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
    //    oDatabase.AddInParameter(odbCommand, "@Poid", DbType.Int32, _PoId);

    //    using (TransactionScope scope = new TransactionScope())
    //    {
    //        try
    //        {
    //            // execute command and get records
    //            oDatabase.ExecuteNonQuery(odbCommand);

    //            scope.Complete();
    //        }
    //        catch (Exception ex)
    //        {
    //            // if error is coming throw that error
    //            throw ex;
    //        }
    //        finally
    //        {
    //            // after used dispose commmond            
    //            odbCommand.Dispose();
    //        }
    //    }
    //}
}
