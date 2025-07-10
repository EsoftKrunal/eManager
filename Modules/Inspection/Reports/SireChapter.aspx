<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SireChapter.aspx.cs" Inherits="Reports_SireChapter" Title="Audit Trend Analysis Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Accident Report</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
<form id="form1" runat="server">
<div>
<ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
        <td style="text-align: left">
        <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
            <colgroup>
                <col width='120px' />
                <col/>
                <col width='80px' />
                <col width='60px' />
                <col width='17px' />
            </colgroup>
            <tr class="headerstyle" style="font-weight:bold;" >
                <td style="text-align:center;" >Source</td>
                <td>&nbsp;Observation</td>
                <td style="text-align:center;" >Sire Chap.</td>
                <td style="text-align:center;" >Status</td>
                <td style="text-align:center;" >&nbsp;</td>
            </tr>
            </table>
            <table cellpadding="0" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <colgroup>
                <col width='120px' />
                <col/>
                <col width='80px' />
                <col width='60px' />
                <col width='17px' />
            </colgroup>
            <asp:Repeater runat="server" ID="rptList1">
            <ItemTemplate>
                <tr>
                    <td style="text-align:left;" >&nbsp;<%#Eval("InspectionNo").ToString()%></td>
                    <td>
                    &nbsp;<%#Eval("Deficiency").ToString()%>
                    <span style='color:Red'><i><%#Eval("CausesList").ToString()%></i></span>
                    </td>
                    <td style="text-align:center;" >
                        <asp:ImageButton runat="server" ID="btnRC"  Visible='<%#(Eval("Status").ToString()=="O")%>' CssClass='<%#Eval("InspectionDueId").ToString() + "|" + Eval("Id").ToString() + "|" + Eval("TableName").ToString()%>' ImageUrl="~/Images/arrow_right.png" OnClick="Select_SireChapter" />
                    </td>
                    <td style="text-align:center;" >
                        <asp:Label ID="Label1" runat="server" Text="Closed" Visible='<%#(Eval("Status").ToString()=="C")%>' ForeColor="Green"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="Open" Visible='<%#(Eval("Status").ToString()=="O")%>' ForeColor="Red"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
                                        
            </table>
                                    
        </td>
    </tr>
</table>
<div style=" height:18px; text-align:right; padding:5px; background-color:#eeeeee; border-top:solid 1px #c2c2c2;position:fixed;bottom:0px; left:0px; width:100%;">
    <asp:Label runat="server" ID="lblRowsCount1" ForeColor="Red" style="float:left" onclick='CallAfterRefresh();'></asp:Label>
</div>
<div style="position:absolute;top:0px;left:0px; width:100%;" id="dvSireChapter" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:400px;  height:350px;padding :5px; text-align :center;background : white; z-index:150;top:25px; border:solid 0px black;  ">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <center >
            <div style="padding:6px; background-color:#D6EBFF; font-size:14px;"><b>Select Sire Chapters</b></div>
            <div style="height:312px; margin-top:5px;">
            <div style="height:282px; margin-top:5px; overflow-x:hidden; overflow-y:scroll;">
            <table cellpadding="0" cellspacing="0" border="0" width='100%'>
            <asp:Repeater runat="server" ID="rptSireChapter">
            <ItemTemplate>
            <tr>
            <td style='text-align:right; width:30px;'> 
                <asp:CheckBox runat="server" id="chkSireChapter" CssClass='<%#Eval("Id")%>' />
            </td>
            <td><%#Eval("ChapterName")%></td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <asp:Button runat="server" ID="btnSireSave" Text="Save" onclick="btnSireSave_Click" style="border:none; background-color:Green; color:White;padding:5px; width:100px; margin-top:5px;" />   
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
               <asp:ImageButton runat="server" ID="btnSireClose" Text="Close" onclick="btnSireClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
             </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSireSave" />
            <asp:PostBackTrigger ControlID="btnSireClose" />
        </Triggers>
        </asp:UpdatePanel>
         </div>
         </center>
        
</div>
</div>
</form>
</body>
</html>

