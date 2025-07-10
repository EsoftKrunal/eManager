using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using System.Data;
using System.Web.Script.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
/// 
[ScriptService()]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCertificates(string prefixText)
    {

        DataSet ds = Budget.getTable("SELECT CERTNAME FROM CERTMASTER WHERE CERTNAME LIKE '" + prefixText + "%'");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["CERTNAME"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetVendorAMC(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct Contract_Vendor from IVM_ITEMS_AMC where Contract_Vendor like '%" + prefixText + "%' and (Contract_Vendor !=null or Contract_Vendor !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["Contract_Vendor"].ToString(), i);
            i++;
        }
        return items;
    }
    [WebMethod]
    public string[] GetContractDetailsAMC(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct Support_Contact_Details from IVM_ITEMS_AMC where Support_Contact_Details like '%" + prefixText + "%' and (Support_Contact_Details !=null or Support_Contact_Details !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["Support_Contact_Details"].ToString(), i);
            i++;
        }
        return items;
    }




    [WebMethod]
    public string[] GetVendorHardware(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct VendorName from IVM_ITEMS_IT_Hardware where VendorName  like '%" + prefixText + "%' and  (VendorName  !=null or VendorName  !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["VendorName"].ToString(), i);
            i++;
        }
        return items;
    }
    [WebMethod]
    public string[] GetMakerHardware(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct Maker from IVM_ITEMS_IT_Hardware where Maker  like '%" + prefixText + "%' and  (Maker  !=null or Maker  !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["Maker"].ToString(), i);
            i++;
        }
        return items;
    }
    [WebMethod]
    public string[] GetTrainingName(string prefixText)
    {
        DataSet ds = Budget.getTable("select Distinct TrainingName from Training where TrainingName like '" + prefixText + "%' and  (TrainingName!=null or TrainingName  !='') order by TrainingName ");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["TrainingName"].ToString(), i);
            i++;
        }
        return items;
    }




    [WebMethod]
    public string[] GetVendorSoftware_license(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct License_Vendor from IVM_ITEMS_SOFTWARE where License_Vendor  like '%" + prefixText + "%' and  (License_Vendor  !=null or License_Vendor  !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["License_Vendor"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetVendorPartner(string prefixText)
    {

        DataSet ds = Budget.getTable("select distinct License_Partner from IVM_ITEMS_SOFTWARE where License_Partner  like '%" + prefixText + "%' and  (License_Partner  !=null or License_Partner  !='')");
        //DataSet ds = Budget.getTable("SELECT countryname FROM country");
        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["License_Partner"].ToString(), i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] GetPortTitles(string prefixText)
    {
        Database obj = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = obj.GetStoredProcCommand("PR_GetPortName");
        obj.AddInParameter(odbCommand, "@prefixtxt", DbType.String, prefixText);
        DataSet ds = new DataSet();
        ds = obj.ExecuteDataSet(odbCommand);

        string[] items = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            items.SetValue(dr["PortName"].ToString(), i);
            i++;
        }
        return items;
    }


}

