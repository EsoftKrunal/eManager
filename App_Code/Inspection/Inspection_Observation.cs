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
/// Summary description for Inspection_Observation
/// </summary>
public class Inspection_Observation
{
	public Inspection_Observation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //retrive Question 
    public static DataTable GetQuestion(string questionno,int ID )
    {

        string procedurename = "PR_GetQuestion";
            DataTable dt1 = new DataTable();

            Database objDatabase = DatabaseFactory.CreateDatabase();
            DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

            try
            {
                objDatabase.AddInParameter(objDbCommand, "@questionno", DbType.String, questionno);                
                objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        } 
        return dt1;
      }
    //Check Valid User Name 
    public static DataTable CheckUserName(string CrewNumber,string Type)
    {

        string procedurename = "PR_GetUserId";
            DataTable dt1 = new DataTable();

            Database objDatabase = DatabaseFactory.CreateDatabase();
            DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
            try
            {
                objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, CrewNumber);
                objDatabase.AddInParameter(objDbCommand, "@Type", DbType.String, Type);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
               
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        } 
        return dt1;
      }
    //Add Update Observation
    public static DataTable UpdateInspectionObservation(int ID,string StartDate,string InspectionNo, int Master, int ChiefOfficer, int SecondOffice, int ChiefEngineer, int AssistantEngineer, string Inspector, string  ResponseDueDate, string ActualLocation, string  ActualDate, int QuestionId, string Deficiency, string Comment, int HighRisk, int NCR, string TransType, int CreatedBy, int ModifiedBy, string _ObsStatus, int _IsObs)
    {
        string procedurename = "PR_ADMS_Observation";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
        objDatabase.AddInParameter(objDbCommand, "@InspectionNo", DbType.String, InspectionNo);
        objDatabase.AddInParameter(objDbCommand, "@Master", DbType.Int32, Master);
        objDatabase.AddInParameter(objDbCommand, "@ChiefOfficer", DbType.Int32, ChiefOfficer);
        objDatabase.AddInParameter(objDbCommand, "@SecondOffice", DbType.Int32, SecondOffice);
        objDatabase.AddInParameter(objDbCommand, "@AssistantEngineer", DbType.Int32, AssistantEngineer);
        objDatabase.AddInParameter(objDbCommand, "@ChiefEngineer", DbType.Int32,ChiefEngineer);
        objDatabase.AddInParameter(objDbCommand, "@Inspector", DbType.String , Inspector);
        if (ResponseDueDate != "")
            objDatabase.AddInParameter(objDbCommand, "@ResponseDueDate", DbType.DateTime, ResponseDueDate);
        else
            objDatabase.AddInParameter(objDbCommand, "@ResponseDueDate", DbType.DateTime, DBNull.Value);
        objDatabase.AddInParameter(objDbCommand, "@ActualLocation", DbType.String, ActualLocation);
        objDatabase.AddInParameter(objDbCommand, "@ActualDate", DbType.String , ActualDate.ToString());
        objDatabase.AddInParameter(objDbCommand, "@Deficiency", DbType.String, Deficiency);
        objDatabase.AddInParameter(objDbCommand, "@Comment", DbType.String, Comment);
        objDatabase.AddInParameter(objDbCommand, "@QuestionId", DbType.Int32, QuestionId);
        objDatabase.AddInParameter(objDbCommand, "@HighRisk", DbType.Int32, HighRisk);
        objDatabase.AddInParameter(objDbCommand, "@NCR", DbType.Int32, NCR);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);
        objDatabase.AddInParameter(objDbCommand, "@StartDate", DbType.String, StartDate.ToString());
        objDatabase.AddInParameter(objDbCommand, "@ObservationStatus", DbType.String, _ObsStatus);
        objDatabase.AddInParameter(objDbCommand, "@IsObservation", DbType.Int32, _IsObs);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt1;
    }
    //Check InspectionType
    public static DataTable CheckInspType(int _InspDueId)
    {
        string procedurename = "PR_GetInspType";
        DataTable dt56 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt56.Load(dr);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
        return dt56;
    }
    //Update Crew
    public static DataSet UpdateCrew(int _InspectionDueId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_GetCrewNoName";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspectionDueId);
        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDataset.Dispose();
            objDbCommand.Dispose();
        }
        return objDataset;
    }
    //Delete Observations of particular InspectionId
    public static void DeleteObs_Checklist(int _InspDueId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("PR_DeleteObservations");
        oDatabase.AddInParameter(odbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
        
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
}
