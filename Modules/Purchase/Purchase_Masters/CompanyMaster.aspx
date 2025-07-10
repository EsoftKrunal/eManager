<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                }
            }
        }
        function fncInputNumericValuesOnly(evnt) {
                
                if (!(event.keyCode == 13 || event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                    event.returnValue = false;

                }

            }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="font-family:Arial;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="border:2px solid #4371a5;" >
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
    <tr>
    <td>
       <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
       <uc2:Registers runat="server" ID="Registers1" />  
        <div class="text headerband">
            Company
        </div>
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:120px;" />
                <col  />
                <col style="width:120px;" />
                <col style="width:120px;" />
                <col style="width:120px;" />
                <col style="width:17px;" />
                <tr align="left" class= "headerstylegrid">
                    <td>
                        Company Code</td>
                    <td>
                        Company Name</td>
                    <td>
                        Active</td>
                    <td>
                       In Accts</td>
                    <td >
                        ReportCo</td>
                    <td></td>
                </tr>
            </colgroup>
        </table>
       <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px ; text-align:center;">
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:120px;" />
                <col  />
                <col style="width:120px;" />
                <col style="width:120px;" />
                <col style="width:120px;" />
                <col />
            </colgroup>
            <asp:Repeater ID="RptComp" runat="server">
                <ItemTemplate>
                        <tr id='tr<%#Eval("Company")%>' class='<%#(Eval("Company")!=SelectedCompId)?"":"selectedrow"%>' onclick='Selectrow(this,"<%#Eval("Company")%>");' >
                        <td>
                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <%# Eval("CompanyName")%>
                        </td>
                        <td >
                            <%# Eval("Active")%>
                        </td>
                        <td >
                            <asp:Label ID="lblInTrav" runat="server" Text='<%# Eval("InAccts") %>'></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblReportCo" runat="server" Text='<%# Eval("ReportCo") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr id='tr<%#Eval("Company")%>' class='<%#(Eval("Company")!=SelectedCompId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,"<%#Eval("Company")%>");' lastclass='alternaterow'>
                        <td>
                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <%# Eval("CompanyName")%>
                        </td>
                        <td >
                            <%# Eval("Active")%>
                        </td>
                        <td >
                            <asp:Label ID="lblInTrav" runat="server" Text='<%# Eval("InAccts") %>'></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblReportCo" runat="server" Text='<%# Eval("ReportCo") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Label ID="lblmsg" runat="server" CssClass="error" ></asp:Label>
       </div>
    </td>
    </tr>
    </table>
    </div>
</div> 
</asp:Content>
 



