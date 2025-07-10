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
/// Summary description for Promotion
/// </summary>
public class Promotion
{
    public Promotion()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectCrewStatus(int _crewId)
    {
        string procedurename = "SelectCrewStatusByCrewId";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, _crewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable SelectAppraisal(string _crewNumber)
    {
        string procedurename = "SelectAppraisalByCrewNumber";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String , _crewNumber);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable SelectCrewStatusByCrewNumber(string  _crewNumber)
    {
        string procedurename = "SelectCrewStatusByCrewNumber";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _crewNumber);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable Chk_ContractLicense(string _CrewNumber, int _RankID, int _NewRankID)
    {
        string procedurename = "Check_Crew_ContractBy_CrewNumber";
        DataTable dt11 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _CrewNumber);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, _RankID);
        objDatabase.AddInParameter(objDbCommand, "@NewRankId", DbType.Int32, _NewRankID);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable SelectNextRankDetails(int _rankid)
    {
        string procedurename = "get_PromotionRank";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.String, _rankid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable SelectPromotionDetailsById(int _CrewId)
    {
        string procedurename = "SelectPromotionDetailsById";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _CrewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static void InsertPromotionDetails(string _strProc, string _crewNumber, int _companyid, int _currentrankid, int _promotionrank, DateTime _promotiondate, int _vesselid, int _vesseltypeid, int _createdby, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CrewNumber", DbType.String, _crewNumber);
        oDatabase.AddInParameter(odbCommand, "@CompanyId", DbType.Int32, _companyid);
        oDatabase.AddInParameter(odbCommand, "@RankId", DbType.Int32, _currentrankid);
        oDatabase.AddInParameter(odbCommand, "@CurrentRankId", DbType.Int32, _promotionrank);
        oDatabase.AddInParameter(odbCommand, "@PDate", DbType.DateTime, _promotiondate);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _vesselid);
        oDatabase.AddInParameter(odbCommand, "@VesselTypeId", DbType.Int32, _vesseltypeid);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
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
    public static void checkcrewnumber(string _strProc, string _crewnumber, out int _result)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        _result = -1;
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CrewNumber", DbType.String, _crewnumber);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, _result);

        using (TransactionScope scope = new TransactionScope())
        {
            try
            {

                oDatabase.ExecuteNonQuery(odbCommand);
                _result = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
                scope.Complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                odbCommand.Dispose();
            }
        }
    }
    public static DataTable selectCrewMembersByEmpNo(string _crewId)
    {
        string procedurename = "SelectPromotionDetailsByEmpNo";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@empno", DbType.String, _crewId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
}
