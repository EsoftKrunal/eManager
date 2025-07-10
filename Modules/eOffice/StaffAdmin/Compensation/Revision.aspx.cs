using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_Compensation_Revision : System.Web.UI.Page
{
    DateTime ToDay;
    //User Defined Properties
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    public int[] _years=new int[5];
    //--Page Load Events---------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Request.Form["ajaxMode"] != null)
        {
            //int Empid = Convert.ToInt32(Request.Form["Empid"]);
            //decimal Amount = Convert.ToDecimal(Request.Form["Amount"]);
            //string ret = UpdateAmount(Empid, Amount);
        }
        else
        {
            Session["CurrentPage"] = 1;
            ToDay = DateTime.Today;
            if (!IsPostBack)
            {
                ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "0");
                BindYear();
                if (Session["loginid"].ToString() != "1")
                {
                    DisableOffice();
                }
                btn_Search_Click(sender, e);
            }
        }
        
    }
    public string FormatNumber(object Data)
    {
        return Math.Round(Convert.ToDecimal(Data), 1).ToString("##0.0"); 
    }

    
    protected void DisableOffice()
    {
        string strSQL = "select Office from Hr_PersonalDetails where EmpId=" + Session["ProfileId"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            ddlOffice.SelectedValue = dt.Rows[0]["Office"].ToString();
            if (dt.Rows[0]["Office"].ToString() != "3")
            {
                ddlOffice.Enabled = false;
            }
        }
    }
    protected void BindGrid()
    {
        //string strSQL = " select p.empid,empcode,FirstName + ' ' + MiddleName + ' ' + FamilyName as EmpName ,isnull(RA.RevisionAmount,0)RevisionAmount, " +
        //                "    (select top 1 replace(convert(varchar, revisiondate,106),' ','-')revisedt from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc) as LastRevisionOn, " +
        //                "    (select sum(headvalue) from HR_CB_Detail d inner join HR_Comp_Head_Master hm on d.HeadID = hm.HeadId where Income_Ded = 'I' and d.RevisionId in(select top 1 RevisionId from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc)) as Income, " +
        //                "    (select sum(headvalue) from HR_CB_Detail d inner join HR_Comp_Head_Master hm on d.HeadID = hm.HeadId where Income_Ded = 'D' and d.RevisionId in(select top 1 RevisionId from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc)) as Deduction " +
        //                "    from Hr_PersonalDetails p  left join EMTM_Revision_Admin_Temp RA on RA.Empid=p.Empid where Office = " + ddlOffice.SelectedValue+" and drc is null order by EmpName ";

        //string strSQL = " select *,replace(convert(varchar, t.revisedt,106),' ','-')LastRevisionOn from  "+
        //                "    (  " +
        //                "        select p.empid, empcode, FirstName + ' ' + MiddleName + ' ' + FamilyName as EmpName, isnull(RA.RevisionAmount, 0)RevisionAmount,  " +
        //                "    (select top 1 revisiondate from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc) as revisedt,  " +
        //                "    (select sum(headvalue) from HR_CB_Detail d inner join HR_Comp_Head_Master hm on d.HeadID = hm.HeadId where Income_Ded = 'I' and d.RevisionId in(select top 1 RevisionId from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc)) as Income,      " +
        //                "    (select sum(headvalue) from HR_CB_Detail d inner join HR_Comp_Head_Master hm on d.HeadID = hm.HeadId where Income_Ded = 'D' and d.RevisionId in(select top 1 RevisionId from HR_CB_Master c where c.EmpID = p.empid order by revisiondate desc)) as Deduction  " +
        //                "    from Hr_PersonalDetails p  left  " +
        //                "    join EMTM_Revision_Admin_Temp RA on RA.Empid = p.Empid  " +
        //                "    where Office = "+ddlOffice.SelectedValue+" and drc is null  " +
        //                "    )t  " +
        //                "    where Year(t.revisedt) = "+ddlYear.SelectedValue+" order by EmpName ";
        int CY = Common.CastAsInt32( ddlYear.SelectedValue);
        _years[0] = CY - 5;
        _years[1] = CY - 4;
        _years[2] = CY - 3;
        _years[3] = CY - 2;
        _years[4] = CY - 1;

        string strSQL = "select p.empid,empcode,FirstName + ' ' + MiddleName + ' ' + FamilyName as EmpName, " +
                    "(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y'  WHERE RevisionId=[" + _years[0] + "]) as Amount1, " +
                    "(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y'  WHERE RevisionId=[" + _years[1] + "]) as Amount2, " +
                    "(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y'  WHERE RevisionId=[" + _years[2] + "]) as Amount3, " +
                    "(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y'  WHERE RevisionId=[" + _years[3] + "]) as Amount4, " +
                    "(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y'  WHERE RevisionId=[" + _years[4] + "]) as Amount5, " +
                    " " +
                    "(select HEADVALUE from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y'  WHERE RevisionId=[" + _years[0] + "]) as Amount1_B, " +
                    "(select HEADVALUE from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y'  WHERE RevisionId=[" + _years[1] + "]) as Amount2_B, " +
                    "(select HEADVALUE from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y'  WHERE RevisionId=[" + _years[2] + "]) as Amount3_B, " +
                    "(select HEADVALUE from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y'  WHERE RevisionId=[" + _years[3] + "]) as Amount4_B, " +
                    "(select HEADVALUE from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y'  WHERE RevisionId=[" + _years[4] + "]) as Amount5_B, " +
                    " " +
                    "(select replace(convert(varchar, max(RevisionDate),106),' ','-')  from  HR_CB_Master r where r.empid=p.EmpId) as RevisionDate, " +
                    "(select RevisionAmount from  HR_Compensation_Revision r where r.empid=p.EmpId and r.Year=" + ddlYear.SelectedValue + ") as RevisionAmount, " +
                    " ((select RevisionAmount from  HR_Compensation_Revision r where r.empid=p.EmpId and r.Year=" + ddlYear.SelectedValue + ")-(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')<>'Y' WHERE RevisionId=[" + _years[4] + "])) as TotIncrement, " +
                    "(select Bonus from  HR_Compensation_Revision r where r.empid=p.EmpId and r.Year=" + ddlYear.SelectedValue + ") as BonusAmount, " +
                    " ((select Bonus from  HR_Compensation_Revision r where r.empid=p.EmpId and r.Year=" + ddlYear.SelectedValue + ")-(select SUM(case when [Income_Ded]='I' then HEADVALUE else 0 end)-SUM(case when [Income_Ded]='D' then HEADVALUE else 0 end) from HR_CB_Detail D INNER JOIN HR_Comp_Head_Master H ON D.HeadID=H.HeadId and ISNULL(H.BONUS,'N')='Y' WHERE RevisionId=[" + _years[4] + "])) as TotIncrementBonus " +
                    "from Hr_PersonalDetails p " +
                    "left join  " +
                    "( " +
                    "SELECT empid, [" + _years[0] + "], [" + _years[1] + "], [" + _years[2] + "], [" + _years[3] + "], [" + _years[4] + "] " +
                    "FROM " +
                    "( " +
                    "	select year(revisiondate) as  ryear,empid,RevisionId from HR_CB_Master  " +
                    ")  " +
                    "AS SourceTable " +
                    "PIVOT " +
                    "( " +
                    "max(RevisionId) " +
                    "FOR ryear IN ([" + _years[0] + "], [" + _years[1] + "], [" + _years[2] + "], [" + _years[3] + "], [" + _years[4] + "]) " +
                    ")  " +
                    "AS PivotTable " +
                    ")E on e.empid=p.empid where p.office="+ddlOffice.SelectedValue+"  ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        RptEmployee.DataSource = dt;
        RptEmployee.DataBind();
        EmpCount.Text = RptEmployee.Items.Count.ToString();
    }
    protected void BindYear()
    {
        ddlYear.ClearSelection();
        for (int i = DateTime.Today.Year; i >= 2014; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }
    // Events ------------
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddlOffice.SelectedIndex = 0;
        BindGrid();
    }


    [System.Web.Services.WebMethod]
    public static string UpdateAmount(int Empid, Decimal Amount,int Year,string Mode)
    {
        string ret = "";
        
            //EMTM_IP_Revision_Admin_Temp
        Common.Set_Procedures("HR_IU_Admin_Revision");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@Empid", Empid),
            new MyParameter("@RevisionAmount", Amount),
            new MyParameter("@RYear", Year),
            new MyParameter("@Mode", Mode)
            );


        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ret = "Y";
            }
            else
            {
                ret = "N";

            }
        }
        catch
        {
            ret = "N";
        }
        return ret;
    }

    
    //public static string UpdateAmount(int Empid, Decimal Amount)
    //{
    //    return "Done";
    //}



}
