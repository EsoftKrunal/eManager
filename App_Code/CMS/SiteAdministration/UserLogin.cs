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


public class UserLogin
{
    public static DataTable selectDataRoleNameDetails()
    {
        string procedurename = "SelectRoleName";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable selectDataDepartmentNameDetails()
    {
        string procedurename = "SelectDepartment";
        DataTable dt41 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt41.Load(dr);
        }

        return dt41;
    }
    public static DataTable selectDataUserLoginDetails()
    {
        string procedurename = "SelectUserLoginDetails";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectDataStatusDetails()
    {
        string procedurename = "Selectstatus";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
       
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable selectDataUserLoginDetailsByUserLoginId(int _LoginId)
    {
        string procedurename = "SelectUserLoginDetailsByLoginId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectadminrole(int _LoginId)
    {
        string procedurename = "selectuseradminrolebyloginid";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@loginid", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static void insertUpdateUserLoginDetails(string _strProc, int _Loginid, string _UserId, int _RoleId, string _Password, string _firstname, string _lastname, string _dob, string _email, string _DepartmentId, string _RecruitingOfcId, int _createdby, int _modifiedby, char _statusId, char _Systemuser)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _Loginid);
        oDatabase.AddInParameter(odbCommand, "@UserId", DbType.String, _UserId);
        oDatabase.AddInParameter(odbCommand, "@RoleId", DbType.Int32, _RoleId);
        oDatabase.AddInParameter(odbCommand, "@Password", DbType.String, _Password);
        oDatabase.AddInParameter(odbCommand, "@FirstName", DbType.String, _firstname);
        oDatabase.AddInParameter(odbCommand, "@LastName", DbType.String, _lastname);
        oDatabase.AddInParameter(odbCommand, "@DateOfBirth", DbType.String, _dob);
        oDatabase.AddInParameter(odbCommand, "@Email", DbType.String, _email);
        oDatabase.AddInParameter(odbCommand, "@DepartmentId", DbType.String, _DepartmentId);
        oDatabase.AddInParameter(odbCommand, "@RecruitingOfficeId", DbType.String, _RecruitingOfcId);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _statusId);
        oDatabase.AddInParameter(odbCommand, "@SuperUser", DbType.String, _Systemuser);

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
    public static void deleteUserLoginDetailsById(string _strProc, int _loginid, int _modifiedby)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, _loginid);
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
    public static string getUserName(int LoginId)
    {
        string uname;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("getUserName");
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, LoginId);
        oDatabase.AddOutParameter(odbCommand, "@Result", DbType.String, 200);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                uname = oDatabase.GetParameterValue(odbCommand, "@Result").ToString();
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
        return uname;
    }
    public static DataTable getEmailAddress(int _loginId)
    {
        string procedurename = "get_EmailAddress";
        DataTable dt33 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt33.Load(dr);
        }

        return dt33;
    }
    public static DataTable selectsuperuser(int _LoginId)
    {
        string procedurename = "selectsuperuserbyloginid";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@loginid", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }

        return dt12;
    }
    public static int UpdatePassword(int Loginid, string Password,string OldPassword)
    {
        int status;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand("UpdatePassword");
        oDatabase.AddInParameter(odbCommand, "@LoginId", DbType.Int32, Loginid);
        oDatabase.AddInParameter(odbCommand, "@Password", DbType.String, Password);
        oDatabase.AddInParameter(odbCommand, "@OldPassword", DbType.String, OldPassword);
        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.String, 1);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                status = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
                scope.Complete();
                return status;
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
    public static DataTable MappingRecordReferences(int _RefType, string _RefNum)
    {
        string procedurename = "sp_TransferStatus_MappingOfRecordReferences";
        DataTable dtmap = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@ReferenceType", DbType.Int32, _RefType);
        objDatabase.AddInParameter(objDbCommand, "@ReferenceNumber", DbType.String, _RefNum);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtmap.Load(dr);
        }

        return dtmap;
    }
}
