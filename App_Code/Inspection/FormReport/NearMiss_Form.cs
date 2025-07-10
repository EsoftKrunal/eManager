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
/// Summary description for NearMiss_Form
/// </summary>
public class NearMiss_Form
{
    public NearMiss_Form()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectVesselCodeFromVesselId(int _vesselid)
    {
        string procedurename = "PR_FR_GetVesselCode";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        
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
    public static DataTable GetNearMissData(int _vesselid, string _fromdate, string _todate)
    {
        string procedurename = "PR_FR_BindNearMissGrid";
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
    //public static DataTable SelectNearMissSearching(string _nearmissno, int _vesselid, string _fromdate, string _todate, string _familyname, string _firstname, int _rankid, int _crewid)
    public static DataTable SelectNearMissSearching(string _vesselid, string _fromdate, string _todate,string InjuryType)
    {
        string procedurename = "PR_FR_Search_NearMiss";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@NearMissNo", DbType.String, _nearmissno);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@FromDate", DbType.String, _fromdate);
        if (_todate.Trim() != "") {} 

        objDatabase.AddInParameter(objDbCommand, "@ToDate", DbType.String, _todate);
        objDatabase.AddInParameter(objDbCommand, "@InjuryType", DbType.String, InjuryType);
        //objDatabase.AddInParameter(objDbCommand, "@FamilyName", DbType.String, _familyname);
        //objDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, _firstname);
        //objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, _rankid);
        //objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);

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
    public static DataTable SelectNearMissSearchingAll(string _vesselid)
    {
        string procedurename = "PR_FR_Search_NearMissAll";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, _vesselid);
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
    public static DataTable GetCrewIdFromCrewNumber(string _crewnumber)
    {
        string procedurename = "PR_FR_GetCrewIdFromCrewNumber";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _crewnumber);

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
    public static DataTable CheckNearMissRecordExists(string _xml)
    {
        string procedurename = "PR_FR_CheckNearMissRecord";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@XMLForm", DbType.String, _xml);
        //objDatabase.AddInParameter(objDbCommand, "@VesselCode", DbType.String, _vesselcode);

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
    public static DataTable CheckFormCounter()
    {
        string procedurename = "PR_FR_CheckCounter";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

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
    public static DataTable GetNearMissDataById(int _nearmissid, int _kpi, string _trans)
    {
        string procedurename = "PR_FR_GetNearMissDetailById";
        DataTable dt7 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NearMissId", DbType.Int32, _nearmissid);
        objDatabase.AddInParameter(objDbCommand, "@KPI", DbType.Int32, _kpi);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _trans);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt7.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt7.Dispose();
            throw se;
        }
        return dt7;
    }
    public static DataTable SaveKPI(int _vesselid, int _month, int _year, int _kpi, string _trans)
    {
        string procedurename = "PR_FR_InsertUpdateKPI";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@Month", DbType.Int32, _month);
        objDatabase.AddInParameter(objDbCommand, "@Year", DbType.Int32, _year);
        objDatabase.AddInParameter(objDbCommand, "@KPI", DbType.Int32, _kpi);
        objDatabase.AddInParameter(objDbCommand, "@Trans", DbType.String, _trans);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt8.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt8.Dispose();
            throw se;
        }
        return dt8;
    }
    public static DataTable SaveOfcComments(int _nearmissid, string _ibfamname, string _ibfrstname, int _ibcrewid, string _smfamname, string _smfrsyname, int _smcrewid, string _ofcComm, string _comType, string _immCause, string _rootCause, int _updatedBy)
    {
        string procedurename = "PR_FR_UpdateOffComments";
        DataTable dt9 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NearMissId", DbType.Int32, _nearmissid);
        objDatabase.AddInParameter(objDbCommand, "@IdentifiedByFamilyName", DbType.String, _ibfamname);
        objDatabase.AddInParameter(objDbCommand, "@IdentifiedByFirstName", DbType.String, _ibfrstname);
        objDatabase.AddInParameter(objDbCommand, "@IdentifiedByCrewId", DbType.Int32, _ibcrewid);
        objDatabase.AddInParameter(objDbCommand, "@SignMasterFamilyName", DbType.String, _smfamname);
        objDatabase.AddInParameter(objDbCommand, "@SignMasterFirstName", DbType.String, _smfrsyname);
        objDatabase.AddInParameter(objDbCommand, "@SignMasterCrewId", DbType.Int32, _smcrewid);
        objDatabase.AddInParameter(objDbCommand, "@OfficeComments", DbType.String, _ofcComm);
        objDatabase.AddInParameter(objDbCommand, "@CommentType", DbType.String, _comType);
        objDatabase.AddInParameter(objDbCommand, "@ImmediateCause", DbType.String, _immCause);
        objDatabase.AddInParameter(objDbCommand, "@RootCause", DbType.String, _rootCause);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, _updatedBy);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt9.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt9.Dispose();
            throw se;
        }
        return dt9;
    }
    public static DataTable CheckCrewNoName(string _crewno, string _firstname, string _familyname)
    {
        string procedurename = "PR_FR_CheckCrew";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.String, _crewno);
        objDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, _firstname);
        objDatabase.AddInParameter(objDbCommand, "@FamilyName", DbType.String, _familyname);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt10.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt10.Dispose();
            throw se;
        }
        return dt10;
    }

    public static DataTable SaveOfcNmCategory(int _nearmissid, string InjuryCategory, string @PollutionCategory, string @ProDamageCategory, int @CatUpdatedBy)
    {
        string procedurename = "PR_FR_UpdateOffNmCategory";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@NearMissId", DbType.Int32, _nearmissid);
        objDatabase.AddInParameter(objDbCommand, "@InjuryCategory", DbType.String, InjuryCategory);
        objDatabase.AddInParameter(objDbCommand, "@PollutionCategory", DbType.String, PollutionCategory);
        objDatabase.AddInParameter(objDbCommand, "@ProDamageCategory", DbType.String, ProDamageCategory);
        objDatabase.AddInParameter(objDbCommand, "@CatUpdatedBy", DbType.Int32, CatUpdatedBy);       

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt11.Load(dr);
            }
        }
        catch (SqlException se)
        {
            dt11.Dispose();
            throw se;
        }
        return dt11;
    }
}
