using System;
using System.Collections.Generic;
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

public partial class UserControls_PODropDown: System.Web.UI.UserControl
{
      public string SelectedValue
      {
          get
          {   string res;
              DataTable dtData;
              dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,InvNo from invoice where  StatusId = 0 and isnull(VerifyStatus,0)=1 and invno='"+txtFilter.Text+"' " +
                                                              "and invoice.VESSElID IN (SELECT vessel.VESSELID FROM vessel WHERE vessel.VESSELCODE='" + ShipId + "') " +
                                                              "order by InvNo desc ");
              if(dtData.Rows.Count >0)
              {
                  res= dtData.Rows[0][1].ToString();
              }
              else if (txtFilter.Text.Trim() == AddText.Trim())
              {
                  res = AddText;
              }
              else
              {
                  res=""; 
              }
              return res;
          }
          set
          {
              string res;
              DataTable dtData;
              dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,InvNo from invoice where  StatusId = 0 and isnull(VerifyStatus,0)=1 and invno='"+value+"' " +
                                                              "and invoice.VESSElID IN (SELECT vessel.VESSELID FROM vessel WHERE vessel.VESSELCODE='" + ShipId + "') " +
                                                              "order by InvNo desc ");
              if(dtData !=null)
                  if (dtData.Rows.Count > 0)
                  {
                      txtFilter.Text = value;  
                  }
                  else if (value == AddText.Trim())
                  {
                      txtFilter.Text = value; 
                  }
                  else
                  {
                      res = "";
                  }
          }
      }
      public string SelectedId
      {
          get
          {
              string res;
              if (Common.CastAsInt32(hfdInvId.Value) <= 0)
              {
                  DataTable dtData;
                  dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,InvNo from invoice where  StatusId = 0 and isnull(VerifyStatus,0)=1 and invno='" + txtFilter.Text.Trim() + "' " +
                                                                  "and invoice.VESSElID IN (SELECT vessel.VESSELID FROM vessel WHERE vessel.VESSELCODE='" + ShipId + "') " +
                                                                  "order by InvNo desc ");
                      if (dtData.Rows.Count > 0)
                      {
                          res = dtData.Rows[0][0].ToString();   
                      }
                      else
                      {
                          res = "";
                      }
                      return res;
              }
              else
              {
                  return hfdInvId.Value;
              }
          }
          set
          {
              hfdInvId.Value = value;
          }
      }
      public string ShipId
      {
          get
          {
              if (ViewState["ShipId"] != null)
                  return Convert.ToString (ViewState["ShipId"]);
              else
                  return "";
          }
          set 
          {
              ViewState["ShipId"] = value; 
          }
      }
      public string AddText
      {
          get
          {
              if (ViewState["AddText"] != null)
                  return Convert.ToString(ViewState["AddText"]);
              else
                  return "";
          }
          set
          {
              ViewState["AddText"] = value;
          }
      }
      public string AddId
      {
          get
          {
              if (ViewState["AddId"] != null)
                  return Convert.ToString(ViewState["AddId"]);
              else
                  return "";
          }
          set
          {
              ViewState["AddId"] = value;
          }
      }
      public void Reset()
      {
          txtFilter.Text = "";
          hfdInvId.Value = "0";  
      }
      protected void Page_Load(object sender, EventArgs e)
      {
       
      }
}
