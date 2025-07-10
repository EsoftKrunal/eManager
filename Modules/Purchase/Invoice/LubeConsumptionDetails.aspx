<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="LubeConsumptionDetails.aspx.cs" Inherits="Modules_Purchase_Invoice_LubeConsumptionDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

    <title> EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div style="text-align: center">
    <div>
    <center>
       <%-- <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td style="  padding:10px;" class="text headerband">
             <strong>Monthly Lube Consumption</strong>
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
                          <td style="text-align: left;padding-left:10px;width: 150px;" >
                            <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="90%"></asp:DropDownList>
                          </td>
                           <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Month :</td>
                          <td style="text-align: left; padding-left:10px; width: 175px; ">
                             <asp:DropDownList ID="ddlMonth" runat="server" Width="90px" >
                                        <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Year :
                          </td>
                          <td style="text-align: left;width: 175px;" >
                             <asp:DropDownList ID="ddlYear" runat="server" Width="100px"  >
                              </asp:DropDownList>
                          </td>
                          <td align="left" style="text-align: left; padding-right:10px; width: 130px; ">
                              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" /> &nbsp;
                              
                          </td>
                      </tr>
                    
                      <tr style="background-color:#E6F3FC">
                             <td colspan="7">
                                <%-- <div style="text-align:left;padding-left:20px;height:25px;vertical-align:central;">
                                     <b> Purchase Order Details : </b>
                                 </div>--%>
                                 <div class="table-responsive">
                                   <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                               
                                                <col style="width:10%;"/>
                                                <col style="width:28%;"/>
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:6%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                <tr class= "headerstylegrid" >
                                                   
                                                    <td>PO #</td>
                                                    <td>Lube Type</td>
                                                    <td style="text-align:center;">Supply Port</td>
                                                    <td style="text-align:center;">Supply Date</td>
                                                    <td style="text-align:center;">Supply Qty</td>
                                                    <td style="text-align:center;">UOM</td>
                                                    <td style="text-align:center;"><asp:Label ID="lblPreviousMonthROB" runat="server"> </asp:Label> </td>
                                                    <td style="text-align:center;"><asp:Label ID="lblCurrMonthConsumption" runat="server" > </asp:Label></td>
                                                    <td style="text-align:center;"><asp:Label ID="lblCurrMonthROB" runat="server" > </asp:Label></td> 
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                               <div id="divPayment" runat="server" class="auto-style1" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                               
                                                <col style="width:10%;"/>
                                                <col style="width:28%;"/>
                                                <col style="width:8%;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:6%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                    </colgroup>
                                            <asp:Repeater ID="RptPOConsumption" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                     
                                                       <td align="left"><%#Eval("Bidponum")%></td>
                                                        <td align="left"><%#Eval("description")%>
                                                        </td>
                                                        <td align="left"><%#Eval("DeliveryPort")%></td>
                                                        <td align="left"><%#Common.ToDateString(Eval("DeliveryDate"))%></td>
                                                        <td align="center"> <%#Common.CastAsDecimal(Eval("QtyRecd"))%> </td>
                                                        <td align="center"><%#Eval("uom")%></td>
                                                        <td align="center"><%#Common.CastAsDecimal(Eval("PreMonthROB"))%>
                                                        </td>
                                                        <td align="center" >
                                                           <%#Common.CastAsDecimal(Eval("CurrMonthConsumpQty"))%>
                                                        </td>
                                                        <td align="center" >
                                                           <%#Common.CastAsDecimal(Eval("CurrMonthROB"))%>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color:#E6F3FC">
                                                        <td align="left"><%#Eval("Bidponum")%></td>
                                                        <td align="left"><%#Eval("description")%>
                                                        </td>
                                                        <td align="left"><%#Eval("DeliveryPort")%></td>
                                                        <td align="left"><%#Common.ToDateString(Eval("DeliveryDate"))%></td>
                                                        <td align="center"> <%#Common.CastAsDecimal(Eval("QtyRecd"))%> </td>
                                                        <td align="center"><%#Eval("uom")%></td>
                                                        <td align="center"><%#Common.CastAsDecimal(Eval("PreMonthROB"))%>
                                                           
                                                           </td>
                                                        <td align="center" >
                                                           <%#Common.CastAsDecimal(Eval("CurrMonthConsumpQty"))%>
                                                        </td>
                                                        <td align="center" >
                                                           <%#Common.CastAsDecimal(Eval("CurrMonthROB"))%>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>

                                                
                                             </table>
                                             <asp:GridView  CellPadding="0" CellSpacing="0" ID="GvPOConsumption" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" Visible="false" OnRowDataBound="GvPOConsumption_RowDataBound"  >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="Bidponum" HeaderText="PO #" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="description" HeaderText="Lube Type" >
                                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="DeliveryPort" HeaderText="Delivery Port" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="DeliveryDate" HeaderText="Delivery Date" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="QtyRecd" HeaderText="Received Qty" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px"   />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="uom" HeaderText="UOM" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PreMonthROB" HeaderText="Previous Month ROB" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CurrMonthConsumpQty" HeaderText="Current Month Consumption" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="Unit Price" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CurrMonthROB" HeaderText="Current Month ROB" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            
                                            
                                        </Columns>                        

                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                                        </div>
                                     </div>
                    
                     </td>
                          </tr>
                      <tr style="background-color:#E6F3FC">
                       <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                           Vessel Remark :</td>
                        <td style="text-align: left;" width="300px" colspan="2" >
                            <asp:TextBox ID="txtConsumptionDescription" TextMode="MultiLine" runat="server" 
                               ReadOnly="true" Width="98%" Height="40px"   ></asp:TextBox>
                        </td>
                           <td style="text-align: right;width: 110px;padding-right:10px;width: 110px;"  >
                            Master Name :
                        </td>
                          <td style="text-align: left;" width="150px">
                               <asp:TextBox ID="txtMasterName"  runat="server" MaxLength="50"
                                ReadOnly="true"  Width="95%"   ></asp:TextBox>
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                               CE Name :  </td>
                        <td style="text-align: left;" width="150px">
                             <asp:TextBox ID="txtCEName"  runat="server" MaxLength="50"
                                ReadOnly="true"  Width="95%"  ></asp:TextBox>
                        </td>
                      
                      
                    </tr>
                  <tr style="background-color:#E6F3FC">
                       <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                           Updated By/On :       </td>
                        <td style="text-align: left;" width="300px" colspan="2" >
                              <asp:Label ID="txtAddedBy"  runat="server" MaxLength="50"
                                ReadOnly="true"  ></asp:Label> / <asp:Label ID="lblAddedon" runat="server"></asp:Label>
                        </td>
                      <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                                  </td>
                        <td style="text-align: left;" width="150px">
                          
                        </td>
                      <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                          
                      </td>
                       <td style="text-align: left;" >
                             
                           </td>
                  </tr>
                  <tr style="background-color:#E6F3FC">
                       <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                             Remark :       </td>
                        <td style="text-align: left;width: 300px;" colspan="2">
                              <asp:TextBox ID="txtOfficeRemark"  runat="server" MaxLength="200"
                                CssClass="input_box"  Width="98%" TextMode="MultiLine" ></asp:TextBox>
                        </td>
                      <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                           Verified By :   </td>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblVerifiedBy" runat="server" > </asp:Label> /  <asp:Label ID="lblVerifiedOn" runat="server" ></asp:Label>
                        </td>
                     
                       <td style="text-align: left;" >
                              <asp:Button ID="btnVerification" runat="server" Text="Verify"   CssClass="btn" OnClick="btnExport_Click"   Visible="false" />
                           <asp:Button ID="btnPrint" runat="server" Text="Export To Excel" CssClass="btn"  OnClick="btnPrint_Click" />
                           </td>
                  </tr>
                     </table>
               <div>
                   
               </div>
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
