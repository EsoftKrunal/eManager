<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CriticalComponentShutdownRequest_VSL.aspx.cs" Inherits="CriticalComponentShutdownRequest_VSL" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>

    <%--<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>--%>
    <script src="JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .myrow
        {
         
        }
        .myrow:hover
        {
            background-color: #ffff99;
        }
        .active
        {
            background-color:#c2c2c2;
        }
        .dvVessel
        {
            padding:5px; 
            margin-bottom:1px; 
            background-color:#cce6ff; 
            color:#444;
        }
        .active
        {
            background-color:#ffad33;
            color:white;
        }
        .verified
        {
            background-color:#e6ffee;
        }
        .notverified
        {
            background-color:#ffebe6;
        }
        .Component
        {
            cursor:pointer;
        }
        .SelComponent
        {
            background-color:#ff704d;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function opendetails(tr) {
            var RP = tr.getAttribute('act');
            var HID = tr.getAttribute('hid');
            var VC = tr.getAttribute('vsl');
            if (RP == 'R') {
                RP = 'R';
                window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');

            }
            if (RP == 'P') {
                RP = 'P';
                window.open('PopupHistoryJobDetails_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }

        }

        function ReloadPage() {
            document.getElementById('btnViewReport').click();
        }
        function ShowComments(Comments) {
            alert(Comments);
        }
    </script>
    <style type="text/css">
        .withborder td
        {
            border: solid 1px #e9e9e9;
        }
    </style>
</head>
<body style="font-family: Calibri; font-size: 14px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" ></asp:ScriptManager>
    <div>
        <div style="text-align: center; padding: 6px; background-color: #4da6ff; color: White;">
            <b>Critical Component Shutdown Request</b>
        </div>
        <table cellpadding="0" cellspacing="0" width="100%" border="1" height="100%" style="border-collapse: collapse">
            <tr>
                <td style="vertical-align: top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat=server>
                        <ContentTemplate>
                        
                    <asp:Button ID="btnTemp" runat="server" OnClick="btnTemp_OnClick" Style="display: none;" />

                    <div style="height: 25px; overflow-y: scroll; overflow-x: hidden; background-color: #e6f2ff;padding:5px;">
                        <table cellspacing="0" border="0" cellpadding="2" style="width: 100%;border-collapse: collapse; height: 25px;">
                            <colgroup>
                                <tr>
                                    <td style="text-align: right">
                                        Component Code :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCompCode" runat="server" MaxLength="15" Width="150px"></asp:TextBox>
                                    </td>
                                     <td style="text-align: right">
                                        Component Name :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtCompName" runat="server" Width="250px" ></asp:TextBox>
                                    </td> 
                                    <td style="text-align: right">
                                        Duration :
                                    </td>
                                    <td style="text-align: left">
                                                <asp:TextBox runat="server" ID="txtDt1" Width="85px" MaxLength="12" style="text-align:center" onfocus="showCalendar('',this,this,'','holder1',0,0,1)"></asp:TextBox>
                                                -
                                                <asp:TextBox runat="server" ID="txtDt2" Width="85px" MaxLength="12"  style="text-align:center" onfocus="showCalendar('',this,this,'','holder1',0,0,1)"></asp:TextBox>
                                                
                                    </td>
                                     <td style="text-align: right">
                                        Approval Status :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList runat="server" ID="ddlAppStatus" Width="80px">
                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td> 
                                    <td>
                                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search"  />
                                        <asp:Button runat="server" ID="btnShowAdd" OnClick="btnShowAdd_Click" Text="Add New"  />
                                        
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    
                    <div style="height:50px; overflow-y: scroll; overflow-x: hidden; background-color: #0066CC;color:#fff;">
                        <table cellspacing="0" class="withborder" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse; font-size: 13px; height:50px; font-weight:bold;">
                            <colgroup>
                                <col style="width: 30px;" />
                                <col style="width: 80px;" />
                                <col />
                                <col style="width:90px;" />
                                <col style="width:120px;" />
                                <col style="width:80px;" />
                                <col style="width:150px;" />
                                <col style="width:150px;" />
                                <col style="width:150px;" />

                                <tr align="left">
                                   <td style="text-align: center"><img src="Images/magnifier.png" style="height: 12px;margin-top:5px;" /></td>                                   
                                   <td>Comp.Code</td>
                                   <td>Component Name</td>
                                   <td>Request Dt.</td>
                                   <td>Master/CE Name</td>
                                   <td>Planned Shutdown (Total Hours)</td>
                                   <td>Maintenance Commenced (Ship’s LT)</td>
                                   <td>Maintenance Completed (Ship’s LT)</td>
                                   <td>Approved</td>                                 
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div style="height: 600px; overflow-y: scroll; overflow-x: hidden;">
                        <table cellspacing="0" class="withborder" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse; font-size: 13px; color:#777;">
                            <colgroup>
                                <col style="width: 30px;" />
                                <col style="width: 80px;" />
                                <col />
                                <col style="width:90px;" />
                                <col style="width:120px;" />
                                <col style="width:80px;" />
                                <col style="width:150px;" />
                                <col style="width:150px;" />                                
                                <col style="width:150px;" />
                            </colgroup>
                            <asp:Repeater ID="rptComponentUnits" runat="server">
                                <ItemTemplate>
                                    <tr>
                                       <td style="text-align: center; padding-top: 5px;">
                                            <a href="VSL_CriticalEqpShutdownReq.aspx?CompCode=<%#Eval("ComponentCode").ToString().Trim()%>&VC=<%#Eval("VesselCode")%>&&SD=<%#Eval("ShutdownId")%>" target="_blank">
                                                <img src="Images/magnifier.png" style="height: 12px;cursor: pointer; border:none;" />
                                            </a>
                                        </td>
                                        <td align="left">
                                            <%#Eval("ComponentCode")%>
                                        </td>
                                        <td align="left">
                                            <%#Eval("ComponentName")%>
                                        </td>
                                        <td align="left">
                                            <b><%#Eval("requestDate_s")%></b>
                                        </td>
                                        <td align="left">
                                            <%#Eval("MasterCEName")%>
                                         </td>
                                         <td align="center">
                                            <%#Eval("Pl_ShutDownTotalHrs")%>
                                         </td>
                                         
                                        <td align="center"><%# ToDateTimeString(Eval("Ma_CommencedDateTime"))%></td>
                                        <td align="center"><%# ToDateTimeString(Eval("Ma_CompletedDateTime"))%></td> 

                                        <td align="left">
                                            <span style="color: maroon">
                                                <span style="color:Green" runat="server" visible='<%#(!(Convert.IsDBNull(Eval("ApprovedOn"))))%>'>Yes</span>
                                                <span style="color:Red" runat="server" visible='<%#(Eval("Approver_Name").ToString() != "") && Convert.IsDBNull(Eval("IssueDate"))%>'>Read Office Comments !</span>
                                            </span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    
                    <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvAddNew" visible="false" >
                    <center>
                    <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                    <div style="position :relative; width:400px; height:400px;overflow-y:hidden;overflow-x:hidden;  padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td style="background-color: #d2d2d2; font-size: 14px; font-weight: bold; height: 25px; vertical-align: middle; text-align: center;">
                                Select Critical Component
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                            <%--<asp:ListBox ID="ListCriticalComponent" runat="server" Height="250px" Width="100%">
                            </asp:ListBox>--%>
                            <div style="width:100%;height:300px;overflow-y:scroll;overflow-x:hidden;border:solid 1px #c2c2c2;">
                            <table width="100%" cellpadding="2" cellspacing="0" >
                            <asp:Repeater ID="ListCriticalComponent" runat="server" >
                            <ItemTemplate>
                                <tr style="text-align:left;" class="Component" onclick="Select(this,'<%#Eval("ComponentCode") %>');" >
                                    <td> <%#Eval("ComponentCode") %></td>
                                    <td> <%#Eval("ComponentName") %></td>
                                </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                            </div>
                            <%--<asp:TextBox ID="txt_CompCode" runat="server" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rew" ControlToValidate="txt_CompCode" ErrorMessage="*" ValidationGroup="v15"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                           <td style="text-align: center; padding-right: 5px; padding-left: 5px;">
                                <asp:HiddenField ID="hfSelectedComponent" runat="server" />

                               <asp:Button ID="btnAdd1" Text="Go" CssClass="btn" runat="server" Width="100px" onclick="btnAdd_Click" CausesValidation="true" ValidationGroup="v15" />&nbsp;
                               <asp:Button ID="btnClose" Text="Close" CssClass="btn" OnClick="btnClose_Click"  runat="server" CausesValidation="false" />
                           </td>
                       </tr>
                       <tr>
                        <td>
                            <asp:Label ID="lblSelCompnentMsg" runat="server" style="color:Red;"></asp:Label>
                        </td>
                       </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnClose" />
                 </Triggers>
                </asp:UpdatePanel>
                    </div> 
                    </center>
                 </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div style="text-align: center; padding: 6px; background-color: #ffffcc">
            &nbsp;
            <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".SpanTotal").click(function () {
                var Vessel = $(this).attr("Vessel");

                $("#hfVessel").val(Vessel);
                $("#hfType").val("1");
                $("#btnTemp").click();
                $(".dvVessel").removeClass("active");
                $(this).parents(".dvVessel").addClass("active");
            });


            $(".SpanUnverified").click(function () {
                var Vessel = $(this).attr("Vessel");
                $("#hfVessel").val(Vessel);
                $("#hfType").val("2");
                $("#btnTemp").click();

                $(".dvVessel").removeClass("active");
                $(this).parents(".dvVessel").addClass("active");
            });

//            $(".Component").click(function () {
//                $(".Component").removeClass("SelComponent");
//                $(this).addClass("SelComponent");
//            });


        });

//        function ComponentClick() {
//            $(".Component").click(function () {
//                alert("HI");
//                $(".Component").removeClass("SelComponent");
//                $(this).addClass("SelComponent");
//            });
//        }

       
    </script>

    <script type="text/javascript">
        function Select(ctrl,code) {
            $("#hfSelectedComponent").val(code);
            $(".Component").removeClass("SelComponent");
            $(ctrl).addClass("SelComponent");
        }
    </script>
    </form>
</body>
</html>