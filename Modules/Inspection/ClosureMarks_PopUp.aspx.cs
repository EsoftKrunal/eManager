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

public partial class ClosureMarks_PopUp : System.Web.UI.Page
{
    string ObsDefText = "";
    public int InsDueId=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try { InsDueId = int.Parse(Session["Insp_Id"].ToString()); }
        catch 
        {
            ScriptManager.RegisterStartupScript(this,this.GetType(),"key","alert('Your session has been expired.');window.close();",true);
            return;
        }
        if(!(IsPostBack))
        {
            DataTable dt22 = Budget.getTable("SELECT STATUS FROM T_INSPECTIONDUE WHERE ID=" + InsDueId.ToString()).Tables[0];
            if (dt22.Rows.Count > 0)
            {
                if (dt22.Rows[0]["STATUS"].ToString().ToLower().Trim() == "closed")
                {
                    btnSave.Visible = false;   
                }
            }

            DataTable dt = Budget.getTable("SELECT * FROM T_CDIRESULT WHERE INSID=" + InsDueId.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                txtQustions.Text =dt.Rows[0]["IQestions"].ToString();
                txtNeg.Text =dt.Rows[0]["INegStat"].ToString();
                txtTotNeg.Text =dt.Rows[0]["ITotNeg"].ToString();
                txtNegReco.Text =dt.Rows[0]["INegReco"].ToString();
                txtPercComp.Text=dt.Rows[0]["IPercComp"].ToString();
                txtNegDesi.Text=dt.Rows[0]["INegDesi"].ToString();
                txtQustions1.Text=dt.Rows[0]["EQestions"].ToString();
                txtNeg1.Text=dt.Rows[0]["ENegStat"].ToString();
                txtTotNeg1.Text=dt.Rows[0]["ETotNeg"].ToString();
                txtNegReco1.Text=dt.Rows[0]["ENegReco"].ToString();
                txtPercComp1.Text=dt.Rows[0]["EPercComp"].ToString();
                txtNegDesi1.Text = dt.Rows[0]["ENegDesi"].ToString();
            }
        }
        lblMessage.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Budget.getTable("SELECT * FROM T_CDIRESULT WHERE INSID=" + InsDueId.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                Budget.getTable("INSERT INTO T_CDIRESULT(InsId,IQestions,INegStat,ITotNeg,INegReco,IPercComp,INegDesi,EQestions,ENegStat,ETotNeg,ENegReco,EPercComp,ENegDesi)" +
                " VALUES(" + InsDueId.ToString() + ",'" +
                txtQustions.Text + "','" + txtNeg.Text + "','" + txtTotNeg.Text + "','" + txtNegReco.Text + "','" + txtPercComp.Text + "','" + txtNegDesi.Text + "','" +
                txtQustions1.Text + "','" + txtNeg1.Text + "','" + txtTotNeg1.Text + "','" + txtNegReco1.Text + "','" + txtPercComp1.Text + "','" + txtNegDesi1.Text + "')");
            }
            else
            {
                Budget.getTable("UPDATE T_CDIRESULT SET" +
                            " IQestions='" + txtQustions.Text + "'," +
                            " INegStat='" + txtNeg.Text + "'," +
                            " ITotNeg='" + txtTotNeg.Text + "'," +
                            " INegReco='" + txtNegReco.Text + "'," +
                            " IPercComp='" + txtPercComp.Text + "'," +
                            " INegDesi='" + txtNegDesi.Text + "'," +

                            " EQestions='" + txtQustions1.Text + "'," +
                            " ENegStat='" + txtNeg1.Text + "'," +
                            " ETotNeg='" + txtTotNeg1.Text + "'," +
                            " ENegReco='" + txtNegReco1.Text + "'," +
                            " EPercComp='" + txtPercComp1.Text + "'," +
                            " ENegDesi='" + txtNegDesi1.Text + "'" +
                            " WHERE INSID=" + InsDueId.ToString());
                
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Successfully saved.');window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btn_SaveApp').disabled='';window.close();", true);  
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Unable to save the details.');", true);  
        }
    }
}
