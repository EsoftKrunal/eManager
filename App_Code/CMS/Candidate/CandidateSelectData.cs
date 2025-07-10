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
/// Summary description for CandidateSelectData
/// </summary>
public class CandidateSelectData
{
    public CandidateSelectData()
    {
        
    }
    public DataSet getData(int candidateid)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand("selectCandidatePersonalDetails");
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
