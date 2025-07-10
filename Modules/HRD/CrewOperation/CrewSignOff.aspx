<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewSignOff.aspx.cs" Inherits="CrewOperation_CrewSignOff" Title="Crew Sign Off" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title></title>
<link href="../styles/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
            <td style="text-align:center" class="text headerband" >
            <strong>Crew Sign-Off</strong>
            </td>
         </tr>
    </table>
    <div>
    <asp:Label ID="lb_msg" runat="server" ForeColor="Red"></asp:Label>
    <table cellspacing="0" width="100%" style=" background-color:#f9f9f9" cellpadding="0" border="0">
    <tr>
    <td style="text-align: center; background-color : #e2e2e2; font-weight:bold; ">
    <table cellpadding="0" cellspacing="0" style="width: 94%; height: 35px;">
<tr>
    <td style="WIDTH: 92px; HEIGHT: 2px" align=right>&nbsp;</td>
    <td style="WIDTH: 172px; HEIGHT: 2px; text-align :right " >Crew # : </td>
    <td style="WIDTH: 125px; TEXT-ALIGN: left">&nbsp;<asp:Label ID="lb_empno" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
    <td style="WIDTH: 130px; TEXT-ALIGN: right">Crew Name : </td>
    <td style="TEXT-ALIGN: left; width: 178px;">&nbsp;<asp:Label ID="lb_name" runat="server" meta:resourcekey="Label29Resource1" Width="80%"></asp:Label></td>
    <td style="width: 199px; text-align: right">Signed On :</td>
    <td style="width: 199px; text-align: left">&nbsp;<asp:Label ID="lb_signon" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
    <td style="width: 76px; text-align: left">&nbsp;</td>
</tr>
    <tr>
        <TD style="WIDTH: 92px; HEIGHT: 7px;text-align :right" >&nbsp;</td>
        <TD style="WIDTH: 172px; HEIGHT: 7px;text-align :right " >Rank : </td>
        <TD style="WIDTH: 125px; HEIGHT: 7px; TEXT-ALIGN: left">&nbsp;<asp:Label ID="lb_Rank" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
        <TD style="WIDTH: 130px; HEIGHT: 7px; TEXT-ALIGN: right">Vessel : </td>
        <TD style="HEIGHT: 7px; TEXT-ALIGN: left; width: 178px;">&nbsp;<asp:Label ID="lb_vessel" runat="server" meta:resourcekey="Label29Resource1" Width="80%"></asp:Label></td>
        <td style="width: 199px; height: 7px; text-align: right">Relief Due : </td>
        <td style="width: 199px; height: 7px; text-align: left">&nbsp;<asp:Label ID="lb_relief" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
        <td style="width: 76px; height: 7px; text-align: left">&nbsp;</td>
    </tr>
 </table>
    </td>
    </tr>
    <tr>
    <td style="text-align:center">
    <table style="TEXT-ALIGN: right; border-collapse:collapse; " cellSpacing="0" cellPadding="2" width="100%" border="1">
    <tr>
        <td style="text-align :right;"></td>
        <td style="text-align :right; width :150px;">Country:</td>
        <td style="text-align: left;">
            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="180px"></asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style="text-align: right;width :150px;">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right;">&nbsp;</td>
        <td style="text-align :right;">Port:</td>
        <td style="text-align: left;">
            <asp:DropDownList ID="dp_port" runat="server" CssClass="required_box" Width="156px"></asp:DropDownList>
            <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="return openpage();" />
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="dp_port" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style="text-align: right;">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right;">&nbsp;</td>
        <td style="text-align :right ">Signoff Reason:</td>
        <td style="text-align: left;">
            <asp:DropDownList ID="dp_signreason" runat="server" CssClass="required_box" Width="200px"></asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dp_signreason" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style=" text-align: right; ">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; ">&nbsp;</td>
        <td style="text-align :right ">Signoff Date:</td>
        <td style="text-align: left;">
            <asp:TextBox ID="txt_signoffdt" CausesValidation="false" runat="server" CssClass="required_box" Width="102px" AutoPostBack="true" OnTextChanged="txt_signoffdt_TextChanged" MaxLength="15"></asp:TextBox>
            <asp:ImageButton ID="img1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
        
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_signoffdt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_signoffdt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
        
        </td>
        <td style=" text-align: right;">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; ">&nbsp;</td>
        <td style="text-align :right ">Leave Period:</td>
        <td style="text-align: left;">
            <asp:Label ID="lb_lvstart" runat="server" ></asp:Label>-<asp:Label ID="lb_lvcomplition" runat="server" ></asp:Label></td>
        <td style=" text-align: right; ">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; ">&nbsp;</td>
        <td style="text-align :right ">Available From:</td>
        <td style="text-align: left;">
            <asp:TextBox ID="txt_DOA" runat="server" CausesValidation="True"
                CssClass="input_box" OnTextChanged="txt_signoffdt_TextChanged" Width="102px" MaxLength="15"></asp:TextBox>
            <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_DOA" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_DOA" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                     
        </td>
        <td style=" text-align: right; ">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; ">&nbsp;</td>
        <td style="text-align :right ">Reliever:</td>
        <td style="text-align: left;">&nbsp;<asp:Label ID="lb_reliever" runat="server" Width="200px"></asp:Label></td>
        <td style=" text-align: right; ">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; ">&nbsp;</td>
        <td style="text-align :right; vertical-align :top; ">Remarks:</td>
        <td style="text-align: left;"><asp:TextBox ID="txt_remark" runat="server" CssClass="input_box" Height="153px" TextMode="MultiLine" Width="600px" MaxLength="250"></asp:TextBox></td>
        <td style=" text-align: right; ">
            &nbsp;</td>
    </tr>
    <tr>
    <td colspan="5" style="text-align :center; padding:5px;">
    <asp:Button ID="btnvisasave" runat="server" CssClass="btn" OnClick="btnvisasave_Click" TabIndex="55" Text="Save" Width="80px" Enabled="False" />
    </td>
    </tr>
    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
        PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_DOA">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
        PopupButtonID="img1" PopupPosition="TopRight" TargetControlID="txt_signoffdt">
    </ajaxToolkit:CalendarExtender>
    </div>
    </form>
</body>
</html>
