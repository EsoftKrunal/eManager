<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewVendor.aspx.cs" Inherits="AddNewVendor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Vendor</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ReloadParentPage()
        {
            window.opener.PageReLoad();
        }
    </script>
            

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
            <td style="vertical-align: top; text-align: left;">
            <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
            <td class="header" style=" padding:4px;" >Add Vendor
            <%--<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " />  --%>
            </td>
            </tr>
            </table>
            </td> 
            </tr> 
        </table> 
        <br />
        <table style="width :1000px;margin:auto; border:solid 1px #4371a5;" cellpadding="2" cellspacing="2" border="1" rules="all">
            <col width="170px" />
            <col width="300px" />
            <col width="170px" />
            <col />
            
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Vendor
                </td>
                <td>
                    <asp:TextBox ID="txtVendor" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="extVendor" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPortTitles" ServicePath="~/WebService1.asmx" TargetControlID="txtVendor"></asp:AutoCompleteExtender>
                </td>
                <td>
                    Traverse Vendor
                </td>
                <td>
                    <asp:DropDownList ID="ddlTravVendor" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_ddlTravVendor" AutoPostBack="true" Width="250px"></asp:DropDownList>
                    <asp:Label ID="lblTravVenID" runat="server" style="font-weight:bold;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Muilti Curr
                </td>
                <td>
                    <asp:DropDownList ID="ddlMultiCurr" runat="server" Width="100px" >
                        <asp:ListItem Value="" Text="< Select >"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                    
                    
                </td>
                <td>
                    Port
                </td>
                <td>
                    <asp:TextBox ID="txtPort" runat="server" Width="150px" MaxLength="15"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="extPort" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetPort" ServicePath="~/WebService1.asmx" TargetControlID="txtPort"></asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Telephone
                </td>
                <td>
                    <asp:TextBox ID="txtTelephone" runat="server" Width="120px" MaxLength="25"></asp:TextBox>
                </td>
                <td>
                    Fax
                </td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Contact 
                </td>
                <td>
                    <asp:TextBox ID="txtContact" runat="server" Width="150px" MaxLength="25"></asp:TextBox>
                </td>
                <td>
                    Email Address
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Active</td>
                <td>
                     <asp:DropDownList ID="ddlActive" runat="server" Width="100px">
                        <asp:ListItem Value="" Text="< Select >"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Preferred
                </td>
                <td>
                    <asp:DropDownList ID="ddlPreferred" runat="server" Width="100px">
                        <asp:ListItem Value="" Text="< Select >"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Approval Type
                </td>
                <td>
                    <asp:TextBox ID="txtApprovalType" runat="server" Width="150px" MaxLength="155"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServiceMethod="GetApprovalType" ServicePath="~/WebService1.asmx" TargetControlID="txtApprovalType"></asp:AutoCompleteExtender>
                </td>
                <td>
                    Service
                </td>
                <td>    
                    <asp:TextBox ID="txtService" runat="server" Width="30px" MaxLength="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblmsg" runat="server" CssClass="error"></asp:Label>
                </td>
                <td >
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="OnClick_btnSave"  Text=" Save " ImageUrl="~/Images/save.jpg"/>
                    <%--<asp:ImageButton ID="btnCancel" runat="server" OnClick="OnClick_Cancel"  Text=" Cancel " ImageUrl="~/Images/cancel.jpg"/>--%>
                    <img src="Images/cancel.jpg"onclick="window.close()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
