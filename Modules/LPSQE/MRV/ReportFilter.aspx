<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportFilter.aspx.cs" Inherits="MRV_ReportFilter" %>
<%@ Register src="mrvmenu.ascx" tagname="mrvmenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    

    <link href="../../HRD/JS/jquery.datetimepicker.js" rel="stylesheet" />
    <script src="../../HRD/JS/jquery.min.js"></script>
    
    

    <script type="text/javascript" src="../js/KPIScript.js"></script>
</head>
<body style="margin:0px !important;" >
<form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
           <%-- <div class="pagename">
                MRV - Monitoring , Reporting & Verfication
            </div>--%>
           <%-- <div class="menuframe">                
                <uc1:mrvmenu ID="mrvmenu1" runat="server" />                
            </div>--%>
            <div style="border-bottom:solid 5px #4371a5;"></div>
        
        <div style="font-weight:bold;text-align:center;padding:3px;">
                <table cellpadding="5" cellspacing="0" border="0" width="100%" style="text-align:left;"> 
                    <col  width="100px"/>
                    <col  width="200px"/>
                    <col  width="100px"/>
                    <col  width="200px"/>
                    <tr>
                        <td style="text-align:right;">Vessel</td>
                        <td>
                            <asp:DropDownList ID="ddlVessel" runat="server" Width="120px"></asp:DropDownList>
                        </td>
                        <td style="text-align:right;">Year</td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            EU Voyage
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFEUVoyage" runat="server" CssClass="input input_text medium" Width="70px">
                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <%--<td>Voyage No.</td>
                        <td>
                            <asp:TextBox ID="txtFVoyageNo" runat="server" CssClass="input input_text medium" Width="150px"></asp:TextBox>
                        </td>
                        <td>Period</td>
                        <td>
                            <asp:TextBox ID="txtFPeriodFrom" runat="server" CssClass="input input_text medium date_only" Width="90px" ></asp:TextBox>
                            -
                            <asp:TextBox ID="txtFPeriodTo" runat="server" CssClass="input input_text medium date_only" Width="90px"></asp:TextBox>
                        </td>
                        <td>Condition</td>
                        <td>
                            <asp:DropDownList ID="ddlFCondition" runat="server" CssClass="input input_text medium" Width="120px">
                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                <asp:ListItem Value="B" Text="Ballast"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Laden"></asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                        <td></td>
                        <td style="text-align:right;">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_OnClick" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" OnClick="btnClear_OnClick" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn" OnClick="btnReport_OnClick"  />
                        </td>
                    </tr>
                </table>
            </div>
        <%----------------------------------------------------------------------------------------------------%>
        <div>
            <div style="overflow-x:hidden;overflow-y:scroll;top:0px;left:0px;">
            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                <colgroup>
                <col width="170px" />
                <col width="150px" />
                <col width="150px" />
                <col width="120px" />
                <col width="130px" />
                <col width="130px" />
                    </colgroup>
                <%--<col width="100px" />--%>
                <tr class= "headerstylegrid">
                        <th style="text-align:left">Voyage No.</th>
                        <th style="text-align:left">From Port</th>
                        <th style="text-align:left">To Port</th>
                        <th style="text-align:center">Start Date</th>
                        <th style="text-align:center">End Date</th>
                        <th style="text-align:left">Condition</th>
                      <%--  <th style="text-align:left">
                            <table cellpadding="0" cellspacing="0" border="0" width="200px">
                                <col width="100px"/>
                                <tr>
                                    <td>Flowmeter</td>
                                    <td>Tank Sounding</td>
                                </tr>
                            </table>
                        </th>--%>
                    </tr>                 
            </table>
            </div>
            <div style="height:500px;overflow-x:hidden;overflow-y:scroll;top:0px;left:0px;" class="ScrollAutoReset" id="div001">
                <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">  
                    <colgroup>
                    <col width="170px" />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="120px" />
                    <col width="130px" />
                    <col width="130px" />
                    <%--<col width="100px" />--%>
                        </colgroup>
            <asp:Repeater runat="server" ID="rptAllVoyage">
                   
                <ItemTemplate>
                    <tr>
                        <td style="text-align:left">
                            <a target="_blank" href="VoyageHistory.aspx?VesselCode=<%#Eval("VesselCode")%>&VoyageId=<%#Eval("VoyageID")%>"><%#Eval("VoyageNo")%></a>                            
                            <asp:HiddenField ID="hfdVoyageID" runat="server" Value='<%#Eval("VoyageID")%>' />
                        </td>
                        <td style="text-align:left"><%#Eval("FromPort")%></td>
                        <td style="text-align:left"><%#Eval("ToPort")%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("StartDate"))%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("EndDate"))%></td>
                        <td style="text-align:left"><%#((Eval("Condition").ToString()=="B")?"Ballast":"Laden")%></td>
                        <%--<td style="text-align:left">
                            <asp:RadioButtonList ID="rdoCalType" runat="server" RepeatDirection="Horizontal" SelectedValue='<%#Eval("CalcMode")%>' Width="200px">
                                <asp:ListItem Text="" Value="1" Selected="True" ></asp:ListItem>
                                <asp:ListItem Text="" Value="2" ></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>--%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>            
        </div>
        
     <%--------------------------------------------------------------------------------%>
        </div>
    </form>

    <script src="../js/jquery.datetimepicker.js"></script>
    <script type="text/javascript">
        function SetCalender() {
            $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            $('.date_time').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y H:i', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            $('.time_only').datetimepicker({ datepicker: false, closeOnDateSelect: true, format: 'H:i', formatDate: 'H:i', allowBlank: true, defaultSelect: false, validateOnBlur: false });
        }
        function Page_CallAfterRefresh() {
            SetCalender();
            RegisterAutoComplete();
        }
        SetCalender();

        //$(document).ready(function () {
        //    RegisterAutoComplete(); 
        //});
        
    </script>
</body>
</html>
                                        
