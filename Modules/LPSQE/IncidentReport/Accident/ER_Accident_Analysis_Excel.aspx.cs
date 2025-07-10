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
using System.Text;
using System.IO;

public partial class ER_S115_Analysis_Excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        BindGrid();
        //ExportDatatable();
    }
    private void BindGrid()
    {
        string SQl = "select v.vesselcode,datename(mm,dateofoccurrence) as NMonth, n.* from dbo.fr_nearmiss n inner join dbo.vessel v on v.vesselid=n.vesselid  ";
        string WC = "";

        string VesselId = Request.QueryString["VesselId"];
        string NMType = Request.QueryString["NMType"];
        string AccCat = Request.QueryString["AccCat"];

        string Fdt = Request.QueryString["Fdt"];
        string Tdt = Request.QueryString["Tdt"];

        if (VesselId!="")
            WC += "# n.VesselId=" + VesselId;

        if (NMType != "")
            WC += "# CommentType='" + NMType + "'";

        if (AccCat!="")
        {
            if (AccCat == "Injury")
                WC += "# InjuryCategory='Injury'";
            if (AccCat == "Pollution")
                WC += "# PollutionCategory='Pollution'";
            if (AccCat == "Property Damage")
                WC += "# ProDamageCategory='Property Damage'";
        }
        if (Fdt.Trim() != "")
        {
            WC += "# dateofoccurrence >='" + Fdt.Trim() + "'";
        }
        if (Tdt.Trim() != "")
        {
            WC += "# dateofoccurrence <='" + Tdt.Trim() + "'";
        }
        if (WC.Contains("#"))
        {
            if (WC.StartsWith("#"))
                WC = WC.Substring(1);

            WC = WC.Replace("#", "And");
            WC = " Where " + WC;
        }

        DataTable DT = Budget.getTable(SQl + WC).Tables[0];

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i <= DT.Rows.Count-1; i++)
        {
            string RootCause= DT.Rows[i]["RootCause"].ToString() ;
            sb.Append("<tr>"); 
            // ------------------- Main Table ----------------
            sb.Append("<td>" + DT.Rows[i]["vesselcode"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["NMonth"].ToString() + "</td>");
            sb.Append("<td>" + DT.Rows[i]["WhatHappened"].ToString() + "</td>");
            for (int j = 0; j <= 15; j++)
            {
                if ((RootCause + ",").Contains(j.ToString()+","))
                {
                    sb.Append("<td style='text-align:center'>X</td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }
            }
            sb.Append("</tr>");
        }
        ltr.Text = sb.ToString();
    }
    public void ExportDatatable()
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + "S115_Analysis" + ".xls");
        Response.ContentType = "application/vnd.xls";

    }
}
