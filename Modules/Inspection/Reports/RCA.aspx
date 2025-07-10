<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RCA.aspx.cs" Inherits="Reports_RCA" Title="Audit Trend Analysis Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Accident Report</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style>
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body style="margin:0px; margin-bottom:20px;">
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top">
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
                        <td style="text-align:center;" >Root Cause</td>
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
                    <asp:Repeater runat="server" ID="rptList">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:left;" >&nbsp;<%#Eval("InspectionNo").ToString()%></td>
                            <td>
                            &nbsp;<%#Eval("Deficiency").ToString()%>
                            <span style='color:Red'><i><%#Eval("CausesList").ToString()%></i></span>
                            </td>
                            <td style="text-align:center;" >
                                <asp:ImageButton runat="server" ID="btnRC"  Visible='<%#(Eval("Status").ToString()=="O")%>' CssClass='<%#Eval("InspectionDueId").ToString() + "|" + Eval("Id").ToString() + "|" + Eval("TableName").ToString()%>' ImageUrl="~/Images/arrow_right.png" OnClick="Select_RootCause" />
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
        <asp:Label runat="server" ID="lblRowsCount" ForeColor="Red" style="float:left" onclick='CallAfterRefresh();'></asp:Label>
    </div>
    <div style="position:absolute;top:0px;left:0px; width:100%;" id="dvRootCause" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:400px;  height:400px;padding :5px; text-align :center;background : white; z-index:150;top:0px; border:solid 0px black;  ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <center >
            <div style="padding:6px; background-color:#D6EBFF; font-size:14px;"><b>Select Root Cause</b></div>
            <div style="height:362px; margin-top:5px;">
            <div style="height:332px; margin-top:5px; overflow-x:hidden; overflow-y:scroll;">
            <table cellpadding="0" cellspacing="0" border="0" width='100%'>
            <asp:Repeater runat="server" ID="rptRootCause">
            <ItemTemplate>
            <tr>
            <td style='text-align:right; width:30px;'> 
                <asp:CheckBox runat="server" id="chkRootCause" CssClass='<%#Eval("CauseId")%>' />
            </td>
            <td><%#Eval("CauseName")%></td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" OnClientClick="this.value='Loading..';" style="border:none; background-color:Green; color:White;padding:5px; width:100px; margin-top:5px;" />   
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
               <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" onclick="btnClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
             </center>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="ImageButton1" />
        </Triggers>
        </asp:UpdatePanel>
         </div>
         </center>
        
</div>
    </td>
    </tr>
    </table>
    <script type="text/javascript">

    function msieversion() {
        var ua = window.navigator.userAgent
        var msie = ua.indexOf("MSIE ")

        if (msie > 0)      // If Internet Explorer, return version number
            return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
        else                 // If another browser, return 0
            return 0

    }
    var Version = msieversion();
    var CSSName = "class";
    if (Version == 7) { CSSName = "className" };


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
        if (isNaN(pos))
        { pos = 0; }
        if (pos > 0) {
            document.getElementById(ctlid).scrollTop = pos;
        }
    }

    function SetScrollPos(ctl) {
        setCookie(ctl.id, ctl.scrollTop, 1);
    }

    function CallAfterRefresh() {
       SetLastFocus("dv_ATA");
    }
    
    // Function will call after Update Panel Post back;
    function SetOnLoad() {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CallAfterRefresh);
        try {
            if (Page_CallAfterRefresh != null)
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_CallAfterRefresh);
        } catch (err)
        { }
    }
   
</script>
</div>
</form>
</body>
</html>

