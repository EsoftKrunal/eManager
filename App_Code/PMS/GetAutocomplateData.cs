using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;



/// <summary>
/// Summary description for GetAutocomplateData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
public class GetAutocomplateData : System.Web.Services.WebService {
//    string FindSearchData = "";
    public GetAutocomplateData () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        string[] Result=new string[100];
        string TableName, FieldName;
        char[] ss = new char[1];
        ss[0] = '|';
        TableName = contextKey.Split(ss)[0];
        FieldName = contextKey.Split(ss)[1];
        
        Common.Set_Procedures("GetAutoData");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@MasterName", TableName), new MyParameter("@FieldName", FieldName), new MyParameter("@Fieldvalue", prefixText));
        DataSet ds = Common.Execute_Procedures_Select();
        DataTable dt = ds.Tables[0];
        if (dt != null)
        {
            Result = new string[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Result[i] = dt.Rows[i][FieldName].ToString();
                }
            }
        }

        //char[] ss = new char[1];
        //ss[0] = ' ';
        //Result = ("a b d d a fsdaf sdfd saf sdafsd f sadf sf sdafa fsdafasd fasd f as fas fsda fs f").Split(ss);


        return Result;
    }

    
    
}

