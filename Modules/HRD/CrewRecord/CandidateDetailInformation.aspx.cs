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

public partial class CandidateDetailInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_License_Message.Text = "";
        this.lbl_info.Text = "";
        if (!Page.IsPostBack)
        {
            binddata();
            tblexp.Visible = false;
            Trcargo.Visible = false;
            if (gvcandidate.Rows.Count == 0)
            {
                this.btn_delete.Enabled = false;
                this.btn_Transfer.Enabled = false;
            }
            else
            {
                this.btn_delete.Enabled = true;
                this.btn_Transfer.Enabled = true;
            }
            
        }
    }
    private void binddata()
    {
        CandidateSelectData obj = new CandidateSelectData();
        this.gvcandidate.DataSource = obj.getData(-1);
        this.gvcandidate.DataBind();
        if (gvcandidate.Rows.Count == 0)
        {
            this.btn_delete.Enabled = false;
            this.btn_Transfer.Enabled = false;
        }
        else
        {
            this.btn_delete.Enabled = true;
            this.btn_Transfer.Enabled = true;
        }

        
   }
    protected void gvcandidate_PreRender(object sender, EventArgs e)
    {
        if (gvcandidate.Rows.Count == 0)
        {
            this.lbl_License_Message.Text = "No Record Found...";
        }
        else
        {
            this.lbl_License_Message.Text = "";
        }
    }
    protected void gvcandidate_SelectedIndexChanged(object sender, EventArgs e)
    {
        CandidateExperienceSelectData objexperience = new CandidateExperienceSelectData();
        DataSet dsex;
        tblexp.Visible = true;
        dsex=objexperience.getData(Convert.ToInt16(gvcandidate.DataKeys[gvcandidate.SelectedIndex].Value.ToString()));
        if (dsex.Tables[0].Rows.Count > 0)
        {
            this.lblexperience.Text = "";
        }
        else
        {
            this.lblexperience.Text = "No Experience Details Found";
        }
        gvexperience.DataSource = dsex;

        gvexperience.DataBind(); 

        //******** For Cargo details

        DataSet dscargo;
        Trcargo.Visible = true;
        dscargo = objexperience.getcargodata(Convert.ToInt16(gvcandidate.DataKeys[gvcandidate.SelectedIndex].Value.ToString()));
        if (dscargo.Tables[0].Rows.Count > 0)
        {
            this.lbl_cargo.Text = "";
        }
        else
        {
            this.lbl_cargo.Text = "No Dangerous Cargo Endrosement Details Found";
        }
        GvDCE.DataSource = dscargo;

        GvDCE.DataBind(); 

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        tblexp.Visible = false;
        try
        {
            string str = "";
            int i;
            for (i = 0; i < gvcandidate.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox)gvcandidate.Rows[i].FindControl("chk1"));
                if (chk.Checked)
                {
                    if (str.Length == 0)
                    {
                        str = gvcandidate.DataKeys[i].Value.ToString();
                    }

                    else
                    {
                        str = str + "," + gvcandidate.DataKeys[i].Value.ToString();
                    }
                }
            }
            if (str == "")
            {
                this.lbl_info.Text = "Select Atleast One Record To Delete";
            }
            else
            {
                DeleteCandidatePersonalDetails objdelete = new DeleteCandidatePersonalDetails();
                objdelete.deletecandidatedata(str);
                this.lbl_info.Text = "Details Deleted Successfully ";
            }
          
        }
        catch (SystemException es)
        {
            this.lbl_License_Message.Text = es.Message;
        }
        binddata();
    }
    protected void btn_Transfer_Click(object sender, EventArgs e)
    {
        DataTable dt= new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("DOB");
        dt.Columns.Add("PassportNo");
        dt.Columns.Add("CrewNumber");
        DataSet ds;
        try
        {
            string str = "";
            int i;
            for (i = 0; i < gvcandidate.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox)gvcandidate.Rows[i].FindControl("chk1"));
                if (chk.Checked)
                {
                       str = gvcandidate.DataKeys[i].Value.ToString();
                       TransferCandidateDetails objtrans = new TransferCandidateDetails();
                       ds=objtrans.transfercandidatedata(Convert.ToInt16(gvcandidate.DataKeys[i].Value.ToString()),1);
                       if (ds.Tables.Count > 0)
                       {
                           dt.Rows.Add(dt.NewRow());
                           dt.Rows[dt.Rows.Count - 1]["Name"] = ds.Tables[0].Rows[0][0].ToString();
                           dt.Rows[dt.Rows.Count - 1]["DOB"] = ds.Tables[0].Rows[0][1].ToString();
                           dt.Rows[dt.Rows.Count - 1]["PassportNo"] = ds.Tables[0].Rows[0][2].ToString();
                           dt.Rows[dt.Rows.Count - 1]["CrewNumber"] = ds.Tables[0].Rows[0][3].ToString();
                       }
                }
            }
            if (str == "")
            {
                this.lbl_info.Text = "Select Atleast One Record To Transfer Data";

            }
            else
            {
                gv_result.DataSource = dt;
                gv_result.DataBind();  
                this.lbl_info.Text = "Candidate Details Transfered Successfully.<br/> Employee Number Generated.</br>(If PassportNo is Duplicate, recrord will not be Transferred.) ";
            }
           
        }
        catch (SystemException es)
        {
            this.lbl_License_Message.Text = es.Message;
        }
        binddata();
        tblexp.Visible = false;
        Trcargo.Visible = false;
    }
}
