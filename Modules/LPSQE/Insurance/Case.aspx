
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Case.aspx.cs" Inherits="Case" Title="Case Management" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <script type="text/javascript" language="javascript">
        function opennewCase(CID) {
            window.open('PopupNewCase.aspx?Mode=E&&CID=0', '');
        }
        function viewcase(CID) {
            window.open('PopupNewCase.aspx?Mode=V&&CID=' + CID, '');
        }
        function editCase(CID) {
            window.open('PopupNewCase.aspx?Mode=E&&CID=' + CID, '');
        }
        function reloadme() {
            //__doPostBack('btnSearch', '');
            document.getElementById('btnSearch').click();
        }
        //--------
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
        function SetLastFocus(ctlid) {
            pos = getCookie(ctlid);
            if (isNaN(pos)) { pos = 0; }
            if (pos > 0) {
                document.getElementById(ctlid).scrollTop = pos;
            }
        }
        function SetScrollPos(ctl) {
            setCookie(ctl.id, ctl.scrollTop, 1);
        }


    </script>
    </head>
    <body style="margin:0 0 0 0">
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
         <ajaxToolkit:ToolkitScriptManager  ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
   <asp:Panel ID="pnl_Search" DefaultButton="btnSearch" runat="server">
    
    
        <table border="0" style="background-color: #f9f9f9; border: #8fafdb 1px solid; border-top: #8fafdb 0px solid;" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >                  
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                        <td style=" padding :5px" >
                           <%-- <fieldset>
                                <legend>Search</legend>--%>
                                <table style="width:100%;border:solid 1px #8fafdb;" >
                                    <tr >
                                        <td style="text-align:left;width:220px;" >
                                           <table style="width:100%;">
                                             <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="vertical-align:top">
                                                                <%--<asp:CheckBox ID="chkAllGroups" Text="All Groups" runat="server" 
                                                                    AutoPostBack="True" oncheckedchanged="chkAllGroups_CheckedChanged" />--%>
                                                                <asp:CheckBoxList ID="chkAllGroups" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkallgp1"><asp:ListItem>All</asp:ListItem></asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align:top">
                                                                <div style="overflow-x: hidden; overflow-y: scroll; border: solid 1px #8fafdb; height: 105px;text-align: left; width:120px;">
                                                                <asp:CheckBoxList ID="chkGroups" runat="server" Width="90px" AutoPostBack="True" onselectedindexchanged="chkGroups_SelectedIndexChanged" ></asp:CheckBoxList> 
                                                                </div>   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                               <td>
                                                   <table style="width: 100%;">
                                                        <tr>
                                                            <td style="vertical-align:top; text-align:left">
                                                                
                                                                <asp:CheckBoxList ID="chkAllSubGroups" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chakallinsp" >
                                                                    <asp:ListItem>All</asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align:top">
                                                               <div style="overflow-x: hidden; overflow-y: scroll; border: solid 1px #8fafdb; height: 105px;width: 120px;">
                                                                <asp:CheckBoxList ID="chkSubGroups" runat="server" Width="100px"></asp:CheckBoxList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                   
                                                </td>
                                             </tr>
                                           </table>
                                            
                                        </td>
                                        <td style=" text-align:left; vertical-align:top; padding-left:20px;" >
                                           <table style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2" style="text-align:center; padding-right:150px;">
                                                                <asp:RadioButtonList ID="rdoOwnerORFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoOwnerORFleet_OnSelectedIndexChanged"  RepeatDirection="Horizontal" style="width:160px;" >
                                                                    <asp:ListItem Text="Fleet" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Owner" Value="2" ></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>                                                            
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                                <asp:Label ID="lblOwnerORFleet" runat="server" Text="Fleet :" ></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="170px" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged" >
                                                                    <asp:ListItem Text="< All >" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Fleet 1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Fleet 2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Fleet 3" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlOwner" runat="server" CssClass="input_box" Width="170px" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlOwner_OnSelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                                Vessel :&nbsp 
                                                            </td>
                                                            <td><asp:DropDownList ID="ddlvessels" runat="server" Width="170px" CssClass="input_box"></asp:DropDownList>&nbsp;<asp:CheckBox 
                                                                    ID="chkInactiveVessels" Text="Inactive Vessel" runat="server" 
                                                                    AutoPostBack="True" oncheckedchanged="chkInactiveVessels_CheckedChanged" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                               U/W :&nbsp;                                                                
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAllUW" runat="server" Width="170px" CssClass="input_box"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                               U/W Case# :&nbsp;                                                                
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ddlRDC" runat="server" Width="150px" CssClass="input_box">
                                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="EXCL" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="1/4" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="4/4" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                                <asp:TextBox ID="txtCaseNo" runat="server" CssClass="input_box" Width="165px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right;">
                                                               Company Case # :&nbsp;
                                                           </td>
                                                           <td>
                                                              <asp:TextBox ID="txtMTMCaseNo" runat="server" CssClass="input_box" Width="165px"></asp:TextBox>
                                                           </td>
                                                        </tr>
                                                    </table>
                                        </td>
                                        <td style="text-align:left; width:310px; vertical-align:top;" >
                                           <table  border="0" width="350px" >
                                                        <tr>
                                                            <td colspan="4" style="text-align:center;">
                                                                <b>Cases By Period   </b> &nbsp 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                                From Dt :
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtFromDt" runat="server" Width="75px" CssClass="input_box" ></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                            </td>
                                                            <td style="text-align:right;">
                                                                To Dt :
                                                            </td>
                                                            <td>&nbsp;<asp:TextBox ID="txtToDt" runat="server" Width="75px" CssClass="input_box" ></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                               Case Status :                                                               
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="ddlpolicyStatus" runat="server" CssClass="input_box">
                                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Closed" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td>
                                                               
                                                           </td>
                                                           <td>
                                                           </td>
                                                        </tr>
                                                        <tr>
                                                           <td colspan="2" style="text-align:center;">
                                                                
                                                            </td>
                                                                   
                                                           
                                                        </tr>
                                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                           PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtFromDt">
                                                       </ajaxToolkit:CalendarExtender>
                                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                           PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtToDt">
                                                       </ajaxToolkit:CalendarExtender>
                                                    </table>
                                        
                                        
                                        </td>
                                    </tr>
                                    
                                </table>
                            <%--</fieldset>--%> 
                         </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <table border="0px" cellpadding="0" cellspacing="0" 
                                    style="width: 100%; padding-top: 5px; padding-bottom: 5px">
                                    <tr>
                                        <td style="text-align: center; padding-right: 5px; padding-left: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="padding-right: 8px; text-align: left">
                                                        <asp:Label ID="Label2" runat="server" Visible="False">Total Records Found:
                                                        </asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblrecord" runat="server"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblmessage"  Font-Bold="true" runat="server" ForeColor="#C00000"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">                                                   
                                                    </td>
                                                    <td style="text-align: right">
                                                        <%--<asp:Button ID="btnNewPolicy" runat="server" CssClass="btn" Text="New Case" 
                                                            Width="100px" onclick="btnNewPolicy_Click" />&nbsp;
                                                            <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" Width="100px" />--%>
                                                            <asp:Button ID="btnSearch" Text="Search" runat="server" Width="70px" CssClass="btn" onclick="btnSearch_Click" />&nbsp;
                                                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="70px" CssClass="btn" onclick="btnClear_Click" />&nbsp;                                                                   
                                                            <asp:Button ID="btnNewPolicy" runat="server" CssClass="btn" Text="New Case" Width="70px" onclick="btnNewPolicy_Click" />&nbsp;
                                                            <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" Width="80px" OnClick="btnPrint_Click" Visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <div id="Div1"  style="width: 100%; border:solid 1px #8fafdb; height: 25px; font-weight:bold; text-align: center; overflow-X:hidden; overflow-Y:scroll; background:#c2c2c2;"  onscroll="SetScrollPos(this)">
                                <table border="0" cellpadding="2" cellspacing="0" width="100%" rules="rows" style="border-collapse:collapse">
                                <colgroup>
                                <col style="width:30px" />
                                <col style="width:30px" />
                                <col />
                                <col style="width:150px" />
                                <col style="width:100px" />
                                <col style="width:130px" />
                                <col style="width:130px" />
                                <col style="width:80px" />
                                <col style="width:150px" />
                                <col style="width:150px" />
                                <col style="width:60px" />
                                <col style="width:17px" />
                                </colgroup>
                                <tr onmouseover="" class= "headerstylegrid">
                                    <td style='text-align:center;'>View</td>
                                    <td style='text-align:center;'>Edit</td>
                                    <td style='text-align:left;'>VSL Name</td>
                                    <td style='text-align:left; '>INSC Type</td>
                                    <td style='text-align:left; '>U/W Name</td>
                                    <td style='text-align:left; '>Company Case#</td>
                                    <td style='text-align:left; '>U/W Case#</td>
                                    <td style='text-align:left; '>Incident Dt.</td>
                                    <td style='text-align:left; '>Total Claimed Amt</td>
                                    <td style='text-align:left; '>Toal Recieved Amt</td>
                                    <td style='text-align:left; '>Status</td>
                                    <td></td>
                                </tr>
                                </table>
                                </div>
                                <div id="dvscroll_OV"  style="width: 100%; border:solid 1px #8fafdb; height: 230px; text-align: center; overflow-X:hidden; overflow-Y:scroll;"  onscroll="SetScrollPos(this)">
                                <table border="0" cellpadding="2" cellspacing="0" width="100%" rules="rows" style="border-collapse:collapse">
                               <colgroup>
                                <col style="width:30px" />
                                <col style="width:30px" />
                                <col />
                                <col style="width:150px" />
                                <col style="width:100px" />
                                <col style="width:130px" />
                                <col style="width:130px" />
                                <col style="width:80px" />
                                <col style="width:150px" />
                                <col style="width:150px" />
                                <col style="width:60px" />
                                <col style="width:17px" />
                                </colgroup>
                                <asp:Repeater runat="server" id="rptCases">
                                <ItemTemplate>
                                <tr onmouseover="">
                                    <td style='text-align:center;'><a onclick='viewcase(<%#Eval("CaseID") %>)' href="#"><img style="border:none" src="../../HRD/Images/HourGlass.png" /></a>
                                    <td style='text-align:center;'><a onclick='editCase(<%#Eval("CaseID") %>)' href="#"><img style="border:none" src="../../HRD/Images/edit.jpg" /></a>
                                    <td style='text-align:left;'><%#Eval("VesselName")%></td>
                                    <td style='text-align:left; '><%#Eval("GroupName")%></td>
                                    <td style='text-align:left; '><%#Eval("ShortName")%></td>
                                    <td style='text-align:left; '><%#Eval("CompanyCaseNumber")%></td>
                                    <td style='text-align:left; '><%#Eval("CaseNumber")%></td>
                                    <td style='text-align:left; '><%#Eval("IncidentDate1")%></td>
                                    <td style='text-align:left; '><%#Eval("TotClaimedAmount")%></td>
                                    <td style='text-align:left; '><%#Eval("RecoveredAmount")%></td>
                                    <td style='text-align:left; '><%#Eval("CaseStatus")%></td>
                                    <td></td>
                                </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </table>
                                </div>
                            </td>                            
                        </tr>
                        
                    </table>
                </td>
            </tr>
        </table>         
    </asp:Panel>
</form>
</body>
    

</html>

