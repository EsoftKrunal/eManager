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

public partial class VesselRecord_CrewMatrix : System.Web.UI.UserControl
{
    #region Declare Property
    private int _vesselid;
    public int Vesselid
    {
        get { return _vesselid; }
        set { _vesselid = value; }
    }

    #endregion
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 40);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        try
        {
            _vesselid = int.Parse(Session["VesselId"].ToString());
        }
        catch { } 
        //**********
        HiddenField1.Value = Convert.ToString(Vesselid);
        this.Label1.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (Page.IsPostBack == false)
        {
            binddata();
            BindFlagNameDropDown();
            try
            {
                txtVesselName.Text = Session["VesselName"].ToString();
                txtFormerVesselName.Text = Session["FormerName"].ToString();
                ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();

                showhidefromto();
            }
            catch { }
            try
            {
                if (rbfilter.SelectedIndex == 0)
                {
                    rbfilter_SelectedIndexChanged(new object(),new EventArgs()); 
                }
                else{
                    btn_Show_Click(new object(), new EventArgs());   
                }
            }
            catch{ }
        }
        btn_Print.Visible = Auth.isPrint;
    }
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddlFlagStateName.DataValueField = "FlagStateId";
        this.ddlFlagStateName.DataTextField = "FlagStateName";
        this.ddlFlagStateName.DataSource = dt1;
        this.ddlFlagStateName.DataBind();
    }
    private void binddata()
    {
        DataSet ds;
        if (rbfilter.SelectedIndex == 0)
        {
            ds = VesselCrewMatrix.getData("SelectCrewmatrixData", Vesselid, "", "");
        }
        else
        {
            ds = VesselCrewMatrix.getData("SelectCrewmatrixData", Vesselid, txtfromdate.Text, txttodate.Text);
        }
        gvmatrix.Visible = true;
        this.gvmatrix.DataSource = ds;
        lblCount.Text = "Total Members : " + ds.Tables[0].Rows.Count.ToString();   
        this.gvmatrix.DataBind();
    }
    protected void gvmatrix_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string str = gvmatrix.DataKeys[gvmatrix.SelectedIndex].Value.ToString();
            string CrewNumber =((HiddenField)gvmatrix.Rows[gvmatrix.SelectedIndex].FindControl("HiddenCrewNumber")).Value;
            if (CrewNumber.Trim().StartsWith("F"))
            {
                int familycrewid = 0;
                DataTable dts = cls_SearchReliever.SelectParentOfFamilyMember(CrewNumber);
                foreach (DataRow dr in dts.Rows)
                {
                    familycrewid = Convert.ToInt32(dr["CrewId"].ToString());
                }
                Session["Mode"] = "View";
                Session["CrewId"] = familycrewid.ToString();
            }
            else
            { 
                Session["Mode"] = "View";
                Session["CrewId"] = gvmatrix.DataKeys[gvmatrix.SelectedIndex].Value.ToString();
            }
            Response.Redirect("../CrewRecord/crewdetails.aspx");
        }
        catch (SystemException es)
        {

        }
    }
    protected void rbfilter_SelectedIndexChanged(object sender, EventArgs e)
    {   
        this.gvmatrix.Visible = false;
        showhidefromto();
        if (this.rbfilter.SelectedIndex == 0)
        {
            binddata();
        }

    }
    private void showhidefromto()
    {
        txtfromdate.Visible = (rbfilter.SelectedIndex == 1);
        txttodate.Visible = (rbfilter.SelectedIndex == 1);
        
        imgfromdate.Visible = (rbfilter.SelectedIndex == 1);
        imgtodate.Visible = (rbfilter.SelectedIndex == 1);
        btn_Show.Visible = (rbfilter.SelectedIndex == 1);
        tdfromdate.Visible = (rbfilter.SelectedIndex == 1);
        tdtodate.Visible = (rbfilter.SelectedIndex == 1);
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        if (this.txtfromdate.Text != "" && this.txttodate.Text != "")
        {
            if (Convert.ToDateTime(txtfromdate.Text) > Convert.ToDateTime(txttodate.Text))
            {
                this.Label1.Text = "From-date must be less or equal to to-date.";
                this.gvmatrix.Visible = false;
                return; 
            }
            binddata();
        }
        else
        {
            this.Label1.Text = "Please enter both from-date and to-date.";
            this.gvmatrix.Visible = false;
        }
    }
    protected void gvmatrix_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgplan1 = ((ImageButton)e.Row.FindControl("img_rel"));
            ImageButton imgplan2 = ((ImageButton)e.Row.FindControl("img_rel1"));
            //if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FirstReliverId").ToString()) > 0)
            //{
            //    imgplan1.Visible = true;
            //}
            //else
            //{
            //    imgplan1.Visible = false;
            //}

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SecondRelieverId").ToString()) > 0)
            {
                imgplan2.Visible = true;
            }
            else
            {
                imgplan2.Visible = false;
            }
                
        }
    }
    protected void gvmatrix_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void gvmatrix_PreRender(object sender, EventArgs e)
    {
        if (gvmatrix.Rows.Count <= 0)
        {
            this.Label1.Text = "No Record To Show";
        }
        else
        {
            this.Label1.Text = "";
        }
        if (this.txtfromdate.Text != "" && this.txttodate.Text != "")
        {
            HiddenField hfd;
            int i;
            for (i = 0; i <= gvmatrix.Rows.Count - 1; i++)
            {
                hfd = (HiddenField)gvmatrix.Rows[i].FindControl("HiddenSignOffDate");
                if (hfd != null)
                {
                    string k = hfd.Value.ToString();
                    if (k != "")
                    {
                        gvmatrix.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    }
                }
            }
        }
    }
    public string showHide(string id)
    {
        string ret = "block";
        if(Common.CastAsInt32( id )>0)
            ret = "block";
        else
            ret = "none";

        return ret;
    }
}
