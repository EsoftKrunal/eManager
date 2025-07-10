<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NTBRInActiveReport.aspx.cs" Inherits="CrewOffStatus" Title="Crew Off Status Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
          <script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
 function DoPost()
 {
 var s="NTBRInActiveReportContainer.aspx?Type=" + document.getElementById("ddl_Type").value + "&FromDate=" + document.getElementById("txt_from").value+ "&ToDate=" + document.getElementById("txt_to").value;
 document.getElementById("IFRAME1").setAttribute("src",s);
 return false;
 }
 
    </script> 
    <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:84px 0px 0px 118px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
        .style1
        {
            height: 13px;
            width: 113px;
        }
        .style2
        {
            height: 13px;
            width: 70px;
        }
        .style3
        {
            height: 13px;
            width: 223px;
        }
        .style4
        {
            height: 13px;
            width: 65px;
        }
        .style5
        {
            height: 13px;
            width: 242px;
        }
        .style6
        {
            height: 16px;
            width: 242px;
        }
        .style7
        {
            height: 13px;
            width: 117px;
        }
    </style>
 </head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
<div style="text-align: center">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Crew Off Status Report</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="border-bottom: #4371a5 1px solid; padding :3px">
                                         <table cellpadding="0" cellspacing="0" width="100%" >
                                                 <tr>
                                                     <td class="style1">
                                                         NTBR / InActive :</td>
                                                     <td style="text-align: left; " class="style7">
                                                         <asp:DropDownList ID="ddl_Type" runat="server" CssClass="input_box" 
                                                             Width="101px">
                                                         <asp:ListItem Text="NTBR" value="NTBR"></asp:ListItem>  
                                                         <asp:ListItem Text="In-Active" value="In-Active"></asp:ListItem>  
                                                         </asp:DropDownList></td>
                                                     <td style="width: 102px; height: 13px; text-align: right">
                                                         From Date :</td>
                                                     <td style="text-align: left">
                                                                 <asp:TextBox ID="txt_from" runat="server" CssClass="input_box" MaxLength="15"
                                                                     Width="100px" TabIndex="2"></asp:TextBox>
                                                                 <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="3" />
                                                                    </td>
                                                     <td style="text-align: left; width :150px;">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                                                                 </td>
                                                     <td style="text-align: right">
                                                                    To Date :</td>
                                                     <td style="text-align: left">
                                                                 <asp:TextBox ID="txt_to" runat="server" CssClass="input_box" MaxLength="15" Width="100px" TabIndex="4"></asp:TextBox>
                                                                 <asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="5" />
                                                                    </td>
                                                     <td style="height: 13px; padding-right: 5px;width :150px;" align="right">
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                 </td>
                                                     <td style="height: 13px; padding-right: 5px;" align="right">
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" OnClientClick="return DoPost();"/></td>
                                                 </tr>
                                             </table>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2">
                                        <iframe runat="server" id="IFRAME1" src=""  frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                                    </td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</td>
</tr>
</table>
</td> </tr> </table> 
        </div>
 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
     PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from">
 </ajaxToolkit:CalendarExtender>
  <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
     PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to">
 </ajaxToolkit:CalendarExtender>
 
    </form>
</body>
</html>

