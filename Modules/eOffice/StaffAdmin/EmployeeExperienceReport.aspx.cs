using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class EmployeeExperienceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            
            if (Request.QueryString["Mode"] == null)
            {
                // -------------------------------------------
                ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "0");
                DataTable dtPOSGroup = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM VesselPositions order by POSITIONNAME");
                ddlPosition.DataSource = dtPOSGroup;
                ddlPosition.DataTextField = "PositionName";
                ddlPosition.DataValueField = "VPId";
                ddlPosition.DataBind();
                ddlPosition.Items.Insert(0, new ListItem("< Select  >", "0"));
                // -------------------------------------------
                rptVTypes.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.VESSELTYPE WHERE STATUSID='A' ORDER BY VESSELTYPENAME");
                rptVTypes.DataBind();
            }
            else
            {
                // -------------------------------------------
                Response.Clear();
                if (Request.QueryString["VesselTypes"].ToString() == "")
                {
                    Response.Write("");
                    Response.End();
                }
                else
                {
                    int OfficeId=Common.CastAsInt32(Request.QueryString["OffIceId"]);
                    int PositionId=Common.CastAsInt32(Request.QueryString["PositionId"]);
                    string VesselTypes=Request.QueryString["VesselTypes"].ToString();
                    string sqlData = "SELECT EMPID,VESSELTYPE,SUM(Experiance)AS EXPR FROM [dbo].[HR_ShipExperienceDetailsNew] GROUP BY EMPID,VESSELTYPE";
                    DataTable dtData = Common.Execute_Procedures_Select_ByQueryCMS(sqlData);
                    string sqlCats = "SELECT * FROM DBO.VESSELTYPE WHERE VESSELTYPEID IN(" + VesselTypes + ")";
                    DataTable dtCats = Common.Execute_Procedures_Select_ByQueryCMS(sqlCats);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table border='1' cellspacing='0' cellpadding='3' style='border-collapse:collapse'>");
                    sb.Append("<tr style='background-color:#dddddd'>");
                    sb.Append("<th>Employee Name</th>");
                    sb.Append("<th>Office</th>");
                    sb.Append("<th>Position</th>");
                    sb.Append("<th>Shore Exp.</th>");
                    foreach (DataRow dr1 in dtCats.Rows)
                    {
                        sb.Append("<th>" + dr1["VesselTypeName"].ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    string sql = "SELECT EMPID,EMPCODE,FIRSTNAME,MIDDLENAME,FAMILYNAME,OFFICENAME,POSITIONNAME,(SELECT SUM(DATEDIFF(DAY,FROMDATE,TODATE))/365 FROM [dbo].[HR_ShoreDetails] SH WHERE SH.EMPID=P.EMPID) AS SHOREEXP  FROM Hr_PersonalDetails P " +
                               "LEFT JOIN  OFFICE O ON P.OFFICE=O.OFFICEID " +
                               "LEFT JOIN  POSITION PP ON P.POSITION=PP.POSITIONID " +
                               "WHERE DRC IS NULL ";
                    string whereclause = "";

                    if (OfficeId > 0)
                        whereclause += " AND P.OFFICE=" + OfficeId;
                    if (PositionId > 0)
                        whereclause += " AND P.Position In (SELECT PP.POSITIONID FROM DBO.POSITION PP WHERE PP.VESSELPOSITIONS=" + PositionId + " ) ";

                    sql = sql + whereclause + " ORDER BY EMPCODE ";

                    DataTable dtEmps = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                    foreach (DataRow dr in dtEmps.Rows)
                    {
                        int Empid = Common.CastAsInt32(dr["EMPID"]);
                        
                        sb.Append("<tr>");
                        sb.Append("<td>" + dr["FIRSTNAME"].ToString() + dr["MIDDLENAME"].ToString() + dr["FAMILYNAME"].ToString() + "</td>");
                        sb.Append("<td>" + dr["OFFICENAME"].ToString() + "</td>");
                        sb.Append("<td>" + dr["POSITIONNAME"].ToString() + "</td>");
                        sb.Append("<td>" + dr["SHOREEXP"].ToString() + "</td>");
                        foreach (DataRow dr1 in dtCats.Rows)
                        {
                            int VesselType = Common.CastAsInt32(dr1["VESSELTYPEID"]);
                            DataRow[] drs = dtData.Select("EMPID=" + Empid.ToString() + " AND VESSELTYPE=" + VesselType);
                            if (drs.Length > 0)
                            {
                                sb.Append("<td>" + drs[0]["EXPR"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td>0</td>");
                            }
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("<table>");
                    sb.Append("</table>");
                    Response.Write(sb.ToString());
                    if ("" + Request.QueryString["Excel"] == "E")
                    {
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                    }
                    Response.End();
                }
            }
        }
    }
        
       
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string Types = "";
        foreach (RepeaterItem ri in rptVTypes.Items)
        {
            CheckBox ch=((CheckBox)ri.FindControl("chkVType"));
            if (ch.Checked)
                Types +=","+ch.CssClass;
        }
        if (Types.StartsWith(","))
            Types = Types.Substring(1);
        if (Types.Trim() == "")
        {
            lblMessage.Text = "Please select at least one vesseltype to see the report.";
            frmReport.Attributes.Add("src", "");
        }
        else
        {
            frmReport.Attributes.Add("src", "EmployeeExperienceReport.aspx?Mode=Report&Excel=" + ((Button)sender).CommandArgument + "&OfficeId=" + ddlOffice.SelectedValue + "&PositionId=" + ddlPosition.SelectedValue + "&VesselTypes=" + Types);
        }
    }
    
}
