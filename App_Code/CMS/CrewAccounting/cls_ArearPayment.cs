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
public class cls_ArearPayment
{
    public cls_ArearPayment()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GET_AREARPAYMENT(string _CrewID)
    {
        string procedurename = "GET_AREARPAYMENT";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _CrewID);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static bool IS_CREWNUMBER_EXISTS(string _CrewID)
    {
        string procedurename = "IS_CREWNUMBER_EXISTS";
        DataTable dt = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _CrewID);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return (Convert.ToInt32(dt.Rows[0][0].ToString()) == 0);
        
    }
 
    public static int INSERTDATA(string _crewnumber,int Month,int Year,int _createdby)
    {
        DataTable dt = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("PAIDCREWAREARDETAILS");
        oDatabase.AddInParameter(odbCommand, "@CrewNumber", DbType.String, _crewnumber);
        oDatabase.AddInParameter(odbCommand, "@Month", DbType.Int32, Month);
        oDatabase.AddInParameter(odbCommand, "@Year", DbType.Int32, Year);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _createdby);

        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt.Load(dr);
        }

        return (Convert.ToInt32(dt.Rows[0][0].ToString()));
    }
    public static int DeleteData(int Id)
    {
        DataTable dt = new DataTable();
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("DELETE_AREARPAYMENT");
        oDatabase.AddInParameter(odbCommand, "@Id", DbType.String, Id);
        
        using (IDataReader dr = oDatabase.ExecuteReader(odbCommand))
        {
            dt.Load(dr);
        }

        return (Convert.ToInt32(dt.Rows[0][0].ToString()));
    }
}
