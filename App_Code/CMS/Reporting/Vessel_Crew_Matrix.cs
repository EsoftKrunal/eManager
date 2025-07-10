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
/// Summary description for Vessel_Crew_Matrix
/// </summary>
public class Vessel_Crew_Matrix
{
    public Vessel_Crew_Matrix()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable selectVesselCrewMatrixDetails(int _vesselid, int _matrixid)
    {
        string procedurename = "Report_VesselCrewMatrix";
        DataTable dtvcm = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@VesselId", DbType.Int32, _vesselid);
        objDatabase.AddInParameter(objDbCommand, "@MatrixId", DbType.Int32, _matrixid);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dtvcm.Load(dr);
        }
        return dtvcm;
    }
}
