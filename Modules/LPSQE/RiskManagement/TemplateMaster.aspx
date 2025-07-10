<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateMaster.aspx.cs" Inherits="EventManagement_TemplateMaster" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="Menu_Event.ascx" TagName="leftmenu" TagPrefix="mtm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
      <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <title>EMANAGER</title> 
    <script type="text/javascript">
    function ShowProgress(ctl) {
        ctl.src = '../../Images/loading.gif';
        }
   </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="font-family:Arial;font-size:12px;">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <div class="text headerband">
             Risk Management
         </div>
        <mtm:leftmenu runat="server" ID="LefuMenu1" />
        
        <div class="box_withpad" style="min-height:450px">
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
                                 <td style="text-align:right; vertical-align:middle;width:100px; font-weight:bold;">Event Name :&nbsp;</td>
                                 <td style="text-align:left;  vertical-align:middle;">
                                    <asp:TextBox runat="server" ID="txtEventName" CssClass="input_box" Width="98%"></asp:TextBox>
                                 </td>
                                 <td style="width:200px;">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"  CssClass="btn" Width="70px"/>
                                    <asp:Button ID="btnAddRisk" runat="server" OnClick="btnAddRisk_Click" Text="New Template" CssClass="btn" Width="115px" />
                                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="" style="display:none;" />
                                 </td>
                                 </tr>
                                 </table>
                            </div>
                            <div class="dvScrollheader">  
                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:50px;" />
                                        <col style="width:200px;" />
                                        <col />                     
                                        <col style="width:100px;" />
                                        <col style="width:150px;" />
                                        <col style="width:100px;" />
                                        <col style="width:110px;" />
                                        <col style="width:20px;" />
                                    </colgroup>
                                    <tr class= "headerstylegrid">
                                        <td style="text-align:center; vertical-align:middle;">View</td>
                                        <td style="text-align:left; vertical-align:middle;">Template Code</td>
                                        <td style="text-align:left;">Event Name</td>
                                        <td style="text-align:left; vertical-align:middle;">Status</td>                                                        
                                        <td style="text-align:left; vertical-align:middle;">Approved By</td>                                                        
                                        <td style="text-align:left;">Approved On</td>
                                        <td style="text-align:center;">Link to vessel</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dvScrolldata" style="height: 400px;">
                            <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:50px;" />
                                    <col style="width:200px;" />
                                    <col />                     
                                    <col style="width:100px;" />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:110px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <asp:Repeater ID="rptTemplate" runat="server">
                                    <ItemTemplate>
                                        <tr >
                                            <td style="text-align:center">
                                                <asp:ImageButton ID="btnView" runat="server" Visible='<%#(Eval("Status").ToString()=="A")%>' CommandArgument='<%#Eval("TemplateId")%>' OnClick="btnView_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View" />
                                                <asp:ImageButton ID="btnEdit" runat="server" Visible='<%#(Eval("Status").ToString()=="P")%>' CommandArgument='<%#Eval("TemplateCode")%>' OnClick="btnEdit_Click" ImageUrl="~/Modules/HRD/Images/12-em-pencil.png" ToolTip="Edit" />
                                                <asp:ImageButton ID="btnHistory" runat="server" CommandArgument='<%#Eval("TemplateCode")%>' OnClick="btnHistory_Click" ImageUrl="~/Modules/HRD/Images/history.png" ToolTip="History" />
                                            </td>
                                            <td align="left"><%#Eval("TemplateCode")%></td>
                                            <td align="left"><%#Eval("EventName")%></td>
                                            <td align="left"><%#((Eval("Status").ToString()=="A")?"Approved":"Not Approved")%></td>
                                            <td align="left"><%#Eval("ApprovedBy")%></td>
                                            <td align="center"><%# Common.ToDateString(Eval("ApprovedOn"))%></td>
                                            <td style="text-align:center">
                                                <asp:ImageButton ID="btnLinkVessel" runat="server" CommandArgument='<%#Eval("TemplateId")%>' OnClick="btnLinkVessel_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" ToolTip="Link to vessel" />
                                            </td>
                                            <td>&nbsp;</td>
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
        <div ID="dv_RiskTopics" runat="server" style="position: absolute; top: 50px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:440px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="text headerband" style='padding:10px 0px 10px  0px'><b>Select Risk Event</b></div>
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
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("EventId")%>' OnClick="btnSelect_Click" ImageUrl="~/Modules/HRD/Images/check.gif" ToolTip="Select" />
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
      </div>       
        <div ID="dv_History" runat="server" style="position: absolute; top: 25px; left: 50px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:25px;left:50px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:80%;  height:500px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="box3" style='padding:10px 0px 10px  0px'><b><asp:Label ID="lblHistoryTital" runat="server"></asp:Label></b></div>                
                   <div class="dvScrollheader">  
                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:30px;" />
                                        <col style="width:100px;" />
                                        <col />                     
                                        <%--<col style="width:100px;" />--%>
                                        <col style="width:150px;" />
                                        <col style="width:100px;" />
                                        <col style="width:20px;" />
                                    </colgroup>
                                    <tr class= "headerstylegrid">
                                        <td style="text-align:center; vertical-align:middle;">View</td>
                                        <td style="text-align:left; vertical-align:middle;">Template Code</td>
                                        <td style="text-align:left; vertical-align:middle;">Event Name</td>
                                        <td style="text-align:left; vertical-align:middle;">Approved By</td>                                                        
                                        <td style="text-align:left;">Approved On</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                   <div class="dvScrolldata" style="height: 400px;">
                            <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:100px;" />
                                    <col />                     
                                    <%--<col style="width:100px;" />--%>
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:20px;" />
                                </colgroup>
                                <asp:Repeater ID="rptHistory" runat="server">
                                    <ItemTemplate>
                                        <tr >
                                            <td style="text-align:center">
                                                <asp:ImageButton ID="btnViewHistoryDetails" runat="server" CommandArgument='<%#Eval("TemplateId")%>' OnClick="btnViewHistoryDetails_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View Details" />
                                            </td>
                                            <td align="left"><%#Eval("TemplateCode")%></td>
                                            <td align="left"><%#Eval("EventName")%></td>
                                            <%--<td align="left"><%#((Eval("Status").ToString()=="H")?"Approved":"Not Approved")%></td>--%>
                                            <td align="left"><%#Eval("ApprovedBy")%></td>
                                            <td align="center"><%# Common.ToDateString(Eval("ApprovedOn"))%></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>                
          </center>
          <div style="padding:3px">
          <asp:Button runat="server" ID="Button1" Text="Close" OnClick="btnClose_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
      </div>

       <div ID="dv_LinkVessel" runat="server" style="position: absolute; top: 25px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:25px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:440px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="text headerband" style='padding:10px 0px 10px  0px'><b>Select Vessel</b></div>
                <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                <input type="checkbox" id='trigger' onclick="CheckUncheck();" />
                
                <input type="text" style='width:90%; padding:4px;' onkeyup="filter1(this);" />
                <asp:HiddenField ID="hfdTemplateId" runat="server" />
                </div>
                <div class="dvScrolldata" style="height: 330px;">
                <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                        <col style="width:50px;" />
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rptVessel" runat="server">
                        <ItemTemplate>
                            <tr class='listitem1'>
                                <td style="text-align:center">
                                    <asp:CheckBox  ID="chkSelect" runat="server" VesselId='<%#Eval("VesselId")%>' Checked='<%#(Eval("Checked").ToString() == "1")%>'  ToolTip="Select" />
                                </td>
                                <td align="left" class='listkey1'><%#Eval("VesselName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
          </center>
          <div style="padding:3px">
          <asp:Button runat="server" ID="btnSaveVesselLinking" Text="Save" OnClick="btnSaveVesselLinking_Click"  style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
          <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnCloseVL_Click" CausesValidation="false" style="  border:solid 1px grey;width:100px;" CssClass="btn"/>
          </div>
          </div>
        </center>
       
        <script type="text/javascript">
            function filter1(ctl) {
                var par = $(ctl).val().toLowerCase();
                $(".listitem1").each(function (i, o) {
                    var txt = $(o).find(".listkey1").first().html().toLowerCase();
                    if (parseInt(txt.search(par)) >= 0) {
                        $(o).css('display', '');
                    }
                    else {
                        $(o).css('display', 'none');
                    }
                });
            }

            function CheckUncheck() {
                $("[name^='rptVessel']").prop("checked", $("#trigger").prop('checked'));
            }
     </script>
      </div> 

    </div>
</asp:Content>
