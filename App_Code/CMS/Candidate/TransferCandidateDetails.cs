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
/// Summary description for TransferCandidateDetails
/// </summary>
public class TransferCandidateDetails
{
    public TransferCandidateDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet transfercandidatedata(int candidateid,int createdby)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand("TransferCandidateInformation");
        DataSet objRank = new DataSet();
        obj.AddInParameter(odbCommand, "@candidateId", DbType.Int16, candidateid);
        obj.AddInParameter(odbCommand, "@CreatedBy", DbType.Int16, createdby);
 
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
