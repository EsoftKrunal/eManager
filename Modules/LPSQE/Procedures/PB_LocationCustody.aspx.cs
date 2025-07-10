using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public partial class PB_LocationCustody : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //------------------------------------
            ProjectCommon.SessionCheck_New();
            //------------------------------------
        }
    }
    protected DataTable getLocation()
    {
        string sql = "SELECT LocationId,LocationName FROM DBO.PB_Publication_Location where officeShip='"  + ddlOfficeShip_F.SelectedValue +  "' order by LocationId";
        DataTable dt=Common.Execute_Procedures_Select_ByQuery(sql);
        dt.Rows.InsertAt(dt.NewRow(), 0);
        dt.Rows[0][0] = 0;
        dt.Rows[0][1] = "< -- Select -- >";
        return dt;
    }
    protected void rptData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hfdPublicationId=(HiddenField)e.Item.FindControl("hfdPublicationId");
        DropDownList ddlLocation = (DropDownList)e.Item.FindControl("ddlLocation");
        DropDownList ddlCustody = (DropDownList)e.Item.FindControl("ddlCustody");
        string sql = "SELECT OfficeLocation,ShipLocation,OfficeCustody,ShipCustody FROM DBO.PB_PUBLICATIONS WHERE PUBLICATIONID=" + hfdPublicationId.Value;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        
        if(ddlOfficeShip_F.SelectedValue=="O")
        {
            ddlLocation.SelectedValue = Common.CastAsInt32(dt.Rows[0]["OfficeLocation"]).ToString();
            ddlCustody.SelectedValue = Common.CastAsInt32(dt.Rows[0]["OfficeCustody"]).ToString();
        }
        
        if (ddlOfficeShip_F.SelectedValue == "S")
        {
            ddlLocation.SelectedValue = Common.CastAsInt32(dt.Rows[0]["ShipLocation"]).ToString();
            ddlCustody.SelectedValue = Common.CastAsInt32(dt.Rows[0]["ShipCustody"]).ToString();
        }
    }
    protected void BindRepeater()
    {
        string WhereClause = " where PUB.OfficeShip in ('" + ddlOfficeShip_F.SelectedValue + "')";
        string sql = "SELECT PUBLICATIONID,PUBLICATIONNAME,TYPENAME,MODENAME,PUBLISHERNAME,OfficeShip=(case when OfficeShip='O' then 'Office' when OfficeShip='S' then 'Ship' else 'Both' End),EditionYear,EditionNo,ValidityDate,CREATEDBY,CREATEDON " +
                   "FROM DBO.PB_PUBLICATIONS PUB  " +
                   "INNER JOIN DBO.PB_Publication_Type T ON T.TYPEID=PUB.TYPEID " +
                   "INNER JOIN DBO.PB_Publication_Mode D ON D.MODEID=PUB.MODEID " +
                   "LEFT JOIN DBO.PB_Publisher S ON S.PUBLISHERID=PUB.PUBLISHERID " + WhereClause;

        rptData.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptData.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlOfficeShip_F_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            string UserName = "";
            //--------------------------
            foreach(RepeaterItem ri in rptData.Items)
            {
                HiddenField hfdPublicationId = (HiddenField)ri.FindControl("hfdPublicationId");
                DropDownList ddlLocation = (DropDownList)ri.FindControl("ddlLocation");
                DropDownList ddlCustody = (DropDownList)ri.FindControl("ddlCustody");
                if (ddlOfficeShip_F.SelectedValue == "O")
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.PB_PUBLICATIONS SET OfficeLocation=" + ddlLocation.SelectedValue + ",OfficeCustody=" + ddlCustody.SelectedValue + " WHERE PUBLICATIONID=" + hfdPublicationId.Value);
                }
                if (ddlOfficeShip_F.SelectedValue == "S")
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.PB_PUBLICATIONS SET ShipLocation=" + ddlLocation.SelectedValue + ",ShipCustody=" + ddlCustody.SelectedValue + " WHERE PUBLICATIONID=" + hfdPublicationId.Value);
                }
            }
            //--------------------------
            BindRepeater();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Record saved successfully.');", true);
    }
}
