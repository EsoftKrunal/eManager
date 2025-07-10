using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BudgetItemComments : System.Web.UI.Page
{
    #region Properties *******************************************************
    public int CommYear
    {
        set { ViewState["CommYear"] = value; }
        get { return int.Parse("0" + ViewState["CommYear"]); }
    }
    public string CommCo
    {
        set { ViewState["CommCo"] = value; }
        get { return (ViewState["CommCo"]).ToString(); }
    }
    public string CommShipID
    {
        set { ViewState["CommShipID"] = value; }
        get { return (ViewState["CommShipID"]).ToString(); }
    }
    public int CommMajID
    {
        set { ViewState["CommMajID"] = value; }
        get { return int.Parse("0" + ViewState["CommMajID"]); }
    }
    public int CommMidID
    {
        set { ViewState["CommMidID"] = value; }
        get { return int.Parse("0" + ViewState["CommMidID"]); }
    }
    public int CommMonth
    {
        set { ViewState["CommMonth"] = value; }
        get { return int.Parse("0" + ViewState["CommMonth"]); }
    }
    public int SelectedCommMonth
    {
        set { ViewState["SelectedCommMonth"] = value; }
        get { return int.Parse("0" + ViewState["SelectedCommMonth"]); }
    }
    public string CompanyName
    {
        set { ViewState["CompanyName"] = value; }
        get { return (ViewState["CompanyName"]).ToString(); }
    }
    public int CommentID
    {
        set { ViewState["CommentID"] = value; }
        get 
        {
            return Common.CastAsInt32(ViewState["CommentID"]);
        }
    }
    public int SelMonth
    {
        set { ViewState["SelMonth"] = value; }
        get
        {
            return Common.CastAsInt32(ViewState["SelMonth"]);
        }
    }
    public int SelectedCommentID
    {
        set { ViewState["SelectedCommentID"] = value; }
        get { return int.Parse("0" + ViewState["SelectedCommentID"]); }
    }

    public decimal CommActual
    {
        set { ViewState["CommActual"] = value; }
        get
        {
            return Common.CastAsDecimal(ViewState["CommActual"]);
        }
    }
    public decimal CommConsumed
    {
        set { ViewState["CommConsumed"] = value; }
        get
        {
            return Common.CastAsDecimal(ViewState["CommConsumed"]);
        }
    }
    public decimal CommBudget
    {
        set { ViewState["CommBudget"] = value; }
        get
        {
            return Common.CastAsDecimal(ViewState["CommBudget"]);
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            CommentID = -1;

            if (Page.Request.QueryString["CommMajID"] != null)
                CommMajID = Common.CastAsInt32( Page.Request.QueryString["CommMajID"]);
            if (Page.Request.QueryString["CommMidID"] != null)
                CommMidID = Common.CastAsInt32(Page.Request.QueryString["CommMidID"]);
            if (Page.Request.QueryString["Company"] != null)
                CommCo = Page.Request.QueryString["Company"].ToString();
            if (Page.Request.QueryString["year"] != null)
                CommYear = Common.CastAsInt32(Page.Request.QueryString["year"]);
            if (Page.Request.QueryString["Vessel"] != null)
                CommShipID = Page.Request.QueryString["Vessel"].ToString();
            if (Page.Request.QueryString["Month"] != null)
                CommMonth = Convert.ToInt32(Page.Request.QueryString["Month"]);
            if (Page.Request.QueryString["CompanyName"] != null)
                CompanyName = Page.Request.QueryString["CompanyName"].ToString();
            
            // Budget Details
            if (Page.Request.QueryString["CommActual"] != null)
                CommActual = Common.CastAsDecimal( Page.Request.QueryString["CommActual"]);  
            if (Page.Request.QueryString["CommConsumed"] != null)
                CommConsumed = Common.CastAsDecimal(Page.Request.QueryString["CommConsumed"]);
            if (Page.Request.QueryString["CommBudget"] != null)
                CommBudget = Common.CastAsDecimal(Page.Request.QueryString["CommBudget"]);

            //lblIComInfo.Text = "Month - "+CommMonth.ToString() + " : Year - " + CommYear.ToString() + " : Company - " + CommCo + " : Ship ID - " + CommShipID + " : MinorCat - " +GetMinCat( CommMidID)+" : MajorCat - "+GetMajCat( CommMajID);
            //lblCommMonth.Text = ProjectCommon.GetMonthName(CommMonth.ToString());
            lblYear.Text = CommYear.ToString();
            lblCompany.Text = CommCo;
            lblShipID.Text = CommShipID;
            lblMidCat.Text = GetMinCat(CommMidID);
            lblMajorCat.Text = GetMajCat(CommMajID);
            ddlPoMonth.SelectedValue = CommMonth.ToString();

            GetCommentPerAccount();
            GetYTDCommentHistory();
        }
    }

    // Function ------------------------------------------------------------------------
    public void GetCommentPerAccount()
    {
        Common.Set_Procedures("sp_NewPR_GetCommentPerAccount"); 
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(
                new MyParameter("@CommYear",CommYear),
                new MyParameter("@CommCo",CommCo),
                new MyParameter("@CommShipID",CommShipID),
                new MyParameter("@CommMajID",CommMajID),
                new MyParameter("@CommMidID",CommMidID),
                new MyParameter("@CommMonth", ddlPoMonth.SelectedValue)   //CommMonth
            );
        DataSet Dsdata = Common.Execute_Procedures_Select();
        if (Dsdata != null)
        {
            
                rptCommPerAcc.DataSource = Dsdata;
                lblRecCount.Text = Dsdata.Tables[0].Rows.Count.ToString() + " record(s) found."; 
                rptCommPerAcc.DataBind();
        }
    }
    public void GetYTDCommentHistory()
    {
        
        //string sql = "SELECT tblBudgetLevelComments.*,datename(mm,convert(datetime,CONVERT(VARCHAR,tblBudgetLevelComments.Commper)+'-'+'1'+'-'+convert(varchar,CommYear))) as CommMonth, "+
        //                "(case when len(convert(varchar(7000), tblBudgetLevelComments.comment))>80 then substring(tblBudgetLevelComments.comment,1,80)+'....' else tblBudgetLevelComments.comment end ) as ShortComment,"+
        //                "(case when len(convert(varchar(7000), tblBudgetLevelComments.comment))>80 then 'block' else 'none' end ) as btnVisiblity "+
        //                " FROM [dbo].tblBudgetLevelComments " +
        //                " WHERE tblBudgetLevelComments.CommYear="+CommYear+"" +
        //                " AND tblBudgetLevelComments.CommCo='"+CommCo+"'" +
        //                " AND tblBudgetLevelComments.CommShipID='"+CommShipID+"'" +
        //                " AND tblBudgetLevelComments.CommMajID= "+CommMajID+"" +
        //                " AND tblBudgetLevelComments.CommMidID="+CommMidID+"" +
        //                " ORDER BY tblBudgetLevelComments.CommYear, tblBudgetLevelComments.CommPer";
        string sql = "SELECT RESULT AS MONTH ,* FROM (select * from dbo.csvTOtable('1,2,3,4,5,6,7,8,9,10,11,12',',')) MNTH " +
                        "LEFT JOIN  " +
                        "( " +
                        "SELECT tblBudgetLevelComments.*,replace(convert(varchar(15),CommentDate,106),' ','-' )as CommentDateFormated,datename(mm,convert(datetime, " +
                        "CONVERT(VARCHAR,tblBudgetLevelComments.Commper)+'-'+'1'+'-'+convert(varchar,CommYear))) as CommMonth  " +
                        "FROM [dbo].tblBudgetLevelComments  " +
                        "WHERE tblBudgetLevelComments.CommYear="+CommYear+"" +
                        "AND tblBudgetLevelComments.CommCo='"+CommCo+"'  " +
                        "AND tblBudgetLevelComments.CommShipID='"+CommShipID+"' " +
                        "AND tblBudgetLevelComments.CommMajID=" + CommMajID + "  " +
                        "AND tblBudgetLevelComments.CommMidID=" + CommMidID + " " +
                        ")  " +
                        "SRC ON SRC.COMMPER=MNTH.RESULT  " +
                        "ORDER BY MNTH.result ";
                        //SRC.CommYear, SRC.CommPer 

        DataTable DTData = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DTData != null)
        {
            
            if (DTData.Rows.Count > 0)
            {
                rptYTDCommHistory.DataSource = DTData;
                rptYTDCommHistory.DataBind();
                //Set last Comment
                //SetLastYTDComment(DTData.Rows[DTData.Rows.Count - 1]["Comment"].ToString(), Convert.ToInt32(DTData.Rows[DTData.Rows.Count - 1]["CommentID"]), DTData.Rows[DTData.Rows.Count - 1]["CommMonth"].ToString());
                CheckUpdatableRow(DTData);
            }
        }
    }

    public void CheckUpdatableRow(DataTable ds)
    {
        int i=11;
        Boolean IsAnyRowInserted = false;

        for(i=11;i>=0;i--)
        {
            string MM, DD, YY;
            string CMM, CDD, CYY;
            DateTime Date,CurrentDate;
            MM = ds.Rows[i]["Month"].ToString();
            YY = CommYear.ToString();
            DD = "1";
            Date = Convert.ToDateTime(MM + "/" + DD + "/" + YY);

            CDD = "1";
            CMM = System.DateTime.Now.Month .ToString();
            CYY = System.DateTime.Now.Year.ToString();
            CurrentDate = Convert.ToDateTime(CMM+"/"+CDD+"/"+CYY);

            if (Date >= CurrentDate)
            {
                ImageButton btnAdd = (ImageButton)rptYTDCommHistory.Items[i].FindControl("btnAdd");
                btnAdd.Visible = true;
            }
            else if (Common.CastAsInt32(MM) + 1 == Common.CastAsInt32(CMM) && Common.CastAsInt32(YY) == Common.CastAsInt32(CYY))
            {
                ImageButton btnViewComment = (ImageButton)rptYTDCommHistory.Items[i].FindControl("btnViewComment");
                ImageButton imgView = (ImageButton)rptYTDCommHistory.Items[i].FindControl("imgView");
                HiddenField hfCommentID = (HiddenField)rptYTDCommHistory.Items[i].FindControl("hfCommentID");

                Boolean IsLocked= GetLockCondition(Common.CastAsInt32(hfCommentID.Value));
                if (IsLocked)
                {
                    imgView.Visible = true;
                    imgView.ImageUrl = "~/Images/Lock.png";
                    imgView.ToolTip = "Locked";
                }
                else
                {
                    btnViewComment.Visible = true;
                }
            }
            else if (Common.CastAsInt32(MM)==12 && Common.CastAsInt32(CMM)==1 && Common.CastAsInt32(YY)+1 == Common.CastAsInt32(CYY))
            {
                ImageButton btnViewComment = (ImageButton)rptYTDCommHistory.Items[i].FindControl("btnViewComment");
                ImageButton imgView = (ImageButton)rptYTDCommHistory.Items[i].FindControl("imgView");
                HiddenField hfCommentID = (HiddenField)rptYTDCommHistory.Items[i].FindControl("hfCommentID");

                Boolean IsLocked = GetLockCondition(Common.CastAsInt32(hfCommentID.Value));
                if (IsLocked)
                {
                    imgView.Visible = true;
                    imgView.ImageUrl = "~/Images/Lock.png";
                    imgView.ToolTip = "Locked";
                }
                else
                {
                    btnViewComment.Visible = true;
                }
            }
            else
            {

                ImageButton imgView = (ImageButton)rptYTDCommHistory.Items[i].FindControl("imgView");
                imgView.Visible = true;

            }
        //if ( ds.Rows[i]["UserName"].ToString().Trim() != "")
        //    {
        //        IsAnyRowInserted = true; 
        //        if (i == 11)
        //        {
        //            ImageButton btnViewComment = (ImageButton)rptYTDCommHistory.Items[i].FindControl("btnViewComment");
        //            btnViewComment.Visible = true;

        //            for (int j = 10; j >= 0; j--)
        //            {
        //                ImageButton imgView = (ImageButton)rptYTDCommHistory.Items[j].FindControl("imgView");
        //                imgView.Visible = true;
        //            }

        //                break;
        //        }
        //        else
        //        {
        //            ImageButton btnViewComment = (ImageButton)rptYTDCommHistory.Items[i].FindControl("btnViewComment");
        //            btnViewComment.Visible = true;
        //            ImageButton btnAdd = (ImageButton)rptYTDCommHistory.Items[i+1].FindControl("btnAdd");
        //            btnAdd.Visible = true;
        //            for (int j = i-1; j >= 0; j--)
        //            {
        //                ImageButton imgView = (ImageButton)rptYTDCommHistory.Items[j].FindControl("imgView");
        //                imgView.Visible = true;
        //            }

        //            break;
        //        }

        //    }
        }
        //if (IsAnyRowInserted == false)
        //{
        //    ImageButton btnAdd = (ImageButton)rptYTDCommHistory.Items[0].FindControl("btnAdd");
        //    btnAdd.Visible = true;
        //}
        
    }
    public void SetLastYTDComment(string Commemt,int CommentID,string Month)
    {
        hfCommentID.Value = CommentID.ToString();
        
    }
    public string GetMinCat(int MidCatID)
    {
        string sql = "select MidCat from [dbo].tblAccountsMid where MidCatID=" + MidCatID + "";
        DataTable DtMinCat = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtMinCat != null)
        {
            if (DtMinCat.Rows.Count > 0)
            {
                return DtMinCat.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

    }
    public string GetMajCat(int MajCatID)
    {
        string sql = "select MajorCat from [dbo].tblAccountsMajor where MajCatID=" + MajCatID + "";
        DataTable DtMajCat = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtMajCat != null)
        {
            if (DtMajCat.Rows.Count > 0)
            {
                return DtMajCat.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        else
        {
            return "";
        }

    }
    public DataRow GetBudgetDetails(int _Month)
    {
        Common.Set_Procedures("getVarianceRepport");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
                            new MyParameter("@COMPCODE ", CommCo),
                            new MyParameter("@MNTH", _Month),
                            new MyParameter("@YR", CommYear),
                            new MyParameter("@VSLCODE", CommShipID)
            );

        DataSet DsValue = Common.Execute_Procedures_Select();
        DataRow[] dr;
        dr = DsValue.Tables[1].Select("MajCatID="+CommMajID+" and MidCatID="+CommMidID+"");
        return dr[0];
        
    }
    public bool GetLockCondition(int CommentID)
    {
        string sql = "select * from BudgetLockTable where CommentID="+CommentID+"";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT != null)
        {
            if (DT.Rows.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    // Event ------------------------------------------------------------------------
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ReportingAndAnalysis.aspx"); 
    }
    protected void btnViewComment_OnClick(object sender, EventArgs e)
    {

        ImageButton btnViewComment = (ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)btnViewComment.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)btnViewComment.Parent.FindControl("hfComment");
        HiddenField hfIntMonth = (HiddenField)btnViewComment.Parent.FindControl("hfIntMonth");

        Session.Add("sComments",hfComment.Value);
        int IsEdit = 1;
        String CommDetails = hfCommentID.Value + "," + hfIntMonth.Value;
        string query = CommYear + "," + CommCo + "," + CommShipID + "," + CommMajID + "," + CommMidID + "," + CommMonth;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&IsEdit=" + IsEdit.ToString() + "&CommActual=" + CommActual + "&CommConsumed=" + CommConsumed + "&CommBudget=" + CommBudget + "" + "','','');", true);
        //width=860px,resizable=yes , toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=100, top=100
        
        //imgSave.Visible = true;
        //ImageButton btnViewComment = (ImageButton)sender;
        //HiddenField hfCommentID = (HiddenField)btnViewComment.FindControl("hfCommentID");
        //CommentID = Common.CastAsInt32(hfCommentID.Value);

        //HiddenField hfMonth = (HiddenField)btnViewComment.FindControl("hfMonth");
        //lblMonth.Text = hfMonth.Value;

       //HiddenField hfComment = (HiddenField)btnViewComment.FindControl("hfComment");
        //txtYTDcomm.Text=hfComment.Value;

        //HiddenField hfIntMonth = (HiddenField)btnViewComment.FindControl("hfIntMonth");
        //SelectedCommMonth=Common.CastAsInt32(hfIntMonth.Value);
        //DataRow dr= GetBudgetDetails(Common.CastAsInt32(hfIntMonth.Value));
        //lblActual.Text = dr["AcctYTDAct"].ToString();
        //lblConsumed.Text = dr["AcctYTDCons"].ToString();
        //lblBudget.Text = dr["AcctYTDBgt"].ToString();
        ////-----------------------
        //SelMonth = Common.CastAsInt32(hfIntMonth.Value);
        //GetYTDCommentHistory();
    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {

        ImageButton btnViewComment = (ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)btnViewComment.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)btnViewComment.Parent.FindControl("hfComment");
        HiddenField hfIntMonth = (HiddenField)btnViewComment.Parent.FindControl("hfIntMonth");


        int IsEdit = 1;
        //Session.Add("sComments", "");
        Session.Add("sComments", hfComment.Value);
        String CommDetails = hfCommentID.Value + "," + hfIntMonth.Value;
        string query = CommYear + "," + CommCo + "," + CommShipID + "," + CommMajID + "," + CommMidID + "," + CommMonth; ;

        
//        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&IsEdit=" + IsEdit.ToString() + "','','toolbar=no,location=no,directories=no,status=no,menubar=no,copyhistory=yes');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&IsEdit=" + IsEdit.ToString() + "&CommActual=" + CommActual + "&CommConsumed=" + CommConsumed + "&CommBudget=" + CommBudget + "" + "','','');", true);
        //width=860px,resizable=no , toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=100, top=100
        
        //txtYTDcomm.Text = "";
        //imgSave.Visible = true;
        //ImageButton btnAdd = (ImageButton)sender;
        //HiddenField hfCommentID = (HiddenField)btnAdd.FindControl("hfCommentID");
        //HiddenField hfMonth = (HiddenField)btnAdd.FindControl("hfMonth");
        //lblMonth.Text = hfMonth.Value;
        //// Budget
        //HiddenField hfIntMonth = (HiddenField)btnAdd.FindControl("hfIntMonth");
        //SelectedCommMonth = Common.CastAsInt32(hfIntMonth.Value);
        //DataRow dr = GetBudgetDetails(Common.CastAsInt32(hfIntMonth.Value));
        //lblActual.Text = dr["AcctYTDAct"].ToString();
        //lblConsumed.Text = dr["AcctYTDCons"].ToString();
        //lblBudget.Text = dr["AcctYTDBgt"].ToString();
        ////-----------------------
        //SelMonth = Common.CastAsInt32(hfIntMonth.Value);
        //CommentID = Common.CastAsInt32(hfCommentID.Value);
        //GetYTDCommentHistory();
    }
    protected void imgView_OnClick(object sender, EventArgs e)
    {
        ImageButton  imgView=(ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)imgView.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)imgView.Parent.FindControl("hfComment");
        HiddenField hfIntMonth = (HiddenField)imgView.Parent.FindControl("hfIntMonth");


        Session.Add("sComments", hfComment.Value);
        String CommDetails = hfCommentID.Value + "," + hfIntMonth.Value;
        string query = CommYear + "," + CommCo + "," + CommShipID + "," + CommMajID + "," + CommMidID + "," + CommMonth; ;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "','','resizable=yes,toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=yes,copyhistory=yes');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&CommActual=" + CommActual + "&CommConsumed=" + CommConsumed + "&CommBudget=" + CommBudget + "" + "','','');", true);
        //,resizable=no , toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,copyhistory=yes,left=100, top=100
        
    }
    protected void btnReload_OnClick(object sender, EventArgs e)
    {
        GetYTDCommentHistory();
    }
    protected void ddlPoMonth_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommentPerAccount();
    }
    //protected void imgSave_OnClick(object sender, EventArgs e)
    //{
    //    if (Common.CastAsInt32(hfCommentID.Value) != 0)
    //    {
    //        string sql = "update [dbo].tblBudgetLevelComments set Comment ='" + txtYTDcomm.Text.Replace("'", "''") + "' where CommentID=" + Common.CastAsInt32(hfCommentID.Value) + "";
    //        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

    //        Common.Set_Procedures("ExecQuery");
    //        Common.Set_ParameterLength(1);
    //        Common.Set_Parameters(new MyParameter("@Query", sql));
    //        Boolean res;
    //        DataSet dsData = new DataSet();
    //        res = Common.Execute_Procedures_IUD(dsData);
    //        if (res)
    //        {
    //            lblmsg.Text = "Comment updated successfully.";
    //            GetYTDCommentHistory();
    //        }
    //        else
    //        {
    //            lblmsg.Text = "Comment could not be updated.";
    //        }
    //    }
    //    else
    //    {
    //        lblmsg.Text = "Nothing to save.";
    //    }


    //} //Old Saving code

}

