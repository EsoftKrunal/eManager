<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="MRV_Home" %>
<%@ Register src="mrvmenu.ascx" tagname="mrvmenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="../js/jquery.datetimepicker.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/KPIScript.js"></script>
    <%--  <link rel="stylesheet" href="../../HRD/Styles/style.css" /> 
	<link rel="stylesheet" type="text/css" href="../../../css/app_style.css" />--%>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</head>
<body style="margin:0px !important;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
            <div class="text headerband">
                MRV - Monitoring , Reporting & Verfication
            </div>
            
        <%----------------------------------------------------------------------------------------------------%>
        <div>
            <div style="overflow-x:hidden;overflow-y:scroll;top:0px;left:0px;">
            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                <col />
                <col width="170px" />
                <col width="150px" />
                <col width="150px" />
                <col width="120px" />
                <col width="130px" />
                <col width="130px" />
                <col width="100px" />
                <tr class= "headerstylegrid">
                        <th style="text-align:left">Vessel Name</th>
                        <th style="text-align:left">Voyage No.</th>
                        <th style="text-align:left">From Port</th>
                        <th style="text-align:left">To Port</th>
                        <th style="text-align:center">Start Date</th>
                        <th style="text-align:center">End Date</th>
                        <th style="text-align:left">Condition</th>
                        <th style="text-align:left">EU Voyage</th>
                    </tr>                 
            </table>
            </div>
            <div style="height:400px;overflow-x:hidden;overflow-y:scroll;top:0px;left:0px;" class="ScrollAutoReset" id="div001">
            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%"> 
                <colgroup>
                <col />
                <col width="170px" />
                <col width="150px" />
                <col width="150px" />
                <col width="120px" />
                <col width="130px" />
                <col width="130px" />
                <col width="100px" />
                     </colgroup>
            <asp:Repeater runat="server" ID="rptData">
                   
                <ItemTemplate>
                    <tr>
                        <td style="text-align:left">
                            
                            <asp:LinkButton ID="lnlVesselName" runat="server" Text='<%#Eval("Name")%>' OnClick="lnlVesselNameOpenVoyagePop_OnClick" CommandArgument='<%#Eval("VesselCode")%>'></asp:LinkButton>
                        </td>
                        <td style="text-align:left">
                            <a target="_blank" href="VoyageHistory1.aspx?VesselCode=<%#Eval("VesselCode")%>&VoyageId=<%#Eval("VoyageID")%>"><%#Eval("VoyageNo")%></a>                            
                        </td>
                        <td style="text-align:left"><%#Eval("FromPort")%></td>
                        <td style="text-align:left"><%#Eval("ToPort")%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("StartDate"))%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("EndDate"))%></td>
                        <td style="text-align:left"><%#Eval("ConditionText")%></td>
                        <%--<td style="text-align:left"><%#((Eval("Condition").ToString()=="B")?"Ballast":"Laden")%></td>--%>
                        <td style="text-align:left">
                            <%#((Eval("FromPort_EU").ToString()=="True" ||Eval("ToPort_EU").ToString()=="True")?"Yes":(Eval("FromPort_EU").ToString()=="False" ||Eval("ToPort_EU").ToString()=="False")?"No":"")%>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                   
            </table>
            </div>
        </div>
        
     <div class="modal_frame" runat="server" id="divAllVoyageByVessel" visible="false" style="width:1000px;height:400px;margin:0px auto;" >
        <div class="text headerband">Voyage Details</div>
        <div class="modal_content">
            <div style="font-size:21px;font-weight:bold;text-align:center;padding:3px;">
                <asp:Literal ID="litVesselName" runat="server"></asp:Literal>
            </div>
            <div style="font-weight:bold;text-align:center;padding:3px;">
                <table cellpadding="5" cellspacing="0" border="0" width="100%"> 
                    
                    <tr>
                        <td>Voyage No.</td>
                        <td>Period</td>
                        <td>Condition</td>
                        <td>EU Voyage</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFVoyageNo" runat="server" CssClass="input input_text medium" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFPeriodFrom" runat="server" CssClass="input input_text medium date_only" Width="90px" ></asp:TextBox>
                            -
                            <asp:TextBox ID="txtFPeriodTo" runat="server" CssClass="input input_text medium date_only" Width="90px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFCondition" runat="server" CssClass="input input_text medium" Width="120px">
                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                <asp:ListItem Value="B" Text="Ballast"></asp:ListItem>
                                <asp:ListItem Value="L" Text="Laden"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFEUVoyage" runat="server" CssClass="input input_text medium" Width="70px">
                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_OnClick" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" OnClick="btnClear_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                    <tr class= "headerstylegrid">
                        
                        <th style="width:170px;text-align:left">Voyage No.</th>
                        <th style="width:150px;text-align:left">From Port</th>
                        <th style="width:150px;text-align:left">To Port</th>
                        <th style="width:120px;text-align:center">Start Date</th>
                        <th style="width:130px;text-align:center">End Date</th>
                        <th style="width:130px;text-align:left">Condition</th>
                        <th style="width:100px;text-align:left">EU Voyage</th>
                    </tr>                 
            <asp:Repeater runat="server" ID="rptAllVoyage">
                   
                <ItemTemplate>
                    <tr>
                        <td style="text-align:left">
                            <a target="_blank" href="VoyageHistory1.aspx?VesselCode=<%#Eval("VesselCode")%>&VoyageId=<%#Eval("VoyageID")%>"><%#Eval("VoyageNo")%></a>                            
                        </td>
                        <td style="text-align:left"><%#Eval("FromPort")%></td>
                        <td style="text-align:left"><%#Eval("ToPort")%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("StartDate"))%></td>
                        <td style="text-align:center"><%#FormatDate(Eval("EndDate"))%></td>
                        <td style="text-align:left"><%#((Eval("Condition").ToString()=="B")?"Ballast":"Laden")%></td>
                        <td style="text-align:left"><%#((Eval("FromPort_EU").ToString()=="True" ||Eval("ToPort_EU").ToString()=="True")?"Yes":"No")%></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
         </div>
        <div class="modal_footer" style="text-align:center;">            
            <asp:Button runat="server" id="Button1" CssClass="btn" Text="Close" OnClick="btnCloseVoyagePopup_Click" />
        </div>
    </div>
    <%--------------------------------------------------------------------------------%>
        </div>
    </form>

    <script  src="../js/jquery.datetimepicker.js"></script>
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
                                        
