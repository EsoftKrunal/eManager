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
/// Summary description for Accident_Form
/// </summary>
public class Accident_Form
{
    public Accident_Form()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable UpdateAccdDocumentDetails(int _accidentid, int _doctypeid, string _docname, string _filepath)
    {
        string procedurename = "PR_FR_UpAccdDoc";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _accidentid);
        objDatabase.AddInParameter(objDbCommand, "@DocType", DbType.Int32, _doctypeid);
        objDatabase.AddInParameter(objDbCommand, "@DocName", DbType.String, _docname);
        objDatabase.AddInParameter(objDbCommand, "@DocumentUpload", DbType.String, _filepath);
        
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
    public static DataTable GetAccidentData(int _vesselid, string _fromdate, string _todate)
    {
        string procedurename = "PR_FR_BindAccidentGrid";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);

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
    public static DataTable SelectAccidentSearching(string _vesselid, string _fromdate, string _todate,string AT)
    {
        string procedurename = "PR_FR_Search_Accident";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);
        objDatabase.AddInParameter(objDbCommand, "@AC", DbType.String, AT);

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
    public static DataTable GetAccidentDetailsById(int _accidentid)
    {
        string procedurename = "PR_FR_GetAccidentDetailById";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _accidentid);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt4.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt4.Dispose();
            throw se;
        }
        return dt4;
    }
    public static DataTable SaveOfcRemarks(int _accidentid, string _repbyfamname, string _repbyfrstname, int _repbycrewid, string _injfamname, string _injfrstname, int _injcrewid, string _ofcRemarks, string _ofcCategory, int _verifiedBy, string _AccidentCategory, string _AccidentSeverity, string _CompanyPolicy, string _DandATest, string _ImmediateCause, string _RootCause, string _IsVerified)
    {
        string procedurename = "PR_FR_UpdateAccdOffRemarks";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _accidentid);
        objDatabase.AddInParameter(objDbCommand, "@ReportedByFamilyName", DbType.String, _repbyfamname);
        objDatabase.AddInParameter(objDbCommand, "@ReportedByFirstName", DbType.String, _repbyfrstname);
        objDatabase.AddInParameter(objDbCommand, "@ReportedByCrewId", DbType.Int32, _repbycrewid);
        objDatabase.AddInParameter(objDbCommand, "@InjuredFamilyName", DbType.String, _injfamname);
        objDatabase.AddInParameter(objDbCommand, "@InjuredFirstName", DbType.String, _injfrstname);
        objDatabase.AddInParameter(objDbCommand, "@InjuredCrewId", DbType.Int32, _injcrewid);
        objDatabase.AddInParameter(objDbCommand, "@OfficeRemarks", DbType.String, _ofcRemarks);
        objDatabase.AddInParameter(objDbCommand, "@OfficeCategory", DbType.String, _ofcCategory);
        objDatabase.AddInParameter(objDbCommand, "@VerifiedBy", DbType.Int32, _verifiedBy);

        objDatabase.AddInParameter(objDbCommand, "@AccidentCategory", DbType.String, _AccidentCategory);
        objDatabase.AddInParameter(objDbCommand, "@AccidentSeverity", DbType.String, _AccidentSeverity);
        objDatabase.AddInParameter(objDbCommand, "@CompanyPolicy", DbType.String, _CompanyPolicy);
        objDatabase.AddInParameter(objDbCommand, "@DandATest", DbType.String, _DandATest);
        objDatabase.AddInParameter(objDbCommand, "@ImmediateCause", DbType.String, _ImmediateCause);
        objDatabase.AddInParameter(objDbCommand, "@RootCause", DbType.String, _RootCause);
        objDatabase.AddInParameter(objDbCommand, "@IsVerified", DbType.String, _IsVerified);
        

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt5.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt5.Dispose();
            throw se;
        }
        return dt5;
    }
    public static DataTable InsertUpdateAccdDocuments(int _accddocid, int _accidentid, int _doctype, string _docname, string _filepath, string _transtype)
    {
        string procedurename = "PR_InsertUpdateAccdDocs";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AC_DocId", DbType.Int32, _accddocid);
        objDatabase.AddInParameter(objDbCommand, "@AC_Id", DbType.Int32, _accidentid);
        objDatabase.AddInParameter(objDbCommand, "@AC_DocType", DbType.Int32, _doctype);
        objDatabase.AddInParameter(objDbCommand, "@AC_DocName", DbType.String, _docname);
        objDatabase.AddInParameter(objDbCommand, "@AC_DocUpload", DbType.String, _filepath);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt6.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt6.Dispose();
            throw se;
        }
        return dt6;
    }
    public static DataTable SelectAccidentImmCauseDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_Accd_ImmCause";
        DataTable dt7 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt7.Load(dr);
        }

        return dt7;
    }
    public static DataTable SelectAccidentRootCauseDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_Accd_RootCause";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable SelectAccidentCategoryDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_AccdCategory";
        DataTable dt9 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt9.Load(dr);
        }

        return dt9;
    }
    public static DataTable SelectAccidentSeverityDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_AccdSeverity";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt10.Load(dr);
        }

        return dt10;
    }
    public static DataTable SelectInjuryCategoryDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_AccdInjuryCat";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable SelectOfficeCategoryDetails(int _AccidentId)
    {
        string procedurename = "PR_RPT_AccdCatAnalysis";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AccidentId", DbType.Int32, _AccidentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }

        return dt12;
    }
}
