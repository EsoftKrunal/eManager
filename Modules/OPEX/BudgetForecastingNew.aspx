<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetForecastingNew.aspx.cs" Inherits="BudgetForecastingNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Year Budget</title>
    <%--<link href="CSS/style.css" rel="Stylesheet" type="text/css" />--%>
     <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>    
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress> 
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td style="width:177px;">
                <div style="text-align : center;background-color : #4371a5;width:177px;min-height:465px; vertical-align : top; padding-bottom :15px;">
                <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/logo.jpg" Visible ="false"  />
                <br />
                 <div style="min-height:200px; width :177px" >
                    <table style="width:177px; text-align : center " cellpadding="5" cellspacing="0" border="0">
                    <tr id="trCurrBudget" runat="server">
                        <td> 
                            <a href='<%=Page.ResolveUrl("~/currentYearBudget.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/curryearbudget.jpg")%>'  border="0"/> </a>
                        </td>
                    </tr>
                    <tr id="trAnalysis" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/ReportingAndAnalysis.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/analysis_comments.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                    <tr id="trBudgetForecast" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/BudgetForecastingNextYearNew.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/budgetforecast.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                   <tr id="trPublish" runat="server">
                        <td>
                            <a href='<%=Page.ResolveUrl("~/PublishReport.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/publishreport.jpg")%>'  border="0"/></a>
                        </td>
                    </tr>
                    </table>
                </div> 
             </div>
            </td>
            <td>
                <table cellpadding="1" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="PageHeader">
                        Current Year Budget Sumamry
                   </td>
                </tr>
                <tr>
                      <td>
                        <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" >
                            <col  width="260px"/>
                            <col  width="260px"/>
                            <%--<col  width="260px"/>
                            <col  width="90px"/>
                            <col  width="90px"/>
                            <col  width="90px"/>
                            <col  width="50px"/>--%>
                            <col />
                            <tr style="font-weight:bold" >
                                <td>
                                    Fleet / Company
                                </td>
                                <td>
                                    Select <asp:Label runat="server" ID="lblFC" Text="Company"></asp:Label>
                                </td>
                                 <td>
                                   
                                </td>
                              <%-- <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                     &nbsp;</td>--%>
                            </tr>
                            <tr style="text-align:center;background-color:#F5FAFF;">
                                <td>
                                 <asp:RadioButtonList ID="rdoList" runat="server" OnSelectedIndexChanged="rdoList_OnSelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Fleet" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Company" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server"  OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" AutoPostBack="true" Visible="true" ></asp:DropDownList>
                                    <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Visible="false" ></asp:DropDownList>
                                 </td>
                               <td>
                                  <asp:DropDownList Visible="false" ID="ddlShip" runat="server"  OnSelectedIndexChanged="ddlShip_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                  <%--<asp:ImageButton ID="Print" runat="server" ImageUrl="~/Images/print.jpg" style=" float :right" onclick="Print_Click"/> --%>
                                  
                                  <%--<asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/Images/Edit.jpg" style=" float :right" onclick="btnEdit_Click"/> --%>
                                  
                                  <asp:Button ID="Print" runat="server" style=" float :right" onclick="Print_Click" Text="Print Details"/> 
                                  <asp:Button ID="btnExportToPDF" runat="server" style=" float :right" Text="Print Fleet Summary" OnClick="btnExportToPDF_OnClick"/>
                                  <asp:Button runat="server" ID="btnEdit" Text="Edit Budget" style=" float :right" onclick="btnEdit_Click"/> 
                               </td>
                               <%--  
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>--%>
                            </tr>
                        </table> 
                      </td>
                 </tr>
               <%-- <tr class="header">
                    <td>
                        &nbsp;</td>
                </tr> --%>
                <tr>
                    <td style="padding:2px; ">
                    <asp:Literal runat="server" ID="lit1"></asp:Literal>  
                    </td>
                </tr>
                </table> 
            </td>
        </tr>
      </table>
      </ContentTemplate>
      <Triggers>
        <asp:PostBackTrigger ControlID="Print" />
        <asp:PostBackTrigger ControlID="btnExportToPDF" />
      </Triggers>
        </asp:UpdatePanel>    
        
    </form>
</body>
</html>
