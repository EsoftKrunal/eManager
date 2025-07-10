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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class SendCircularForm : System.Web.UI.Page
{
    public int CID
    {
        set { ViewState["CID"] = value; }
        get { return int.Parse("0" + ViewState["CID"]); }
    }
    public int ExtID
    {
        set { ViewState["ExtID"] = value; }
        get { return int.Parse("0" + ViewState["ExtID"]); }
    }
    public int IntID
    {
        set { ViewState["IntID"] = value; }
        get { return int.Parse("0" + ViewState["IntID"]); }
    }
    public string CType
    {
        set { ViewState["CType"] = value; }
        get { return ViewState["CType"].ToString(); }
    }

    public string CircularNumber
    {
        set { ViewState["CircularNumber"] = value; }
        get { return ViewState["CircularNumber"].ToString(); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        CID = Common.CastAsInt32(Page.Request.QueryString["CID"]);
        CType = Page.Request.QueryString["CTyype"].ToString();
        CircularNumber = Page.Request.QueryString["CircularNumber"].ToString();


        if (!Page.IsPostBack)
        {
            //Delete all record where mail has not been send
            string sql = "delete from CircularInternal where CID=" + CID + " and sendBy is null";
            DataSet DsTmp = Budget.getTable(sql);

            lblCircularNumber.Text = " - "+CircularNumber;
            BindMails();
            BindSendMailDetails();            
        }
        //if (CType == "E")
        //    tblExternal.Attributes.Add("Style", "display:block;border:solid 1px #C8C8FF; height:350px;");
        //else
        //    tblExternal.Attributes.Add("Style", "display:none;");
        int s; Boolean l;
        l=Int32.TryParse("120", out s);

    }
    // image button
    protected void imgAdd_OnClick(object sender, EventArgs e)
    {
        Boolean Flag = false;
        for (int i = 0; i < chkMail.Items.Count; i++)
        {
            if (chkMail.Items[i].Selected)
                Flag = true;
        }
        if (!Flag)
        {
            lblmsg.Text = "Please select one mail address.";
            return;
        }

        try
        {
            string MailID = "";
            string[] Tmp;
            for (int i = 0; i < chkMail.Items.Count; i++)
            {
                if (chkMail.Items[i].Selected)
                {
                    Tmp = chkMail.Items[i].Text.Split(' ');

                    MailID = Tmp[Tmp.Length - 1].Trim().ToString();
                    string sql = "sp_InsertIntoCircularInternal " + CID + ",'" + MailID + "'";
                    DataSet ds = Budget.getTable(sql);
                }
            }

            lblmsg.Text = "Record saved successfully.";
            BindSendMailDetails();
            BindMails();
        }
        catch (System.Exception ex)
        {
            lblmsg.Text = "Record could not be saved." + ex.Message;
        }


    }
    protected void btnAckRecv_btnAckRecv(object sender, EventArgs e)
    {
        Boolean Flag = false;
        foreach (RepeaterItem rptitm in rptSendEmals.Items)
        {
            CheckBox chkAck = (CheckBox)rptitm.FindControl("chkAck");
            if (chkAck.Checked)
            {
                Flag = true;
            }
        }
        if (!Flag)
        {
            lblmsg.Text = "Please select one mail address for recieving acknowledge.";
            return;
        }
        DivAckRecieve.Visible = true;
        txtRemarks.Text = "";
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        DivAckRecieve.Visible = false;
        BindSendMailDetails();
    }
    //Ack Recd
    protected void btnRcvAckSave_OnClick(object sender, EventArgs e)
    {
        string sql = "update CircularInternal  set Ack_rec='Y', Ack_By=" + Common.CastAsInt32(Session["loginid"].ToString()) + "  ,Ack_on ='" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "', Remarks='" + txtRemarks.Text.Trim().Replace("'", "`") + "' where IntID=" + IntID.ToString() +" ";
        DataSet Ds = Budget.getTable(sql);

        DivAckRecieve.Visible = false;
        BindSendMailDetails();
        IntID = 0;
    }
    //Main Button
    protected void btnSendMail_btnAckRecv(object sender, EventArgs e)
    {

        Boolean Flag = false;
        for (int i = 0; i < chkMail.Items.Count; i++)
        {
            if (chkMail.Items[i].Selected)
                Flag = true;
        }
        if (!Flag)
        {
            lblmsg.Text = "Please select one mail address.";
            return;
        }

        try
        {
            string MailID = "";
            int VesselID = 0;
            string[] Tmp;
            for (int i = 0; i < chkMail.Items.Count; i++)
            {
                if (chkMail.Items[i].Selected)
                {
                    Tmp = chkMail.Items[i].Text.Split(' ');

                    MailID = Tmp[Tmp.Length - 1].Trim().ToString();
                    VesselID = Common.CastAsInt32( chkMail.Items[i].Value);

                    string sql = "sp_InsertIntoCircularInternal " + CID + ",'" + MailID + "'," + VesselID .ToString()+ "";
                    DataSet ds = Budget.getTable(sql);
                }
            }

            lblmsg.Text = "Mail send successfully.";
            BindSendMailDetails();
            BindMails();
        }
        catch (System.Exception ex)
        {
            lblmsg.Text = "Record could not be saved." + ex.Message;
        }
        

        //----------------------------------------------------------------------------------------------------------------

        string sql1 = "";
        Flag = false;
        
        foreach (RepeaterItem rptitm in rptSendEmals.Items)
        {
            HiddenField hfIntID = (HiddenField)rptitm.FindControl("hfIntID");
            CheckBox chkAck = (CheckBox)rptitm.FindControl("chkAck");
            HiddenField hfMailID = (HiddenField)rptitm.FindControl("hfMailID");
            HiddenField hfCircularNumber = (HiddenField)rptitm.FindControl("hfCircularNumber");
            HiddenField hfFileName = (HiddenField)rptitm.FindControl("hfFileName");
            HiddenField hfTopic = (HiddenField)rptitm.FindControl("hfTopic");
            HiddenField hfSendBy = (HiddenField)rptitm.FindControl("hfSendBy");

            if (hfSendBy.Value == "")
            {
                string MailDetails = SendMail.SendCircularMails("emanager@energiossolutions.com", hfMailID.Value, Session["EmailAddress"].ToString(), hfCircularNumber.Value, hfTopic.Value, Server.MapPath("~/EMANAGERBLOB/LPSQE/Circular/" + hfFileName.Value + "").ToString(), true);
                if (MailDetails == "Mail send") // It means mail hase been send
                {
                    sql1 = "Update CircularInternal set SendBy=" + Common.CastAsInt32(Session["loginid"].ToString()) + ",SendOn='" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "' where IntID=" + hfIntID.Value + "";
                    DataSet Ds = Budget.getTable(sql1);
                    Flag = true;
                }
                else
                {
                     sql1 = "delete from CircularInternal  where IntID=" + hfIntID.Value + "";
                    DataSet Ds = Budget.getTable(sql1);
                    lblmsg.Text = MailDetails;
                    BindSendMailDetails();
                    BindMails();
                    return;

                }
            }
        }

        //Delete all record where mail has not been send
        string sqlML = "delete from CircularInternal where CID=" + CID + " and sendBy is null";
        DataSet DsTmp = Budget.getTable(sqlML);


        BindSendMailDetails();
        BindMails();
       
    }
    protected void chkSelectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        bool Val;
        if (chkSelectAll.Checked)
            Val = true;
        else
            Val = false;
        for (int i = 0; i < chkMail.Items.Count; i++)
        {
            chkMail.Items[i].Selected = Val;
        }

    }

    protected void chkAckReceice_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAckReceice = (CheckBox)sender;
        HiddenField hfIntID = (HiddenField)chkAckReceice.Parent.FindControl("hfIntID");
        IntID = Common.CastAsInt32(hfIntID.Value);
                
        DivAckRecieve.Visible = true;
        txtRemarks.Text = "";
    }
    protected void imgViewComments_OnClick(object sender, EventArgs e)
    {
        ImageButton imgViewComments = (ImageButton)sender;
        HiddenField hfComments = (HiddenField)imgViewComments.Parent.FindControl("hfComments");
        
        DivAckRecieve.Visible = true;
        txtRemarks.Text = hfComments.Value;
        btnRcvAckSave.Visible = false;

    }

    protected void btnTempMail_OnClick(object sender, EventArgs e)
    {
        //string MailDetails = SendMail.SendCircularMails();
        //lblmsg.Text = MailDetails;
    }
    // Function ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public void BindMails()
    {
        string sql = "";
        if(CType=="I")
            sql = "select VesselID,( (case when VesselCode = ' ' then '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' else VesselCode+' '  end)+'- '+Email)  as  Email1  from vw_CircularDist_List where Email not in(select SendTo from CircularInternal where CID =" + CID + ") and VesselID=0 order by ((case when VesselCode = ' ' then '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' else VesselCode+' '  end)+'- '+Email)";
        else
            sql = "select VesselID,( (case when VesselCode = ' ' then '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' else VesselCode+' '  end)+'- '+Email)  as  Email1  from vw_CircularDist_List where Email not in(select SendTo from CircularInternal where CID =" + CID + ") order by ((case when VesselCode = ' ' then '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' else VesselCode+' '  end)+'- '+Email)";
        DataSet DsEmal = Budget.getTable(sql);
        if (DsEmal != null)
        {
            chkMail.DataSource = DsEmal;
            chkMail.DataTextField = "Email1";
            chkMail.DataValueField = "VesselID";
            chkMail.DataBind();
        }
    }
    public void BindSendMailDetails()
    {
        string sql = "select  (case when VesselID =0 then SendTo else (select VesselName from dbo.Vessel V where V.VesselID=CI.VesselID )end)VesselEmail " +
                " ,SendTo,IntID ,(case when Ack_rec='Y' then 'Yes' else 'No' end)Acknowledge,(select CircularNumber from CreateCircular CC where cc.CID=CI.CID)CircularNumber" +
                " ,(select (case when Status<>2 then CFileName else 'CircularFile_'+ convert(varchar(10),Cid)+'.pdf' end) from CreateCircular CC where cc.CID=CI.CID)FileName " +
                " ,(select Topic from CreateCircular CC where cc.CID=CI.CID)Topic" +
                " ,(case when Ack_rec='Y' then 'false' else 'true' end)Ack_rec" +
                " ,(select FirstName+' '+LastName from ShipSoft_Admin.dbo.UserMaster U where U .LoginID=CI.SendBy)SendBy " +
                " ,(select FirstName+' '+LastName from ShipSoft_Admin.dbo.UserMaster U where U.LoginID=CI.Ack_by)Ack_by " +
                " ,replace(convert(varchar,Ack_on ,106),' ','-')Ack_on  " +
                " ,replace(convert(varchar,SendOn,106),' ','-')SendOn ,Remarks " +
                " from CircularInternal CI  where CID="+CID+"";

        DataSet DsEmal = Budget.getTable(sql);
        if (DsEmal != null)
        {
            rptSendEmals.DataSource = DsEmal;
            rptSendEmals.DataBind();            
        }
    }
}

