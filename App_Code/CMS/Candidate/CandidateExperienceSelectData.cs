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
using Microsoft.Practices.EnterpriseLibrary.Data;


/// <summary>
/// Summary description for CandidateExperienceSelectData
/// </summary>
public class CandidateExperienceSelectData
{
    public CandidateExperienceSelectData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getData(int candidateid)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand("[selectCandidateExperienceDetails]");
        DataSet objRank = new DataSet();
        obj.AddInParameter(odbCommand, "@Id", DbType.Int16, candidateid);
        try
        {
            objRank = obj.ExecuteDataSet(odbCommand);

        }
        catch (SystemException es)
        {
            throw es;
        }
        finally
        {
            objRank.Dispose();
            odbCommand.Dispose();
        }
        return objRank;

    }

    public DataSet getcargodata(int candidateid)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand("selectCandidateDCEDetails");
        DataSet objRank = new DataSet();
        obj.AddInParameter(odbCommand, "@Id", DbType.Int16, candidateid);
        try
        {
            objRank = obj.ExecuteDataSet(odbCommand);

        }
        catch (SystemException es)
        {
            throw es;
        }
        finally
        {
            objRank.Dispose();
            odbCommand.Dispose();
        }
        return objRank;
    }
}
