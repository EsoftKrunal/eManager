<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LubeConsumptionSummary.aspx.cs" Inherits="Modules_Purchase_Invoice_LubeConsumptionSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
    <div id="log" style="display:none"></div>
    <div>
    <center>
     <%--   <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td style="  padding:10px;" class="text headerband">
             <strong> Lube Consumption Summary</strong>
         </td>
         </tr>
         </table>--%>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style=" vertical-align:top; padding-top:3px;padding-left:5px;padding-right:5px;">
    <div style="border:solid 1px #008AE6;font-family:Arial, Helvetica, sans-serif;font-size:11px; " >
        <div>
  <%--  <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
         <table cellpadding="6" cellspacing="0" width="100%">
           <tr>
               <td style=" background-color:#FFFFCC">
                 <asp:Label ID="lblMessage" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
           <td>
           <table border="1" cellpadding="0" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
           <tr>
           <td>
            
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                     
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Vessel :</td>
                          <td style="text-align: left;padding-left:10px;" >
                            <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="90%"></asp:DropDownList>
                          </td>
                           <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                             Year :</td>
                          <td style="text-align: left; padding-left:10px; width: 175px; ">
                             <asp:DropDownList ID="ddlYear" runat="server" Width="100px" >
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              
                          </td>
                          <td style="text-align: left;width: 175px;" >
                            
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                          </td>
                          

                      </tr>
                    
                      <tr style="background-color:#E6F3FC">
                             <td colspan="7">
                                 <%--<div style="text-align:left;padding-left:20px;height:25px;vertical-align:central;">
                                     <b> Item wise Purchase Order Details : </b>
                                 </div>--%>
                                 <div class="table-responsive">
                                   <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 60px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:60px;  border-collapse:collapse;">
                                            <colgroup>
                                                 <col style="width:3%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:28%;"/>
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:5%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                <tr class= "headerstylegrid" >
                                                     <td>View</td>
                                                    <td>PO #</td>
                                                    <td>Lube Type</td>
                                                    <td style="text-align:center;" colspan="4"> Received </td>
                                                    <td style="text-align:center;" colspan="2">Consumption</td>
                                                    <td style="text-align:center;" colspan="2">ROB</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td style="text-align:center;">Quantity</td>
                                                    <td style="text-align:center;">Unit Price</td>
                                                    <td style="text-align:center;">Unit </td>
                                                    <td style="text-align:center;">Total Cost</td>
                                                    <td style="text-align:center;">Quantity</td>
                                                    <td style="text-align:center;">Total Cost</td>
                                                    <td style="text-align:center;">Quantity</td>
                                                    <td style="text-align:center;">Total Cost</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                               <div id="divPayment" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 400px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:3%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:28%;"/>
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:5%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                    </colgroup>
                                            <asp:Repeater ID="RptPOConsumption" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center"><asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("BidItemId")%>' BidId='<%#Eval("BidId")%>' OnClick="imgbtnView_Click" CausesValidation="false" /></td>
                                                        <td align="left"><%#Eval("Bidponum")%></td>
                                                        <td align="left"><%#Eval("BidDescription")%>
                                                        </td>
                                                        <td align="center"><%#Eval("RecQty")%></td>
                                                        <td align="center"><%#Eval("PriceFor")%></td>
                                                        <td align="center"><%#Eval("uom")%></td>
                                                        <td align="center"><%#Eval("ForTotal")%></td>
                                                        <td align="center"><%#Eval("ConsumpQty")%></td>
                                                        <td align="center"><%#Eval("ConsumpTotal")%></td>
                                                        <td align="center"><%#Eval("ROBQty")%></td>
                                                        <td align="center"><%#Eval("ROBTotal")%></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color:#E6F3FC">
                                                          <td align="center"><asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("BidItemId")%>' BidId='<%#Eval("BidId")%>' OnClick="imgbtnView_Click" CausesValidation="false"/></td>
                                                        <td align="left"><%#Eval("Bidponum")%></td>
                                                        <td align="left"><%#Eval("BidDescription")%>
                                                        </td>
                                                        <td align="center"><%#Eval("RecQty")%></td>
                                                        <td align="center"><%#Eval("PriceFor")%></td>
                                                        <td align="center"><%#Eval("uom")%></td>
                                                        <td align="center"><%#Eval("ForTotal")%></td>
                                                        <td align="center"><%#Eval("ConsumpQty")%></td>
                                                        <td align="center"><%#Eval("ConsumpTotal")%></td>
                                                        <td align="center"><%#Eval("ROBQty")%></td>
                                                        <td align="center"><%#Eval("ROBTotal")%></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                             </table>
                                        </div>
                                     </div>
                             </td>
                          </tr>
                     </table>
           </td>
           </tr>
           </table>
           </td>
         </tr>
       </table>
     <%--  </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btn_Save"  />
       </Triggers>
     </asp:UpdatePanel>--%>
            </div>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dv_LubeSummary" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:2;top:25px; border:solid 10px black;">
            <center>
                <div class="text headerband">
                    <strong> Lube Consumption Details </strong>
                </div>
                <table width="100%">
                    <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Vessel :</td>
                          <td style="text-align: left;padding-left:10px;width:200px;" >
                             <asp:Label ID="lblVesselName" runat="server" > </asp:Label>
                          </td>
                        <td  align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                            Supplier Port : 
                        </td>
                        <td style="text-align: left;padding-left:10px;width:200px;">
                            <asp:Label ID="lblSupplierPort" runat="server" > </asp:Label>
                        </td>
                         <td  align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                            Supplier Date : 
                        </td>
                        <td style="text-align: left;padding-left:10px;width:200px;">
                            <asp:Label ID="lblSupplierDt" runat="server" > </asp:Label>
                        </td>
                          
                      </tr>
                   <tr style="height:40px;vertical-align:top;" > <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Lube Type :</td>
                          <td style="text-align: left;padding-left:10px;" colspan="5">
                             <asp:Label ID="lblItemDescription" runat="server" > </asp:Label>
                          </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Received Qty :</td>
                          <td style="text-align: left;padding-left:10px;width:200px;" >
                             <asp:Label ID="lblReceivedQty" runat="server" > </asp:Label>
                          </td>
                        <td  align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                           Unit Price/UOM :  
                        </td>
                        <td style="text-align: left;padding-left:10px;width:200px;">
                             <asp:Label ID="lblUnitPrice" runat="server" > </asp:Label> / <asp:Label ID="lblUOM" runat="server" > </asp:Label>
                        </td>
                         <td  align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                            Total Cost  : 
                        </td>
                        <td style="text-align: left;padding-left:10px;width:200px;">
                           <asp:Label ID="lblTotalReceivedCost" runat="server" > </asp:Label>
                        </td>
                          
                      </tr>
                </table>
            
                        <div class="table-responsive">
                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                <colgroup>
                                               
                                    <col style="width:15%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:2%;"/>
                                    <tr class= "headerstylegrid" >
                                        <td style="text-align:center;">Consumption  Month & Year</td>
                                        <td style="text-align:center;">Previous Month ROB</td> 
                                        <td style="text-align:center;">Consumption Qty</td>
                                        <td style="text-align:center;">ROB</td> 
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                        <div id="div1" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 250px; text-align:center;">
                                <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                    <colgroup>
                                    <col style="width:15%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:10%;text-align:center;" />
                                    <col style="width:2%;"/>
                                        </colgroup>
                                <asp:Repeater ID="rptConsumptionDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center"><%#Eval("ConsumpMonthYear")%></td>
                                            <td align="center"><%#Eval("PreMonthROB")%></td>
                                            <td align="center"><%#Eval("CurrMonthConsumpQty")%> </td>
                                            <td align="center"><%#Eval("CurrMonthROB")%></td>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color:#E6F3FC">
                                            <td align="center"><%#Eval("ConsumpMonthYear")%></td>
                                            <td align="center"><%#Eval("PreMonthROB")%></td>
                                            <td align="center"><%#Eval("CurrMonthConsumpQty")%> </td>
                                            <td align="center"><%#Eval("CurrMonthROB")%></td>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                    </table>
                            </div>
                        </div>  
                 <div class="searchsection-btn">
                      <asp:Button ID="btnClose1" runat="server" Text="Close" Width="80px"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnClose1_Click" CausesValidation="false" /> &nbsp;
                 </div>
              

               
               
           
            </center>
        </div>
    </center>
    </div>
    </div>

    </td>
    
    </tr>
    </table>
    </center>
    </div>
            
    </div>
    </form>
</body>
</html>
