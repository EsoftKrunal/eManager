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
/// <summary>
/// Summary description for CrewDocument
/// </summary>
public class CrewDocument
{
    public CrewDocument()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable selectCrewDocument(int _crewId,int _Passport,int _visa,int _seamanbook,int _license,int _course,int _cargo,int _other)
    {
        string procedurename = "SelectCrewMemberDocuments";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@crewid", DbType.Int32, _crewId);
        objDatabase.AddInParameter(objDbCommand, "@passport", DbType.Int32, _Passport);
        objDatabase.AddInParameter(objDbCommand, "@Visa", DbType.Int32, _visa);
        objDatabase.AddInParameter(objDbCommand, "@SeamanBook", DbType.Int32, _seamanbook);
        objDatabase.AddInParameter(objDbCommand, "@License", DbType.Int32, _license);
        objDatabase.AddInParameter(objDbCommand, "@Course", DbType.Int32, _course);
        objDatabase.AddInParameter(objDbCommand, "@DangerousCargo", DbType.Int32, _cargo);
        objDatabase.AddInParameter(objDbCommand, "@Otherdocument", DbType.Int32, _other);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }

        return dt;
    }
}
