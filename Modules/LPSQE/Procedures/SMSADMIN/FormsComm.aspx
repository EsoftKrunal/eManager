<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormsComm.aspx.cs" Inherits="FormsComm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <script src="../../js/Common.js" type="text/javascript" language="javascript"></script>
    <script src="../../js/KPIScript.js" type="text/javascript"></script>
<head runat="server">
    <title>EMANAGER </title>
    <%--<link rel="Stylesheet" href="../../CSS/style.css" />--%>
    <link rel="Stylesheet" href="../../../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
            .sbtn
            {
                background-color:White;
                color:Black;
                font-size:11px;
                font-weight:bold;
            }
            .sel_sbtn
            {
                background-color:orange;
                color:White;
                font-weight:bold;
            }
            td
            {
                font-size:12px;
            }
    </style>
    <script type="text/javascript">
        function ConfirmDelete(ctl) {
            if (confirm('Are you sure to delete ?')) {
                ctl.src = '../../Images/loading.gif';
                  return true;
            }
            else {
                return false;
            }
        }
        function SelectAll(ctl) {
            var ctls = document.getElementById("dvscroll_Manuals").getElementsByTagName("input");
            for (i = 0; i <= ctls.length - 1; i++) {
                if (ctls[i].getAttribute("type") == "checkbox") {
                    ctls[i].setAttribute("checked", ctl.getAttribute("checked"));
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
    <div style="font-family:Arial;font-size:12px;">
    <table cellpadding="3" cellspacing="0" border="0" width="100%">
    <tr>
    <td style="text-align:left; width:100px;">Select Vessel :    </td>
    <td style="text-align:left; width:300px;"><asp:DropDownList runat="server" ID="ddlVessel" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_OnSelectIndexChanged" required="yes"></asp:DropDownList>    </td>
    <td > <asp:CheckBox ID="chkAckNotRevieved" runat="server" AutoPostBack="true" OnCheckedChanged="chkAckNotRevieved_OnCheckedChanged" Text="&nbsp;Ack. not recieved"/>   </td>
    
    </tr>
    </table>
    <div>
            <div id="Div1" onscroll="SetScrollPos(this)" style="height:30px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" class='ScrollAutoReset'>   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;height:30px">
                <colgroup>
                    <col style="width:70px;" />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;"/>
                    <col style="width:100px;"/>
                    <col style="width:100px;"/>
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:130px;" />
                    <col style="width:17px;" />
                    <tr align="left" class= "headerstylegrid" >
                        <td style="text-align:left"><input type="checkbox" onclick='SelectAll(this);' />Select</td>
                        <td>FORM #</td>
                        <td>FORM Name</td>
                        <td>Version</td>
                        <td>Release Dt</td>
                        <td>Scheduled</td>
                        <td>Scheduled On</td>
                        <td>Ack. Recd.</td>
                        <td>Ack. Recd. On</td>
                        <td>Cancel Schedule</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            </div>
            <div id="dvscroll_Manuals" onscroll="SetScrollPos(this)" style="height:380px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                    <col style="width:70px;" />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;"/>
                    <col style="width:100px;"/>
                    <col style="width:100px;"/>
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:130px;" />
                    <col style="width:17px;" />
            </colgroup>
            <asp:Repeater ID="rptForms" runat="server">
            <ItemTemplate>
            <tr style='font-size:11px;'>
                    <td style="text-align:left">
                        <asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("MX")%>' />
                    </td>
                    <td style="text-align:left"><%#Eval("FormNo")%></td>
                    <td style="text-align:left"><%#Eval("FormName")%></td>
                    <td><%#Eval("VersionNo")%></td>
                    <td><%#Common.ToDateString(Eval("CreatedOn"))%></td>
                   <td><%#(Eval("Scheduled").ToString()=="True")?"Yes":"No"%></td>
                    <td><%#Common.ToDateString(Eval("ScheduledOn"))%></td>
                    <td><%#(Eval("Ack_Recd").ToString() == "True") ? "Yes" : "No"%></td>
                    <td><%#Common.ToDateString(Eval("Ack_RecdOn"))%></td>
                    <td>
                        <asp:ImageButton ID="btnDelete" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("MX")%>' runat="server" OnClientClick="return ConfirmDelete(this);" CommandName="Delete" ImageUrl="~/Modules/LPSQE/Procedures/Images/delete.png" ToolTip="Edit" Visible='<%#Auth.IsDelete && (Eval("Scheduled").ToString()=="True") %>'/>                                            
                    </td>
                    <td>&nbsp;</td>
             </tr>   
            </ItemTemplate>
            </asp:Repeater>
            </table>
    </div>
    <table cellpadding="0" cellspacing="0" border="1" width="100%">
    <tr>
    <td>
        <asp:Button ID="btnFormComm" runat="server" Text=" Schedule Selected Forms " CssClass="btn" OnClick="btnAssignSelected_OnClick" style="margin:8px; "  OnClientClick="this.value='Processing..';" />
        <asp:Button ID="btnFormComm0" runat="server" CssClass="btn" 
            OnClick="btnSendSelected_OnClick" OnClientClick="this.value='Processing..';" 
            style="margin:8px; " 
            Text=" Send Selected Forms " />


        <asp:Button ID="btnSendRanks" runat="server" CssClass="btn" 
            OnClick="btnSendRanks_OnClick" style="margin:8px; " 
            Text=" Send Ranks " />
    </td>
    </tr>
    </table>
     </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
