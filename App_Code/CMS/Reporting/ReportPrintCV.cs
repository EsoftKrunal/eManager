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
/// Summary description for ReportPrintCV
/// </summary>
public class ReportPrintCV
{
    public static DataTable selectCVDetails(int crewid)
    {
        string procedurename = "PrintCVDetails";
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
    public static DataTable selectCVEducationalDetails(int crewid)
    {
        string procedurename = "PrintCVEducationalDetails";
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
    public static DataTable selectCVLicencesDetails(int crewid)
    {
        string procedurename = "PrintCVLicencesDetails";
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
    public static DataTable selectCVDCEDetails(int crewid)
    {
        string procedurename = "PrintCVDCEDetails";
        DataTable dt24 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt24.Load(dr);
        }
        return dt24;
    }
    public static DataTable selectCVCourseDetails(int crewid)
    {
        string procedurename = "PrintCVCourseDetails";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }
        return dt3;
    }
    public static DataTable selectCVExperienceDetails(int crewid)
    {
        string procedurename = "PrintCVExperienceDetails";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }
        return dt4;
    }
    //public static void Insert(byte[] a)
    //{
       
    //    Database oDatabase = DatabaseFactory.CreateDatabase();

    //    DbCommand odbCommand = oDatabase.GetStoredProcCommand("AddImage");
    //    //DbCommand odbCommand = oDatabase.GetSqlStringCommand("insert into ACrewImage values ('" + a + "')");
        
    //    oDatabase.AddInParameter(odbCommand, "@ii", DbType.Byte ,a);
             
        
    //    using (TransactionScope scope = new TransactionScope())
    //    {
    //        try
    //        {
    //            // execute command and get records
    //            oDatabase.ExecuteNonQuery(odbCommand);
    //           // _rowId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
    //            scope.Complete();
    //        }
    //        catch (Exception ex)
    //        {
    //            // if error is coming throw that error
    //            throw ex;
    //        }
    //        finally
    //        {
    //            // after used dispose commmond            
    //            odbCommand.Dispose();
    //        }
    //    }
    //}
    public static void ImageSave(string path,string NoImage)
    {
        long intImageSize;
        String strImageType;
        Stream ImageStream;
        String connString;
        FileStream fs ;
        connString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();  
        if(System.IO.File.Exists(path))
            fs = File.OpenRead(path);
        else
            fs = File.OpenRead(NoImage);
        intImageSize = fs.Length;// FileUpload1.PostedFile.ContentLength;
        Byte[] ImageContent = new Byte[intImageSize];
        fs.Read(ImageContent, 0, ImageContent.Length);
        fs.Close();
        SqlConnection myConnection = new SqlConnection(connString);
        SqlCommand myCommand = new SqlCommand("AddImage", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter prmPersonImage = new SqlParameter("@ii", SqlDbType.Image);
        prmPersonImage.Value = ImageContent;
        myCommand.Parameters.Add(prmPersonImage);
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }
    public static DataTable selectCrewIdCrewNumber(string _crewNumber)
    {
        string procedurename = "SelectCrewIdCrewNumberInMiscCost";
        DataTable dt22 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewNumber", DbType.String, _crewNumber);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt22.Load(dr);
        }

        return dt22;
    }
    public static DataTable selectCrewnumberCrewid(int crewid)
    {
        string procedurename = "SelectCrewnumberCrewid";
        DataTable dt23 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt23.Load(dr);
        }

        return dt23;
    }
}
