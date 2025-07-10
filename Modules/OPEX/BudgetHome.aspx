<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetHome.aspx.cs" Inherits="BudgetHome" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--<%@ Register Src="~/Modules/Purchase/UserControls/BudgetLeftMenu.ascx" TagName="Left" TagPrefix="BM" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


     <%--<link href="CSS/style.css" rel="Stylesheet" type="text/css" />--%>
     <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <%--<link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />--%>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>        
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>     
            <td>
             <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="../HRD/Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress> 
            <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td>
                    <div class="Text headerband"> 
                        Budget Variance Summary
                    </div>
                   </td>
                </tr>
                <tr>
                      <td>
                        <table cellpadding="5" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >
                                <colgroup>
                                <col width="260px" />
                                <col width="260px" />
                                <col width="60px" />
                                <col width="50px" />
                                <col />
                                </colgroup>
                                <tr style="font-weight:bold">
                                    <td>Fleet / Company</td>
                                    <td>Select <asp:Label runat="server" ID="lblFC" Text="Company"></asp:Label></td>
                                    <td>Year
                                    </td>
                                    <td>Month
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="text-align:center;background-color:#F5FAFF;">
                                    <td>
                                       <asp:RadioButtonList ID="rdoList" runat="server" OnSelectedIndexChanged="rdoList_OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                       <asp:ListItem Text="Fleet" Value="1" Selected="True"></asp:ListItem>
                                       <asp:ListItem Text="Company" Value="2"></asp:ListItem>
                                       </asp:RadioButtonList>
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" Width="270px"></asp:DropDownList>
                                       <asp:DropDownList ID="ddlOwner" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOwner_OnSelectedIndexChanged" Width="270px" Visible="false"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MonthYearChanged" Width="60px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MonthYearChanged" Width="50px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style=" text-align :left">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn"
                                            onclick="btnPrint_Click" Visible="false" />
                                    </td>
                                </tr>
                            
                        </table> 
                      </td>
                 </tr>
                <tr>
                    <td style="padding:2px;">
                    <center>
                    <div style="height:35px; overflow-y:scroll;overflow-x:hidden;">
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                        <colgroup>
                            <col  />
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="150px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <thead>
                                <tr class= "headerstylegrid">
                                    <td>Vessel</td>
                                    <td>Manning</td>
                                    <td>Consumables</td>
                                    <td>Lube Oils</td>
                                    <td>Spares, Maintenance &amp; Repair</td>
                                    <td>General Expenses</td>
                                    <td>Insurance</td>
                                    <td>Management &amp; Admin Fees</td>
                                    <td>Total YTD Var(%)</td>
                                </tr>
                            </thead>
                        </colgroup>
                    </table>
                    </div>
                    <div style="height:335px;" class='ScrollAutoReset' id='dv_KPI_Details_HSQE009'>
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat1' border='1' style='border-collapse:collapse' bordercolor="#c2c2c2">
                        <colgroup>
                            <col  />
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="150px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <col width="130px"/>
                            <tbody>
                                <asp:Repeater ID="rptShip" runat="server">
                                    <ItemTemplate>
                                        <tr onmouseover="">
                                            <td style="text-align :left"><%#Eval("Vessel")%></td>
                                            <td style="text-align:right"><span><%#Eval("Manning_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Manning"))>0)?"red":"green" %>'><%#Eval("Manning")%>%</span> ) </td>
                                            <td style="text-align:right"><span><%#Eval("Consumables_amt")%></span>(<span style='color:<%#(Common.CastAsDecimal(Eval("Consumables"))>0)?"red":"green" %>'><%#Eval("Consumables")%>%</span>) </td>
                                            <td style="text-align:right"><span><%#Eval("Lube Oils_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Lube Oils"))>0)?"red":"green" %>'><%#Eval("Lube Oils")%>%</span> ) </td>
                                            <td style="text-align:right"><span><%#Eval("Spares, Maintenance & Repair_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Spares, Maintenance & Repair"))>0)?"red":"green" %>'><%#Eval("Spares, Maintenance & Repair")%>%</span>) </td>
                                            <td style="text-align:right"><span><%#Eval("General Expenses_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("General Expenses"))>0)?"red":"green" %>'><%#Eval("General Expenses")%>%</span> ) </td>
                                            <td style="text-align:right"><span><%#Eval("Insurance_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Insurance"))>0)?"red":"green" %>'><%#Eval("Insurance")%>%</span> ) </td>
                                            <td style="text-align:right"><span><%#Eval("Management & Admin Fees_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Management & Admin Fees"))>0)?"red":"green" %>'><%#Eval("Management & Admin Fees")%>%</span> ) </td>
                                            <td style="text-align:right"><span><%#Eval("Var_amt")%></span>( <span style='color:<%#(Common.CastAsDecimal(Eval("Var"))>0)?"red":"green" %>'><%#Eval("Var")%>%</span> ) </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </colgroup>
                    </table>
                    </div>
                    <div style="height:25px; overflow-y:scroll;overflow-x:hidden;">
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                    <tfoot>
                        <tr>
                            <td>&nbsp;</td>
                            <%--<td style="width:60px">&nbsp;</td>
                            <td style="width:80px">&nbsp;</td>
                            <td style="width:70px">&nbsp;</td>
                            <td style="width:180px">&nbsp;</td>
                            <td style="width:120px">&nbsp;</td>
                            <td style="width:70px">&nbsp;</td>
                            <td style="width:170px">&nbsp;</td>
                            <td style="width:130px">&nbsp;</td>--%>
                        </tr>
                    </tfoot>
                    </table>
                    </div>
                    </center>
                    </td>
                </tr>
                </table> 
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        </tr>
      </table>
  </asp:Content>
