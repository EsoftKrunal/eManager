<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselMenu.ascx.cs" Inherits="VesselMenu" %>
 <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<div style="text-align : center;width:180px; vertical-align : top; background-color :#e2e2e2; padding-bottom :2px 0px 15px 2px; border :solid 1px #4371a5;font-family:Arial;font-size:12px;">
<table cellpadding="1" cellspacing ="0" width="100%" >
<tr>
    <td style=" text-align :center"><asp:DropDownList ID="ddlVesselStatus" runat="server" CssClass="input_box" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="DDlIndexChanged"></asp:DropDownList></td>
</tr>
<tr>
    <td style=" text-align :center "><asp:DropDownList ID="ddlOwner" runat="server" CssClass="input_box" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="DDlIndexChanged"></asp:DropDownList> </td>
</tr>
<tr>
    <td style=" text-align :center "><asp:DropDownList ID="ddlMgmtType" runat="server" CssClass="input_box" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="DDlIndexChanged"></asp:DropDownList> </td>
</tr>
</table>
</div>
<div style="min-height:200px;height:375px; width :180px;overflow-x:hidden; overflow-y:scroll;border :solid 1px #4371a5;font-family:Arial;font-size:12px;" >
<table style="width:177px; text-align : center " cellpadding="2" cellspacing="0" border="0">
<asp:Repeater runat="server" ID="rptVessel">
        
<ItemTemplate >
<tr>
    <td style =" text-align: center" >
        <asp:ImageButton runat="server" ID="imgEdit" style='<%# "display:" + Eval("IsEdit")%>'  OnClick="VesselEdit" ForeColor="DarkGreen" CausesValidation="false" ToolTip='<%#Eval("VesselFullName") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("VesselId") %>' Font-Size="14px"> </asp:ImageButton> 
    </td>
    <td style =" text-align: left">
        <asp:LinkButton runat="server" Id="lnkVessel" OnClick="VesselView" CausesValidation="false" CommandArgument='<%#Eval("VesselId") %>' Text='<%#Eval("VesselFullName") %>' Font-Size="14px" ForeColor="DarkGreen"></asp:LinkButton> 
    </td>
</tr>
</ItemTemplate>
</asp:Repeater> 
</table>
</div>
<div style=" height:30px;width :180px; text-align: center; vertical-align: middle; padding-top :3px;border :solid 1px #4371a5; " >
    <asp:Button ID="btn_Add_Vessel" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Add_Click" Text="Add Vessel" Width="80px" /> &nbsp;&nbsp;
    <asp:Button ID="btn_Rename_Vessel" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Rename_Click" Text="Rename " Width="80px" />
<br />
</div>
