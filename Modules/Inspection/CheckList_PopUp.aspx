<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckList_PopUp.aspx.cs" Inherits="Transactions_CheckList_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            height: 13px;
        }
        .style2
        {
            text-align: center;
        }
        .style3
        {
            height: 13px;
            text-align: right;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="5" style=" height:23px" class="text headerband">
                    CheckList PopUp</td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="style2" colspan="3">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                    <span lang="en-us">&nbsp;</span></td>
                <td>
                </td>
            </tr>
            <tr runat="server" visible="true" >
                <td style="padding-right: 5px; text-align: right;">
                    Inspection Group :</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddl_InspGroup" runat="server" AutoPostBack="True" CssClass="input_box"
                        OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged" TabIndex="1" Width="203px">
                    </asp:DropDownList></td>
                <td style="padding-right: 5px; text-align: right;">
                    Inspection :</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlChapterName" runat="server" AutoPostBack="True" CssClass="input_box"
                        TabIndex="2" Width="203px">
                    </asp:DropDownList></td>
                <td style="padding-left: 10px; text-align: left">
                    &nbsp;</td>
            </tr>
            <tr runat="server" visible="true" >
                <td style="padding-right: 5px; text-align: right;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Required." ControlToValidate="ddl_InspGroup" ></asp:RequiredFieldValidator>
                </td>
                <td style="padding-right: 5px; text-align: right;">
                    &nbsp;</td>
                <td style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Required." ControlToValidate="ddlChapterName" ></asp:RequiredFieldValidator></td>
                <td style="padding-left: 10px; text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style3">
                    <span lang="en-us">Download Link :</span></td>
                <td style="text-align: left" class="style1">
                    <a runat="server" visible="false" id="aFile" href="">Download Link</a> 
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="btn_generate" runat="server" CssClass="btn" Text="Generate CheckList" OnClick="btn_generate_Click" Width="136px"/></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left" colspan="3">
                    <i>
                    <span lang="en-us" style="color :Gray"  >(Right click on link and select &quot;Save target as&quot; to download 
                    the file)</span>
                    </i>
                    </td>
                <td style="padding-left: 10px; text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <span lang="en-us">&nbsp;</span></td>
                <td>
                    <asp:LinkButton ID="lnk_CloseWindow" runat="server" OnClientClick="window.close();">Close Window</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
