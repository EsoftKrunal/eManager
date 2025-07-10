<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePlannedTrainingPopUp.aspx.cs" Inherits="UpdatePlannedTrainingPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Training Entry</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                <span style="font-size:11px">
                    <asp:TextBox ID="txtTrId" runat="server" style="display:none"></asp:TextBox>
                    <asp:Literal runat="server" ID="litSummary"></asp:Literal> 
                </span>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div id="dvUP" runat="server">
                <hr />
                <span style="font-size:12px">| Update Training |</span> 
                <hr />
                <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                <table style="border-collapse:collapse" border="0" cellpadding="3" cellspacing="0">
                           <tr>
                               <td style="text-align:right">From Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="rf1" ControlToValidate="txt_FromDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                               <td style="text-align:right">To Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_ToDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txt_ToDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                           <td style="text-align:right">Institute :</td>
                               <td style="text-align: left"><asp:DropDownList ID="DropDownList1" runat="server" CssClass="required_box" TabIndex="2" Width="150px"></asp:DropDownList>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="DropDownList1" ErrorMessage="*"></asp:RequiredFieldValidator>
                               
                               </td>
                               <td style="text-align: left">&nbsp;</td>
                               <td><asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text=" Update " Width="60px" TabIndex="9" OnClick="btn_UpdateTraining_Click" /></td>
                           </tr>
                        </table>      
                <asp:HiddenField runat="server" ID="hfdPKId" /><asp:HiddenField runat="server" ID="hfdTId" /><asp:HiddenField runat="server" ID="hfdDD" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_FromDate">
                       </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton4" PopupPosition="TopLeft" TargetControlID="txt_ToDate">
                       </ajaxToolkit:CalendarExtender>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btn_Save_PlanTraining" />
                </Triggers>
                </asp:UpdatePanel>
                </div>
                </div>
        </center>
    </form>
</body>
</html>
