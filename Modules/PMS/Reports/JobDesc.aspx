<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobDesc.aspx.cs" Inherits="JobDesc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
       <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body onclick="document.getElementById('btnprnt').style.display='';" >
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;" >
        <div style="font-weight:bold;text-align:center;" class="text headerband">
           Job Description
        </div>
        <table cellspacing="0" border="1" rules="all" cellpadding="4" style="width: 100%; border-collapse: collapse; font-size:14px;">
        <col width="150px;" />
        <col />
        <tr>
            <td colspan="2">
                <asp:Label ID="lblCompName" runat="server" style="font-weight:bold;" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Job Category</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblJobType" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Short Description </b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblShortDesc" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Department </b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblDepartment" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Primary Responsibility </b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblAssignedFor" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Assigned Ranks </b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblAssignedForOther" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b>Interval Type </b>
            </td>
            <td style="text-align: left;">
                <asp:Label ID="lblIntervalType" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <b> Long Description</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblLongDesc" runat="server" Width="300px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <img src ="../Images/PrintReport.jpg" id='btnprnt'  onclick="document.getElementById('btnprnt').style.display='none';window.print();" class="btn"/>   
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
