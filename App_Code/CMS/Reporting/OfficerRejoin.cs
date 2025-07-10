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
/// Summary description for OfficerRejoin
/// </summary>
public class OfficerRejoin
{
    public OfficerRejoin()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectOfficerRejoin(int _month,int _year)
    {
        string procedurename = "OfficerRejoinCompany";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@month", DbType.Int32, _month);
        objDatabase.AddInParameter(objDbCommand, "@year", DbType.Int32, _year);
      
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }


    public static DataTable selectMemberJoin(int _vesselid, DateTime _fromdate,DateTime _todate)
    {
        string procedurename = "ReportCrewChangeSignOn";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable selectPromotedMember(DateTime _fromdate, DateTime _todate)
    {
        string procedurename = "PromotedOfficerList";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectCrewContract(DateTime _fromdate, DateTime _todate)
    {
        string procedurename = "DateWiseCrewContratDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Fromdate", DbType.DateTime, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@Todate", DbType.DateTime, _todate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable selectvesselmanning(int _vesselid)
    {
        string procedurename = "SelectVesselManningScale";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int16, _vesselid);
        

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
}
