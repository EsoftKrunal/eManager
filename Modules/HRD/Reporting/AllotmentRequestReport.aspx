<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllotmentRequestReport.aspx.cs" Inherits="Reporting_AllotmentRequestReport" MasterPageFile="~/MasterPage.master" Title="Allotment Request Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<style type="text/css" >
.fixedbar
{
position:fixed;
margin:80px 0px 0px 120px;   
background-color:#f0f0f0;  
z-index:100;
border:solid 1px #5c5c5c;
}
</style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
<tr>
    <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5"> Allotment Request</td>
</tr>
<tr>
    <td style="width: 100%">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
            </tr>
                        <tr>
                            <td style=" padding:3px;" >
                                <TABLE cellSpacing=0 cellPadding=0 width="100%">
                                    <TBODY>
                                        <TR>
                                            <TD style="HEIGHT: 18px; width: 105px; text-align: right;">Vessel :</TD>
                                            <TD align="left" style="height: 18px"><asp:DropDownList id="ddl_Vessel" runat="server" CssClass="required_box" Width="200px" TabIndex="1"></asp:DropDownList></TD>
                                            <TD align="right" style="height: 18px; text-align: left;">
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel"
                                                ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></TD>
                                            <TD align="right" style="height: 18px">Month:</TD>
                                            <td align="left" style="height: 18px">
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="2"
                                                    Width="111px">
                                                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
    <td align="right" style="height: 18px; text-align: left;">
                              <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                  ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
    <td align="right" style="height: 18px">
        Year:</td>
    <td align="left" style="height: 18px">
        <asp:DropDownList ID="ddl_Year" runat="server" CssClass="input_box" TabIndex="3"
            Width="81px">
        </asp:DropDownList></td>
    <td align="left" style="height: 18px">
        <asp:Button id="Button1" runat="server" CssClass="btn" Text="Show Report" OnClick="Button1_Click" TabIndex="4"></asp:Button>&nbsp;&nbsp; </td>
                                                            </TR>
</TBODY>
                            </TABLE>
    </td>
</tr>
<tr>
    <td style="text-align: left">
   <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
    </td>
</tr>
</table>
    </td>
</tr>
</table>
</td>
</tr>
</table>
</td></tr></table> 
</div>
</asp:Content>
