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

public partial class ItemsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
       Response.Clear();
       DataTable dtData ;
       bool ResultFound = false;
       string result = "<ul class='autocomplete_completionListElement' id='myDiv' style='max-height:300px;overflow-x:hidden;overflow-y:auto'>";
        if (Request.QueryString["Item"] == "VSL")
        {
            if (Session["NWC"] != null && Session["NWC"].ToString() == "Y")
            {
                if (int.Parse(Request.QueryString["InActive"]) > 0)
                {
                    if (int.Parse(Request.QueryString["NWC"]) > 0)
                    {
                        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company<>'NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID");
                    }
                    else
                    {
                        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company='NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID");
                    }
                    
                }
                else
                {
                    if (int.Parse(Request.QueryString["NWC"]) > 0)
                    {
                        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE  vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company<>'NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY active desc,vw.ShipID");
                    }
                    else
                    {
                        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE  vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company='NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY active desc,vw.ShipID");
                    }
                }
            }
            else
            {
                if (int.Parse(Request.QueryString["InActive"]) > 0)
                    dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company<>'NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY vw.ShipID");
                else
                    dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE  vw.ShipID LIKE '" + Request.QueryString["Param"] + "%' and vw.Company<>'NWC' and vw.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY active desc,vw.ShipID");
            }

            foreach (DataRow dr in dtData.Rows)
            {
                result = result + "<li ><a href='#' onclick=\"SetValue('" + dr[0].ToString() + "')\" value='" + dr[0].ToString() + "' onmouseover=\"this.childNodes[0].style.backgroundColor='#5f8ab7';this.childNodes[1].style.backgroundColor='#5f8ab7';\" onmouseout=\"this.childNodes[0].style.backgroundColor='';this.childNodes[1].style.backgroundColor='';\" ><div style='width:50px; text-align:center; float:left;'>" + dr[0].ToString() + "</div><div style='width:240px; text-align:left; float:left; background-color :inherit;'>| " + dr[1].ToString() + "</div></a></li>";
                ResultFound = true; 
            }
            result = result + "</ul>";

       }
        if (Request.QueryString["Item"] == "ACC")
        {
            dtData = Common.Execute_Procedures_Select_ByQuery("select AccountNumber,AccountName from dbo.VW_sql_tblSMDPRAccounts WHERE AccountNumber LIKE '" + Request.QueryString["Param"] + "%' ORDER BY AccountNumber");
            foreach (DataRow dr in dtData.Rows)
            {
                result = result + "<li ><a href='#' onclick=\"" + Request.QueryString["PK"] + "_SetValue('" + dr[0].ToString() + "')\" value='" + dr[0].ToString() + "' onmouseover=\"this.childNodes[0].style.backgroundColor='#5f8ab7';this.childNodes[1].style.backgroundColor='#5f8ab7';\" onmouseout=\"this.childNodes[0].style.backgroundColor='';this.childNodes[1].style.backgroundColor='';\" ><div style='width:35px; text-align:center; float:left;'>" + dr[0].ToString() + "</div><div style='width:260px; text-align:left; float:left; background-color :inherit;'>| " + dr[1].ToString() + "</div></a></li>";
                ResultFound = true;
            }
            result = result + "</ul>";

        }
      if (Request.QueryString["Item"] == "PO")
        {
            string vsl = Request.QueryString["VSL"];

            dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,InvNo from invoice where StatusId = 0 and isnull(VerifyStatus,0)=1 and InvNo LIKE '" + Request.QueryString["Param"] + "%' " +
                                                                "and invoice.VESSElID IN (SELECT vessel.VESSELID FROM vessel WHERE vessel.VESSELCODE='" + Request.QueryString["vsl"] + "') " +
                                                                "order by InvNo desc ");
            
            result = "<ul class='autocomplete_completionListElement' id='myDiv' style='max-height:200px;overflow-x:hidden;overflow-y:auto'>";
            if ("" + Request.QueryString["AddText"].ToString().Trim() != "")
            {
                result = result + "<li ><a href='#' onclick=\"SetValue('" + Request.QueryString["AddId"] + "','" + Request.QueryString["AddText"] + "')\" onmouseover=\"this.childNodes[0].style.backgroundColor='#5f8ab7';\" onmouseout=\"this.childNodes[0].style.backgroundColor='';\" ><div style='width:300px; text-align:left; float:left; background-color :inherit;'>" + Request.QueryString["AddText"] + "</div></a></li>";
                ResultFound = true;
            }
            foreach (DataRow dr in dtData.Rows)
            {
                result = result + "<li ><a href='#' onclick=\"SetValue('" + dr[0].ToString() + "','" + dr[1].ToString() + "')\" onmouseover=\"this.childNodes[0].style.backgroundColor='#5f8ab7';\" onmouseout=\"this.childNodes[0].style.backgroundColor='';\" ><div style='width:300px; text-align:left; float:left; background-color :inherit;'>" + dr[1].ToString() + "</div></a></li>";
                ResultFound = true;
            }
            result = result + "</ul>";
            
        }
        if (ResultFound)
            Response.Write(result);

        Response.End();
        Response.Flush();  
    }
}
