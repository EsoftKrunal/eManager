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
/// Summary description for Search
/// </summary>
public class Search
{
	public Search()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable SearchRecord(int ID, string InspectionId, int Owner, int Vessel, int Port, string Status,string FromDate, string Todate, int DueInDays, string InspectorName, string Chapter, string InspectionNo, int CrewNo, int InactiveVessel)
    {

        string procedurename = "PR_Search";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, InspectionId);
            objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, Owner);
            objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, Vessel);
            objDatabase.AddInParameter(objDbCommand, "@Port", DbType.Int32, Port);
            objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, Status);
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String , FromDate);
            objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.String , Todate);
            objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.Int32 , DueInDays);
            objDatabase.AddInParameter(objDbCommand, "@InspectorName", DbType.String , InspectorName);
            objDatabase.AddInParameter(objDbCommand, "@Chapter", DbType.String, Chapter);
            objDatabase.AddInParameter(objDbCommand, "@InspectionNo", DbType.String, InspectionNo);
            objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.Int32, CrewNo);
            objDatabase.AddInParameter(objDbCommand, "@InactiveVsl", DbType.Int32, InactiveVessel);
            
             
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
    public static DataTable SearchRecordNew(int ID, string InspectionId, int Owner, int Vessel, int Port, string Status, string RadDO, string RadPF, string FromDate, string Todate, int DueInDays, string InspectorName, string Chapter, string InspectionNo, int CrewNo, int InactiveVessel,string pv, int loginId)
    {

        string procedurename = "PR_Search_New";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.String, InspectionId);
            objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, Owner);
            objDatabase.AddInParameter(objDbCommand, "@Vessel", DbType.Int32, Vessel);
            objDatabase.AddInParameter(objDbCommand, "@Port", DbType.Int32, Port);
            objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, Status);
            objDatabase.AddInParameter(objDbCommand, "@RadDO", DbType.String, RadDO);
            objDatabase.AddInParameter(objDbCommand, "@RadPF", DbType.String, RadPF);
            objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, FromDate);
            objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.String, Todate);
            objDatabase.AddInParameter(objDbCommand, "@DueInDays", DbType.Int32, DueInDays);
            objDatabase.AddInParameter(objDbCommand, "@InspectorName", DbType.String, InspectorName);
            objDatabase.AddInParameter(objDbCommand, "@Chapter", DbType.String, Chapter);
            objDatabase.AddInParameter(objDbCommand, "@InspectionNo", DbType.String, InspectionNo);
            objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.Int32, CrewNo);
            objDatabase.AddInParameter(objDbCommand, "@InactiveVsl", DbType.Int32, InactiveVessel);
            objDatabase.AddInParameter(objDbCommand, "@pv", DbType.String, pv);
            objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, loginId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
    public static DataTable GetInspections(string  InspectionGroup)
    {

        string procedurename = "PR_Inspections";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionGroup1", DbType.String, InspectionGroup);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
    public static DataTable GetVessel(int OwnerId, int loginId)
    {

        string procedurename = "PR_GetVessel";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        try
        {
            objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, OwnerId);
            objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, loginId);

            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
}
