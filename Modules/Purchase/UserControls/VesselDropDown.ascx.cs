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

public partial class UserControls_MyDropDown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
     public string SelectedValue
      {
          get
          {   string res;
              DataTable dtData;
              //if (Session["NWC"].ToString() == "Y")
              //{
              //    if (IncludeInActive)
              //        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='Y')) And vw.ShipID='" + txtFilter.Text + "' and vw.Company='NWC' ORDER BY vw.ShipID");
              //    else
              //        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE vw.Active in('Y','N') And vw.ShipID='" + txtFilter.Text + "' and vw.Company='NWC' ORDER BY active desc,vw.ShipID");
              //}
              //else
              //{
              //    if (IncludeInActive)
              //        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='Y')) And vw.ShipID='" + txtFilter.Text + "' and vw.Company<>'NWC' ORDER BY vw.ShipID");
              //    else
              //        dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE vw.Active in('Y','N') And vw.ShipID='" + txtFilter.Text + "' and vw.Company<>'NWC' ORDER BY active desc,vw.ShipID");
              //}

              if (IncludeInActive)
                  dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID='" + txtFilter.Text + "' ORDER BY vw.ShipID");
              else
                  dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE  vw.ShipID='" + txtFilter.Text + "' ORDER BY active desc,vw.ShipID");

              if(txtFilter.Text=="-All-")
              {
                  res="All";
              }
              if(dtData.Rows.Count >0)
              {
                  res= dtData.Rows[0][0].ToString();   
              }
              else
              {
                  res=""; 
              }
              return res;
          }
          set
          {
              DataTable dtData;
              if (Convert.ToString(Session["NWC"]) == "Y")
              {
                  if (IncludeInActive)
                      dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID='" + value + "' and vw.Company='NWC' ORDER BY vw.ShipID");
                  else
                      dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE  vw.ShipID='" + value + "' and vw.Company='NWC' ORDER BY active desc,vw.ShipID");
              }
              else
              {
                  if (IncludeInActive)
                      dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE (((vw.Active)='A')) And vw.ShipID='" + value + "' and vw.Company<>'NWC' ORDER BY vw.ShipID");
                  else
                      dtData = Common.Execute_Procedures_Select_ByQuery("SELECT vw.ShipID, vw.ShipName FROM VW_sql_tblSMDPRVessels vw WHERE vw.ShipID='" + value + "' and vw.Company<>'NWC' ORDER BY active desc,vw.ShipID");
              }


           if(dtData !=null)
                if (dtData.Rows.Count > 0)
                {
                    txtFilter.Text = value;  
                }
          }
      }
      public bool IncludeInActive
      {
          get
          {
              if (ViewState["IncludeInActive"] != null)
                  return Convert.ToBoolean(ViewState["IncludeInActive"]);
              else
                  return false;
          }
          set 
          {
              ViewState["IncludeInActive"] = value;
              hdfINA.Value = (value) ? "0" : "1"; ; 
          }
      }
      public bool IncludeNWC
      {
          get
          {
              if (ViewState["IncludeNWC"] != null)
                  return Convert.ToBoolean(ViewState["IncludeNWC"]);
              else
                  return false;
          }
          set
          {
              ViewState["IncludeNWC"] = value;
              hfdNWC.Value = (value) ? "0" : "1"; ;
          }
      }
      public void Reset()
      {
          txtFilter.Text = "";
      }
}
