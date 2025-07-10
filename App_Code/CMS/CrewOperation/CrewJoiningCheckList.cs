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
/// Summary description for CrewJoiningCheckList
/// </summary>
public class CrewJoiningCheckList
{
    public CrewJoiningCheckList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable get_contractchecklistheader(int _crewid,int _vesselid,int _contractid)
    {
        string procedurename = "get_Check_List_Header";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, _contractid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable get_contractchecklistheader_Other(int _crewid,int Mode,int Rank)
    {
        string procedurename;
        if (Mode == 0)
        {
            procedurename = "get_Check_List_Header_Other";
        }
        else if (Mode == 1)
        { 
            procedurename = "get_Check_List_Header_Other1"; 
        }
        else if (Mode == 2)
        {
            procedurename = "get_Check_List_Header_Other2";
        }
        else
        {
            procedurename = "get_Check_List_Header_Other3";
        }
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        objDatabase.AddInParameter(objDbCommand, "@Rank", DbType.Int32, _crewid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }


    public static DataTable get_Check_List_Details(int _contractid)
    {
        string procedurename = "get_Check_List_Details";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ContractId", DbType.Int32, _contractid);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
}
