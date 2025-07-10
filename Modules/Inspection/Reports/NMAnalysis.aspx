<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NMAnalysis.aspx.cs" Inherits="NMAnalysis" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body style="font-family:Arial; font-size:13px;" >
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <div style="border:solid 1px black;">
            <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                <tr>
                    <td>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%" style=" background-color:#D6D9E3">
                    <tr>
                    <td style="text-align:center; background-color:#334375; color:White; padding:5px; font-size:14px;" colspan="5"><b>NM Analysis Report</b></td>
                    </tr>
                    <tr>
                    <td style="text-align:center">Vessel </td>
                    <td style="text-align:center">Period </td>
                    <td style="text-align:center">&nbsp;</td>
                    <td style="text-align:center">
                        NM Category </td>
                    <td style="text-align:center">
                        NM Type </td>
                    </tr>
                    <tr>
                    <td style="text-align:center"><asp:DropDownList runat="server" ID="ddlVessel"></asp:DropDownList> </td>
                    <td style="text-align:center">
                    <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
                    <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> &nbsp;&nbsp;
                    -
                    <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox>
                    <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton>
                    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td style="text-align:center">&nbsp;</td>
                    <td style="text-align:center">
                    <asp:DropDownList ID="ddlAcccategory" runat="server" >
                        <asp:ListItem Value="" Text=" < All >"></asp:ListItem>
                        <asp:ListItem Value="Injury" Text="Injury"></asp:ListItem>
                        <asp:ListItem Value="Pollution" Text="Pollution"></asp:ListItem>
                        <asp:ListItem Value="Property Damage" Text="Property Damage"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="text-align:center">
                    <asp:DropDownList ID="ddlNMType" runat="server" >
                        <asp:ListItem Value="" Text=" < All >"></asp:ListItem>
                        <asp:ListItem Value="Minor" Text="Minor"></asp:ListItem>
                        <asp:ListItem Value="Significant" Text="Significant"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:center" colspan="5">
                    <asp:Button runat="server" ID="btnDownloadExcel" Text="Download Excel" 
                            onclick="btnDownloadExcel_Click" />
                    </td>
                    </tr>
                    <tr>
                    <td style="text-align:center" colspan="5"><asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label> </td>
                    </tr>
                    <tr>
                    <td style="text-align:center">&nbsp;</td>
                    <td style="text-align:center">
                        &nbsp;</td>
                    <td style="text-align:center">&nbsp;</td>
                    <td style="text-align:center">
                        &nbsp;</td>
                    <td style="text-align:center">
                        &nbsp;</td>
                    </tr>
                    </table>
                    </td>
                </tr>
                </table>
            </div>
    </div>
    </form>
</body>
</html>
