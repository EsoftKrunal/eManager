using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class InviceDetailsPopUp : System.Web.UI.Page
{
    string InvoiceNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        InvoiceNo = "" + Request.QueryString["InvId"];
        DataTable dtData = Common.Execute_Procedures_Select_ByQuery("select InvoiceId,VESSELID, RefNo,(select Company from (select PortAgentId as AgentId,Company from portagent union select TravelAgentid as AgentId,Company from travelagent union select SupplierId as AgentId,Company from SupplierMaster) a where a.Agentid=Vendorid) as  VendorId, " +
	                                                                    "round(convert(float,InvoiceAmount),2) as InvoiceAmount, " +
	                                                                    "InvNo, " +
	                                                                    "replace(convert(varchar,InvDate,106),' ','-') as Invdate, " +
	                                                                    "replace(convert(varchar,DueDate,106),' ','-') as Duedate, " +
	                                                                    "(select PoNo from POHeader where POHeader.PoId=Invoice.POId) AS PoNo, " +
	                                                                    "(select sum(isnull(amountUSD,0)) from PODetails where PODetails.PoId=Invoice.POId) AS PoAmount, " +
	                                                                    " GST, InvoiceAmount + GST as TotalInvoiceAmount, " +
                                                                        "(select CurrencyName from Currency where Currency.CurrencyId=invoice.Currencyid) as CurrencyId " +
                                                                        "from invoice where InvNo='" + InvoiceNo + "'");
        if(dtData !=null)
            if (dtData.Rows.Count > 0)
            {
                lblVendor.Text = dtData.Rows[0]["VendorId"].ToString();
                lblRefNo.Text = dtData.Rows[0]["REfNo"].ToString();
                lblInvNo.Text = dtData.Rows[0]["InvNo"].ToString();
                lblCurrency.Text = dtData.Rows[0]["Currencyid"].ToString();
                lblDueDt.Text = dtData.Rows[0]["DueDate"].ToString();
                lblInvAmt .Text = dtData.Rows[0]["InvoiceAmount"].ToString();
            }
    }
}
