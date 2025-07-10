using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GetMaster
/// </summary>
public class GetMaster
{
	public GetMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// To Get selectManagementType
    /// </summary>
    /// <returns></returns>
    public DataSet selectManagementType()
    {

       DataSet  ds = new DataSet();
       Common.Set_ParameterLength(0);
        Common.Set_Procedures("SelectManagementType");
        ds = Common.Execute_Procedures_Select();
        return ds;

    }
    /// <summary>
    /// To execute any query
    /// </summary>
    /// <param name="Query"></param>
    /// <returns></returns>
    public static DataSet ExecuteQry(string Query)
    {

        DataSet  ds = new DataSet();
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Qry", Query));
         ds = Common.Execute_Procedures_Select();
        return ds;

    }

}
