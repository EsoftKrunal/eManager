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
using System.IO;

/// <summary>
/// Summary description for SafetyInspectionReport
/// </summary>
public class SafetyInspectionReport
{
    public SafetyInspectionReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable SelectSafetyInspectionReportDetails(int _InspDueId)
    {
        //string procedurename = "PR_RPT_SafetyInspection";
        DataTable dt = new DataTable();

        //Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
        //objDbCommand.CommandTimeout = 60; 
        //using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        //{
        //    dt.Load(dr);
        //}
        //return dt;

        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("PR_RPT_SafetyInspection", myConnection);
        myCommand.CommandTimeout = 120;
        SqlDataAdapter adp = new SqlDataAdapter(myCommand); 
        myCommand.CommandTimeout = 120;
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmInspDueId = new SqlParameter("@InspectionDueId", SqlDbType.Int);
        prmInspDueId.Value = _InspDueId;
        myCommand.Parameters.Add(prmInspDueId);
        adp.SelectCommand.CommandTimeout = 120;
        adp.Fill(dt);
        return dt;
    }
    public static DataTable SelectSafetyInspection_Report(int _InspDueId)
    {
        //string procedurename = "PR_RPT_SafetyInspection_Report";
        DataTable dt = new DataTable();

        //Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
        //objDbCommand.CommandTimeout = 60; 
        //using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        //{
        //    dt2.Load(dr);
        //}

        //return dt2;


        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("PR_RPT_SafetyInspection_Report", myConnection);
        myCommand.CommandTimeout = 120;
        SqlDataAdapter adp = new SqlDataAdapter(myCommand);
        myCommand.CommandTimeout = 120;
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmInspDueId = new SqlParameter("@InspectionDueId", SqlDbType.Int);
        prmInspDueId.Value = _InspDueId;
        myCommand.Parameters.Add(prmInspDueId);
        adp.SelectCommand.CommandTimeout = 120;
        adp.Fill(dt);
        return dt;
    }
    public static DataTable SelectSafetyInspectionChildReportDetails(int _InspDueId)
    {
        //string procedurename = "PR_RPT_SafetyInspection_Child";
        DataTable dt = new DataTable();

        //Database objDatabase = DatabaseFactory.CreateDatabase();
        //DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        //objDatabase.AddInParameter(objDbCommand, "@InspectionDueId", DbType.Int32, _InspDueId);
        //objDbCommand.CommandTimeout = 60; 
        //using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        //{
        //    dt1.Load(dr);
        //}

        //return dt1;

        string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("PR_RPT_SafetyInspection_Child", myConnection);
        myCommand.CommandTimeout = 120;  
        SqlDataAdapter adp = new SqlDataAdapter(myCommand);
        myCommand.CommandTimeout = 120;
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmInspDueId = new SqlParameter("@InspectionDueId", SqlDbType.Int);
        prmInspDueId.Value = _InspDueId;
        myCommand.Parameters.Add(prmInspDueId);
        adp.SelectCommand.CommandTimeout = 120;
        adp.Fill(dt);
        return dt;

    }
    public static void TruncateImages()
    {
        string procedurename = "PR_Truncate_TranscationReportImage";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDbCommand.CommandTimeout = 60; 
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
    }
    public static void ImageSave(int insdueid, string srnum, string path, int chldtblid)
    {
        long intImageSize;
        String strImageType;
        Stream ImageStream;
        String connString;
        connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();  // "Data Source=172.30.1.105;Initial Catalog=ShipsoftDev;User Id=sa;Password=Esoft!@#$%^;";
        FileStream fs = File.OpenRead(path);
        intImageSize = fs.Length;// FileUpload1.PostedFile.ContentLength;
        Byte[] ImageContent = new Byte[intImageSize];
        fs.Read(ImageContent, 0, ImageContent.Length);
        fs.Close();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("AddImage", myConnection);
        myCommand.CommandTimeout = 60; 
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmInspDueId = new SqlParameter("@InspectionDueId", SqlDbType.Int);
        SqlParameter prmSrNumber = new SqlParameter("@SrNum", SqlDbType.VarChar);
        SqlParameter prmPersonImage = new SqlParameter("@ii", SqlDbType.Image);
        SqlParameter prmChildTblId = new SqlParameter("@ChildTableId", SqlDbType.Int);
        prmInspDueId.Value = insdueid;
        prmSrNumber.Value = srnum;
        prmPersonImage.Value = ImageContent;
        prmChildTblId.Value = chldtblid;
        myCommand.Parameters.Add(prmInspDueId);
        myCommand.Parameters.Add(prmSrNumber);
        myCommand.Parameters.Add(prmPersonImage);
        myCommand.Parameters.Add(prmChildTblId);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }
    public static void VslImageSave(string path)
    {
        long intImageSize;
        String strImageType;
        Stream ImageStream;
        String connString;
        connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();  // "Data Source=172.30.1.105;Initial Catalog=ShipsoftDev;User Id=sa;Password=Esoft!@#$%^;";
        FileStream fs = File.OpenRead(path);
        intImageSize = fs.Length;// FileUpload1.PostedFile.ContentLength;
        Byte[] ImageContent = new Byte[intImageSize];
        fs.Read(ImageContent, 0, ImageContent.Length);
        fs.Close();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("AddVesselImage", myConnection);
        myCommand.CommandTimeout = 60;
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmPersonImage = new SqlParameter("@vslii", SqlDbType.Image);
        prmPersonImage.Value = ImageContent;
        myCommand.Parameters.Add(prmPersonImage);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }
}
