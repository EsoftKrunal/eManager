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

/// <summary>
/// Summary description for Vessel_Matrix_CrewApproval
/// </summary>
public class Vessel_Matrix_CrewApproval
{
    public Vessel_Matrix_CrewApproval()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectVesselMatrixDetails1forCrewApproval(int _relieveid, int _relieverid, int _matrixid, int _vesselid)
    {
        string procedurename = "Report_VesselMatrix1_CrewApproval";
        DataTable dtvm1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RelieveId", DbType.Int32, _relieveid);
        objDatabase.AddInParameter(objDbCommand, "@RelieverId", DbType.Int32, _relieverid);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.Int32, _matrixid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtvm1.Load(dr);
        }
        return dtvm1;
    }
    public static DataTable selectVesselMatrixDetails2forCrewApproval(int _relieveid, int _relieverid, int _matrixid, int _vesselid)
    {
        string procedurename = "Report_VesselMatrix2_CrewApproval";
        DataTable dtvm2 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RelieveId", DbType.Int32, _relieveid);
        objDatabase.AddInParameter(objDbCommand, "@RelieverId", DbType.Int32, _relieverid);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.Int32, _matrixid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtvm2.Load(dr);
        }
        return dtvm2;
    }
    public static DataTable getRemarks(int _relieveid, int _relieverid, int _matrixid, int _vesselid)
    {
        string procedurename = "Report_VesselMatrix_Remarks";
        DataTable dtrem = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@RelieveId", DbType.Int32, _relieveid);
        objDatabase.AddInParameter(objDbCommand, "@RelieverId", DbType.Int32, _relieverid);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.Int32, _matrixid);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtrem.Load(dr);
        }
        return dtrem;
    }
}
