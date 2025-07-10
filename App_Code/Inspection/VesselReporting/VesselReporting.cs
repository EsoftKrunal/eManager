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
using System.Data.SqlClient;




/// <summary>
/// Summary description for MiningScale
/// </summary>
public class VesselReporting
{
    public VesselReporting()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataSet getTable(string Query)
    {
        DataSet ds=new DataSet(); 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString()); 
        SqlCommand cmd=new SqlCommand(Query,con);  
        SqlDataAdapter adp=new SqlDataAdapter(cmd);
        try
        {
            adp.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
        
    }
 
}
