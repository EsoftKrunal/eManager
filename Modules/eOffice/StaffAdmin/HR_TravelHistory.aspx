<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_TravelHistory.aspx.cs" Inherits="emtm_Emtm_TravelHistory" EnableEventValidation="false" %>


<%@ Register src="HR_TravelDocumentHeader.ascx" tagname="HR_TravelDocumentHeader" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel History</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
            
                <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="dottedscrollbox" style="padding-left:5px;">
                        <div style=" font-size :25px; width:300px;display :inline" >Travel Details :</div>
                        <asp:Label runat="server" ID="lblUserFullName"></asp:Label> 
                        </div>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        </asp:PlaceHolder>
                        <div class="dottedscrollbox">
                            <uc2:HR_TravelDocumentHeader ID="Emtm_HR_TravelDocumentHeader1" runat="server" />
                        </div> 
                        
                        <div style="padding:5px 5px 5px 5px;" >
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <col style="width:25px;" />
                               <col style="width:50px;"/>
                               <col />
                               <col style="width:100px;" />
                               <col style="width:100px;" />
                               <col style="width:120px;" />
                               <col style="width:100px;" />
                               <col style="width:17px;" />
                               <tr align="left" class="blueheader"> 
                               <td></td>                 
                               <td>Sr.#</td>
                               <td>Travel Purpose</td>
                               <td>Days</td>
                               <td>From Date</td>
                               <td>To Date</td>
                               <td>EXP.RPRT</td>
                               <td></td>
                               </tr>
                            </table>      
                            
                        <div  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 200px ; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                           <col style="width:25px;" />
                           <col style="width:50px;"/>
                           <col />
                           <col style="width:100px;" />
                           <col style="width:100px;" />
                           <col style="width:120px;" />
                           <col style="width:100px;" />
                           <col style="width:17px;" />
                            </table>
                        <asp:Repeater ID="rptRunningHour" runat="server">
                               <ItemTemplate>
                                  <tr class='<%# (Common.CastAsInt32(Eval("srno"))==SelectedId)?"selectedrow":"row"%>'>
                                       <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("srno") %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"/></td>
                                       <td align="left"><%#Eval("srno")%></td>                          
                                       <td align="left"><%#Eval("travelpurpose")%></td>
                                       <td align="center"><%#Eval("travellingdays")%></td>
                                       <td align="center"><%#Eval("fromdate")%></td>
                                       <td align="center"><%#Eval("todate")%></td>
                                       <td align="center"><%#Eval("expert")%></td>
                                       <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                   </tr>
                               </ItemTemplate>
                               <AlternatingItemTemplate>
                                   <tr class='<%#(Common.CastAsInt32(Eval("srno"))==SelectedId)?"selectedrow":"alternaterow"%>'>
                                       <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("srno") %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"/></td>
                                       <td align="left"><%#Eval("srno")%></td>                          
                                       <td align="left"><%#Eval("travelpurpose")%></td>
                                       <td align="center"><%#Eval("travellingdays")%></td>
                                       <td align="center"><%#Eval("fromdate")%></td>
                                       <td align="center"><%#Eval("todate")%></td>
                                       <td align="center"><%#Eval("expert")%></td>
                                       <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                       </tr>
                                </AlternatingItemTemplate>
                              </asp:Repeater>
                          </table>
                        </div> 
                        <br /> 
                           <table id="tblview" runat="server" width="100%" cellpadding="2" cellspacing ="0" border="0">
                            <col width="120px" style="text-align :left" align="left">
                            <col />
                            <col width="120px" style="text-align :left" align="left">
                            <col />
                            <tr>
                                <td>
                                    Travel Purpose :</td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" Text="Business Purpose"></asp:TextBox>
                                </td>
                                <td>
                                    Travelling Days : </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Text="15 Days"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    From Date :</td>
                                <td>    
                                    <asp:TextBox ID="txtfromdate" runat="server" Text="11-Sep-2011"></asp:TextBox>
                                    <asp:ImageButton ID="imgfromdate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                                OnClientClick="return false;" />
                                </td>
                                <td>
                                    To Date :</td>
                                <td>
                                    <asp:TextBox ID="txttodate" runat="server" Text="26-Sep-2011"></asp:TextBox>
                                    <asp:ImageButton ID="imgtodate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                                OnClientClick="return false;" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    EXP.RPRT : </td> 
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" Text="Yes"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfromdate" TargetControlID="txtfromdate"></ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgtodate" TargetControlID="txttodate"></ajaxToolkit:CalendarExtender>
                                    </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td align="right">
                                    <asp:Button ID="Button3" CssClass="btn"  runat="server" Text="Save"></asp:Button>
                                    <asp:Button ID="Button4" CssClass="btn"  runat="server" Text="Delete"></asp:Button>
                                    <asp:Button ID="Button5" CssClass="btn"  runat="server" Text="Cancel"></asp:Button>
                                </td>
                            </tr>
                            </table>
                        </div> 
                    </td>
                </tr>
         </table>  
         
    </div>
    </form>
</body>
</html>
