<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="AuditTrendAnalysisReport.aspx.cs" Inherits="Reports_AuditTrendAnalysisReport" Title="Audit Trend Analysis Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" /> 
<script type="text/javascript">
    function DisableMe() {
        DisableAfter();
    }
    function DisableAfter() {
        window.setTimeout(function () {
            //$(ctl).disabled = true;
            //document.getElementById('ctl00_ContentPlaceHolder1_btn_Show').style.display = 'none';
            document.getElementById('btn_Show').setAttribute('value', 'loading...');
            document.getElementById('btn_Show').style.backgroundColor = 'grey';
            document.getElementById('tn_Show').onclick = function () { return false; };

            //$(ctl).css('display', 'none');
        }, 20);
    }
</script>
     </head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div class="text headerband" style="font-family:Arial;font-size:12px;">
             Audit Trend Analysis Report
        </div>
<table width='100%' style="font-family:Arial;font-size:12px;">
<tr>
<td style="width:250px; vertical-align:top;">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td style="text-align: left">
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <table cellpadding="1" cellspacing="0" width="100%" border="1" >
    <tr>
    <td style="text-align: center" valign="bottom">
                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="80px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate"></ajaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"/>
                    &nbsp;
                    <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="80px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate"></ajaxToolkit:CalendarExtender>
                    <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"/>
    </td>
    </tr>
    <tr>
     <td style="text-align: center" valign="bottom">
                    <asp:RadioButton ID="rad_All" runat="server" Checked="true" GroupName="ab" Text="All" />
                    <asp:RadioButton ID="rad_O" runat="server" GroupName="ab" Text="Open" />
                    <asp:RadioButton ID="rad_C" runat="server" GroupName="ab" Text="Closed" />
                </td>
    </tr>
    <tr><td style=" background-color:#e2e2e2; text-align:center">Inspection Group</td></tr>
    <tr>
      <td valign="bottom">
       <div style="height:100px; overflow-y:scroll;">
       <asp:CheckBoxList runat="server" ID="ddlInspGroup" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlInspGroup_OnSelectedIndexChanged" ></asp:CheckBoxList> 
       </div>
      </td>
    </tr>
    <tr><td style=" background-color:#e2e2e2; text-align:center">Inspection</td></tr>
    <tr>
       <td style="text-align: left" valign="bottom">
       <div style="height:220px; overflow-y:scroll;">
        <asp:CheckBoxList runat="server" ID="ddlInsp" CssClass="input_box" ></asp:CheckBoxList> 
        </div>
       </td>
    </tr>
    
    <tr>
      <td style="text-align: center" valign="bottom">
                    <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClientClick="DisableMe(this);" OnClick="btn_Show_Click" Width="100px"   />
                    &nbsp;<asp:Button ID="btn_Clear" runat="server" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="80px"   />
                    </td>
    </tr>
    </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btn_Show" />
    </Triggers>
    </asp:UpdatePanel>
</td>
</tr>
</table>
</td>
<td style="vertical-align:top">
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
<ajaxToolkit:TabPanel ID="panel1" runat="server" HeaderText="RCA">
<ContentTemplate>
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                    <td style="text-align: left">
                                    <div style=" height:25px; overflow-y:scroll; overflow-x:hidden;">
                                    <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='120px' />
                                            <col/>
                                            <col width='80px' />
                                            <col width='60px' />
                                            <col width='17px' />
                                        </colgroup>
                                        <tr class= "headerstylegrid" style="font-weight:bold;" >
                                            <td style="text-align:center;" >Source</td>
                                            <td>&nbsp;Observation</td>
                                            <td style="text-align:center;" >Root Cause</td>
                                            <td style="text-align:center;" >Status</td>
                                            <td style="text-align:center;" >&nbsp;</td>
                                        </tr>
                                        </table>
                                    </div>
                                    <div style=" height:340px; overflow-y:scroll; overflow-x:hidden;"  class='ScrollAutoReset' id='dv_ATA' onscroll="SetScrollPos(this);">
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
                                                    <asp:ImageButton runat="server" ID="btnRC"  Visible='<%#(Eval("Status").ToString()=="O")%>' CssClass='<%#Eval("InspectionDueId").ToString() + "|" + Eval("Id").ToString() + "|" + Eval("TableName").ToString()%>' ImageUrl="~/Modules/HRD/Images/arrow_right.png" OnClick="Select_RootCause" />
                                                </td>
                                                <td style="text-align:center;" >
                                                    <asp:Label runat="server" Text="Closed" Visible='<%#(Eval("Status").ToString()=="C")%>' ForeColor="Green"></asp:Label>
                                                    <asp:Label runat="server" Text="Open" Visible='<%#(Eval("Status").ToString()=="O")%>' ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        </table>
                                        </div>
                                    <div style=" height:20px; text-align:right; padding:5px">
                                            <asp:Label runat="server" ID="lblRowsCount" ForeColor="Red" style="float:left" onclick='CallAfterRefresh();'></asp:Label>
                                    </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
</table>
<div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvRootCause" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:400px;  height:420px;padding :5px; text-align :center;background : white; z-index:150;top:25px; border:solid 0px black;  ">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
            <center >
            <div style="padding:6px;  font-size:14px;" class="text headerband"><b>Select Root Cause</b></div>
            <div style="height:382px; margin-top:5px;">
            <div style="height:352px; margin-top:5px; overflow-x:hidden; overflow-y:scroll;">
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
            <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" OnClientClick="this.value='Loading..';" style="border:none; padding:5px; width:100px; margin-top:5px;"  CssClass="btn"/>   
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
               <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" onclick="btnClose_Click" ImageUrl="~/Modules/HRD/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
             </center>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="ImageButton1" />
        </Triggers>
        </asp:UpdatePanel>--%>
         </div>
         </center>
        
</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Sire-Chapter">
<ContentTemplate>
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" valign="top" >
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                    <td style="text-align: left">
                                    <div style=" height:25px; overflow-y:scroll; overflow-x:hidden;">
                                    <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='120px' />
                                            <col/>
                                            <col width='80px' />
                                            <col width='60px' />
                                            <col width='17px' />
                                        </colgroup>
                                        <tr class= "headerstylegrid" style="font-weight:bold;" >
                                            <td style="text-align:center;" >Source</td>
                                            <td>&nbsp;Observation</td>
                                            <td style="text-align:center;" >Sire Chap.</td>
                                            <td style="text-align:center;" >Status</td>
                                            <td style="text-align:center;" >&nbsp;</td>
                                        </tr>
                                        </table>
                                    </div>
                                    <div style=" height:340px; overflow-y:scroll; overflow-x:hidden;"  class='ScrollAutoReset' id='Div1' onscroll="SetScrollPos(this);">
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
                                        </div>
                                    <div style=" height:20px; text-align:right; padding:5px">
                                            <asp:Label runat="server" ID="lblRowsCount1" ForeColor="Red" style="float:left" onclick='CallAfterRefresh();'></asp:Label>
                                    </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
</table>
<div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvSireChapter" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:400px;  height:420px;padding :5px; text-align :center;background : white; z-index:150;top:25px; border:solid 0px black;  ">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <center >
            <div style="padding:6px; background-color:#D6EBFF; font-size:14px;"><b>Select Sire Chapters</b></div>
            <div style="height:382px; margin-top:5px;">
            <div style="height:352px; margin-top:5px; overflow-x:hidden; overflow-y:scroll;">
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
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Report">
<ContentTemplate>

<ajaxToolkit:TabContainer ID="TabContainer4" runat="server">
<ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="RCA">
<ContentTemplate>

<ajaxToolkit:TabContainer ID="TabContainer2" runat="server">
<ajaxToolkit:TabPanel ID="panel12" runat="server" HeaderText="Chart">
<ContentTemplate>
<asp:Chart ID="TAChart" runat="server" Width='900px' Height="340px" >
            <Series>
                <%--<asp:Series Name="TAData" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" ></asp:Series>--%>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
 </asp:Chart>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="panel11" runat="server" HeaderText="Data">
<ContentTemplate>
<div style="height:45px; width:100%; overflow-x:hidden; overflow-y:scroll; background-color:#c2c2c2">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <tr>
             <asp:Literal runat="server" ID="litHeaderMain"></asp:Literal>
            </tr>
            <tr>
             <asp:Literal runat="server" ID="litHeaders"></asp:Literal>
            </tr>
        </table>
        </div>
<div style="height:300px; width:100%; overflow-x:hidden; overflow-y:scroll">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <asp:Literal runat="server" ID="litDataRows"></asp:Literal>
        </table>
        </div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</ContentTemplate>
</ajaxToolkit:TabPanel>

<ajaxToolkit:TabPanel ID="TabPanel7" runat="server" HeaderText="Sire-Chapter">
<ContentTemplate>
<ajaxToolkit:TabContainer ID="TabContainer3" runat="server">
<ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Chart">
<ContentTemplate>
<asp:Chart ID="Chart1" runat="server" Width='900px' Height="340px" >
            <Series></Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
 </asp:Chart>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Data">
<ContentTemplate>
<div style="height:45px; width:100%; overflow-x:hidden; overflow-y:scroll; background-color:#c2c2c2">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <tr>
             <asp:Literal runat="server" ID="Literal1"></asp:Literal>
            </tr>
            <tr>
             <asp:Literal runat="server" ID="Literal2"></asp:Literal>
            </tr>
        </table>
        </div>
<div style="height:300px; width:100%; overflow-x:hidden; overflow-y:scroll">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <asp:Literal runat="server" ID="LitDataRows1"></asp:Literal>
        </table>
        </div>
</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>
</ContentTemplate>
</ajaxToolkit:TabPanel>

</ajaxToolkit:TabContainer>

</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="Analysis">
<ContentTemplate>
<div style="height:380px">
<asp:UpdatePanel runat="server" ID="r">
<ContentTemplate>
<asp:Button ID="load" runat="server" Text="Load Chart Data" OnClick="load_click" CssClass="btn" OnClientClick="this.value='Processing...'"/>
<asp:Button ID="download" runat="server" Text="Download Chart Data" OnClick="download_click" CssClass="btn" OnClientClick="this.value='Processing...'"/>
<asp:Chart ID="AnalysisChart" runat="server" Width='900px' Height="340px" >
            <Series>
                <%--<asp:Series Name="TAData" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" ></asp:Series>--%>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
 </asp:Chart>

 </ContentTemplate>
 <Triggers>
 <asp:PostBackTrigger ControlID="download" />
 </Triggers>
</asp:UpdatePanel>
</div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
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
</form>
</body>
</html>
