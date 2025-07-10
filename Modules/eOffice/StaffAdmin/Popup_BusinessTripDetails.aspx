<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_BusinessTripDetails.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Popup_BusinessTripDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Travel Details</title>
    <style type="text/css">
    body
    {
    	font-family:Arial;
    	font-size:12px; 
    }
    </style> 
    
    <script type="text/javascript">
        window.focus();
        //window.moveTo(0, 0);
        //window.resizeTo(screen.availWidth, screen.availHeight);
    </script> 
</head>
<body onclick="document.getElementById('btnprnt').style.display='';">
    <form id="form1" runat="server">
       <div>                   
                            <center> <h3>Business Travel</h3></center>
                            <br />
                            <b >Employee <span lang="en-us">Details</span> : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                    <colgroup>
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:60px"/>
                                    <col />
                                    <col style="width:50px"/>
                                    <col />
                                    </colgroup>
                                   <tr>
                                       <td style="text-align :right">
                                           Emp Name :&nbsp;</td>
                                       <td style="text-align :left">
                                           <asp:Label ID="lblEmpName" runat="server" ></asp:Label>
                                       </td>
                                       <td style="text-align :right">
                                           Position :&nbsp;</td>
                                       <td style="text-align :left">
                                           <asp:Label ID="lblDesignation" runat="server" ></asp:Label>
                                       </td>
                                       <td style="text-align :right">
                                           Department :&nbsp;</td>
                                       <td style="text-align :left">
                                          <asp:Label ID="lblDepartment" runat="server"></asp:Label> 
                                       </td>
                                       <td style="text-align :right">
                                           Office :&nbsp;</td>
                                       <td>
                                            <asp:Label ID="lblOffice" runat="server"></asp:Label>  
                                       </td>
                                       <td style="text-align :right">
                                           Year :&nbsp;</td>
                                       <td style="text-align :left">
                                            <asp:Label ID="lblCurrentYear" runat="server"></asp:Label>  
                                       </td>
                                   </tr>
                            </table>
                            <br />                            
                            <div id="divLeaveDetails" runat="server">
                            <br />
                            <b >Business Travel Details : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                <colgroup>                                    
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col />                                    
                                    <tr>                                        
                                        <td align="center" style="font-weight:bold;">Leave From </td>
                                        <td align="center" style="font-weight:bold;">Leave To</td>
                                        <td align="center" style="font-weight:bold;">Duration</td>
                                        <td align="left" style="font-weight:bold;">Reason</td>                                        
                                    </tr>
                                </colgroup>
                            </table>      
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                            <colgroup>                                    
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col />                                    
                            </colgroup>
                            <asp:Repeater ID="RptLeaveRequest" runat="server">
                                <ItemTemplate>
                                    <tr style="font-size:11px;">                                       
                                        <td align="center">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="center">
                                            <%#Eval("Duration")%></td>
                                        <td align="left">
                                            <%#Eval("Reason")%></td>
                                        
                                    </tr>
                                </ItemTemplate>
                                
                            </asp:Repeater>
                            </table>
                            </div>   
    </div>
    <center>
    <br />
    <img src ="../../Images/Emtm/print.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>
    </center>
    </form>
</body>

</html>
