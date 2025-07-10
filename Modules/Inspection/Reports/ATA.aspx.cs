using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

public partial class Reports_ATA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        if (!Page.IsPostBack)
        {
            txtfromdate.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txttodate.Text = DateTime.Today.ToString("31-DEC-yyyy");
        }
    }
    protected void radtype_OnCheckedChanged(object sender, EventArgs e)
    {
        frm1.Attributes.Add("src", "");
    }
    protected void btn_Analyse_Click(object sender, EventArgs e)
    {
        if (radRCA.Checked)
            frm1.Attributes.Add("src", "RCA.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")));
        else if (radSC.Checked)
            frm1.Attributes.Add("src", "SireChapter.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")));
    
        //else if (radReport.Checked)
        //    frm1.Attributes.Add("src", "RCAReport.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")) + "&Inspections=" + Inspections + "&InsNames=" + string.Join(",", InsNames.ToArray()));
        //else
        //    frm1.Attributes.Add("src", "ATAAnalysis.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")) + "&Inspections=" + Inspections + "&InsNames=" + string.Join(",", InsNames.ToArray()));
    }

    protected void btn_Show_Click(object sender, EventArgs e)
    {
        if (radRCA.Checked)
            frm1.Attributes.Add("src", "RCAReport.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")));
        else
            frm1.Attributes.Add("src", "SireAnalysisReport.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")));
            //frm1.Attributes.Add("src", "SireChapterReport.aspx?FDT=" + txtfromdate.Text + "&TDT=" + txttodate.Text + "&Status=" + ((rad_O.Checked) ? "O" : ((rad_C.Checked) ? "C" : "A")));
    }
    //protected void btn_Clear_Click(object sender, EventArgs e)
    //{
    //    txtfromdate.Text = "";
    //    txttodate.Text = "";
    //    frm1.Attributes.Add("src", "");
    //}
}
