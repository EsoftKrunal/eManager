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
/// Summary description for PrintPO
/// </summary>
public class PrintPO
{
    public PrintPO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectPOHeader(int _poid)
    {
        string procedurename = "Print_Po_Header";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@POId", DbType.Int32, _poid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectPODetails(int _poid, int _vendorid, int _vesselid, DateTime _podate)
    {
        string procedurename = "Print_PO_Details";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@POId", DbType.Int32, _poid);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@PoDate", DbType.DateTime, _podate);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }

    // MISC
    public static DataTable selectMiscHeader(int _miscid)
    {
        string procedurename = "Print_Misc_Po_Header";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoId", DbType.Int32, _miscid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;
    }
    public static DataTable selectMiscDetails(int _miscid, int _vendorid, int _vesselid, DateTime _date)
    {
        string procedurename = "Print_Misc_Po_Details";
        DataTable dt22 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@PoId", DbType.Int32, _miscid);
        objDatabase.AddInParameter(objDbCommand, "@VendorId", DbType.Int32, _vendorid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Date", DbType.DateTime, _date);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }
        return dt22;
    }

    public static DataTable getCrewDetails(int _poid)
    {
        string procedurename = "get_CrewDetailsfor_PrintPO";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@POId", DbType.Int32, _poid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
}
