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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Registers_BonusScale : System.Web.UI.Page
{
    int LoginId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        if (!Page.IsPostBack)
        {
            BindBS();
        }
    }
    protected void BindBS()
    {
        rptBS.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY BonusId DESC)  AS SNo,*,UL.FIRSTNAME + ' ' + UL.LASTNAME AS 'CreatedByName',UL1.FIRSTNAME + ' ' + UL1.LASTNAME AS 'ModifiedByName' FROM bonusmaster bm " +
                                                                        "left join dbo.userlogin ul on ul.loginid=bm.createdby " +
                                                                        "left join dbo.userlogin ul1 on ul1.loginid=bm.modifiedby " +
                                                                        "ORDER BY BonusId DESC");
        rptBS.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEffectveFrom.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fs", "alert('Please enter effective date');", true);
            txtEffectveFrom.Focus();
            return;
        }
        int Bonusid=Common.CastAsInt32(hfdbonusid.Value);
        if (Bonusid > 0)
        { 
            Common.Execute_Procedures_Select_ByQuery("UPDATE BonusMaster SET EFFDATE='" + txtEffectveFrom.Text.Trim() + "',SUPTDPER='"+ Common.CastAsInt32(txtper.Text).ToString() + "',MODIFIEDBY=" + LoginId.ToString() + ",MODIFIEDON=GETDATE() WHERE BONUSID=" + Bonusid.ToString());
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM  BonusRankDetails WHERE BONUSID=" + Bonusid.ToString());
            for (int i = 0; i <= rpt_RankList.Items.Count - 1; i++)
            {
                int RankId = Common.CastAsInt32(((HiddenField)rpt_RankList.Items[i].FindControl("hfdRank")).Value);
                decimal Amt = Common.CastAsInt32(((TextBox)rpt_RankList.Items[i].FindControl("txtAmount")).Text);
                if (Amt > 0)
                {
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO BonusRankDetails(BONUSID,Rankid,Amount) VALUES(" + Bonusid.ToString() + "," + RankId.ToString().Trim() + "," + Amt.ToString().Trim() + ")");
                }
            }
        }
        else
        { 
            DataTable dt=Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(BONUSID),0)+1 FROM BonusMaster");
            Bonusid=Common.CastAsInt32(dt.Rows[0][0]);
            Common.Execute_Procedures_Select_ByQuery("INSERT INTO BonusMaster(BONUSID,EffDate,SuptdPer,CreatedBy,CreatedOn) VALUES(" + Bonusid.ToString() + ",'" + txtEffectveFrom.Text.Trim() + "'," + Common.CastAsInt32(txtper.Text).ToString() + "," + LoginId.ToString() + ",GETDATE())");
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM  BonusRankDetails WHERE BONUSID=" + Bonusid.ToString());

            for (int i = 0; i <= rpt_RankList.Items.Count-1; i++)
            {
                int RankId = Common.CastAsInt32(((HiddenField)rpt_RankList.Items[i].FindControl("hfdRank")).Value);
                decimal Amt = Common.CastAsInt32(((TextBox)rpt_RankList.Items[i].FindControl("txtAmount")).Text);
                if (Amt > 0)
                {
                    Common.Execute_Procedures_Select_ByQuery("INSERT INTO BonusRankDetails(BONUSID,Rankid,Amount) VALUES(" + Bonusid.ToString() + "," + RankId.ToString().Trim() + "," + Amt.ToString().Trim() + ")");
                }
            }
        }
        BindBS();
        dvAddEditBS.Visible = false;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvAddEditBS.Visible = false;
    }
    protected void btnAddScale_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        txtEffectveFrom.Enabled = true;
        hfdbonusid.Value = "";
        txtEffectveFrom.Text = "";
        txtper.Text = "";
        dvAddEditBS.Visible = true;
        BindRanks();
    }
    protected void btnViewClick(object sender, EventArgs e)
    {
        int BonusId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM BonusMaster WHERE BONUSID=" + BonusId.ToString());
        if (dt.Rows.Count > 0)
        {
            btnSave.Visible = false;
            txtEffectveFrom.Text = Common.ToDateString(dt.Rows[0]["EffDate"]);
            txtper.Text = dt.Rows[0]["SuptdPer"].ToString();

            hfdbonusid.Value = BonusId.ToString();
            dvAddEditBS.Visible = true;
            BindEditRanks(BonusId);
        }
    }
    protected void btnEditClick(object sender, EventArgs e)
    {
        int BonusId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM BonusMaster WHERE BONUSID=" + BonusId.ToString());
        if(dt.Rows.Count>0)
        {
            btnSave.Visible = true;
            txtEffectveFrom.Text = Common.ToDateString(dt.Rows[0]["EffDate"]);
            txtEffectveFrom.Enabled = false;
            txtper.Text = dt.Rows[0]["Suptdper"].ToString();
            
            hfdbonusid.Value = BonusId.ToString();
            dvAddEditBS.Visible = true;
            BindEditRanks(BonusId);
        }
    }
    protected void BindRanks()
    {
        rpt_RankList.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY RANKLEVEL) AS SNO,RankId,RANKCODE,0 as Amount FROM DBO.RANK WHERE STATUSID='A' ORDER BY RANKLEVEL");
        rpt_RankList.DataBind();
    }
    protected void BindEditRanks(int BonusId)
    {
        rpt_RankList.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY RANKLEVEL) AS SNO,RankId,RANKCODE,(SELECT AMOUNT FROM BonusRankDetails WHERE RANKID=RANK.RANKID AND BONUSID=" + BonusId.ToString() + ") as Amount FROM DBO.RANK WHERE STATUSID='A' ORDER BY RANKLEVEL");
        rpt_RankList.DataBind();
    }
    protected void btnDeleteClick(object sender, EventArgs e)
    {
        int BonusId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM  BonusMaster WHERE BONUSID=" + BonusId.ToString());
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM  BonusRankDetails WHERE BONUSID=" + BonusId.ToString());
        BindBS();
    }
}
