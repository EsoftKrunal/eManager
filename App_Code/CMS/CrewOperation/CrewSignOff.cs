using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CrewSignOff
/// </summary>
public class CrewSignOff
{
    public CrewSignOff()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable get_PendingCrewSignOff(int LoginId)
      {
        string procedurename = "get_PendingCrewSignOff";
        DataTable dt11 = new DataTable(); 
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, LoginId);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static DataTable Crw_signoff(int x)
    {
        string procedurename = "CrewSignOff_Reliver";
        DataTable dt3 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable chksignoff(int x)
    {
        string procedurename = "Check_sign_off";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, x);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataSet insertdata(int _crew, DateTime dtsignoff,int port,int signoffreason,string expjoindt,string remark,int _loginid,int _PortCallId)  
    {
        string RValue;
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Insert_Sign_off_Details";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crew);
        objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, dtsignoff);
        objDatabase.AddInParameter(objDbCommand, "@Port", DbType.Int32, port);
        objDatabase.AddInParameter(objDbCommand, "@SignOfReason", DbType.Int32, signoffreason);
        objDatabase.AddInParameter(objDbCommand, "@ExpJoinDt", DbType.String,expjoindt);
        objDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, remark);
        objDatabase.AddInParameter(objDbCommand, "@loginid", DbType.Int32, _loginid);
        objDatabase.AddInParameter(objDbCommand, "@PortCallId", DbType.Int32, _PortCallId);
        
        try
        {
            // execute command and get records
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
        //NewId = RValue;
        return objDataset;

    }
    public static DataTable Crw_signoffgetdate()
    {
        string procedurename = "SignOff_getdate";
        DataTable dt4 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable Select_CrewMemberSignOffDetails(int _Crewid)
    {
        string procedurename = "Select_CrewMemberSignOffDetails";
        DataTable dt5 = new DataTable();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, _Crewid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataSet insertcrewsignoffdata(int _crewid, DateTime _dtsignoff, string _expjoindt)
    {
        string RValue;
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "Update_CrewMemberSignOffDetails";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();

        objDatabase.AddInParameter(objDbCommand, "@CrewId", DbType.Int32, _crewid);
        objDatabase.AddInParameter(objDbCommand, "@SignOffDate", DbType.DateTime, _dtsignoff);
        if (_expjoindt == "")
        {
            objDatabase.AddInParameter(objDbCommand, "@Expjoindt", DbType.DateTime, null);
        }
        else
        {
            objDatabase.AddInParameter(objDbCommand, "@Expjoindt", DbType.DateTime, _expjoindt);
        }

        try
        {
            // execute command and get records
            objDataset = objDatabase.ExecuteDataSet(objDbCommand);
                    }
        catch (Exception ex)
        {
            // if error is coming throw that error

            throw ex;
        }
        finally
        {
           
            objDataset.Dispose();
            objDbCommand.Dispose();
        }

        // Note: connection was closed by ExecuteDataSet method call 

        return objDataset;

    }


}
