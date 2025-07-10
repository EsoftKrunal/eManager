<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_Search.aspx.cs" Inherits="emtm_Inventory_Emtm_Inv_Search" %>
<%@ Register src="~/Modules/eOffice/Inventory/Emtm_Inv_HeaderMenu.ascx"  tagname="Emtm_Inv_HeaderMenu" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
            function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 46)) {
                 event.returnValue = false;
             }
            }
            function openitemwin(ItemID)
              {
                 window.open('Emtm_Inv_ITHardwareEntry.aspx?Mode=View&&ITEMId='+ ItemID,'','');
              }
            function openSoftwin(ItemID)
              {
                 window.open('Emtm_Inv_SoftwareEntry.aspx?Mode=View&&ITEMId='+ ItemID,'','');
              }
            function openAmcwin(ItemID)
              {
                 window.open('Emtm_Inv_AMCEntry.aspx?Mode=View&&ITEMId='+ ItemID,'','');
              }  
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
          <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                Inventory Management                                  
                            </div>
                             <div>
                                <uc2:Emtm_Inv_HeaderMenu ID="UCHeader" runat="server" />
                             </div>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <table cellpadding="5" cellspacing="0" width="100%" >
                                    <tr>
                                        <td colspan="5" class="dottedscrollbox" 
                                            style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                            Search Items
                                        </td>
                                    </tr>
                                     <tr>
                                       <td style="height:10px; text-align:center" colspan="5">
                                       <asp:Label ID="lblMsg" style="font-size:12px; color:Red;" runat="server"></asp:Label>
                                       </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            Main Category :&nbsp;
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlMainCat" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCat_SelectedIndexChanged" runat="server" Width="225px" required="yes"></asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            Mid Category :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlMidCat" runat="server" required="yes" Width="225px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" Text=" GO " 
                                                Width="80px" />
                                        </td>
                                    </tr>
                                    <tr>
                                       <td colspan="5">
                                       <asp:Panel ID="pnlITHardware" runat="server" Visible="false" >
                                         
                                         <asp:UpdatePanel ID="upITHardware" runat="server">
                                         <ContentTemplate>
                                           <table cellpadding="2" cellspacing="0" width="100%" >
                                            <tr>
                                               <td style="text-align: right">Asset Code :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                                  <asp:TextBox ID="txtAssetCode" runat="server"></asp:TextBox>
                                               </td>
                                               
                                               <td style="text-align: right">Maker :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtMaker" runat="server"></asp:TextBox>
                                               </td>
                                               
                                               <td style="text-align: right">Model# :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                                               </td>
                                               </tr>
                                               
                                               <tr>
                                               <td style="text-align: right"><%--Purchase Dt :&nbsp;--%>
                                               </td>
                                               <td style="text-align: left">
                                               <%--<asp:TextBox ID="txtPurchaseDt" ReadOnly="true" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImageButton1" TargetControlID="txtPurchaseDt" PopupPosition="BottomRight">
                                                </ajaxToolkit:CalendarExtender>--%>
                                               </td>
                                               
                                               <td colspan="4" style="text-align: right; padding-right:10px;">
                                                <asp:Button ID="btnITHSearch" runat="server" Text="Search" Width="80px" onclick="btnITHSearch_Click" />
                                               </td>
                                               
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="vertical-align: top; padding-top:2px;">
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table cellpadding="2" cellspacing="1" width="100%" rules="all" height="26px">
                                                <colgroup>
                                                        <col width="30px" />
                                                        <col width="150px" />
                                                        <col />
                                                        <col width="100px" />
                                                        <col width="100px" />
                                                        <%--<col width="90px" />
                                                        <col width="100px" />
                                                        <col width="100px" />--%>
                                                    <col width="25px" />
                                                    </colgroup>
                                                    <tr class="blueheader">
                                                        <td>
                                                            
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Asset Code
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Maker
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Model#
                                                        </td>
                                                        <%--<td style="text-align: left">
                                                            Sr#
                                                        </td>
                                                        <td style="text-align: left">
                                                            Purchase Date
                                                        </td>
                                                        <td style="text-align: left">
                                                            Price
                                                        </td>--%>
                                                        <td style="text-align: left">
                                                          &nbsp;Amount (US$)
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                            </div>
                                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                height: 300px; text-align: center;">
                                                <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                    <colgroup>
                                                        <col width="30px" />
                                                        <col width="150px" />
                                                        <col />
                                                        <col width="100px" />
                                                        <col width="100px" />
                                                        <%--<col width="90px" />
                                                        <col width="100px" />
                                                        <col width="100px" />--%>
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptITHardware" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgViewITH" ToolTip="View Details" runat="server" CommandArgument='<%# Eval("ItemId").ToString() %>'
                                                                        ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="imgViewITH_OnClick" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("AssetCode")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("Maker")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("ModelNumber")%>
                                                                </td>
                                                                <%--<td align="left">
                                                                    <%#Eval("SRNumber")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("PurchaseDate")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("Price")%>
                                                                </td>--%>
                                                                <td align="right">
                                                                    <%#Eval("Amount")%>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                                
                                                </td>
                                            
                                            </tr>
                                                 
                                           </table>
                                           </ContentTemplate>
                                           <Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="btnITHSearch" />
                                           </Triggers>
                                         </asp:UpdatePanel>
                                         
                                       </asp:Panel> 
                                       
                                       <asp:Panel ID="pnlSoftware" runat="server" Visible="false" >
                                         
                                         <asp:UpdatePanel ID="upSoftware" runat="server">
                                         <ContentTemplate>
                                           <table cellpadding="2" cellspacing="0" width="100%" >
                                            <tr>
                                               <td style="text-align: right">Description :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                                  <asp:TextBox ID="txtSoftDescr" runat="server"></asp:TextBox>
                                               </td>
                                               
                                               <td style="text-align: right">Authrization# :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtSoftAuthNo" runat="server"></asp:TextBox>
                                               </td>
                                               
                                               <td style="text-align: right">Lic. Qty :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtSoftLicqty" onkeypress="fncInputNumericValuesOnly(this)" runat="server"></asp:TextBox>
                                               </td>
                                               </tr>
                                               
                                               <tr>
                                               <td style="text-align: right"><%--Purchase Dt :&nbsp;--%>
                                               </td>
                                               <td style="text-align: left">
                                               <%--<asp:TextBox ID="txtPurchaseDt" ReadOnly="true" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImageButton1" TargetControlID="txtPurchaseDt" PopupPosition="BottomRight">
                                                </ajaxToolkit:CalendarExtender>--%>
                                               </td>
                                               
                                               <td colspan="4" style="text-align: right; padding-right:10px;">
                                                <asp:Button ID="btnSoftSearch" OnClick="btnSoftSearch_Click" runat="server" Text="Search" Width="80px" />
                                               </td>
                                               
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="vertical-align: top; padding-top:2px;">
                                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                  <table cellpadding="2" cellspacing="1" width="100%" rules="all" height="26px">
                                                <colgroup>
                                                        <col width="30px" />
                                                        <col />
                                                        <col width="80px" />
                                                        <col width="170px" />
                                                        <col width="80px" />
                                                        <col width="120px" />
                                                        <%--<col width="100px" />
                                                        <col width="100px" />--%>
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <tr class="blueheader">
                                                        <td>
                                                        </td>
                                                        
                                                        <td style="text-align: left">
                                                            &nbsp;Description
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Sr#
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Authrization#
                                                        </td>
                                                        <td style="text-align: left">
                                                            Licence Qty
                                                        </td>
                                                        <%--<td style="text-align: left">
                                                            Purchase Date
                                                        </td>
                                                        <td style="text-align: left">
                                                            Price
                                                        </td>--%>
                                                        <td style="text-align: left">
                                                          &nbsp;Amount (US$)
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                            </div>
                                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                height: 300px; text-align: center;">
                                                <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                    <colgroup>
                                                        <col width="30px" />
                                                        <col />
                                                        <col width="80px" />
                                                        <col width="170px" />
                                                        <col width="80px" />
                                                        <col width="120px" />
                                                        <%--<col width="100px" />
                                                        <col width="100px" />--%>
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptSoftware" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgViewSoft" OnClick="imgViewSoft_Click" ToolTip="View Details" runat="server" CommandArgument='<%# Eval("ItemId").ToString() %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif"  />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("SOFT_DESC")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("LIC_SRNO")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("LIC_AUTHNO")%>
                                                                </td>
                                                                <td align="right">
                                                                    <%#Eval("LIC_QTY")%>
                                                                </td>
                                                                <%--<td align="left">
                                                                    <%#Eval("PurchaseDate")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("Price")%>
                                                                </td>--%>
                                                                <td align="right">
                                                                    <%#Eval("AMOUNTUSD")%>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                                
                                                </td>
                                            
                                            </tr>
                                                 
                                           </table>
                                           </ContentTemplate>
                                           <Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="btnITHSearch" />
                                           </Triggers>
                                         </asp:UpdatePanel>
                                         
                                       </asp:Panel> 
                                       
                                       <asp:Panel ID="pnlAMC" runat="server" Visible="false" >
                                         
                                         <asp:UpdatePanel ID="upAMC" runat="server">
                                         <ContentTemplate>
                                           <table cellpadding="2" cellspacing="0" width="100%" >
                                            <tr>
                                               <td style="text-align: right">Contract# :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                                  <asp:TextBox ID="txtAMCContNo" runat="server"></asp:TextBox>
                                               </td>
                                               
                                               <td style="text-align: right">Start Date :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtAMCStDt" runat="server"></asp:TextBox>
                                               <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImageButton1" TargetControlID="txtAMCStDt" PopupPosition="BottomRight">
                                                </ajaxToolkit:CalendarExtender>
                                               </td>
                                               
                                               <td style="text-align: right">End Date :&nbsp;
                                               </td>
                                               <td style="text-align: left">
                                               <asp:TextBox ID="txtAMCEndDt" runat="server"></asp:TextBox>
                                               <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImageButton2" TargetControlID="txtAMCEndDt" PopupPosition="BottomRight">
                                                </ajaxToolkit:CalendarExtender>
                                               </td>
                                               </tr>
                                               
                                               <tr>
                                               <td style="text-align: right"><%--Purchase Dt :&nbsp;--%>
                                               </td>
                                               <td style="text-align: left">
                                               <%--<asp:TextBox ID="txtPurchaseDt" ReadOnly="true" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    PopupButtonID="ImageButton1" TargetControlID="txtPurchaseDt" PopupPosition="BottomRight">
                                                </ajaxToolkit:CalendarExtender>--%>
                                               </td>
                                               
                                               <td colspan="4" style="text-align: right; padding-right:10px;">
                                                <asp:Button ID="btnAMCSearch" runat="server" Text="Search" Width="80px" onclick="btnAMCSearch_Click" />
                                               </td>
                                               
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="vertical-align: top; padding-top:2px;">
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                            <table cellpadding="2" cellspacing="1" width="100%" rules="all" height="26px">
                                                <colgroup>
                                                        <col width="30px" />
                                                        <col width="150px" />
                                                        <col width="80px" />
                                                        <col width="80px" />
                                                        <col />
                                                        <col width="120px" />
                                                        <%--<col width="100px" />
                                                        <col width="100px" />--%>
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <tr class="blueheader">
                                                        <td>
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Contract#
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;Start Date
                                                        </td>
                                                        <td style="text-align: left">
                                                            &nbsp;End Date
                                                        </td>
                                                        <td style="text-align: left">
                                                            Contract Vendor
                                                        </td>
                                                        <%--<td style="text-align: left">
                                                            Purchase Date
                                                        </td>
                                                        <td style="text-align: left">
                                                            Price
                                                        </td>--%>
                                                        <td style="text-align: left">
                                                          &nbsp;Amount (US$)
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                            </table>
                                            </div>
                                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;
                                                height: 300px; text-align: center;">
                                                <table cellpadding="2" cellspacing="2" width="100%" rules="all">
                                                    <colgroup>
                                                        <col width="30px" />
                                                        <col width="150px" />
                                                        <col width="80px" />
                                                        <col width="80px" />
                                                        <col />
                                                        <col width="120px" />
                                                        <%--<col width="100px" />
                                                        <col width="100px" />--%>
                                                        <col width="25px" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptAMC" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgViewAMC" ToolTip="View Details" runat="server" CommandArgument='<%# Eval("ItemId").ToString() %>'
                                                                        ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="imgViewAMC_OnClick" />
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("CONTRACTNO")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("STARTDATE")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("ENDDATE")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("CONTRACT_VENDOR")%>
                                                                </td>
                                                                <%--<td align="left">
                                                                    <%#Eval("PurchaseDate")%>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("Price")%>
                                                                </td>--%>
                                                                <td align="right">
                                                                    <%#Eval("AMOUNT_USD")%>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                                
                                                </td>
                                            
                                            </tr>
                                                 
                                           </table>
                                           </ContentTemplate>
                                           <Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="btnITHSearch" />
                                           </Triggers>
                                         </asp:UpdatePanel>
                                         
                                       </asp:Panel>     
                                       
                                       </td>
                                    
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        </table>  
                    </td>
                    </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
