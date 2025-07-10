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
/// Summary description for Inspection_Planning
/// </summary>
public class Inspection_Planning
{
    public Inspection_Planning()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable CreateInspectionNo(int VesselId)
    {

        string procedurename = "PR_InspectionNo";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);


        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);

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
    public static DataTable CheckPort(string PortName)
    {

        string procedurename = "PR_GetPortId";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        try
        {
            objDatabase.AddInParameter(objDbCommand, "@PortName", DbType.String, PortName);


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

    public static DataSet CheckSupt(string PlanDate, int SuptId)//string RandomInspection,
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_SuptDetail";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        //objDatabase.AddInParameter(objDbCommand, "@RandomInspection", DbType.String, RandomInspection);
        objDatabase.AddInParameter(objDbCommand, "@PlanDate", DbType.String, PlanDate);
        objDatabase.AddInParameter(objDbCommand, "@SuptId", DbType.Int32, SuptId);

        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataSet GetInspection(string Inspectiontype)//string RandomInspection,
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_GetInspection";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        //objDatabase.AddInParameter(objDbCommand, "@RandomInspection", DbType.String, RandomInspection);
        objDatabase.AddInParameter(objDbCommand, "@Inspectiontype", DbType.String, Inspectiontype);

        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;
    }
    public static DataTable AddInspectors(int Id, int InspectionDueId, int SuperintendentId, int Attending, DateTime DepartureDate, int FromPort, int ToPort, DateTime ExpCompDate, string Remarks, int CreatedBy, int ModifiedBy, string TransType)
    {

        string procedurename = "PR_ADMS_Inspectors";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, Id);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, InspectionDueId);
        objDatabase.AddInParameter(objDbCommand, "@SuperintendentId", DbType.Int32, SuperintendentId);
        objDatabase.AddInParameter(objDbCommand, "@Attending", DbType.Boolean, Attending);

        objDatabase.AddInParameter(objDbCommand, "@DepartureDate", DbType.DateTime, DepartureDate);
        objDatabase.AddInParameter(objDbCommand, "@FromPort", DbType.Int32, FromPort);
        objDatabase.AddInParameter(objDbCommand, "@ToPort", DbType.Int32, ToPort);
        objDatabase.AddInParameter(objDbCommand, "@ExpCompletionDate", DbType.DateTime, ExpCompDate);
        objDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, Remarks);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);

        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
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
    public static DataTable AdInspectionPlanning(int ID, int VesselId, string InspectionNo, int InspectionId, DateTime PlanDate, string PlanLocation, int PlanFromPort, int PlanToPort, int ReqForInsp, int CreatedBy, int ModifiedBy, string TransType, string PlanRemark, string _Status, string _ActualDt, string _ActualLocat, string _InspValDt, string _OnHold, string _IsSire, int _VersionId)
    {

        string procedurename = "PR_ADMS_InspectionPlanning";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32, ID);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);
        objDatabase.AddInParameter(objDbCommand, "@InspectionNo", DbType.String, InspectionNo);
        objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, InspectionId);
        objDatabase.AddInParameter(objDbCommand, "@PlanDate", DbType.DateTime, PlanDate);
        objDatabase.AddInParameter(objDbCommand, "@PlanLocation", DbType.String, PlanLocation);
        objDatabase.AddInParameter(objDbCommand, "@PlanFromPort", DbType.Int32, PlanFromPort);
        objDatabase.AddInParameter(objDbCommand, "@PlanToPort", DbType.Int32, PlanToPort);
        objDatabase.AddInParameter(objDbCommand, "@ReqForInsp", DbType.Boolean, ReqForInsp);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, CreatedBy);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, ModifiedBy);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, TransType);
        objDatabase.AddInParameter(objDbCommand, "@PlanRemark", DbType.String, PlanRemark);
        objDatabase.AddInParameter(objDbCommand, "@Status", DbType.String, _Status);
        objDatabase.AddInParameter(objDbCommand, "@ActualDate", DbType.String, _ActualDt);
        objDatabase.AddInParameter(objDbCommand, "@ActualLocation", DbType.String, _ActualLocat);
        objDatabase.AddInParameter(objDbCommand, "@InspectionValidity", DbType.String, _InspValDt);
        objDatabase.AddInParameter(objDbCommand, "@OnHold", DbType.String, _OnHold);
        objDatabase.AddInParameter(objDbCommand, "@IsSire", DbType.String, _IsSire);
        objDatabase.AddInParameter(objDbCommand, "@VersionId", DbType.Int32, _VersionId);
        
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
    public static DataTable GetVesselCode(int VslID)
    {
        string procedurename = "PR_GetVesselCode";
        DataTable dt1 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        try
        {
            objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, VslID);
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

        //Database objDatabase = DatabaseFactory.CreateDatabase();
        //string procedurename = "PR_GetVesselCode";
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //DataSet objDataset = new DataSet();
        //objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VslID);

        //try
        //{
        //    objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        //}
        //catch (Exception ex)
        //{
        //    // if error is coming throw that error
        //    throw ex;
        //}
        //finally
        //{
        //    // after used dispose dataset and commmand
        //    objDataset.Dispose();
        //    objDbCommand.Dispose();
        //}
        //// Note: connection was closed by ExecuteDataSet method call 
        //return objDataset;
    }
    //public static DataSet getDataInspector(int Id,string Mode)
    //{
    //    Database objDatabase = DatabaseFactory.CreateDatabase();
    //    string procedurename = "PR_ADMS_Inspectors";
    //    DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
    //    DataSet objDataset = new DataSet();

    //    objDatabase.AddInParameter(objDbCommand, "@ID", DbType.Int32,Id);
    //    objDatabase.AddInParameter(objDbCommand, "@Mode", DbType.String,Mode);
    //    try
    //    {
    //        objDataset = objDatabase.ExecuteDataSet(objDbCommand);
    //    }
    //    catch (Exception ex)
    //    {
    //        // if error is coming throw that error
    //        throw ex;
    //    }
    //    finally
    //    {
    //        // after used dispose dataset and commmand
    //        objDataset.Dispose();
    //        objDbCommand.Dispose();
    //    }

    //    // Note: connection was closed by ExecuteDataSet method call 

    //    return objDataset;
    //}
    public static DataTable CheckInspectionStatus(int _InspID)
    {
        string procedurename = "PR_CheckInspStatus";
        DataTable dt88 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _InspID);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt88.Load(dr);
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
        return dt88;
    }
    public static void SetClosedStatus_OfOldInsp(int _InspDueId)
    {
        string procedurename = "PR_SetStatus";
        DataTable dt98 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _InspDueId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt98.Load(dr);
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
    }
    public static DataTable GetRandomInspection(int _InspID)
    {
        string procedurename = "PR_GetRandomInsp";
        DataTable dt15 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _InspID);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt15.Load(dr);
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
        return dt15;
    }
    public static DataTable SetClosedStatus_OfDueInsp(int _InspId, int _VesselId)
    {
        string procedurename = "PR_SetStatusofDueInsp";
        DataTable dt76 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@InspectionId", DbType.Int32, _InspId);
            objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _VesselId);
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt76.Load(dr);
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
        return dt76;
    }
    public static DataTable GetCrewIdFromCrewNo(string CrewNo)
    {
        string procedurename = "PR_GetCrewId";
        DataTable dt75 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        try
        {
            objDatabase.AddInParameter(objDbCommand, "@CrewNum", DbType.String, CrewNo);

            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt75.Load(dr);
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
        return dt75;
    }
    public static DataTable GetEmailIdofSuperintendent(string _SuptId)
    {
        string procedurename = "PR_GetEmailOfSupt";
        DataTable dt81 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@SuptId", DbType.String, _SuptId);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt81.Load(dr);
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
        return dt81;
    }
    public static DataSet GetSuptDetails(int _InspectionDueId)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_SuptDetailByInspDueId";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        //objDatabase.AddInParameter(objDbCommand, "@RandomInspection", DbType.String, RandomInspection);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspectionDueId);

        try
        {
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
        }
        catch (Exception ex)
        {
            // if error is coming throw that error
            throw ex;
        }
        finally
        {
            // after used dispose dataset and commmand
            objDataset.Dispose();
            objDbCommand.Dispose();
        }
        // Note: connection was closed by ExecuteDataSet method call 
        return objDataset;
    }
    public static string ExportDatatable(HttpResponse Response, DataSet ds)
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();
        dg.HeaderStyle.Font.Bold = true;
        dg.HeaderStyle.Font.Size = 11;
        dg.HeaderStyle.Font.Name = "Arial";
        dg.CellSpacing = 5;
        dg.CellPadding = 2;
        dg.BorderWidth = 0;
        dg.RowStyle.Font.Size = 10;
        dg.RowStyle.Font.Name = "Arial";
        dg.DataSource = ds;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        return stringWrite.ToString();
    }
    public static DataTable CheckSuperintendent(int _inspdueid)
    {
        string procedurename = "PR_CheckSuperintendent";
        DataTable dt83 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@INSPECTIONDUEID", DbType.Int32, _inspdueid);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt83.Load(dr);
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
        return dt83;
    }
}
