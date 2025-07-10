<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_VesselDockets.aspx.cs" Inherits="DD_OFC_VesselDockets" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../CSS/style.css" rel="stylesheet" type="text/css" />
<link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
<title>eMANAGER</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        
        <td style=" text-align :left; vertical-align : top;"> 
            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px">
            <tr>  
            <td>
             <div style="border:none; background-color : #FFB870; font-size :14px; padding:3px; text-align:center;">
                <b>Docket List - <asp:Label runat="server" ID="lblVesselName"></asp:Label></b>
             </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 25px ; text-align:center; background-color:#1589FF; color:White;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px;">
                                <colgroup>
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                </colgroup>
                                <tr>
                                    <td style="text-align:left;"><b>Docket# </b></td>
                                    <td style="text-align:center;"><b>Start Date</b></td>
                                    <td style="text-align:center;"><b>End Date</b></td>
                                    <td style="text-align:center;"><b>Status</b></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                            </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 411px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                    <col style="width:80px;" />
                                </colgroup>
                                <asp:Repeater ID="rptDocket" runat="server">
                                    <ItemTemplate>
                                            <tr>                                                
                                                <td style="text-align:left"><a target="_blank" href='#'><%#Eval("DocketNo")%></a></td>
                                                <td align="center"> <%#Common.ToDateString(Eval("StartDate"))%></td>
                                                <td align="center"> <%#Common.ToDateString(Eval("EndDate"))%></td>
                                                <td align="center"> <%#Eval("Status")%></td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                                <td style="text-align:left"><a target="_blank" href='#'><%#Eval("DocketNo")%></a></td>
                                                <td align="center"> <%#Common.ToDateString(Eval("StartDate"))%></td>
                                                <td align="center"> <%#Common.ToDateString(Eval("EndDate"))%></td>
                                                <td align="center"> <%#Eval("Status")%></td>
                                                <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
            </div>
            </td>
            </tr>
            </table>
        </td> 
        </tr>
        </table>
    </div>
     
    <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
