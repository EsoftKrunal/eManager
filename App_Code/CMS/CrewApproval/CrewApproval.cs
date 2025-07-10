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
/// Summary description for CrewApproval
/// </summary>
public class CrewApproval
{
    public CrewApproval()
    {
        
    }
    public static DataTable Bind_CrewApprovalGrid(int _VesselId, int _OwnerId, char _ApprovalStatus, int _Rank, char _RankGroup, int _CrewStatus,string _CrewNo, int _LoginId)
    {
        string procedurename = "Get_CrewApprovalDetails";
        DataTable dtca = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        objDatabase.AddInParameter(objDbCommand, "@ApprovalStatus", DbType.String, _ApprovalStatus);
        objDatabase.AddInParameter(objDbCommand, "@RankId", DbType.Int32, _Rank);
        objDatabase.AddInParameter(objDbCommand, "@RankGroupId", DbType.String, _RankGroup);
        objDatabase.AddInParameter(objDbCommand, "@CrewStatusId", DbType.Int32, _CrewStatus);
        objDatabase.AddInParameter(objDbCommand, "@CrewNo", DbType.String, _CrewNo);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _LoginId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtca.Load(dr);
        }
        return dtca;
    }
    public static void UpdateCrewVesselPlanningHistory(string _strProc, int _planningid, char _appstatus, string _appremark, int _loginid)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@PlanningId", DbType.Int32, _planningid);
        oDatabase.AddInParameter(odbCommand, "@AppStatus", DbType.String, _appstatus);
        oDatabase.AddInParameter(odbCommand, "@AppRemark", DbType.String, _appremark);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _loginid);
        
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
    public static DataTable getRankAccordingtoRankGroup(char _rankgroup)
    {
        string procedurename = "get_RankfromRankGroup";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RankGroup", DbType.String, _rankgroup);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }
        return dt5;
    }
}
