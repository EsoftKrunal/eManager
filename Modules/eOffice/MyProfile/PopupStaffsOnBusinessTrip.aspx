<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStaffsOnBusinessTrip.aspx.cs" Inherits="emtm_MyProfile_Emtm_PopupStaffsOnBusinessTrip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staffs on Business Trip</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function CloseWindow() {
            window.close();
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <table width="100%">
            <tr>
            <td valign="top" style="border:solid 1px #4371a5; height:100px;">
                <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                     Staffs on Business Trip
                </div>
                <asp:UpdatePanel ID="UpdatePanel2"  runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div id="div1" runat="server">
                                       <%-- <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col />
                                                <col style="width:90px;" />
                                                <col style="width:40px;" />
                                                <col style="width:17px;"/>
                                                
                                                <tr align="left" class="blueheader">
                                                    <td>
                                                    <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                                    <tr>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlOffice" runat="server" Width="90px" AutoPostBack="true" onselectedindexchanged="ddlOffice_SelectedIndexChanged" onchange="WhoIsOff_Search();"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlDept" runat="server" Width="120px" AutoPostBack="true" onselectedindexchanged="UpdateOffGrid"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                            <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td style="text-align :left">
                                                             <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                             <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" TargetControlID="txtLeaveFrom"></ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" TargetControlID="txtLeaveTo"></ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>--%> 
                                        
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:170px;" />
                                                <col style="width:150px;" />
                                                <col />
                                                <col style="width:80px;" />
                                                <col style="width:80px;" />
                                                <col style="width:17px;"/>
                                            </colgroup>
                                            <tr class="blueheader">
                                              <td>Employee Name</td>
                                              <td>Position</td>
                                              <td>Purpose of office absence</td>
                                              <td>From Dt.</td>
                                              <td>To Dt.</td>
                                              <td></td>
                                            </tr> 
                                            </table>       
                                        <div id="div2" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 600px; text-align:center;">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:170px;" />
                                                <col style="width:150px;" />
                                                <col />
                                                <col style="width:80px;" />
                                                <col style="width:80px;" />
                                                <col style="width:17px;"/>
                                            </colgroup>                                            
                                            <asp:Repeater ID="rptWhoIsOff" runat="server">
                                                <ItemTemplate>
                                                    <tr class='row'>
                                                        <td align="left">
                                                            <%#Eval("NAME")%></td>
                                                        <td align="left">
                                                            <%#Eval("PositionName")%></td> 
                                                        <td align="left">
                                                            <%#Eval("Reason")%></td> 
                                                        <td align="left">
                                                            <%#Eval("LeaveFrom")%></td>                                                                    
                                                        <td align="center">
                                                            <%#Eval("LeaveTo")%>
                                                        </td>   
                                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>   
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>                                            
                                         </table>
                                         </div>
                                        
                                    <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="CloseWindow();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </ContentTemplate> 
                                </asp:UpdatePanel>
                    
                            
               
            </td>
        </tr> 
      </table>  
    </div>

    </form>
</body>
</html>
