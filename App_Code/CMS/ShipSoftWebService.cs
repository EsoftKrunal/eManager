using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using System.Data;


/// <summary>
/// Summary description for ShipSoftWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ShipSoftWebService : System.Web.Services.WebService
{

    public ShipSoftWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SavePersonalDetails(string strProc, int rankapplied, string availablefrom, string fname, string mname, string lname, string dob, string emailid, int nationality, string contactno, string POB, int quali,int createdby, int gender, DataSet ds,DataSet dsdce, string _passportno, string _issuedate, string _expirydate)
    {
        DataTable dt;
        dt = ds.Tables[0];
        try
        {


            int CandidateId = 0;
            Database oDatabase = DatabaseFactory.CreateDatabase();

            DbCommand odbCommand = oDatabase.GetStoredProcCommand(strProc);


            oDatabase.AddInParameter(odbCommand, "@RankAppliedId", DbType.Int16, rankapplied);
            if (availablefrom == "")
            {
                oDatabase.AddInParameter(odbCommand, "@AvailableFrom", DbType.DateTime, null);
            }
            else
            {
                oDatabase.AddInParameter(odbCommand, "@AvailableFrom", DbType.DateTime, Convert.ToDateTime(availablefrom));
            }
            oDatabase.AddInParameter(odbCommand, "@firstname", DbType.String, fname);
            oDatabase.AddInParameter(odbCommand, "@middlename", DbType.String, mname);
            oDatabase.AddInParameter(odbCommand, "@lastName", DbType.String, lname);
            if (dob == "")
            {
                oDatabase.AddInParameter(odbCommand, "@DateOfBirth", DbType.DateTime, null);
            }
            else
            {
                oDatabase.AddInParameter(odbCommand, "@DateOfBirth", DbType.DateTime, Convert.ToDateTime(dob));
            }
            oDatabase.AddInParameter(odbCommand, "@NationalityId", DbType.Int16, nationality);
            oDatabase.AddInParameter(odbCommand, "@EmailId", DbType.String, emailid);
            oDatabase.AddInParameter(odbCommand, "@ContactNo", DbType.String, contactno);
            oDatabase.AddInParameter(odbCommand, "@PlaceOfBirth", DbType.String, POB);
            oDatabase.AddInParameter(odbCommand, "@QualificationId", DbType.Int16, quali);
            //oDatabase.AddInParameter(odbCommand, "@DangerousCargeEndorsement", DbType.Int16, DGE);
            oDatabase.AddInParameter(odbCommand, "@CreatedBY", DbType.Int16, createdby);
            oDatabase.AddInParameter(odbCommand, "@Gender", DbType.Int16, gender);
            oDatabase.AddInParameter(odbCommand, "PassportNo", DbType.String, _passportno);
            if (_issuedate == "")
            {
                oDatabase.AddInParameter(odbCommand, "@IssueDate", DbType.DateTime, null);
            }
            else
            {
                oDatabase.AddInParameter(odbCommand, "@IssueDate", DbType.DateTime, Convert.ToDateTime(_issuedate));
            }
            if (_expirydate == "")
            {
                oDatabase.AddInParameter(odbCommand, "@ExpiryDate", DbType.DateTime, null);
            }
            else
            {
                oDatabase.AddInParameter(odbCommand, "@ExpiryDate", DbType.DateTime, Convert.ToDateTime(_expirydate));
            }
            oDatabase.AddOutParameter(odbCommand, "@CandidateId", DbType.Int16, CandidateId);

            //using (TransactionScope scope = new TransactionScope())
            //{
            try
            {

                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                CandidateId = Convert.ToInt32(oDatabase.GetParameterValue(odbCommand, "@CandidateId"));
                // scope.Complete();

            }

            catch (Exception ex)
            {
                // if error is coming throw that error
                return ex.InnerException.ToString();

            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();

            }
            //}
            //using (TransactionScope scope = new TransactionScope())
            //{

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Database obj = DatabaseFactory.CreateDatabase();
                    DbCommand odbCommand1 = obj.GetStoredProcCommand("[InsertCandidateExperienceDetails]");
                    DataRow dr;
                    dr = dt.Rows[i];

                    obj.AddInParameter(odbCommand1, "@CandidateId", DbType.Int16, CandidateId);
                    obj.AddInParameter(odbCommand1, "@CompanyName", DbType.String, dr["CompanyName"].ToString());
                    obj.AddInParameter(odbCommand1, "@RankId", DbType.Int16, Convert.ToInt16(dr["RankId"].ToString()));
                    obj.AddInParameter(odbCommand1, "@SignOn", DbType.DateTime, dr["SignOn"].ToString());
                    obj.AddInParameter(odbCommand1, "@SignOff", DbType.DateTime, dr["SignOff"].ToString());
                    obj.AddInParameter(odbCommand1, "@SignOffReasonId", DbType.Int16, Convert.ToInt16(dr["SignOffReasonId"].ToString()));
                    obj.AddInParameter(odbCommand1, "@VesselName", DbType.String, dr["VesselName"].ToString());
                    obj.AddInParameter(odbCommand1, "@VesselTypeId", DbType.Int16, Convert.ToInt16(dr["VesselTypeId"].ToString()));
                    obj.AddInParameter(odbCommand1, "@Registry", DbType.String, dr["Registry"].ToString());
                    obj.AddInParameter(odbCommand1, "@DWT", DbType.String, dr["DWT"].ToString());
                    obj.AddInParameter(odbCommand1, "@GWT", DbType.String, dr["GWT"].ToString());
                    obj.AddInParameter(odbCommand1, "@BHP", DbType.String, dr["BHP"].ToString());
                    obj.AddInParameter(odbCommand1, "@CreatedBY", DbType.Int16, createdby);
                    obj.ExecuteNonQuery(odbCommand1);

                }
            }

            DataTable dtdce;
            dtdce = dsdce.Tables[0];
            if (dtdce.Rows.Count > 0)
            {
                for (int i = 0; i < dtdce.Rows.Count; i++)
                {
                    Database objdce = DatabaseFactory.CreateDatabase();
                    DbCommand odbCommand2 = objdce.GetStoredProcCommand("[InsertCandidateCargoDetails]");
                    DataRow dr;
                    dr = dtdce.Rows[i];
                    //@CandidateId,
//@CargoId ,
//@Number,
//@NationalityId,
//@GradeLevel ,
//@PlaceOfIssue,
//@DateOfIssue ,
//@ExpiryDate ,
//@CreatedBy,

                    objdce.AddInParameter(odbCommand2, "@CandidateId", DbType.Int16, CandidateId);
                    objdce.AddInParameter(odbCommand2, "@CargoId", DbType.Int16, Convert.ToInt16(dr["CargoId"].ToString()));
                    objdce.AddInParameter(odbCommand2, "@Number", DbType.String, dr["Number"].ToString());
                    objdce.AddInParameter(odbCommand2, "@NationalityId", DbType.Int16, Convert.ToInt16(dr["NationalityId"].ToString()));
                    objdce.AddInParameter(odbCommand2, "@GradeLevel", DbType.String, dr["GradeLevel"].ToString());
                    objdce.AddInParameter(odbCommand2, "@PlaceOfIssue", DbType.String, dr["PlaceOfIssue"].ToString());
                    if (dr["DateOfIssue"].ToString() == "")
                    {
                        objdce.AddInParameter(odbCommand2, "@DateOfIssue", DbType.DateTime , null);
                    }
                    else
                    {
                        objdce.AddInParameter(odbCommand2, "@DateOfIssue", DbType.DateTime, dr["DateOfIssue"].ToString());
                    }
                    if (dr["ExpiryDate"].ToString() == "")
                    {
                        objdce.AddInParameter(odbCommand2, "@ExpiryDate", DbType.DateTime, null);
                    }
                    else
                    {
                        objdce.AddInParameter(odbCommand2, "@ExpiryDate", DbType.DateTime, dr["ExpiryDate"].ToString());
                    }
                    objdce.AddInParameter(odbCommand2, "@CreatedBY", DbType.Int16, createdby);
                    objdce.ExecuteNonQuery(odbCommand2);

                }
            }
            return "Your details have been posted to us:<br/> We will contact you shortly";
        }





        catch (SystemException es)
        {
            return es.Message;
        }
    }
    [WebMethod]
    public DataSet getData(string strprocname)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetStoredProcCommand(strprocname);
        DataSet objRank = new DataSet();
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
    [WebMethod]
    public DataSet getDataQuery(string strquery)
    {
        Database obj = DatabaseFactory.CreateDatabase();
        DbCommand odbCommand = obj.GetSqlStringCommand(strquery);

        DataSet objRank = new DataSet();
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
    [WebMethod]
    public DataSet getdatabyid(string strproc, int cid)
    {
        Database obj = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = obj.GetStoredProcCommand(strproc);
        obj.AddInParameter(odbCommand, "@Id", DbType.Int32, cid);
        DataSet objRank = new DataSet();
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

