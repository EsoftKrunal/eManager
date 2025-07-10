using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Web.SessionState;
using System.Web.UI;
using System.Data.Common;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Summary description for CrewPersonalInsertData
/// </summary>
public class CrewPersonalInsertData
{
    public CrewPersonalInsertData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void insertPersonalDetails(string _strProc,string _fName, string _mName,string _lname,string _dob,int _nationality,int _gender)
    {
         Database oDatabase = DatabaseFactory.CreateDatabase();
         DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
         oDatabase.AddInParameter(odbCommand, "@firstname", DbType.String, _fName);
         oDatabase.AddInParameter(odbCommand, "@middlename", DbType.String, _lname);
         oDatabase.AddInParameter(odbCommand, "@lastName", DbType.String, _mName);
         if (_dob == "")
         {
             oDatabase.AddInParameter(odbCommand, "@DateOfBirth", DbType.DateTime, null);
         }
         else
         {
             oDatabase.AddInParameter(odbCommand, "@DateOfBirth", DbType.DateTime, Convert.ToDateTime(_dob));
         }
         oDatabase.AddInParameter(odbCommand, "@Gender", DbType.Int16 , _gender);
         oDatabase.AddInParameter(odbCommand, "@Nationality", DbType.Int16, _nationality);
         oDatabase.AddInParameter(odbCommand, "@CreatedBY", DbType.Int16,1) ;
        
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
