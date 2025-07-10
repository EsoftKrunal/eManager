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
/// Summary description for VesselDetailsGeneral
/// </summary>
public class VesselDetailsGeneral
{
    public static DataTable selectDataFlag()
    {
        string procedurename = "SelectFlagName";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt1.Load(dr);
        }
        return dt1;
    }
    public static DataTable selectDataMgmtType()
    {
        string procedurename = "SelectManagementType";
        DataTable dt2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt2.Load(dr);
        }
        return dt2;
    }
    public static DataTable selectDataOwnerName()
    {
        string procedurename = "SelectOwnerName";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }

    public static DataTable selectDataAccountCompany()
    {
        string procedurename = "SelectAccountCompany";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectDataOwnerPoolName()
    {
        string procedurename = "SelectOwnerPoolName";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
    public static DataTable selectDataCharterer()
    {
        string procedurename = "SelectCharterer";
        DataTable dt4 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt4.Load(dr);
        }

        return dt4;
    }
    public static DataTable selectDataShipManager()
    {
        string procedurename = "SelectShipManager";
        DataTable dt5 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt5.Load(dr);
        }

        return dt5;
    }
    public static DataTable selectDataPIClub()
    {
        string procedurename = "SelectPIClub";
        DataTable dt6 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt6.Load(dr);
        }

        return dt6;
    }
    public static DataTable selectCrewNationality()
    {
        string procedurename = "SelectNatioalityGroupId";
        DataTable dt7 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt7.Load(dr);
        }

        return dt7;
    }
    public static DataTable selectWageScale(int VeselId)
    {
        string procedurename = "SelectWageScalesByVesselId";
        DataTable dt8 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VeselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt8.Load(dr);
        }

        return dt8;
    }
    public static DataTable selectDataVesselStatus()
    {
        string procedurename = "SelectVesselstatus";
        DataTable dt9 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt9.Load(dr);
        }
        return dt9;
    }
    public static DataTable selectDataVesselTypeName()
    {
        string procedurename = "SelectVesselTypeNameDDL";
        DataTable dt12 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt12.Load(dr);
        }

        return dt12;
    }
    public static DataTable selectDataPortOfRegistryDetails()
    {
        string procedurename = "SelectPortOfRegistryName";
        DataTable dt13 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt13.Load(dr);
        }

        return dt13;
    }
    public static DataTable selectDataCurrentClassDetails()
    {
        string procedurename = "SelectCurrentClassName";
        DataTable dt14 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt14.Load(dr);
        }

        return dt14;
    }
    public static void insertUpdateVesselGeneralDetails(string _strProc, out int _ReturnValue, int _VesselId, string _VesselName, int _FlagStateId, string _VesselCode, int _ManagementTypeId, int _OwnerId, int _ChartererId, int _ShipManagerId, int _PNIClubId, string _NationalityGroupId,string _NationalityGroupIdRat, int _VesselStatusId, int _VesselTypeId, string _LRIMONumber, int _portOfRegistryId,
                                                string _officialNumber, string _MMISMNumber, int _currentClassId,
                                                string _yearBuilt, string _age, string _yard,
                                                string _ltd, string _teu, string _length, string _breath, string _depth,
                                                string _draught, string _effectiveDate, int _createdby, int _modifiedby, string Gemail,bool VettingRequired, string _callsign, int _OwnerAgentId, int _MLCOwnerId, int _ManningAgentId, string _AccountCompany, bool _IsMultiAccount, int _FleetId, string _POIssuingAccountCompany)
    {
        _ReturnValue = -1;
        Database oDatabase = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);

        oDatabase.AddOutParameter(odbCommand, "@ReturnValue", DbType.Int32, _ReturnValue);
        oDatabase.AddInParameter(odbCommand, "@VesselId", DbType.Int32, _VesselId);
        oDatabase.AddInParameter(odbCommand, "@VesselName", DbType.String, _VesselName);
        oDatabase.AddInParameter(odbCommand, "@FlagStateId", DbType.Int32, _FlagStateId);
        oDatabase.AddInParameter(odbCommand, "@VesselCode", DbType.String, _VesselCode);
        oDatabase.AddInParameter(odbCommand, "@ManagementTypeId", DbType.Int32, _ManagementTypeId);
        oDatabase.AddInParameter(odbCommand, "@OwnerId", DbType.Int32, _OwnerId);
        oDatabase.AddInParameter(odbCommand, "@ChartererId", DbType.Int32, _ChartererId);
        oDatabase.AddInParameter(odbCommand, "@ShipManagerId", DbType.Int32, _ShipManagerId);
        oDatabase.AddInParameter(odbCommand, "@PNIClubId", DbType.Int32, _PNIClubId);
        oDatabase.AddInParameter(odbCommand, "@NationalityGroupId", DbType.String, _NationalityGroupId);
        oDatabase.AddInParameter(odbCommand, "@NationalityGroupIdRat", DbType.String, _NationalityGroupIdRat);
        oDatabase.AddInParameter(odbCommand, "@VesselStatusId", DbType.Int32, _VesselStatusId);
        oDatabase.AddInParameter(odbCommand, "@VesselTypeId", DbType.Int32, _VesselTypeId);
        oDatabase.AddInParameter(odbCommand, "@LRIMONumber", DbType.String, _LRIMONumber);
        oDatabase.AddInParameter(odbCommand, "@portOfRegistryId", DbType.Int32, _portOfRegistryId);
        oDatabase.AddInParameter(odbCommand, "@officialNumber", DbType.String, _officialNumber);
        oDatabase.AddInParameter(odbCommand, "@MMSINumber", DbType.String, _MMISMNumber);
        oDatabase.AddInParameter(odbCommand, "@currentClassId", DbType.Int32, _currentClassId);
        oDatabase.AddInParameter(odbCommand, "@yearBuilt", DbType.String, _yearBuilt);
        oDatabase.AddInParameter(odbCommand, "@age", DbType.String, _age);
        oDatabase.AddInParameter(odbCommand, "@yard", DbType.String, _yard);

        oDatabase.AddInParameter(odbCommand, "@LDT", DbType.String, _ltd);
        oDatabase.AddInParameter(odbCommand, "@TEU", DbType.String, _teu);
        oDatabase.AddInParameter(odbCommand, "@length", DbType.String, _length);
        oDatabase.AddInParameter(odbCommand, "@breath", DbType.String, _breath);
        oDatabase.AddInParameter(odbCommand, "@depth", DbType.String, _depth);
        oDatabase.AddInParameter(odbCommand, "@draught", DbType.String, _draught);
        oDatabase.AddInParameter(odbCommand, "@EffectiveDate", DbType.String, _effectiveDate);
        
        oDatabase.AddInParameter(odbCommand, "@CreatedBy", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@ModifiedBy", DbType.Int32, _modifiedby);
        oDatabase.AddInParameter(odbCommand, "@Gemail", DbType.String, Gemail);
        oDatabase.AddInParameter(odbCommand, "@VettingRequired", DbType.Boolean, VettingRequired);
      //  oDatabase.AddInParameter(odbCommand, "@ShipOperator", DbType.String, _shipoperator);
        oDatabase.AddInParameter(odbCommand, "@CallSign", DbType.String, _callsign);
        oDatabase.AddInParameter(odbCommand, "@ManningAgentId", DbType.Int32, _ManningAgentId);
        oDatabase.AddInParameter(odbCommand, "@OwnerAgentId", DbType.Int32, _OwnerAgentId);
        oDatabase.AddInParameter(odbCommand, "@MLCAgentId", DbType.Int32, _MLCOwnerId);
        oDatabase.AddInParameter(odbCommand, "@AccountCompany", DbType.String, _AccountCompany);
        oDatabase.AddInParameter(odbCommand, "@IsMultiAccount", DbType.Boolean, _IsMultiAccount);
        oDatabase.AddInParameter(odbCommand, "@FleetId", DbType.Int32, _FleetId);
        oDatabase.AddInParameter(odbCommand, "@POIssuingCompanyId", DbType.String, _POIssuingAccountCompany);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                _ReturnValue = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@ReturnValue"));
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
    public static DataTable selectDataVesselGeneralByVesselId(int _VesselId)
    {
        string procedurename = "SelectVesselGeneralByVesselId";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt10.Load(dr);
        }

        return dt10;
    }
    public static DataTable selectDataVesselNameByVesselHistory(int _VesselId)
    {
        string procedurename = "SelectVesselNameByVesselHistory";
        DataTable dt11 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt11.Load(dr);
        }

        return dt11;
    }
    public static void Insert_Vessels(int WageScaleId, string VesselId)
    {
        string procedurename = "Insert_Vessels";
        DataSet objDataset = new DataSet();
        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.String, VesselId);
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

    }
    public static Boolean Check_Vessels_InWageScale(int WageScaleId, int VesselId)
    {
        string procedurename = "Check_Vessels_InWageScale";
        DataTable dt10 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@WageScaleId", DbType.Int32, WageScaleId);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt10.Load(dr);
        }
        if (Convert.ToInt32(dt10.Rows[0][0].ToString()) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static DataTable selectDataCrewOnVessel(int _VesselId)
    {
        string procedurename = "selectCrewOnVessel";
        DataTable dt15 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _VesselId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt15.Load(dr);
        }

        return dt15;
    }

    public static DataTable selectDataPOIssuingCompany()
    {
        string procedurename = "GetAccountCompanyHeader";
        DataTable dt3 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt3.Load(dr);
        }

        return dt3;
    }
}
