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

public partial class UserControls_AccountDropDown : System.Web.UI.UserControl
{
     public string SelectedValue
      {
          get
          {   string res;
              DataTable dtData;
              dtData = Common.Execute_Procedures_Select_ByQuery("select AccountNumber,AccountName from dbo.VW_sql_tblSMDPRAccounts WHERE AccountNumber='" + txtFilter.Text + "' ORDER BY AccountNumber");
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
              dtData = Common.Execute_Procedures_Select_ByQuery("select AccountNumber,AccountName from dbo.VW_sql_tblSMDPRAccounts WHERE AccountNumber='" + value + "' ORDER BY AccountNumber");
              if(dtData !=null)
                  if (dtData.Rows.Count > 0)
                  {
                      txtFilter.Text = value;  
                  }
          }
      }
      public void Reset()
      {
          txtFilter.Text = "";
      }
}
