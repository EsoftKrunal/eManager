using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Emtm_Inventory : System.Web.UI.Page
{
    public AuthenticationManager auth; 
    # region User Functions
    protected void ShowRecord()
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
           DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString()); 
            if(dt!=null )
                if (dt.Rows.Count > 0)
                {
                    DataRow dr=dt.Rows[0];
                    Session["ProfileName"] = "";
                    Session["ProfileMode"] = "Edit";
                }
        }
    }
    
    #endregion

    //-------------
    # region Events
    
    #endregion
  
}

    