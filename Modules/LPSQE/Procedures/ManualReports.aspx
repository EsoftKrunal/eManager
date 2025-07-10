<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualReports.aspx.cs" Inherits="ManualReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="SMSSubTab.ascx" tagname="SMSSubTab" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
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
                background-color:#4371A5;
                color:White;
                font-weight:bold;
            }
    </style>
    <script type="text/javascript">
        function checkall(ctl) {
            var els = document.getElementById("d1").getElementsByTagName("input");
            for(i=0;i<=els.length-1;i++) {
                els[i].checked = ctl.checked;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
           <%-- <uc3:SMSManualMenu ID="ManualMenu2" runat="server" />--%>
    <uc4:SMSSubTab ID="SMSManualMenu1" runat="server" />
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
    <td style="width:375px;vertical-align:top;border:solid 1px black; border-top:none; overflow:hidden;">
        <div style="width:375px; height:455px;">
        <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" >             
            <tr>
                <td>
                    <div style="width:100%; border:solid 1px gray; border-right:none; border-top:none;width:375px;  height:22px;overflow-x:hidden; overflow-y:hidden; " class= "headerstylegrid">
                        <div style="padding-top:0px;float:left;padding-left:5px;width:200px; ">
                        <%--<input type="checkbox" onclick="checkAll(this)" id="chall" visible="false" runat="server" style="float:left"/>--%>
                        <input type="checkbox" id="chall" onclick='checkall(this);' runat="server" style="float:left"/>
                        <div style="padding-top:2px">&nbsp;<asp:DropDownList ID="ddlManualCategories" Width="150px" runat="server" AutoPostBack="True" onselectedindexchanged="ddlManualCategories_SelectedIndexChanged"></asp:DropDownList> </div>
                        
                        </div>                        
                    <span style="float:right;padding-right:20px; padding-top:3px;">VersionNo</span>
                    </div>

                    <div style="width:375px; height:431px;border:solid 1px gray;border-right:none;overflow-x:hidden; overflow-y:scroll;" id="d1">
                    <div style="margin:5px">
                        <asp:Repeater runat="server" ID="rptManuals">
                        <ItemTemplate>
                          <div title='<%#Eval("ManualName")%>' style="width:375px; height:18px; overflow:hidden;border-bottom:dotted 1px #4371a5;">
                              <div style="float:left; width:20px; clear:left;height:18px; ">
                                <%--<asp:CheckBox runat="server" id="chkSelect" CommandArgument='<%#Eval("ManualId")%>' Visible="<%#ShowSearch%>"/>--%>
                                <asp:CheckBox runat="server" id="chkSelect" CommandArgument='<%#Eval("ManualId")%>' />
                              </div>
                              <div style="float:left; width:260px; height:18px; padding-top:3px; padding-left:2px;">
                                <%--<asp:LinkButton runat="server" ID="lnkManual" CommandArgument='<%#Eval("ManualId")%>' Text='<%#Eval("ManualName")%>' ></asp:LinkButton>--%>
                                <%#Eval("ManualName")%>
                              </div>
                              <div style="float:left; width:50px; text-align:right;">
                                <%#Eval("VersionNo")%>
                              </div>
                          </div>
                        </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                </td>
            </tr>
            </table>

        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        </td>
        <td style="vertical-align:top; border:solid 1px black; border-top:none;">
        <div style="padding:4px; text-align:center;background-color:#c2c2c2; color:White;">
            From Date :
            <asp:TextBox id="txt_FromDt" runat="server" Width="80px" CssClass="input_box" MaxLength="11"></asp:TextBox>&nbsp;
            <asp:ImageButton id="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
            To Date :
            <asp:TextBox id="txt_ToDt" runat="server" Width="80px" CssClass="input_box" MaxLength="11"></asp:TextBox>&nbsp;
            <asp:ImageButton id="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
            &nbsp;
            <asp:Button id="btn_Report" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Report_Click" style="padding:1px; width:150px;"></asp:Button>
        </div>
        <%--<div style="height:25px;overflow-x:hidden;">
        <table cellpadding="3" cellspacing="0" width='100%' border='1' style='border-collapse:collapse' bordercolor="grey">
        <thead>
            <tr>
                <td style="width:50px">View</td>
                <td style="text-align:left">Manual Name</td>
                <td style="text-align:left">Section</td> 
            </tr>
        </thead>
        </table>
        </div>--%>
        <div style="padding:2px; text-align:center;">
            <div style="width:375px; height:431px;border:solid 1px gray;border-right:none;overflow-x:hidden; overflow-y:scroll; width:100%">
            <table cellpadding="3" cellspacing="0" width='100%' border='0' style='border-collapse:collapse' bordercolor="grey">
            <asp:Repeater runat="server" ID="rptManualslist">
            <ItemTemplate>
            <tr>
            <td>
            <div style=" background-color:#6699FF; width:100%; padding:3px; color:White; text-align:left">
                <%#Eval("ManualName") %>
            </div>
            </td>
            </tr>
            <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width='100%' border='1' style='border-collapse:collapse' bordercolor="grey">
                <tr style=" background-color:#C2E0FF; color:Black;">
                <td style="width:50px">View</td>
                <td style="text-align:center;width:90px">Section</td>
                <td style="text-align:left">Heading</td> 
                <td style="text-align:center;width:90px">Revision#</td> 
                <td style="width:90px">Approved On</td> 
                </tr>
                </table>
                <table cellpadding="3" cellspacing="0" width='100%' border='1' style='border-collapse:collapse' bordercolor="grey">
                <asp:Repeater runat="server" ID="rptManualSections" DataSource='<%#getManualChanges(Eval("ManualId"))%>'>
                <ItemTemplate>
                    <tr>
                        <td style="width:50px"><a href='ReadManualSection1.aspx?ManualId=<%#Eval("ManualId") %>&SectionId=<%#Eval("Section") %>' target="_blank" > <img src="../Images/HourGlass.png" /> </a></td>
                        <td style="width:90px"><%#Eval("Section")%></td>
                        <td style="text-align:left"><%#Eval("Heading")%></td>
                        <td style="width:90px"><%#Eval("Revision#")%></td>
                        <td style="width:90px"><%#Common.ToDateString(Eval("Approved On"))%></td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>
            </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
        </div>
        </td>
    </tr>
    </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txt_FromDt"></ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_ToDt"></ajaxToolkit:CalendarExtender>
    </form>
</body>
</html>
