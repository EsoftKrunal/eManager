using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using ShipSoft.CrewManager.DataAccessLayer;
using System.Configuration;
public enum MyValueType
{
    Int, Decimal, String, Date, DateTime, Bool
}
public class Common
{
    static String[] Procedures;
    static int[] ParameterLength;
    public static string Prefix = "";
    static MyParameter[] Parameters;
    static Parameter_Mappings[] ParameterMapping;
    static SqlTransaction BulkTrans;
    static SqlConnection BulkConn;
    public static string ErrMsg = "";
    static String ConnectionString = AppConfiguration.ConnectionString;
    public static string getLastError()
    {
        string Message = Common.ErrMsg.Replace("\"", "`").Replace("'", "`").Replace("\n", " ").Replace("\r", " ");
        return Message;
    }
    //------
    public static void Set_Procedures(params string[] Dummy)
    {
        Procedures = Dummy;
    }
    public static void Set_ParameterLength(params int[] Dummy)
    {
        ParameterLength = Dummy;
    }
    public static void Set_Parameters(params MyParameter[] Dummy)
    {
        Parameters = Dummy;
    }
    public static void Set_Mappings(params Parameter_Mappings[] Dummy)
    {
        ParameterMapping = Dummy;
    }
    public static void Set_ConnectionString(String Dummy)
    {
        ConnectionString = Dummy;
    }
    public static Parameter_Mappings[] Get_Mapping_For_Procedure(string _ProcedureName)
    {
        int Count = 0;
        try
        {
            for (int i = 0; i <= ParameterMapping.Length - 1; i++)
            {
                if (ParameterMapping[i].SourceProcedureName.Trim() == _ProcedureName.Trim())
                {
                    Count = Count + 1;
                }
            }
        }
        catch { }
        Parameter_Mappings[] Temp = new Parameter_Mappings[Count];
        try
        {
            for (int i = 0; i <= ParameterMapping.Length - 1; i++)
            {
                if (ParameterMapping[i].SourceProcedureName.Trim() == _ProcedureName.Trim())
                {
                    Temp[i] = new Parameter_Mappings(ParameterMapping[i].SourceProcedureName, ParameterMapping[i].SourceParameterName, ParameterMapping[i].DestProcedureName, ParameterMapping[i].DestRow, ParameterMapping[i].DestCol);
                }
            }
        }
        catch { }
        return Temp;
    }

    //------CONSTRUCTOR ---
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //------COMMON PROCEDURE WHICH IS CALL AT EACH PAGE POST BACK ---
    public static void ServerCommon()
    {
        ConnectionString = AppConfiguration.ConnectionString;
    }
    //------COMMON PROCEDURE WHICH IS CALL FOR EXECUTING ANY SQL COMMAND FOR SAVE/UPDATE/DELETE ---
    public static void BulkTransStart()
    {
        BulkConn = new SqlConnection(ConnectionString);
        BulkConn.Open();
        BulkTrans = BulkConn.BeginTransaction();

    }
    public static bool BulkTransEnd()
    {
        try
        {
            BulkTrans.Commit();
            BulkConn.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool Execute_Procedures_IUDBulk(DataSet ResultBulkTrans)
    {
        int Count = 0;
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];

        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = BulkConn;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            Commands[i].Transaction = BulkTrans;
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                DataTable dt = new DataTable();
                Parameter_Mappings[] TempMapping = Get_Mapping_For_Procedure(Procedures[i].ToString());
                for (int j = 0; j <= TempMapping.Length - 1; j++)
                {
                    Commands[i].Parameters[TempMapping[j].SourceParameterName].Value = ResultBulkTrans.Tables[TempMapping[j].DestProcedureName + "_Result"].Rows[TempMapping[j].DestRow][TempMapping[j].DestCol];
                }
                dt.Load(Commands[i].ExecuteReader());
                dt.TableName = Procedures[i].ToString() + "_Result";
                ResultBulkTrans.Tables.Add(dt);
            }
            return true;
        }
        catch (Exception exx)
        {
            return false;
        }
        finally
        {
        }
    }
    public static bool Execute_Procedures_IUD(DataSet Result)
    {

        SqlConnection myConnection = new SqlConnection(ConnectionString);
        int Count = 0;
        myConnection.Open();
        SqlTransaction myTrans = myConnection.BeginTransaction();
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];

        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = myConnection;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            Commands[i].Transaction = myTrans;
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                DataTable dt = new DataTable();
                Parameter_Mappings[] TempMapping = Get_Mapping_For_Procedure(Procedures[i].ToString());
                for (int j = 0; j <= TempMapping.Length - 1; j++)
                {
                    Commands[i].Parameters[TempMapping[j].SourceParameterName].Value = Result.Tables[TempMapping[j].DestProcedureName + "_Result"].Rows[TempMapping[j].DestRow][TempMapping[j].DestCol];
                }
                dt.Load(Commands[i].ExecuteReader());
                dt.TableName = Procedures[i].ToString() + "_Result";
                Result.Tables.Add(dt);
            }
            myTrans.Commit();
            ErrMsg = "";
            return true;
        }
        catch (Exception exx)
        {
            ErrMsg = exx.Message;
            return false;
        }
        finally
        {
            myConnection.Close();
        }

    }
    public static bool Execute_Procedures_IUD_CMS(DataSet Result)
    {

        SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString);
        int Count = 0;
        myConnection.Open();
        SqlTransaction myTrans = myConnection.BeginTransaction();
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];

        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = myConnection;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            Commands[i].Transaction = myTrans;
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                DataTable dt = new DataTable();
                Parameter_Mappings[] TempMapping = Get_Mapping_For_Procedure(Procedures[i].ToString());
                for (int j = 0; j <= TempMapping.Length - 1; j++)
                {
                    Commands[i].Parameters[TempMapping[j].SourceParameterName].Value = Result.Tables[TempMapping[j].DestProcedureName + "_Result"].Rows[TempMapping[j].DestRow][TempMapping[j].DestCol];
                }
                dt.Load(Commands[i].ExecuteReader());
                dt.TableName = Procedures[i].ToString() + "_Result";
                Result.Tables.Add(dt);
            }
            myTrans.Commit();
            ErrMsg = "";
            return true;
        }
        catch (Exception exx)
        {
            ErrMsg = exx.Message;
            return false;
        }
        finally
        {
            myConnection.Close();
        }

    }
    //------COMMON PROCEDURE WHICH IS CALL FOR SELECT DATA FROM SQL COMMAND ---
    public static DataTable Execute_Procedures_Select_ByQuery(string Query)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        SqlDataAdapter Adp = new SqlDataAdapter();
        SqlCommand Command = new SqlCommand(Query, myConnection);
        Command.CommandTimeout = 300;
        Adp.SelectCommand = Command;
        Adp.Fill(RetValue, "Result");
        try
        {
            return RetValue.Tables[0];
        }
        catch { return null; }
    }
    public static DataTable Execute_Procedures_Select_ByQueryCMS(string Query)
    {
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
        {
            DataSet RetValue = new DataSet();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand(Query, myConnection);
            Command.CommandTimeout = 300;
            Adp.SelectCommand = Command;
            Adp.Fill(RetValue, "Result");
            try
            {
                return RetValue.Tables[0];
            }
            catch { return null; }
        }
    }
    public static DataSet Execute_Procedures_Select()
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        SqlDataAdapter Adp = new SqlDataAdapter();
        int Count = 0;
        myConnection.Open();
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];
        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = myConnection;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Adp.SelectCommand = Commands[i];
                Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
            }
            ErrMsg = "";
            return RetValue;

        }
        catch (Exception exx)
        {
            ErrMsg = exx.Message;
            return null;
        }
        finally
        {
            myConnection.Close();
        }

    }
    public static DataSet Execute_Procedures_Select_CMS()
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString);
        SqlDataAdapter Adp = new SqlDataAdapter();
        int Count = 0;
        myConnection.Open();
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];
        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = myConnection;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Adp.SelectCommand = Commands[i];
                Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
            }
            ErrMsg = "";
            return RetValue;

        }
        catch (Exception exx)
        {
            ErrMsg = exx.Message;
            return null;
        }
        finally
        {
            myConnection.Close();
        }

    }
    public static void UpdateDataTableById(ref DataTable dt, string ColumnName, object Value)
    {
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][ColumnName] = Value;
        }
    }
    public static DataSet Execute_Procedures_Select(string Message)
    {
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        SqlDataAdapter Adp = new SqlDataAdapter();
        int Count = 0;
        myConnection.Open();
        SqlCommand[] Commands = new SqlCommand[Procedures.Length];
        // Enlist the command in the current transaction.
        for (int i = 0; i <= Commands.Length - 1; i++)
        {
            Commands[i] = new SqlCommand();
            Commands[i].Connection = myConnection;
            Commands[i].CommandTimeout = 300;
            Commands[i].CommandType = CommandType.StoredProcedure;
            Commands[i].CommandText = Prefix + Procedures[i].ToString();
            for (int j = 0; j <= ParameterLength[i] - 1; j++)
            {
                Commands[i].Parameters.Add(new SqlParameter(Parameters[Count].Name, Parameters[Count].Value));
                Count++;
            }
        }
        try
        {
            for (int i = 0; i <= Commands.Length - 1; i++)
            {
                Adp.SelectCommand = Commands[i];
                Adp.Fill(RetValue, Procedures[i].ToString() + "_Result");
            }
            ErrMsg = "";
            return RetValue;
        }
        catch (Exception exx)
        {
            ErrMsg = exx.Message;
            return null;
        }
        finally
        {
            myConnection.Close();
        }

    }

    public static Int32 CastAsInt32(object Data)
    {
        decimal res = 0;
        try
        {
            if (Data != null)
            {
                res = decimal.Parse(Data.ToString());
                return Convert.ToInt32(res);
            }
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }
    public static decimal CastAsDecimal(object Data)
    {
        decimal res = 0;
        try
        {
            res = decimal.Parse(Data.ToString());
        }
        catch
        { }
        return res;
    }
    public static object CastAsDate(object Data)
    {
        DateTime res;
        try
        {
            res = DateTime.Parse(Data.ToString());
            return Convert.ToDateTime(res.ToString("MM/dd/yyyy"));
        }
        catch
        {
            return null;
        }
    }
    public static string ToDateString(object Data)
    {
        DateTime res;
        try
        {
            res = DateTime.Parse(Data.ToString());
            return res.ToString("dd-MMM-yyyy");
        }
        catch
        {
            return "";
        }
    }
    public static string ConvertToCSV(string[] Data)
    {
        string ret = "";
        for (int i = 0; i <= Data.Length - 1; i++)
        {
            if (i == 0)
                ret = Data[0];
            else
                ret = ret + "," + Data[i];
        }
        return ret;
    }

    //---------------------
}

