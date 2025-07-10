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
/// Summary description for TravelAgent
/// </summary>
public class TravelAgent
{
    public static DataTable selectDataStatus()
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
    public static DataTable selectDataTravelAgentDetails()
    {
        string procedurename = "SelectTravelAgentDetails";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
    public static DataTable selectDataTravelAgentDetailsByTravelAgentId(int _TravelAgentId)
    {
        string procedurename = "SelectTravelAgentDetailsByTravelAgentId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TravelAgentId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void insertUpdateTravelAgentDetails(string _strProc, int _TravelAgentid, string _TravelAgentEmail,string _PortAgentCompany, string _PortAgentContactPerson, string _PortAgentAddress, string _PortAgentPhone, string _PortAgentMobile,string _PortAgentFax,string VendorNo, int _createdby, int _modifiedby, char _status)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TravelAgentId", DbType.Int32, _TravelAgentid);
        oDatabase.AddInParameter(odbCommand, "@TravelAgentEmail", DbType.String, _TravelAgentEmail);
        oDatabase.AddInParameter(odbCommand, "@Company", DbType.String, _PortAgentCompany);
        oDatabase.AddInParameter(odbCommand, "@ContactPerson", DbType.String, _PortAgentContactPerson);
        oDatabase.AddInParameter(odbCommand, "@Address", DbType.String, _PortAgentAddress);
        oDatabase.AddInParameter(odbCommand, "@Phone", DbType.String, _PortAgentPhone);
        oDatabase.AddInParameter(odbCommand, "@Mobile", DbType.String, _PortAgentMobile);
        oDatabase.AddInParameter(odbCommand, "@Fax", DbType.String, _PortAgentFax);
        oDatabase.AddInParameter(odbCommand, "@VendorNo", DbType.String, VendorNo);
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@StatusId", DbType.String, _status);

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
    public static void deleteTravelAgentDetails(string _strProc, int _TravelAgentid, int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();


        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@TravelAgentId", DbType.Int32, _TravelAgentid);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _ModifiedBy);


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
