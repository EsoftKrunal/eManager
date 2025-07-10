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
/// Summary description for PlanTraining
/// </summary>
public class PlanTraining
{
    public PlanTraining()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable selectPlanTrainingDetails()
    {
        string procedurename = "SelectPlanTrainingDetails1";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }

    public static DataTable selectTypeOfTraining(int _TrainingId)
    {
        string procedurename = "get_TypeOfTraining";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TrainingId", DbType.Int32, _TrainingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }

    public static DataTable selectDataPlanTrainingName()
    {
        string procedurename = "SelectPlanTrainingName";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }

    public static DataTable selectDataInstituteName()
    {
        string procedurename = "SelectInstituteName";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static DataTable selectDataTrainingZoneName()
    {
        string procedurename = "SelectRecruitingOffice";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }

    public static DataTable selectDataTrainingStatusName()
    {
        string procedurename = "SelectTrainingStatusName";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }

    public static int InsertUpdateTrainingPlanningDeatils(string _strProc, int _PlanTrainingId, int _TrainingId, int _InstituteId, string _FromDate, string _ToDate, int _Duration, string _TrainingCost, int _NoofSeats, int _TotalBatches, int _TrainingZoneId, int _TrainingStatus, int _createdBy, int _modifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
         DataTable dt5 = new DataTable();
         Database objDatabase = DatabaseFactory.CreateDatabase();
        
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TrainingPlanningId", DbType.Int32, _PlanTrainingId);
        oDatabase.AddInParameter(odbCommand, "@TrainingId", DbType.Int32, _TrainingId);
        oDatabase.AddInParameter(odbCommand, "@TrainingInstituteId", DbType.Int32, _InstituteId);
        oDatabase.AddInParameter(odbCommand, "@FromDate", DbType.String, _FromDate);
        oDatabase.AddInParameter(odbCommand, "@ToDate", DbType.String, _ToDate);
        oDatabase.AddInParameter(odbCommand, "@Duration", DbType.Int32, _Duration);
        oDatabase.AddInParameter(odbCommand, "@TrainingCost", DbType.String, _TrainingCost);
        oDatabase.AddInParameter(odbCommand, "@TotalSeats", DbType.Int32, _NoofSeats);
        oDatabase.AddInParameter(odbCommand, "@TotalBatches", DbType.Int32, _TotalBatches);
        oDatabase.AddInParameter(odbCommand, "@RecruitingOfficeId", DbType.Int32, _TrainingZoneId);
        oDatabase.AddInParameter(odbCommand, "@TrainingStatusId", DbType.Int32, _TrainingStatus);
        oDatabase.AddInParameter(odbCommand, "@Createdby", DbType.Int32, _createdBy);
        oDatabase.AddInParameter(odbCommand, "@Modifiedby", DbType.Int32, _modifiedBy);
        

        using (IDataReader dr = objDatabase.ExecuteReader(odbCommand))
        {
            dt5.Load(dr);
        }

        return Convert.ToInt32(dt5.Rows[0][0].ToString());
  

    }

    public static DataTable selectDataPlanTrainingDetailsById(int _PlanTrainingId)
    {
        string procedurename = "SelectPlanTrainingDetailsById";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@TrainingPlanningId", DbType.Int32, _PlanTrainingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
}
