<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpJobPlanningDescr.aspx.cs" Inherits="PopUpJobPlanningDescr" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
</head>
<body onclick="document.getElementById('btnprnt').style.display='';" >
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" >   
                    Job Description&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td >
                            <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td ></td>
                        </tr>
                        <tr>
                          <td>
                              <table cellpadding="2" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;">
                               <tr>
                                  <td style="text-align:right; font-weight:bold;">Component :&nbsp;</td>
                                  <td style="width:750px;text-align:left"><asp:Label ID="lblComponent" runat="server"></asp:Label></td>
                               </tr>
                               <tr>
                                  <td style="font-weight:bold;" >Job :&nbsp;</td>
                                  <td style="text-align:left"><asp:Label ID="lblJob" runat="server"></asp:Label> </td>
                               </tr>
                               <tr>
                                   <td colspan="2" style="padding-bottom:5px;">
                                       <asp:Label ID="txtDescr" TextMode="MultiLine" runat="server" Height="230px" Width="550px"></asp:Label>
                                   </td>
                               </tr>
                              </table>
                          </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src ="../Images/PrintReport.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>   
                            </td>
                        </tr>
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
     </div>
    </form>
</body>
</html>
