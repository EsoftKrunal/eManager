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

public class Inspection_Master
{
    public static string ErrMsg = "";
    public static DataTable InspectionDetails(int _id, string _code, string _name, int _inspgroup, int _interval, int _tolerance, int _inspectiondept, string _randominsp, int _createdby, int _modifiedby, string _transtype, int _followupcategory,string _Color)
    {
        string procedurename = "PR_ADMS_Inspection";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _id);
        objDatabase.AddInParameter(objDbCommand, "@Code", DbType.String, _code);
        objDatabase.AddInParameter(objDbCommand, "@Name", DbType.String, _name);
        objDatabase.AddInParameter(objDbCommand, "@InspectionGroup", DbType.Int32, _inspgroup);
        objDatabase.AddInParameter(objDbCommand, "@Interval", DbType.Int32, _interval);
        objDatabase.AddInParameter(objDbCommand, "@Tolerance", DbType.Int32, _tolerance);
        objDatabase.AddInParameter(objDbCommand, "@InspectionDept", DbType.Int32, _inspectiondept);
        objDatabase.AddInParameter(objDbCommand, "@RandomInspection", DbType.String, _randominsp);
        objDatabase.AddInParameter(objDbCommand, "@CreatedBy", DbType.Int32, _createdby);
        objDatabase.AddInParameter(objDbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        objDatabase.AddInParameter(objDbCommand, "@TransType", DbType.String, _transtype);
        objDatabase.AddInParameter(objDbCommand, "@FollowUpCategory", DbType.Int32, _followupcategory);
        objDatabase.AddInParameter(objDbCommand, "@Color", DbType.String, _Color);
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
            }
            return dt1;
        }
        catch (SqlException se)
        {
            dt1.Dispose();
            ErrMsg = se.Message;
            return null;
        }
    }
    public static DataSet getMasterData(string TableName, string Field1, string Field2)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_GetMaster";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@MasterName", DbType.String, TableName);
        objDatabase.AddInParameter(objDbCommand, "@Field1", DbType.String, Field1);
        objDatabase.AddInParameter(objDbCommand, "@Field2", DbType.String, Field2);
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
    public static DataSet getMasterDataforInspection(string TableName, string Field1, string Field2, int loginId = 0)
    {
        Database objDatabase = DatabaseFactory.CreateDatabase();
        string procedurename = "PR_GetMaster_ForInspection";
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        DataSet objDataset = new DataSet();
        objDatabase.AddInParameter(objDbCommand, "@MasterName", DbType.String, TableName);
        objDatabase.AddInParameter(objDbCommand, "@Field1", DbType.String, Field1);
        objDatabase.AddInParameter(objDbCommand, "@Field2", DbType.String, Field2);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, loginId);
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
}
