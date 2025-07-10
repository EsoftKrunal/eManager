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

public class VesselSearch
{
    public static DataTable selectDataVesselTypeName()
    {
        string procedurename = "SelectVesselTypeNameDDL";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }

        return dt1;
    }
    public static DataTable selectDataVesselDetails(int _loginid,int _vesselTypeId ,int _vesselstatusid, int _mgmttypeid,int _ownerid,int _ownerPoolid )
    {
        string procedurename = "selectvesseldetailsingrid";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@LoginId", DbType.Int32, _loginid);
        objDatabase.AddInParameter(objDbCommand, "@VesselTypeId", DbType.Int32, _vesselTypeId);
        objDatabase.AddInParameter(objDbCommand, "@VesselStatusId", DbType.Int32, _vesselstatusid);
        objDatabase.AddInParameter(objDbCommand, "@MgmtTypeId", DbType.Int32, _mgmttypeid);
        objDatabase.AddInParameter(objDbCommand, "@OwnerId", DbType.Int32, _ownerid);
        objDatabase.AddInParameter(objDbCommand, "@OwnerPoolId", DbType.Int32, _ownerPoolid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }

        return dt2;
    }
    public static DataTable selectDataVesselDetailsByVesselId(int _vesselId)
    {
        string procedurename = "SelectVesselDetailsByVesselId";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static void deleteVesselDetailsById(string _strProc, int _vesselId,int _ModifiedBy)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@vesselid", DbType.Int32, _vesselId);
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
