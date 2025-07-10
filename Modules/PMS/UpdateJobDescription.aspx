<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateJobDescription.aspx.cs" Inherits="UpdateJobDescription" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
     function refresh()
     {
        window.opener.refreshgrid();
        window.close();
     }
    </script>
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
                <td align="center" class="text headerband" >
                    
                    Update Job Description&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>                       
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <table cellpadding="0" cellspacing="0" width="100%">
                               <tr>
                                    <td style="padding-bottom:5px;">
                                         <asp:Button ID="btnOfMDescr" CssClass="btn" Text="From Office Master" 
                                             runat="server" Width="150px" onclick="btnOfMDescr_Click" />
                                    </td>
                               </tr>
                               <tr>
                                   <td style="padding-bottom:5px;">
                                       <asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" Height="250px" Width="550px"></asp:TextBox>
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                      <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" 
                                           onclick="btnSave_Click" /> 
                                       <br /><br />
                                   </td>
                               </tr>
                               <tr>
                                   <td>
                                    <div style="width:100%;" ><uc1:MessageBox ID="MessageBox1" runat="server" /></div>
                                   </td>
                               </tr>
                              
                              </table>
                          </td>
                        </tr>
                        </table>                       
                        </ContentTemplate>
                        </asp:UpdatePanel> 
                        </div>
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            <div style=" margin:10px">
                    <table cellpadding="5" cellspacing="0" border="1" width="100%" style="border-collapse:collapse;" class="bordered" >
                        <tr class= "headerstylegrid">
                            <td style="width:30px; text-align:center">Sr#</td>
                            <td style="width:500px;">Attachments required to execute this job.</td>
                        </tr>
                    </table>
                    <table cellpadding="5" cellspacing="0" border="1" width="100%" style="border-collapse:collapse" class="bordered" >
                        <asp:Repeater ID="rptFiles" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("srno")%></td>
                                    <td style="width:500px;">
                                        <asp:Label runat="server" ID="lblDetails" Text='<%#Eval("AttachmentDetails")%>'></asp:Label>
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
        </td> 
        </tr>
        </table>
     </div>
    </form>
</body>
</html>
