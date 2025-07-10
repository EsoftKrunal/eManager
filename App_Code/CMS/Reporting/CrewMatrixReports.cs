using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.IO;
using System.Web.Security;
using System.Data.Common;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CrewMatrixReports
/// </summary>
public class CrewMatrixReports
{
    public static DataTable selectheaderDetails(int crewid)
    {
        string procedurename = "PrintCrewMatrixPrimaryHeaderDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
    public static DataTable selectCrewMatrix1Details(int crewid)
    {
        string procedurename = "PrintCrewMatrixDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectCrewMatrix2Details(int crewid)
    {
        string procedurename = "SelectCrewTankerExperience";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }
        return dt11;
    }
    public static DataTable selectCrewMatrix3Details(int crewid)
    {
        string procedurename = "PrintCrewMatrixMTMSMDetails";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
}
