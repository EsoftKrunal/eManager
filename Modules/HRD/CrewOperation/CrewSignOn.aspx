<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewSignOn.aspx.cs" Inherits="CrewOperation_CrewSignOn" Title="Crew Sign On" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title></title>
<link href="../styles/style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<script language="javascript" type="text/javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>

    <script language="javascript" type="text/javascript">
        function openpageManningAgent() {
            var url = "../Registers/ManningAgent.aspx"
            window.open(url, null, 'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
            return false;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 140px;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            width: 245px;
            height: 23px;
        }
        .style4
        {
            width: 88px;
            height: 23px;
        }
        .style5
        {
            width: 331px;
            height: 23px;
        }
        .style6
        {
            width: 331px;
        }
        .style7
        {
            height: 19px;
            width: 331px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table cellpadding="0" cellspacing="0" width="100%">
     <tr>
        <td style=" text-align:center" class="text headerband" >
        <strong>Crew Sign-On</strong>
        </td>
     </tr>
</table>
<div>
<asp:Label runat="server" id="lbl_signon_message" Text="" ForeColor="Red" ></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%"  border="1">
<tr>
<td style=" border:solid 1px gray;background-color : #e2e2e2; font-weight:bold;"  >
   <table cellpadding="3" cellspacing="0" style="width: 100%; padding-right: 5px; height: 50px;">
       <tr>
           <td style="text-align: right;width:15%;">
               Crew #<span lang="en-us"> </span>:</td>
           <td style="text-align: left; width: 25%; ">
               <asp:Label ID="lbl_EmpNo" runat="server" Width="160px"></asp:Label></td>
           <td style="text-align: right;width:15%; ">
               Crew Name<span lang="en-us"> </span>:</td>
           <td style="text-align: left; width: 25%; ">
               <asp:Label ID="lbl_Name" runat="server" Width="180px"></asp:Label></td>
           <td style="text-align: right; ">
               </td>
           <td style="text-align: left; ">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; ">
               Sign On Vessel<span lang="en-us"> </span>:</td>
           <td style="text-align: left; width: 149px; ">
               <asp:Label ID="lbl_Vessel" runat="server" Width="180px"></asp:Label>
           </td>
           <td style="text-align: right; ">
               <%--Pay Rank<span lang="en-us"> </span>:--%>

           </td>
           <td style="text-align: left; width: 192px; ">
               <asp:Label ID="lbl_Rank" runat="server" Width="180px" Visible="false"></asp:Label></td>
           <td style="">
           </td>
           <td style="">
               <asp:Label ID="lblportcallid" runat="server" Visible="False" Width="1px"></asp:Label>
           </td>
       </tr>
   </table>
   </td>
   </tr>
   <tr>
   <td>
   <table cellpadding="2" cellspacing="0" style="width: 100%; padding-right: 5px; height : 160px;">
       <tr >
           <td style="text-align: right;width:15% " >
               Sign On Rank <span lang="en-us"> </span> : </td>
           <td style="text-align: left; width:25%;" >
               <asp:Label ID="lblSignOnAs" runat="server" ></asp:Label>
                <asp:HiddenField ID="hfSignOnAs" runat="server" ></asp:HiddenField>
               <asp:DropDownList ID="ddl_SignOnas" runat="server" CssClass="required_box" Width="200px" Visible="false">
               </asp:DropDownList>
               <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddl_SignOnas"
                   ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>       
           </td>
           <td style="text-align: right;width:15% " >
               Sign On Date :</td>
           <td style="text-align: left;width:25%; " >
               <asp:TextBox ID="txt_SignOnDate" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" runat="server" CssClass="required_box" Width="80px"></asp:TextBox>
               <%--<asp:ImageButton
                   ID="imgsignonas" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />--%>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_SignOnDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_SignOnDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
           
               </td>
           <td style="text-align: right; width:20%;" >
               </td>
       </tr>
       <tr>
           <td style="text-align: right; " >
               Country :</td>
           <td style="text-align: left">
               <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box"
                   OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="200px">
               </asp:DropDownList></td>
           <td style="text-align: right; ">
               Port :</td>
           <td style="text-align: left; width: 245px;">
               <asp:DropDownList ID="ddl_Port" runat="server" CssClass="required_box" Width="163px">
               </asp:DropDownList>
               <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                   OnClientClick="return openpage();" /></td>
           <td style="text-align: right; width: 88px;">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; " >
               Duration (Months) :</td>
           <td style="text-align: left; height: 19px;">
               <asp:TextBox ID="txt_Duration" runat="server" CssClass="input_box" Width="82px" MaxLength="2" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                </td>
           <td style="text-align: right; height: 19px;">
               Relief Due :</td>
           <td style="text-align: left; height: 19px; width: 245px;">
               <asp:TextBox ID="txt_ReliefDate" runat="server" CssClass="input_box" Width="82px" ReadOnly="True"></asp:TextBox>
               <asp:ImageButton ID="imgSignOffDate" Visible ="false" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
               
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_ReliefDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_ReliefDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                                  
               </td>
           <td style="text-align: right; height: 19px; width: 88px;">
                                                                </td>
       </tr>
       <tr>
           <td style="text-align: right;padding-top:5px; " valign="top" >
               Remarks :</td>
           <td style="text-align: left;padding:5px;">
               <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" 
                   TextMode="MultiLine" MaxLength="99" Height="150px" Width="497px"></asp:TextBox></td>
           <td style="text-align: right; height: 19px; " valign="top" >
               Manning Agent :
               </td>
           <td style="text-align: left; height: 19px; width: 245px;" valign="top" >
              
                
               <asp:DropDownList ID="ddl_ManningAgent" runat="server" CssClass="required_box" Width="163px">
                   <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
               </asp:DropDownList>
                 <asp:ImageButton ID="imgAddManningAgent" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                   OnClientClick="return openpageManningAgent();" />
               <asp:CompareValidator ID="cvddl_ManningAgent" runat="server" ControlToValidate="ddl_ManningAgent"
                   ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                 <asp:HiddenField ID="hdnManningAgent" runat="server" ></asp:HiddenField>
           </td>
           <td style="text-align: right; height: 19px; width: 88px;">

           </td>
       </tr>
       <tr>
           <td style="text-align: right; " valign="top" >
               Updated By :</td>
                <td style="text-align: left" >
               <asp:Label ID="lbl_UpdatedBy" runat="server" Width="160px"></asp:Label>
               </td>
               <td style=" text-align :right " >Updated On :</td>
               <td style="text-align:left" >
               <asp:Label ID="lbl_UpdatedOn" runat="server" Width="160px" style="text-align: left"></asp:Label></td>
           <td style="text-align: right; width: 88px;" valign="top">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; " valign="top" >
               &nbsp;</td>
                <td style="text-align: center;padding:5px;" colspan="2">
                     <asp:Button ID="btn_save" runat="server" Text="Save" CssClass="btn" Width="70px" OnClick="btn_save_Click" /></td>
               <%--<td style=" text-align :right " >&nbsp;</td>--%>
               <td style="text-align:center" >
                   
           </td>
           <td style="text-align: right; width: 88px;" valign="top">
               &nbsp;</td>
       </tr>
       </table>
</td>
</tr>
</table>    
<asp:HiddenField ID="HiddenFamilyMember" runat="server" />
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txt_SignOnDate" PopupPosition="TopRight" TargetControlID="txt_SignOnDate"></ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgSignOffDate" PopupPosition="TopRight"  TargetControlID="txt_ReliefDate"> </ajaxToolkit:CalendarExtender>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderDuration" runat="server" FilterType="Numbers" TargetControlID="txt_Duration"> </ajaxToolkit:FilteredTextBoxExtender> 
<asp:Label ID="lblcrewid" runat="server" Visible="False" Width="118px" style="display:none"></asp:Label>
</div>
  </form>
</body>
</html>

    