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
/// Summary description for Safety_Inspection
/// </summary>
public class Safety_Inspection
{
    public Safety_Inspection()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable InsertSafetyInspDetails(int _inspdueid, string _srno, string _contentheading, string _contenttext, int _createdby, string _transtype)
    {
        string procedurename = "PR_SafetyInspectionReport";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _inspdueid);
        objDatabase.AddInParameter(objDbCommand, "@SrNo", DbType.String, _srno);
        objDatabase.AddInParameter(objDbCommand, "@ContentHeading", DbType.String, _contentheading);
        objDatabase.AddInParameter(objDbCommand, "@ContentText", DbType.String, _contenttext);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt1.Dispose();
            throw se;
        }
        return dt1;
    }
    public static DataTable InsertSafetyInspChildDetails(int _maintableid, int _inspdueid, string _srno, string _piccaption, string _filepath, int _createdby, string _transtype)
    {
        string procedurename = "PR_SafetyInspectionReport_Child";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@MainTableId", DbType.Int32, _maintableid);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _inspdueid);
        objDatabase.AddInParameter(objDbCommand, "@SrNo", DbType.String, _srno);
        objDatabase.AddInParameter(objDbCommand, "@PicCaption", DbType.String, _piccaption);
        objDatabase.AddInParameter(objDbCommand, "@FilePath", DbType.String, _filepath);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt2.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt2.Dispose();
            throw se;
        }
        return dt2;
    }
    public static DataTable CheckSrNo(string _srno, int _insdueid)
    {
        string procedurename = "PR_CheckSrNo_SafetyInsp";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@SrNo", DbType.String, _srno);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _insdueid);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt3.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt3.Dispose();
            throw se;
        }
        return dt3;
    }
    public static DataTable CheckMainTable(int _insdueid, string _srno)
    {
        string procedurename = "PR_CheckMainEntry";
        DataTable dt22 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _insdueid);
        objDatabase.AddInParameter(objDbCommand, "@SrNo", DbType.String, _srno);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt22.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt22.Dispose();
            throw se;
        }
        return dt22;
    }
    public static DataTable CheckCrewDetails(int _insdueid)
    {
        string procedurename = "PR_CheckCrewDetails";
        DataTable dt65 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _insdueid);
        
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt65.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt65.Dispose();
            throw se;
        }
        return dt65;
    }
}
