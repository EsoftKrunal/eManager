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
/// Summary description for PortPlanner1
/// </summary>
public class PortPlanner1
{
    public static DataTable selectPortReferenceNumberDetails(int _portid,int _vesselid,string EmpNo,string Status, int loginId)
    {
        string procedurename = "SelectPortReferenceNumberDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortId", DbType.Int32, _portid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.String, EmpNo);
        objDatabase.AddInParameter(objDbCommand, "@PStatus", DbType.String, Status);
        objDatabase.AddInParameter(objDbCommand, "@loginId", DbType.Int32, loginId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectSignOffGridDetails(int _id)
    {
        string procedurename = "SelectSignOffGridDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _id);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectSignOnGridDetails(int _id)
    {
        string procedurename = "SelectSignOnGridDetails";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _id);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
    public static DataTable Check_PortCallDeleted(int _id)
    {
        string procedurename = "Check_PortCallDeleted";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _id);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
    public static DataTable Check_PortCallDeleted_travel(int _id,int _pid)
    {
        string procedurename = "Check_PortCallDeleted_travel";
        DataTable dt27 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _pid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt27.Load(dr);
        }
        return dt27;
    }
    public static void deletePortCallCrewDetailsById(string _strProc, int _CrewId, int _PortCallid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CrewId", DbType.Int32, _CrewId);
        oDatabase.AddInParameter(odbCommand, "@PortCallId", DbType.Int32, _PortCallid);


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

    public static int Insert_NewMembertoPortCall(int _id, int _pid)
    {
        string procedurename = "Insert_NewMembertoPortCall";
        DataTable dt27 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _pid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt27.Load(dr);
        }
        return Convert.ToInt32(dt27.Rows[0][0].ToString());
    }
}
