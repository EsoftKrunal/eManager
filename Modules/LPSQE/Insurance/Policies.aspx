<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Policies.aspx.cs" Inherits="InsuranceRecordManagement_Policies" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
        function opennewpolicy(Mode, PId) {
            window.open('PopupNewPolicy.aspx?Mode=' + Mode + '&&PId=' + PId, '');
        }
        function reloadme() {
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:Panel ID="pnl_Search" DefaultButton="btnSearch" runat="server">
    
        <table border="0" style="background-color: #f9f9f9; border: #8fafdb 1px solid; border-top: #8fafdb 0px solid;" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >                    
                    <table border="1" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td style=" padding :5px" >
                                <table style="border:solid 1px #8fafdb" width="100%">
                                    <tr>
                                        <td style="text-align:left;width:220px;" >
                                           <table style="width:100%;">
                                             <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="vertical-align:top">
                                                                <asp:CheckBox ID="chkAllGroups" runat="server" AutoPostBack="True" OnCheckedChanged="chkallgp1" Text="All" /></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align:top">
                                                                <div style="overflow-x: hidden; overflow-y: scroll; border: solid 1px #8fafdb; height: 105px;text-align: left;">
                                                                <asp:CheckBoxList ID="chkGroups" runat="server" Width="90px" ></asp:CheckBoxList> 
                                                                </div>   
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             </tr>
                                           </table>
                                        </td>
                                        <td style=" text-align:left; vertical-align:top;" >
                                           <table style="width: 100%;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                                Owner :&nbsp 
                                                            </td>
                                                            <td><asp:DropDownList ID="ddlOwner" AutoPostBack="true" runat="server" 
                                                                    Width="150px" CssClass="input_box" 
                                                                    onselectedindexchanged="ddlOwner_SelectedIndexChanged"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                                Vessel :&nbsp 
                                                            </td>
                                                            <td><asp:DropDownList ID="ddlvessels" runat="server" Width="130px" CssClass="input_box"></asp:DropDownList>&nbsp;<asp:CheckBox 
                                                                    ID="chkInactiveVessels" Text="Inactive Vessel" runat="server" 
                                                                    AutoPostBack="True" oncheckedchanged="chkInactiveVessels_CheckedChanged" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                               U/W :&nbsp;                                                                
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAllUW" runat="server" Width="150px" CssClass="input_box"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;">
                                                               RDC :&nbsp;                                                                
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRDC" runat="server" Width="150px" CssClass="input_box">
                                                                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="EXCL" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="1/4" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="4/4" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right;">
                                                               Policy # :&nbsp;
                                                           </td>
                                                           <td>
                                                              <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="input_box" Width="146px"></asp:TextBox>
                                                           </td>
                                                        </tr>
                                                    </table>
                                        </td>
                                        <td style=" text-align:left; width:310px; vertical-align:top;" >
                                           <table style="width: 410px;" border="0">
                                                        <colgroup>
                                                            <col width="120px" />
                                                            <col width="130px" />
                                                            <col />
                                                            <col width="150px" />
                                                            <tr>
                                                                <td colspan="4" style="text-align:center;">
                                                                    <b></b>&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:right;">
                                                                    Policy Status :&nbsp;
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:DropDownList ID="ddlpolicyStatus" runat="server" CssClass="input_box">
                                                                        <asp:ListItem Selected="True" Text="All" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Expired" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    Premium Due From :
                                                                    <asp:TextBox ID="txtPremFromDt" runat="server" CssClass="input_box" 
                                                                        Width="80px"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgPremFrom" runat="server" CausesValidation="False" 
                                                                        ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                    <%--<asp:CheckBox ID="chkOverdue" Text="Over Due" runat="server" />
                                                                <asp:CheckBox ID="chkDue" Text="Due" runat="server" AutoPostBack="true" oncheckedchanged="chkDue_CheckedChanged" /> --%>
                                                                </td>
                                                                <td>
                                                                    To : &nbsp;
                                                                    <asp:TextBox ID="txtPremToDt" runat="server" CssClass="input_box" Width="80px"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgPremTo" runat="server" CausesValidation="False" 
                                                                        ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Pay by Company :</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPayByMTM" runat="server" CssClass="input_box" 
                                                                        Width="120px">
                                                                        <asp:ListItem Selected="True" Text=" All " Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text=" Yes " Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text=" No " Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <%--<tr>
                                                            <td style="text-align:right;">
                                                                Days :&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDueDays" runat="server" Width="38px" CssClass="input_box" style="margin-left:5px;"></asp:TextBox>&nbsp;Days
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>--%>
                                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                           PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtFromDt">
                                                       </ajaxToolkit:CalendarExtender>
                                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                           PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtToDt">
                                                       </ajaxToolkit:CalendarExtender>--%>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                                Format="dd-MMM-yyyy" PopupButtonID="imgPremFrom" PopupPosition="TopRight" 
                                                                TargetControlID="txtPremFromDt">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                Format="dd-MMM-yyyy" PopupButtonID="imgPremTo" PopupPosition="TopRight" 
                                                                TargetControlID="txtPremToDt">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </colgroup>
                                                    </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <table border="0" cellpadding="1" cellspacing="1" width="100%" >
                                    <tr>
                                        <td style="padding-right: 8px; text-align: left">
                                            <asp:Label ID="Label2" runat="server" Visible="False">Total Records Found:</asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblrecord" runat="server"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblmessage" Font-Bold="true" runat="server" ForeColor="#C00000"></asp:Label>
                                        </td>
                                        <td style="text-align: right; padding-right:4px;">
                                            <asp:Button ID="btnSearch" Text="Search" runat="server" Width="70px" CssClass="btn" onclick="btnSearch_Click" />&nbsp;
                                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="70px" CssClass="btn" onclick="btnClear_Click" />&nbsp;                                                                  
                                            <asp:Button ID="btnNewPolicy" runat="server" CssClass="btn" Text="New Policy" Width="80px" onclick="btnNewPolicy_Click" />&nbsp;
                                            <asp:Button ID="btnPrint" runat="server" CssClass="btn" Text="Print" Width="80px" OnClick="btnPrint_OnClick" />      
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <%--<div id="dvscroll_OV"  style="width: 100%; border:solid 1px #8fafdb; height: 250px; text-align: center; overflow-X:hidden; overflow-Y:scroll;"  onscroll="SetScrollPos(this)">
                                            <asp:GridView ID="GrdSearch" runat="server" AutoGenerateColumns="False" 
                                                GridLines="Horizontal" HeaderStyle-Wrap="false" style="TEXT-ALIGN: center" 
                                                Width="100%" onrowcommand="GrdSearch_RowCommand" 
                                                onrowediting="GrdSearch_RowEditing">
                                                <FooterStyle HorizontalAlign="Center" />
                                                <RowStyle CssClass="rowstyle" />
                                                <Columns>
                                                <asp:TemplateField HeaderText="View">
                                                 <ItemTemplate>
                                                         <asp:ImageButton ID="imgView" runat="server" CausesValidation="False" CommandArgument='<%#Eval("PolicyId") %>'  CommandName="View" ImageUrl="~/Images/HourGlass.gif" ToolTip="View" />
                                                 </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                          <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%#Eval("PolicyId") %>' CommandName="Edit" ImageUrl="~/Images/edit.jpg" ToolTip="Edit" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" Wrap="false"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VesselName" HeaderText="Vsl Name">
                                                       <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GroupName" HeaderText="INSC Type">
                                                       <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="UW" HeaderText="U/W Name" >
                                                 <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                </asp:BoundField>
                                                    <asp:BoundField DataField="PolicyNo" HeaderText="Policy Number">
                                                       <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ExpiryDt" HeaderText="Expiry Dt." >
                                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    </asp:BoundField>                                                    
                                                    <asp:BoundField DataField="PaymentByMTM" HeaderText="MTM Pay" >
                                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                    </asp:BoundField>                                                    
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheader" ForeColor="#0E64A0" Wrap="false" />
                                            </asp:GridView>
                                </div>--%>

                                <div id="Div1"  style="width: 100%; border:solid 1px #8fafdb; height: 25px; font-weight:bold; text-align: center; overflow-X:hidden; overflow-Y:scroll; background:#c2c2c2;"  onscroll="SetScrollPos(this)">
                                <table border="1" cellpadding="2" cellspacing="0" width="100%" rules="rows" style="border-collapse:collapse">
                                <colgroup>
                                <col style="width:30px" />
                                <col style="width:30px" />
                                <col />
                                <col style="width:100px" />
                                <col style="width:100px" />
                                <col style="width:250px" />
                                <col style="width:100px" />
                                <col style="width:100px" />
                                <col style="width:17px" />
                                </colgroup>
                                <tr onmouseover="" class= "headerstylegrid">
                                    <td style='text-align:center;'>View</td>
                                    <td style='text-align:center;'>Edit</td>
                                    <td style='text-align:left;'>VSL Name</td>
                                    <td style='text-align:left; '>INSC Type</td>
                                    <td style='text-align:left; '>U/W Name</td>
                                    <td style='text-align:left; '>Policy #</td>
                                    <td style='text-align:left; '>Expiry Dt.</td>
                                    <td style='text-align:left; '>Company Pay</td>
                                    <td></td>
                                </tr>
                                </table>
                                </div>
                                <div id="dvscroll_OV"  style="width: 100%; border:solid 1px #8fafdb; height: 235px; text-align: center; overflow-X:hidden; overflow-Y:scroll;" onscroll="SetScrollPos(this)">
                                <table border="1" cellpadding="2" cellspacing="0" width="100%" rules="rows" style="border-collapse:collapse">
                               <colgroup>
                                <col style="width:30px" />
                                <col style="width:30px" />
                                <col />
                                <col style="width:100px" />
                                <col style="width:100px" />
                                <col style="width:250px" />
                                <col style="width:100px" />
                                <col style="width:100px" />
                                <col style="width:17px" />
                                </colgroup>
                                <asp:Repeater runat="server" id="rptPolicies">
                                <ItemTemplate>
                                <tr onmouseover="">
                                    <td style='text-align:center;'><a onclick='opennewpolicy("V",<%#Eval("PolicyId") %>)' href="#"><img style="border:none" src="../../HRD/Images/HourGlass.png" /></a>
                                    <td style='text-align:center;'><a onclick='opennewpolicy("E",<%#Eval("PolicyId") %>)' href="#"><img style="border:none" src="../../HRD/Images/edit.jpg" /></a>
                                    <td style='text-align:left;'><%#Eval("VesselName")%></td>
                                    <td style='text-align:left; '><%#Eval("GroupName")%></td>
                                    <td style='text-align:left; '><%#Eval("UW")%></td>
                                    <td style='text-align:left; '><%#Eval("PolicyNo")%></td>
                                    <td style='text-align:left; '><%#Eval("ExpiryDt")%></td>
                                    <td style='text-align:left; '><%#Eval("PaymentByMTM")%></td>
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


