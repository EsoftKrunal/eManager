<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VSL_JobDesc.aspx.cs" Inherits="VSL_JobDesc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
</head>
<body onclick="document.getElementById('btnprnt').style.display='';" >
    <form id="form1" runat="server">
    <div >
        <div style="font-weight:bold;text-align:center;">
            <h3> Job Description</h3>
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
            <td style="text-align: left">
                <b> Estimated Job Cost</b>
            </td>
            <td style="text-align: left">
                <asp:Label ID="lblJobCost" runat="server" Width="300px" ></asp:Label>
            </td>
        </tr>
       
    </table>
        <div style="font-weight:bold;text-align:center;">
            <h3> Spare Requirement </h3>
        </div>
        <table width="100%" cellpadding="3" cellspacing="0" border="1">
            <tr style="font-weight:bold;">
                <td>Spare Details</td>
                <td style="text-align:center;width:100px;">Qty</td>
            </tr>
            <asp:Repeater runat="server" ID="rptSpares">
                <ItemTemplate>
                <tr>
                    <td><%# Eval("sparename")%></td>
                    <td style="text-align:right;"><%#Eval("Qty")%></td>
                </tr>
                    </ItemTemplate>
            </asp:Repeater>
        </table>
          <div style="font-weight:bold;text-align:center;">
            <h3>Attachments required to execute this job</h3>
        </div>
   

          <div>
                    <table cellpadding="5" cellspacing="0" border="1" width="100%" style="border-collapse:collapse" class="bordered" >
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("srno")%></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDetails" Text='<%#Eval("AttachmentDetails")%>'></asp:Label>
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
         <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
            <td  align="center">
                <img src ="../Images/PrintReport.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>   
            </td>
        </tr>
    </table>
    </div>

    </form>
</body>
</html>
