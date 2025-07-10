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
/// Summary description for CrewFamilyMemberReport
/// </summary>
public class CrewFamilyMemberReport
{
    public static DataTable selectCrewFamilyMemberData(int _agefrom, int _ageto, int _relation, int _gender, int _country, string _areacode, char _nok, int _status)
    {
        string procedurename = "FamilyMemberDetails_Search";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@AgeFrom", DbType.Int32, _agefrom);
        objDatabase.AddInParameter(objDbCommand, "@AgeTo", DbType.Int32, _ageto);
        objDatabase.AddInParameter(objDbCommand, "@Relation", DbType.Int32, _relation);
        objDatabase.AddInParameter(objDbCommand, "@Gender", DbType.Int32, _gender);
        objDatabase.AddInParameter(objDbCommand, "@Country", DbType.Int32, _country);
        objDatabase.AddInParameter(objDbCommand, "@AreaCode", DbType.String, _areacode);
        objDatabase.AddInParameter(objDbCommand, "@NOK", DbType.String, _nok);
        objDatabase.AddInParameter(objDbCommand, "@CrewStatusId", DbType.Int16, _status);
        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
        }
        return dt;
    }
}
