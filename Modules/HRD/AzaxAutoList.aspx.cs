using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class AzaxAutoList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string Key = Request.QueryString["Key"];

        DataTable Dt;
        if (Request.QueryString["Type"] == "1")
        {
            Dt = Budget.getTable("select DISTINCT AirlineName from CrewPortCallTravelDetails WHERE AirlineName<>'' and AirlineName LIKE '" + Key + "%'").Tables[0];
        }
        else
        {
            Dt = Budget.getTable("select DISTINCT LOCALAIRPORTID from crewcontactdetails WHERE LOCALAIRPORTID IS NOT NULL AND LOCALAIRPORTID<>'' and LOCALAIRPORTID LIKE '" + Key + "%'").Tables[0];
        }
       string Result = "<select name='drop1' onKeyDown='SetValue_Back_Key();' ondblclick='SetValue_Back(this);' id='Select1' size='4' multiple style='width:200px; height:300px; border:solid 1px black;' >";
        foreach (DataRow dr in Dt.Rows)
        {
            Result += "<option value=''>" +dr[0].ToString() +"</option>";
        }
        Result += "</select>";
        Response.Write(Result);
        Response.Flush();
        Response.End();
    }
}