<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiskAssessment.aspx.cs" Inherits="RiskManagement_RiskAnalysis" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/LPSQE/RiskManagement/Menu_Event.ascx" TagName="leftmenu" TagPrefix="mtm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


  <%--  <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <title>EMANAGER</title>
    <script type="text/javascript" language="javascript">
        function openRiskWindow(EventId) {
            window.open('AddRisk.aspx?EventId=' + EventId, '_blank', '', true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">

    <div style="font-family:Arial;font-size:12px;">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr><td>
            <div class="text headerband"> 
                Risk Assessment
            </div>
            </td></tr>
        </table>
        <div class="box_withpad" style="min-height:450px">
        <mtm:leftmenu runat="server" ID="LefuMenu1" />
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        <td style=" text-align :left; vertical-align : top;"> 
            <div>
            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px">
            <tr>  
            <td>
             <div style="border:none;">
                <div class="box1">  
                     <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                     <tr>
                     <td style="text-align:right; vertical-align:middle;">Vessel :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlVessel" runat="server" Width="200px" /></td>
                     <%--<td style="text-align:right; vertical-align:middle;">Office :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlOffice" runat="server" Width="150px" /></td>--%>
                     <td style="text-align:right; vertical-align:middle;">Period :&nbsp;</td>
                     <td style="text-align:left; width:90px; vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtEventDate" CssClass="input_box" MaxLength="15" Width="85px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEventDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                     </td>
                     <td style="text-align:right;width:90px; vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtEventDate1" CssClass="input_box" MaxLength="15" Width="85px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEventDate1" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                     </td>
                     <td style="text-align:right; vertical-align:middle;">Status :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                     <asp:ListItem Text="All" Value=""></asp:ListItem>
                     <asp:ListItem Text="Open" Value="O" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="Closed" Value="C"></asp:ListItem>
                     </asp:DropDownList>
                     </td>
                     <td>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn" />
                        <asp:Button runat="server" ID="btnRefresh" Text="" onclick="btnRefresh_Click" style="display:none;" />
                        <%--<asp:Button ID="btnAddRisk" runat="server" OnClick="btnAddRisk_Click" Text="New RCA" />--%>
                     </td>
                     </tr>
                     </table>
                </div>
                <div class="dvScrollheader">  
                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:50px;" />
                            <col style="width:150px;" />
                            <col style="width:125px;" />
                            <col style="width:100px;" />
                            <col />                     
                            <col style="width:250px;" />
                            <col style="width:80px;" />
                            <col style="width:80px;" />
                            <col style="width:100px;" />
                            <col style="width:20px;" />
                       
                        <tr class= "headerstylegrid">
                            <td style="width:50px;"><b>View</b></td>
                            <td style="width:150px;color:White;text-align:center;"><b>Vessel Name</b></td>
                            <td style="width:125px;color:White;text-align:center;"><b>RA#</b></td>
                            <td style="width:100px;color:White;text-align:center;"><b>Event Date</b></td>
                            <td style="color:White;text-align:left;"><b>Event Name</b></td>
                            <td style="width:250px;color:White;text-align:left;"><b>HOD Name/ Position</b></td>
                            <td style="width:80px;color:White;text-align:center;"><b>Status</b></td>
                            <td style="width:80px;color:White;text-align:center;"><b>Recd On</b></td>
                            <td style="width:100px;color:White;text-align:center;"><b>Export to ship</b></td>
                            <td style="width:20px;"><b>&nbsp;</b></td>
                        </tr>
                             </colgroup>
                    </table>
                </div>
                <div class="dvScrolldata" style="height: 400px;">
                    <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                         <colgroup>
                            <col style="width:50px;" />
                            <col style="width:150px;" />
                            <col style="width:125px;" />
                            <col style="width:100px;" />
                            <col />                     
                            <col style="width:250px;" />
                            <col style="width:80px;" />
                            <col style="width:80px;" />
                            <col style="width:100px;" />
                            <col style="width:20px;" />
                       
                       </colgroup>
                       
                       
                       
                        <asp:Repeater ID="rptRisks" runat="server">
                            <ItemTemplate>
                                <tr>
                                      <td style="width:50px;text-align:center;">
                                        <asp:ImageButton runat="server" ID="btnView" VesselCode='<%#Eval("VesselCode")%>' ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="btnView_Click" ToolTip="View" CommandArgument='<%#Eval("RiskId")%>' />
                                      </td>
                                      <td style="width:150px;text-align:left;"><%#Eval("VesselName")%></td>
                                      <td style="width:125px;text-align:center;"><%#Eval("REFNO")%></td>
                                      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("EVENTDATE"))%></td>
                                      <td style="text-align:left;"><%#Eval("EVENTNAME")%></td>
                                      <td style="width:250px;text-align:left;"><%#Eval("HOD_POSITION")%></td>
                                      <td style="width:80px;text-align:center;">
                                        <img id="Img1" title="Office Comments received" alt="" src="~/Modules/HRD/Images/green_circle.gif" runat="server" visible='<%#(Eval("O_Verified").ToString()=="Y")%>' />
                                        <img id="Img2" alt="" src="~/Modules/HRD/Images/red_circle.png" title="Office Comments not received" runat="server" visible='<%#(Eval("O_Verified").ToString()=="N")%>' />
                                      </td>
                                      <td style="width:80px;text-align:center;"><%#Common.ToDateString(Eval("OfficeRecdOn"))%></td>
                                      <td style="width:100px;text-align:center;">
                                      <asp:ImageButton runat="server" ID="btnExport" VesselCode='<%#Eval("VesselCode")%>' ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="btnExport_Click" visible='<%#(Eval("Verify_Needed").ToString()=="Y")%>'  ToolTip="Export to ship" CommandArgument='<%#Eval("RiskId")%>'/>
                                      </td>
                                      <td style="width:20px;"><b>&nbsp;</b></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                             
                             
                    </table>
                </div>

             </div>
            </td>
            </tr>
            </table>
            </div>
        </td>
        </tr>
        </table>
        </div>

        <%--<div ID="dv_RiskTopics" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:440px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="box3" style='padding:10px 0px 10px  0px'><b>Select Risk Topic</b></div>
                <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                <input type="text" style='width:90%; padding:4px;' onkeyup="filter(this);" />
                </div>
                <div class="dvScrolldata" style="height: 330px;">
                <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                        <col style="width:50px;" />
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rpt_Events" runat="server">
                        <ItemTemplate>
                            <tr class='listitem'>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("EventId")%>' OnClick="btnSelect_Click" ImageUrl="~/Images/check.gif" ToolTip="Select" />
                                </td>
                                <td align="left" class='listkey'><%#Eval("EventName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
          </center>
          <div style="padding:3px">
          <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancelNew_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
       
        <script type="text/javascript">
            function filter(ctl) {
                var par = $(ctl).val().toLowerCase();
                $(".listitem").each(function (i, o) {
                    var txt = $(o).find(".listkey").first().html().toLowerCase();
                    if (parseInt(txt.search(par)) >= 0) {
                        $(o).css('display', '');
                    }
                    else {
                        $(o).css('display', 'none');
                    }
                });
            }
     </script>
      </div>--%>
    </div>
</asp:Content>
