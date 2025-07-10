<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Activity.aspx.cs" Inherits="MRV_Activity" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
    <link href="style.css?14" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../CSS/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../../CSS/StyleSheet.css" />
	 <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <%--<div class="pagename">
                MRV - Monitoring , Reporting & Verfication
            </div>--%>
          
            <div style="border-bottom:solid 5px #4371a5;"></div>
            
            <h3>
                                           <span style="margin-left:10px;">Activity</span>
                           
                                       </h3>
                                      <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                                         <tr>                                            
                                            <th style="text-align:left;width:120px;">ID</th>
                                            <th style="text-align:left;">Activity Name</th>                                            
                                        </tr>
                                       <asp:Repeater runat="server" ID="rptActivity">
                                            <ItemTemplate>
                                                <tr>                                                    
                                                    <td style="text-align:left"><%#Eval("ActivityId")%></td>
                                                    <td style="text-align:left"><%#Eval("ActivityName")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                       </table>
        </div>
    
    <div class="message">
        <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                
    </div>
    </form>
</body>
</html>
                                        
