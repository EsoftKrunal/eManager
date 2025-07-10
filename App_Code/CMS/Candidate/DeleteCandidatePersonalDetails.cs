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
/// Summary description for DeleteCandidatePersonalDetails
/// </summary>
public class DeleteCandidatePersonalDetails
{
    public DeleteCandidatePersonalDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string deletecandidatedata(string candidateid)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand("DeleteCandidateDetails");
        obj.AddInParameter(odbCommand, "@CandidateId", DbType.String, candidateid);
        try
        {
            obj.ExecuteDataSet(odbCommand);

        }
        catch (SystemException es)
        {
            throw es;
            return es.Message;
        }
        finally
        {
            
            odbCommand.Dispose();
        }
        return "aa";

    }
}
