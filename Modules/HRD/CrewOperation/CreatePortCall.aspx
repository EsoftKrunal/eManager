<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CreatePortCall.aspx.cs" MasterPageFile="~/MasterPage.master"  Inherits="HRD_CrewOperation_CreatePortCall" Title="Create Port Call" %>
<%@ Register Src="CreatePortCall.ascx" TagName="CreatePortCall" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table cellpadding="0" cellspacing="0" width="100%">
     <tr>
        <td style="text-align:center" class="text headerband" >
        <strong>Create New Port-Call</strong>
        </td>
     </tr>
</table>
<div>
 <uc1:CreatePortCall ID="CreatePortCall1" runat="server" /></div>
     </asp:Content>

    