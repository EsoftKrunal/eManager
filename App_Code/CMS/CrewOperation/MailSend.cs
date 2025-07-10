using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Net.Mail;  

/// <summary>
/// Summary description for MailSend
/// </summary>
public class MailSend
{
    String _MyMessageBody;
    public MailSend()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
   
    public static DataTable SelectPortAgentBooking_Mail_Header(int _PortAgentBookingId)
    {
        string procedurename = "PortAgentBooking_Mail_Header";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _PortAgentBookingId);
      
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable SelectPortAgentBooking_Mail_SignOnMembers(int _PortAgentBookingId)
    {
        string procedurename = "PortAgentBooking_Mail_SignOnMembers";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _PortAgentBookingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable SelectPortAgentBooking_Mail_SignOffMembers(int _PortAgentBookingId)
    {
        string procedurename = "PortAgentBooking_Mail_SignOffMembers";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _PortAgentBookingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable SelectTravelAgentBooking_Mail_Header(int _TravelBookingId)
    {
        string procedurename = "TravelAgentBooking_Mail_Header";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TravelBookingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static DataTable SelectTravelAgentBooking_Mail_Details(int _TravelBookingId)
    {
        string procedurename = "TravelAgentBooking_Mail_Details";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _TravelBookingId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }

    public static  string LoginUserEmailId(int _LoginId)
    {
        string Loginuseremail = "";
        string procedurename = "SelectUserLoginDetailsByLoginId";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
            Loginuseremail = dt.Rows[0]["Email"].ToString();
        }

        return Loginuseremail;
        //return dt;
    }

}
