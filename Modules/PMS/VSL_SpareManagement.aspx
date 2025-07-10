<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VSL_SpareManagement.aspx.cs"
    Inherits="VSL_SpareManagement" %>

<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function opensearchwindow() {
            if (typeof (winref) == 'undefined' || winref.closed) {
                winref = window.open('SearchComponentsForVessel.aspx', '', '');
            }
            else {
                winref.focus();
            }
        }
        function reloadComponents(CompCode) {
            document.getElementById('hfSearchCode').value = CompCode;
            __doPostBack('btnSearchedCode', '');
        }

        function setFocus(ctltitle) {
            ctltitle = ctltitle.toLowerCase().replace(/^\s+|\s+$/g, "");
            ctls = document.getElementsByTagName("a");
            i = 0;
            for (i = 0; i <= ctls.length - 1; i++) {
                var v = ctls[i].title.toLowerCase().replace(/^\s+|\s+$/g, "");
                if (v == ctltitle) {
                    ctls[i].focus();
                    dvscroll_Componenttree.scrollLeft = 0;
                    dvscroll_Componenttree.scrollTop = dvscroll_Componenttree.scrollTop + 50;
                }
            }
        }
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }

        function reloadunits() {

            __doPostBack('btnRefresh', '');
        }
        function opendefectdetails(DN) {
            //window.open('Reports/Office_BreakdownDefectReport.aspx?DN=' + DN, '', '');
            window.open('Office_Popup_BreakDown.aspx?DN=' + DN + '&FM=1', '', '');
        }
        function openAddUnPlanJob(VssCode, UPId) {
            //window.open('Reports/Office_BreakdownDefectReport.aspx?DN=' + DN, '', '');
            window.open('Popup_AddUnPlanJob.aspx?VSL=' + VssCode + '&UPId=' + UPId + '', '', '');
        }

        function OpenPrintWindow() {
            window.open('Reports/SpareMgmtReport.aspx', '', '');
        }

       
       
    </script>
    <style type="text/css">
        .whitebordered tr td
        {
            border:solid 1px #fff;
        }
        .bordered tr td
        {
            border:solid 1px #e5e5e5;
        }
        .CriticalType_C
        {
            background-color:#ff6666;
            display:inline-block;
            padding:2px;
            height:12px;
            width:12px;
            text-align:center;
            word-break: break-all;
            font-size:9px;
        }

         .OR_True
        {
            background-color:#FFBE33;
            display:inline-block;
            padding:2px;
            height:12px;
            width:12px;
            text-align:center;
            word-break: break-all;
            font-size:9px;
        }
          .CriticalType_E
        {
            background-color:#66ff66;
            display:inline-block;
            padding:2px;
            height:12px;
            width:12px;
            text-align:center;
            word-break: break-all;
            font-size:9px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align: center">
    <div>
        <hm:HMenu runat="server" ID="menu2" />
    </div>

            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position: absolute; top: 399px; left: 244px; width: 100%; z-index: 100;
                        text-align: center; color: Blue;">
                        <center>
                            <div style="border: dotted 1px blue; height: 50px; width: 120px; background-color: White;">
                                <img src="Images/loading.gif" alt="loading">
                                Loading ...
                            </div>
                        </center>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

        <div>
          
            <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Always">
                <ContentTemplate>
                    <asp:HiddenField ID="hfReportQuery" runat="server" Value="" />
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td style="text-align: left; padding-left: 1px;">
                                <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None">
                                    <table cellpadding="3" cellspacing="0" rules="none" border="0" style="border-collapse: collapse; background-color: #f9f9f9;" width="100%">
                                        <tr style="background-color: #F2F2F2">
                                           <%-- <td style="text-align: center; font-weight: bold; padding-left: 1px">
                                                Fleet &nbsp;
                                                <asp:DropDownList ID="ddlFleet" runat="server" Width="100px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>--%>
                                            <td style="text-align: center; font-weight: bold;">
                                                Comp. Code &nbsp;<asp:TextBox ID="txtCompCode" MaxLength="12" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="text-align: center; font-weight: bold;">
                                                Comp. Name &nbsp;<asp:TextBox ID="txtCompName" MaxLength="12" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <b>Spare Status : &nbsp;</b>
                                                <asp:DropDownList ID="ddlSpareStatus" runat="server" Width="110px">
                                                <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:CheckBox ID="chlsCriticalComponent" runat="server" Text="Critical Components" /> 
                                                
                                            </td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:CheckBox ID="chlsCriticalSpare" runat="server" Text="Critical Spares" /> 
                                            </td>
<td style="text-align: left; font-weight: bold;">         
                                                   <asp:CheckBox ID="chkOR" runat="server" Text="OR" />
                                               </td>
                                               <td align="right">
                                                <asp:Button ID="btnSearch" Text="Search" CssClass="btnorange" runat="server" OnClick="btnSearch_Click" />&nbsp;
                                                
                                                <asp:Button ID="btnClear" Text="Clear" CssClass="btnorange" runat="server" OnClick="btnClear_Click" />&nbsp;
                                                <asp:Button ID="btnPrint" Text="Print" CssClass="btnorange" runat="server" OnClick="btnPrint_Click"/>
                                                <%--PostBackUrl="~/Reports/SpareMgmtReport.aspx" --%>
                                                
                                            </td>
                                        </tr>
                                    </table>
                               
                                <div style="height:30px; overflow-y:scroll; border:solid 1px #5b8fc9; overflow-x:hidden;  ">
                                <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse; height:30px; " class="whitebordered">
                                    <colgroup>
                                        <col style="width: 45px;" />
                                        <col style="width: 45px;" />
                                        <col style="width: 400px;" />
                                        <col />
                                        <col style="width: 120px;" />
                                        <col style="width: 120px;" />
                                        <col style="width: 70px;" />
                                        <col style="width: 50px;" />
                                        <col style="width: 200px;" />
                                        <tr align="left" class= "headerstylegrid">
                                            <td>Spare</td>
                                            <td>Stock</td>
                                            <td>Spare Name</td>
                                            <td>Component Name</td>
                                            <td>Component Code</td>
                                            <td>Part#</td>
                                            <td>Qty(Min)</td>
                                            <td>ROB</td>
                                            <td>Stock Location</td>
                                        </tr>
                                    </colgroup>
                                </table>
                                </div>
                                <div id="dvDefects" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;overflow-x: hidden; height: 360px;">

                                    <table cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="bordered">
                                        <colgroup>
                                            <col style="width: 45px;" />                                            
                                            <col style="width: 45px;" />    
                                            <col style="width: 400px;" />
                                            <col />
                                            <col style="width: 120px;" />
                                            <col style="width: 120px;" />
                                            <col style="width: 70px;" />
                                            <col style="width: 50px;" />
                                            <col style="width: 200px;" />
                                            
                                        </colgroup>
                                        <asp:Repeater ID="rptDefects" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center">
                                                        <%--<a href="AddSpares.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/HourGlass.png" style="border:none" /> </a>--%>
                                                        <a href="Ship_AddEditSpares.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/HourGlass.png" style="border:none" /> </a>
                                                        <asp:HiddenField ID="hfPreCritical" runat="server" Value='<%#Eval("Critical")%>' />
                                                        <asp:HiddenField ID="hfdComponentCritical" runat="server" Value='<%#Eval("CriticalType")%>' />
                                                        <asp:HiddenField ID="hfdSpareStatus" runat="server" Value='<%#Eval("Status")%>' />
                                                        <asp:HiddenField ID="hfVesselCode" runat="server" Value='<%#Eval("VesselCode")%>' />
                                                        <asp:HiddenField ID="hfComponentId" runat="server" Value='<%#Eval("ComponentId")%>' />
                                                        <asp:HiddenField ID="hfOffice_Ship" runat="server" Value='<%#Eval("Office_Ship")%>' />
                                                        <asp:HiddenField ID="hfSpareId" runat="server" Value='<%#Eval("SpareId")%>' />
                                                        <asp:HiddenField ID="hfCritical" runat="server" Value='<%#Eval("Critical")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <a href="Ship_AddEditStock.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/stock.png" style="border:none" /> </a>
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("SpareName")%>
                                                        <%#"<span class='CriticalType_" + Eval("Critical").ToString() + "'>" + Eval("Critical").ToString() + "</span>"%>
							<%#"<span style='color:white' class='OR_" + Eval("isOR").ToString() + "'>O</span>"%>                                                        
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("ComponentName")%>
                                                        <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>" + Eval("CriticalType").ToString() + "</span>"%>
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("ComponentCode")%>
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("PartNo")%>
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("MinQty")%>
                                                    </td>
                                                    <td align="center">
                                                        <%#Eval("ROB")%>
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("Location")%>
                                                    </td>
                                                    
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div style="padding: 5px; background-color:#ffffb3; overflow:auto; height:25px;">
                                
                                     
                                     <span style="float:right">
                                     
                                    <asp:Label ID="lblRecordCount" Style="font-weight: bold" runat="server"  Font-Size=Larger></asp:Label> 
                                     </span>
                                </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>

                        <%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                                    <div id="divEditCriticalComp" style="position: absolute; top: 0px; left: 0px; height: 100%;width: 100%; z-index: 100; display:none;" runat="server" visible="false">
                                        <center>
                                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)">
                                            </div>
                                            <div style="position: relative; width: 350px; height: 100px; padding: 3px; text-align: center;border: solid 1px #4371a5; background: white; z-index: 150; top: 30px; opacity: 1;filter: alpha(opacity=100)">
                                                <div style="background-color: #c2c2c2; padding: 7px; text-align: center;">
                                                    <b style="font-size: 14px;">Confirmation</b></div>
                                                    <br />
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkCritical" runat="server" Text="Critical Component" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnCloseCritical" runat="server" Text="Close" OnClick="btnCloseCritical_OnClick" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="lblMsgCritical" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                                                        
                                                                                    
                                            </div>
                                        </center>
                                    </div>
                                    <%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
                                    
    </div>
    </form>
</body>
</html>
