<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpareManagement.aspx.cs" Inherits="SpareManagement" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControls/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
     <script src="./JS/jquery_v1.10.2.min.js" type="text/javascript"></script>

    <link href="./CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/Calender.js" type="text/javascript"></script>
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
                document.getElementById('ctl00_ContentMainMaster_hfSearchCode').value = CompCode;
               // __doPostBack('ctl00_ContentMainMaster_btnSearchedCode', '');
                document.getElementById('ctl00_ContentMainMaster_btnSearchedCode').click();
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

               // __doPostBack('btnRefresh', '');
                document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
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
    </style>
          <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="text headerband">
        Spare Management
        </div>
    <div style="font-family:Arial;font-size:12px;text-align: center" >
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
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;">
                                                Vessel &nbsp;
                                               
                                            </td>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;">
                                                Component Code/Name &nbsp;
                                            </td>
                                            
                                            <td>
                                                <b>Spare Status : &nbsp;</b>
                                                
                                            </td>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;">
                                               Stock Location  
                                            </td>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;">
                                               <asp:CheckBox ID="chlsCriticalComponent" runat="server" Text="Critical Components" /> 
                                            </td>
                                               <td style="text-align: left; font-weight: bold;">         
                                                   <asp:CheckBox ID="chkOR" runat="server" Text="Owner Requires" />
                                               </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;"> <asp:DropDownList ID="ddlVessels" runat="server" Width="200px">
                                                </asp:DropDownList></td>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;"> <asp:TextBox ID="txtCompCode" MaxLength="12" runat="server" Width="150px"></asp:TextBox></td>
                                            <td> <asp:DropDownList ID="ddlSpareStatus" runat="server" Width="110px">
                                                <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="text-align: center;padding-left:10px;"> 
                                                <asp:DropDownList ID="ddlStockLocation" runat="server" Width="150px">
                                                </asp:DropDownList></td>
                                            <td style="text-align: left; font-weight: bold;padding-left:10px;padding-top:5px;"> <asp:CheckBox ID="chlsCriticalSpare" runat="server" Text="Critical Spares" />  </td>
                                            <td>
                                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn" runat="server" OnClick="btnSearch_Click" />&nbsp;
                                                
                                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn" runat="server" OnClick="btnClear_Click" />&nbsp;
                                                <asp:Button ID="btnPrint" Text="Print" CssClass="btn" runat="server" OnClick="btnPrint_Click"/>
                                                <%--PostBackUrl="~/Reports/SpareMgmtReport.aspx" --%>
                                            </td>
                                        </tr>
                                    </table>
                               
                                <div style="height:30px; overflow-y:scroll; border:solid 1px #5b8fc9;   ">
                                <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse; height:30px;" class="whitebordered">
                                    <colgroup>
                                        <col style="width: 4%;" />
                                        <col style="width: 4%;" />
                                        <col style="width: 4%;" />
                                        <col style="width: 8%;" />
                                        <col style="width: 19%;"/>
                                      
                                        <col style="width: 23%;" />
                                       
                                        <col style="width: 15%;" />
                                        <col style="width: 6%;" />
                                        <col style="width: 6%;" />
                                        <col style="width: 12%;" />
                                       <%-- <col style="width: 10%;" />
                                        <col style="width: 7%;" />--%>
                                        <col style="width: 2%;" />
                                        <tr align="left" class= "headerstylegrid">
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>Comp. Code</td>
                                            <td>Component Name</td>
                                           
                                            <td>Spare Name</td>
                                            <td>Part#</td>
                                            <td>Qty(Min)</td>
                                            <td>ROB</td>
                                            <td>Stock Location</td>
                                            <td></td>
                                           <%-- <td>Updated By</td>
                                            <td>Updated On</td>--%>
                                        </tr>
                                    </colgroup>
                                </table>
                                </div>
                                <div id="dvDefects" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;overflow-x: hidden; height: 415px;">

                                    <table cellpadding="4" cellspacing="0" style="width: 100%;border-collapse: collapse;" class="bordered">
                                        <colgroup>
                                            <col style="width: 4%;" />                             
                                            <col style="width: 4%;" />                             
                                            <col style="width: 4%;" />
                                            <col style="width: 7%;" />
                                            <col style="width: 19%;"/>
                                            <col style="width: 23%;" />
                                            <col style="width: 15%;" />
                                            <col style="width: 6%;" />
                                            <col style="width: 6%;" />
                                            <col style="width: 12%;" />
                                          <%--  <col style="width: 7%;" />
                                            <col style="width: 7%;" />--%>
                                             <col style="width: 2%;" />
                                        
                                        <asp:Repeater ID="rptDefects" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_OnClick" Visible="false"></asp:LinkButton>
                                                        <asp:CheckBox ID="chkEdit" runat="server" Checked='<%#(Eval("Critical").ToString()=="C")%>' Visible="false" />
                                                        
                                                        <asp:HiddenField ID="hfPreCritical" runat="server" Value='<%#Eval("Critical")%>' />
                                                        <asp:HiddenField ID="HfPreOR" runat="server" Value='<%#Eval("isOR")%>' />
                                                        <asp:HiddenField ID="hfdComponentCritical" runat="server" Value='<%#Eval("CriticalType")%>' />
                                                        <asp:HiddenField ID="hfdSpareStatus" runat="server" Value='<%#Eval("Status")%>' />
                                                        <asp:HiddenField ID="hfVesselCode" runat="server" Value='<%#Eval("VesselCode")%>' />
                                                        <asp:HiddenField ID="hfComponentId" runat="server" Value='<%#Eval("ComponentId")%>' />
                                                        <asp:HiddenField ID="hfOffice_Ship" runat="server" Value='<%#Eval("Office_Ship")%>' />
                                                        <asp:HiddenField ID="hfSpareId" runat="server" Value='<%#Eval("SpareId")%>' />
                                                        <asp:HiddenField ID="hfCritical" runat="server" Value='<%#Eval("Critical")%>' />
                                                    </td>
                                                    <td>
                                                        <%--<a href="AddSpares.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/HourGlass.png" style="border:none" /> </a>--%>
                                                        <a title="View spare details." href="Ship_AddEditSpares.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/HourGlass.png" style="border:none" /> </a>
                                                    </td>
                                                    <td align="center">
                                                        <a title="View stock details."  href="Ship_AddEditStock.aspx?CompCode= <%#Eval("ComponentCode")%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"> <img src="Images/stock.png" style="border:none" /> </a>
                                                    </td>
                                                     <td align="left">
                                                        <%#Eval("ComponentCode")%>
                                                    </td>
                                                      <td align="left">
                                                        <%#Eval("ComponentName")%>
                                                        <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>" + Eval("CriticalType").ToString() + "</span>"%>
                                                        
                                                    </td>
                                                   
                                                    <td align="left">
                                                        <%#Eval("SpareName")%>
                                                        <%#"<span class='CriticalType_" + Eval("Critical").ToString() + "'>" + Eval("Critical").ToString() + "</span>"%>
                                                                                <b style='<%#(Eval("isOR").ToString().Trim()=="")?"display:none":""%>'> <%#"<span style='color:white' class='OR_" + Eval("isOR").ToString() + "'>O</span>"%> </b>                               
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
                                                        <%#Eval("StockLocation")%>
                                                    </td>
                                                    <%--<td align="left">
                                                        <%#Eval("CriticalUpdatedBy")%>
                                                    </td>
                                                    <td align="left">
                                                        <%# Common.ToDateString(Eval("CriticalUpdatedByOn"))%> 
                                                    </td>--%>
                                                    <td></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                            </colgroup>
                                    </table>
                                </div>
                                <div style="padding: 5px; background-color:#ffffb3; overflow:auto; height:25px;">
                                <span style="float:left">
                                    <asp:Button ID="btnUpdateCriticalType" runat="server" CssClass="btn" Text="Update Critical" Visible="false" OnClick="btnUpdateCriticalType_OnClick" style="Width:150px"/>
                                    <asp:Button ID="btnModify" Text="Mark Critical Spares" CssClass="btn" runat="server" OnClick="btnModify_Click" Visible="false" Width="150px" />&nbsp;

                                    <asp:Button ID="btnUpdateSpareOR" runat="server" CssClass="btn" Text="Update OR" Visible="false" OnClick="btnUpdateSpareOR_OnClick" style="Width:150px"/>
                                    <asp:Button ID="btnMarkOR" Text="Mark Spares OR" CssClass="btn" runat="server" OnClick="btnMarkOR_Click" Visible="false" Width="150px" />&nbsp;

                                    <asp:Button ID="btnCancelUpdate" Text="Cancel" CssClass="btn" runat="server" OnClick="btnCancelUpdate_Click" Visible="false" />&nbsp;
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Size=Larger ></asp:Label>
                                </span>
                                     
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
                                                                <asp:Button ID="btnCritical" runat="server" Text="Save" OnClick="btnCritical_OnClick" />
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
    </asp:Content>
