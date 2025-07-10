<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewActivities.aspx.cs" Inherits="CrewActivities" Title="User Tools" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
         <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #e4d7d7;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 133px;
        }
        .style2
        {
            height: 6px;
            width: 133px;
        }
        .style3
        {
            width: 116px;
        }
        .style4
        {
            height: 6px;
            width: 116px;
        }
        .style5
        {
            width: 205px;
        }
        .style6
        {
            height: 6px;
            width: 205px;
        }
        .style7
        {
            width: 124px;
        }
        .style8
        {
            height: 6px;
            width: 124px;
        }
    </style>
    <style type="text/css">
.selbtn
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px; 
}
.btn1
{
	 background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: left">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style=" text-align :left; vertical-align : top;" >
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" colspan="2">
                <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
                User Tools
                </td>
            </tr>
             <tr>
            <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
            <div style="">
                <asp:Button runat="server" ID="btnActivity" Text="Crew Activity" CssClass="btn1" OnClick="btnActivity_Click"  CausesValidation="false"  />&nbsp;
                <asp:Button runat="server" ID="btnCrewDocuments" Text="Document Checklists" CssClass="btn1" onclick="btnCrewDocs_Click" CausesValidation="false" /> &nbsp
                <asp:Button runat="server" ID="btnCheckListMaster" Text="Checklist Master" CssClass="btn1" onclick="btnCheckListMaster_Click" CausesValidation="false"  /> &nbsp
                 <asp:Button runat="server" ID="btnCrewContractHistory" Text="Contract Details" CssClass="btn1" onclick="btnCrewContractHistory_Click" CausesValidation="false"  /> &nbsp
                <%--<asp:Button runat="server" ID="btnChecklist" Text="Approval Management" CssClass="btn1" onclick="btnChecklist_Click" CausesValidation="false" Visible="false" />--%>
            </div>
            </td>
        </tr>
            </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%" id="tblActivity" runat="server">
            <tr>
                <td colspan="2" style="width: 700px;height:25px;text-align :center"  >
                    <asp:Label ID="lblCrewMemberMsg" runat="server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%"> </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                    <table style="width:100%;">
                        <tr>
                            <td style="padding-right: 5px; text-align :right;width:100px;height:35px; ">
                                 Crew Number :
                            </td>
                            <td style="text-align :left;width:400px; height:35px;padding-right: 10px;">
                                <asp:TextBox ID="txtMemberNo" runat="server" MaxLength="6" Width="150px" OnTextChanged="txtMemberNo_TextChanged"></asp:TextBox> 
                                 <asp:RequiredFieldValidator id="rfvMemberNo" runat="server" ErrorMessage="Required." ControlToValidate="txtMemberNo" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" Width="100px" OnClick="btnSearch_Click" />
                            </td>
                            <td style="text-align :left;width:300px; height:35px;">

                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align:left;padding:5px;">
                                <asp:Button runat="server"  CommandArgument="0" Text="Onboard Promotion" OnClick="Menu1_MenuItemClick" ID="b1" CssClass="btn1"  Font-Bold="True" Width="140px" CausesValidation="false" />
                                &nbsp;
                                <asp:Button runat="server"  CommandArgument="1" Text="NTBR/De-NTBR" OnClick="Menu1_MenuItemClick"  ID="b2"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="false"/>
                                 &nbsp;
                                <asp:Button runat="server"  CommandArgument="2" Text="Active/InActive" OnClick="Menu1_MenuItemClick" ID="b3"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="false"/>
                                &nbsp;
                                <asp:Button runat="server"  CommandArgument="3" Text="Crew Extension" OnClick="Menu1_MenuItemClick" ID="b4"  CssClass="btn1"  Font-Bold="True" Width="120px" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>
                 
                </td>
            </tr>
            <tr id="tbltr" runat="server" visible="false">
                <td style="width:75%;">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                        <tr>
                            <td ><asp:LinkButton runat="server" ID="lnkPopUp" Visible ="false" ForeColor="Red" Font-Size="14pt" OnClientClick ="window.open('../CriticalRemarkPopUp.aspx');return false;" Text ="Critical Remarks" Font-Bold="True" ></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td style="width: 600px; text-align :center ">
                                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%"></asp:Label></td>
                                         <td style="text-align: left">
                                         </td>
                                         <td style="text-align: center">
                                         <asp:HyperLink runat="Server" ID="doc_Alert" Target="_blank" NavigateUrl="DocumentAlerts.aspx" ForeColor="Red" ></asp:HyperLink> 
                                         </td>
                                         <td>
                                         </td>
                                     </tr>
                                    <%--<tr>
                                        <td style="height: 38px;">
                                  <asp:Menu ID="Menu1" runat="server" Width="357px" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" >
                                    <Items>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/promotion_a.gif" Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/ntbr_d.gif" Text=" " Value="1"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/actinact_d.gif" Text=" " Value="2"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/crewext_d.gif" Text=" " Value="3"></asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                                        </td>
                                        <td style="height: 38px;width :50px;"><asp:ImageButton ID="imgbtn_Personal" runat="server" ImageUrl="~/Modules/HRD/Images/btnPersonal.gif" OnClick="imgbtn_Personal_Click" ToolTip="Personal Details/Exp." CausesValidation="False" /></td>
                                        <td style="height: 38px;width :50px;"><asp:ImageButton ID="imgbtn_Document" runat="server" ImageUrl="~/Modules/HRD/Images/btnDocument.gif" OnClick="imgbtn_Document_Click" ToolTip="Crew Documents" CausesValidation="False" /></td>
                                        <td style="height: 38px;width :50px;"><asp:ImageButton ID="imgbtn_CRM" runat="server" ImageUrl="~/Modules/HRD/Images/btnCRM.gif" OnClick="imgbtn_CRM_Click" ToolTip="CRM & HRD" CausesValidation="False" /></td>
                                    </tr>--%>
                                </table>
                               
                                 <table cellpadding="0" cellspacing="0" width="100%" style="display:none;">
                                    <tr>
                                <td style="height: 13px">
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    
                                    <table border="0" cellpadding="3" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td  style="width: 151px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label43" runat="server" meta:resourcekey="Label43Resource1" Text="First Name:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_FirstName" runat="server" CssClass="required_box" MaxLength="24"
                                                        meta:resourcekey="txt_FirstNameResource1" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                                <td style="padding-left: 5px; width: 121px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label37" runat="server" meta:resourcekey="Label37Resource1" Text="Middle Name:"
                                                        Width="82px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_MiddleName" runat="server" CssClass="input_box" MaxLength="24" Width="160px"></asp:TextBox></td>
                                                <td  style="width: 174px; height: 13px">
                                                    <asp:Label ID="Label38" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                        Width="100%"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_LastName" runat="server" CssClass="required_box" MaxLength="24" Width="160px" ReadOnly="True"></asp:TextBox></td>                    
                                            </tr>
                                            <tr>
                                                <td  style="width: 151px; height: 13px; text-align: right">
                                                    <asp:Label ID="Label15" runat="server" meta:resourcekey="Label15Resource1" Text="Current Rank:"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="ddcurrentrank" runat="server" CssClass="input_box" meta:resourcekey="ddcurrentrankResource1"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td style="padding-left: 5px; height: 13px; text-align: right; width: 121px;">
                                                    <asp:Label ID="Label44" runat="server" meta:resourcekey="Label44Resource1" Text="Last Vessel:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_LastVessel" runat="server" CssClass="input_box" MaxLength="24"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td  style="width: 174px; height: 13px">
                                                    Passport No :</td>
                                                <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_Passport" runat="server" CssClass="input_box" MaxLength="49"
                                                        ReadOnly="True" TabIndex="0" Width="160px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td  style="width: 151px; height: 13px; text-align: right">
                                                    Status:</td>
                                                <td colspan="5" style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                    <asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" MaxLength="10" meta:resourcekey="txt_MemberIdResource1"
                                                        ReadOnly="True" Width="520px"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                        </td>
                                    </tr>
                                    </table>
                                <center>
                                <asp:Label runat="server" ID="lblMessMain" ForeColor="Red" style=" display : block"></asp:Label>
                                </center> 
                                <asp:Panel id="tdWhole" runat="server" >
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="Tab1" runat="server">
                                        <table cellspacing="0" width="100%" border="1" >
                                        <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lbl_promotion_message" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td runat="server" id="trPromotion">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 15px; text-align :right ">
                                                    Emp.#:</td>
                                                <td style="text-align :left">
                                                    <asp:TextBox ID="txtPEmpNo" MaxLength="6" runat="server" CssClass="input_box" 
                                                        Enabled="false" TabIndex="1"></asp:TextBox></td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Name:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPName" runat="server" Width="100%"></asp:Label></td>
                                                <td style="text-align: right; padding-right: 15px">
                                                    Present Rank:</td>
                                                <td  style="text-align: left;">
                                                    <asp:Label ID="lblPPresentRank" runat="server"></asp:Label>
                                                    </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Status:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPStatus" runat="server" ></asp:Label></td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Vessel:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPVessel" runat="server" ></asp:Label></td>
                                                <td style="text-align: right; padding-right: 15px">
                                                    Signed Off:</td>
                                                <td  style="text-align: left">
                                                    <asp:Label ID="lblPSignedOff" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td >
                                                </td>
                                                <td >
                                                    &nbsp;
                                                </td>
                                                <td >
                                                </td>
                                                <td >
                                                </td>
                                                <td  style="text-align: right">
                                                </td>
                                                <td  style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Available Date:</td>
                                                <td style="text-align :left">
                                                    <asp:Label ID="lblPAvailableDate" runat="server" Width="122px"></asp:Label></td>
                                                <td style="padding-right: 15px;text-align :right ">
                                                    Promotion Rank:</td>
                                                <td style="text-align :left">
                                                    <asp:DropDownList ID="ddlPRank" runat="server" CssClass="input_box" 
                                                        Width="182px" TabIndex="2">
                                                    </asp:DropDownList></td>
                                                <td style="text-align: right; padding-right: 15px">
                                                    Promotion Dt:</td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txt_PPromotionDt" runat="server" CssClass="required_box" 
                                                        MaxLength="15" Width="80px" TabIndex="3"></asp:TextBox>
                                                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td ></td>
                                                <td >&nbsp;</td>
                                                <td ></td>
                                                <td ></td>
                                                <td style="text-align: left">
                                                </td>
                                                <td style="text-align: left;">
                                                <asp:RequiredFieldValidator runat="server" ID="Req1" Display="Dynamic" 
                                                        ControlToValidate="txt_PPromotionDt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                        ControlToValidate="txt_PPromotionDt" 
                                                        ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" 
                                                        ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                </td>
                                                <tr>
                                                <td colspan="6" style="border: solid 1px gray; text-align :right ; height :30px;" >
                                                <asp:Button ID="btn_SavePromotion" runat="server" CssClass="btn" TabIndex="5" Text="Promote" Width="80px" OnClick="btn_SavePromotion_Click"/>
                                        &nbsp;
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" 
                                            TargetControlID="txt_PPromotionDt"></ajaxToolkit:CalendarExtender>
                                                </td>
                                                </tr>
                                            </tr>
                                        </table>
                                        </td>
                                     </tr>
                                <tr>
                                <td style=" height :90px;">&nbsp;  </td>
                                </tr>
                            </table>
                        </asp:View>
                                <asp:View ID="Tab2" runat="server">
                        <table width="100%" cellspacing="0" cellpadding ="0" border="1"  >
                         <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lbl_ntbr_message" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                             <tr><td style="text-align: center">
                             <tr><td>
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="text-align: right;padding-right: 5px; width: 114px;" >Employee #:</td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtNEmpNo" runat="server" CssClass="required_box" 
                                                    Width="100px" AutoPostBack="True" MaxLength="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="txtNEmpNo" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                            <td  style="text-align: right;padding-right: 5px;">Name:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNName" runat="server" CssClass="input_box" ReadOnly="True" 
                                                    Width="190px" BackColor="#E2E2E2"></asp:TextBox></td>
                                            <td style="text-align: right;">Nationality:</td>
                                            <td align="left"><asp:TextBox ID="txtNNationality" runat="server" 
                                                    CssClass="input_box" ReadOnly="True" Width="140px" BackColor="#E2E2E2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td  style="text-align: right;padding-right: 5px; width: 114px; ">Present Rank:</td>
                                            <td align="left"><asp:TextBox ID="txtNPresentRank" runat="server" 
                                                    CssClass="input_box" ReadOnly="True" Width="100px" BackColor="#E2E2E2" ></asp:TextBox></td>
                                            <td  style="text-align: right;padding-right: 5px;"> Last Vessel:</td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtNLastVsl" runat="server" 
                                                    CssClass="input_box" ReadOnly="True" Width="190px" BackColor="#E2E2E2"></asp:TextBox></td>
                                            <td style="text-align: right;text-align: right">Signed Off:</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txtNSignedOff" runat="server" 
                                                    CssClass="input_box" ReadOnly="True" Width="140px" BackColor="#E2E2E2"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td  style="text-align: right;padding-right: 5px; width: 114px; ">Available Date:</td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtNAvailableDate" runat="server" CssClass="input_box" 
                                                    ReadOnly="True" Width="100px" BackColor="#E2E2E2"></asp:TextBox>
                                            </td>
                                            <td  style="padding-right: 5px;"></td>
                                            <td align="left" ></td>
                                            <td style="width: 94px" ></td>
                                            <td ></td>
                                        </tr>
                                        <tr>
                                            <td  style="text-align: right;padding-right: 5px; width: 114px; height: 6px;">NTBR Date:</td>
                                            <td align="left" style="height: 6px">
                                                <asp:TextBox ID="txtNNTBRDate" runat="server" CssClass="required_box" 
                                                    MaxLength="15" Width="100px" TabIndex="1"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                    ControlToValidate="txtNNTBRDate" 
                                                    ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" 
                                                    ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                                                    ControlToValidate="txtNNTBRDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                            </td>
                                            <td  style="text-align: right;padding-right: 5px; height: 6px;">Reason:</td>
                                            <td align="left" style="height: 6px">
                                                <asp:DropDownList ID="ddNNTBRReason" 
                                                    runat="server" CssClass="required_box" Width="195px" TabIndex="2"></asp:DropDownList>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                                    ControlToValidate="ddNNTBRReason" ErrorMessage="Required." MaximumValue="5000" 
                                                    MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                            <td style="text-align: right;padding-right: 5px; height: 6px;">NTBR/DeNTBR:</td>
                                            <td style="height: 6px; text-align: left">
                                                <asp:DropDownList ID="ddlNNTBR" runat="server" CssClass="required_box" 
                                                    Width="148px" AutoPostBack="True" 
                                                    OnSelectedIndexChanged="ddntbr_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                <asp:ListItem Value="1">NTBR</asp:ListItem>
                                                <asp:ListItem Value="2">DeNTBR</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" 
                                                    ControlToValidate="ddlNNTBR" ErrorMessage="Required." MaximumValue="5000" 
                                                    MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  style="padding-right: 5px; width: 114px;text-align: right;">Remarks:</td>
                                            <td align="left" colspan="5" rowspan="3"> 
                                                <asp:TextBox ID="txtNRemarks" 
                                                    runat="server" CssClass="input_box" Height="48px" TextMode="MultiLine" 
                                                    Width="627px" MaxLength="999" TabIndex="4"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td  style="padding-right: 5px; width: 114px;"></td>
                                        </tr>
                                        <tr>
                                            <td  style="padding-right: 5px; width: 114px; "></td>
                                        </tr>
                                        <tr>
                                            <td  style="padding-right: 5px; width: 114px;"></td>
                                            <td align="left" colspan="5" rowspan="1" >
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" 
                                                    TargetControlID="txtNNTBRDate"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                </td></tr>
                                <tr>
                                <td style="text-align :right; background-color :#e2e2e2 ">
                                <asp:Button ID="btn_save_Ntbr" runat="server" CssClass="btn" 
                                        OnClick="btn_save_Click" Text="Save" Width="80px" TabIndex="5" />&nbsp;</td></tr>
                             </table>
                                    </asp:View>
                                <asp:View ID="Tab3" runat="server">
                                <table width="100%" cellpadding="0" cellspacing ="0" border="1">
                                <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lbl_AI_message" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                       <tr><td>
                            <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                <tr>
                                    <td style="padding-right: 5px; width: 153px;" align="right">Employee #:</td>
                                    <td align="left" class="style3" >
                                    <asp:TextBox ID="txtAIEmpNo" runat="server" CssClass="required_box" Width="100px" 
                                            AutoPostBack="True" MaxLength="6" 
                                            ></asp:TextBox></td>
                                    <td align="right" style="padding-right: 5px;" class="style1">Name:</td>
                                    <td align="left" class="style5">
                                        <asp:TextBox ID="txtAEmpName" runat="server" CssClass="input_box" 
                                            ReadOnly="True" Width="190px"></asp:TextBox></td>
                                    <td align="right" style="padding-right: 5px; " class="style7">Nationality:</td>
                                    <td align="left"><asp:TextBox ID="txtANationality" runat="server" 
                                            CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px;">
                                        Present Rank:</td>
                                    <td align="left" class="style3">
                                        <asp:TextBox ID="txtAPresentRank" runat="server" CssClass="input_box" 
                                            ReadOnly="True" Width="100px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style1" style="padding-right: 5px;">
                                        Last Vessel:</td>
                                    <td align="left" class="style5">
                                        <asp:TextBox ID="txtALastVsl" runat="server" CssClass="input_box" 
                                            ReadOnly="True" Width="190px"></asp:TextBox>
                                    </td>
                                    <td class="style7" style="text-align: right;">
                                        Signed Off:</td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtASignedOff" runat="server" CssClass="input_box" 
                                            ReadOnly="True" Width="140px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px; ">Available Date:</td>
                                    <td align="left" class="style3">
                                        <asp:TextBox ID="txtAAvlDate" runat="server" 
                                            CssClass="input_box" ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    <td align="right" style="padding-right: 5px;" class="style1"> Current Reason:</td>
                                    <td align="left" class="style5" >
                                        <asp:Label ID="lblALastReason" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: right" class="style7">Current Status:</td>
                                    <td style="text-align: left" >
                                        <asp:Label ID="lblALastStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px; height: 6px;">
                                        Active/InActive Date:</td>
                                    <td align="left" class="style4" >
                                        <asp:TextBox ID="txtAIDate" runat="server" 
                                            CssClass="required_box" Width="80px" TabIndex="1"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                    </td>
                                    <td align="right" style="padding-right: 5px;" class="style2">Reason:</td>
                                    <td align="left" class="style6" >
                                        <asp:DropDownList ID="ddlAIReason" runat="server" CssClass="required_box" 
                                            TabIndex="2" Width="195px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; padding-right: 5px;" class="style8" >New Status:</td>
                                    <td style ="text-align : left; height: 6px;" >
                                        <asp:DropDownList ID="ddlANewStatus" runat="server" AutoPostBack="True" 
                                            CssClass="required_box" 
                                            OnSelectedIndexChanged="ddlANewStatus_SelectedIndexChanged" TabIndex="3" 
                                            Width="148px">
                                            <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                            <asp:ListItem Value="4">InActive</asp:ListItem>
                                            <asp:ListItem Value="2">Active</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px; height: 6px;">&nbsp;</td>
                                    <td align="left" class="style4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ControlToValidate="txtAIDate" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                            ControlToValidate="txtAIDate" Display="Dynamic" ErrorMessage="Invalid Date." 
                                            ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"></asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right" style="padding-right: 5px; " class="style2">&nbsp;</td>
                                    <td align="left" class="style6">
                                        &nbsp;</td>
                                    <td style="padding-right: 5px; text-align :right" class="style8">&nbsp;</td>
                                    <td style="height: 6px; text-align: left">
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" 
                                            ControlToValidate="ddlANewStatus" ErrorMessage="Required." MaximumValue="5000" 
                                            MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px; ">
                                        Remarks:</td>
                                    <td align="left" colspan="5" rowspan="3">
                                        <asp:TextBox ID="txtARemarks" runat="server" CssClass="input_box" Height="48px" 
                                            MaxLength="999" TabIndex="4" TextMode="MultiLine" Width="530px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px;"></td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px;"></td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-right: 5px; width: 153px; "></td>
                                    <td align="left" colspan="5" rowspan="1">
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                            Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" 
                                            TargetControlID="txtAIDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td></tr>
                        <tr>
    <td style=" text-align :right; background-color :#e2e2e2 ">
    <asp:Button ID="btnSaveAI" runat="server" CssClass="btn" OnClick="btn_saveAI_Click" Text="Save" Width="59px" TabIndex="5" /></td></tr>
 </table>
                                </asp:View>
                                <asp:View ID="Tab4" runat="server">

                                    <div>
                                         <table width="100%" class="bordered" cellspacing="0" cellpadding="4" style="border-collapse:collapse">
                                            <tr class="headerstylegrid">
                                                <td style="width:150px;">Vessel Name</td>
                                                <td style="width:150px">Contract #</td>
                                                <td style="width:90px">Relief Due.</td>
                                                <td style="width:120px">Next Relief Due.</td>
                                                <td style="width:90px">Activity Type</td>
                                                <td>Remarks</td>
                                                <td style="width:150px">Activity By</td>
                                                <td style="width:90px">ActivityOn</td>
                                            </tr>
                                             </table>
                                        <div style="oveflow-y:scroll;overflow-x:hidden;height:160px;">
                                        <table width="100%" class="bordered" cellspacing="0" cellpadding="4" style="border-collapse:collapse">
                                            <asp:Repeater runat="server" ID="rptCrewActivity">
                                                
                                               
                                                <ItemTemplate>
                                                <tr>
                                                    <td style="width:150px;text-align:left;"><%#Eval("VesselName")%></td>
                                                    <td style="width:150px"><%#Eval("ContractReferenceNumber")%></td>
                                                    <td style="width:90px"><%#Common.ToDateString(Eval("ReliefDueDt"))%></td>
                                                    <td style="width:120px"><%#Common.ToDateString(Eval("NextReliefDueDate"))%></td>
                                                    <td style="width:90px"><%#Eval("ActivityType")%></td>
                                                    <td style="text-align:left"><%#Eval("Remarks")%></td>
                                                    <td style="text-align:left;width:150px"><%#Eval("ActionBy")%></td>
                                                    <td style="width:90px"><%#Common.ToDateString(Eval("ActionOn"))%></td>
                                                </tr>
                                                </ItemTemplate>
                                          
                                            </asp:Repeater>
                                        </table>
                                        </div>
                                    <div style="padding:5px; background-color:#e2e2e2">
                                        <asp:RadioButton runat="server" ID="radExtension" GroupName="g1" Text="Extension" Font-Bold="true" AutoPostBack="true" Checked="true" OnCheckedChanged="radCheck_OnCheckedChanged" />
                                        <asp:RadioButton runat="server" ID="radEarly" GroupName="g1" Text="Early Relief" Font-Bold="true" AutoPostBack="true" OnCheckedChanged="radCheck_OnCheckedChanged" />
                                    </div>
                                    <div style="padding:5px">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server" id="tabExtension" visible="true" >
                                        <tr>
                                            <td style=" text-align :center ">
                                                <asp:Label ID="lbl_ext_message" runat="server" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td runat="server" id="tdExt" >
                                        <table cellpadding="3" cellspacing="0" style="width: 100%; padding-right: 0px; ">
       <tr>
           <td style="text-align: right; width: 140px;">
               Current Rank :</td>
           <td style="text-align: left">
               <asp:DropDownList ID="ddl_SignOnas" runat="server" CssClass="required_box" Width="200px">
               </asp:DropDownList>
               </td>
           <td style="text-align: right; width: 140px;">
               Sign On Date:</td>
           <td style="text-align: left; width: 245px;">
               <asp:TextBox ID="txt_SignOnDate" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" runat="server" CssClass="required_box" Width="80px"></asp:TextBox>
           
               </td>
           <td style="text-align: right; width: 88px;">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; height: 19px; width: 140px;">
               Duration(Months):</td>
           <td style="text-align: left; height: 19px;">
               <asp:TextBox ID="txt_Duration" runat="server" CssClass="input_box" Width="82px" MaxLength="2" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                </td>
           <td style="text-align: right; height: 19px; width: 140px;">
               Relief Due:</td>
           <td style="text-align: left; height: 19px; width: 245px;">
               <asp:TextBox ID="txt_ReliefDate" runat="server" CssClass="input_box" Width="82px" ReadOnly="True"></asp:TextBox>
               <asp:ImageButton ID="imgSignOffDate" Visible ="false" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
               
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_ReliefDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txt_ReliefDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                                  
               </td>
           <td style="text-align: right; height: 19px; width: 88px;">
                                                                </td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;" valign="top">
               Remarks:</td>
           <td style="text-align: left" colspan="3">
               <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" 
                   TextMode="MultiLine" MaxLength="99" Height="46px" Width="757px">0</asp:TextBox></td>
           <td style="text-align: right; width: 88px;" valign="top">
               </td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;">Updated By:</td>
           <td style="text-align: left" ><asp:Label ID="lbl_UpdatedBy" runat="server"></asp:Label>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgSignOffDate" PopupPosition="TopRight" TargetControlID="txt_ReliefDate" ></ajaxToolkit:CalendarExtender>
               </td>
               <td style=" text-align :right " >Updated On:</td>
               <td style="text-align:left" >
               <asp:Label ID="lbl_UpdatedOn" runat="server" Width="160px" style="text-align: left"></asp:Label></td>
               <td style="text-align: right; width: 88px;" valign="top">
                    <asp:Button ID="btn_save_ext" runat="server" Text="Save" CssClass="btn" Width="70px" OnClick="btn_Extend_Click" />
               </td>
       </tr>
       </table>
                                        </td>
                                        </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="3" width="100%" border="0" runat="server" id="tabEarly" visible="false" >
                                            <tr>
                                                <td style=" text-align:right;">Expected Relief Dt. : </td>
                                                <td style=" text-align:LEFT;">
                                                    <asp:TextBox ID="txtExpectedReliefDt" runat="server" CssClass="input_box" Width="82px" ValidationGroup="dd"></asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton3" Visible ="false" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtExpectedReliefDt" ></ajaxToolkit:CalendarExtender>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtExpectedReliefDt" ValidationGroup="dd" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtExpectedReliefDt" ValidationGroup="dd" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                                  
                                                </td>
                                                <td></td>
                                            </tr>
                                             <tr>
                                                <td align="right" style="padding-right: 5px; width: 153px; ">
                                                    Remarks:</td>
                                                <td align="left" colspan="5" rowspan="3">
                                                    <asp:TextBox ID="txtERRemarks" runat="server" CssClass="input_box" Height="48px" MaxLength="999" TabIndex="4" TextMode="MultiLine" Width="530px" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtERRemarks" ValidationGroup="dd" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                                                </td>
                                                 <td style=" text-align:right;"><asp:Button ID="btn_early" runat="server" Text="Save" CssClass="btn" Width="70px" OnClick="btn_EarlyRelief_Click" ValidationGroup="dd" CausesValidation="true" /></td>
                                            </tr>
                                        </table>
                                        <asp:Label runat="server" ID="lblmsg" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </div>
                                        </div>
                                </asp:View>
                                </asp:MultiView>
                                </asp:Panel>
                            </td>
                            </tr>
                     </table>
                </td>
                <td style="width: 25%;">
                     <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                 <asp:Image ID="img_Crew" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="108px" Width="100px" ImageUrl="" CausesValidation="False" />
                                 <asp:HiddenField ID="HiddenPK" runat="server" />
                            </td>  
                         </tr>
                         <tr>
                             <td colspan="2" style="text-align:center;">
                                 <legend style="text-align:center;"><strong>[<asp:Label ID="txt_MemberId" runat="server"></asp:Label>]</strong></legend>
                             </td>
                         </tr>
                         </table>
                     <table style="background-color:#f9f9f9;border:1px dashed;" cellpadding="0" cellspacing="0" width="100%" class="table table-bordered">
                         
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">    
                                <strong>Name :</strong> </td>
                             <td style="text-align:left;padding:5px;width:125px;">
                                 <asp:Label ID="lblName" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong>Rank :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblCurrRank" runat="server"></asp:Label>
                                 <%--<asp:TextBox ID="ddcurrentrank" runat="server"   ReadOnly="True" Visible="false"></asp:TextBox>--%>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong> Age : </strong></td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                  <strong>Last Vessel :</strong>
                                   </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblLastVessel" runat="server"></asp:Label>
                                <%-- <asp:TextBox ID="txt_LastVessel" runat="server" MaxLength="24" ReadOnly="True" Visible="false"></asp:TextBox>--%>
                                  </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                <strong> Rank Exp. : </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                <asp:Label ID="lblRankExp" runat="server"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Rating :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">

                                 <asp:Label ID="lblRating" runat="server"></asp:Label>

                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Status :  </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblStatus" runat="server"></asp:Label>
                             </td>
                         </tr>
                     </table> 
                     <br />
                     
                </td>
            </tr>
         </table>

        <div id="divfrm" runat="server" style="width:100%;">
            <iframe runat="server" id="frm" width="100%" height="850px" scrolling="no" frameborder="1"></iframe>    
        </div>

    </td>
    </tr>
    </table>
    </td>
    </tr>
   </table>
</div>
   </asp:Content>

