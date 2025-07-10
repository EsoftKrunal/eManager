<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpJobPlanningDescr.aspx.cs" Inherits="PopUpJobPlanningDescr" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
        <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >                     
                    Job Description&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>                       
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <table cellpadding="0" cellspacing="0" border="0" width="100%">
                               <tr>
                                  <td style=" text-align:right">Component :&nbsp;</td>
                                  <td style="width:750px;text-align:left">
                                      <asp:Label ID="lblComponent" Font-Bold="true" runat="server"></asp:Label>
                                       <asp:Button ID="btnPring" runat="server" CssClass="btnorange" 
                                              onclick="btnPring_Click" style="width:80px; float:right;" 
                                              Text="Print" />
                                  </td>
                               </tr>
                               <tr>
                                  <td style="text-align:right">Job :&nbsp;</td>
                                  <td style="text-align:left"><asp:Label ID="lblJob" Font-Bold="true" runat="server"></asp:Label> 
                                  
                                  </td>
                               </tr>
                               <tr>
                                  <td colspan="2" style="height:3px;">&nbsp;</td>
                               </tr>
                               <tr>
                                   <td colspan="2" style="padding-bottom:5px;">
                                       <asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" Height="220px" Width="550px"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                    <td colspan="2" style="padding-bottom:3px;" >
                                     <b>Attachments</b>
                                    </td>
                               </tr>
                               <tr>
                                    <td colspan="2">
                                    <table cellpadding="2" cellspacing="" border="1" width="100%" style=" background-color:#c2c2c2; border-collapse:collapse" >
                                    <tr class= "headerstylegrid">
                                        <td style="width:30px; text-align:center">Sr#</td>
                                        <td style="width:300px;">Description</td>
                                        <td style="width:150px;">File Name</td>
                                        <td style="width:12px;">&nbsp;</td>
                                    </tr>
                                    </table>
                                    <div style="width:100%;height:150px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                                    <table cellpadding="2" cellspacing="" border="1" width="100%" style="border-collapse:collapse" >
                                        <asp:Repeater ID="rptFiles" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                                    <td style="text-align:left;width:300px;" >&nbsp;<%#Eval("Descr")%></td>
                                                    <td style="width:150px;">
                                                        <a href='UploadFiles/<%#((Eval("VesselCode").ToString().Trim()=="")?"AttachmentForm/":VesselCode+"/")+ Eval("UpFileName").ToString()%>' target="_blank" ><%#Eval("UpFileName")%> </a>
                                                    </td> 
                                                     <td style="width:12px;">&nbsp;</td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    </div>
                                    </td>
                               </tr>
                              </table>
                          </td>
                        </tr>
                        </table>                       
                        </ContentTemplate>
                        </asp:UpdatePanel> 
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
